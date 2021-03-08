using ITRootsTask.Models.Entities;
using PagedList;
namespace ITRootsTask.Models.DTOs
{
    public class InvoiceViewModel
    {
        public InvoiceFilterDTO Filter { get; set; } = new InvoiceFilterDTO();
        public IPagedList<Invoice> List { get; set; }
    }
}