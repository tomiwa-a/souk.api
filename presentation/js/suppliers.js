$(document).ready(function() {
    loadList('suppliers', 'suppliersList', function(s) {
        return `<li class="list-group-item">${s.name} - ${s.emailAddress}</li>`;
    });
});