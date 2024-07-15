using InvoiceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InvoiceApp.Services.Models
{
    public class ItemDto
    {
        public string? Name { get; set; }
        public Unit? Unit { get; set; }
        public string? PKWiU { get; set; }
        public float? NetPrice { get; set; }
        public float? GrossPrice { get; set; }
        public int? Vat { get; set; }
    }
}
