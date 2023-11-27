﻿using Kursach.Models;

namespace Kursach.Data
{
    public static class DbInitializer
    {
        public static void Initialize(KursachContext kursachContext)
        {
            if (kursachContext.Authorizations.Any() && kursachContext.Employees.Any() && kursachContext.Groups.Any() && kursachContext.JobTitles.Any() && kursachContext.Lessons.Any() && kursachContext.Payments.Any() && kursachContext.Students.Any() && kursachContext.Subjects.Any() && kursachContext.Teachers.Any() && kursachContext.Journals.Any())
                return;

            kursachContext.SaveChanges();
        }
    }
}