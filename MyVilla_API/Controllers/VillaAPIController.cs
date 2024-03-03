using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVilla_API.Data;
using MyVilla_API.Models;
using MyVilla_API.Models.DTO;

namespace MyVilla_API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public ILogger<VillaAPIController> _logger { get; }
        public ApplicationDbContext _dbContext { get; }
        public IMapper _mapper { get; }

        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {   
            _logger.LogInformation("Getting all villas");
            IEnumerable<Villa> villaList = await _dbContext.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
        }
            
        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]

        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0 )
            {
                _logger.LogInformation("Get villa error with id" + id);
                return BadRequest();
            }
            var villa =await _dbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            if (await _dbContext.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Villa already exists!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            //if (villaDTO.Id>0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            //Villa model = new()
            //{
            //    Amenity = createDTO.Amenity,
            //    Details = createDTO.Details,
            //    ImageUrl = createDTO.ImageUrl,
            //    Name = createDTO.Name,
            //    Occupancy = createDTO.Occupancy,
            //    Rate = createDTO.Rate,
            //    Sqft = createDTO.Sqft
            //};

            Villa model = _mapper.Map<Villa>(createDTO);

            await _dbContext.Villas.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            //return Ok(villaDTO);


            // To provide the URL of the newly created resource upon its creation.
            // This enables users to directly access the location where the resource has been generated.
            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>  UpdateVilla(int id, [FromBody]VillaUpdateDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest(updateDTO);
            }

            if (await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id) == null)
            {
                return NotFound();
            }
            //villa.Name = villaDTO.Name;
            //villa.Occupancy = villaDTO.Occupancy;
            //villa.Sqft = villaDTO.Sqft;


            //Villa model = new()
            //{
            //    Amenity = updateDTO.Amenity,
            //    Details = updateDTO.Details,
            //    Id = updateDTO.Id,
            //    ImageUrl = updateDTO.ImageUrl,
            //    Name = updateDTO.Name,
            //    Occupancy = updateDTO.Occupancy,
            //    Rate = updateDTO.Rate,
            //    Sqft = updateDTO.Sqft
            //};

            Villa model = _mapper.Map<Villa>(updateDTO);

            _dbContext.Villas.Update(model);
            await _dbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            //VillaUpdateDTO villaDTO = new()
            //{
            //    Amenity = villa?.Amenity,
            //    Details = villa?.Details,
            //    Id = villa.Id,
            //    ImageUrl = villa.ImageUrl,
            //    Name = villa.Name,
            //    Occupancy = villa.Occupancy,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft
            //};

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

            patchDTO.ApplyTo(villaDTO, ModelState);


            //Villa model = new Villa()
            //{
            //    Amenity = villaDTO.Amenity,
            //    Details = villaDTO.Details,
            //    Id = villaDTO.Id,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Name = villaDTO.Name,
            //    Occupancy = villaDTO.Occupancy,
            //    Rate = villaDTO.Rate,
            //    Sqft = villaDTO.Sqft
            //};

            Villa model = _mapper.Map<Villa>(villaDTO);

            _dbContext.Villas.Update(model);
            await _dbContext.SaveChangesAsync();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _dbContext.Villas.Remove(villa);
            await _dbContext.SaveChangesAsync();

            return NoContent();

        }
    }
}
