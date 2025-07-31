using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace BlazorApp1.Entities;

public interface SoftDelete
{
    public bool IsDeleted { get; set; }
}

public class User : SoftDelete
{
    public int Id { get; set; }
    public int Username { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string PasswordHash { get; set; }
    [NotMapped]
    public bool IsPersistence { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<UserClaim> UserClaims { get; set; }
}

public class Role : SoftDelete
{
    private string role;

    public int Id { get; set; }
    public string Name { get => role; set { role = value?.ToUpper(); } }
    public bool IsDeleted { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    public ICollection<RoleClaim> RoleClaims { get; set; } = new HashSet<RoleClaim>();
}

public class UserRole : SoftDelete
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public User User { get; set; }
    public Role Role { get; set; }
    public bool IsDeleted { get; set; }
}

public class UserClaim : SoftDelete
{
    private string claimType;
    private string claimValue;

    public int Id { get; set; }
    public int UserId { get; set; }
    public string ClaimType { get => claimType; set { claimType = value?.ToUpper(); } }
    public string ClaimValue { get => claimValue; set { claimValue = value?.ToUpper(); } }
    public bool IsDeleted { get; set; }
    public User User { get; set; }
}

public class RoleClaim : SoftDelete
{
    private string claimType;
    private string claimValue;

    public int Id { get; set; }
    public int RoleId { get; set; }
    public string ClaimType { get => claimType; set { claimType = value?.ToUpper(); } }
    public string ClaimValue { get => claimValue; set { claimValue = value?.ToUpper(); } }
    public bool IsDeleted { get; set; }
    public Role Role { get; set; }
}

