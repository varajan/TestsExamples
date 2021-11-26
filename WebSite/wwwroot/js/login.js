RemindPassword = function () {
  document.getElementById('remindPasswordView').hidden = false;
}

Login = function () {
  var login = document.getElementById('login').value;
  var password = document.getElementById('password').value;

  $.ajax({
    type: 'POST',
      url: 'api/login/validate',
      contentType: 'application/json',
      data: JSON.stringify({ 'login': login, 'password': password }),
    success: function (response) {
        createCookie("login", login, 1);
        Calculator();
    },
      error: function (error) {
          ShowError(error.responseText);
    }
  });
}

ShowError = function (error) {
  document.getElementById('errorMessage').textContent = error;
}
