using System.ComponentModel.DataAnnotations;
using static InformacioniSistemZU.Enums.Enums;

namespace InformacioniSistemZU.Dtos.Requests
{
    public class UnesiLekaraDtoRequest
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Ime moze sadrzati najvise 10 karaktera")]
        public string Ime { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Prezime moze sadrzati najvise 15 karaktera")]
        public string Prezime { get; set; }
        [Required]
        [MinLength(13, ErrorMessage = "Maticni broj mora imati minimalno 13 karaktera")]
        [MaxLength(13, ErrorMessage = "Maticni broj mora imati maksimalno 13 karaktera")]
        public string Jmbg { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }
        [Required]
        [Range(1,2)]
        public Pol Pol { get; set; }
        public string Opis { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        [Range(1,3)]
        public int SpecijalnostId { get; set; }
    }
}
