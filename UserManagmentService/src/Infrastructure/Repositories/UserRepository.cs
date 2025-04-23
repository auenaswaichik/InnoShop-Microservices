using Domain.Entities;
using Domain.IRepositories;
using Infrastrucure.DbContexts;
using Shared.DTOs.UserDTOs;

namespace Infrastrucure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManagmentDbContext _context;
    public UserRepository(UserManagmentDbContext context) => _context = context;
    
    public Task<User> CreateUser(UserCreateDTO userCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById()
    {
        throw new NotImplementedException();
    }

    public Task RemoveUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User> Updateuser(Guid id, UserUpdateDTO userUpdateDTO)
    {
        throw new NotImplementedException();
    }
}