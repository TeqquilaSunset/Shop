using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Shop.Repositories;
using Shop.Model;
using Shop.Services;
using Microsoft.OpenApi.Models;
using CatalogMicroservice.Services.Intefraces;
using CatalogMicroservice.Services;
using CatalogMicroservice.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Shop
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSwaggerGen(options =>
            {
            });

            builder.Services.AddControllers();

            //Добавления контекста базы данных
            builder.Services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Репозиторий
            builder.Services.AddScoped(typeof(IRepository<Brand>), typeof(BrandRepository));
            builder.Services.AddScoped(typeof(IRepository<Product>), typeof(ProductRepository));
            builder.Services.AddScoped(typeof(IRepository<Category>), typeof(CategoryRepository));

            //Сервис
            builder.Services.AddTransient<IBrandService, BrandService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ICatalogService, CatalogService>();

            //Авто маппер
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //Аторизация
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!))
                    };
                    //Чтение токена из куков
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.ContainsKey("auth_access_token"))
                            {
                                context.Token = context.Request.Cookies["auth_access_token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                })
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // optional
                        options.Cookie.HttpOnly = true;
                        options.SlidingExpiration = true;
                    });

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

            app.UseAuthorization();
            app.UseAuthentication();

            app.Run();
        }
    }
}