using Microsoft.EntityFrameworkCore;
using ShopeeFoodChap2.Data;
using ShopeeFoodChap2.Repositories.Implement;
using ShopeeFoodChap2.Repositories.Interfaces;
using ShopeeFoodChap2.Services.Implement;
using ShopeeFoodChap2.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShopeeFoodDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

// Add Repo
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

// Add Serv
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
