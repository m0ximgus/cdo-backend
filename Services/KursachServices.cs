using Kursach.Models;
using Kursach.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kursach.Services;

public class AddonService
{
    private readonly KursachContext addonContext;

    public AddonService(KursachContext context)
    {
        addonContext = context;
    }

    public IEnumerable<Addon> GetAll()
    {
        var addons = addonContext.Addons.AsNoTracking().ToList().OrderBy(p => p.addonID);
        foreach (var addon in addons)
        {
            var lesson = addonContext.Lessons.AsNoTracking().SingleOrDefault(p => p.lessonID == addon.lessonID);
            lesson.Subject = addonContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            addon.Lesson = lesson;
        }
        return addons;
    }

    public Addon? GetById(int id)
    {
        var addon = addonContext.Addons.AsNoTracking().SingleOrDefault(p => p.addonID == id);
        var lesson = addonContext.Lessons.AsNoTracking().SingleOrDefault(p => p.lessonID == addon.lessonID);
        lesson.Subject = addonContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
        addon.Lesson = lesson; return addon;
    }

    public IEnumerable<Addon>? GetByLessonId(int lessonId)
    {
        var addons = addonContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lessonId).OrderBy(p => p.addonID);
        foreach (var addon in addons)
        {
            var lesson = addonContext.Lessons.AsNoTracking().SingleOrDefault(p => p.lessonID == addon.lessonID);
            lesson.Subject = addonContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            addon.Lesson = lesson;
        }
        return addons;
    }

    public Addon Add(Addon addon)
    {
        addonContext.Addons.Add(addon);
        addonContext.SaveChanges();

        return addon;
    }

    public void Delete(int id)
    {
        var addonToDelete = addonContext.Addons.Find(id);
        if (addonToDelete is not null)
        {
            addonContext.Addons.Remove(addonToDelete);
            addonContext.SaveChanges();
        }
    }

    public void headerUpdate(int id, string newHeader)
    {
        var addonToUpdate = addonContext.Addons.Find(id);

        if (addonToUpdate is null || newHeader is null)
            throw new InvalidOperationException("There some problem.");

        addonToUpdate.addonHeader = newHeader;

        addonContext.SaveChanges();
    }

    public void descriptionUpdate(int id, string newDescription)
    {
        var addonToUpdate = addonContext.Addons.Find(id);

        if (addonToUpdate is null || newDescription is null)
            throw new InvalidOperationException("There some problem.");

        addonToUpdate.addonDescription = newDescription;

        addonContext.SaveChanges();
    }
}

public class AuthorizationService
{
    private readonly KursachContext authContext;

    public AuthorizationService(KursachContext context)
    {
        authContext = context;
    }

