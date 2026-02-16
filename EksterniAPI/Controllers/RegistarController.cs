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
        public RegistarController(Registar registar) 
        {
            Jmbg = registar.Jmbg;
            IsActive = registar.IsActive;
        }

        private bool DaLiJeAktivan(string jmbg)
        { 
            if(string.IsNullOrEmpty(jmbg) || jmbg.Length != 13)
            {
                return false;
            }

            int prvi = jmbg[0] - '0';
            int drugi = jmbg[1] - '0';
            int predZadnji = jmbg[11] - '0';
            int zadnji = jmbg[12] - '0';

            if (predZadnji % 2 != 0 && zadnji % 2 != 0)
            {
                return false;
            }

            if (prvi % 2 == 0 && drugi % 2 == 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet("lekar/{jmbg}")]
        public IActionResult DaLiJeAktivanLekar(string jmbg)
        {
            bool provera = DaLiJeAktivan(jmbg);
            if(provera == false)
            {
                return NotFound();
            }
            return Ok(provera);
        }
    }
}
