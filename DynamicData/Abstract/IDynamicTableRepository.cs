using DynamicData.Concrete;

namespace DynamicData.Abstract
{
    public interface IDynamicTableRepository
    {
        IQueryable<Table> Tables { get; }

        Task CreateTable(Table table);
    }
}
