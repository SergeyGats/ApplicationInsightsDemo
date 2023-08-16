using ApplicationInsightsDemo.BusinessLogic.Models.User;
using ApplicationInsightsDemo.WebApi.Dtos.User;
using AutoMapper;

namespace ApplicationInsightsDemo.WebApi.AutoMapper.Profiles
{
    public class UserDtosProfile : Profile
    {
        public UserDtosProfile()
        {
            CreateMap<UserModel, UserDto>();
            CreateMap<UserCreateDto, UserCreateModel>();
            CreateMap<UserUpdateDto, UserUpdateModel>();
        }
    }
}