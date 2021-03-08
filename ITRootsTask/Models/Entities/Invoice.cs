using System.Collections.Generic;

namespace ITRootsTask.Models.Entities
{
    public class Invoice  : Entity
    {
        public string InvoiceNumber { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
