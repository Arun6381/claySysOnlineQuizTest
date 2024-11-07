$(document).ready(function () {
    // Validation functions
    function validateEmail() {
        const email = $('#email').val().trim();
        const errorMessage = $('#email-error');

        if (email === '') {
            errorMessage.text('Email is required.').show();
            $("#email").addClass("invalid");
            return false;
        } else if (!/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(email)) {
            errorMessage.text('Invalid email format.').show();
            $("#email").addClass("invalid");
            return false;
        } else {
            errorMessage.hide();
            $("#email").removeClass("invalid").addClass("valid");
            return true;
        }
    }

    function validatePassword() {
        const password = $('#password').val().trim();
        const errorMessage = $('#password-error');

        if (password === '') {
            errorMessage.text('Password is required.').show();
            $("#password").addClass("invalid");
            return false;
        } else {
            errorMessage.hide();
            $("#password").removeClass("invalid").addClass("valid");
            return true;
        }
    }

    $("#email").on("keyup focusout input", validateEmail);
    $("#password").on("keyup focusout input", validatePassword);

   
    $("#loginbtn").on("click", function (e) {
        e.preventDefault();

        const isEmailValid = validateEmail();
        const isPasswordValid = validatePassword();

        if (!isEmailValid && isPasswordValid) {
          
            $('#email').val('');
            $('#password').val('');
        } else {
            $(this).closest('form').submit();
            $('#email').val('');
            $('#password').val('');
        }
    });
});



