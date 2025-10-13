const apiBase = 'http://localhost:5156/api';

function showError(message) {
    $('#error').text(message).show();
}

function hideError() {
    $('#error').hide();
}

function showLoading() {
    $('#loading').show();
}

function hideLoading() {
    $('#loading').hide();
}

// Utility to load data into a list
function loadList(endpoint, containerId, itemTemplate) {
    showLoading();
    $.get(`${apiBase}/${endpoint}?page=1&pageSize=100`, function(data) {
        const items = data.map(itemTemplate).join('');
        $(`#${containerId}`).html(items);
        hideLoading();
    }).fail(function(xhr) {
        showError('Failed to load data: ' + xhr.responseText);
        hideLoading();
    });
}

// Utility to submit form
function submitForm(formId, endpoint, data, successCallback) {
    hideError();
    showLoading();
    $.ajax({
        url: `${apiBase}/${endpoint}`,
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: successCallback,
        error: function(xhr) {
            showError('Error: ' + (xhr.responseJSON?.message || xhr.responseText));
            hideLoading();
        }
    });
}