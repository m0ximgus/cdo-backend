using Kursach.Models;
using Kursach.Data;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

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

    public void nameUpdate(int id, string newName)
    {
        var studentToUpdate = _context.Students.Find(id);

        if (studentToUpdate is null || newName is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.name = newName;

        _context.SaveChanges();
    }

    public void groupUpdate(int id, string newGroup)
    {
        var studentToUpdate = _context.Students.Find(id);

        if (studentToUpdate is null || newGroup is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.group = newGroup;

        _context.SaveChanges();
    }

    public void ageUpdate(int id, string newAge)
    {
        var studentToUpdate = _context.Students.Find(id);

        if (studentToUpdate is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.age = Int32.Parse(newAge);

        _context.SaveChanges();
    }

    public void admissionTimeUpdate(int id, string newAdmissionTime)
    {
        var studentToUpdate = _context.Students.Find(id);

        if (studentToUpdate is null || newAdmissionTime is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.admissionTime = newAdmissionTime;

        _context.SaveChanges();
    }

    public void contactMailUpdate(int id, string newContactMail)
    {
        var studentToUpdate = _context.Students.Find(id);

        if (studentToUpdate is null || newContactMail is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.contactMail = newContactMail;

        _context.SaveChanges();
    }

    public void contactPhoneUpdate(int id, string newContactPhone)
    {
        var studentToUpdate = _context.Students.Find(id);

        if (studentToUpdate is null || newContactPhone is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.contactPhone = newContactPhone;

        _context.SaveChanges();
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

    public void usernameUpdate(int id, string newUsername)
    {
        var authToUpdate = _context.Auths.Find(id);

        if (authToUpdate is null || newUsername is null)
            throw new InvalidOperationException("There some problem.");

        authToUpdate.username = newUsername;

        _context.SaveChanges();
    }

    public void passwordUpdate(int id, string newPassword)
    {
        var authToUpdate = _context.Auths.Find(id);

        if (authToUpdate is null || newPassword is null)
            throw new InvalidOperationException("There some problem.");

        authToUpdate.password = newPassword;

        _context.SaveChanges();
    }

    public void typeUpdate(int id, string newType)
    {
        var authToUpdate = _context.Auths.Find(id);

        if (authToUpdate is null)
            throw new InvalidOperationException("There some problem.");

        authToUpdate.type = Int32.Parse(newType);

        _context.SaveChanges();
    }
}