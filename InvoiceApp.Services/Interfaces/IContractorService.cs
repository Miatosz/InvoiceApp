using InvoiceApp.Domain.Entities;
using InvoiceApp.Services.Models;

namespace InvoiceApp.Services.Interfaces
{
    public interface IContractorService
    {
        Contractor GetContractor(int id);
        Contractor GetContractorByNip(string nip);
        bool UpdateContractor(ContractorDto contractor, int id); 
    }
}
