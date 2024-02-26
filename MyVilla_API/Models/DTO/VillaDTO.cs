using System.ComponentModel.DataAnnotations;

namespace MyVilla_API.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
    }
}
