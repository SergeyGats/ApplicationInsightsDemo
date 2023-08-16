using ApplicationInsightsDemo.BusinessLogic.Models.User;
using ApplicationInsightsDemo.DataAccess.DataModels;
using ApplicationInsightsDemo.DataAccess.Entities;
using AutoMapper;

namespace ApplicationInsightsDemo.BusinessLogic.AutoMapper.Profiles
{
    public class UserModelsProfile : Profile
    {
        public UserModelsProfile()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserCreateModel, UserEntity>();
            CreateMap<UserUpdateModel, UserEntity>();
            CreateMap<UserLoginInfoDataModel, UserLoginInfoModel>();
        }
    }
}