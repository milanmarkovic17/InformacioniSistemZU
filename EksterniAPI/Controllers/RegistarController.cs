using EksterniAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EksterniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistarController : ControllerBase
    {
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

            if (jmbg.Substring(0, 2).All(x => x % 2 == 0))
            {
                return true;
            }
            
            return false;
        }

        [HttpGet("lekar")]
        public IActionResult DaLiJeAktivanLekar([FromQuery] string jmbg)
        {
           
            bool provera = DaLiJeAktivan(jmbg);

            RegistarResponse response = new RegistarResponse()
                {
                    Jmbg = jmbg,
                    IsActive = true,
                };

            return Ok(response);
        }
    }
}
