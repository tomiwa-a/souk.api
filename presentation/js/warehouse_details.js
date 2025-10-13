const urlParams = new URLSearchParams(window.location.search);

$(document).ready(function() {
    const id = urlParams.get('id');

    if (!id) {
        showError('No warehouse ID provided');
        return;
    }

    loadWarehouseDetails(id);
});

function loadWarehouseDetails(id) {
    // Load warehouse
    $.ajax({
        url: `http://localhost:5156/api/warehouses/${id}`,
        method: 'GET',
        success: function(warehouse) {
            $('#loading').hide();
            $('#warehouseDetails').show();
            $('#warehouseId').text(warehouse.id);
            $('#warehouseName').text(warehouse.name);
            $('#warehouseNameDetail').text(warehouse.name);
            $('#warehouseLocation').text(warehouse.location);
            $('#warehouseCapacity').text(warehouse.capacity);
            $('#addProductLink').attr('href', `add_product_to_warehouse.html?id=${warehouse.id}`);

            // Load products in warehouse
            loadProductsInWarehouse(id);

            // Load purchase orders for warehouse
            loadPurchaseOrdersForWarehouse(id);
        },
        error: function(xhr) {
            $('#loading').hide();
            showError('Failed to load warehouse details: ' + xhr.responseText);
        }
    });
}

function loadProductsInWarehouse(warehouseId) {
    $.ajax({
        url: `http://localhost:5156/api/products/warehouse/${warehouseId}`,
        method: 'GET',
        success: function(products) {
            const tbody = $('#productsTableBody');
            tbody.empty();
            products.forEach(product => {
                tbody.append(`
                    <tr>
                        <td>${product.id}</td>
                        <td><a href="product_details.html?id=${product.id}">${product.name}</a></td>
                        <td>${product.quantity}</td>
                    </tr>
                `);
            });
        },
        error: function(xhr) {
            showError('Failed to load products: ' + xhr.responseText);
        }
    });
}

function loadPurchaseOrdersForWarehouse(warehouseId) {
    $.ajax({
        url: `http://localhost:5156/api/purchase-orders?page=1&pageSize=1000`,
        method: 'GET',
        success: function(orders) {
            const filteredOrders = orders.filter(order => order.warehouseId == warehouseId);
            const tbody = $('#ordersTableBody');
            tbody.empty();
            
            filteredOrders.forEach(order => {
                const btnDisabled = order.purchaseStatus == 0 ? "" : "disabled";
                tbody.append(`
                    <tr>
                        <td><a href="purchase_orders.html">${order.id}</a></td>
                        <td>${order.productId}</td>
                        <td>${order.quantity}</td>
                        <td>${new Date(order.orderedAt).toLocaleDateString()}</td>
                        <td>${new Date(order.expectedArrivalDate).toLocaleDateString()}</td>
                        <td><button class="btn btn-success btn-sm ${btnDisabled}" onclick="fulfilOrder(${order.id})">Fulfil</button></td>
                    </tr>
                `);
            });
        },
        error: function(xhr) {
            showError('Failed to load purchase orders: ' + xhr.responseText);
        }
    });
}

function fulfilOrder(orderId) {
    const warehouseId = urlParams.get('id');
    $.ajax({
        url: `http://localhost:5156/api/warehouses/${warehouseId}/purchase-orders/${orderId}/fulfill`,
        method: 'POST',
        success: function() {
            loadPurchaseOrdersForWarehouse(warehouseId);
            loadProductsInWarehouse(warehouseId);
        },
        error: function(xhr) {
            showError('Failed to fulfil order: ' + xhr.responseText);
        }
    });
}

function showError(message) {
    $('#error').text(message).show();
}