using Microsoft.EntityFrameworkCore;
using SignalRApi.DAL;
using SignalRApi.Hubs;
using SignalRApi.Model;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Veritabaný baðlantý ayarlarýný yapýyoruz
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// VisitorService gibi uygulama içerisindeki servisleri ekliyoruz
builder.Services.AddScoped<VisitorService>();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed((host)=>true).AllowCredentials();
    });
});

// Add Controllers
builder.Services.AddControllers();

// SignalR için gerekli ayarlar (eðer kullanýyorsanýz)
builder.Services.AddSignalR();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

// Eðer SignalR Hub kullanýyorsanýz burada ekleyin
 app.MapHub<VisitorHub>("/VisitorHub");

app.Run();
