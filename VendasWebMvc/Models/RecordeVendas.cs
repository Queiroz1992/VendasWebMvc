using System;
using VendasWebMvc.Models.Enums;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class RecordeVendas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Quantia { get; set; }
        public StatusVenda Status { get; set; }        
        public Vendedor Vendedor { get; set; }

        public RecordeVendas()
        {
        }

        public RecordeVendas(int id, DateTime data, double quantia, StatusVenda statusVenda, Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Quantia = quantia;
            Status = statusVenda;
            Vendedor = vendedor;
        }        
    }
}
