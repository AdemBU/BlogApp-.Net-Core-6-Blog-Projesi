using Blog.Core.Entities;
using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Data.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        private DbSet<T> Table { get => _context.Set<T>(); }

        // Task => void ile aynı anlamdadır. Geriye bir şey döndürmez. Farkı asenkron çalışır.

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        // predicate → filtre koşulu (örneğin x => x.Price > 100)
        // includeProperties → navigation property’leri dahil eder(örneğin x => x.Category)
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            if (predicate != null)
                query = query.Where(predicate);  //Filtre varsa Where uygular.

            if (includeProperties.Any())
                foreach (var item in includeProperties)
                    query = query.Include(item);  //Navigation property varsa Include uygular. Dahil edilecek ilişkiler varsa Include ile ekler.

            return await query.ToListAsync();  //Son olarak listeye çevirir ve döner.
        }


        //predicate zorunlu (örneğin x => x.Id == id). Gerekirse ilişkili tablolar(Include) eklenir.
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);

            if (includeProperties.Any())
                foreach (var item in includeProperties)
                    query = query.Include(item);

            return await query.SingleAsync();  //SingleAsync() → tam olarak 1 kayıt bekler. Eğer hiç veya birden fazla varsa hata atar.
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            //Asenkron gibi görünse de aslında Task.Run ile sahte bir async yapılmış. Gerçek bir async işlem değil(çünkü EF Core Update zaten senkron çalışır).
            await Task.Run(() => Table.Update(entity));
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
            //Belirli bir koşula göre kayıt olup olmadığını kontrol eder.
            //örnek => bool exists = await _userRepository.AnyAsync(x => x.Email == "test@gmail.com");
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await Table.CountAsync(predicate);
            //Kayıt sayısını döner.
        }
    }
}
