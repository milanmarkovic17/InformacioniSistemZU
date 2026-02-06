using AutoMapper;
using InformacioniSistemZU.BusinessModell.RepositoriesBM;
using InformacioniSistemZU.CustomActionFilters;
using InformacioniSistemZU.Dtos.Requests;
using InformacioniSistemZU.Dtos.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static InformacioniSistemZU.Enums.Enums;

namespace InformacioniSistemZU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LekarController : ControllerBase
    {
        private readonly ILekarService _lekarservice;
        private readonly ILogger<LekarController> _logger;

        public LekarController(ILekarService lekarService, ILogger<LekarController> logger)
        {
            _lekarservice = lekarService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult VratiSveLekare()
        {
            return Ok(_lekarservice.VratiSveLekare());
        }

        [HttpGet("{id:int}")]
        public IActionResult VratiLekaraPoId(int id)
        {
            var lekarExist = _lekarservice.VratiLekaraPoId(id);
            if (lekarExist == null)
            {
                return NotFound();
            }
            return Ok(lekarExist);
        }

       
        [HttpGet("{lekarid:int}/pacijenti")]
        public IActionResult VratiPacijentePoIdLekara(int lekarid)
        {
            var pacijenti = _lekarservice.VratiPacijentePoIdLekara(lekarid);
            return Ok(pacijenti);
        }

        //ja bih i bez pretrage u ruti, ali bitno je ovo FromQuery
        [HttpGet("pretraga")]
        public IActionResult VratiLekarePoImenu([FromQuery] LekarPretragaDtoResponse lekarResponse, [FromQuery] int strana = 1, [FromQuery] int velicinaStrane = 10)
        {
            var lekari = _lekarservice.VratiLekarePoFilteru(lekarResponse, strana, velicinaStrane);
                                                                
            if (lekari == null)
            {
                return NotFound();
            }
            return Ok(lekari);
        }


        [HttpPost]
        [ValidateModel]
        public IActionResult SacuvajLekara(UnesiLekaraDtoRequest unesiLekara)
        {
            var unetiLekar = _lekarservice.UnesiLekara(unesiLekara);
           
            return Ok(unetiLekar);
        }

        [HttpPut("{id:int}")]
        [ValidateModel]
        public IActionResult IzmeniLekara(int id, IzmeniLekaraDtoRequest izmeniLekara)
        {
            var izmenjeniLekar = _lekarservice.IzmeniLekara(id, izmeniLekara);
            if (izmenjeniLekar == null)
            {
                return NotFound();
            }
            return Ok(izmenjeniLekar);
        }

        [HttpDelete("{id:int}")]
        public IActionResult ObrisiLekara(int id)
        {
            var obrisaniLekar = _lekarservice.ObrisiLekara(id);
            if (obrisaniLekar == null)
            {
                return NotFound();
            }
            return Ok(obrisaniLekar);
        }
    }
}
