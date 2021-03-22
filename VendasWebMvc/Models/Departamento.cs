using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendasWebMvc.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        //Um departamento possui vários vendedores
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        public Departamento()
        {
        }

        public Departamento(string nome)
        {
            Nome = nome;
        }

        public void AdicionarVendedor(Vendedor vendedor)
        {
            Vendedores.Add(vendedor);
        }

        public double TotalVendas(DateTime inicio, DateTime final)
        {
            //Aqui eu estou pegando cada vendedor da minha lista, chamando o TotalVendas do vendedor naqule perído inicial e final e ai então eu faço uma soma dess resultado pra todos vendedores do departamento
            return Vendedores.Sum(vendedor => vendedor.TotalVendas(inicio, final));
        }
    }
}
