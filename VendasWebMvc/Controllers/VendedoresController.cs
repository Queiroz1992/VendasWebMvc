using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;
using VendasWebMvc.Models.ViewModels;
using VendasWebMvc.Services;
using VendasWebMvc.Services.Exceptions;

namespace VendasWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly ServicoVendedor _servicoVendedor;
        private readonly ServicoDepartamento _servicoDepartamento;

        public VendedoresController(ServicoVendedor servicoVendedor, ServicoDepartamento servicoDepartamento)
        {
            _servicoVendedor = servicoVendedor;
            _servicoDepartamento = servicoDepartamento;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _servicoVendedor.ObterTodosVendedoresAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _servicoDepartamento.ObterTodosDepartamentosAsync();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            //Se o modelo não for validado
            if (!ModelState.IsValid)
            {
                var departamentos = await _servicoDepartamento.ObterTodosDepartamentosAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }                 
            await _servicoVendedor.InserirAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        //o ? significa que é opcional
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { mensagem = "Id não foi fornecido" });

            var obj = await _servicoVendedor.ObertPorIdAsync(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { mensagem = "Id não encontrado" });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _servicoVendedor.ExcluirAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegridadeException e)
            {
                return RedirectToAction(nameof(Error), new { mensagem = e.Message });
            }
            
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { mensagem = "Id não foi fornecido" });

            var obj = await _servicoVendedor.ObertPorIdAsync(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { mensagem = "Id não encontrado" });

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não foi fornecido" });
            }

            var obj = await _servicoVendedor.ObertPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não encontrado" });
            }

            List<Departamento> departamentos = await _servicoDepartamento.ObterTodosDepartamentosAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _servicoDepartamento.ObterTodosDepartamentosAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não corresponde" });
            }
            try
            {
                await _servicoVendedor.AtualizarAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { mensagem = e.Message });
            }
        }

        public IActionResult Error(string mensagem)
        {
            var viewModel = new ErrorViewModel
            {
                Mensagem = mensagem,
                //massete do framework pra pegar o id da requisição 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
