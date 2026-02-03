using InformacioniSistemZU.Dtos.Requests;
using InformacioniSistemZU.Dtos.Responses;

namespace InformacioniSistemZU.BusinessModell.Services
{
    public interface IPregledService
    {
        IEnumerable<PregledDtoResponse> VratiSvePreglede();
        PregledDtoResponse VratiPregledPoId(int id);
        PregledDtoResponse UnesiPregled(UnesiPregledDtoRequest pregledRequest);
        PregledDtoResponse IzmeniPregled(int id, IzmeniPregledDtoRequest pregledRequest);
        PregledDtoResponse ObrisiPregled(int id);
        IEnumerable<PregledDtoResponse> VratiPregledePoSpecijalnostId(int specijanlnostid);
        IEnumerable<PregledDtoResponse> VratiPregledePoDatumu(DateOnly datumOd, DateOnly? datumDo);
    }
}
