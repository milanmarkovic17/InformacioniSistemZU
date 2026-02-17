
using EksterniAPI.Models;

namespace InformacioniSistemZU.BusinessModell.Services
{
    public class ProveraAktivnostiLekaraService : IProveraAktivnostiLekaraService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProveraAktivnostiLekaraService> _logger;

        public ProveraAktivnostiLekaraService(HttpClient httpClient, ILogger<ProveraAktivnostiLekaraService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<bool> ProveraAktivnosti(string jmbg)
        {
            try
            {
                //ruta je bila pogresna, falio je api/ na pocetku. To je ona definisana na ekternomAPI-ju
                var response = await _httpClient.GetAsync($"api/Registar/lekar?jmbg={jmbg}");
                if (response.IsSuccessStatusCode)
                {
                    var rezultat = await response.Content.ReadFromJsonAsync<RegistarResponse>();
                    if(rezultat != null && rezultat.IsActive)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greska prilikom pristupanja eksternom Api-ju");
                return false;
            }
        }
    }
}
