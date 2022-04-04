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
            CreateMap<UserForUpdate, User>();
            CreateMap<FileForRequest, Files>();
            CreateMap<Files, FileForReturn>();
            CreateMap<ServiceForRequest, Services>();
            CreateMap<Services, ServiceForReturn>();
            CreateMap<ServiceForUpdate, Services>();
            CreateMap<Services, ServiceForUpdate>();
            CreateMap<FileForDownload, Files>();
            CreateMap<Files, FileForDownload>();
        }
    }
}