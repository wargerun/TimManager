let inputPassword = document.getElementById("InputPassword");
let checkboxShowPassword = document.getElementById("InputCheckboxShowPassword");

function showPassword() {
    if (checkboxShowPassword.checked) {
        inputPassword.type = "text";
    }
    else {
        inputPassword.type = "password";
    }
}