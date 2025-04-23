using Domain.Entities;
using Shared.DTOs.UserDTOs;

namespace Application.IServices;

public interface IUserService {
    public Task<List<UserDTO>> GetAll();
    public Task<UserDTO> GetById();
    public Task RemoveUser(Guid id);
    public Task<User> UpdateUser(Guid id, UserUpdateDTO userUpdateDTO);
    public Task<User> CreateUser(UserCreateDTO userCreateDTO);
}