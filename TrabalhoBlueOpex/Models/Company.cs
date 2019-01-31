using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoBlueOpex.Models
{
    public class Company
    {
        [Key]
        public long CompanyId { get; set; }
        public IList<Employee> Employee { get; set; }

        public String Name { get; set; }
        public Company() {}
        public Company(String name) { Name = name; }

    }
}
