
using MinisAPI.Models;
using MinisAPI.Models.DTO;
using AutoMapper;

namespace MinisAPI.Models.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PaintItem, PaintItemDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.HexCode, opt => opt.MapFrom(src => src.HexCode))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand));
        }

    }
}