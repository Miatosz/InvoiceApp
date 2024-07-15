using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Entities
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public Unit Unit { get; set; }
        public string PKWiU { get; set; }
        public float NetPrice { get; set; }
        public float GrossPrice { get; set; }
        public int Vat { get; set; }
    }

    public enum Unit
    {
        kg,
        mb,
        m3,
        pcs,
        service
    }
}
