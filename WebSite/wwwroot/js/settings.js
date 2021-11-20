var dateFormat = document.getElementById('dateFormat');
var numberFormat = document.getElementById('numberFormat');
var currency = document.getElementById('currency');

function Save() {
    $.ajax({
        type: 'POST',
        url: 'api/settings/save',
        data: {
            'login': getCookie('login'),
            'dateFormat': dateFormat.options[dateFormat.selectedIndex].textContent,
            'numberFormat': numberFormat.options[numberFormat.selectedIndex].textContent,
            'currency': currency.options[currency.selectedIndex].textContent
        },

        dataType: 'json',
        success: function (response) {
            alert('Changes are saved!');
        }
    });
}

function Select(dropdown, value) {
    for (var i = 0; i < dropdown.options.length; i++) {
        if (dropdown.options[i].text === value) {
            dropdown.selectedIndex = i;
            break;
        }
    }
}

function Get() {
    $.ajax({
        type: 'POST',
        url: 'api/settings',
        dataType: 'json',
        data: {
            'login': getCookie('login')
        },
        success: function (response) {
            Select(dateFormat, response.DateFormat);
            Select(numberFormat, response.NumberFormat);
            Select(currency, response.Currency);
        }
    });
}

Get();
