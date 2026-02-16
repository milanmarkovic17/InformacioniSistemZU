
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
            //ruta je bila pogresna, falio je api/ na pocetku. To je ona definisana na ekternomAPI-ju
            var response = await _httpClient.GetAsync($"api/Registar/lekar/{jmbg}");
            if (response.IsSuccessStatusCode)
            {
                var sadrzaj = await response.Content.ReadAsStringAsync();
                return bool.TryParse(sadrzaj, out bool rezultat) && rezultat; //ovo si verovatno uzeo kod sa chatGPT-a, prekonfuzan kod. Napisi drugacije, SAM :)
                //na kraju malo bolje da ishendlujes celu metodu
            }
            return false;
        }
    }
}
