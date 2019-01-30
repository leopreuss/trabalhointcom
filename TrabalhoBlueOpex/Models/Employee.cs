using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoBlueOpex.Models
{
    public class Employee : IdentityUser
    {
        public long CompanyRefIf { get; set; }

        [ForeignKey("CompanyRefId")]
        public Company Company { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public long Identifier { get; set; }

        [Required]
        public String Charge { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public String ProfileType { get; set; }
        public String Cpf { get; set; }
        public String Passport { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password {get; set;}

        public Employee() { }

        public Employee(Company company)
        {
            Company = company;
        }
    }
}
