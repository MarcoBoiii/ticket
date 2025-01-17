using Models.Interfaces;
using Models;
using Services.Shared;
using Services.Dto;

namespace Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) =>
        _userRepository = userRepository;

    public async Task CreateUser(UserDto dto)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            IsEmployee = dto.IsEmployee,
            Password = dto.Password.Encrypt(),
        };
        await _userRepository.AddAsync(user);
    }

    public async Task<User?> GetUserAsync(string email, string password)
    {
        return await _userRepository.GetUserAsync(email, password.Encrypt());
    }
    public async Task DeleteUserById(Guid userId)
    {
        await _userRepository.DeleteAsync(userId);
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task UpdateUser(UserDto dto)
    {
        await _userRepository.UpdateAsync(new User
        {
            Id = dto.Id,
            FirstName = dto.FirstName ?? string.Empty,
            LastName = dto.LastName ?? string.Empty,
            Password = dto.Password != null ? dto.Password.Encrypt() : string.Empty,
            Email = dto.Email ?? string.Empty,
        });
    }

    public async Task<(string FirstName, string LastName)?> GetUserNameById(Guid id)
    {
        return await _userRepository.GetUserNameByIdAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }
}
