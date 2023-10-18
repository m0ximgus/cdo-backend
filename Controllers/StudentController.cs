using Kursach.Models;
using Kursach.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kursach.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    StudentService _service;
    public StudentController(StudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Student> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetById(int id)
    {
        var student = _service.GetById(id);

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
        var student = _service.Add(newStudent);
        return CreatedAtAction(nameof(GetById), new { id = student!.Id }, student);
    }

    [HttpPut("{id}/{parametr}/{value}")]
    public IActionResult Update(int id, int parametr, string value)
    {
        var updatingStudent = _service.GetById(id);
        if (updatingStudent is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                _service.nameUpdate(id, value);
                break;
            case 2:
                _service.groupUpdate(id, value);
                break;
            case 3:
                _service.ageUpdate(id, value);
                break;
            case 4:
                _service.admissionTimeUpdate(id, value);
                break;
            case 5:
                _service.contactMailUpdate(id, value);
                break;
            case 6:
                _service.contactPhoneUpdate(id, value);
                break;
            default:
                break;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = _service.GetById(id);

        if (student is not null)
        {
            _service.Delete(id);
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
public class AuthController : ControllerBase
{
    AuthService _service;
    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Auth> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Auth> GetById(int id)
    {
        var auth = _service.GetById(id);

        if (auth is not null)
        {
            return auth;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{username}/{password}")]
    public ActionResult<Auth> GetByLogin(string username, string password)
    {
        var auth = _service.GetByLogin(username, password);

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
    public IActionResult Create(Auth newAuth)
    {
        var auth = _service.Add(newAuth);
        return CreatedAtAction(nameof(GetById), new { id = auth!.AuthId }, auth);
    }

    [HttpPut("{id}/{parametr}/{value}")]
    public IActionResult Update(int id, int parametr, string value)
    {
        var updatingAuth = _service.GetById(id);
        if (updatingAuth is null)
            return BadRequest();
        switch (parametr)
        {
            case 1:
                _service.usernameUpdate(id, value);
                break;
            case 2:
                _service.passwordUpdate(id, value);
                break;
            case 3:
                _service.typeUpdate(id, value);
                break;
            default:
                break;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var auth = _service.GetById(id);

        if (auth is not null)
        {
            _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}