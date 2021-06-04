

$(document).ready(function () {
     var file = document.getElementById("customFileImage").files[0];
     if (file != null) {
         var readImg = new FileReader();
         readImg.readAsDataURL(file);
         readImg.onload = function (e) {
             $('#previewImageDiv').show();
             $('#previewImage').attr('src', e.target.result).fadeIn();
         }
     }
     else {
         $('#previewImageDiv').hide();
     }



     $("#customFileImage").mouseout(function () {
            var file = document.getElementById("customFileImage").files[0];
         if (file != null) {
             var readImg = new FileReader();
             readImg.readAsDataURL(file);
                readImg.onload = function (e) {
        $('#previewImageDiv').show();
    $('#previewImage').attr('src', e.target.result).fadeIn();
}
}
else {
        $('#previewImageDiv').hide();
}
     });
 });



//$(document).ready(function () {
//    $("#customFileImage").mouseout(function () {
//        var file = document.getElementById("customFileImage").files[0];
//        if (file != null) {
//            var readImg = new FileReader();
//            readImg.readAsDataURL(file);
//            $('#previewImageDiv').show();
//            $('#previewImage').attr('src', e.target.result).fadeIn();
//        }
//        else {
//            $('#previewImageDiv').hide();
//        }
//    });

//    $("#submit").click(function () {
//        var file = document.getElementById("customFileImage").files[0];
//        if (file.readImg != null)
//            $("#productImageValidation").text("The Product Image is required field.");
//    });
//});