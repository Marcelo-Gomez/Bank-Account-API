using Api.Configurations;
using Api.Middlewares;
using Application.Validators;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;

#region Builder

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc()
                .AddFluentValidation(opt =>
                {
                    opt.RegisterValidatorsFromAssemblyContaining<DepositRequestValidator>();
                });

var assembly = AppDomain.CurrentDomain.Load("Application");

builder.Services.AddMediatR(assembly);
builder.Services.AddAutoMapper(assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Stone - API de Conta Bancária", Version = "v1" });
});
builder.Services.AddDependencyInjectionConfiguration();

#endregion

#region App

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.DocumentTitle = "Stone - API de Conta Bancária";
    opt.SwaggerEndpoint("../swagger/v1/swagger.json", "Desafio Stone");
});

app.UseAuthorization();

app.UseExceptionHandlerMiddleware();

app.MapControllers();

app.Run();

#endregion