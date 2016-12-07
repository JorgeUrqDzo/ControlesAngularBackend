var SeccionControlService = function () {

    var Guardar = function (varUrl, objSeccionControlViewModel, funSuccess) {
        //var objViewModel = {
        //    objSeccionControlViewModel: objSeccionControlViewModel
        //};

        $.ajax({
            async: true,
            url: varUrl,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objSeccionControlViewModel),
            contentType: 'application/json',
            success: function (data) {
                funSuccess(data);
            },
            error: function (errorThrow) {

            }
        });
    },
    Cargar = function (Url, funSuccess) {
        $.getJSON(Url, function (result) {
            funSuccess(result);
        });
    }

    var cargarControles = function (Url, tablaSeleccionada, funCallBack) {

        //$.get(Url+"/"+tablaSeleccionada,function(data) {
        //    funCallBack(data);
        //})

        var arr = [];
        arr.push(tablaSeleccionada);

        $.ajax({
            async: true,
            url: Url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(arr),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }


    return {
        Guardar: Guardar,
        Cargar: Cargar,
        cargarControles:cargarControles
    }

}