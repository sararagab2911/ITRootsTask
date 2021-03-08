'use strict';

let defaultImage = '/Assets/imgs/default-photo.png';
let maxQueyParameterNumber = 20;

// location
let arrLocations = '';
let controller = '';
let actionList = '';
let action = '';
try {
    arrLocations = window.location.href.split('/');
    controller = arrLocations[3];
    actionList = arrLocations.length > 4 ? arrLocations[4].split('?') : ['index'];
    action = actionList[0];
} catch (ex) { console.log(ex); }
//


function Toast(type, message) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": IsRTL ? "toast-top-left" : "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "200",
        "hideDuration": "500",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    Command: toastr[type](message);
}

function InfoToast(message) { Toast('info', message); }
function SuccessToast(message) { Toast('success', message); }
function ErrorToast(message) { Toast('error', message); }
function WarningToast(message) { Toast('warning', message); }


function SwalInfo(message) {
    Swal.fire(message);
}

function SwalConfirm(message) {
    return Swal.fire({
        title: message ? message : ARE_YOU_SURE,
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: YES,
        cancelButtonText: CANCEL,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33'
    });
}


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(input).closest('div.img-collection').find('img').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    } else {
        $('input[type=hidden][name=' + $(input).attr('name') + ']').val('');
        $(input).closest('div').find('img').attr('src', '/assets/placeholder.png');
    }
}

function readURLs(input) {
    let $target = $(input).closest('div').find('.images-container');
    $target.html('');
    if (input.files && input.files[0]) {
        for (var i = 0; i < input.files.length; i++) {
            let reader = new FileReader();
            reader.onload = function (e) {
                $target.append(`<img width='100' height='100' class='m-1' src='${e.target.result}' onerror='this.src="/assets/pdf.png"' />`);
            };
            reader.readAsDataURL(input.files[i]);
        }
    } else {
        $target.html('');
    }
}


function RemoveImage(element) {
    $(element).closest('div.img-collection').find('img').attr('src', defaultImage);
    $(element).closest('div.img-collection').find('input[type=hidden]').val('');
}


function OrderPageTables() {
    for (var i = 0; i < $('.table').length; i++) {
        OrderTable($($('.table')[i]).attr('id'));
    }
}

function OrderTable(tblId) {
    if (!tblId) return;
    let hash = tblId.includes('#') ? '' : '#';
    $(hash + tblId + ' tbody tr[class!=exclude]').each((i, tr) => {
        $(tr).find('td span.row-index').text(i + 1);
    });
}


function ValidateTable(tblId) {
    let valid = true;
    let hash = tblId.includes('#') ? '' : '#';
    $(`${hash + tblId} tbody tr`).each(function (i, tr) {
        $(tr).find("input.required").each((x, requiredInput) => {
            if (!$(requiredInput).val()) {
                valid = false;
            }
        });
    });
    return valid;
}


function OrderTableNames(tblId, prefix = '') {
    if (!tblId) return;
    let hash = tblId.includes('#') ? '' : '#';
    $(`${hash + tblId} tbody tr[class!=exclude]`).each((i, tr) => {
        $(tr).find('input,select').each((x, input) => {
            let splitted = $(input).attr('name') ? $(input).attr('name').split('.') : '';
            let suffix = splitted.length > 1 ? splitted[1] : $(input).attr('name');
            $(input).attr('name', `${prefix}[${i}].${suffix}`);
        });
    });
}


function RemoveRow(element) {
    $(element).closest('tr').remove();
}


function LOAD() {
    $('body').addClass('loading-ok');
}

function UNLOAD() {
    $('body').removeClass('loading-ok');
}


function DatePicker() {
    $('.datepicker').datepicker({
        format: "yyyy/mm/dd",
        todayBtn: "linked",
        autoclose: true
    });
}


function FancyBox() {
    $(".fancybox").fancybox({
        openEffect: 'elastic',
        closeEffect: 'elastic',
        helpers: { title: { type: 'inside' } }
    });
}


