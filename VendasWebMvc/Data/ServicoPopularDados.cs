using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;
using VendasWebMvc.Models.Enums;


namespace VendasWebMvc.Data
{
    public class ServicoPopularDados
    {
        private readonly VendasWebMvcContext _context;

        public ServicoPopularDados(VendasWebMvcContext context)
        {
            _context = context;
        }

        //Populando a base de dados caso não esteja ainda
        public void PopularDados()
        {
            //Operação Any do linq vai testar se existe algum registro nessa tabela
            if (_context.Departamento.Any() || _context.Vendedor.Any() || _context.RecordeVendas.Any())
            {
                //se existe registro, o return vai cortar a execução do método e não vai retornar nada. Banco já está preenchido
                return;
            }

            Departamento d1 = new Departamento("Computador");
            Departamento d2 = new Departamento("Eletronicos");
            Departamento d3 = new Departamento("Moda");
            Departamento d4 = new Departamento("Livro");

            Vendedor v1 = new Vendedor("Filipe", "filipe@filipe.com", new DateTime(1992, 3, 19), 6000.0, d1);
            Vendedor v2 = new Vendedor("Queiroz", "f2@f2.com", new DateTime(1990, 3, 25), 4000.0, d2);
            Vendedor v3 = new Vendedor("Luzia", "luzia@luzia.com", new DateTime(1998, 11, 09), 5000.0, d4);
            Vendedor v4 = new Vendedor("Angel", "angel@hotmail.com", new DateTime(2019, 03, 19), 500.0, d3);

            RecordeVendas r1 = new RecordeVendas( new DateTime(2018, 09, 25), 1100.0, StatusVenda.Faturado, v1);
            RecordeVendas r2 = new RecordeVendas(new DateTime(2018, 07, 10), 2000.0, StatusVenda.Pendente, v2);
            RecordeVendas r3 = new RecordeVendas(new DateTime(2019, 11, 25), 900.0, StatusVenda.Faturado, v3);
            RecordeVendas r4 = new RecordeVendas(new DateTime(2020, 02, 15), 200.0, StatusVenda.Cancelado, v4);
            RecordeVendas r5 = new RecordeVendas(new DateTime(2021, 09, 25), 3000.0, StatusVenda.Faturado, v1);
            RecordeVendas r6 = new RecordeVendas(new DateTime(2021, 12, 22), 4000.0, StatusVenda.Faturado, v3);

            _context.Departamento.AddRange(d1, d2, d3, d4);
            _context.Vendedor.AddRange(v1, v2, v3, v4);
            _context.RecordeVendas.AddRange(r1, r2, r3, r4, r5, r6);

            _context.SaveChanges();
        }

    }
}
