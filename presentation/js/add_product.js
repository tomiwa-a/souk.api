$(document).ready(function() {
    // Load suppliers
    $.get(`${apiBase}/suppliers?page=1&pageSize=100`, function(data) {
        const options = data.map(s => `<option value="${s.id}">${s.name}</option>`).join('');
        $('#supplierId').append(options);
    });

    // Load warehouses
    $.get(`${apiBase}/warehouses?page=1&pageSize=100`, function(data) {
        const options = data.map(w => `<option value="${w.id}">${w.name}</option>`).join('');
        $('#warehouseId').append(options);
    });

    $('#addProductForm').submit(function(e) {
        e.preventDefault();
        const data = {
            name: $('#name').val(),
            description: $('#description').val(),
            quantity: parseInt($('#quantity').val()),
            reorderThreshold: parseInt($('#reorderThreshold').val()),
            supplierId: parseInt($('#supplierId').val()),
            warehouseId: parseInt($('#warehouseId').val())
        };
        submitForm('addProductForm', `warehouses/${data.warehouseId}/products`, data, function() {
            window.location.href = 'products.html';
        });
    });
});