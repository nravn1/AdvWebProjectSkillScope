using CSCI3110KRTERMPROJ.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110KRTERMPROJ.Services;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    // The purpose of defining a `DbSet<entityname>` in your `ApplicationDbContext` is to
    // map the `Entityname` entity class to a corresponding table in the database.
    
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Privilege> Privileges => Set<Privilege>();
    public DbSet<PersonPrivilege> PersonPrivileges { get; set; }
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<PersonRole> PersonRoles => Set<PersonRole>();
    public DbSet<PersonSkill> PersonSkills => Set<PersonSkill>();
    public DbSet<RoleSkill> RoleSkills => Set<RoleSkill>();
    public DbSet<RoleSkill> Skill => Set<RoleSkill>();

}
