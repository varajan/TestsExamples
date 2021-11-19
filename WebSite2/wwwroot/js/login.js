RemindPassword = function () {
  document.getElementById('remindPasswordView').hidden = false;
}

Login = function () {
  var login = document.getElementById('login').value;
  var password = document.getElementById('password').value;

  $.ajax({
    type: 'POST',
    url: 'api/login/validate',
    data: { 'login': login, 'password': password },
    success: function (response) {
      createCookie("login", login, 1);
      window.location.href = window.location = 'Calculator';
    },
    error: function () {
      ShowError("Incorrect user name or password!");
    }
  });
}

ShowError = function (error) {
  document.getElementById('errorMessage').textContent = error;
}
