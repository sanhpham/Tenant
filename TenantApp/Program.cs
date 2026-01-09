using Microsoft.EntityFrameworkCore;
using TenantApp.Infrastructure;
using TenantApp.Infrastructure.Persistence;
using TenantApp.Infrastructure.Repositories;
using TenantApp.Infrastructure.Tenancy;
using TenantApp;
using TenantApp.Middleware;
using TenantApp.Application.Abstractions;
using TenantApp.Application.Abstractions.Repositories;
using TenantApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// DB context
string connStr = builder.Configuration.GetSection(nameof(ConnectionStringSetting)).Get<ConnectionStringSetting>()?.DefaultConnection ?? "";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(connStr));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<TenantMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