    public IEnumerable<Authorization> GetAll()
    {
        var auths = authContext.Authorizations.AsNoTracking().ToList().OrderBy(p => p.authToken);
        foreach (var auth in auths)
        {
            if (auth.type == "student")
            {
                auth.Student = authContext.Students.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
                auth.Student.Group = authContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == auth.Student.groupID);
                var journals = authContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == auth.Student.studentID);
                foreach (var journal in journals)
                {
                    var lesson = authContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
                    lesson.Addons = authContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                    lesson.Subject = authContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                    lesson.Teacher = authContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
                    journal.Lessons = lesson;
                }
                auth.Student.Journal = journals;
                auth.Student.Payments = authContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == auth.Student.studentID);
            }
            else if (auth.type == "teacher")
            {
                auth.Teacher = authContext.Teachers.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
                auth.Teacher.JobTitles = authContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == auth.Teacher.jobID);
                var lessons = authContext.Lessons.AsNoTracking().ToList().Where(p => p.teacherID == auth.Teacher.teacherID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder) ;
                foreach (var lesson in lessons)
                {
                    lesson.Addons = authContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                    lesson.Group = authContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
                    lesson.Subject = authContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                    var journals = authContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                    foreach (var journal in journals)
                        journal.Students = authContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
                    journals.OrderBy(p => p.Students.fullNameStudent);
                    lesson.Journals = journals;
                }
                auth.Teacher.Lessons = lessons;
            }
            else if (auth.type == "employee")
            {
                auth.Employee = authContext.Employees.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
                auth.Employee.JobTitles = authContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == auth.Employee.jobID);

            }
        }
        return auths;
    }

    public Authorization? GetById(int id)
    {
        var auth = authContext.Authorizations.AsNoTracking().SingleOrDefault(p => p.authToken == id);

        if (auth.type == "student")
        {
            auth.Student = authContext.Students.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
            auth.Student.Group = authContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == auth.Student.groupID);
            var journals = authContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == auth.Student.studentID);
            foreach (var journal in journals)
            {
                var lesson = authContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
                lesson.Addons = authContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                lesson.Subject = authContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                lesson.Teacher = authContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
                journal.Lessons = lesson;
            }
            auth.Student.Journal = journals;
            auth.Student.Payments = authContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == auth.Student.studentID);
        }
        else if (auth.type == "teacher")
        {
            auth.Teacher = authContext.Teachers.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
            auth.Teacher.JobTitles = authContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == auth.Teacher.jobID);
            var lessons = authContext.Lessons.AsNoTracking().ToList().Where(p => p.teacherID == auth.Teacher.teacherID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
            foreach (var lesson in lessons)
            {
                lesson.Addons = authContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                lesson.Group = authContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
                lesson.Subject = authContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                var journals = authContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                foreach (var journal in journals)
                    journal.Students = authContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
                journals.OrderBy(p => p.Students.fullNameStudent);
                lesson.Journals = journals;
            }
            auth.Teacher.Lessons = lessons;
        }
        else if (auth.type == "employee")
        {
            auth.Employee = authContext.Employees.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
            auth.Employee.JobTitles = authContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == auth.Employee.jobID);

        }
        return auth;
    }

    public Authorization? GetByLogin(string login, string password)
    {
        var auth = authContext.Authorizations.AsNoTracking().SingleOrDefault(p => (p.login == login && p.password == password));
        if (auth.type == "student")
        {
            auth.Student = authContext.Students.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
            auth.Student.Group = authContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == auth.Student.groupID);
            var journals = authContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == auth.Student.studentID);
            foreach (var journal in journals)
            {
                var lesson = authContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
                lesson.Addons = authContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                lesson.Subject = authContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                lesson.Teacher = authContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
                journal.Lessons = lesson;
            }
            auth.Student.Journal = journals;
            auth.Student.Payments = authContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == auth.Student.studentID);
        }
        else if (auth.type == "teacher")
        {
            auth.Teacher = authContext.Teachers.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
            auth.Teacher.JobTitles = authContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == auth.Teacher.jobID);
            var lessons = authContext.Lessons.AsNoTracking().ToList().Where(p => p.teacherID == auth.Teacher.teacherID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
            foreach (var lesson in lessons)
            {
                lesson.Addons = authContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                lesson.Group = authContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
                lesson.Subject = authContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                var journals = authContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                foreach (var journal in journals)
                    journal.Students = authContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
                journals.OrderBy(p => p.Students.fullNameStudent);
                lesson.Journals = journals;
            }
            auth.Teacher.Lessons = lessons;
        }
        else if (auth.type == "employee")
        {
            auth.Employee = authContext.Employees.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
            auth.Employee.JobTitles = authContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == auth.Employee.jobID);

        }
        return auth;
    }

    public IEnumerable<Authorization> GetByType(string type)
    {
        var auths = authContext.Authorizations.AsNoTracking().ToList().Where(p => p.type == type).OrderBy(p => p.authToken);
        foreach (var auth in auths)
        {
            if (auth.type == "student")
            {
                auth.Student = authContext.Students.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
                auth.Student.Group = authContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == auth.Student.groupID);
                var journals = authContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == auth.Student.studentID);
                foreach (var journal in journals)
                {
                    var lesson = authContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
                    lesson.Addons = authContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                    lesson.Subject = authContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                    lesson.Teacher = authContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
                    journal.Lessons = lesson;
                }
                auth.Student.Journal = journals;
                auth.Student.Payments = authContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == auth.Student.studentID);
            }
            else if (auth.type == "teacher")
            {
                auth.Teacher = authContext.Teachers.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
                auth.Teacher.JobTitles = authContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == auth.Teacher.jobID);
                var lessons = authContext.Lessons.AsNoTracking().ToList().Where(p => p.teacherID == auth.Teacher.teacherID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
                foreach (var lesson in lessons)
                {
                    lesson.Addons = authContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                    lesson.Group = authContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
                    lesson.Subject = authContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                    var journals = authContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                    foreach (var journal in journals)
                        journal.Students = authContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
                    journals.OrderBy(p => p.Students.fullNameStudent);
                    lesson.Journals = journals;
                }
                auth.Teacher.Lessons = lessons;
            }
            else if (auth.type == "employee")
            {
                auth.Employee = authContext.Employees.AsNoTracking().SingleOrDefault(p => p.authToken == auth.authToken);
                auth.Employee.JobTitles = authContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == auth.Employee.jobID);

            }
        }
        return auths;
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
        var emps = empContext.Employees.AsNoTracking().ToList().OrderBy(p => p.fullNameEmployee);
        foreach (var emp in emps)
            emp.JobTitles = empContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == emp.jobID);
        return emps;
    }

    public Employee? GetByAuthToken(int authToken)
    {
        var emp = empContext.Employees.AsNoTracking().SingleOrDefault(p => p.authToken == authToken);
        emp.JobTitles = empContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == emp.jobID);
        return emp;
    }

    public Employee? GetById(int id)
    {
        var emp = empContext.Employees.AsNoTracking().SingleOrDefault(p => p.employeeID == id);
        emp.JobTitles = empContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == emp.jobID);
        return emp;
    }

    public IEnumerable<Employee>? GetByJobId(int id)
    {
        var emps = empContext.Employees.AsNoTracking().ToList().Where(p => p.jobID == id).OrderBy(p => p.fullNameEmployee);
        foreach (var emp in emps)
            emp.JobTitles = empContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == emp.jobID);
        return emps;
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
            var authToDelete = empContext.Authorizations.SingleOrDefault(p => p.authToken == employeeToDelete.authToken);
            empContext.Employees.Remove(employeeToDelete);
            empContext.Authorizations.Remove(authToDelete);
            empContext.SaveChanges();
        }
    }
}

