$(document).ready(function() {
    $('#addPurchaseOrderForm').submit(function(e) {
        e.preventDefault();
        const data = {
            productId: parseInt($('#productId').val()),
            warehouseId: parseInt($('#warehouseId').val()),
            quantity: parseInt($('#quantity').val())
        };
        submitForm('addPurchaseOrderForm', `warehouses/${data.warehouseId}/purchase-orders`, data, function() {
            window.location.href = 'purchase_orders.html';
        });
    });
});