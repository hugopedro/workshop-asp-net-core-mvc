using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {
        }
        //lembrando que nao se deve por argumentos que sao coleções no construtor
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        { // pegando cada vendedor da minha lista chamando o totalsales do vendedor naquele periodo inicial e final e ai faço uma soma desse resultado pra todos os vendedores do meu departamento
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }

    }
}
