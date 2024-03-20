using System.ComponentModel.DataAnnotations;

namespace MyVilla_API.Models.DTO
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
