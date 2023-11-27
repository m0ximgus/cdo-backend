using Microsoft.EntityFrameworkCore;
using Kursach.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace Kursach.Data;

public class KursachContext : DbContext
{
    public KursachContext(DbContextOptions<KursachContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\SHARP-PROJECTS\KURSACH4\KURSACH\KURSACH\DB\KURSACH.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    public DbSet<Authorization> Authorizations => Set<Authorization>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<JobTitle> JobTitles => Set<JobTitle>();
    public DbSet<Journal> Journals => Set<Journal>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
}

public class AuthorizationContextFactory : IDesignTimeDbContextFactory<KursachContext>
{
    public KursachContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<KursachContext>();
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\SHARP-PROJECTS\\KURSACH4\\KURSACH\\KURSACH\\DB\\KURSACH.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        return new KursachContext(optionsBuilder.Options);
    }
}