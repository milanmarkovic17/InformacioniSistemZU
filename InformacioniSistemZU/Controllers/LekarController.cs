using AutoMapper;
using InformacioniSistemZU.BusinessModell.RepositoriesBM;
using InformacioniSistemZU.BusinessModell.Services;
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
        private readonly IProveraAktivnostiLekaraService _proveraAktivnostiLekara;

        public LekarController(ILekarService lekarService, ILogger<LekarController> logger, IProveraAktivnostiLekaraService proveraAktivnostiLekara)
        {
            _lekarservice = lekarService;
            _logger = logger;
            _proveraAktivnostiLekara = proveraAktivnostiLekara;
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
        public async Task<IActionResult> SacuvajLekara(UnesiLekaraDtoRequest unesiLekara)
        {
            //metoda ProveraAktivnosti je async a pozivao si je bez async await. To nikako nije moglo da radi.
            //ispravio sam i potpis metode
            var proveraLekara = await _proveraAktivnostiLekara.ProveraAktivnosti(unesiLekara.Jmbg);

            //cela ova linija iznad, bi trebalo da bude u okviru _lekarservice.UnesiLekara, ne na nivou kontrolera
            //takodje sa proveraLekara nista ne radis, mozda nisi implementirao dalje jer nisi uspeo da izvrsis kod

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
