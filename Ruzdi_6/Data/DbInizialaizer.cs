using Microsoft.EntityFrameworkCore;
using Ruzdi_DB.Context;
using Ruzdi_DB.Entityes;
using System.Windows;

namespace Ruzdi_6.Data
{
    internal class DbInizialaizer
    {
        private DB_Ruzdi _db;

        public DbInizialaizer(DB_Ruzdi db)
        {
            _db = db;
        }

        public async Task InizializeAsync()
        {
            // _db.Database.EnsureDeletedAsync().Wait();
            //bool res =_db.Database.EnsureCreated();            
            await _db.Database.MigrateAsync();
           
            
            Task<int> CountRegions = _db.Regions.CountAsync(); //запуск подсчета кол-ва регионов

            if (await CountRegions < 4)
            {
                await _db.Database.EnsureDeletedAsync();
               
                await _db.Database.MigrateAsync();
   
                Regions[] regions =
                {
                    new Regions { Id = "1", CodeRegion = "77", Region = "Москва" },
                    new Regions { Id = "2", CodeRegion = "50", Region = "Московская область" },
                    new Regions { Id = "3", CodeRegion = "78", Region = "Санкт-Петербург" },
                    new Regions { Id = "4", CodeRegion = "29", Region = "Архангельская область" }
                };
                _db.AddRange(regions);
                await Task.Run(()=> _db.Regions.AddRange(regions)); //_db.Regions.AddRangeAsync(regions);
                await Task.Run(() => _db.SaveChanges()); //_db.SaveChangesAsync();
            }    
            
            
        }
    }
}
