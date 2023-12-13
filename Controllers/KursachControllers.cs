using Kursach.Models;
using Kursach.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Kursach.Controllers;

[ApiController]
[Route("[controller]")]
public class AddonController : ControllerBase
{
    AddonService addonService;
    public AddonController(AddonService service)
    {
        addonService = service;
    }

    [HttpGet]
    public IEnumerable<Addon> GetAll()
    {
        return addonService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Addon> GetById(int id) 
    {

        var addon = addonService.GetById(id);

        if (addon is not null)
        {
            return addon;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{lessonID}/lessonId")]
    public IEnumerable<Addon> GetByLessonId(int lessonID)
    {
        return addonService.GetByLessonId(lessonID);
    }

    [HttpPost]
    public IActionResult Create(Addon newAddon)
    {
        var addon = addonService.Add(newAddon);
        return CreatedAtAction(nameof(GetById), new { id = addon!.addonID }, addon);
    }

    [HttpPut("{id}/{parametr}/{value}")]
    public IActionResult Update(int id, int parametr, string value)
    {
        var updatingAddon = addonService.GetById(id);
        if (updatingAddon is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                addonService.headerUpdate(id, value);
                break;
            case 2:
                addonService.descriptionUpdate(id, value);
                break;
            default:
                break;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var addon = addonService.GetById(id);

        if (addon is not null)
        {
            addonService.Delete(id);
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

    [HttpGet("{type}/type")]
    public IEnumerable<Authorization> GetByType(string type)
    {
        return authService.GetByType(type);
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

    [HttpGet("{authToken}/authToken")]
    public ActionResult<Employee> GetByAuthToken(int authToken)
    {
        var employee = employeeService.GetByAuthToken(authToken);

        if (employee is not null)
        {
            return employee;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{jobId}/jobID")]
    public IEnumerable<Employee> GetByJobId(int jobId)
    {
        return employeeService.GetByJobId(jobId);
    }

    [HttpPost]
    public IActionResult Create(Employee newEmployee)
    {
        var employee = employeeService.Add(newEmployee);
        return CreatedAtAction(nameof(GetById), new { id = employee!.employeeID }, employee);
    }

    [HttpPut("{id}/{parametr}/{value}")]
    public IActionResult Update(int id, int parametr, string value)
    {
        var updatingEmployee = employeeService.GetById(id);
        if (updatingEmployee is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                employeeService.nameUpdate(id, value);
                break;
            case 2:
                employeeService.jobUpdate(id, value);
                break;
            case 3:
                employeeService.mailUpdate(id, value);
                break;
            case 4:
                employeeService.phoneUpdate(id, value);
                break;
            default:
                break;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var employee = employeeService.GetById(id);

        if (employee is not null)
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
public class EventController : ControllerBase
{
    EventService eventService;
    public EventController(EventService service)
    {
        eventService = service;
    }

    [HttpGet]
    public IEnumerable<Event> GetAll()
    {
        return eventService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Event> GetById(int id)
    {
        var _event = eventService.GetById(id);

        if (_event is not null)
        {
            return _event;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Event newEvent)
    {
        var _event = eventService.Add(newEvent);
        return CreatedAtAction(nameof(GetById), new { id = _event!.eventID }, _event);
    }

    [HttpPut("{id}/{parametr}/{value}")]
    public IActionResult Update(int id, int parametr, string value)
    {
        var updatingEvent = eventService.GetById(id);
        if (updatingEvent is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                eventService.headerUpdate(id, value);
                break;
            case 2:
                eventService.discriptionUpdate(id, value);
                break;
            case 3:
                eventService.dateUpdate(id, value);
                break;
            default:
                break;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var _event = eventService.GetById(id);

        if (_event is not null)
        {
            eventService.Delete(id);
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

    [HttpPut("{id}/{value}")]
    public IActionResult Update(int id, string value)
    {
        var updatingGroup = groupService.GetById(id);
        if (updatingGroup is null)
            return BadRequest();
        groupService.nameUpdate(id, value);
        return NoContent();
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

    [HttpPut("{id}/{parametr}/{value}")]
    public IActionResult Update(int id, int parametr, string value)
    {
        var updatingJobTitle = jobTitleService.GetById(id);
        if (updatingJobTitle is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                jobTitleService.nameUpdate(id, value);
                break;
            case 2:
                jobTitleService.salaryUpdate(id, value);
                break;
            default:
                break;
        }
        return NoContent();
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

    [HttpGet("{groupId}/groupId")]
    public IEnumerable<Journal> GetByGroupId(int groupId)
    {
        return journalService.GetById(groupId);
    }

    [HttpGet("{studentId}/studentId")]
    public IEnumerable<Journal> GetByStudentId(int studentId)
    {
        return journalService.GetByStudentId(studentId);
    }

    [HttpGet("{lessonId}/lessonId")]
    public IEnumerable<Journal> GetByLessonId(int lessonId)
    {
        return journalService.GetByLessonId(lessonId);
    }

    [HttpGet("/debted")]
    public IEnumerable<Journal> GetDebted()
    {
        return journalService.GetDebted();
    }

    [HttpPost]
    public IActionResult Create(Journal newJournal)
    {
        var journal = journalService.Add(newJournal);
        return CreatedAtAction(nameof(GetByGroupId), new { id = journal!.groupID }, journal);
    }

    [HttpPut("{studentId}/{parametr}/{value}")]
    public IActionResult Update(int studentId, int parametr, string value)
    {
        var updatingJournal = journalService.GetById(studentId);
        if (updatingJournal is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                journalService.markUpdate(studentId, value);
                break;
            case 2:
                journalService.firstRatingUpdate(studentId, value);
                break;
            case 3:
                journalService.secondRatingUpdate(studentId, value);
                break;
            case 4:
                journalService.thirdRatingUpdate(studentId, value);
                break;
            default:
                break;
        }
        return NoContent();
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

    [HttpGet("{teacherId}/teacherId")]
    public IEnumerable<Lesson> GetByTeacherId(int teacherId)
    {
        return lessonService.GetByTeacherId(teacherId);
    }

    [HttpGet("{groupId}/groupId")]
    public IEnumerable<Lesson> GetByGroupId(int groupId)
    {
        return lessonService.GetByGroupId(groupId);
    }

    [HttpGet("{subjectId}/subjectId")]
    public IEnumerable<Lesson> GetBySubjectId(int subjectId)
    {
        return lessonService.GetByTeacherId(subjectId);
    }

    [HttpPost]
    public IActionResult Create(Lesson newLesson)
    {
        var lesson = lessonService.Add(newLesson);
        return CreatedAtAction(nameof(GetById), new { id = lesson!.groupID }, lesson);
    }

    [HttpPut("{id}/{value}")]
    public IActionResult Update(int id, string value)
    {
        var updatingLesson = lessonService.GetById(id);
        if (updatingLesson is null)
            return BadRequest();
        lessonService.classUpdate(id, value);
        return NoContent();
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

    [HttpGet("{studentId}/studentId")]
    public IEnumerable<Payment> GetByStudentId(int studentId)
    {
        return paymentService.GetByStudentId(studentId);
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

    [HttpGet("{authToken}/authToken")]
    public ActionResult<Student> GetByAuthToken(int authToken)
    {
        var student = studentService.GetByAuthToken(authToken);

        if (student is not null)
        {
            return student;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{groupId}/groupID")]
    public IEnumerable<Student> GetByGroupId(int groupId)
    {
        return studentService.GetByGroupId(groupId);
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
            case 7:
                studentService.genderUpdate(id, value);
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

    [HttpPut("{id}/{value}")]
    public IActionResult Update(int id, string value)
    {
        var updatingSubject = subjectService.GetById(id);
        if (updatingSubject is null)
            return BadRequest();
        subjectService.nameUpdate(id, value);
        return NoContent();
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

    [HttpGet("{authToken}/authToken")]
    public ActionResult<Teacher> GetByAuthToken(int authToken)
    {
        var teacher = teacherService.GetByAuthToken(authToken);

        if (teacher is not null)
        {
            return teacher;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{jobId}/jobID")]
    public IEnumerable<Teacher> GetByJobId(int jobId)
    {
        return teacherService.GetByJobId(jobId);
    }

    [HttpPost]
    public IActionResult Create(Teacher newTeacher)
    {
        var teacher = teacherService.Add(newTeacher);
        return CreatedAtAction(nameof(GetById), new { id = teacher!.teacherID }, teacher);
    }

    [HttpPut("{id}/{parametr}/{value}")]
    public IActionResult Update(int id, int parametr, string value)
    {
        var updatingTeacher = teacherService.GetById(id);
        if (updatingTeacher is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                teacherService.nameUpdate(id, value);
                break;
            case 2:
                teacherService.jobUpdate(id, value);
                break;
            case 3:
                teacherService.mailUpdate(id, value);
                break;
            case 4:
                teacherService.phoneUpdate(id, value);
                break;
            default:
                break;
        }
        return NoContent();
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