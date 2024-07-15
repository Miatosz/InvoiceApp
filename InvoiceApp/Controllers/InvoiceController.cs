using InvoiceApp.Services.Interfaces;
using InvoiceApp.Services.Models;
using Microsoft.AspNetCore.Mvc;


namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        public IInvoiceService InvoiceService { get; set; }

        public InvoiceController(IInvoiceService invoiceService)
        {
            this.InvoiceService = invoiceService;
        }


        [HttpGet]
        [Route("MyInvoices")]
        public async Task<dynamic> GetInvoices([FromQuery] int userId)
        {
            var invoices = InvoiceService.GetInvoicesByUser(userId);
            return invoices;
        }

        [HttpPost]
        [Route("AddInvoice")]
        public async Task<dynamic> AddInvoice([FromBody] InvoiceDto invoice)
        {
            var result = InvoiceService.AddInvoice(invoice);
            return result;

        }
    }
}