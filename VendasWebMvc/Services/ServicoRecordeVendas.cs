using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Data;
using VendasWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace VendasWebMvc.Services
{
    public class ServicoRecordeVendas
    {
        private readonly VendasWebMvcContext _context;

        public ServicoRecordeVendas(VendasWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<RecordeVenda>> BuscaPorDataAsync(DateTime? Datamin, DateTime? Datamax)
        {
            var resultado = from obj in _context.RecordeVendas select obj;

            if (Datamin.HasValue)            
                // x que leva em x.data
                resultado = resultado.Where(x => x.Data >= Datamin.Value);
            
            if (Datamax.HasValue)            
                resultado = resultado.Where(x => x.Data >= Datamax.Value);            

            //fazendo um join entre as tabelas Vendedor e Departamento
            return await resultado
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Departamento, RecordeVenda>>> BuscaPorDataAgrupadaAsync(DateTime? Datamin, DateTime? Datamax)
        {
            var resultado = from obj in _context.RecordeVendas select obj;

            if (Datamin.HasValue)
                resultado = resultado.Where(x => x.Data >= Datamin.Value);

            if (Datamax.HasValue)
                resultado = resultado.Where(x => x.Data <= Datamax.Value);

            return await resultado
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .GroupBy(x => x.Vendedor.Departamento)
                .ToListAsync();
        }
    }
}
