using InvoiceApp.Services.Interfaces;
using InvoiceApp.Services.Models;
using Microsoft.AspNetCore.Mvc;


namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class InvoiceController : ControllerBase
    {
        public IInvoiceService InvoiceService { get; set; }
        public IPdfService PdfService { get; set; }
        public InvoiceController(IInvoiceService invoiceService, IPdfService pdfService)
        {
            this.InvoiceService = invoiceService;
            this.PdfService = pdfService;
        }


        [HttpGet]
        [Route("myInvoices")]
        public async Task<dynamic> GetInvoices([FromQuery] int userId)
        {
            var invoices = InvoiceService.GetInvoicesByUser(userId);
            return invoices;
        }

        [HttpPost]
        [Route("addInvoice")]
        public async Task<dynamic> AddInvoice([FromBody] InvoiceDto invoice)
        {
            var result = InvoiceService.AddInvoice(invoice);
            return result;

        }

        [HttpPost]
        [Route("download")]
        public async Task<dynamic> DownloadInvoice([FromQuery] int invoiceNumber)
        {
            var pdfBytes = PdfService.GenerateInvoicePdf();

            return File(pdfBytes, "application/pdf", $"Invoice_.pdf");
        }
    }
}