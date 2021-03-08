using ITRootsTask.Models.DTOs;
using ITRootsTask.Models.Entities;
using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITRootsTask.Services.InvoiceService
{
    public interface IInvoiceService
    {
        Task<IPagedList<Invoice>> FilterPage(InvoiceFilterDTO search, int pageNumber);
        Task<Invoice> Get(long id);
        Task<bool> Save(Invoice invoice, List<InvoiceDetail> invoiceDetails);
        Task<bool> Delete(long id);
    }
}