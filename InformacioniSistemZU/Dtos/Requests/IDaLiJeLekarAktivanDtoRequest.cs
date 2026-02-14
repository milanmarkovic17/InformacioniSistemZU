namespace InformacioniSistemZU.Dtos.Requests
{
    public interface IDaLiJeLekarAktivanDtoRequest
    {
        Task<bool> DaLiJeAktivan(string jmbg, bool isActive);
    }
}
