using System.Data.Common;
using Kursach.Services;

namespace Kursach.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var studentContext = services.GetRequiredService<StudentContext>();
            var authContext = services.GetRequiredService<AuthContext>();
            studentContext.Database.EnsureCreated();
            authContext.Database.EnsureCreated();
            DbInitializer.Initialize(studentContext, authContext);
        }
    }
}