using InformacioniSistemZU.BusinessModell.RepositoriesBM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InformacioniSistemZU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistarController : ControllerBase
    {
        private readonly ILekarService _lekarService;

        public RegistarController(ILekarService lekarService)
        {
            _lekarService = lekarService;
        }

        [HttpGet("lekar")]
        public IActionResult GetAll([FromQuery] string jmbg, [FromQuery] bool isActive)
        {
            var sviLekari = _lekarService.VratiSveLekare().Where(l => l.Jmbg == jmbg && l.IsActive == isActive).ToList();
                                                          
            if(!sviLekari.Any())
            {
                return NotFound();
            }
            return Ok(sviLekari);
        }
    }
}
