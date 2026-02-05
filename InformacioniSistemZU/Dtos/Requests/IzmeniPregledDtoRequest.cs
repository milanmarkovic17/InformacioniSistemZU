using System.ComponentModel.DataAnnotations;

namespace InformacioniSistemZU.Dtos.Requests
{
    public class IzmeniPregledDtoRequest
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required]
        public int DijagnozaId { get; set; }
        [Required]
        public int? LekarId { get; set; }
        [Required]
        public int? PacijentId { get; set; }
    }
}
