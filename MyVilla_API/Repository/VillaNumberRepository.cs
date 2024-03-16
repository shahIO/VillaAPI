using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyVilla_API.Controllers;
using MyVilla_API.Data;
using MyVilla_API.Models;
using MyVilla_API.Repository.IRepository;
using System.Linq.Expressions;

namespace MyVilla_API.Repository
{
    public class VillaNumberRepository :Repository<VillaNumber>, IVillaNumberRepository
    {
        public ApplicationDbContext _dbContext { get; }

        public VillaNumberRepository(ApplicationDbContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _dbContext.VillaNumbers.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
