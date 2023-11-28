using Kursach.Models;
using Kursach.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kursach.Services;
public class AuthorizationService
{
    private readonly KursachContext authContext;

    public AuthorizationService(KursachContext context)
    {
        authContext = context;
    }

    public IEnumerable<Authorization> GetAll()
    {
        return authContext.Authorizations.AsNoTracking().ToList();
    }

    public Authorization? GetById(int id)
    {
        return authContext.Authorizations.AsNoTracking().SingleOrDefault(p => p.authToken == id);
    }

    public Authorization? GetByLogin(string login, string password)
    {
        return authContext.Authorizations.AsNoTracking().SingleOrDefault(p => (p.login == login && p.password == password));
    }

    public Authorization Add(Authorization auth)
    {
        authContext.Authorizations.Add(auth);
        authContext.SaveChanges();

        return auth;
    }

    public void Delete(int id)
    {
        var authToDelete = authContext.Authorizations.Find(id);
        if (authToDelete is not null)
        {
            authContext.Authorizations.Remove(authToDelete);
            authContext.SaveChanges();
        }
    }

    public void usernameUpdate(int id, string newLogin)
    {
        var authToUpdate = authContext.Authorizations.Find(id);

        if (authToUpdate is null || newLogin is null)
            throw new InvalidOperationException("There some problem.");

        authToUpdate.login = newLogin;

        authContext.SaveChanges();
    }

    public void passwordUpdate(int id, string newPassword)
    {
        var authToUpdate = authContext.Authorizations.Find(id);

        if (authToUpdate is null || newPassword is null)
            throw new InvalidOperationException("There some problem.");

        authToUpdate.password = newPassword;

        authContext.SaveChanges();
    }

    public void typeUpdate(int id, string newType)
    {
        var authToUpdate = authContext.Authorizations.Find(id);

        if (authToUpdate is null)
            throw new InvalidOperationException("There some problem.");

        authToUpdate.type = newType;

        authContext.SaveChanges();
    }
}

public class EmployeeService
{
    private readonly KursachContext empContext;

    public EmployeeService(KursachContext context)
    {
        empContext = context;
    }

    public IEnumerable<Employee> GetAll()
    {
        return empContext.Employees.AsNoTracking().ToList();
    }

    public Employee? GetById(int id)
    {
        return empContext.Employees.AsNoTracking().SingleOrDefault(p => p.employeeID == id);
    }

    public IEnumerable<Employee>? GetByJobId(int id)
    {
        return empContext.Employees.AsNoTracking().ToList().Where(p => p.jobID == id);
    }

    public Employee Add(Employee employee)
    {
        empContext.Employees.Add(employee);
        empContext.SaveChanges();

        return employee;
    }

    public void nameUpdate(int id, string newName)
    {
        var employeeToUpdate = empContext.Employees.Find(id);

        if (employeeToUpdate is null || newName is null)
            throw new InvalidOperationException("There some problem.");

        employeeToUpdate.fullNameEmployee = newName;

        empContext.SaveChanges();
    }

    public void jobUpdate(int id, string newJob)
    {
        var employeeToUpdate = empContext.Employees.Find(id);

        if (employeeToUpdate is null || newJob is null)
            throw new InvalidOperationException("There some problem.");

        employeeToUpdate.jobID = Int32.Parse(newJob);

        empContext.SaveChanges();
    }

    public void phoneUpdate(int id, string newPhone)
    {
        var employeeToUpdate = empContext.Employees.Find(id);

        if (employeeToUpdate is null || newPhone is null)
            throw new InvalidOperationException("There some problem.");

        employeeToUpdate.contactPhoneEmployee = newPhone;

        empContext.SaveChanges();
    }

