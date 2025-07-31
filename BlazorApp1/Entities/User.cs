using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public string Name { get => role; set { role = value.ToUpper(); } }
    public bool IsDeleted { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RoleClaim> RoleClaims { get; set; }
}

public class UserRole
{
    [Key]
    public int UserId { get; set; }
    [Key]
    public int RoleId { get; set; }
    public User User { get; set; }
    public Role Role { get; set; }
}

public class UserClaim
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ClaimId { get; set; }
    public User User { get; set; }
    public Claim Claim { get; set; }
}

public class RoleClaim
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
    public Role Role { get; set; }
    public Claim Claim { get; set; }
}

