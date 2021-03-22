﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Data;
using VendasWebMvc.Models;

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
    }
}
