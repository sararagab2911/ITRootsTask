'use strict';

const key = 'http://www.indiaoasisrestaurent.com';
const regex = /(.)*[?]/;
let actuallocation = window.location.href.replace('https://localhost:44356', key).replace('http://192.168.100.16', key).toLowerCase();
if (actuallocation.indexOf('#') > 0) {
    actuallocation = actuallocation.substring('#', actuallocation.indexOf('#'));
}
let oasislocation = '';
const match = actuallocation.match(regex);
if (match !== null) oasislocation = match[0].replace('?', '');
else oasislocation = actuallocation;
// URLS (shift)
let cashierUrls = ['/Cashier', '/Cashier/Index', '/Cashier/ListOfTables', '/Cashier/Orders'];
let waiterUrls = ['/Waiter', '/Waiter/Index', '/Waiter/ListOfTables', '/Waiter/Orders'];
let delivaryUrls = ['/Delivary', '/Delivary/Index', '/Delivary/Orders'];
let adminUrls = ['/Shift', '/Shift/Index'];
// URLS (inner order)
let innerOrderUrls = ['/Waiter/ListOfTables', '/Waiter', '/Waiter/Index', '/Cashier/ListOfTables'];
//let innerOrderUrlsToFilter = ['/Order', '/Order/Index', '/Waiter/Orders'];
let innerOrderUrlsToFilter = ['/Waiter/Orders'];
let innerOrderUrlsToCashier = ['/cashier/Orders', '/cashier/Orders?orderType=1'];
// URLS (take-away order)
let takeAwayOrderUrls = ['/Cashier', '/Cashier/Index'];
//let takeAwayOrderUrlsToFilter = ['/Order', '/Order/Index'];
let takeAwayOrderUrlsToFilter = [];
let takeAwayOrderUrlsToFilterToCashier = ['/Cashier', '/Cashier/Orders'];
// URL (delivary order)
let delivaryOrderUrls = ['/Delivary', '/Delivary/Index'];
//let delivaryOrderUrlsToFilter = ['/Order', '/Order/Index', '/Delivary/Orders'];
let delivaryOrderUrlsToFilter = ['/Delivary/Orders'];
let delivaryOrderUrlsToCashier = ['/Cashier/Orders', '/cashier/Orders?orderType=3'];
// URL (sendtoKitcken)
//let sendToKitckenUrlstoFilter = ['/Order', '/Order/Index'];
let sendToKitckenUrlstoFilter = [];
let sendToKitckenUrlstoFilterWaiter = ['/waiter/Orders'];
let sendToKitckenUrlstoFilterCashier = ['/Cashier/Orders'];
let sendToKitckenUrlstoFilterDelivary = ['/Delivary/Orders'];
// URL (finish order)
let finishUrlstoRefresh = ['/Waiter/ListOfTables', '/Cashier/ListOfTables'];
//let finishUrlstoFilter = ['/Order', '/Order/Index'];
let finishUrlstoFilter = [];
let finishUrlstoFilterWaiter = ['/Waiter/Orders'];
let finishUrlstoFilterCashier = ['/Cashier/Orders'];
let finishUrlstoFilterDelivary = ['/Delivary/Orders'];


// urls modifications
cashierUrls = cashierUrls.map((e) => { return key + e.toLowerCase(); });
waiterUrls = waiterUrls.map((e) => { return key + e.toLowerCase(); });
delivaryUrls = delivaryUrls.map((e) => { return key + e.toLowerCase(); });
adminUrls = adminUrls.map((e) => { return key + e.toLowerCase(); });
//
innerOrderUrls = innerOrderUrls.map((e) => { return key + e.toLowerCase(); });
innerOrderUrlsToFilter = innerOrderUrlsToFilter.map((e) => { return key + e.toLowerCase(); });
innerOrderUrlsToCashier = innerOrderUrlsToCashier.map((e) => { return key + e.toLowerCase(); });
//
takeAwayOrderUrls = takeAwayOrderUrls.map((e) => { return key + e.toLowerCase(); });
takeAwayOrderUrlsToFilter = takeAwayOrderUrlsToFilter.map((e) => { return key + e.toLowerCase(); });
takeAwayOrderUrlsToFilterToCashier = takeAwayOrderUrlsToFilterToCashier.map((e) => { return key + e.toLowerCase(); });
//
delivaryOrderUrls = delivaryOrderUrls.map((e) => { return key + e.toLowerCase(); });
delivaryOrderUrlsToFilter = delivaryOrderUrlsToFilter.map((e) => { return key + e.toLowerCase(); });
delivaryOrderUrlsToCashier = delivaryOrderUrlsToCashier.map((e) => { return key + e.toLowerCase(); });
//
sendToKitckenUrlstoFilter = sendToKitckenUrlstoFilter.map((e) => { return key + e.toLowerCase(); });
sendToKitckenUrlstoFilterWaiter = sendToKitckenUrlstoFilterWaiter.map((e) => { return key + e.toLowerCase(); });
sendToKitckenUrlstoFilterCashier = sendToKitckenUrlstoFilterCashier.map((e) => { return key + e.toLowerCase(); });
sendToKitckenUrlstoFilterDelivary = sendToKitckenUrlstoFilterDelivary.map((e) => { return key + e.toLowerCase(); });
//
finishUrlstoRefresh = finishUrlstoRefresh.map((e) => { return key + e.toLowerCase(); });
finishUrlstoFilter = finishUrlstoFilter.map((e) => { return key + e.toLowerCase(); });
finishUrlstoFilterWaiter = finishUrlstoFilterWaiter.map((e) => { return key + e.toLowerCase(); });
finishUrlstoFilterCashier = finishUrlstoFilterCashier.map((e) => { return key + e.toLowerCase(); });
finishUrlstoFilterDelivary = finishUrlstoFilterDelivary.map((e) => { return key + e.toLowerCase(); });


