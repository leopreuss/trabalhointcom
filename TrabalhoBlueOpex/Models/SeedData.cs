using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoBlueOpex.Db;

namespace TrabalhoBlueOpex.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                Company companyIntcom;
                Company companyGoogle;
                Company companyMicrosoft;

                if (!context.Company.Any())
                {
                    companyIntcom = new Company("Intcom");
                    companyGoogle = new Company("Google");
                    companyMicrosoft = new Company("Microsoft");
                    context.Company.AddRange(companyGoogle, companyIntcom, companyMicrosoft);
                }
                else
                {
                    companyIntcom = context.Company.Where(c => c.Name == "Intcom").Single();
                    companyGoogle = context.Company.Where(c => c.Name == "Google").Single();
                    companyMicrosoft = context.Company.Where(c => c.Name == "Microsoft").Single();
                }

                Employee employee1;

                if (!context.Employee.Any())
                {
                    employee1 = new Employee(companyIntcom)
                    {
                        Name = "Leo",
                        Cpf = "115.943.057-81",
                        DateOfAdmission = new DateTime(2017, 3, 5),
                        BirthDate = new DateTime(1987, 12, 15),
                        Charge = "Desenvolvedor",
                        Identifier = 1111,
                    };

                    context.Employee.Add(employee1);
                } else
                {
                    employee1 = context.Employee.Where(e => e.Name == "Leo").First();
                }

                context.SaveChanges();
            }
        }

    }
}
