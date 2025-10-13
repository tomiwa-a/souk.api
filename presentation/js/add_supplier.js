$(document).ready(function() {
    $('#addSupplierForm').submit(function(e) {
        e.preventDefault();
        const data = {
            name: $('#name').val(),
            emailAddress: $('#email').val()
        };
        submitForm('addSupplierForm', 'suppliers', data, function() {
            window.location.href = 'suppliers.html';
        });
    });
});