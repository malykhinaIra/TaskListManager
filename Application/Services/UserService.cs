using AutoMapper;
using TestTask1.Application.DataTransferObjects.Requests;
using TestTask1.Application.Services.Interfaces;
using TestTask1.Domain.Entities;
using TestTask1.Domain.Interfaces;

namespace TestTask1.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> CreateUserAsync(CreateUserRequest request)
    {
        var userEntity = _mapper.Map<User>(request);
       
        await _userRepository.CreateAsync(userEntity);
        
        return true;
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        return await _userRepository.ExistsAsync(userId);
    }
} 