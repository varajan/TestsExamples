function hide() {
    window.frameElement.hidden = true;
}

function send() {
    let email = document.getElementById('email').value;

    $.ajax({
        type: 'POST',
        url: 'api/login/remind',
        data: { 'email': email },
        dataType: 'json',
        success: function (response) {
            hide();
            alert(response);
        },
        error: function (error) {
            document.getElementById('message').hidden = false;
            document.getElementById('message').textContent = error.responseText;
        }
    });
}
