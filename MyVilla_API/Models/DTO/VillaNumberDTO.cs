using System.ComponentModel.DataAnnotations;

namespace MyVilla_API.Models.DTO
{
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }

        public required string SpecialDetails { get; set; }
    }
}
