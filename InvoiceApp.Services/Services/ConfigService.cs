using InvoiceApp.Domain;
using InvoiceApp.Services.Interfaces;

namespace InvoiceApp.Services.Services
{
    public class ConfigService : IConfigService
    {
        public string GetValue(string key)
        {
            using (var context = new InvoiceAppContext())
            {
                var op = context.Config.FirstOrDefault(x => x.Key == key);
                return op?.Value;
            }
        }

        public bool UpdateValue(string key, string value)
        {
            try
            {
                using (var context = new InvoiceAppContext())
                {
                    var ce = context.Config.FirstOrDefault(x => x.Key == key);

                    if (ce is null)
                        return false;

                    ce.Value = value;
                    context.Update(ce);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
