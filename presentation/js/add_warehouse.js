$(document).ready(function() {
    $('#addWarehouseForm').submit(function(e) {
        e.preventDefault();
        const data = {
            name: $('#name').val(),
            location: $('#location').val(),
            capacity: parseInt($('#capacity').val())
        };
        submitForm('addWarehouseForm', 'warehouses', data, function() {
            window.location.href = 'warehouses.html';
        });
    });
});