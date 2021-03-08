using System.ComponentModel.DataAnnotations.Schema;

namespace ITRootsTask.Models.Entities
{
    public class InvoiceDetail : Entity
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
    }
}
