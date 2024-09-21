using DynamicData.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DynamicData.Concrete
{
    public class EfDynamicProductRepository : IDynamicProductRepository
    {
        readonly DatabaseContext _context;

        public EfDynamicProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public async Task CreateProduct(Product product)
        {
            using(var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Products.AddAsync(product);
                    _context.SaveChanges();
                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task UpdateProduct(Product product)
        {
            using(var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Products.Where(p => p.Id == product.Id).ExecuteUpdateAsync(set => set.SetProperty(p => p.Name, product.Name)
                                                                                       .SetProperty(p => p.Ml1Name, product.Ml1Name)
                                                                                       .SetProperty(p => p.Ml2Name, product.Ml2Name)
                                                                                       .SetProperty(p => p.Price, product.Price)
                                                                                       .SetProperty(p => p.Description, product.Description)
                                                                                       .SetProperty(p => p.SubCategoryId, product.SubCategoryId)
                                                                                       .SetProperty(p => p.BrandId, product.BrandId));

                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Products.Where(p => p.Id == id).ExecuteDeleteAsync();
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
