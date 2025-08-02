using BlazorApp1.Data;
using BlazorApp1.Entities;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlazorApp1.Services.Entities;

public class UserServices
{
    private readonly AppDbContext dbContext;

    public UserServices(AppDbContext dbContext) => this.dbContext = dbContext;

    public async Task<TransactionResultAdd> CreateAsync(User user, string password)
    {
        user.Username = user.Username.Trim();
        var userExists = await dbContext.Users.AnyAsync(u => u.Username == user.Username.Trim());

        if (userExists)
            return new()
            {
                IsSuccess = false,
                RowChangedCount = 0,
                Error = "کاربر مورد نظر موجود است"
            };

        user.PasswordHash = Argon2.Hash(password);
        await dbContext.Users.AddAsync(user);
        var result = await dbContext.SaveChangesAsync();

        return new TransactionResultAdd()
        {
            IsSuccess = result > 0,
            RowChangedCount = result
        };
    }

    public async Task<bool> SignInAsync(User user,string password,bool isPersistence)
    {
        var foundUserByUsername = await FindByUserName(user.Username);
        if (foundUserByUsername == null)
            return false;

        var foundUser = await dbContext.Users.FindAsync(foundUserByUsername.Id);
        if (foundUser == null)
            return false;

        return await Task.Run(() => Argon2.Verify(foundUser.PasswordHash, password));
    }

    private async Task<User> FindByUserName(string username)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}

public class TransactionResultAdd
{
    public bool IsSuccess { get; set; }
    public int RowChangedCount { get; set; }
    public string? Error { get; set; }
}


public interface ICurrentUser
{
    Task<User?> GetCurrentUser();
}

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _dbContext;
    private User? _cachedUser;

    public CurrentUser(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public async Task<User?> GetCurrentUser()
    {
        if (_cachedUser != null)
            return _cachedUser;

        var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        if (string.IsNullOrWhiteSpace(username))
            return null;

        _cachedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        return _cachedUser;
    }
}