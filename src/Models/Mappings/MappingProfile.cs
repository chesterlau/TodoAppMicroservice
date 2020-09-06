using AutoMapper;
using TodoAppMicroservice.Models.Dtos;

namespace TodoAppMicroservice.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PatchTodoRequest, Todo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default(string)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != default(string)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.IsComplete, opt => opt.Condition(src => src.IsComplete != null))
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete));

            CreateMap<Todo, GetTodoResponse>();
        }
    }
}
