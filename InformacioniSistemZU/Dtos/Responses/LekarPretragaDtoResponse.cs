using InformacioniSistemZU.Models;
using static InformacioniSistemZU.Enums.Enums;

namespace InformacioniSistemZU.Dtos.Responses
{
    public class LekarPretragaDtoResponse
    {
       
        public string? Ime { get; set; }
        public string? Jmbg { get; set; }
        public Pol? Pol { get; set; }
        public bool? IsActive { get; set; }
        
    }
}
