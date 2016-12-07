var MenuIconoService = function () {

    var getIconos = function (url, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    return {
     
        getIconos: getIconos

    }
}
