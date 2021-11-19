RemindPassword = function () {
    document.getElementById('remindPasswordView').hidden = false;
}

Register = function () {
    var login = document.getElementById('login').value;
    var email = document.getElementById('email').value;
    var password1 = document.getElementById('password1').value;
    var password2 = document.getElementById('password2').value;

    $.ajax({
        type: 'POST',
        url: 'api/register',
        data: { 'login': login, 'password': password1, 'password2': password2, 'email': email },
        dataType: 'json',
        success: function (response) {
            alert("Registration was successful.");
            window.location.href = '/';
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
}

ShowError = function (error) {
    document.getElementById('errorMessage').textContent = error;
}
