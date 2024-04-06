using AltaVision.Logger;
using AltaVisionBackEnd.DataAcessLayer.DataAccess;
using AltaVisionBackEnd.DataAcessLayer.Interfaces;
using Microsoft.AspNetCore.Cors;
using SolarEcoBackEnd.DataAcessLayer.Interfaces;
using SolarEcoBackEnd.DB;
using SolarEcoBackEnd.NewFolder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var MyallowSpecific = "_MyallowSpecific";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_MyallowSpecific",
        builder =>
        {
            builder
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    // Add other logging providers if needed
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEmailsender,Emailsender>();
builder.Services.AddScoped<ICustomerDB, CustomerDB>();
builder.Services.AddScoped<IAdminDB, AdminDB>();
builder.Services.AddScoped<IAppointmentDB, AppointmentDB>();
builder.Services.AddScoped<IPredictionsDB, PredictionsDB>();

builder.Services.AddScoped<ISolarPanelDB, SolarPanelDB>();
builder.Services.AddScoped<ILogs, Logs>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseDeveloperExceptionPage();
app.UseCors(MyallowSpecific);
app.Run();

