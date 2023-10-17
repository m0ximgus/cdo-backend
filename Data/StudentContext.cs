using Microsoft.EntityFrameworkCore;
using Kursach.Models;

namespace Kursach.Data;

public class StudentContext : DbContext
{
    public StudentContext (DbContextOptions<StudentContext> options) : base(options)
    {

    }

    public DbSet<Student> Students => Set<Student>();
}

public class AuthContext: DbContext
{
    public AuthContext (DbContextOptions<AuthContext> options) : base(options)
    {

    }

    public DbSet<Auth> Auths=> Set<Auth>();
}