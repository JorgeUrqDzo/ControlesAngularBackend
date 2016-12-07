var BandejaFiltrosService = function () {
    Guardar = function (action, objModBandejaFiltros, funSucces) {
        $.ajax({
            async: true,
            url: action,
            type: "POST",
            datatype: 'json',
            data: objModBandejaFiltros,
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
    Eliminar = function (action, objModBandejaFiltros, funSucces) {
        $.ajax({
            async: true,
            url: action,
            type: "POST",
            dataType: 'json',
            data: objModBandejaFiltros,
            success: function (data) {
                funSucces(data);
            },
            error: function (errorThrown) {
                funSucces(null);
            }
        });
    }
    return {
        Guardar: Guardar,
        Cargar: Cargar,
        Eliminar: Eliminar
    }

}