$(document).ready(function() {
    loadCounts();
});

function loadCounts() {
    // Fetch and count warehouses
    $.ajax({
        url: 'http://localhost:5156/api/warehouses?page=1&pageSize=1000',
        method: 'GET',
        success: function(data) {
            $('#warehouseCount').text(data.length);
        },
        error: function(xhr) {
            $('#warehouseCount').text('Error');
            showError('Failed to load warehouse count: ' + xhr.responseText);
        }
    });

    // Fetch and count products
    $.ajax({
        url: 'http://localhost:5156/api/products?page=1&pageSize=1000',
        method: 'GET',
        success: function(data) {
            $('#productCount').text(data.length);
        },
        error: function(xhr) {
            $('#productCount').text('Error');
            showError('Failed to load product count: ' + xhr.responseText);
        }
    });

    // Fetch and count suppliers
    $.ajax({
        url: 'http://localhost:5156/api/suppliers?page=1&pageSize=1000',
        method: 'GET',
        success: function(data) {
            $('#supplierCount').text(data.length);
        },
        error: function(xhr) {
            $('#supplierCount').text('Error');
            showError('Failed to load supplier count: ' + xhr.responseText);
        }
    });

    // Fetch and count purchase orders
    $.ajax({
        url: 'http://localhost:5156/api/purchase-orders?page=1&pageSize=1000',
        method: 'GET',
        success: function(data) {
            $('#orderCount').text(data.length);
        },
        error: function(xhr) {
            $('#orderCount').text('Error');
            showError('Failed to load order count: ' + xhr.responseText);
        }
    });
}

function showError(message) {
    $('#error').text(message).show();
}