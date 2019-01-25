using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Requerido")] // faz com que o campo seja obrigatorio digitar antes de enviar
        [StringLength(60, MinimumLength= 3, ErrorMessage ="{0} deve ser entre {2} e {1}")] // os numeros das chaves sao definidos pela ordem dos parametros DESSA linha
        public string Name { get; set; } // o {0} pega automaticamente o nome do atributo(no caso é o nome)

        [Required(ErrorMessage = "{0} Requerido")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)] // isso serve pra deixar os emails hyperlinkados
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Requerido")]
        [Display(Name = "Birth Date")] // esse tipo de anotação serve pra customizar o que vai aparecer no display(na pagina)
        [DataType(DataType.Date)] // isso tira a hora e minuto que é desnecessário
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")] // serve pra ordenar em dia/mes/ano que nem no br
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} Requerido")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] // o salario deve ser no minimo 100 e no max 50k
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
