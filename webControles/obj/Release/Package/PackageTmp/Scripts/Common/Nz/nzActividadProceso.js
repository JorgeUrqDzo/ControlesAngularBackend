var ActividadProceso = function () {

    var getActividades = function (url, funCallBack) {
        $.ajax({
            async: true,
            url: "http://localhost:64632/api/actividadproceso",
            type: "GET",
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

        getActividades: getActividades

    }
}