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
        public List<Vendedor> ObterTodosVendedores()
        {
            return _context.Vendedor.ToList();
        }

        public void Inserir(Vendedor obj)
        {
            //obj.Departamento = _context.Departamento.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Vendedor ObertPorId(int id)
        {
            return _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(obj => obj.Id == id);
        }

        public void Excluir(int id)
        {
            var obj = _context.Vendedor.Find(id);
            _context.Vendedor.Remove(obj);
            _context.SaveChanges();
        }

        public void Atualizar(Vendedor obj)
        {
            //Any serve para falar se existe um registro no banco com a condição que for colocado
            if (!_context.Vendedor.Any(v => v.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
