using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
namespace Models.Interfaces;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<UserRepository> _logger;


    public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddAsync(User user)
    {
        if(_context.Users.Any(x => x.Email == user.Email))
        {
            _logger.LogInformation($"User with Mail: {user.Email} Already Exists");
            return;
        }

        _context.Add(user);
        _context.SaveChanges();
        _logger.LogInformation($"User with ID: {user.Id} Successfully Created");
        await Task.CompletedTask;
    }

    public async Task<User?> GetUserAsync(string email, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Password == password);

        if (user is null)
        {
            _logger.LogError($"No user with email: {email} and password {password} found");
            return null;
        }

        return user;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            _logger.LogError($"User with ID: {id} not found.");
            return null;
        }
        return user;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            _logger.LogError($"User with ID: {id} Not Found");
            return;
        }

        _context.Remove(user);
        _logger.LogInformation($"User with ID: {id} Successfully Deleted");
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser == null)
        {
            _logger.LogWarning($"User with ID {user.Id} not found. Update operation aborted.");
            return;
        }

        existingUser.FirstName = !string.IsNullOrWhiteSpace(user.FirstName) ? user.FirstName : existingUser.FirstName;
        existingUser.LastName = !string.IsNullOrWhiteSpace(user.LastName) ? user.LastName : existingUser.LastName;
        existingUser.Email = !string.IsNullOrWhiteSpace(user.Email) ? user.Email : existingUser.Email;
        existingUser.Password = !string.IsNullOrWhiteSpace(user.Password) ? user.Password : existingUser.Password;

        _context.Users.Update(existingUser);
        _logger.LogInformation($"User with ID {user.Id} was successfully updated.");
        await _context.SaveChangesAsync();
    }

    public async Task<(string FirstName, string LastName)?> GetUserNameByIdAsync(Guid id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            _logger.LogWarning($"User with ID {id} not found.");
            return null;
        }

        return (user.FirstName, user.LastName);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
          .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            _logger.LogWarning($"User with Mailadress: {email} not found.");
            return null;
        }

        return (user);
    }
}
