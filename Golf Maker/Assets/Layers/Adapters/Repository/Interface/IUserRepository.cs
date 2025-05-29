using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<List<UserEntity>> GetAll();
    Task<UserEntity> Create(UserEntity user);
    Task<UserEntity> Update(int id, UserEntity user);
    Task Delete(int id);
    Task<UserEntity> GetById(int id);

    Task<UserEntity> GetByUsername(string username);
} 