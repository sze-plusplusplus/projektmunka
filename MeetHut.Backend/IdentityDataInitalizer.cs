using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MeetHut.Backend
{
    /// <summary>
    /// Init Identity data
    /// </summary>
    public static class IdentityDataInitalizer
    {
        private static readonly List<UserRole> ROLES = new List<UserRole> { UserRole.Admin, UserRole.Moderator, UserRole.Lecturer, UserRole.Student };
        /// <summary>
        /// Seed default roles
        /// </summary>
        /// <param name="roleManager">Role manager</param>
        public static void SeedRoles(RoleManager<Role> roleManager)
        {
            ROLES.ForEach(roleName =>
            {
                if (!roleManager.RoleExistsAsync(roleName.ToString()).Result)
                {
                    var role = new Role
                    {
                        Name = roleName.ToString()
                    };
                    var result = roleManager.CreateAsync(role).Result;
                }
            });
        }
    }
}
