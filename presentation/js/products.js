$(document).ready(function() {
    loadList('products', 'productsList', function(p) {
        return `<li class="list-group-item"><a href="product_details.html?id=${p.id}">${p.name} - ${p.description} (Qty: ${p.quantity})</a></li>`;
    });
});