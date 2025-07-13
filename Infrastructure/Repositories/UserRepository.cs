using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTask1.Domain.Entities;
using TestTask1.Domain.Interfaces;
using TestTask1.Infrastructure.Data;
using TestTask1.Infrastructure.Data.Models;

namespace TestTask1.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskListDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(TaskListDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User> CreateAsync(User user)
    {
        var userModel = _mapper.Map<UserModel>(user);
        _context.Users.Add(userModel);
        await _context.SaveChangesAsync();
        return _mapper.Map<User>(userModel);
    }

    public async Task<bool> ExistsAsync(string userId)
    {
        return await _context.Users.AnyAsync(u => u.UserId == userId);
    }
} 