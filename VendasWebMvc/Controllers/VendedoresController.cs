using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            var list = _servicoVendedor.ObterTodosVendedores();
            return View(list);
        }

        public IActionResult Create()
        {
            var departamentos = _servicoDepartamento.ObterTodosDepartamentos();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            _servicoVendedor.Inserir(vendedor);
            return RedirectToAction(nameof(Index));
        }

        //o ? significa que é opcional
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var obj = _servicoVendedor.ObertPorId(id.Value);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _servicoVendedor.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var obj = _servicoVendedor.ObertPorId(id.Value);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _servicoVendedor.ObertPorId(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Departamento> departamentos = _servicoDepartamento.ObterTodosDepartamentos();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return BadRequest();
            }
            try
            {
                _servicoVendedor.Atualizar(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
