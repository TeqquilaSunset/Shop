using AuthenticatorAPI.Models;
using AuthenticatorAPI.Models.Request;
using AutoMapper;

namespace AuthenticatorAPI
{
    /// <summary>
    /// Конфигурация автомаппера
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, RegisterRequest>().ReverseMap();
        }
    }
}
