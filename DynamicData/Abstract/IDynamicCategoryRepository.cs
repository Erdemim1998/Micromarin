using DynamicData.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Abstract
{
    public interface IDynamicCategoryRepository
    {
        IQueryable<Category> Categories { get; }

        Task CreateCategory(Category category);

        Task UpdateCategory(Category category);

        Task DeleteCategory(int id);
    }
}
