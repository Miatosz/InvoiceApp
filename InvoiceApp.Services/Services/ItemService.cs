using InvoiceApp.Domain;
using InvoiceApp.Domain.Entities;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Services.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InvoiceApp.Services.Services
{
    public class ItemService : IItemService
    {
        public Item GetItem(int id)
        {
            using (var context = new InvoiceAppContext())
            {
                var item = context.Item.Find(id);
                return item ?? new Item();
            }
        }

        public Item GetItemByName(string Name)
        {
            using (var context = new InvoiceAppContext())
            {
                var item = context.Item.FirstOrDefault(x => x.Name == Name);
                return item ?? new Item();
            }
        }

        public bool UpdateItem(ItemDto item, int id)
        {
            try
            {
                using (var context = new InvoiceAppContext())
                {
                    var it = context.Item.Find(id);

                    if (it is null)
                        return false;

                    it.NetPrice = item.NetPrice ?? it.NetPrice;
                    it.Unit = item.Unit ?? it.Unit;
                    it.GrossPrice = item.GrossPrice ?? it.GrossPrice;
                    it.PKWiU = item.PKWiU ?? it.PKWiU;
                    it.Name = item.Name ?? it.Name;
                    it.Vat = item.Vat ?? it.Vat;
                    
                    context.Update(it);
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
