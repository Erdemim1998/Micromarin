using DynamicData.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Concrete
{
    public class EfDynamicCategoryRepository : IDynamicCategoryRepository
    {
        readonly DatabaseContext _context;

        public EfDynamicCategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories;

        public async Task CreateCategory(Category category)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Categories.AddAsync(category);
                    _context.SaveChanges();
                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task UpdateCategory(Category category)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Categories.Where(c => c.Id == category.Id).ExecuteUpdateAsync(set => set.SetProperty(c => c.Name, category.Name)
                                                                                                .SetProperty(c => c.Ml1Name, category.Ml1Name)
                                                                                                .SetProperty(c => c.Ml2Name, category.Ml2Name));

                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task DeleteCategory(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Categories.Where(c => c.Id == id).ExecuteDeleteAsync();
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
