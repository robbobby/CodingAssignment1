using Api.Hubs.ExportJob;
using Db;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IExportJobService, ExportJobService>();
builder.Services.AddScoped<IExportJobDb, ExportJobDb>();
builder.Services.AddSingleton<ExportJobPollingService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<ExportJobPollingService>());
builder.Services.AddSignalR();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();

app.UseCors();

app.MapHub<ExportJobHub>("/ws/exportJobHub");

PollingServiceSetup.ExportJobBootstrap(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
