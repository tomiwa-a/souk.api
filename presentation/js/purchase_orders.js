$(document).ready(function() {
    loadList('purchase-orders', 'purchaseOrdersList', function(po) {
        return `<li class="list-group-item">Order ${po.id} - Product ${po.productId}, Qty: ${po.quantity}</li>`;
    });
});