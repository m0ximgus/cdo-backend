using Kursach.Models;

namespace Kursach.Data
{
    public static class DbInitializer
    {
        public static void Initialize(KursachContext kursachContext)
        {
            if (kursachContext.Addons.Any() &&kursachContext.Authorizations.Any() && kursachContext.Employees.Any() && kursachContext.Events.Any() && kursachContext.Groups.Any() && kursachContext.JobTitles.Any() && kursachContext.Lessons.Any() && kursachContext.Payments.Any() && kursachContext.Students.Any() && kursachContext.StudyLoads.Any() && kursachContext.Subjects.Any() && kursachContext.Teachers.Any() && kursachContext.Journals.Any())
                return;

            kursachContext.SaveChanges();
        }
    }
}