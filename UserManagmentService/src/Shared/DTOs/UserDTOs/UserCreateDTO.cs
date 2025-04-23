namespace Shared.DTOs.UserDTOs;

public record class UserCreateDTO (
    string UserName,
    string UserEmail,
    string PasswordHash
);