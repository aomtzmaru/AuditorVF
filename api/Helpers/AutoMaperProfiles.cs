using api.Dtos;
using api.Models;
using AutoMapper;

namespace api.Helpers
{
    public class AutoMaperProfiles: Profile
    {
        public AutoMaperProfiles()
        {
            CreateMap<UserForRegister, User>();
            CreateMap<User, UserForReturn>();
            CreateMap<ServiceForRequest, Services>();
            CreateMap<Services, ServiceForReturn>();
        }
    }
}