using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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

        public void Insert(Seller obj) //aqui o objeto seller está devidamento instanciado já com o departamento
        { // método pra poder exibir um seller, é bem simples no entity framework
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id) // encontrar o vendedor por id
        {   // aqui é pra ele puxar o ID do vendedor tbm, entao aparece o nome do departamento referente nos detalhes!
            // é assim que se fazer o "eager loading" que significa carregar objetos relacionados ao objeto principal!
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {                //testando se o id já existe no banco, ja que tamos atualizando o id do objeto tem que existir
                //se nao existir...
                throw new NotFoundException("Id não encontrando");
            }
            try
            {
                _context.Update(obj); // atualizando o objeto utilizando entity framework
                _context.SaveChanges();
            }
            catch (DbConcurrencyException e ) // Interceptando uma exceção do nivel de acesso a dados
            {
                throw new DbConcurrencyException(e.Message); // relançando a exceção só que a nivel de serviço, respitando o padrão MVC
            } // Ou seja, isso é muito importante pra dividir as camadas, onde a camada de serviços (NotFoundException) não vai propagar uma exceção do nível de acesso a dados(DbConcurrencyException)
        } // portando se uma exceção de nivel de acesso a dados acontecer a minha camada de serviço vai lançar uma exceção da camada dela
    } // daí o controlador (SellerController) vai lhe dar com exceções só da camada de serviço
}
