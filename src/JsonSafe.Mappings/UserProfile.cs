namespace JsonSafe.Mappings
{
    using System;
    using AutoMapper;
    using DbModels;
    using Dtos.UserModels;
    using Models;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DbUser, UserModel>();
            CreateMap<UserModel, DbUser>();

            CreateMap<RegisterUserRequestDto, UserModel>()
                .ForMember(destination => destination.Id, options => options.MapFrom(_ => Guid.NewGuid()))
                .ForMember(destination => destination.Created, options => options.MapFrom(_ => DateTime.UtcNow))
                .ForMember(destination => destination.Updated, options => options.MapFrom(_ => DateTime.UtcNow))
                .ForMember(destination => destination.PasswordHash, options => options.Ignore())
                .ForMember(destination => destination.Salt, options => options.Ignore())
                .ForMember(destination => destination.ApiKey, options => options.Ignore());

            CreateMap<UserModel, RegisterUserResponseDto>()
                .ForMember(destination => destination.Token, options => options.Ignore());
            CreateMap<UserModel, LoginUserResponseDto>()
                .ForMember(destination => destination.Token, options => options.Ignore());
        }
    }
}
