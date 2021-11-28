clear = function () {
    var data = document.getElementsByClassName('data-td');
    while (data[0])
        data[0].parentNode.removeChild(data[0]);

    $.ajax({ type: 'POST', url: 'api/history/clear', data: { 'login': getCookie('login') } } );
}
document.getElementById("clear").addEventListener("click", clear);

show = function () {
    $.ajax({
        type: 'GET',
        url: 'api/history',
        dataType: 'json',
        data: { 'login': getCookie('login') },
        success: function (response) {
            addRow(response[0], 'th');

            for (ir = 1; ir < response.length; ir++) {
                addRow(response[ir], 'td');
            }
        }
    });
}

addRow = function (row, type) {
    var tr = document.createElement('tr');
    tr.className = 'data-' + type;

    for (i = 0; i < row.length; i++) {
        var cell = document.createElement(type);
        cell.textContent = row[i];
        tr.appendChild(cell);
    }

    document.getElementById('history').appendChild(tr);
}

show();
