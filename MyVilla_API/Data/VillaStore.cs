using MyVilla_API.Models.DTO;

namespace MyVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO { Id = 1, Name = "Hill View", Occupancy = 8, Sqft = 220 },
                new VillaDTO { Id = 2, Name = "Sea View", Occupancy = 3, Sqft = 120 }
            };
    }
}
