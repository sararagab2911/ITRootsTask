'use strict';
let itemForm = '#InvoiceForm';
let handleBarRowId = '#InvoiceDetailRow';
let tblInvoiceDetails = '#tblInvoiceDetails';
let prefix = 'InvoiceDetails';


function AppendRow() {
    if (ValidateTable(tblInvoiceDetails)) {
        var template = Handlebars.compile($(handleBarRowId).html());
        var templateHtml = template({ Index: Number($(`${tblInvoiceDetails} tbody tr`).length), Counter: Number($(`${tblInvoiceDetails} tbody tr`).length + 1) });
        $(`${tblInvoiceDetails} tbody`).append(templateHtml); // append
        OrderTableNames(tblInvoiceDetails, prefix); // order
        RefreshRequiredInputsValidation();
    } else {
        WarningToast(PleaseFillRequiredFields);
    }
}


function RemoveDetailRow(element) {
    RemoveRow(element);
    OrderTableNames(tblInvoiceDetails, prefix); // order
}



function SubmitForm() {
    let hash = tblInvoiceDetails.includes('#') ? '' : '#';

    if (!$(itemForm).valid() || !($(`${hash + tblInvoiceDetails} tbody tr`).length > 0 && ValidateTable(tblInvoiceDetails))) {
        WarningToast(PleaseFillRequiredFields);
    }
    else {
        $(itemForm).submit();
    }
}


