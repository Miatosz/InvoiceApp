using InvoiceApp.Domain.Entities;
using InvoiceApp.Services.Models;

namespace InvoiceApp.Services.Interfaces
{
    public interface IInvoiceService
    {
        Invoice GetInvoice(int id);
        Invoice GetInvoiceByNumber(string invoiceNumber);
        bool UpdateInvoice(InvoiceDto invoice, int id);
        List<Invoice> GetInvoicesByUser(int userId);
        Invoice AddInvoice(InvoiceDto invoice);
    }
}
