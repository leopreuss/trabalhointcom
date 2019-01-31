using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoBlueOpex.Models
{
    public class Employee
    {
        [Key]
        public long Id{ get; set; }

        [ForeignKey("CompanyId")]
        public long CompanyId { get; set; }
        public Company Company { get; set; }

        [Required]
        [Display(Prompt = "Nome")]
        public String Name { get; set; }

        [Required]
        [Display(Prompt = "Identificador")]
        public long Identifier { get; set; }

        [Required]
        [Display(Prompt = "Cargo")]
        public String Charge { get; set; }

        [Required]
        [Display(Prompt = "Data de nascimento")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Prompt = "Data de admissão")]
        public DateTime DateOfAdmission { get; set; }

        [Display(Prompt = "Tipo de Perfil")]
        public String ProfileType { get; set; }

        [Display(Prompt = "CPF")]
        public String Cpf { get; set; }

        [Display(Prompt = "Passaporte")]
        public String Passport { get; set; }

        public Employee() { }

        public Employee(Company company)
        {
            Company = company;
        }
    }
}
