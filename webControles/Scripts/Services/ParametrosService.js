var ParametrosService = function () {

    var insertarParametro = function (url, objParametrosModel, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objParametrosModel),
            contentType: 'application/json',
            success: function (data) {
                //alert(data);
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var actualizarParametro = function (url, objParametrosModel) {
        $.ajax({
            async: false,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objParametrosModel),
            contentType: 'application/json',
            success: function (data) {
                //alert(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var eliminarParametro = function (url, objParametrosModel, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objParametrosModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var cargarParametroXId = function (url, objParametrosModel, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objParametrosModel),
            contentType: 'application/json',
            success: function (data) {
                //alert(data);
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var CargarParametrosXIdEncConfBandeja = function (url, id, funCallBack) {
        obj = { IdEncConfBandeja: id };
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(obj),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {
                funCallBack(errorThrow);
            }
        });
    }
    return {
        Insertar: insertarParametro,
        Actualizar: actualizarParametro,
        Eliminar: eliminarParametro,
        CargarXId: cargarParametroXId,
        CargarParametrosXIdEncConfBandeja: CargarParametrosXIdEncConfBandeja
    }
}