using Microsoft.AspNetCore.Identity;
using MoboFix.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoboFix.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
