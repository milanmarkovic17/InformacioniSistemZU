using AutoMapper;
using InformacioniSistemZU.Dtos.Requests;
using InformacioniSistemZU.Dtos.Responses;
using InformacioniSistemZU.Models;

namespace InformacioniSistemZU.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Lekar, LekarDtoResponse>().ReverseMap();
            CreateMap<UnesiLekaraDtoRequest, LekarDtoResponse>().ReverseMap();
            CreateMap<UnesiLekaraDtoRequest, Lekar>();
            CreateMap<IzmeniLekaraDtoRequest, Lekar>();
            CreateMap<Pacijent, PacijentDtoResponse>();
            CreateMap<UnesiPacijentaDtoRequest, Pacijent>();
            CreateMap<IzmeniPacijentaDtoRequest, Pacijent>();
            CreateMap<Pregled, PregledDtoResponse>();
            CreateMap<UnesiPregledDtoRequest, Pregled>();
            CreateMap<IzmeniPregledDtoRequest, Pregled>();
            CreateMap<Lekar, LekarPretragaDtoResponse>();
        }
    }
}
