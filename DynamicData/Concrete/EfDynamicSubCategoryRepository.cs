using DynamicData.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Concrete
{
    public class EfDynamicSubCategoryRepository : IDynamicSubCategoryRepository
    {
        readonly DatabaseContext _context;

        public EfDynamicSubCategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<SubCategory> SubCategories => _context.SubCategories;

        public async Task CreateSubCategory(SubCategory subCategory)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SubCategories.AddAsync(subCategory);
                    _context.SaveChanges();
                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task UpdateSubCategory(SubCategory subCategory)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SubCategories.Where(sc => sc.Id == subCategory.Id).ExecuteUpdateAsync(set => set.SetProperty(sc => sc.Name, subCategory.Name)
                                                                                                  .SetProperty(sc => sc.Ml1Name, subCategory.Ml1Name)
                                                                                                  .SetProperty(sc => sc.Ml2Name, subCategory.Ml2Name)
                                                                                                  .SetProperty(sc => sc.CategoryId, subCategory.CategoryId));

                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task DeleteSubCategory(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SubCategories.Where(sc => sc.Id == id).ExecuteDeleteAsync();
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
