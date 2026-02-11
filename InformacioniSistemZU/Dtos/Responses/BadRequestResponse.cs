namespace InformacioniSistemZU.Dtos.Responses
{
    public class BadRequestResponse
    {
        public BadRequestResponse(List<string> errors)
        {
            Errors = errors;
        }

        List<string> Errors { get; set; } = new List<string>();
    }
}
