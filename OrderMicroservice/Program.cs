using Microsoft.EntityFrameworkCore;
using OrderMicroservice;
using OrderMicroservice.Model;
using OrderMicroservice.Repositories;
using OrderMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Добавления контекста базы данных
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

//Авто маппер
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//определяет маршруты
app.UseRouting();

//Определяет что делать с маршрутами

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