function ResetSearch(element) {
    $(element).closest('form').find('input[type=text],input[type=search]').val('');
    $(element).closest('form').submit();
}


function CLEAR_MODAL() {
    $('.modal').on('hidden.bs.modal', function (e) {
        $('.modal').find('input[type=text],input[type=email],input[type=password],textarea').val('');
    });
}


// AJAX CALL
function AjaxCall(type, url, data, async, onSuccess, onComplete) {
    $.ajax({
        url: url,
        type: type ? type : 'POST',
        data: JSON.stringify(data),
        async: async,
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            if (onSuccess) { onSuccess(res); }
        },
        complete: function (res) {
            if (onComplete) { onComplete(res); }
        },
        error: function (error) {
            console.log('Ajax Call Error : ' + error);
        }
    });
}


// BIND DDL
function BindDDL(ID, items, skipSelect, skipClear, skipAddingSelect, skipChange) {
    $("select[id='" + ID + "']").each(function (i, o) {
        var DDL = $(o);
        var currentVal = DDL.val();
        if (!skipClear)
            DDL.empty();
        if (!skipAddingSelect) {

            DDL.append($("<option></option>").html(defaultSelectText).val(null));
            DDL.val(DDL.find("option:first").val());
        }

        var valueFounded = false;
        $(items).each(function (i, o) {
            DDL.append($("<option></option>").val(o.Id).html(o.Name));
            if (o.Id === currentVal) { valueFounded = true; }
        });

        if (!skipSelect && valueFounded)
            DDL.val(currentVal);

        if (DDL.hasClass("select2")) {
            DDL.select2();
        } //Refresh

        if (!skipChange && currentVal !== DDL.val())
            DDL.trigger("change"); //Fire change event if value changed
    });
}

// UPLOAD FILE
function UploadFile(_fileUpload, url, onSuccess) {
    if (window.FormData !== undefined) {
        var fileUpload = $(_fileUpload).get(0);
        var files = fileUpload.files;
        if (files.length > 0) {
            //LOAD(); //uncomment if change async to false
            var fileData = new FormData();
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
            $.ajax({
                url: url,
                type: "POST",
                contentType: false,
                processData: false,
                async: true,
                data: fileData,
                success: function (res) {
                    //UNLOAD(); //uncomment if change async to false
                    if (onSuccess) { onSuccess(res); }
                },
                error: function (err) {
                    console.log(err.statusText);
                }
            });
        }
    } else {
        alert("FormData is not supported.");
    }
}


// AUTO COMPLETE
function AutoComplete(url, afterSelect, appendTo) {
    $('.autocomlete').keypress(function () {
        var element = $(this);
        $(this).autocomplete({
            appendTo: appendTo === undefined ? '' : appendTo,
            source: function (request, response) {
                $.ajax({
                    url: url,
                    type: 'POST',
                    dataType: 'JSON',
                    data: { text: request.term },
                    success: function (data) { response($.map(data, function (item) { return item; })); }
                });
            },
            focus: function (event, ui) {
                $(this).val(ui.item.label);
                event.preventDefault();
            },
            select: function (event, ui) {
                afterSelect(element, ui.item.value);
                event.preventDefault();
                return false;
            }
        });
    });
}


// GENERAL DELETE
function Delete(url, id, target) {
    SwalConfirm().then((result) => {
        if (result.value) {
            AjaxCall('POST', url, { id: id }, true, (response) => {
                if (response.Success) {
                    SuccessToast(response.Message);
                    if (target) {
                        $(target).submit();
                    }
                }
                else ErrorToast(response.Message);
            });
        }
    });
}

function LoadPartial(divId, url, afterLoad = null) {
    $(divId).load(url, () => {
        if (afterLoad) afterLoad();
    });
}

