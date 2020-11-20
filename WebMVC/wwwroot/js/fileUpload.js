var fileUpload = (function () {
    var data = {
        formData: null
    }

    var methods = {
        init: function () {
            data.formData = new FormData();
            $("#success").hide();
            $("#error").hide();
        },

        uploadFiles: function () {
            $.ajax({
                url: "/api/FileUploadApi/UploadFile",
                data: data.formData,
                processData: false,
                contentType: false,
                type: "POST",
                success: function (data) {
                    $("#success").show();
                    $("#success").text("File Uploaded Successfully");
                },
                error: function (xhrRequest, textStatus, err) {
                    $("#error").show();
                    $("#error").text(xhrRequest.responseText || "Failed Uploading file");
                }
            });
        },

        getSelectedFile: function (e) {
            var input = document.getElementById("file");
            var files = input.files;
            data.formData.append("file", files[0]);
        }
    }
    methods.init();

    return methods;
})();