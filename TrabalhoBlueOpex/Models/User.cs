using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoBlueOpex.Models
{
    public class User : IdentityUser
    {
        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
