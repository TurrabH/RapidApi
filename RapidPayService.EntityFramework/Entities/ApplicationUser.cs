using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPayService.EntityFramework.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual CardHolder CardHolder { get; set; }
    }
}
