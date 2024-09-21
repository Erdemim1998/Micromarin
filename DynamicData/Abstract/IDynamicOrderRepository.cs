using DynamicData.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Abstract
{
    public interface IDynamicOrderRepository
    {
        IQueryable<Order> Orders { get; }

        Task CreateOrder(Order order);

        Task UpdateOrder(Order order);

        Task DeleteOrder(int id);
    }
}
