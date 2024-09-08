using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Container;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using TraversalCoreProje.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.IO;
using FluentValidation;
using DTOLayer.DTOs.AnnouncementDTOs;
using BusinessLayer.ValidationRules;
using FluentValidation.AspNetCore;
using TraversalCoreProje.CQRS.Handlers.DestinationHandlers;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.Extensions.DependencyInjection;
using TraversalCoreProje.CQRS.Handlers.GuideHandlers;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Serilog yapýlandýrmasýný ekleyin
var path = Directory.GetCurrentDirectory();
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Error() // Minimum log seviyesi
	.WriteTo.Console()    // Konsola yazma
	.WriteTo.File($"{path}\\Logs\\Log1.txt", rollingInterval: RollingInterval.Day) // Dosyaya yazma
	.CreateLogger();

// Serilog'u ASP.NET Core'a entegre edin
builder.Host.UseSerilog(); // Bu satýrý ekleyin

// Add services to the container.
builder.Services.AddLogging(loggingBuilder =>
{
	loggingBuilder.ClearProviders(); // Varsayýlan saðlayýcýlarý temizle
	loggingBuilder.AddSerilog();     // Serilog'u ekle
	loggingBuilder.AddDebug();
});

builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>()
	.AddEntityFrameworkStores<Context>()
	.AddErrorDescriber<CustomIdentityValidator>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<Context>();
builder.Services.AddHttpClient();
builder.Services.ContainerDependencies();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.CustomerValidator();
builder.Services.AddScoped<GetAllDestinationQueryHandler>();
builder.Services.AddScoped<GetDestinationByIDQueryHandler>();
builder.Services.AddScoped<CreateDestinationCommandHandler>();
builder.Services.AddScoped<RemoveDestinationCommandHandler>();
builder.Services.AddScoped<UpdateDestinationCommandHandler>();
builder.Services.AddMediatR(cfg => {
	// MediatR için özel konfigürasyonlar ekleyebilirsiniz.
	cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});




builder.Services.AddControllersWithViews().AddFluentValidation();

builder.Services.AddMvc(config =>
{
	var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddLocalization(
	opt =>{
		opt.ResourcesPath = "Resources";
	}
);
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
var supportedCultures = new[] { "tr", "en" ,"fr","es","gr","de"};
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=SignIn}/{id?}");

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
