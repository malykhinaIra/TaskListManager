using TestTask1.Application.DataTransferObjects.Requests;

namespace TestTask1.Application.Services.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(CreateUserRequest request);
    Task<bool> UserExistsAsync(string userId);
} 