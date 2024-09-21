using DynamicData.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Abstract
{
    public interface IDynamicProductRepository
    {
        IQueryable<Product> Products { get; }

        Task CreateProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int id);
    }
}
