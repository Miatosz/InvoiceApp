namespace InvoiceApp.Services.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateInvoicePdf();
    }
}
