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

function select(dropdown, value) {
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
            select(dateFormat, response.DateFormat);
            select(numberFormat, response.NumberFormat);
            select(currency, response.Currency);
        }
    });
}

function sleep(time) {
    return new Promise((resolve) => setTimeout(resolve, time));
}

function show(id) { document.getElementById(id).style.display = "initial"; }
function hide(id) { document.getElementById(id).style.display = "none"; }

Get();
hide('save');
hide('cancel');

sleep(500).then(() => {
    show('save');
    sleep(500).then(() => show('cancel'));
});
