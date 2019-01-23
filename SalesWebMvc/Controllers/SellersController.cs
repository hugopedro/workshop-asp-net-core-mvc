using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}