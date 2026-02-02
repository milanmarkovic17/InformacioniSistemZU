using AutoMapper;
using InformacioniSistemZU.DataModel.Repositories;
using InformacioniSistemZU.Dtos.Requests;
using InformacioniSistemZU.Dtos.Responses;
using InformacioniSistemZU.Models;

namespace InformacioniSistemZU.BusinessModell.Services
{
    public class PregledService : IPregledService
    {
        private readonly IPregledRepository _pregledRepository;
        private readonly IMapper _mapper;

        public PregledService(IPregledRepository pregledRepository, IMapper mapper)
        {
            _pregledRepository = pregledRepository;
            _mapper = mapper;
        }

        public PregledDtoResponse IzmeniPregled(int id, IzmeniPregledDtoRequest pregledRequest)
        {
            var dataPregled = _mapper.Map<Pregled>(pregledRequest);
            if (dataPregled == null)
            {
                return null;
            }
            var izmenjeniPregled = _pregledRepository.IzmeniPregled(id, dataPregled);
            var pregledResponse = _mapper.Map<PregledDtoResponse>(izmenjeniPregled);
            return pregledResponse;
        }

        public PregledDtoResponse ObrisiPregled(int id)
        {
            var dataPregled = _pregledRepository.ObrisiPregled(id);
            if (dataPregled == null)
            {
                return null;
            }
            var pregledResponse = _mapper.Map<PregledDtoResponse>(dataPregled);
            return pregledResponse;
        }

        public PregledDtoResponse UnesiPregled(UnesiPregledDtoRequest pregledRequest)
        {
            var dataPregled = _mapper.Map<Pregled>(pregledRequest);
            var unetiPregled = _pregledRepository.UnesiPregled(dataPregled);
            var pregledResponse = _mapper.Map<PregledDtoResponse>(unetiPregled);
            return pregledResponse;
        }

        /// zanimljiva logika... i lepo si je obradio. Bravo!
        /// ja bih stavio datumOd i datumDo. Pa za tacan datum bi morali da se poklapaju datumOd i datumDo
        public IEnumerable<PregledDtoResponse> VratiPregledePoDatumu(DateOnly tacanDatum, DateOnly? doDatuma)
        {
            //mozes ovde validaciju, ovako kako si napisao i jos uslov da doDatuma ima vrednost tj da nije null
            /*
            if(doDatuma >= tacanDatum)
            {
                return null;
            }*/

            var pregledi = _pregledRepository.VratiSvePreglede();

            if(!doDatuma.HasValue)
            {
                pregledi = pregledi.Where(p => DateOnly.FromDateTime(p.Datum) == tacanDatum);
            }
            else
            {
                pregledi = pregledi.Where(p => DateOnly.FromDateTime(p.Datum) >= tacanDatum && DateOnly.FromDateTime(p.Datum) <= doDatuma);
            }

            //vracas praznu listu u ovakvim slucajevima gde se trazi pretraga, ne null
            if(pregledi == null)
            {
                return null;
            }

            var preglediResponse = _mapper.Map<IEnumerable<PregledDtoResponse>>(pregledi);
            return preglediResponse;
        }

        public IEnumerable<PregledDtoResponse> VratiPregledePoSpecijalnostId(int specijanlnostid)
        {
            var pregledi = _pregledRepository.VratiSvePreglede().Where(x => x.Lekar.SpecijalnostId == specijanlnostid);
            if (pregledi == null)
            {
                return null;
            }
            var preglediResponse = _mapper.Map<IEnumerable<PregledDtoResponse>>(pregledi);
            return preglediResponse;
        }

        public PregledDtoResponse VratiPregledPoId(int id)
        {
            var dataPregled = _pregledRepository.VratiPregledPoId(id);
            if (dataPregled == null)
            {
                return null;
            }
            var pregledResponse = _mapper.Map<PregledDtoResponse>(dataPregled);
            return pregledResponse;
        }

        public IEnumerable<PregledDtoResponse> VratiSvePreglede()
        {
            var sviPregledi = _pregledRepository.VratiSvePreglede();
            var preglediResponse = _mapper.Map<IEnumerable<PregledDtoResponse>>(sviPregledi);
            return preglediResponse;
        }
    }
}
