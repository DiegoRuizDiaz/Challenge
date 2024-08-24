using ApiDevBP.Repository.Entities;
using ApiDevBP.Repository.Interfaces;
using ApiDevBP.Services.Interfaces;
using ApiDevBP.Services.Models;
using AutoMapper;

namespace ApiDevBP.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _iUserRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository iUserRepo, IMapper mapper)
        {
            _iUserRepo = iUserRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var users = _iUserRepo.GetAll();

            if (users == null || users.Count() == 0)
            {
                return Enumerable.Empty<UserModel>();
            }

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }
        //Get by Id
        public async Task<UserModel> Get(int id)
        {
            var user = _iUserRepo.Get(id);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<UserModel>(user);          
        }
        //Get by name and lastName
        public async Task<IEnumerable<UserModelWithId>> Get(string name, string lastName)
        {
            var users = _iUserRepo.Get(name,lastName);

            if (users == null)
            {
                return Enumerable.Empty<UserModelWithId>();
            }

            return _mapper.Map<IEnumerable<UserModelWithId>>(users);           
        }
        public async Task AddUser(UserModel userModel) 
        {
            //Validate object
            await this.validateRequest(userModel.Name,userModel.Lastname);

            var result = _iUserRepo.Add(_mapper.Map<UserEntity>(userModel));
            if (!result)
            {
                throw new InvalidOperationException("Failed when add user. 0 results affected");
            }
        }
        public async Task UpdateUser(int id, UserModel userModel) 
        {
            //Validate if user exists.
            var userEntity = this._iUserRepo.Get(id);
            if (userEntity == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            //Validate object
            await this.validateRequest(userModel.Name,userModel.Lastname);

            userEntity.Name = userModel.Name;
            userEntity.Lastname = userModel.Lastname;

            var result = this._iUserRepo.Update(userEntity);
            if (!result)
            {
                throw new InvalidOperationException("Failed when edit user. 0 results affected");
            }            
        }
        public async Task DeleteUser(int id)
        {
            //Validate if user exists.
            var userEntity = this._iUserRepo.Get(id);
            if (userEntity == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var result = this._iUserRepo.Delete(userEntity);
            if (!result) 
            {
                throw new InvalidOperationException("Failed when edit user. 0 results affected");
            }
        }      
        public async Task validateRequest(string name, string lastName)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Field " + nameof(name) + " cannot be empty.");
            }
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Field " + nameof(lastName) + " cannot be empty.");
            }
        }
    }
}
