using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

using rinha_2024_q1;
using rinha_2024_q1.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup DbContext
builder.Services.AddDbContextFactory<RinhaDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Basic json setup
builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

var app = builder.Build();

app.UseStatusCodePages(async statusCodeContext
    => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
        .ExecuteAsync(statusCodeContext.HttpContext));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Transação Api
app.MapTransacaoApiEndpoints(); 

app.Run();

