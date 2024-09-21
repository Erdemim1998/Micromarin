using DynamicData.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Concrete
{
    public class EfDynamicOrderRepository : IDynamicOrderRepository
    {
        readonly DatabaseContext _context;

        public EfDynamicOrderRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders;

        public async Task CreateOrder(Order order)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Orders.AddAsync(order);
                    _context.SaveChanges();
                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task UpdateOrder(Order order)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Orders.Where(o => o.Id == order.Id).ExecuteUpdateAsync(set => set.SetProperty(o => o.OrderDate, order.OrderDate)
                                                                                   .SetProperty(o => o.PhoneNumber, order.PhoneNumber)
                                                                                   .SetProperty(o => o.Email, order.Email)
                                                                                   .SetProperty(o => o.Address, order.Address)
                                                                                   .SetProperty(o => o.UserName, order.UserName)
                                                                                   .SetProperty(o => o.CartNumber, order.CartNumber)
                                                                                   .SetProperty(o => o.ExpirationMonth, order.ExpirationMonth)
                                                                                   .SetProperty(o => o.ExpirationYear, order.ExpirationYear)
                                                                                   .SetProperty(o => o.Cvc, order.Cvc));

                    await _context.Database.CommitTransactionAsync();
                }

                catch
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        public async Task DeleteOrder(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Orders.Where(p => p.Id == id).ExecuteDeleteAsync();
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
