using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.DTOS.AccountDTO
{
    public class LoginDto
    {
        //[EmailAddress(ErrorMessage ="Email should be with the right format")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
