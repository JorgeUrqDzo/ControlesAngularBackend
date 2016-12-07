var ConfigurarActividadService = function () {


    var Buscar = function (url) {
        $.get(url, function (result) {
            $('#divLista').html(result);
            nzSwitch.SetNzSwitch();
        });
    }

    var CargarXId = function (url, datos, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(datos),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var ActividadXTipo = function(url, funCallBack) {
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

    var Guardar = function (url,datos, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(datos),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    return {
        Buscar: Buscar,
        CargarXId: CargarXId,
        ActividadXTipo: ActividadXTipo,
        Guardar:Guardar,

    }
}