using Kursach.Models;
using Kursach.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kursach.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    AuthorizationService authService;
    public AuthorizationController(AuthorizationService service)
    {
        authService = service;
    }

    [HttpGet]
    public IEnumerable<Authorization> GetAll()
    {
        return authService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Authorization> GetById(int id)
    {
        var authorization = authService.GetById(id);

        if (authorization is not null)
        {
            return authorization;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{login}/{password}")]
    public ActionResult<Authorization> GetByLogin(string login, string password)
    {
        var auth = authService.GetByLogin(login, password);

        if (auth is not null)
        {
            return auth;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Authorization newAuthorization)
    {
        var auth = authService.Add(newAuthorization);
        return CreatedAtAction(nameof(GetById), new { id = auth!.authToken }, auth);
    }

    [HttpPut("{id}/{parametr}/{value}")]
    public IActionResult Update(int id, int parametr, string value)
    {
        var updatingAuthorization = authService.GetById(id);
        if (updatingAuthorization is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                authService.usernameUpdate(id, value);
                break;
            case 2:
                authService.passwordUpdate(id, value);
                break;
            case 3:
                authService.typeUpdate(id, value);
                break;
            default:
                break;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var auth = authService.GetById(id);

        if (auth is not null)
        {
            authService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    EmployeeService employeeService;
    public EmployeeController(EmployeeService service)
    {
        employeeService = service;
    }

    [HttpGet]
    public IEnumerable<Employee> GetAll()
    {
        return employeeService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Employee> GetById(int id)
    {
        var employee = employeeService.GetById(id);

        if (employee is not null)
        {
            return employee;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Employee newEmployee)
    {
        var employee = employeeService.Add(newEmployee);
        return CreatedAtAction(nameof(GetById), new { id = employee!.employeeID }, employee);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = employeeService.GetById(id);

        if (student is not null)
        {
            employeeService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class GroupController : ControllerBase
{
    GroupService groupService;
    public GroupController(GroupService service)
    {
        groupService = service;
    }

    [HttpGet]
    public IEnumerable<Group> GetAll()
    {
        return groupService.GetAll();
    }
    [HttpGet("{id}")]
    public ActionResult<Group> GetById(int id)
    {
        var group = groupService.GetById(id);

        if (group is not null)
        {
            return group;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Group newGroup)
    {
        var group = groupService.Add(newGroup);
        return CreatedAtAction(nameof(GetById), new { id = group!.groupID }, group);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = groupService.GetById(id);

        if (student is not null)
        {
            groupService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class JobTitleController : ControllerBase
{
    JobTitleService jobTitleService;
    public JobTitleController(JobTitleService service)
    {
        jobTitleService = service;
    }

    [HttpGet]
    public IEnumerable<JobTitle> GetAll()
    {
        return jobTitleService.GetAll();
    }
    [HttpGet("{id}")]
    public ActionResult<JobTitle> GetById(int id)
    {
        var jobTitle = jobTitleService.GetById(id);

        if (jobTitle is not null)
        {
            return jobTitle;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(JobTitle newJobTitle)
    {
        var jobTitle = jobTitleService.Add(newJobTitle);
        return CreatedAtAction(nameof(GetById), new { id = jobTitle!.jobID }, jobTitle);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = jobTitleService.GetById(id);

        if (student is not null)
        {
            jobTitleService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class JournalController : ControllerBase
{
    JournalService journalService;
    public JournalController(JournalService service)
    {
        journalService = service;
    }

    [HttpGet]
    public IEnumerable<Journal> GetAll()
    {
        return journalService.GetAll();
    }
    [HttpGet("{id}")]
    public ActionResult<Journal> GetById(int id)
    {
        var journal = journalService.GetById(id);

        if (journal is not null)
        {
            return journal;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Journal newJournal)
    {
        var journal = journalService.Add(newJournal);
        return CreatedAtAction(nameof(GetById), new { id = journal!.groupID }, journal);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var journal = journalService.GetById(id);

        if (journal is not null)
        {
            journalService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class LessonController : ControllerBase
{
    LessonService lessonService;
    public LessonController(LessonService service)
    {
        lessonService = service;
    }

    [HttpGet]
    public IEnumerable<Lesson> GetAll()
    {
        return lessonService.GetAll();
    }
    [HttpGet("{id}")]
    public ActionResult<Lesson> GetById(int id)
    {
        var lesson = lessonService.GetById(id);

        if (lesson is not null)
        {
            return lesson;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Lesson newLesson)
    {
        var lesson = lessonService.Add(newLesson);
        return CreatedAtAction(nameof(GetById), new { id = lesson!.groupID }, lesson);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = lessonService.GetById(id);

        if (student is not null)
        {
            lessonService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    PaymentService paymentService;
    public PaymentController(PaymentService service)
    {
        paymentService = service;
    }

    [HttpGet]
    public IEnumerable<Payment> GetAll()
    {
        return paymentService.GetAll();
    }
    [HttpGet("{id}")]
    public ActionResult<Payment> GetById(int id)
    {
        var payment = paymentService.GetById(id);

        if (payment is not null)
        {
            return payment;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Payment newPayment)
    {
        var payment = paymentService.Add(newPayment);
        return CreatedAtAction(nameof(GetById), new { id = payment!.studentID }, payment);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = paymentService.GetById(id);

        if (student is not null)
        {
            paymentService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    StudentService studentService;
    public StudentController(StudentService service)
    {
        studentService = service;
    }

    [HttpGet]
    public IEnumerable<Student> GetAll()
    {
        return studentService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetById(int id)
    {
        var student = studentService.GetById(id);

        if (student is not null)
        {
            return student;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Student newStudent)
    {
        var student = studentService.Add(newStudent);
        return CreatedAtAction(nameof(GetById), new { id = student!.studentID }, student);
    }

    [HttpPut("{id}/{parametr}/{value}")]
    public IActionResult Update(int id, int parametr, string value)
    {
        var updatingStudent = studentService.GetById(id);
        if (updatingStudent is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                studentService.nameUpdate(id, value);
                break;
            case 2:
                studentService.groupUpdate(id, value);
                break;
            case 3:
                studentService.ageUpdate(id, value);
                break;
            case 4:
                studentService.enrollmentTimeUpdate(id, value);
                break;
            case 5:
                studentService.contactMailUpdate(id, value);
                break;
            case 6:
                studentService.contactPhoneUpdate(id, value);
                break;
            default:
                break;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = studentService.GetById(id);

        if (student is not null)
        {
            studentService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class SubjectController : ControllerBase
{
    SubjectService subjectService;
    public SubjectController(SubjectService service)
    {
        subjectService = service;
    }

    [HttpGet]
    public IEnumerable<Subject> GetAll()
    {
        return subjectService.GetAll();
    }
    [HttpGet("{id}")]
    public ActionResult<Subject> GetById(int id)
    {
        var subject = subjectService.GetById(id);

        if (subject is not null)
        {
            return subject;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Subject newSubject)
    {
        var subject = subjectService.Add(newSubject);
        return CreatedAtAction(nameof(GetById), new { id = subject!.subjectID }, subject);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = subjectService.GetById(id);

        if (student is not null)
        {
            subjectService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class TeacherController : ControllerBase
{
    TeacherService teacherService;
    public TeacherController(TeacherService service)
    {
        teacherService = service;
    }

    [HttpGet]
    public IEnumerable<Teacher> GetAll()
    {
        return teacherService.GetAll();
    }
    [HttpGet("{id}")]
    public ActionResult<Teacher> GetById(int id)
    {
        var teacher = teacherService.GetById(id);

        if (teacher is not null)
        {
            return teacher;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Teacher newTeacher)
    {
        var teacher = teacherService.Add(newTeacher);
        return CreatedAtAction(nameof(GetById), new { id = teacher!.teacherID }, teacher);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = teacherService.GetById(id);

        if (student is not null)
        {
            teacherService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}