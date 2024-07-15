using InvoiceApp.Domain.Entities;
using InvoiceApp.Services.Models;

namespace InvoiceApp.Services.Interfaces
{
    public interface IItemService
    {
        Item GetItem(int Id);
        Item GetItemByName(string Name);
        bool UpdateItem(ItemDto item, int id); 
    }
}
