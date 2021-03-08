using ITRootsTask.Filters;
using ITRootsTask.Models.DTOs;
using ITRootsTask.Models.Entities;
using ITRootsTask.Models.Shared;
using ITRootsTask.Services.InvoiceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ITRootsTask.Controllers
{
    public class InvoicesController : BaseAuthController
    {
        private readonly IInvoiceService _invoiceService;
        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(InvoiceFilterDTO filter, int page = 1)
        {
            var model = await _invoiceService.FilterPage(filter, page);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Invoices/Partial/InvoicesList.cshtml", model);
            }

            return View(new InvoiceViewModel
            {
                Filter = filter,
                List = model,
            });
        }

        [HttpGet]
        public async Task<ActionResult> InvoiceData(long Id = 0)
        {
            return View(await _invoiceService.Get(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Invoice invoice, [Bind(Prefix = "InvoiceDetails")] List<InvoiceDetail> invoiceDetails)
        {
            var res =await _invoiceService.Save(invoice, invoiceDetails);
            if (res)
                return RedirectToAction("Index");
            else
                return TaskError();
        }

        [HttpPost]
        [AjaxOnly]
        public async Task<ActionResult> Delete(long Id)
        {
            var res = await _invoiceService.Delete(Id);
            return Json(new Response { Success = res, Message = res ? "Success" : "Error" });
        }
    }
}