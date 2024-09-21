using DynamicData.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Concrete
{
    public class EfDynamicBrandRepository : IDynamicBrandRepository
    {
        readonly DatabaseContext _context;

        public EfDynamicBrandRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Brand> Brands => _context.Brands;

        public async Task CreateBrand(Brand brand)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Brands.AddAsync(brand);
                    _context.SaveChanges();
                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task UpdateBrand(Brand brand)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Brands.Where(b => b.Id == brand.Id).ExecuteUpdateAsync(set => set.SetProperty(b => b.Name, brand.Name)
                                                                                   .SetProperty(b => b.Ml1Name, brand.Ml1Name)
                                                                                   .SetProperty(b => b.Ml2Name, brand.Ml2Name));

                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task DeleteBrand(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Brands.Where(b => b.Id == id).ExecuteDeleteAsync();
                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }
    }
}
