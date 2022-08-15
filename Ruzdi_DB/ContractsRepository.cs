using Microsoft.EntityFrameworkCore;
using Ruzdi_DB.Context;
using Ruzdi_DB.Entityes;

namespace Ruzdi_DB
{
    internal class ContractsRepository : DbRepository<Contracts>
    {
        public ContractsRepository(DB_Ruzdi db) : base(db)
        {
        }


        public override IQueryable<Contracts> Items => base.Items.Include(Item=> Item.Number);
    }
}
