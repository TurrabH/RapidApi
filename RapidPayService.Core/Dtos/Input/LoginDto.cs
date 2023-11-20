using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPayService.Core.Dtos.Input
{
    public class LoginDto
    {
        [Required]
        public virtual string Email { get; set; }
        [Required]
        public virtual string Password { get; set; }
    }
}
