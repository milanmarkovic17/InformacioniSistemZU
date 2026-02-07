using AutoMapper;
using InformacioniSistemZU.DataModel.Repositories;
using InformacioniSistemZU.Dtos.Requests;
using InformacioniSistemZU.Dtos.Responses;
using InformacioniSistemZU.Models;
using InformacioniSistemZU.ResultPatern;
using Microsoft.Identity.Client;
using System.Data;
using System.Xml.Linq;

namespace InformacioniSistemZU.BusinessModell.Services
{
    public class PacijentService : IPacijentService
    {
        private readonly IPacijentRepository _pacijentRepository;
        private readonly IMapper _mapper;
        private readonly ILekarRepository _lekarRepository;

        public PacijentService(IPacijentRepository pacijentRepository, IMapper mapper, ILekarRepository lekarRepository)
        {
            _pacijentRepository = pacijentRepository;
            _mapper = mapper;
            _lekarRepository = lekarRepository;
        }

        public PacijentDtoResponse IzmeniPacijenta(int id, IzmeniPacijentaDtoRequest pacijentRequest)
        {
            ValidacijaPodataka(pacijentRequest.Jmbg, pacijentRequest.DatumKreiranja, pacijentRequest.IsActive);
            
            var dataPacijent = _mapper.Map<Pacijent>(pacijentRequest);

            var lekar = _lekarRepository.VratiLekaraPoId(pacijentRequest.LekarId);
            if (lekar == null)
            {
                return null;
            }

            if(lekar.Pacijenti.Count() > 4)
            {
                return null;
            }

            lekar.Pacijenti.Add(dataPacijent);


            var izmenjeniPacijent = _pacijentRepository.IzmeniPacijenta(id, dataPacijent);
            if (izmenjeniPacijent == null)
            {
                return null;
            }
            var response = _mapper.Map<PacijentDtoResponse>(izmenjeniPacijent);
            return response;
        }

        public PacijentDtoResponse ObrisiPacijenta(int id)
        {
            var dataPacijent = _pacijentRepository.IzbrisiPacijenta(id);
            if (dataPacijent == null)
            {
                return null;
            }
            var obrisaniPacijent = _mapper.Map<PacijentDtoResponse>(dataPacijent);
            return obrisaniPacijent;
        }

        public PacijentDtoResponse UnesiPacijenta(UnesiPacijentaDtoRequest pacijentRequest)
        {
            ValidacijaPodataka(pacijentRequest.Jmbg, pacijentRequest.DatumKreiranja, pacijentRequest.IsActive);
           
            var dataPacijent = _mapper.Map<Pacijent>(pacijentRequest);

            var lekar = _lekarRepository.VratiLekaraPoId(pacijentRequest.LekarId); 
            if (lekar == null)
            {
                return null;
            }

            if (lekar.Pacijenti.Count > 4)
            {
                return null;
            }

            lekar.Pacijenti.Add(dataPacijent);
            
            var kreiraniPacijent = _pacijentRepository.UnesiPacijenta(dataPacijent);
            var response = _mapper.Map<PacijentDtoResponse>(kreiraniPacijent);
            return response;
        }

        private Result ValidacijaPodataka(string jmbg, DateTime datumKreiranja, bool isActive = true)
        {
            if (string.IsNullOrEmpty(jmbg) || jmbg.Length != 13)
            {

                return Result.Failure("Maticni broj mora imati tacno 13 karaktera");
            }

            if (datumKreiranja.Date > DateTime.Now)
            {
                return Result.Failure("Datum unosa ne sme biti u buducnosti");
            }

            if (!isActive)
            {
                return Result.Failure("Novi ili izmenjeni pacijent mora biti aktivan");
            }
            return Result.Success();
        }

        public PacijentDtoResponse VratiPacijentaPoId(int id)
        {
            var dataPacijent = _pacijentRepository.VratiPacijentaPoId(id);
            if (dataPacijent == null)
            {
                return null;
            }
            var response = _mapper.Map<PacijentDtoResponse>(dataPacijent);
            return response;
        }

        public IEnumerable<PacijentDtoResponse> VratiSvePacijente()
        {
            var dataPacijenti = _pacijentRepository.VratiSvePacijente();
            var response = _mapper.Map<IEnumerable<PacijentDtoResponse>>(dataPacijenti);
            return response;
        }
    }
}
