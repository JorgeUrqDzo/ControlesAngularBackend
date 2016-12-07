var EncConfBandejaService = function () {

    var InsertarForm = function (objBandejaModel, url, funSuccess) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objBandejaModel),
            contentType: 'application/json',
            success: function(data) {
                funSuccess(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var validarConsulta = function (objBandejaModel, url, funCallback) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objBandejaModel),
            contentType: 'application/json',
            success: function (data) {
                funCallback(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var CargarXId = function (objBandejaModel,url, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objBandejaModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var CargarNombresColumnas = function (url, objBandejaModel, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objBandejaModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    return {
        InsertarForm: InsertarForm,
        validarConsulta: validarConsulta,
        CargarXId: CargarXId,
        CargarNombresColumnas: CargarNombresColumnas
    }
}