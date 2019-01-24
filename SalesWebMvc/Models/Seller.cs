using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)] // isso serve pra deixar os emails hyperlinkados
        public string Email { get; set; }

        [Display(Name = "Birth Date")] // esse tipo de anotação serve pra customizar o que vai aparecer no display(na pagina)
        [DataType(DataType.Date)] // isso tira a hora e minuto que é desnecessário
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")] // serve pra ordenar em dia/mes/ano que nem no br
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")] // faz com que numeros tenham duas casas decimais
        public double Salary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; } // <<< isso é necessario para avisar ao Entity Framework que o Id tem que existir, uma vez que o tipo int nao pode ser nulo(negocio do null no banco sql)
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double salary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Salary = salary;
            Department = department;
        }

        public void AddSales (SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales (SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        { // calcula o total de vendas de um vendedor em um intervalo de datas
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }

    }
}
