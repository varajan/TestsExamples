clear = function () {
    var data = document.getElementsByClassName("data");
    while (data[0])
        data[0].parentNode.removeChild(data[0]);

    $.ajax({ type: 'POST', url: 'api/history/clear', data: { 'login': getCookie('login'), dataType: 'json' } },
    );
}
document.getElementById("clear").addEventListener("click", clear);

show = function () {
    $.ajax({
        type: 'GET',
        url: 'api/history',
        dataType: 'json',
        data: { 'login': getCookie('login') },
        success: function (response) {
            for (ir = 0; ir < response.length; ir++) {
                addRow(response[ir]);
            }
        }
    });
}

addRow = function (row) {
    var tr = document.createElement('tr');
    tr.className = 'data';

    for (i = 0; i < row.length; i++) {
        var td = document.createElement('td');
        td.textContent = row[i];
        tr.appendChild(td);
    }

    document.getElementById('history').appendChild(tr);
}

show();
