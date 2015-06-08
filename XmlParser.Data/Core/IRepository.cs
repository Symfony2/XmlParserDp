using System.Linq;

namespace XmlParser.Data.Core
{
    public partial interface IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        T GetById(TKey id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
    }
}