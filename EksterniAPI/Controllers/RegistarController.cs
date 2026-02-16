using EksterniAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EksterniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistarController : ControllerBase
    {
        public string Jmbg { get; set; }
        public bool IsActive { get; set; }

        //kontroler sa klasom je ozbiljna greska iz nekoliko razloga
        //1. dodao si klasu koju nigde nisi iskoristio
        //2. stavio si u konstruktor klasu direktno a nisi je registrovao u DI kontainer.
        //Kao da ne razumes kako DI i instanciranje klasa radi (konstruktor), to mi je najveca zamerka
        //ovaj API zbog toga nikad ne bi mogao da izvrsi ni jednu metodu iz kontrolera jer ne zna da kreira kontroler

        //jos se ucis, zaboravljaju se takve stvari, nije to nista strasno ali razmisli/procitaj malo opet kako to funkcionice
        private bool DaLiJeAktivan(string jmbg)
        { 
            if(string.IsNullOrEmpty(jmbg) || jmbg.Length != 13)
            {
                return false;
            }

            if (jmbg.StartsWith("1"))
            {
                return true;
            }
            //ovu logiku sam zakomentarisao ispod jer sam hteo max da uprostim
            //objasnices mi sta si ovde hteo, tj logiku. Verovatno si opet AI koristio :) 

            //int prvi = jmbg[0] - '0';
            //int drugi = jmbg[1] - '0';
            //int predZadnji = jmbg[11] - '0';
            //int zadnji = jmbg[12] - '0';

            //if (predZadnji % 2 != 0 && zadnji % 2 != 0)
            //{
            //    return false;
            //}

            //if (prvi % 2 == 0 && drugi % 2 == 0)
            //{
            //    return true;
            //}
            return false;
        }

        [HttpGet("lekar/{jmbg}")]
        public IActionResult DaLiJeAktivanLekar(string jmbg)
        {
            //takodje mislim da bi bolja ruta bila sa query parametrom (lekar?jmbg=123412), to smo vec pominjali
            //u tom slucaju ne bi vracao NotFound nego uvek OK za response klasom
            bool provera = DaLiJeAktivan(jmbg);
            if(provera == false)
            {
                return NotFound();
            }

            //kada sam ja pominjao da vratis klasu (RegistarResponse recimo) mislio sam ovako nesto

            //Registar registar = new Registar
            //{
            //    Jmbg = jmbg,
            //    IsActive = true
            //};
            //return Ok(registar);

            return Ok(provera);
        }
    }
}
