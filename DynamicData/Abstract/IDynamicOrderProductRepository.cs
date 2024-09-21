using DynamicData.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Abstract
{
    public interface IDynamicOrderProductRepository
    {
        IQueryable<OrderProduct> OrderProducts { get; }

        Task CreateOrderProduct(OrderProduct product);

        Task UpdateOrderProduct(OrderProduct product);

        Task DeleteOrderProduct(int id);
    }
}
