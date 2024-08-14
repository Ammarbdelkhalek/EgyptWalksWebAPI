using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }

        public List<Review> Reviews { set; get; }
    }
}
