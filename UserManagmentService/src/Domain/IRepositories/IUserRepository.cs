using Domain.Entities;
using Shared.DTOs.UserDTOs;

namespace Domain.IRepositories;

public interface IUserRepository {

    public Task<List<User>> GetAll();
    public Task<User> GetById();
    public Task RemoveUser(Guid id);
    public Task<User> CreateUser(UserCreateDTO userCreateDTO);
    public Task<User> Updateuser(Guid id, UserUpdateDTO userUpdateDTO);

}