using System;
using VendasWebMvc.Models.Enums;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendasWebMvc.Models
{
    public class RecordeVenda
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Quantia { get; set; }
        public StatusVenda Status { get; set; }        
        public Vendedor Vendedor { get; set; }

        public RecordeVenda()
        {
        }

        public RecordeVenda(DateTime data, double quantia, StatusVenda statusVenda, Vendedor vendedor)
        {

            Data = data;
            Quantia = quantia;
            Status = statusVenda;
            Vendedor = vendedor;
        }        
    }
}
