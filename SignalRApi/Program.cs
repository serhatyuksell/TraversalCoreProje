using Microsoft.EntityFrameworkCore;
using SignalRApi.DAL;
using SignalRApi.Hubs;
using SignalRApi.Model;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Veritaban� ba�lant� ayarlar�n� yap�yoruz
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// VisitorService gibi uygulama i�erisindeki servisleri ekliyoruz
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

// SignalR i�in gerekli ayarlar (e�er kullan�yorsan�z)
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

// E�er SignalR Hub kullan�yorsan�z burada ekleyin
 app.MapHub<VisitorHub>("/VisitorHub");

app.Run();
