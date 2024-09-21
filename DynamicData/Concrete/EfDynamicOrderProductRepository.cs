using DynamicData.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DynamicData.Concrete
{
    public class EfDynamicOrderProductRepository : IDynamicOrderProductRepository
    {
        readonly DatabaseContext _context;

        public EfDynamicOrderProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<OrderProduct> OrderProducts => _context.OrderProducts;

        public async Task CreateOrderProduct(OrderProduct product)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.OrderProducts.AddAsync(product);
                    _context.SaveChanges();
                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task UpdateOrderProduct(OrderProduct product)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.OrderProducts.Where(o => o.Id == product.Id).ExecuteUpdateAsync(set => set.SetProperty(o => o.ProductId, product.ProductId)
                                                                                                          .SetProperty(o => o.OrderId, product.OrderId));

                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task DeleteOrderProduct(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.OrderProducts.Where(p => p.Id == id).ExecuteDeleteAsync();
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
