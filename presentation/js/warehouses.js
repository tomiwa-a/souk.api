$(document).ready(function() {
    loadList('warehouses', 'warehousesList', function(w) {
        return `<li class="list-group-item"><a href="warehouse_details.html?id=${w.id}">${w.name} - ${w.location} (Capacity: ${w.capacity})</a></li>`;
    });
});