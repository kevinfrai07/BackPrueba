using FluentValidation;
using ProviderWks.Domain.Entities;
using ProviderWks.Infraestructure.Extension;
using Service.ProviderWks.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerOpenAPI(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddDbContext<IApplicationDbContext,ApplicationDbContext>();
builder.Services.AddCors(o => o.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .WithMethods("GET", "POST", "PUT", "DELETE")
    .SetIsOriginAllowed(a => true);
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        setupAction.SwaggerEndpoint("/swagger/OpenAPISpecification/swagger.json", "ProviderWks");
    });
}
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseStatusCodePages();

app.Run();
