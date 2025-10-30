using Blog.Core.Entities;
using Blog.Data.Repositories.Abstractions;

namespace Blog.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        //Yani tek bir UnitOfWork örneği üzerinden farklı tablolarla çalışabilirsin. Generic Repository pattern kullanarak farklı entity türleri için repository sağlar.
        IRepository<T> GetRepository<T>() where T : class, IEntityBase, new();

        Task<int> SaveChangesAsync();
        int Save();
    }
}