    public void mailUpdate(int id, string newMail)
    {
        var employeeToUpdate = empContext.Employees.Find(id);

        if (employeeToUpdate is null || newMail is null)
            throw new InvalidOperationException("There some problem.");

        employeeToUpdate.contactMailEmployee = newMail;

        empContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var employeeToDelete = empContext.Employees.Find(id);
        if (employeeToDelete is not null)
        {
            empContext.Employees.Remove(employeeToDelete);
            empContext.SaveChanges();
        }
    }
}

public class GroupService
{
    private readonly KursachContext groupContext;

    public GroupService(KursachContext context)
    {
        groupContext = context;
    }

    public IEnumerable<Group> GetAll()
    {
        return groupContext.Groups.AsNoTracking().ToList();
    }

    public Group? GetById(int id)
    {
        return groupContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == id);
    }


    public Group Add(Group employee)
    {
        groupContext.Groups.Add(employee);
        groupContext.SaveChanges();

        return employee;
    }

    public void nameUpdate(int id, string newName)
    {
        var groupToUpdate = groupContext.Groups.Find(id);

        if (groupToUpdate is null || newName is null)
            throw new InvalidOperationException("There some problem.");

        groupToUpdate.groupName = newName;

        groupContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var employeeToDelete = groupContext.Groups.Find(id);
        if (employeeToDelete is not null)
        {
            groupContext.Groups.Remove(employeeToDelete);
            groupContext.SaveChanges();
        }
    }
}

public class JobTitleService
{
    private readonly KursachContext jobTitleContext;

    public JobTitleService(KursachContext context)
    {
        jobTitleContext = context;
    }

    public IEnumerable<JobTitle> GetAll()
    {
        return jobTitleContext.JobTitles.AsNoTracking().ToList();
    }

    public JobTitle? GetById(int id)
    {
        return jobTitleContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == id);
    }


    public JobTitle Add(JobTitle employee)
    {
        jobTitleContext.JobTitles.Add(employee);
        jobTitleContext.SaveChanges();

        return employee;
    }

    public void Delete(int id)
    {
        var employeeToDelete = jobTitleContext.JobTitles.Find(id);
        if (employeeToDelete is not null)
        {
            jobTitleContext.JobTitles.Remove(employeeToDelete);
            jobTitleContext.SaveChanges();
        }
    }

    public void nameUpdate(int id, string newName)
    {
        var jobToUpdate = jobTitleContext.JobTitles.Find(id);

        if (jobToUpdate is null || newName is null)
            throw new InvalidOperationException("There some problem.");

        jobToUpdate.jobName = newName;

        jobTitleContext.SaveChanges();
    }

    public void salaryUpdate(int id, string newSalary)
    {
        var jobToUpdate = jobTitleContext.JobTitles.Find(id);

        if (jobToUpdate is null || newSalary is null)
            throw new InvalidOperationException("There some problem.");

        jobToUpdate.salary = Int32.Parse(newSalary);

        jobTitleContext.SaveChanges();
    }
}

public class JournalService
{
    private readonly KursachContext journalContext;

    public JournalService(KursachContext context)
    {
        journalContext = context;
    }

    public IEnumerable<Journal> GetAll()
    {
        return journalContext.Journals.AsNoTracking().ToList();
    }

    public IEnumerable<Journal>? GetById(int id)
    {
        return journalContext.Journals.AsNoTracking().ToList().Where(p => p.groupID == id);
    }

    public Journal Add(Journal journal)
    {
        journalContext.Journals.Add(journal);
        journalContext.SaveChanges();

        return journal;
    }

    public void markUpdate(int id, string newMark)
    {
        var journalToUpdate = journalContext.Journals.Find(id);

        if (journalToUpdate is null || newMark is null)
            throw new InvalidOperationException("There some problem.");

        journalToUpdate.mark = Int32.Parse(newMark);

        journalContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var journalToDelete = journalContext.Journals.Find(id);
        if (journalToDelete is not null)
        {
            journalContext.Journals.Remove(journalToDelete);
            journalContext.SaveChanges();
        }
    }
}

public class LessonService
{
    private readonly KursachContext lessonContext;

