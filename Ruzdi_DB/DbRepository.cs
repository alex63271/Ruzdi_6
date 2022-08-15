using Microsoft.EntityFrameworkCore;
using Ruzdi_DB.Context;
using Ruzdi_Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_DB
{
    internal class DbRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public DbRepository(DB_Ruzdi db)
        {
            this.db = db;
            set = db.Set<T>();
        }

        private DB_Ruzdi db;
        private DbSet<T> set;
        public bool AutoSaveChanges { get; set; } = true;
        

        public virtual IQueryable<T> Items => set;

        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)    //на случай добавления большого массива данных - метод SaveChanges() вызвать один раз
            {
                db.SaveChanges();
            }
            return item;
        }

        public T Get(string id) => Items.SingleOrDefault(item => item.Id == id);

        public ObservableCollection<T> GetAll() => new ObservableCollection<T>(Items);

        public void Remove(string id)
        {
            db.Remove(new T { Id = id });

            if (AutoSaveChanges)    //на случай добавления большого массива данных - метод SaveChanges() вызвать один раз
            {
                db.SaveChanges();
            }
        }

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            
            db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)    //на случай добавления большого массива данных - метод SaveChanges() вызвать один раз
            {
                db.SaveChanges();
            }
        }                     
    }
}
