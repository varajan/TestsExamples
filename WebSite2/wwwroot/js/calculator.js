VerifyNumber = function (id, min, max) {
    var value = Number(document.getElementById(id).value);

    if (isNaN(value) || value < min || value > max) {
        document.getElementById(id).value = "0";
    }

    SetCalculateButtonState();
}

VerifyTerm = function (id, min) {
    var max = document.querySelector('#finYear input').checked ? 365 : 360;
    var value = Number(document.getElementById(id).value);

    if (!Number.isInteger(value) || isNaN(value) || value < min || value > max) {
        document.getElementById(id).value = "0";
    }

    SetCalculateButtonState();
}

Date.prototype.yyyymmdd = function () {
    var mm = this.getMonth() + 1;
    var dd = this.getDate();
    return [(dd > 9 ? '' : '0') + dd, (mm > 9 ? '' : '0') + mm, this.getFullYear()].join('/');
};

CalculateDate = function () {
    var day = document.getElementById('day').selectedIndex + 1;
    var month = document.getElementById('month').selectedIndex;
    var year = document.getElementById('year').value;
    var days = Number(document.getElementById('term').value);

    $.ajax({
        type: 'GET',
        // url: '@Url.Action("Date", "Settings")',
        data: { 'date': new Date(year, month, day + days).yyyymmdd(), 'login': getCookie('login') },
        dataType: 'json',
        success: function (response) {
            document.getElementById('endDate').value = response;
        }
    });
}

async function SetNumber(id, number) {
    $.ajax({
        type: 'GET',
        // url: '@Url.Action("Number", "Settings")',
        data: { 'number': number, 'login': getCookie('login') },
        dataType: 'json',
        success: function (response) {
            document.getElementById(id).value = response;
            return Promise.resolve(response);
        }
    }).then(function () { return Promise.resolve('done'); });
}

ResetMonth = function () {
    var day = ++document.getElementById('day').selectedIndex;
    var month = document.getElementById('month').selectedIndex;
    var leapYear = document.getElementById('year').value % 4 === 0;

    switch (month) {
        case 0:
        case 2:
        case 4:
        case 6:
        case 7:
        case 9:
        case 11:
            AddOptions('day', 1, 31);
            SetDay(day);
            break;
        case 3:
        case 5:
        case 8:
        case 10:
            AddOptions('day', 1, 30);
            SetDay(day < 31 ? day : 30);
            break;
        default:
            AddOptions('day', 1, leapYear ? 29 : 28);
            SetDay(day < 29 ? day : 28);
            break;
    }
}

SetCurrentDate = function () {
    var date = new Date();

    AddOptions('day', 1, 31);
    AddOptions('year', 2010, 2025);

    document.getElementById('day').value = date.getDate();
    document.getElementById('month').selectedIndex = date.getMonth();
    document.getElementById('year').value = date.getFullYear();
}

SetDay = function (day) {
    document.getElementById('day').value = day;
}

AddOptions = function (id, min, max) {
    var select = document.getElementById(id);
    for (var i = select.options.length - 1; i >= 0; i--) {
        select.remove(i);
    }

    for (var i = min; i <= max; i++) {
        var opt = document.createElement('option');
        opt.value = i;
        opt.innerHTML = i;
        select.appendChild(opt);
    }
}

async function Save() {
    await new Promise((resolve, reject) => setTimeout(resolve, 500));

    var day = document.getElementById('day').selectedIndex + 1;
    var month = document.getElementById('month').selectedIndex;
    var year = document.getElementById('year').value;

    var amount = Number(document.getElementById('amount').value);
    var days = Number(document.getElementById('term').value);
    var percent = Number(document.getElementById('percent').value);
    var finYear = document.querySelector('#finYear input').checked ? 365 : 360;
    var interest = document.getElementById('interest').value;
    var income = document.getElementById('income').value;
    var startDate = new Date(year, month, day).yyyymmdd();
    var endDate = new Date(year, month, day + days).yyyymmdd();

    $.ajax({
        type: 'POST',
        // url: '@Url.Action("Save", "History")',
        data: {
            'login': getCookie('login'),
            'amount': amount,
            'percent': percent,
            'interest': interest,
            'days': days,
            'startDate': startDate,
            'endDate': endDate,
            'year': finYear,
            'income': income
        },
        dataType: 'json',
        success: function (response) {
            document.getElementById('calculateBtn').disabled = false;
        }
    });
}

function SetCalculateButtonState() {
    var amount = Number(document.getElementById('amount').value);
    var days = Number(document.getElementById('term').value);
    var percent = Number(document.getElementById('percent').value);

    document.getElementById('calculateBtn').disabled = (amount == 0 || days == 0 || percent == 0);
}

async function Calculate() {
    document.getElementById('calculateBtn').disabled = true;

    var year = document.querySelector('#finYear input').checked ? 365 : 360;
    var amount = Number(document.getElementById('amount').value);
    var days = Number(document.getElementById('term').value);
    var percent = Number(document.getElementById('percent').value);
    var interest = amount * percent * days / (year * 100);
    var income = amount + interest;

    CalculateDate();

    await SetNumber('interest', interest);
    await SetNumber('income', income);

    return Promise.resolve('done');
}

SetYear = function (year) {
    if (year == 365) {
        document.querySelector('#finYear td:nth-child(2) input').checked = false;
    } else {
        document.querySelector('#finYear td:nth-child(3) input').checked = false;
    }
}

SetCurrency = function () {
    $.ajax({
        type: 'GET',
        // url: '@Url.Action("Currency", "Settings")',
        dataType: 'json',
        data: { 'login': getCookie('login') },
        success: function (response) {
            document.getElementById('currency').textContent = response;
        }
    });
}

SetCalculateButtonState();
SetCurrency();
SetCurrentDate();
ResetMonth();
Calculate();
