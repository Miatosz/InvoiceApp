using Autofac.Extensions.DependencyInjection;
using Autofac;
using InvoiceApp.Domain;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<InvoiceAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterType<InvoiceService>().As<IInvoiceService>();
            });

            var app = builder.Build();



            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
