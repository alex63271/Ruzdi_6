using Microsoft.EntityFrameworkCore;
using Ruzdi_DB.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace Ruzdi_DB.Context
{
    public class DB_Ruzdi : DbContext
    {
        public DB_Ruzdi(DbContextOptions<DB_Ruzdi> options) :base(options)
        {
            
        }

        public DB_Ruzdi()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            optionsBuilder.UseMySql("server=localhost;user id=enot;database=Ruzdi_DB;Password=gdh1geit;port=3306", ServerVersion.AutoDetect("server=localhost;user id=enot;database=Ruzdi_DB;Password=gdh1geit;port=3306"));
        }


        // public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Notification> Notifications { get; set; }    
        //public DbSet<Pledgor> Pledgors { get; set; }
        public DbSet<Regions> Regions { get; set; }
        //public DbSet<RegistrationCertificate> RegistrationCertificates { get; set; }


    }
    
}
