using AutoMapper;
using Shop.Model;
using Shop.Model.Dtos;

namespace Shop
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<IEnumerable<BrandDto>, BrandDto>().ReverseMap();
            CreateMap<IEnumerable<BrandDto>, CategoryDto>().ReverseMap();
            CreateMap<IEnumerable<BrandDto>, ProductDto>().ReverseMap();

        }
    }
}
