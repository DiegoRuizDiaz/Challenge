using ApiDevBP.Repository.Entities;

namespace ApiDevBP.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserEntity> GetAll();
        UserEntity Get(int id);
        IEnumerable<UserEntity> Get(string name, string lastName);
        bool Add(UserEntity userEntity);
        bool Update(UserEntity userEntity);
        bool Delete(UserEntity userEntity);
    }
}
