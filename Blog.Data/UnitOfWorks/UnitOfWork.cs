using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        // Unit of Work: Birden fazla veritabanı işlemini tek bir bütün (transaction) olarak yönetmek
        //UnitOfWork = Tüm repository’leri bir araya getirip tek transaction haline getirir.
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            //Kaynak temizleme
            await _context.DisposeAsync();
        }

        public int Save()
        {
            //Senkron kayıt
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            //Asenkron kayıt
            return await _context.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            //Repository döner
            return new Repository<T>(_context);
        }
    }
}
