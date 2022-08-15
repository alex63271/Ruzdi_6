using Microsoft.EntityFrameworkCore;
using Ruzdi_DB.Context;
using Ruzdi_DB.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_DB
{
    internal class NotificationRepository : DbRepository<Notification>
    {
        public NotificationRepository(DB_Ruzdi db) : base(db)
        {
        }


        public override IQueryable<Notification> Items => base.Items.Include(Item => Item.PledgeContract);
    }
}
