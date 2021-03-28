using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Services;

namespace VendasWebMvc.Controllers
{
    public class RecordeVendasController : Controller
    {
        private readonly ServicoRecordeVendas _servicoRecordeVendas;

        public RecordeVendasController(ServicoRecordeVendas servicoRecordeVendas)
        {
            _servicoRecordeVendas = servicoRecordeVendas;
        }

        public IActionResult Index()
        {
            return View();
        }

        //? opciopnal
        public async Task<IActionResult> BuscaSimples(DateTime? Datamin, DateTime? Datamax)
        {
            //Se a data não possui um valor minimo ou se a data não existir 
            if (!Datamin.HasValue)            
                Datamin = new DateTime(DateTime.Now.Year, 1, 1);            

            if (!Datamax.HasValue)            
                Datamax = DateTime.Now;

            ViewData["Datamin"] = Datamin.Value.ToString("yyyy-MM-dd");
            ViewData["Datamax"] = Datamax.Value.ToString("yyyy-MM-dd");

            var resultado = await _servicoRecordeVendas.BuscaPorDataAsync(Datamin, Datamax);
            return View(resultado);
        }

        public IActionResult PesquisaPorAgrupamento()
        {
            return View();
        }
    }
}
