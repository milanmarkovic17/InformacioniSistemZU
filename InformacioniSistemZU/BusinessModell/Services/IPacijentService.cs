using InformacioniSistemZU.Dtos.Requests;
using InformacioniSistemZU.Dtos.Responses;
using InformacioniSistemZU.ResultPatern;

namespace InformacioniSistemZU.BusinessModell.Services
{
    public interface IPacijentService
    {
        IEnumerable<PacijentDtoResponse> VratiSvePacijente();
        PacijentDtoResponse VratiPacijentaPoId(int id);
        Result<PacijentDtoResponse> UnesiPacijenta(UnesiPacijentaDtoRequest pacijentRequest);
        Result<PacijentDtoResponse> IzmeniPacijenta (int id, IzmeniPacijentaDtoRequest pacijentRequest);
        PacijentDtoResponse ObrisiPacijenta(int id);
    }
}
