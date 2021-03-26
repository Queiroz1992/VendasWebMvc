using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Data;
using VendasWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Services.Exceptions;

namespace VendasWebMvc.Services
{
    public class ServicoVendedor 
    {
        private readonly VendasWebMvcContext _context;

        public ServicoVendedor(VendasWebMvcContext context)
        {
            _context = context;
        }

        //Lista que retorna todos vendedores do banco de dados
        public async Task<List<Vendedor>> ObterTodosVendedoresAsync()
        {
            return await _context.Vendedor.ToListAsync();
        }

        public async Task InserirAsync(Vendedor obj)
        {
            //obj.Departamento = _context.Departamento.First();
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Vendedor> ObertPorIdAsync(int id)
        {
            return await _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task ExcluirAsync(int id)
        {
            var obj = await _context.Vendedor.FindAsync(id);
            _context.Vendedor.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Vendedor obj)
        {
            //Any serve para falar se existe um registro no banco com a condição que for colocado
            bool temAlgum = await _context.Vendedor.AnyAsync(v => v.Id == obj.Id);
            
            if (!temAlgum)
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
