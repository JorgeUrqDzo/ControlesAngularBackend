var AreaProcesoService = function () {

    var CrearAreaProceso = function (url, objAreaProcesoModel, urlBusqueda, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objAreaProcesoModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data, urlBusqueda);
            },
            error: function (errorThrow) {

            }
        });
    }


    var CargarXId = function (url, objAreaProcesoModel, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objAreaProcesoModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var Actualizar = function (url, objAreaProcesoModel, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objAreaProcesoModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var ActualizarEstado = function (url, objAreaProcesoModel, urlBusqueda, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objAreaProcesoModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data, urlBusqueda);
            },
            error: function (errorThrow) {

            }
        });
    }

    var Buscar2 = function (url) {
        $.get(url, function (result) {
            $('#divCreate').html(result);
        });
    }
 
    var Buscar = function (url) {
        $.get(url, function (result) {
            $('#divLista').html(result);
            nzSwitch.SetNzSwitch();
        });
    }

    return {
        CrearAreaProceso: CrearAreaProceso,
        CargarXId: CargarXId,
        Actualizar: Actualizar,
        ActualizarEstado: ActualizarEstado,
        Buscar: Buscar
    };
}