public class EventService
{
    private readonly KursachContext eventContext;

    public EventService(KursachContext context)
    {
        eventContext = context;
    }

    public IEnumerable<Event> GetAll()
    {
        return eventContext.Events.AsNoTracking().ToList().OrderBy(p => p.eventDate);
    }

    public Event? GetById(int id)
    {
        return eventContext.Events.AsNoTracking().SingleOrDefault(p => p.eventID == id);
    }

    public Event Add(Event event_)
    {
        eventContext.Events.Add(event_);
        eventContext.SaveChanges();

        return event_;
    }

    public void headerUpdate(int id, string newHeader)
    {
        var eventToUpdate = eventContext.Events.Find(id);

        if (eventToUpdate is null || newHeader is null)
            throw new InvalidOperationException("There some problem.");

        eventToUpdate.eventHeader = newHeader;

        eventContext.SaveChanges();
    }

    public void discriptionUpdate(int id, string newDiscription)
    {
        var eventToUpdate = eventContext.Events.Find(id);

        if (eventToUpdate is null || newDiscription is null)
            throw new InvalidOperationException("There some problem.");

        eventToUpdate.eventDescription = newDiscription;

        eventContext.SaveChanges();
    }

    public void dateUpdate(int id, string newDate)
    {
        var eventToUpdate = eventContext.Events.Find(id);

        if (eventToUpdate is null || newDate is null)
            throw new InvalidOperationException("There some problem.");

        eventToUpdate.eventDate = DateTime.Parse(newDate);

        eventContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var eventToDelete = eventContext.Events.Find(id);
        if (eventToDelete is not null)
        {
            eventContext.Events.Remove(eventToDelete);
            eventContext.SaveChanges();
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
        var groups = groupContext.Groups.AsNoTracking().ToList().OrderBy(p => p.groupName);

        foreach (var group in groups)
        {
            group.Students = groupContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
            var lessons = groupContext.Lessons.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
            foreach (var lesson in lessons)
            {
                lesson.Addons = groupContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                lesson.Subject = groupContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                lesson.Teacher = groupContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
            }
            group.Lessons = lessons;
            group.Journal = groupContext.Journals.AsNoTracking().ToList().Where(p => p.groupID == group.groupID);
        }
        return groups;
    }

    public Group? GetById(int id)
    {
        var group = groupContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == id);
        group.Students = groupContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
        var lessons = groupContext.Lessons.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
        foreach (var lesson in lessons)
        {
            lesson.Addons = groupContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Subject = groupContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Teacher = groupContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
        }
        group.Lessons = lessons;
        group.Journal = groupContext.Journals.AsNoTracking().ToList().Where(p => p.groupID == group.groupID);
        return group;
    }

