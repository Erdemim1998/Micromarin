using DynamicData.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Abstract
{
    public interface IDynamicFieldRepository
    {
        IQueryable<Field> Fields { get; }

        Task CreateField(Field field);
    }
}
