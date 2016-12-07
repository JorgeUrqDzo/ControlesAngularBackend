var BandejaColumnaService = function () {
    Guardar = function (action, objModBandejaColumna, funSucces) {
        $.ajax({
            async: true,
            url: action,
            type: "POST",
            datatype: 'json',
            data: objModBandejaColumna,
            success: function (data) {
                funSucces(data);
            },
            error: function (errorThrown) {
                funSucces(null);
            }

        });
    },

    Cargar = function (action, funSucces) {
        $.get(action, null, function (data) {
            funSucces(data);
        });
    },
    Eliminar = function (action, objModBandejaColumna, funSucces) {
        $.ajax({
            async: true,
            url: action,
            type: "POST",
            dataType: 'json',
            data: objModBandejaColumna,
            success: function (data) {
                funSucces(data);
            },
            error: function (errorThrown) {
                funSucces(null);
            }
        });
    }

    var CargarLista = function (url, id) {
        $.ajax({
            async: true,
            url: url + "/" + id,
            type: "GET",
            //dataType: 'json',
            //data: JSON.stringify(id),
            //contentType: 'application/json',
            success: function (data) {
                //console.log(data);
                $("#btnBuscar").click();
                //Ocultar Loading
                $('#TablaLoad').hide();
            },
            error: function (errorThrown) {
            }
        });
    }

    var cambiarOrden = function (url, objBandejaColumna, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objBandejaColumna),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrown) {
                funCallBack(errorThrown);
            }
        });
    }

    return {
        Guardar: Guardar,
        Cargar: Cargar,
        Eliminar: Eliminar,
        CargarLista: CargarLista,
        cambiarOrden: cambiarOrden

    }

}