using InformacioniSistemZU.Dtos.Requests;
using InformacioniSistemZU.Dtos.Responses;
using InformacioniSistemZU.Models;
using static InformacioniSistemZU.Enums.Enums;

namespace InformacioniSistemZU.BusinessModell.RepositoriesBM
{
    public interface ILekarService
    {
        IEnumerable<LekarDtoResponse> VratiSveLekare();
        LekarDtoResponse VratiLekaraPoId(int id);
        Task<LekarDtoResponse> UnesiLekara(UnesiLekaraDtoRequest lekarRequest);
        LekarDtoResponse IzmeniLekara(int id, IzmeniLekaraDtoRequest lekarRequest);
        LekarDtoResponse ObrisiLekara(int id);
        IEnumerable<PacijentDtoResponse> VratiPacijentePoIdLekara(int id);
        IEnumerable<LekarPretragaDtoResponse> VratiLekarePoFilteru(LekarPretragaDtoResponse lekarResponse, int strana = 1, int velicinaStrane = 10);
    }
}
