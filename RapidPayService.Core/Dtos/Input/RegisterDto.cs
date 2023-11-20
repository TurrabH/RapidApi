using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPayService.Core.Dtos.Input
{
    public class RegisterDto
    {
        [Required]
        public virtual string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }
        public virtual string? PhoneNumber { get; set; }
        [Required]
        [PasswordPropertyText]
        public virtual string Password { get; set; }
    }
}
