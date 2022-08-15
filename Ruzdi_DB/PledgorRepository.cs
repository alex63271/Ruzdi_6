using Microsoft.EntityFrameworkCore;
using Ruzdi_DB.Context;
using Ruzdi_DB.Entityes;

namespace Ruzdi_DB
{
    internal class PledgorRepository : DbRepository<Pledgor>
    {
        public PledgorRepository(DB_Ruzdi db) : base(db)
        {
        }

        public override IQueryable<Pledgor> Items => base.Items.Include(Item => Item.Id);
    }
}
