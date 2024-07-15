using Autofac;
using Autofac.Extensions.DependencyInjection;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Services.Services;
using System.Reflection;

namespace InvoiceApp
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            try
            {
                RegisterServices(builder);
            }
            catch (Exception ex)
            {
            }
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ContractorService>().As<IContractorService>();
            builder.RegisterType<ConfigService>().As<IConfigService>();
            builder.RegisterType<InvoiceService>().As<IInvoiceService>();
            builder.RegisterType<ItemService>().As<IItemService>();
        }
    }
}