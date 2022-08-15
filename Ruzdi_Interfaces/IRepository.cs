using System.Collections.ObjectModel;

namespace Ruzdi_Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Items { get; }

        T Get(string id);
        ObservableCollection<T> GetAll();
        T Add(T item);
        void Update(T item);
        void Remove(string id);
    }
}
