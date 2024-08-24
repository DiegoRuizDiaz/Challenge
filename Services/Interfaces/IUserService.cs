using ApiDevBP.Services.Models;

namespace ApiDevBP.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> Get(int id);
        Task<IEnumerable<UserModelWithId>> Get(string name, string lastName);
        Task AddUser(UserModel userModel);        
        Task UpdateUser(int id,UserModel userModel);
        Task DeleteUser(int id);
    }
}
