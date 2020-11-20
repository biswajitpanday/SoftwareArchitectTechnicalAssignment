
var fileUpload = (function () {
    var data = {
        formData: null
    }

    var methods = {
        init: function() {
            data.formData = new FormData();
        },

        uploadFiles: function() {
            $.ajax({
                url: "/api/FileUploadApi/UploadFile",
                data: data.formData,
                processData: false,
                contentType: false,
                type: "POST",
                success: function (data) {

                }
            });
        },

        getSelectedFile: function(e) {
            var input = document.getElementById("file");
            var files = input.files;
            data.formData.append("file", files[0]);
        }
    }
    methods.init();

    return methods;
})();