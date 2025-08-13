using ContratacaoService.Infrastructure;
using ContratacaoService.Infrastructure.Data;
using ContratacaoService.Infrastructure.Startup;

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

app.MapGet("/health", () => Results.Ok("ok"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
