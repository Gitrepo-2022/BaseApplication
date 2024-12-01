using BaseApplication.Repository.Data;
using BaseApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Repository.Repositories
{
    public class UnitOfWork : IUnitofWork
    {
        private BaseApplicationDbContext _context;
        private bool _isDisposed;
        private DbContextOptions<BaseApplicationDbContext> options;

        public UnitOfWork(BaseApplicationDbContext context)
        {
            this._context = context;
        }
        public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            return (DbRawSqlQuery<T>)this._context.Database.SqlQueryRaw<T>(sql, parameters);
        }
        public BaseApplicationDbContext GetContext()
        {
            return this._context;
        }

        public BaseApplicationDbContext GetNewContext()
        {
            try
            {
                this._context = _context;
                return this._context;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void Dispose()
        {
            if (_context != null && !_isDisposed)
            {
                _context.Dispose();
                _isDisposed = true;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
