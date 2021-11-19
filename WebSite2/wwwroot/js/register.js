RemindPassword = function () {
    document.getElementById('remindPasswordView').hidden = false;
}

Register = function () {
    var login = document.getElementById('login').value;
    var email = document.getElementById('email').value;
    var password1 = document.getElementById('password1').value;
    var password2 = document.getElementById('password2').value;

    if (password1 !== password2) {
        ShowError("Passwords are different!");
        return;
    }

    $.ajax({
        type: 'POST',
        // url: '@Url.Action("Register", "Register")',
        data: { 'login': login, 'password': password1, 'email': email },
        dataType: 'json',
        success: function (response) {
            alert("Registration was successful.");
            // window.location.href = window.location = '@Url.Action("Index", "Login")';
        },
        error: function (jqXHR, textStatus, errorThrown) {
            ShowError(errorThrown);
        }
    });
}

ShowError = function (error) {
    document.getElementById('errorMessage').textContent = error;
}
