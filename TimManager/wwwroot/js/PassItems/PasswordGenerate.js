let symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

function getRandomValue(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

function passwordGenerate() {
    let password = document.getElementById("InputPassword");
    let length = Number(password.value);

    if (isNaN(length) || length <= 0) {
        length = 15; // default value
    }

    password.value = "";

    for (var i = 0; i < length; i++) {
        password.value += symbols.charAt(getRandomValue(0, symbols.length))
    }
}