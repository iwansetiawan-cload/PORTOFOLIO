using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTOFOLIO.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }     
        public string? Gender { get; set; }
        public string? Company { get; set; }
        public string? Address { get; set; }       
        public string? RolesName { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
