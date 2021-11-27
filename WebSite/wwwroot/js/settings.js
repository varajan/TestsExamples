function Save() {
    $.ajax({
        type: 'POST',
        url: 'api/settings/save',
        contentType: 'application/json',
        data: JSON.stringify({
            'login': getCookie('login'),
            'dateFormat': document.getElementById('dateFormat').selectedIndex,
            'numberFormat': document.getElementById('numberFormat').selectedIndex,
            'currency': document.getElementById('currency').selectedIndex
        }),
        success: function (response) {
            alert('Changes are saved!');
            window.location = 'Calculator';
        }
    });
}

function Cancel() {
    window.location = 'Calculator';
}

function Get() {
    $.ajax({
        type: 'POST',
        url: 'api/settings',
        contentType: 'application/json',
        data: JSON.stringify({
            'login': getCookie('login')
        }),
        success: function (response) {
            SetDropdownValuesFromValues('dateFormat',   response.dateFormat);
            SetDropdownValuesFromValues('numberFormat', response.numberFormat);
            SetDropdownValuesFromValues('currency',     response.currency);
        }
    });
}

Get();
