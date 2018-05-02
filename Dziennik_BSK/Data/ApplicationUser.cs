using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Dziennik_BSK.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
            Role = Roles.STUDENT;
        }

        public Roles Role { get; set; }
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
        public int? ParentId { get; set; }
    }

    public enum Roles {
        ADMIN,
        STUDENT,
        TEACHER,
        PARENT
    }

}
