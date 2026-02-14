using AutoMapper;
using InformacioniSistemZU.DataModel.Repositories;
using InformacioniSistemZU.Dtos.Requests;
using InformacioniSistemZU.Dtos.Responses;
using InformacioniSistemZU.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static InformacioniSistemZU.Enums.Enums;

namespace InformacioniSistemZU.BusinessModell.RepositoriesBM
{
    public class LekarService : ILekarService
    {
        private readonly ILekarRepository _lekarRepository;
        private readonly IMapper _mapper;
        private readonly ISpecijalnostRepository _specijalnostRepository;
        private readonly IPregledRepository _pregledRepository;
        private readonly IDaLiJeLekarAktivanDtoRequest _daLiJeLekarAktivan;

        public LekarService(ILekarRepository lekarRepository, IMapper mapper, ISpecijalnostRepository specijalnostRepository,
                            IPregledRepository pregledRepository, IDaLiJeLekarAktivanDtoRequest daLiJeLekarAktivan)
        {
            _lekarRepository = lekarRepository;
            _mapper = mapper;
            _specijalnostRepository = specijalnostRepository;
            _pregledRepository = pregledRepository;
            _daLiJeLekarAktivan = daLiJeLekarAktivan;
        }

        public LekarDtoResponse IzmeniLekara(int id, IzmeniLekaraDtoRequest lekarRequest)
        {
            BrojGodina(lekarRequest.DatumRodjenja);

            var dataLekar = _mapper.Map<Lekar>(lekarRequest);
            var izmenjeniLekar = _lekarRepository.IzmeniLekara(id, dataLekar);
            if (izmenjeniLekar == null)
            {
                return null;
            }
            var lekarResponse = _mapper.Map<LekarDtoResponse>(izmenjeniLekar);
            return lekarResponse;
        }

        public LekarDtoResponse ObrisiLekara(int id)
        {
            var dataLekar = _lekarRepository.IzbrisiLekara(id);
            if (dataLekar == null)
            {
                return null;
            }
            var lekarResponse = _mapper.Map<LekarDtoResponse>(dataLekar);
            return lekarResponse;
        }

        //Kod unosa lekara proveriti da li lekar sa tim jmbg-om postoji u registru suspendovanih lekara
        //registar suspendovanih lekara ce ti biti endpoint na novom API-ju
        public async Task<LekarDtoResponse> UnesiLekara(UnesiLekaraDtoRequest lekarRequest)
        {
            BrojGodina(lekarRequest.DatumRodjenja);

            bool isActive = await _daLiJeLekarAktivan.DaLiJeAktivan(lekarRequest.Jmbg, lekarRequest.IsActive == true);

            if (!isActive)
            {
                return null;
            }


            var dataLekar = _mapper.Map<Lekar>(lekarRequest);
            var specijalnostId = _specijalnostRepository.VratiPoId(lekarRequest.SpecijalnostId); 
            if (specijalnostId == null)                                                            
            {                                                                                 
                return null;
            } 
            var kreiraniLekar = _lekarRepository.UnesiLekara(dataLekar);
            var lekarResponse = _mapper.Map<LekarDtoResponse>(kreiraniLekar);
            return lekarResponse;
        }

        public LekarDtoResponse VratiLekaraPoId(int id)
        {
            var dataLekar = _lekarRepository.VratiLekaraPoId(id);
            var bmLekar = _mapper.Map<LekarDtoResponse>(dataLekar);
            return bmLekar;
        }

        public IEnumerable<LekarPretragaDtoResponse> VratiLekarePoFilteru(LekarPretragaDtoResponse lekarResponse, int strana = 1, int velecinaStrane = 10)
        {


            var lekari = _lekarRepository.VratiSveLekare().OrderBy(x => x.Id).AsQueryable();

            if(!string.IsNullOrWhiteSpace(lekarResponse.Ime))
            { 
                lekari = lekari.Where(x => x.Ime.ToLower().StartsWith(lekarResponse.Ime.ToLower()));
            }
                
            if(lekarResponse.Pol.HasValue)
            {
                lekari = lekari.Where(x => x.Pol == lekarResponse.Pol);
            }

            if (!string.IsNullOrWhiteSpace(lekarResponse.Jmbg))
            {
                lekari = lekari.Where(x => x.Jmbg == lekarResponse.Jmbg);
            }

            if (lekarResponse.IsActive.HasValue)
            {
                lekari = lekari.Where(x => x.IsActive == lekarResponse.IsActive);
            }

            var paginacija = lekari.Skip((strana - 1) * velecinaStrane).Take(velecinaStrane).ToList();

            var lekariResponse = _mapper.Map<IEnumerable<LekarPretragaDtoResponse>>(paginacija);
            return lekariResponse;
        }

        public IEnumerable<PacijentDtoResponse> VratiPacijentePoIdLekara(int id)
        {
            var lekar = _lekarRepository.VratiLekaraPoId(id);
            if (lekar == null)
            {
                return null;
            }
            var pacijenti = lekar.Pacijenti;
            var pacijentiResponse = _mapper.Map<IEnumerable<PacijentDtoResponse>>(pacijenti);
            return pacijentiResponse;
        }
        

        public IEnumerable<LekarDtoResponse> VratiSveLekare()
        {
            var dataLekar = _lekarRepository.VratiSveLekare();
            var bmLekar = _mapper.Map<IEnumerable<LekarDtoResponse>>(dataLekar);
            return bmLekar;
        }

        private void BrojGodina(DateTime datumRodjenja)
        {
            var brojGodine = DateTime.Today.Year - datumRodjenja.Year;
            if (brojGodine >= 70)
            {
                throw new ArgumentOutOfRangeException("Ne mozete uneti ili izmeniti lekara ukoliko ima vise od 70 godina.");
            }

            /*
            var godinaRodjenja = datumRodjenja.Year;
            var danas = DateTime.Now.Year;
            var brojGodina = danas - godinaRodjenja;
            if (brojGodina > 70)
            {
                throw new ArgumentOutOfRangeException("Ne mozete uneti ili izmeniti lekara ukoliko ima vise od 70 godina.");
            }*/
        }
    }
}
