using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyVilla_API.Controllers;
using MyVilla_API.Data;
using MyVilla_API.Models;
using MyVilla_API.Repository.IRepository;
using System.Linq.Expressions;

namespace MyVilla_API.Repository
{
    public class VillaRepository :Repository<Villa>, IVillaRepository
    {
        public ApplicationDbContext DbContext { get; }

        public VillaRepository(ApplicationDbContext dbContext) : base (dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            DbContext.Villas.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
    }
}
