using DynamicData.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Abstract
{
    public interface IDynamicBrandRepository
    {
        IQueryable<Brand> Brands { get; }

        Task CreateBrand(Brand brand);

        Task UpdateBrand(Brand brand);

        Task DeleteBrand(int id);
    }
}
