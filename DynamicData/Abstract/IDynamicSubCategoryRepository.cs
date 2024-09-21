using DynamicData.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Abstract
{
    public interface IDynamicSubCategoryRepository
    {
        IQueryable<SubCategory> SubCategories { get; }

        Task CreateSubCategory(SubCategory subCategory);

        Task UpdateSubCategory(SubCategory subCategory);

        Task DeleteSubCategory(int id);
    }
}
