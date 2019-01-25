using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{

    public class DepartmentService //esse serviço vai ter a mesma estrutura de dependencia do SellerService(_context e construtor)
    {
        //readonly previne que essa dependencia nao seja alterada
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //método pra retornar todos os departamentos
        //método assincrono serve pra carregar pequenas coisas mais rapida
        public async Task<List<Department>> FindAllAsync() //retorna um Task<List<Department>>
        { // pra tudo isso funfar tem que importar o   Microsoft.EntityFrameworkCore;
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); // Expressoes LINQ só vao ser executadas quando chamamos outra coisa que provoca a execução dela, no caso é a chamada .ToList
        } //ToList executa a consulta e transforma o resultado para List

    }
}
