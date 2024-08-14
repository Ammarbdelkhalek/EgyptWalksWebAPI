using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.DTOS.AccountDTO
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100,ErrorMessage ="First Name should be at least 100 character")]
        public  string FirstName { get; set; }
        [MaxLength(100, ErrorMessage = "Last Name should be at least 100 character")]

        public string LastName { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage ="Email should be with the right format like example@example.com")]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(150, ErrorMessage = "Last Name should be at least 150 character")]
        public string UserName {  get; set; }
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
