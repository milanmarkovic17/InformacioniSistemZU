using InformacioniSistemZU.BusinessModell.Services;
using InformacioniSistemZU.CustomActionFilters;
using InformacioniSistemZU.Dtos.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InformacioniSistemZU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacijentController : ControllerBase
    {
        private readonly IPacijentService _pacijentService;

        public PacijentController(IPacijentService pacijentService)
        {
            _pacijentService = pacijentService;
        }

        [HttpGet]
        public IActionResult VratiSvePacijente() 
        {
            var sviPacijenti = _pacijentService.VratiSvePacijente();
            return Ok(sviPacijenti);
        }

        [HttpGet("{pacijentid:int}")]
        public IActionResult VratiPacijentaPoId(int pacijentid)
        {
            var pacijent = _pacijentService.VratiPacijentaPoId(pacijentid);
            if (pacijentid == null)
            {
                return NotFound();
            }
            return Ok(pacijent);
        }


        [HttpPost]
        [ValidateModel]
        public IActionResult SacuvajPacijenta(UnesiPacijentaDtoRequest unesiPacijenta)
        {
            var unetiPacijent = _pacijentService.UnesiPacijenta(unesiPacijenta);
            return Ok(unetiPacijent);
        }

        [HttpPut("{id:int}")]
        [ValidateModel]
        public IActionResult IzmeniPacijenta(int id, IzmeniPacijentaDtoRequest izmeniPacijenta)
        {
            var izmenjeniPacijent = _pacijentService.IzmeniPacijenta(id, izmeniPacijenta);
            if (izmenjeniPacijent == null)
            {
                return NotFound();
            }
            return Ok(izmenjeniPacijent);
        }

        [HttpDelete("{id:int}")]
        public IActionResult ObrisiPacijenta(int id)
        {
            var obrisaniPacijent = _pacijentService.ObrisiPacijenta(id);
            if (obrisaniPacijent ==null)
            {
                return NotFound();
            }
            return Ok(obrisaniPacijent);
        }
    }
}
