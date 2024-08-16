using BankingManagementSystem.DbService.Model;
using BankingManagementSystem.Services.Features.Account;
using BankingManagementSystem.Services.Features.AdminUser;
using BankingManagementSystem.Services.Features.Generate;
using BankingManagementSystem.Services.Features.State;
using BankingManagementSystem.Services.Features.TownShip;
using BankingManagementSystem.Services.Features.Transaction;
using BankingManagementSystem.Services.Features.User;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7237", "http://localhost:5023") // or AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

builder.Services.AddDbContext<AppDbContext>(
    opt => { opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")); }, ServiceLifetime.Transient,
    ServiceLifetime.Transient);

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AdminUserService>();
builder.Services.AddScoped<StateService>();
builder.Services.AddScoped<TownShipService>();
builder.Services.AddScoped<GenerateService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
