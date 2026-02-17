namespace InformacioniSistemZU.AppSettingsJson
{
    public class ExternalServiceSettings
    {
        public string BaseUri { get; set; } = string.Empty;
        public int Timeout { get; set; }
        public string AktivanLekarPath { get; set; } = string.Empty;
        public string AuthorizationKey { get; set; } = string.Empty;
    }
}
