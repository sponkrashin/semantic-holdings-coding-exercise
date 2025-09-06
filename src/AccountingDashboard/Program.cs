using System.Reflection;

using Accounting.Persistence.InMemory;

using AccountingDashboard.CQRS;
using AccountingDashboard.Infrastructure;

using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddControllers(options => options.Filters.AddService<ValidationActionFilter>());

builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

RegisterValidatorTypes();
builder.Services.AddScoped<ValidationActionFilter>();

var corsOrigins = builder.Configuration
    .GetSection("CorsOrigins")
    .Get<string[]>();

if (corsOrigins?.Any() == true)
{
    builder.Services.AddCors(options => options.AddDefaultPolicy(policyBuilder => policyBuilder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(corsOrigins)));
}

builder.Services.AddInMemoryDatabaseContext();
builder.Services.AddCQRS();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.UseCors();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();

return;

void RegisterValidatorTypes()
{
    var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
    var validatorType = typeof(IValidator);

    var validatorTypes = assemblyTypes
        .Where(x =>
        {
            if (x.IsAbstract || x.IsGenericTypeDefinition)
            {
                return false;
            }

            return x.GetInterfaces().Any(i => i == validatorType);
        });

    foreach (var type in validatorTypes)
    {
        builder.Services.AddScoped(validatorType, type);
    }
}
