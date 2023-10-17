using System.ComponentModel.DataAnnotations;

namespace Kursach.Models;

public class Student
{
    [Required]
    [MaxLength(100)]
    public string name { get; set; }
    [Required]
    [MaxLength(10)]
    public string group { get; set; }
    public int Id { get; set; }
    public int age { get; set; }
    [Required]
    public DateTime admissionTime { get; set; }
    public string contactMail { get; set; }
    public string contactPhone { get; set; }
}

public class Auth
{
    [Required]
    [MaxLength(32)]
    public string username { get; set; }
    [Required]
    [MaxLength(32)]
    public string password { get; set; }
    [Required]
    public int AuthId { get; set; }
}