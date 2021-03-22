using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public double SalarioBase { get; set; }
        public Departamento Departamento { get; set; }

        //Um vendedor possui vários recordes de vendas
        public ICollection<RecordeVendas> Vendas { get; set; } = new List<RecordeVendas>();

        public Vendedor()
        { 
        }

        public Vendedor(string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void AdicionarVendas(RecordeVendas rv)
        {
            Vendas.Add(rv);
        }

        public void RemoverVendas(RecordeVendas rv)
        {
            Vendas.Remove(rv);
        }

        public double TotalVendas(DateTime inicio, DateTime final)
        {
            return Vendas.Where(rv => rv.Data >= inicio && rv.Data <= final).Sum(rv => rv.Quantia);
        }
    }
}
