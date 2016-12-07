var SeccionService = function () {
    var webcontroles = "/Controles"
    //Función para guardar sección mediante Ajax
    GuardarSeccion = function (objSeccion, url, funSuccess) {
        //var objSeccionesModel = {
        //    objSeccionesModel: objSeccion
        //};
        $.ajax({
            async: false,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objSeccion),
            contentType: 'application/json',
            success: function (data) {
                funSuccess(data);
            },
            error: function (errorThrow) {

            }
        });
    },

    GuardarGrupo = function (action, objNuevoGrupo, funSuccess) {
        $.ajax({
            type: "POST",
            url: action,
            datatype: 'json',
            data: objNuevoGrupo,
            success: function (data) {
                funSuccess(data);
            },
            error: function (errorThrown) {

            }
        });

    },

    CargarGrupos = function (intIdFormulario, funResultado) {

        $.get("/Secciones/ObtenerGrupos", {IdFormulario: intIdFormulario},function(data){
            funResultado(data);
        })

    },


    CargarSeccion = function (UrlCreate, varIdSeccion, funSeccion) {
        var url = UrlCreate + "/" + varIdSeccion;
        $.get(url, function (result) {
            funSeccion(result);

        });
    },

    ActualizarOrden = function (action, objGrupo, funSuccess) {
        $.ajax({
            type: "POST",
            url: action,
            datatype: 'json',
            data: objGrupo,
            success: function (data) {
                funSuccess(data);
            },
            error: function (errorThrown) {

            }
        });

    }

    var getDBTablesName = function(url, funSuccess) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            success: function (data) {
                funSuccess(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    var getTableColumnName = function( url,objGetColumnName, funSuccess) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objGetColumnName),
            contentType: 'application/json',
            success: function (data) {
                funSuccess(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    return {
        GuardarSeccion: GuardarSeccion,
        CargarSeccion: CargarSeccion,
        CargarGrupos: CargarGrupos,
        GuardarGrupo: GuardarGrupo,
        ActualizarOrden: ActualizarOrden,
        getDBTablesName: getDBTablesName,
        getTableColumnName: getTableColumnName,
    }
}