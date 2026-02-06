using InformacioniSistemZU.BusinessModell.Services;
using InformacioniSistemZU.Dtos.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InformacioniSistemZU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PregledController : ControllerBase
    {
        private readonly IPregledService _pregledService;
        private readonly ILogger<PregledController> _logger;

        public PregledController(IPregledService pregledService, ILogger<PregledController> logger)
        {
            _pregledService = pregledService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult VratiSvePregled()
        { 
            return Ok(_pregledService.VratiSvePreglede());
        }

        [HttpGet("{id:int}")]
        public IActionResult VratiPregledPoId(int id)
        {
            var pregled = _pregledService.VratiPregledPoId(id);
            if (pregled == null)
            {
                return NotFound();
            }
            return Ok(pregled);
        }

        [HttpGet("pospecijalnosti/{specijalnostid:int}")]
        public IActionResult VratiPregledPoSpecijalnostId(int specijalnostid)
        {
            var pregledi = _pregledService.VratiPregledePoSpecijalnostId(specijalnostid);
            if(pregledi == null)
            {
                return NotFound();
            }
            return Ok(pregledi);
        }

        [HttpGet("datum")]
        public IActionResult VratiPregledePoDatumu([FromQuery] DateOnly datumOd, [FromQuery] DateOnly? datumDo)
        {
            var pregledi = _pregledService.VratiPregledePoDatumu(datumOd, datumDo);
            if(pregledi == null)
            {
                return NotFound();
            }
            return Ok(pregledi);
        }

        [HttpPost]
        public IActionResult SacuvajPregled(UnesiPregledDtoRequest unesiPregled)
        {
            var unetiPregled = _pregledService.UnesiPregled(unesiPregled);
            return Ok(unetiPregled);
        }

        [HttpPut("{id:int}")]
        public IActionResult IzmeniPregled (int id, IzmeniPregledDtoRequest izmeniPregled)
        {
            var izmenjeniPregled = _pregledService.IzmeniPregled(id, izmeniPregled);
            if (izmenjeniPregled == null)
            {
                return NotFound();
            }
            return Ok(izmenjeniPregled);
        }


        [HttpDelete("{id:int}")]
        public IActionResult ObrisiPregled(int id)
        {
            var obrisaniPregled = _pregledService.ObrisiPregled(id);
            if (obrisaniPregled == null)
            {
                return NotFound();
            }
            return Ok(obrisaniPregled);
        }
    }
}
