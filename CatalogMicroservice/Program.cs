using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Shop.Repositories;
using Shop.Model;
using Shop.Services;
using Microsoft.OpenApi.Models;
using CatalogMicroservice.Services.Intefraces;
using CatalogMicroservice.Services;
using CatalogMicroservice.Repositories;

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

            //���������� ��������� ���� ������
            builder.Services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            //�����������
            builder.Services.AddScoped(typeof(IRepository<Brand>), typeof(BrandRepository));
            builder.Services.AddScoped(typeof(IRepository<Product>), typeof(ProductRepository));
            builder.Services.AddScoped(typeof(IRepository<Category>), typeof(CategoryRepository));

            //������
            builder.Services.AddTransient<IBrandService, BrandService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ICatalogService, CatalogService>();

            //���� ������
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //���������� ��������
            app.UseRouting();

            //���������� ��� ������ � ����������

            app.UseHttpsRedirection();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}