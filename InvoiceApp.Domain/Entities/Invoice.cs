using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Domain.Entities
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public string InvoiceNumber { get; set; }
        public Contractor Buyer { get; set; }
        public float NetValue { get; set; }
        public float GrossValue { get; set; }
        public float VatAmmount { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime DateOfPayment { get; set; }
        public float LeftToPay { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public string Items { get; set; }

    }
}
