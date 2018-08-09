namespace JsonSafe.Mappings
{
    using System;
    using AutoMapper;
    using DbModels;
    using Dtos.JsonModels;
    using Models;

    public class JsonProfile : Profile
    {
        public JsonProfile()
        {
            CreateMap<DbJson, JsonModel>();
            CreateMap<JsonModel, DbJson>();

            CreateMap<CreateJsonRequestDto, JsonModel>()
                .ForMember(destination => destination.Id, options => options.MapFrom(_ => Guid.NewGuid()))
                .ForMember(destination => destination.Created, options => options.MapFrom(_ => DateTime.UtcNow))
                .ForMember(destination => destination.Updated, options => options.MapFrom(_ => DateTime.UtcNow));

            CreateMap<JsonModel, GetJsonResponseDto>();
        }
    }
}
