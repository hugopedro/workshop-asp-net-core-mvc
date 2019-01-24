using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerservice, DepartmentService departmentService)
        { // injeção de dependencia
            _sellerService = sellerservice;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            //implementação da chamada server service .findall
            var list = _sellerService.FindAll(); // isso irá retornar uma lista de seller
            return View(list); // a lista será passada como argumento no método view pra ele gerar um IActionResult contendo a lista ali.
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll(); // busca no banco todos os departamentos
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel); // quando a tela de cadastro for acionada ele ja vai receber os departamentos!
        }

        [HttpPost] // o método de inserir tem q ser post por isso isso ta aqui
        [ValidateAntiForgeryToken] // é pra previnir ataques CSRF(Quando alguem aproveita a seção de autenticação e envia malware aproveitando a autenticação)
        public IActionResult Create(Seller seller) // só funfou pq criei no Seller.cs o DepartmentId!
        {
            _sellerService.Insert(seller);
            //redirecionar a requisição pra index(tela principal)
            return RedirectToAction(nameof(Index)); //nameof(Index) melhora a manutenção do sistema pq se algum dia mudar o nome do string da linha 21 nao vai ter que mudar nada!
        }

        public IActionResult Delete(int? id) //o ? significa que o int é opcional
        {
            if (id == null) // se o id for nulo quer dizer que a requisição foi feita de uma forma indevida
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value); // tem q por .value pq ele é um nullable(objeto opcional) 
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) // esse método é pro botao de delete funfar
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {

            if (id == null) // se o id for nulo quer dizer que a requisição foi feita de uma forma indevida
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value); // tem q por .value pq ele é um nullable(objeto opcional) 
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public IActionResult Edit(int? id) // acho q quando tem o ? eh a versao get do método
        {
            if (id == null) // se o id for nulo quer dizer que a requisição foi feita de uma forma indevida
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments }; // instanciação do viewmodel
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Seller seller) // tambem recebe o objeto seller
        { // isso é pro botão de edição funfar
            if (id !=seller.Id) // se o id for diferente do id do vendedor algo está errado
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            } /* ja que as duas exceções são iguais pode-se colocar um ApplicationException, então as exceções vão casar atraves de um upcasting
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }*/
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            } // ou seja em ambos os casos vou redirecionar pra pagina de Error passando a mensagem da exceção
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // (wtf)
            };

            return View(viewModel);
        }
    }
}