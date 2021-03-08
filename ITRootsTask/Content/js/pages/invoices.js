'use strict';
let targetFilterForm = '#InvoicesFilterForm';


function deleteInvoice(id) {
    Delete('/Invoices/Delete', id, targetFilterForm);
}

