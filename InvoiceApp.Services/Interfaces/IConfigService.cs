namespace InvoiceApp.Services.Interfaces
{
    public interface IConfigService
    {
        string GetValue(string key);
        bool UpdateValue(string key, string value);
    }
}
