

using PropostaService.Infrastructure;
using PropostaService.Infrastructure.Data;
using PropostaService.Infrastructure.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();

await app.Services.ApplyMigrationsAsync<AppDbContext>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
