var dateFormat = document.getElementById('dateFormat');
var numberFormat = document.getElementById('numberFormat');
var currency = document.getElementById('currency');

function Save() {
    $.ajax({
        type: 'POST',
        url: 'api/settings/save',
        contentType: 'application/json',
        data: JSON.stringify({
            'login': getCookie('login'),
            'dateFormat': dateFormat.options[dateFormat.selectedIndex].textContent,
            'numberFormat': numberFormat.options[numberFormat.selectedIndex].textContent,
            'currency': currency.options[currency.selectedIndex].textContent
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
        dataType: 'application/json',
        contentType: 'application/json',
        data: JSON.stringify({
            'login': getCookie('login')
        }),
        success: function (response) {
            Select(dateFormat, response.DateFormat);
            Select(numberFormat, response.NumberFormat);
            Select(currency, response.Currency);
        }
    });
}

function SetDropdownValues(id) {
    $.ajax({
        type: 'GET',
        url: 'api/settings/values',
        dataType: 'json',
        data: { 'name': id },
        success: function (response) {
            var dropdown = document.getElementById(id);

            for (const val of response) {
                var option = document.createElement("option");
                option.value = val;
                option.text = val;
                dropdown.appendChild(option);
            }
        }
    });
}

SetDropdownValues("dateFormat");
SetDropdownValues("numberFormat");
SetDropdownValues("currency");

Get();
