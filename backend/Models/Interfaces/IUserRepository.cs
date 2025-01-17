namespace Models.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserAsync( string email, string password );
    Task<User?> GetByIdAsync(Guid id);
    Task<(string FirstName, string LastName)?> GetUserNameByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
}
