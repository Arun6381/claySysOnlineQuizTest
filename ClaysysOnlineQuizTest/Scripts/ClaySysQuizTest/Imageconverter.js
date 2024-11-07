document.getElementById('imageFile').addEventListener('change', function (event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById('testImage').value = reader.result;
        };
        reader.readAsDataURL(file);
    }
});


//document.getElementById('imageInput').addEventListener('change', function (event) {
//    const file = event.target.files[0];
//    if (file) {
//        const reader = new FileReader();
//        reader.onloadend = function () {
//            document.getElementById('imageBase64').value = reader.result;
//        };
//        reader.readAsDataURL(file);
//    }
//});