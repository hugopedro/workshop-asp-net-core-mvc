using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerservice)
        { // injeção de dependencia
            _sellerService = sellerservice;
        }

        public IActionResult Index()
        {
            //implementação da chamada server service .findall
            var list = _sellerService.FindAll(); // isso irá retornar uma lista de seller
            return View(list); // a lista será passada como argumento no método view pra ele gerar um IActionResult contendo a lista ali.
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // o método de inserir tem q ser post por isso isso ta aqui
        [ValidateAntiForgeryToken] // é pra previnir ataques CSRF(Quando alguem aproveita a seção de autenticação e envia malware aproveitando a autenticação)
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            //redirecionar a requisição pra index(tela principal)
            return RedirectToAction(nameof(Index)); //nameof(Index) melhora a manutenção do sistema pq se algum dia mudar o nome do string da linha 21 nao vai ter que mudar nada!
        }

    }
}