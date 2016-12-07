var RelSeccionesService = function () {

    var cargarRelaciones = function (url, objModRelSecciones, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: "json",
            data: JSON.stringify(objModRelSecciones),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrown) {
            }
        });
    }

    var guardarRelacion = function (url, objRelSecciones, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objRelSecciones),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {
                funCallBack(errorThrow);
            }
        });
    }

    var eliminarRelacion = function(url, objRelSecciones, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objRelSecciones),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {
                funCallBack(errorThrow);
            }
        });
    }

    
    var cambiarOrden = function(url, objRelSecciones, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objRelSecciones),
            contentType: 'application/json',
            success: function(data) {
                funCallBack(data);
            },
            error: function(errorThrown) {
                funCallBack(errorThrown);
            }
        });
    }

    return {
        cargarRelaciones: cargarRelaciones,
        guardarRelacion: guardarRelacion,
        eliminarRelacion: eliminarRelacion,
        cambiarOrden:cambiarOrden
    }
}