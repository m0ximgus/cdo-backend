using System.Data.Common;
using Kursach.Services;
using Microsoft.EntityFrameworkCore;

namespace Kursach.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var kursachContext = services.GetRequiredService<KursachContext>();
            kursachContext.Database.EnsureCreated();

            DbInitializer.Initialize(kursachContext);
        }
    }
}