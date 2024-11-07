let timeLimit = 600;
const timerElement = document.getElementById('time');
const timerInterval = setInterval(() => {
    const minutes = Math.floor(timeLimit / 60);
    const seconds = timeLimit % 60;

    timerElement.innerText = `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`;

    if (timeLimit <= 0) {
        clearInterval(timerInterval);
        document.getElementById("quizForm").submit();
    }

    timeLimit--;
}, 1000);

let currentQuestionIndex = 0;
const totalQuestions = @Model.Count();

document.getElementById("nextButton").addEventListener("click", function () {
    const currentQuestion = document.getElementById(`question_${currentQuestionIndex}`);
    if (currentQuestionIndex < totalQuestions - 1) {
        currentQuestion.style.display = "none";
        currentQuestionIndex++;
        document.getElementById(`question_${currentQuestionIndex}`).style.display = "block";

        if (currentQuestionIndex === totalQuestions - 1) {
            document.getElementById("nextButton").style.display = "none";
            document.getElementById("submitButton").style.display = "inline";
        }
        document.getElementById("prevButton").style.display = "inline";
    }
});

document.getElementById("prevButton").addEventListener("click", function () {
    const currentQuestion = document.getElementById(`question_${currentQuestionIndex}`);
    if (currentQuestionIndex > 0) {
        currentQuestion.style.display = "none";
        currentQuestionIndex--;
        document.getElementById(`question_${currentQuestionIndex}`).style.display = "block";

        if (currentQuestionIndex === 0) {
            document.getElementById("prevButton").style.display = "none";
        }
        document.getElementById("nextButton").style.display = "inline";
        document.getElementById("submitButton").style.display = "none";
    }
});

function validateAllQuestionsAnswered() {
    for (let i = 0; i < totalQuestions; i++) {
        const options = document.getElementsByName(`answers[${i}].UserAnswer`);
        let answered = false;
        for (const option of options) {
            if (option.checked) {
                answered = true;
                break;
            }
        }
        if (!answered) {
            alert("Please answer all questions before submitting.");
            return false;
        }
    }
    return true;
}
function clearRadioButtons() {
    const radios = document.querySelectorAll('input[type="radio"]');
    radios.forEach(radio => radio.checked = false);
}

document.getElementById("submitButton").addEventListener("click", function (event) {
    event.preventDefault();

    if (validateAllQuestionsAnswered()) {
        document.getElementById("quizForm").submit();

        setTimeout(() => {
            clearRadioButtons();
        }, 2000);
    }
});
