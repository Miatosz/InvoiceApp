using Common.Logging;
using DinkToPdf;
using DinkToPdf.Contracts;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Services.Models;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
namespace InvoiceApp.Services.Services
{
    public class PdfService : IPdfService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PdfService));
        private readonly IConverter converter;
        private readonly IConfigService configService;
        private readonly IInvoiceService invoiceService;
        private readonly IItemService itemService; 

        public PdfService(IConverter converter, IConfigService configService, IInvoiceService invoiceService, IItemService itemService)
        {
            this.converter = converter;
            this.configService = configService;
            this.invoiceService = invoiceService;
            this.itemService = itemService;
        }

        public byte[] GenerateInvoicePdf()
        {
            string test = configService.GetValue("InvoiceHtml");
            test = DecodeHtml(test);
            var htmlContent = FillTemplateWithData(test);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 }
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return converter.Convert(pdf);
        }

        private string FillTemplateWithData(string htmlContent)
        {
            var invoice = invoiceService.GetInvoice(1);
            var items = itemService.GetItemsById(invoice.Items.Split(',').Select(int.Parse).ToList());

            var itemsHtml = new StringBuilder();
            foreach (var item in items)
            {
                itemsHtml.AppendLine($@"
                <tr class='item'>
                    <td>{item.Name}</td>
                    <td>todo: quantity</td>
                    <td>{item.GrossPrice}</td>
                    <td>todo: quantity * grossprice</td>
                </tr>");
            }

            return htmlContent
                .Replace("{{invoiceNummber}}", invoice.InvoiceNumber)
                .Replace("{{buyerFullName}}", invoice.Buyer.FullName)
                .Replace("{{BuyerEmail}}", invoice.Buyer.Email)
                .Replace("{{BuyerPhoneNumber}}", invoice.Buyer.PhoneNumber)
                .Replace("{{buyerAddress}}", invoice.Buyer.Address)
                .Replace("{{buyerNip}}", invoice.Buyer.Nip)
                .Replace("{{dateOfIssue}}", invoice.DateOfIssue.ToString("yyyy-MM-dd"))
                .Replace("{{dateOfPayment}}", invoice.DateOfPayment.ToString("yyyy-MM-dd"))
                .Replace("{{grossValue}}", invoice.GrossValue.ToString())
                .Replace("{{DeliveryDate}}", invoice.DeliveryDate.ToString("yyyy-MM-dd"))
                .Replace("{{LeftToPay}}", invoice.LeftToPay.ToString())
                .Replace("{{NetValue}}", invoice.NetValue.ToString())
                .Replace("{{Type}}", invoice.Type)
                .Replace("{{vatAmmount}}", invoice.VatAmmount.ToString())
                .Replace("{{itemsRows}}", itemsHtml.ToString())
                .Replace("{netValue}", invoice.NetValue.ToString())
                .Replace("{vatAmount}", invoice.VatAmmount.ToString())
                .Replace("{grossValue}", invoice.GrossValue.ToString());
        }

        private string DecodeHtml(string htmlContent)
        {
            // Unescape the HTML content
            return Regex.Unescape(htmlContent);
        }

    }    
}




