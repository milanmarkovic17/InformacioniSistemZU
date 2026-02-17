
using EksterniAPI.Models;
using InformacioniSistemZU.Dtos.Responses;

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
                var response = await _httpClient.GetAsync($"api/Registar/lekar?jmbg={jmbg}");
                if (response.IsSuccessStatusCode)
                {
                    var rezultat = await response.Content.ReadFromJsonAsync<RegistarDtoResponse>();
                    if(rezultat != null && rezultat.IsActive == true)
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
