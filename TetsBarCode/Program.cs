using Microsoft.EntityFrameworkCore;
using TetsBarCode.Controllers;
using TetsBarCode.Data;
using static TetsBarCode.Controllers.BarcodeController;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4200", 
                "http://localhost:63854",  
                "http://localhost:5222", 
                "http://localhost:5001",  
                "http://localhost:44337",
                "http://localhost:7054"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
//builder.Services.AddScoped<ErrorLogService>();
builder.Services.AddDbContext<BarcodeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
