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
        private readonly ILogger<PacijentService> _logger;

        public PacijentService(IPacijentRepository pacijentRepository, IMapper mapper, ILekarRepository lekarRepository, ILogger<PacijentService> logger)
        {
            _pacijentRepository = pacijentRepository;
            _mapper = mapper;
            _lekarRepository = lekarRepository;
            _logger = logger;
        }

        public Result<PacijentDtoResponse> IzmeniPacijenta(int id, IzmeniPacijentaDtoRequest pacijentRequest)
        {
            _logger.LogInformation($"Izmena pacijenta sa Id-jem {id}.");

            var proveraPodataka = ValidacijaPodataka(pacijentRequest.Jmbg, pacijentRequest.DatumKreiranja, pacijentRequest.IsActive);

            _logger.LogError("Greska prilikom validacije podataka");
            if (proveraPodataka.IsFailure)
            {
                return Result<PacijentDtoResponse>.Failure(proveraPodataka.Errors);
            }
            
            var dataPacijent = _mapper.Map<Pacijent>(pacijentRequest);

            var lekar = _lekarRepository.VratiLekaraPoId(pacijentRequest.LekarId);
            if (lekar == null)
            {
                return Result<PacijentDtoResponse>.FailureMessage("Izabrani lekar ne postoji u sistemu");
            }

            if(lekar.Pacijenti.Count() > 4)
            {
                return Result<PacijentDtoResponse>.FailureMessage("Izabrani lekar je dostigao maksimalan broj pacijenata");
            }

            lekar.Pacijenti.Add(dataPacijent);


            var izmenjeniPacijent = _pacijentRepository.IzmeniPacijenta(id, dataPacijent);
            if (izmenjeniPacijent == null)
            {
                return Result<PacijentDtoResponse>.FailureMessage("Pacijent sa izabranim Id-jem ne postoji u sistemu");
            }
            var response = _mapper.Map<PacijentDtoResponse>(izmenjeniPacijent);
            _logger.LogInformation($"Uspesna izmena za pacijenta sa Id-jem {id}");
            return Result<PacijentDtoResponse>.Success(response);
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

        public Result<PacijentDtoResponse> UnesiPacijenta(UnesiPacijentaDtoRequest pacijentRequest)
        {

            var proveraPodataka = ValidacijaPodataka(pacijentRequest.Jmbg, pacijentRequest.DatumKreiranja, pacijentRequest.IsActive);

            if (proveraPodataka.IsFailure)
            {
                return Result<PacijentDtoResponse>.Failure(proveraPodataka.Errors);
            }
           
            var dataPacijent = _mapper.Map<Pacijent>(pacijentRequest);

            var lekar = _lekarRepository.VratiLekaraPoId(pacijentRequest.LekarId); 
            if (lekar == null)
            {
                return Result<PacijentDtoResponse>.FailureMessage("Izabrani lekar ne postoji u sistemu");
            }

            if (lekar.Pacijenti.Count > 4)
            {
                return Result<PacijentDtoResponse>.FailureMessage("Izabrani lekar je dostigao maksimalan broj pacijenata");
            }

            lekar.Pacijenti.Add(dataPacijent);
            
            var kreiraniPacijent = _pacijentRepository.UnesiPacijenta(dataPacijent);
            var response = _mapper.Map<PacijentDtoResponse>(kreiraniPacijent);
            return Result<PacijentDtoResponse>.Success(response);
        }

        private Result ValidacijaPodataka(string jmbg, DateTime datumKreiranja, bool isActive = true)
        {
            var validacijaPodataka = new List<String>();
            
                if (string.IsNullOrEmpty(jmbg) || jmbg.Length != 13)
                {
                    validacijaPodataka.Add("Maticni broj mora imati tacno 13 karaktera");
                }

                if (datumKreiranja.Date > DateTime.Now)
                {
                    validacijaPodataka.Add("Datum unosa ne sme biti u buducnosti");
                }

                if (!isActive)
                {
                    validacijaPodataka.Add("Novi ili izmenjeni pacijent mora biti aktivan");
                }
            
            if(validacijaPodataka.Any())
            {
                return Result.Failure(validacijaPodataka);
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
