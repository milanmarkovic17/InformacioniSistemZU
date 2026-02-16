namespace InformacioniSistemZU.BusinessModell.Services
{
    public interface IProveraAktivnostiLekaraService
    {
        Task<bool> ProveraAktivnosti(string jmbg);
    }
}
