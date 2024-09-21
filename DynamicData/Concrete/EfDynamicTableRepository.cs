using DynamicData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Concrete
{
    public class EfDynamicTableRepository : IDynamicTableRepository
    {
        private readonly DatabaseContext _context;

        public EfDynamicTableRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Table> Tables => _context.Tables;

        public async Task CreateTable(Table table)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Tables.AddAsync(table);
                    _context.SaveChanges();
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
