using InformacioniSistemZU.Models;
using static InformacioniSistemZU.Enums.Enums;

namespace InformacioniSistemZU.Dtos.Responses
{
    public class LekarDtoResponse
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public Pol Pol { get; set; }
        public string Opis { get; set; }
        public bool IsActive { get; set; }
        public int SpecijalnostId { get; set; }  
    }
}
