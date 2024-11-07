$(document).ready(function () {
function validateFirstName() {
    const firstname = $('#firstname').val().trim();
    const errorMessage = $('#firstname-error');
    if (firstname === '') {
        errorMessage.text('First name is required.').show();
        $("#firstname").addClass("invalid");
        return false;
    } else if (!/^[A-Za-z]+$/.test(firstname)) {
        errorMessage.text('First name should contain only letters.').show();
        $("#firstname").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#firstname")
            .removeClass("invalid")
            .addClass("valid");
        return true;
    }
}

function validateLastName() {
    const lastname = $('#lastname').val().trim();
    const errorMessage = $('#lastname-error');
    if (lastname === '') {
        errorMessage.text('Last name is required.').show();
        $("#lastname").addClass("invalid");
        return false;
    } else if (!/^[A-Za-z]+$/.test(lastname)) {
        errorMessage.text('Last name should contain only letters.').show();
        $("#lastname").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#lastname").removeClass("invalid")
            .addClass("valid");

        return true;
    }
}

function validateDOB() {
    const dob = $('#dob').val().trim();
    const errorMessage = $('#dob-error');
    if (dob === '') {
        errorMessage.text('Date of birth is required.').show();
        $("#dob").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#dob").removeClass("invalid")
            .addClass("valid");

        return true;
    }
}

function validateGender() {
    const gender = $('input[name="Gender"]:checked').val();
    const errorMessage = $('#gender-error');
    if (!gender) {
        errorMessage.text('Please select a gender.').show();
        $('#input[name="Gender"]:checked').addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $('#input[name="Gender"]:checked').removeClass("invalid")
            .addClass("valid");

        return true;
    }
}

function validateEducation() {
    const education = $('#education').val().trim();
    const errorMessage = $('#education-error');
    if (education === '') {
        errorMessage.text('Education qualification is required.').show();
        $("#education").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#education").removeClass("invalid")
            .addClass("valid");

        return true;
    }
}

function validatePhone() {
    const phone = $('#phone').val().trim();
    const errorMessage = $('#phone-error');
    if (phone === '') {
        errorMessage.text('Phone number is required.').show();
        $("#phone").addClass("invalid");
        return false;
    } else if (!/^\d{10}$/.test(phone)) {
        errorMessage.text('Phone number should contain 10 digits.').show();
        $("#phone").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#phone").removeClass("invalid")
            .addClass("valid");

        return true;
    }
}

    function validateEmail() {
        const email = $('#email').val().trim();
        const errorMessage = $('#email-error');

        if (email === '') {
            errorMessage.text('Email is required.').show();
            $("#email").addClass("invalid");
            return false;
        } else {
            $.ajax({
                url: '/SignUp/CheckEmail',
                type: 'POST',
                data: { email: email },
                success: function (response) {
                    if (response.isTaken) {
                        errorMessage.text('Email is already taken.').show();
                        $("#email").addClass("invalid");
                    } else {
                        errorMessage.hide();
                        $("#email").removeClass("invalid")
                            .addClass("valid");
                    }
                },
                error: function () {
                    errorMessage.text('Error checking email. Please try again later.').show();
                    $("#email").addClass("invalid");
                }
            });
            return true;
        }
    }
   

function validateAddress() {
    const address = $('#address').val().trim();
    const errorMessage = $('#address-error');
    if (address === '') {
        errorMessage.text('Address is required.').show();
        $("#address").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#address").removeClass("invalid")
            .addClass("valid");

        return true;
    }
}

function validateState() {
    const state = $('#stateDropdown').val();
    const errorMessage = $('#state-error');
    if (state === '') {
        errorMessage.text('Please select a state.').show();
        $("#stateDropdown").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#stateDropdown").removeClass("invalid")
            .addClass("valid");
        return true;
    }
}

function validateCity() {
    const city = $('#cityDropdown').val();
    const errorMessage = $('#city-error');
    if (city === '') {
        errorMessage.text('Please select a city.').show();
        $("#cityDropdown").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#cityDropdown").removeClass("invalid")
            .addClass("valid");
        return true;
    }
}

function validateUsername() {
    const username = $('#username').val().trim();
    const errorMessage = $('#username-error');
    if (username === '') {
        errorMessage.text('Username is required.').show();
        $("#username").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#username").removeClass("invalid")
            .addClass("valid");
        return true;
    }
}

    function validatePassword() {
        const password = $('#password').val().trim();
        const errorMessage = $('#password-error');
        const passwordRegex = /^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/;

        if (password === '') {
            errorMessage.text('Password is required.').show();
            $("#password").addClass("invalid");
            return false;
        } else if (!passwordRegex.test(password)) {
            errorMessage.text('Password must be at least 8 characters long, contain uppercase and lowercase letters, and at least one number or special character.').show();
            $("#password").addClass("invalid");
            return false;
        } else {
            errorMessage.text('').hide();
            $("#password").removeClass("invalid").addClass("valid");
            return true;
        }
    }


function validateConfirmPassword() {
    const password = $('#password').val().trim();
    const confirmPassword = $('#confirm-password').val().trim();
    const errorMessage = $('#confirm-password-error');
    if (confirmPassword === '') {
        errorMessage.text('Please confirm your password.').show();
        $("#confirm-password").addClass("invalid");
        return false;
    } else if (confirmPassword !== password) {
        errorMessage.text('Passwords do not match.').show();
        $("#confirm-password").addClass("invalid");
        return false;
    } else {
        errorMessage.text('').hide();
        $("#confirm-password").removeClass("invalid")
            .addClass("valid");
        return true;
    }
}



    $("#firstname").on("focusout ", validateFirstName);
    $("#firstname").on(" keyup", validateFirstName);
    $("#lastname").on("focusout ", validateLastName);
    $("#lastname").on(" keyup", validateLastName);
    $("input[name='gender']").on("change ", validateGender);
    
    $("#dob").on("focusout ", validateDOB);
    $("#dob").on(" keyup", validateDOB);
    $("#email").on("focusout ", validateEmail);
    $("#email").on("change", validateEmail);
    $("#password").on("focusout ", validatePassword);
    $("#password").on(" keyup", validatePassword);
    $("#confirm-password").on("focusout ", validateConfirmPassword);
    $("#confirm-password").on(" keyup", validateConfirmPassword);
    $("#phone").on("focusout ", validatePhone);
    $("#phone").on(" keyup", validatePhone);
    $("#address").on("focusout ", validateAddress);
    $("#address").on(" keyup", validateAddress);
    $("#stateDropdown").on("focusout ", validateState);
    $("#stateDropdown").on(" keyup", validateState);
    $("#cityDropdown").on("focusout ", validateCity);
    $("#cityDropdown").on(" keyup", validateCity);
    $("#username").on("focusout ", validateUsername);
    $("#username").on(" keyup", validateUsername);
    $("#education").on("focusout ", validateEducation);
    $("#education").on(" keyup", validateEducation);

   
    $("#submitform").on("click", function (e) {
        //e.preventDefault();
        const isFirstNameValid = validateFirstName();
        const isLastNameValid = validateLastName();
        const isEmailValid = validateEmail(); 
        const isPasswordValid = validatePassword();
        const isConfirmPasswordValid = validateConfirmPassword();
        const isPhoneValid = validatePhone();
        const isGenderValid = validateGender();
        const isDOBValid = validateDOB();
        const isAddressValid = validateAddress();
        const isStateValid = validateState();
        const isCityValid = validateCity();
        const isUsernameValid = validateUsername();
        const isEducationValid = validateEducation();
        if (isFirstNameValid && isLastNameValid && isEmailValid && isPasswordValid &&
            isConfirmPasswordValid && isPhoneValid && isGenderValid &&
            isDOBValid && isAddressValid && isStateValid && isCityValid &&
            isUsernameValid && isEducationValid) {
            alert('SignUp successfully completed');
            window.location.href = '/Login/Login';
        } else {
            e.preventDefault();
            alert('signup field')
            $("#submitform").attr("disabled");
        }
    });
});