// combined urls
let employeeUrls = [...cashierUrls, ...waiterUrls, ...delivaryUrls];


// START SIGNALR =========================================================================================================================================
var forceLogoutHub = $.connection.forceLogoutHub;
var employeeActionsHub = $.connection.employeeActionsHub;
var orderHub = $.connection.orderHub;


forceLogoutHub.client.clientForceLogout = function (userId) {
    if (userId === USER_ID) {
        $('#logoutForm').submit();
    }
};


// start shift
employeeActionsHub.client.broadcastStartShift = function (userId) {
    if (employeeUrls.includes(oasislocation) && userId === USER_ID) refresh();
    else if (adminUrls.includes(oasislocation)) filter();
};

// end shift
employeeActionsHub.client.broadcastEndShift = function (userId, isCashier) {
    if (isCashier && (cashierUrls.includes(oasislocation) || waiterUrls.includes(oasislocation))) refresh();
    else if (employeeUrls.includes(oasislocation) && userId === USER_ID) refresh();
    else if (adminUrls.includes(oasislocation)) filter();
};


// add order
orderHub.client.broadcastAddOrder = function (orderTypeId, showToast) {
    switch (orderTypeId) {
        case 1: // inner
            if (innerOrderUrls.includes(oasislocation)) refresh();
            else if (innerOrderUrlsToFilter.includes(oasislocation)) filter();
            else if (innerOrderUrlsToCashier.includes(oasislocation)) {
                getOrdersCountThatNeedsCashierAction();
                if (showToast) {
                    InfoToast(waiteraddedneworderInfo);
                    playAudio();
                }
                if (innerOrderUrlsToCashier.includes(actuallocation)) { filter(); }
            }
            break;
        case 2: // take-away
            if (takeAwayOrderUrls.includes(oasislocation)) refresh();
            else if (takeAwayOrderUrlsToFilter.includes(oasislocation)) filter();
            else if (takeAwayOrderUrlsToFilterToCashier.includes(oasislocation)) filter();
            break;
        case 3: // delivary
            if (delivaryOrderUrls.includes(oasislocation)) refresh();
            else if (delivaryOrderUrlsToFilter.includes(oasislocation)) filter();
            else if (delivaryOrderUrlsToCashier.includes(oasislocation)) {
                getOrdersCountThatNeedsCashierAction();
                if (showToast) {
                    InfoToast(delivaryaddedneworderInfo);
                }
                playAudio();
                if (delivaryOrderUrlsToCashier.includes(actuallocation)) { filter(); }
            }
            break;
        default: break;
    }
};

// change order status
orderHub.client.broadcastChangeOrderStatus = function (orderTypeId, statusId) {
    switch (statusId) {
        case 1: // new
            break;
        case 2: // in-kitcken
            if (sendToKitckenUrlstoFilter.includes(oasislocation)) filter();
            else if (sendToKitckenUrlstoFilterCashier.includes(oasislocation)) { // from cashier to othe cashiers
                filter();
                getOrdersCountThatNeedsCashierAction();
            }
            else if (orderTypeId === 1 && sendToKitckenUrlstoFilterWaiter.includes(oasislocation)) filter();
            else if (orderTypeId === 3 && sendToKitckenUrlstoFilterDelivary.includes(oasislocation)) filter();
            break;
        case 4: // on-way-to-client
            if (orderTypeId === 3 && sendToKitckenUrlstoFilterDelivary.includes(oasislocation)) filter();
            break;
        case 5: // finished
            if (finishUrlstoRefresh.includes(oasislocation)) refresh();
            else if (finishUrlstoFilter.includes(oasislocation)) filter();
            else if (finishUrlstoFilterCashier.includes(oasislocation)) { // from cashier to other cashiers
                filter();
                getOrdersCountThatNeedsCashierAction();
            }
            else if (orderTypeId === 1 && finishUrlstoFilterWaiter.includes(oasislocation)) filter();
            else if (orderTypeId === 3 && finishUrlstoFilterDelivary.includes(oasislocation)) filter();
            else if (orderTypeId === 3 && delivaryOrderUrlsToCashier.includes(actuallocation)) filter();
            break;
        default: break;
    }
};



forceLogoutHub.connection.start().done(function () { });
employeeActionsHub.connection.start().done(function () { });
orderHub.connection.start().done(function () { });

// END SIGNALR =========================================================================================================================================



function refresh() { window.location.href = ''; }

function filter() { $('.pagination-filter-form').submit(); }

function getOrdersCountThatNeedsCashierAction() {
    AjaxCall('GET', '/Cashier/GetCashierPendingOrdersCount', null, true, (data) => {
        $('#InnerOrdersNeedsCashierAction').text(data.InnerOrdersNeedsCashierAction);
        $('#TakeAwayOrdersNeedsCashierAction').text(data.TakeAwayOrdersNeedsCashierAction);
        $('#DelivaryOrdersNeedsCashierAction').text(data.DelivaryOrdersNeedsCashierAction);
    });
}

function playAudio() {
    new Audio('/assets/sounds/swiftly.mp3').play();
}
