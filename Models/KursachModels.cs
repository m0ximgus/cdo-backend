using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Kursach.Models;

[Table("Authorizations")]
public class Authorization
{
    [Required]
    [MaxLength(32)]
    public string login { get; set; }
    [Required]
    [MaxLength(32)]
    public string password { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int authToken { get; set; }
    [Required]
    [MaxLength(11)]
    public string type { get; set; }


    //Nav
    public Student? Student { get; }
    public Teacher? Teacher { get; }
    public Employee? Employee { get; }
}

[Table("Employees")]
public class Employee
{
    [Required]
    public string fullNameEmployee { get; set; }
    public string contactMailEmployee { get; set; }
    public string contactPhoneEmployee { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int employeeID { get; set; }

    //FK
    public int? authToken { get; set; }
    [ForeignKey("authToken")]
    public Authorization? Authorizations { get; set; } = null;
    public int? jobID { get; set; }
    [ForeignKey("jobID")]
    public JobTitle? JobTitles { get; set; } = null;
}

[Table("Groups")]
public class Group
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int groupID { get; set; }
    [Required]
    public string groupName { get; set; }

    //Nav
    public ICollection<Student> Students { get; } = new List<Student>();
    public List<Lesson> Lessons { get; } = new();
    public List<Subject> Subjects { get; } = new();
    public List<Teacher> Teachers { get; } = new();
    public Journal? Journal { get; }
}

[Table("JobTitles")]
public class JobTitle
{
    [Required]
    public string jobName { get; set; }
    [Required]
    public double salary { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int jobID { get; set; }

    //Nav
    public Teacher? Teacher { get; }
    public Employee? Employee { get; }
}

[PrimaryKey(nameof(groupID), nameof(studentID))]
[Table("Journal")]
public class Journal
{
    public int mark { get; set; }

    //FK
    public int? groupID { get; set; }
    [ForeignKey("groupID")]
    public Group? Group { get; set; } = null!;

    public int? studentID { get; set; }
    [ForeignKey("studentID")]
    public Student? Students { get; set; } = null;
}

[PrimaryKey(nameof(groupID), nameof(teacherID), nameof(subjectID))]
[Table("Lessons")]
public class Lesson
{
    [Required]
    public string classroom { get; set; }

    //FK
    public int? groupID { get; set; }
    [ForeignKey("groupID")]
    public Group? Group { get; set; } = null;

    public int? teacherID { get; set; }
    [ForeignKey("teacherID")]
    public Teacher? Teacher { get; set; } = null;

    public int? subjectID { get; set; }
    [ForeignKey("subjectID")]
    public Subject? Subject { get; set; } = null;

}

[PrimaryKey(nameof(studentID))]
[Table("Payments")]
public class Payment
{
    [Required]
    public string paymentType { get; set; }
    [Required]
    public double paymentCost { get; set; }
    [Required]
    public DateTime paymentDate { get; set; }

    //FK
    public int? studentID { get; set; }
    [ForeignKey("studentID")]
    public Student? Student { get; set; } = null;
}

[Table("Students")]
public class Student
{
    [Required]
    [MaxLength(100)]
    public string fullNameStudent { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int studentID { get; set; }
    public DateTime age { get; set; }
    [Required]
    public DateTime enrollmentDate { get; set; }
    public string contactMailStudent { get; set; }
    public string contactPhoneStudent { get; set; }
    public bool gender { get; set; }

    //FK
    public int? groupID { get; set; }
    [ForeignKey("groupID")]
    public Group? Group { get; set;} = null;

    public int? authToken { get; set; }
    [ForeignKey("authToken")]
    public Authorization? Authorizations { get; set; } = null;

    //Nav
    public ICollection<Payment> Payments { get; } = new List<Payment>();
    public ICollection<Journal> Journal { get; } = new List<Journal>();
}

[Table("Subjects")]
public class Subject
{
    [Required]
    public string subjectName { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int subjectID { get; set; }

    //Nav
    public List<Lesson> Lessons { get; } = new();
    public List<Group> Groups { get; } = new();
    public List<Teacher> Teachers { get; } = new();
}

[Table("Teachers")]
public class Teacher
{
    [Required]
    public string fullNameTeacher { get; set; }
    public string contactMailTeacher { get; set; }
    public string contactPhoneTeacher { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int teacherID { get; set; }

    //FK
    public int? authToken { get; set; }
    [ForeignKey("authToken")]
    public Authorization? Authorizations { get; set; } = null;

    public int? jobID { get; set; }
    [ForeignKey("jobID")]
    public JobTitle? JobTitles { get; set; } = null;

    //Nav
    public List<Lesson> Lessons { get; } = new();
    public List<Group> Groups { get; } = new();
    public List<Subject> Subjects { get; } = new();
}