using JAMBAPI.Data;
using JAMBAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JAMBAPI.SeedData
{
    public static class SeedSuperAdmin
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<JambDbContext>();

                var existingSuperAdmin = dbContext.Admins.SingleOrDefaultAsync(a => a.Role == "SuperAdmin").Result;

                if (existingSuperAdmin == null)
                {
                    var superAdmin = new Admin
                    {
                        Email = "superadmin@jamb.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("SuperAdminPassword"),
                        Role = "SuperAdmin",
                        CanManageAdmins = true
                    };

                    dbContext.Admins.Add(superAdmin);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
