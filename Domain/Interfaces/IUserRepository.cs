using TestTask1.Domain.Entities;

namespace TestTask1.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<bool> ExistsAsync(string userId);
} 