    public Group Add(Group group)
    {
        groupContext.Groups.Add(group);
        groupContext.SaveChanges();

        return group;
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
        var groupToDelete = groupContext.Groups.Find(id);
        if (groupToDelete is not null)
        {
            groupContext.Groups.Remove(groupToDelete);
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

    public JobTitle Add(JobTitle jobTitle)
    {
        jobTitleContext.JobTitles.Add(jobTitle);
        jobTitleContext.SaveChanges();

        return jobTitle;
    }

    public void Delete(int id)
    {
        var jobTitleToDelete = jobTitleContext.JobTitles.Find(id);
        if (jobTitleToDelete is not null)
        {
            jobTitleContext.JobTitles.Remove(jobTitleToDelete);
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
        var journals = journalContext.Journals.AsNoTracking().ToList();
        foreach (var journal in journals)
        {
            journal.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == journal.groupID);
            var lesson = journalContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
            lesson.Addons = journalContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            lesson.Subject = journalContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Teacher = journalContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
            journal.Lessons = lesson;
            journal.Students = journalContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
        }
        return journals;
    }

    public Journal? GetByAll(int studentId, int groupId, int lessonId)
    {
        var journal = journalContext.Journals.SingleOrDefault(p => p.studentID == studentId && p.groupID == groupId && p.lessonID == lessonId);
        journal.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == journal.groupID);
        var lesson = journalContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
        lesson.Addons = journalContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
        lesson.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
        lesson.Subject = journalContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
        lesson.Journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
        lesson.Teacher = journalContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
        journal.Lessons = lesson;
        journal.Students = journalContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
        return journal;
    }

    public IEnumerable<Journal>? GetByGroupId(int id)
    {
        var journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.groupID == id);
        foreach (var journal in journals)
        {
            journal.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == journal.groupID);
            var lesson = journalContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
            lesson.Addons = journalContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            lesson.Subject = journalContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Teacher = journalContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
            journal.Lessons = lesson;
            journal.Students = journalContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
        }
        return journals;
    }

    public IEnumerable<Journal>? GetByStudentId(int id)
    {
        var journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == id);
        foreach (var journal in journals)
        {
            journal.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == journal.groupID);
            var lesson = journalContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
            lesson.Addons = journalContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            lesson.Subject = journalContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Teacher = journalContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
            journal.Lessons = lesson;
            journal.Students = journalContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
        }
        return journals;
    }

    public IEnumerable<Journal>? GetByLessonId(int id)
    {
        var journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == id);
        foreach (var journal in journals)
        {
            journal.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == journal.groupID);
            var lesson = journalContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
            lesson.Addons = journalContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            lesson.Subject = journalContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Teacher = journalContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
            journal.Lessons = lesson;
            journal.Students = journalContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
        }
        return journals;
    }

    public IEnumerable<Journal>? GetDebted()
    {
        var journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.mark is null);
        foreach (var journal in journals)
        {
            journal.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == journal.groupID);
            var lesson = journalContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
            lesson.Addons = journalContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Group = journalContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            lesson.Subject = journalContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = journalContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Teacher = journalContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
            journal.Lessons = lesson;
            journal.Students = journalContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == journal.studentID);
        }
        return journals;
    }

    public Journal Add(Journal journal)
    {
        journalContext.Journals.Add(journal);
        journalContext.SaveChanges();

        return journal;
    }

    public void markUpdate(int studentId, int lessonId, string newMark)
    {
        var journalToUpdate = journalContext.Journals.Where(p => p.studentID == studentId && p.lessonID == lessonId).SingleOrDefault();

        if (journalToUpdate is null || newMark is null)
            throw new InvalidOperationException("There some problem.");

        journalToUpdate.mark = newMark;

        journalContext.SaveChanges();
    }

    public void firstRatingUpdate(int studentId, int lessonId, string newRating)
    {
        var journalToUpdate = journalContext.Journals.Where(p => p.studentID == studentId && p.lessonID == lessonId).SingleOrDefault();

        if (journalToUpdate is null || newRating is null)
            throw new InvalidOperationException("There some problem.");

        journalToUpdate.rating[0] = Int32.Parse(newRating);

        journalContext.SaveChanges();
    }

    public void secondRatingUpdate(int studentId, int lessonId, string newRating)
    {
        var journalToUpdate = journalContext.Journals.Where(p => p.studentID == studentId && p.lessonID == lessonId).SingleOrDefault();

        if (journalToUpdate is null || newRating is null)
            throw new InvalidOperationException("There some problem.");

        journalToUpdate.rating[1] = Int32.Parse(newRating);

        journalContext.SaveChanges();
    }

    public void thirdRatingUpdate(int studentId, int lessonId, string newRating)
    {
        var journalToUpdate = journalContext.Journals.Where(p => p.studentID == studentId && p.lessonID == lessonId).SingleOrDefault();

        if (journalToUpdate is null || newRating is null)
            throw new InvalidOperationException("There some problem.");

        journalToUpdate.rating[2] = Int32.Parse(newRating);

        journalContext.SaveChanges();
    }

    public void Delete(int studentId, int groupId, int lessonId)
    {
        var journalToDelete = journalContext.Journals.SingleOrDefault(p => p.studentID == studentId && p.groupID == groupId && p.lessonID == lessonId);
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
        var lessons = lessonContext.Lessons.AsNoTracking().ToList().OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
        foreach (var lesson in lessons)
        {
            lesson.Addons = lessonContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            var group = lessonContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            group.Students = lessonContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
            lesson.Group = group;
            lesson.Subject = lessonContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = lessonContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Teacher = lessonContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
        }
        return lessons;
    }

    public Lesson? GetById(int id)
    {
        var lesson = lessonContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == id);
        lesson.Addons = lessonContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
        var group = lessonContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
        group.Students = lessonContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
        lesson.Group = group;
        lesson.Subject = lessonContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
        lesson.Journals = lessonContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
        lesson.Teacher = lessonContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
        return lesson;
    }

    public IEnumerable<Lesson> GetByTeacherId(int id)
    {
        var lessons = lessonContext.Lessons.AsNoTracking().ToList().Where(p => p.teacherID == id).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
        foreach (var lesson in lessons)
        {
            lesson.Addons = lessonContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            var group = lessonContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            group.Students = lessonContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
            lesson.Group = group;
            lesson.Subject = lessonContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = lessonContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Teacher = lessonContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
        }
        return lessons;
    }

    public IEnumerable<Lesson> GetByGroupId(int id)
    {
        var lessons = lessonContext.Lessons.AsNoTracking().ToList().Where(p => p.groupID == id).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
        foreach (var lesson in lessons)
        {
            lesson.Addons = lessonContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            var group = lessonContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            group.Students = lessonContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
            lesson.Group = group;
            lesson.Subject = lessonContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = lessonContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Teacher = lessonContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
        }
        return lessons;
    }

    public IEnumerable<Lesson> GetBySubjectId(int id)
    {
        var lessons = lessonContext.Lessons.AsNoTracking().ToList().Where(p => p.subjectID == id).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
        foreach (var lesson in lessons)
        {
            lesson.Addons = lessonContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            var group = lessonContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            group.Students = lessonContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
            lesson.Group = group;
            lesson.Subject = lessonContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = lessonContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Teacher = lessonContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
        }
        return lessons;
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

    public void weekdayUpdate(int id, string newWeekday)
    {
        var lessonToUpdate = lessonContext.Lessons.Find(id);

        if (lessonToUpdate is null || newWeekday is null)
            throw new InvalidOperationException("There some problem.");

        lessonToUpdate.weekdays = Int32.Parse(newWeekday);

        lessonContext.SaveChanges();
    }

    public void dayOrderUpdate(int id, string newDayOrder)
    {
        var lessonToUpdate = lessonContext.Lessons.Find(id);

        if (lessonToUpdate is null || newDayOrder is null)
            throw new InvalidOperationException("There some problem.");

        lessonToUpdate.dayOrder = Int32.Parse(newDayOrder);

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
        var payments = paymentContext.Payments.AsNoTracking().ToList().OrderBy(p => p.studentID);
        foreach (var payment in payments)
            payment.Student = paymentContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == payment.studentID);
        return payments;
    }

    public Payment? GetById(int id)
    {
        var payment = paymentContext.Payments.AsNoTracking().SingleOrDefault(p => p.paymentID == id);
        payment.Student = paymentContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == payment.studentID);
        return payment;
    }

    public IEnumerable<Payment>? GetByStudentId(int id)
    {
        var payments = paymentContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == id);
        foreach (var payment in payments)
            payment.Student = paymentContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == payment.studentID);
        return payments;
    }

    public IEnumerable<Payment>? GetByDirection(bool direction, int studentId)
    {
        var payments = paymentContext.Payments.AsNoTracking().ToList().Where(p => p.paymentDirection == direction && p.studentID == studentId).OrderBy(p => p.studentID);
        foreach (var payment in payments)
            payment.Student = paymentContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == payment.studentID);
        return payments;
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

    public void isPaidUpdate(int id, bool state)
    {
        var paymentToUpdate = paymentContext.Payments.Find(id);
        if (paymentToUpdate is null)
            throw new InvalidOperationException("There some problem.");

        paymentToUpdate.isPaid = state;

        paymentContext.SaveChanges();
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
        var students = studentContext.Students.AsNoTracking().ToList().OrderBy(p => p.fullNameStudent);
        foreach (var student in students)
        {
            student.Group = studentContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == student.groupID);
            var journals = studentContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
            foreach (var journal in journals)
            {
                var lesson = studentContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
                lesson.Addons = studentContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                lesson.Subject = studentContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                lesson.Teacher = studentContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
                journal.Lessons = lesson;
            }
            student.Journal = journals;
            student.Payments = studentContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
        }
        return students;
    }

    public Student? GetById(int id)
    {
        var student = studentContext.Students.AsNoTracking().SingleOrDefault(p => p.studentID == id);
        student.Group = studentContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == student.groupID);
        var journals = studentContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
        foreach (var journal in journals)
        {
            var lesson = studentContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
            lesson.Addons = studentContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Subject = studentContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Teacher = studentContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
            journal.Lessons = lesson;
        }
        student.Journal = journals;
        student.Payments = studentContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
        return student;
    }

    public Student? GetByAuthToken(int authToken)
    {
        var student = studentContext.Students.AsNoTracking().SingleOrDefault(p => p.authToken == authToken);
        student.Group = studentContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == student.groupID);
        var journals = studentContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
        foreach (var journal in journals)
        {
            var lesson = studentContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
            lesson.Addons = studentContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            lesson.Subject = studentContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Teacher = studentContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
            journal.Lessons = lesson;
        }
        student.Journal = journals;
        student.Payments = studentContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
        return student;
    }

    public IEnumerable<Student>? GetByGroupId(int id)
    {
        var students = studentContext.Students.AsNoTracking().ToList().Where(p => p.groupID == id).OrderBy(p => p.fullNameStudent);
        foreach (var student in students)
        {
            student.Group = studentContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == student.groupID);
            var journals = studentContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
            foreach (var journal in journals)
            {
                var lesson = studentContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
                lesson.Addons = studentContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                lesson.Subject = studentContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                lesson.Teacher = studentContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
                journal.Lessons = lesson;
            }
            student.Journal = journals;
            student.Payments = studentContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
        }
        return students;
    }

    public IEnumerable<Student>? GetByBudget(bool budget)
    {
        var students = studentContext.Students.AsNoTracking().ToList().Where(p => p.budget == budget);
        foreach (var student in students)
        {
            student.Group = studentContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == student.groupID);
            var journals = studentContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
            foreach (var journal in journals)
            {
                var lesson = studentContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
                lesson.Addons = studentContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                lesson.Subject = studentContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                lesson.Teacher = studentContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
                journal.Lessons = lesson;
            }
            student.Journal = journals;
            student.Payments = studentContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
        }
        return students;
    }

    public IEnumerable<Student>? GetByHostelRent(bool rent)
    {
        var students = studentContext.Students.AsNoTracking().ToList().Where(p => p.hostelRent == rent);
        foreach (var student in students)
        {
            student.Group = studentContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == student.groupID);
            var journals = studentContext.Journals.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
            foreach (var journal in journals)
            {
                var lesson = studentContext.Lessons.OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder).AsNoTracking().SingleOrDefault(p => p.lessonID == journal.lessonID);
                lesson.Addons = studentContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                lesson.Subject = studentContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                lesson.Teacher = studentContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == lesson.teacherID);
                journal.Lessons = lesson;
            }
            student.Journal = journals;
            student.Payments = studentContext.Payments.AsNoTracking().ToList().Where(p => p.studentID == student.studentID);
        }
        return students;
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
            var authToDelete = studentContext.Authorizations.SingleOrDefault(p => p.authToken == studentToDelete.authToken);
            studentContext.Students.Remove(studentToDelete);
            studentContext.Authorizations.Remove(authToDelete);
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

    public void budgetUpdate(int id, string newBudget)
    {
        var studentToUpdate = studentContext.Students.Find(id);

        if (studentToUpdate is null || newBudget is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.budget = bool.Parse(newBudget);

        studentContext.SaveChanges();
    }

    public void hostelRentUpdate(int id, string newState)
    {
        var studentToUpdate = studentContext.Students.Find(id);

        if (studentToUpdate is null || newState is null)
            throw new InvalidOperationException("There some problem.");

        studentToUpdate.hostelRent = bool.Parse(newState);

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
        var teachers = teacherContext.Teachers.AsNoTracking().ToList().OrderBy(p => p.fullNameTeacher);
        foreach (var teacher in teachers)
        {
            var lessons = teacherContext.Lessons.AsNoTracking().ToList().Where(p => p.teacherID == teacher.teacherID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
            foreach (var lesson in lessons)
            {
                lesson.Addons = teacherContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                var group = teacherContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
                group.Students = teacherContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
                lesson.Group = group;
                lesson.Subject = teacherContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                lesson.Journals = teacherContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            }
            teacher.Lessons = lessons;
            teacher.JobTitles = teacherContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == teacher.jobID);
        }
        return teachers;
    }

    public Teacher? GetById(int id)
    {
        var teacher = teacherContext.Teachers.AsNoTracking().SingleOrDefault(p => p.teacherID == id);
        var lessons = teacherContext.Lessons.AsNoTracking().ToList().Where(p => p.teacherID == teacher.teacherID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
        foreach (var lesson in lessons)
        {
            lesson.Addons = teacherContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            var group = teacherContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            group.Students = teacherContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
            lesson.Group = group;
            lesson.Subject = teacherContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = teacherContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
        }
        teacher.Lessons = lessons;
        teacher.JobTitles = teacherContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == teacher.jobID);
        return teacher;
    }

    public Teacher? GetByAuthToken(int authToken)
    {
        var teacher = teacherContext.Teachers.AsNoTracking().SingleOrDefault(p => p.authToken == authToken);
        var lessons = teacherContext.Lessons.AsNoTracking().ToList().Where(p => p.teacherID == teacher.teacherID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
        foreach (var lesson in lessons)
        {
            lesson.Addons = teacherContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            var group = teacherContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
            group.Students = teacherContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
            lesson.Group = group;
            lesson.Subject = teacherContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
            lesson.Journals = teacherContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
        }
        teacher.Lessons = lessons;
        teacher.JobTitles = teacherContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == teacher.jobID);
        return teacher;
    }

    public IEnumerable<Teacher>? GetByJobId(int id)
    {
        var teachers = teacherContext.Teachers.AsNoTracking().ToList().Where(p => p.jobID == id).OrderBy(p => p.fullNameTeacher);
        foreach (var teacher in teachers)
        {
            var lessons = teacherContext.Lessons.AsNoTracking().ToList().Where(p => p.teacherID == teacher.teacherID).OrderBy(p => p.weekdays).ThenBy(p => p.dayOrder);
            foreach (var lesson in lessons)
            {
                lesson.Addons = teacherContext.Addons.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
                var group = teacherContext.Groups.AsNoTracking().SingleOrDefault(p => p.groupID == lesson.groupID);
                group.Students = teacherContext.Students.AsNoTracking().ToList().Where(p => p.groupID == group.groupID).OrderBy(p => p.fullNameStudent);
                lesson.Group = group;
                lesson.Subject = teacherContext.Subjects.AsNoTracking().SingleOrDefault(p => p.subjectID == lesson.subjectID);
                lesson.Journals = teacherContext.Journals.AsNoTracking().ToList().Where(p => p.lessonID == lesson.lessonID);
            }
            teacher.Lessons = lessons;
            teacher.JobTitles = teacherContext.JobTitles.AsNoTracking().SingleOrDefault(p => p.jobID == teacher.jobID);
        }
        return teachers;
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
        var teacherToDelete = teacherContext.Teachers.Find(id);
        if (teacherToDelete is not null)
        {
            var authToDelete = teacherContext.Authorizations.SingleOrDefault(p => p.authToken == teacherToDelete.authToken);
            teacherContext.Teachers.Remove(teacherToDelete);
            teacherContext.Authorizations.Remove(authToDelete);
            teacherContext.SaveChanges();
        }
    }
}