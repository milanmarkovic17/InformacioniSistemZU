
namespace InformacioniSistemZU.BusinessModell.Services
{
    public class ProveraAktivnostiLekaraService : IProveraAktivnostiLekaraService
    {
        private readonly HttpClient _httpClient;

        public ProveraAktivnostiLekaraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> ProveraAktivnosti(string jmbg)
        {
            var response = await _httpClient.GetAsync($"Registar/lekar/{jmbg}");
            if (response.IsSuccessStatusCode)
            {
                //var sadrzaj = await response.Content.ReadAsStringAsync();
                //return bool.TryParse(sadrzaj, out bool rezultat) && rezultat;

                return true;
            }
            return false;
        }
    }
}
