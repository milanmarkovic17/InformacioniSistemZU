using System.ComponentModel.DataAnnotations;
using static InformacioniSistemZU.Enums.Enums;

namespace InformacioniSistemZU.Dtos.Requests
{
    public class IzmeniPacijentaDtoRequest
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Ime moze sadrzati najvise 10 karaktera")]
        public string Ime { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Prezime moze sadrzati najvise 15 karaktera")]
        public string Prezime { get; set; }
        [Required]
        [Length(13, 13, ErrorMessage = "Maticni broj mora imati tacno 13 karaktera")]
        public string Jmbg { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }
        [Required]
        public Pol Pol { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatumKreiranja { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int LekarId { get; set; }
    }
}
