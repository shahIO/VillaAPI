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
        public ApplicationDbContext DbContext { get; }

        public VillaNumberRepository(ApplicationDbContext dbContext) : base (dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            DbContext.VillaNumbers.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
    }
}
