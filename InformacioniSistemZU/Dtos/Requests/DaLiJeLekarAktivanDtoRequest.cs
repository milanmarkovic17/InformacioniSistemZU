namespace InformacioniSistemZU.Dtos.Requests
{
    public class DaLiJeLekarAktivanDtoRequest : IDaLiJeLekarAktivanDtoRequest
    {
        private readonly HttpClient _httpClient;

        public DaLiJeLekarAktivanDtoRequest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<bool> DaLiJeAktivan(string jmbg, bool isActive)
        {
            var response = await _httpClient.GetAsync($"/api/Registar/lekar?jmbg={jmbg}&isActive={isActive}");
            if (response.IsSuccessStatusCode)
            { 
                return true;
            }
            return false;
        }

        
    }
}
