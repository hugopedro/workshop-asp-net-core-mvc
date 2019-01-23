using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    { //readonly previne que essa dependencia nao seja alterada
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        { // implementar uma operação no Entity Framework pra retornar no banco de dados todos os vendedores
            return _context.Seller.ToList(); // Isso irá acessar a fonte de dados contido na tabela vendedores e converter isso para uma lista

        }

        public void Insert(Seller obj)
        { // método pra poder exibir um seller, é bem simples no entity framework
            obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }


    }
}
