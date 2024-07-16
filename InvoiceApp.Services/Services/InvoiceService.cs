using Common.Logging;
using InvoiceApp.Domain;
using InvoiceApp.Domain.Entities;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace InvoiceApp.Services.Services
{
    public class InvoiceService : IInvoiceService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(InvoiceService));

        public Invoice GetInvoice(int id)
        {
            using (var context = new InvoiceAppContext())
            {
                var invoice = context.Invoice
                    .Include(i => i.Buyer)
                    .Include(i => i.User)
                    .FirstOrDefault(i => i.Id == id);
                return invoice ?? new Invoice();
            }
        }

        public Invoice GetInvoiceByNumber(string invoiceNumber)
        {
            using (var context = new InvoiceAppContext())
            {
                var invoice = context.Invoice
                    .Include(i => i.Buyer)
                    .Include(i => i.User)
                    .FirstOrDefault(x => x.InvoiceNumber == invoiceNumber);
                return invoice ?? new Invoice();
            }
        }

        public List<Invoice> GetInvoicesByUser(int userId)
        {
            using (var context = new InvoiceAppContext())
            {
                var invoices = new List<Invoice>();
                invoices = context.Invoice
                    .Include(i => i.Buyer)
                    .Include(i => i.User)
                    .Where(inv =>  inv.UserId == userId)
                    .ToList();
                return invoices;
            }
        }

        public bool UpdateInvoice(InvoiceDto invoice, int id)
        {
            try
            {
                using (var context = new InvoiceAppContext())
                {
                    var inv = context.Invoice.Find(id);

                    if (inv is null)
                        return false;

                    inv.NetValue = invoice.NetValue ?? inv.NetValue;
                    inv.InvoiceNumber = invoice.InvoiceNumber ?? inv.InvoiceNumber;
                    inv.DateOfIssue = invoice.DateOfIssue ?? inv.DateOfIssue;
                    inv.DateOfPayment = invoice.DateOfPayment ?? inv.DateOfPayment;
                    inv.DeliveryDate = invoice.DeliveryDate ?? inv.DeliveryDate;
                    inv.Buyer = invoice.Buyer ?? inv.Buyer;
                    inv.GrossValue = invoice.GrossValue ?? inv.GrossValue;
                    inv.LeftToPay = invoice.LeftToPay ?? inv.LeftToPay;
                    inv.VatAmmount = invoice.VatAmmount ?? inv.VatAmmount;


                    context.Update(inv);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error on updating invoice - {id}", ex);
                return false;
            }
        }

        public Invoice AddInvoice(InvoiceDto invoiceDto)
        {
            try
            {
                using (var context = new InvoiceAppContext())
                {
                    var newInvoice = new Invoice
                    {
                        Type = invoiceDto.Type ?? "",
                        InvoiceNumber = invoiceDto.InvoiceNumber ?? "",
                        NetValue = invoiceDto.NetValue ?? 0,
                        GrossValue = invoiceDto.GrossValue ?? 0,
                        VatAmmount = invoiceDto.VatAmmount ?? 0,
                        DateOfIssue = invoiceDto.DateOfIssue ?? new DateTime(),
                        DeliveryDate = invoiceDto.DeliveryDate ?? new DateTime(),
                        DateOfPayment = invoiceDto.DateOfPayment ?? new DateTime(),
                        LeftToPay = invoiceDto.LeftToPay ?? 0,
                        UserId = invoiceDto.UserId ?? 0
                    };

                    context.Invoice.Add(newInvoice);
                    context.SaveChanges();
                    return newInvoice;
                }                
            }
            catch (Exception ex)
            {
                Logger.Error("Error on adding invoice", ex);
                return new Invoice();
            }
        }
    }
}
