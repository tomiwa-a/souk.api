$(document).ready(function() {
    const urlParams = new URLSearchParams(window.location.search);
    const warehouseId = urlParams.get('id');

    if (!warehouseId) {
        showError('No warehouse ID provided');
        return;
    }

    loadWarehouse(warehouseId);
    loadSuppliers();

    $('#addProductForm').submit(function(e) {
        e.preventDefault();
        addProduct(warehouseId);
    });
});

function loadWarehouse(id) {
    $.ajax({
        url: `http://localhost:5156/api/warehouses/${id}`,
        method: 'GET',
        success: function(warehouse) {
            $('#warehouseName').text(warehouse.name);
            $('#warehouseDisplay').val(warehouse.name);
            $('#warehouseId').val(warehouse.id);
            $('#cancelLink').attr('href', `warehouse_details.html?id=${warehouse.id}`);
        },
        error: function(xhr) {
            showError('Failed to load warehouse: ' + xhr.responseText);
        }
    });
}

function loadSuppliers() {
    $.ajax({
        url: 'http://localhost:5156/api/suppliers?page=1&pageSize=1000',
        method: 'GET',
        success: function(suppliers) {
            const select = $('#supplierId');
            suppliers.forEach(supplier => {
                select.append(`<option value="${supplier.id}">${supplier.name}</option>`);
            });
        },
        error: function(xhr) {
            showError('Failed to load suppliers: ' + xhr.responseText);
        }
    });
}

function addProduct(warehouseId) {
    const data = {
        name: $('#name').val(),
        description: $('#description').val(),
        quantity: parseInt($('#quantity').val()),
        reorderThreshold: parseInt($('#reorderThreshold').val()),
        supplierId: parseInt($('#supplierId').val()),
        warehouseId: parseInt(warehouseId)
    };

    if (!data.name || !data.description || isNaN(data.quantity) || isNaN(data.reorderThreshold) || isNaN(data.supplierId)) {
        showError('Please fill all fields correctly.');
        return;
    }

    $('#loading').show();
    $('#error').hide();

    $.ajax({
        url: `http://localhost:5156/api/warehouses/${warehouseId}/products`,
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function() {
            $('#loading').hide();
            window.location.href = `warehouse_details.html?id=${warehouseId}`;
        },
        error: function(xhr) {
            $('#loading').hide();
            showError('Failed to add product: ' + xhr.responseText);
        }
    });
}

function showError(message) {
    $('#error').text(message).show();
}