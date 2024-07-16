using Autofac;
using DinkToPdf;
using DinkToPdf.Contracts;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Services.Services;
using Common.Logging;

namespace InvoiceApp
{
    public class AutofacModule : Autofac.Module
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AutofacModule));

        protected override void Load(ContainerBuilder builder)
        {
            try
            {
                RegisterServices(builder);
            }
            catch (Exception ex)
            {
                Logger.Error("Error on registering services", ex);
            }
        }

        private void RegisterServices(ContainerBuilder builder)
        {

            builder.RegisterInstance(new SynchronizedConverter(new PdfTools())).As<IConverter>().SingleInstance();
            builder.RegisterType<ContractorService>().As<IContractorService>();
            builder.RegisterType<ConfigService>().As<IConfigService>();
            builder.RegisterType<InvoiceService>().As<IInvoiceService>();
            builder.RegisterType<ItemService>().As<IItemService>();
            builder.RegisterType<PdfService>().As<IPdfService>();
        }
    }
}