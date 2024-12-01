using BaseApplication.Repository.Data;
using System.Data.Entity.Infrastructure;

namespace BaseApplication.Repository.Interface
{
    public interface IUnitofWork
    {
        BaseApplicationDbContext GetContext();
        BaseApplicationDbContext GetNewContext();
        // IRepository<UserProfile> UserRepository { get; }
        DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters);

        void Save();
        void Dispose();
    }
}
