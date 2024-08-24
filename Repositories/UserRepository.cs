using ApiDevBP.Repository;
using ApiDevBP.Repository.Entities;
using ApiDevBP.Repository.Interfaces;
using Microsoft.Extensions.Options;
using SQLite;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLiteConnection _db;
        private readonly DbOptions _dbOptions;

        public UserRepository(IOptions<DbOptions> dbOptions)
        {
            this._dbOptions = dbOptions.Value;

            _db = new SQLiteConnection(_dbOptions.ConnectionString);
            _db.CreateTable<UserEntity>();
        }                
        public IEnumerable<UserEntity> GetAll()
        {
            return _db.Query<UserEntity>($"SELECT * FROM Users");
        }
        public UserEntity Get(int id)
        {
            return _db.Find<UserEntity>(id);
        }
        public IEnumerable<UserEntity> Get(string name,string lastName)
        {
            return _db.Table<UserEntity>()
                .Where(x => x.Name == name && x.Lastname == lastName);                
        }
        public bool Add(UserEntity userEntity)
        {  
            var result = _db.Insert(userEntity);

            return result > 0;
        }
        public bool Update(UserEntity userEntity)
        {
            var result = _db.Update(userEntity);

            return result > 0;
        }
        public bool Delete(UserEntity userEntity)
        {
            var result = _db.Delete(userEntity);

            return result > 0;
        }
    }
}