    public LessonService(KursachContext context)
    {
        lessonContext = context;
    }

    public IEnumerable<Lesson> GetAll()
    {
        return lessonContext.Lessons.AsNoTracking().ToList();
    }

    public Lesson? GetById(int id)
    {
        return lessonContext.Lessons.AsNoTracking().SingleOrDefault(p => p.groupID == id);
    }


    public Lesson Add(Lesson employee)
    {
        lessonContext.Lessons.Add(employee);
        lessonContext.SaveChanges();

        return employee;
    }

    public void classUpdate(int id, string newClass)
    {
        var lessonToUpdate = lessonContext.Lessons.Find(id);

        if (lessonToUpdate is null || newClass is null)
            throw new InvalidOperationException("There some problem.");

        lessonToUpdate.classroom = newClass;

        lessonContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var employeeToDelete = lessonContext.Lessons.Find(id);
        if (employeeToDelete is not null)
        {
            lessonContext.Lessons.Remove(employeeToDelete);
            lessonContext.SaveChanges();
        }
    }
}

public class PaymentService
{
    private readonly KursachContext paymentContext;

    public PaymentService(KursachContext context)
    {
        paymentContext = context;
    }

    public IEnumerable<Payment> GetAll()
    {
        return paymentContext.Payments.AsNoTracking().ToList();
    }

    public Payment? GetById(int id)
    {
        return paymentContext.Payments.AsNoTracking().SingleOrDefault(p => p.studentID == id);
    }


    public Payment Add(Payment payment)
    {
        paymentContext.Payments.Add(payment);
        paymentContext.SaveChanges();

        return payment;
    }

    public void Delete(int id)
    {
        var paymentToDelete = paymentContext.Payments.Find(id);
        if (paymentToDelete is not null)
        {
            paymentContext.Payments.Remove(paymentToDelete);
            paymentContext.SaveChanges();
        }
    }
}

public class StudentService
{
    private readonly KursachContext studentContext;

    public StudentService(KursachContext context)
    {
        studentContext = context;
    }

    public IEnumerable<Student> GetAll()
    {
        return studentContext.Students.AsNoTracking().ToList();
    }

