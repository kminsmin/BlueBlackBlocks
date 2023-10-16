const PASSWORD = document.getElementById("AccountPassword")
const CONFIRM_PASSWORD = document.getElementById("AccountPasswordConfirm");

function validatePassword() {
    if (PASSWORD.value != CONFIRM_PASSWORD.value) {
        CONFIRM_PASSWORD.setCustomValidity("패스워드가 맞지 않습니다.");
    }
    else { 
        CONFIRM_PASSWORD.setCustomValidity('');
    }
}

function validateAccountName() {
    
}

function init() {
    PASSWORD.addEventListener("change", validatePassword);
    CONFIRM_PASSWORD.addEventListener("keyup", validatePassword);
}

init();