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
using static InformacioniSistemZU.Enums.Enums;

namespace InformacioniSistemZU.BusinessModell.RepositoriesBM
{
    public class LekarService : ILekarService
    {
        private readonly ILekarRepository _lekarRepository;
        private readonly IMapper _mapper;
        private readonly ISpecijalnostRepository _specijalnostRepository;
        private readonly IPregledRepository _pregledRepository;

        public LekarService(ILekarRepository lekarRepository, IMapper mapper, ISpecijalnostRepository specijalnostRepository,
                            IPregledRepository pregledRepository)
        {
            _lekarRepository = lekarRepository;
            _mapper = mapper;
            _specijalnostRepository = specijalnostRepository;
            _pregledRepository = pregledRepository;
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

        public LekarDtoResponse UnesiLekara(UnesiLekaraDtoRequest lekarRequest)
        {
            BrojGodina(lekarRequest.DatumRodjenja);

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

        public IEnumerable<LekarDtoResponse> VratiLekarePoImenuIPolu(string ime, Pol pol)
        {
            var lekari = _lekarRepository.VratiSveLekare().Where(x => x.Ime.ToLower().StartsWith(ime.ToLower()) && x.Pol == pol);
                                                           
                                                           
            if (lekari == null)
            {
                return null;
            }

            var lekariResponse = _mapper.Map<IEnumerable<LekarDtoResponse>>(lekari);
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
            var godinaRodjenja = datumRodjenja.Year;
            var danas = DateTime.Now.Year;
            var brojGodina = danas - godinaRodjenja;
            if (brojGodina > 70)
            {
                throw new ArgumentOutOfRangeException("Ne mozete uneti ili izmeniti lekara ukoliko ima vise od 70 godina.");
            }
        }
    }
}
