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
        public ApplicationDbContext _dbContext { get; }

        public VillaRepository(ApplicationDbContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _dbContext.Villas.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