    public Student? GetById(int id)
    {
        return studentContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == id);
    }

    public IEnumerable<Student>? GetByGroupId(int id)
    {
        return studentContext.Students.AsNoTracking().ToList().Where(p => p.groupID == id);
    }

    public Student Add(Student student)
    {
        studentContext.Students.Add(student);
        studentContext.SaveChanges();

        return student;
    }

    public void Delete(int id)
    {
        var studentToDelete = studentContext.Students.Find(id);
        if (studentToDelete is not null)
        {
            studentContext.Students.Remove(studentToDelete);
            studentContext.SaveChanges();
        }
    }

    public void nameUpdate(int id, string newName)
    {
        var studentToUpdate = studentContext.Students.Find(id);

        if (studentToUpdate is null || newName is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.fullNameStudent = newName;

        studentContext.SaveChanges();
    }

    public void groupUpdate(int id, string newGroup)
    {
        var studentToUpdate = studentContext.Students.Find(id);

        if (studentToUpdate is null || newGroup is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.groupID = Int32.Parse(newGroup);

        studentContext.SaveChanges();
    }

    public void ageUpdate(int id, string newAge)
    {
        var studentToUpdate = studentContext.Students.Find(id);

        if (studentToUpdate is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.age = DateTime.Parse(newAge);

        studentContext.SaveChanges();
    }

    public void enrollmentTimeUpdate(int id, string newEnrollmentTime)
    {
        var studentToUpdate = studentContext.Students.Find(id);

        if (studentToUpdate is null || newEnrollmentTime is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.enrollmentDate = DateTime.Parse(newEnrollmentTime);

        studentContext.SaveChanges();
    }

    public void contactMailUpdate(int id, string newContactMail)
    {
        var studentToUpdate = studentContext.Students.Find(id);

        if (studentToUpdate is null || newContactMail is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.contactMailStudent = newContactMail;

        studentContext.SaveChanges();
    }

    public void contactPhoneUpdate(int id, string newContactPhone)
    {
        var studentToUpdate = studentContext.Students.Find(id);

        if (studentToUpdate is null || newContactPhone is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.contactPhoneStudent = newContactPhone;

        studentContext.SaveChanges();
    }

    public void genderUpdate(int id, string newGender)
    {
        var studentToUpdate = studentContext.Students.Find(id);

        if (studentToUpdate is null || newGender is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.gender = bool.Parse(newGender);

        studentContext.SaveChanges();
    }
}

public class SubjectService
{
    private readonly KursachContext subjectContext;

    public SubjectService(KursachContext context)
    {
        subjectContext = context;
    }

    public IEnumerable<Subject> GetAll()
    {
        return subjectContext.Subjects.AsNoTracking().ToList();
    }

    public Subject? GetById(int id)
    {
        return subjectContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == id);
    }


    public Subject Add(Subject employee)
    {
        subjectContext.Subjects.Add(employee);
        subjectContext.SaveChanges();

        return employee;
    }

    public void nameUpdate(int id, string newName)
    {
        var subjectToUpdate = subjectContext.Subjects.Find(id);

        if (subjectToUpdate is null || newName is null)
            throw new InvalidOperationException("There some problem.");

        subjectToUpdate.subjectName = newName;

        subjectContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var employeeToDelete = subjectContext.Subjects.Find(id);
        if (employeeToDelete is not null)
        {
            subjectContext.Subjects.Remove(employeeToDelete);
            subjectContext.SaveChanges();
        }
    }
}

public class TeacherService
{
    private readonly KursachContext teacherContext;

    public TeacherService(KursachContext context)
    {
        teacherContext = context;
    }

    public IEnumerable<Teacher> GetAll()
    {
        return teacherContext.Teachers.AsNoTracking().ToList();
    }

    public Teacher? GetById(int id)
    {
        return teacherContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == id);
    }

    public IEnumerable<Teacher>? GetByJobId(int id)
    {
        return teacherContext.Teachers.AsNoTracking().ToList().Where(p => p.jobID == id);
    }

    public Teacher Add(Teacher employee)
    {
        teacherContext.Teachers.Add(employee);
        teacherContext.SaveChanges();

        return employee;
    }

    public void nameUpdate(int id, string newName)
    {
        var teacherToUpdate = teacherContext.Teachers.Find(id);

        if (teacherToUpdate is null || newName is null)
            throw new InvalidOperationException("There some problem.");

        teacherToUpdate.fullNameTeacher = newName;

        teacherContext.SaveChanges();
    }

    public void jobUpdate(int id, string newJob)
    {
        var teacherToUpdate = teacherContext.Teachers.Find(id);

        if (teacherToUpdate is null || newJob is null)
            throw new InvalidOperationException("There some problem.");

        teacherToUpdate.jobID = Int32.Parse(newJob);

        teacherContext.SaveChanges();
    }

    public void phoneUpdate(int id, string newPhone)
    {
        var teacherToUpdate = teacherContext.Teachers.Find(id);

        if (teacherToUpdate is null || newPhone is null)
            throw new InvalidOperationException("There some problem.");

        teacherToUpdate.contactPhoneTeacher = newPhone;

        teacherContext.SaveChanges();
    }

    public void mailUpdate(int id, string newMail)
    {
        var teacherToUpdate = teacherContext.Teachers.Find(id);

        if (teacherToUpdate is null || newMail is null)
            throw new InvalidOperationException("There some problem.");

        teacherToUpdate.contactMailTeacher = newMail;

        teacherContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var employeeToDelete = teacherContext.Teachers.Find(id);
        if (employeeToDelete is not null)
        {
            teacherContext.Teachers.Remove(employeeToDelete);
            teacherContext.SaveChanges();
        }
    }
}