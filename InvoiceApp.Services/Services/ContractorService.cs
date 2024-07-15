using InvoiceApp.Domain;
using InvoiceApp.Domain.Entities;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Services.Models;

namespace InvoiceApp.Services.Services
{
    public class ContractorService : IContractorService
    {
        public Contractor GetContractor(int id)
        {
            using (var context = new InvoiceAppContext())
            {
                var contractor = context.Contractor.Find(id);
                return contractor ?? new Contractor();
            }
        }

        public Contractor GetContractorByNip(string nip)
        {
            using (var context = new InvoiceAppContext())
            {
                var contractor = context.Contractor.FirstOrDefault(x => x.Nip == nip);
                return contractor ?? new Contractor();
            }
        }

        public bool UpdateContractor(ContractorDto contractor, int id)
        {
            try
            {
                using (var context = new InvoiceAppContext())
                {
                    var co = context.Contractor.Find(id);

                    if (co is null)
                        return false;

                    co.Address = contractor.Address ?? co.Address;
                    co.PhoneNumber = contractor.PhoneNumber ?? co.PhoneNumber;
                    co.FullName = contractor.FullName ?? co.FullName;
                    co.ShortName = contractor.ShortName ?? co.ShortName;
                    co.Email = contractor.Email ?? co.Email;
                    co.Nip = contractor.Nip ?? co.Nip;

                    context.Update(co);
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
