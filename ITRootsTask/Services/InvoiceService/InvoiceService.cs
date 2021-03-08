using ITRootsTask.Context;
using ITRootsTask.Models.DTOs;
using ITRootsTask.Models.Entities;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ITRootsTask.Helpers;

namespace ITRootsTask.Services.InvoiceService
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        private readonly ApplicationDbContext _context;


        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IPagedList<Invoice>> FilterPage(InvoiceFilterDTO filter, int pageNumber)
        {
            return _context.Invoices
                .Where(x => string.IsNullOrEmpty(filter.InvoiceNumber) || x.InvoiceNumber.Contains(filter.InvoiceNumber))
                .Include(x => x.InvoiceDetails)
                .OrderBy(x => x.Id)
                .ToPagedList(pageNumber, 10);
        }

        public async Task<Invoice> Get(long id)
        {
            return id == 0 ?
                    new Invoice() { InvoiceNumber = "I" + (DateTime.Now.Millisecond).ToString() + (_context.Invoices.Count() + 1).ToString() }
                    : _context.Invoices.Where(x => x.Id == id).Include(x => x.InvoiceDetails).FirstOrDefault();
        }

        public async Task<bool> Save(Invoice invoice, List<InvoiceDetail> invoiceDetails)
        {
            bool s1, s2;
            if (invoice.Id == 0)
            {
                invoice.createdOn = DateTime.Now;
                invoice.CreatedBy = ReadSession.User.Id;
                invoice.TotalPrice = invoiceDetails.Sum(x => x.Price);
                _context.Invoices.Add(invoice);
                s1 = s2 = await SaveChanges();
            }
            else
            {
                var dbInvoice = _context.Invoices.AsNoTracking().FirstOrDefault(x => x.Id == invoice.Id);
                dbInvoice.TotalPrice = invoiceDetails.Sum(x => x.Price);
                _context.Entry<Invoice>(dbInvoice).State = EntityState.Modified;
                s1 = await SaveChanges();

                var oldInvoiceDetails = _context.InvoiceDetails.Where(x => x.InvoiceId == invoice.Id);
                _context.InvoiceDetails.RemoveRange(oldInvoiceDetails);

                invoiceDetails.ForEach(x =>
                {
                    x.InvoiceId = invoice.Id;
                    x.createdOn = DateTime.Now;
                    x.CreatedBy = ReadSession.User.Id;
                });
                _context.InvoiceDetails.AddRange(invoiceDetails);
                s2 = await SaveChanges();
            }
            return s1 & s2;
        }

        public async Task<bool> Delete(long id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}