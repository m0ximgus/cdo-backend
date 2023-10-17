using Kursach.Models;
using Kursach.Data;
using Microsoft.EntityFrameworkCore;

namespace Kursach.Services;

public class StudentService
{
    private readonly StudentContext _context;

    public StudentService(StudentContext context)
    {
        _context = context;
    }

    public IEnumerable<Student> GetAll()
    {
        return _context.Students .AsNoTracking() . ToList();
    }

    public Student? GetById(int id)
    {
        return _context.Students .AsNoTracking() .SingleOrDefault(p => p.Id == id);
    }

    public Student Add(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();

        return student;
    }

    public void Delete(int id)
    {
       var studentToDelete = _context.Students.Find(id);
       if (studentToDelete is not null)
       {
            _context.Students.Remove(studentToDelete);
            _context.SaveChanges();
       }
    }

    public static void Update(Student student)
    {
        //Обновление данных студентика (ДУМОЙ КАК РЕАЛИЗОВАТЬ)
    }
}

public class AuthService
{
    private readonly AuthContext _context;

    public AuthService(AuthContext context)
    {
        _context = context;
    }

    public IEnumerable<Auth> GetAll()
    {
        return _context.Auths .AsNoTracking() . ToList();
    }

    public Auth? GetById(int id)
    {
        return _context.Auths .AsNoTracking() .SingleOrDefault(p => p.AuthId == id);
    }

    public Auth? GetByLogin(string login, string password)
    {
        return _context.Auths .AsNoTracking() .SingleOrDefault(p => (p.username == login && p.password == password));
    }

    public Auth Add(Auth auth)
    {
        _context.Auths.Add(auth);
        _context.SaveChanges();

        return auth;
    }

    public void Delete(int id)
    {
       var authToDelete = _context.Auths.Find(id);
       if (authToDelete is not null)
       {
            _context.Auths.Remove(authToDelete);
            _context.SaveChanges();
       }
    }

    public static void Update(Auth auth)
    {
        //Обновление данных студентика (ДУМОЙ КАК РЕАЛИЗОВАТЬ)
    }
}