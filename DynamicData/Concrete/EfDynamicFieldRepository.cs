using DynamicData.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Concrete
{
    public class EfDynamicFieldRepository : IDynamicFieldRepository
    {
        private readonly DatabaseContext _context;

        public EfDynamicFieldRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Field> Fields => _context.Fields;

        public async Task CreateField(Field field)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Fields.AddAsync(field);
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
