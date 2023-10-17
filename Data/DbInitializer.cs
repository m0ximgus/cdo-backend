using Kursach.Models;

namespace Kursach.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StudentContext studentContext, AuthContext authContext)
        {
            if (studentContext.Students.Any() && authContext.Auths.Any())
                return;

            studentContext.SaveChanges();
            authContext.SaveChanges();
        }
    }
}