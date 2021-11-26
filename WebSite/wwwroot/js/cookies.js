function createCookie(name, value, days) {
    var expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }
    else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function deleteCookie(name) {
    var value = getCookie(name);

    if (value) {
        createCookie(name, value, -100);
    }
}

function getCookie(name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(name + "=");
        if (c_start != -1) {
            c_start = c_start + name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) {
                c_end = document.cookie.length;
            }
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}

function Calculator() {
    window.location = 'Calculator';
}

function Register() {
    window.location = '/Register';
}

function Logout() {
    deleteCookie('login');
    window.location = '/';
}

function verifyLoggedIn() {
    var baseUrl = getBaseUrl();
    var name = getCookie('login');
    var noLogin = [
        baseUrl,
        baseUrl + '/',
        baseUrl + '/Register'
    ];

    if (!name && !noLogin.includes(window.location.href)) {
        Logout();
    }
}

function getBaseUrl() {
    var getUrl = window.location;
    return getUrl.protocol + "//" + getUrl.host;
}

verifyLoggedIn();
