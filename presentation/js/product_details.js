$(document).ready(function() {
    const urlParams = new URLSearchParams(window.location.search);
    const id = urlParams.get('id');

    if (!id) {
        showError('No product ID provided');
        return;
    }

    loadProductDetails(id);

    $('#addBtn').click(function() {
        adjustQuantity(id, parseInt($('#adjustmentAmount').val()), 'increase');
    });

    $('#subtractBtn').click(function() {
        adjustQuantity(id, parseInt($('#adjustmentAmount').val()), 'decrease');
    });
});

function loadProductDetails(id) {
    $.ajax({
        url: `http://localhost:5156/api/products/${id}`,
        method: 'GET',
        success: function(product) {
            $('#loading').hide();
            $('#productDetails').show();
            $('#productId').text(product.id);
            $('#productName').text(product.name);
            $('#productNameDetail').text(product.name);
            $('#productDescription').text(product.description);
            $('#productQuantity').text(product.quantity);
            $('#productReorderThreshold').text(product.reorderThreshold);
            $('#productSupplierId').text(product.supplierId);
            $('#productWarehouseId').text(product.warehouseId);
            if (product.warehouseId) {
                $('#backToWarehouseBtn').attr('href', `warehouse_details.html?id=${product.warehouseId}`).show();
            }
        },
        error: function(xhr) {
            $('#loading').hide();
            showError('Failed to load product details: ' + xhr.responseText);
        }
    });
}

function adjustQuantity(id, amount, action) {
    if (amount <= 0) return;

    
    $.ajax({
        url: `http://localhost:5156/api/products/${id}`,
        method: 'GET',
        success: function(product) {
            const endpoint = `http://localhost:5156/api/warehouses/${product.warehouseId}/products/${id}/${action}?quantity=${amount}`;
            $.ajax({
                url: endpoint,
                method: 'PUT',
                contentType: 'application/json',
                success: function() {
                    
                    const currentQty = parseInt($('#productQuantity').text());
                    const newQty = action === 'increase' ? currentQty + amount : currentQty - amount;
                    if (newQty >= 0) {
                        $('#productQuantity').text(newQty);
                    }
                },
                error: function(xhr) {
                    showError('Failed to adjust quantity: ' + xhr.responseText);
                }
            });
        },
        error: function(xhr) {
            showError('Failed to get product for adjustment: ' + xhr.responseText);
        }
    });
}

function showError(message) {
    $('#error').text(message).show();
}