using EksterniAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EksterniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistarController : ControllerBase
    {
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
                    IsActive = provera      
                };         
            
            return Ok(response);
        }
    }
}