// MODAL EDIT
function LoadPartialInModal(url, title, largeModal = false, afterLoad = null, isstatic = true) {
    let modalId = largeModal ? '#GeneralLargeModal' : '#GeneralModal';
    $(modalId).find('.modal-body').load(url, () => {
        $(modalId).find('.modal-title').text(title);
        isstatic ? $(modalId).modal({ backdrop: 'static', keyboard: false }) : $(modalId).modal('show');
        if (afterLoad) {
            afterLoad();
        }
    });
}

function LoadViewOnlyPartialInModal(url, title, largeModal = false) {
    LoadPartialInModal(url, title, largeModal, () => {
        $('#GeneralModal').find('.modal-title').text(title);
        $('#GeneralModal').find('select,input').css('pointer-events', 'none');
        $('#GeneralModal').find('button[type="submit"]').hide();
    });
}

// GENERAL SUCCESS
function Success(data, target) {
    if (data) {
        if (data.Success) {
            SuccessToast(data.Message);
            if (target) {
                $(target).submit();
            }
            $('.modal').modal('hide');
        }
        else ErrorToast(data.Message);
    }
}



var getPagination = function () {
    var $a = $(this);
    var $target = $(this).closest('div.pagedList').attr('data-target');

    var _serialized = '';
    for (let i = 0; i < maxQueyParameterNumber; i++) {
        _serialized += $('.pagination-filter-form').serialize().split('&')[i] + (i < (maxQueyParameterNumber - 1) ? '&' : '');
    }

    var options = {
        type: "GET",
        url: $a.attr("href"),
        data: _serialized
    };

    $.ajax(options).done(function (data) {
        if (data) {
            $($target).html(data);
        }
    });
    return false;
};


var paginationFilterFormSubmit = function () {
    var $form = $(this);
    var $target = $(this).attr('data-target');

    var _serialized = '';
    for (let i = 0; i < maxQueyParameterNumber; i++) {
        _serialized += $form.serialize().split('&')[i] + (i < (maxQueyParameterNumber - 1) ? '&' : '');
    }

    var options = {
        type: "GET",
        url: $form.attr("url"),
        data: _serialized
    };

    $.ajax(options).done(function (data) {
        if (data) {
            $($target).html(data);
        }
    });
    return false;
};

function NumbersOnly() {
    $(document).on("keypress keyup blur", ".number", function (event) {
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

}

function RefreshRequiredInputsValidation() {
    $('input.required').on('keyup', (e) => {
        if (!$(e.target).val()) {
            $(e.target).css('box-shadow', '1px 1px 1px red');
        } else {
            $(e.target).css('box-shadow', '0px 0px 0px white');
        }
    });
}


$(function () {
    $('body').on('click', '.pagedList a', getPagination);
    $('body').on('submit', 'form.pagination-filter-form', paginationFilterFormSubmit);


    function REQUIREDACTIONS() {
        UNLOAD();
        DatePicker();
        FancyBox();
        OrderPageTables();
        CLEAR_MODAL();
        $('.select2').select2();
        $('table thead tr th, table tbody tr td').addClass('text-center');
        $('.table thead tr input,.table thead tr select,.table thead tr span.select2-container').css('min-width', '100px');
        $('.table tbody tr input,.table tbody tr select,.table tbody tr span.select2-container').css('min-width', '100px');
        $('.readonly').prop('readonly', 'readonly');
        if (IsRTL) {
            $('.datepicker').css('direction', 'rtl');
        }
        // default image 
        $('body .img').each((i, img) => { if (!$(img).attr('src')) $(img).attr('src', defaultImage); });
        $('body .a-img').each((i, img) => { if (!$(img).attr('href')) $(img).attr('href', defaultImage); });
        //// show validation shadow on submit
        //$('form').on('submit', (e) => { if (!$(e.target).valid()) $(e.target).find('input.required').css('box-shadow', '1px 1px 1px red'); });
        RefreshRequiredInputsValidation();
        NumbersOnly();
    } REQUIREDACTIONS();

    $(document)
        .ajaxStart(function () { LOAD(); })
        .ajaxStop(function () {
            REQUIREDACTIONS();
        });

});

