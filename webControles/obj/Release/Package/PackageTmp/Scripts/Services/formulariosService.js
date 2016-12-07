var FormulariosService = function () {
    var webcontroles = "/Controles"

    //Función para guardar Formulario mediante Ajax
    Guardar = function(varUrl, objFormularioViewModel, funSuccess){
        $.ajax({
            async: false,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objFormularioViewModel),
            contentType: 'application/json',

            success: function (data) {
                funSuccess(data);
            },
            error: function (errorThrow) {

            }
        });
    },

    ObtenerUUID = function(url,varId){

        //$.get(url, function (result) {
        //})
        $.ajax({
            type: "GET",
            url: url,
            data: "varId=" + varId,

            success: function (data) {
                $('#ObtenerUUID').html(data);
                $('#ObtenerUUIDModal').modal('show', "width", 50);
            },

            error: function (errorThrow) {
            }
        });

    },

    ObtenerUUIDForm = function (url, varId, funcCallback) {

        $.ajax({
            type: "GET",
            url: url,
            data: "varId=" + varId,

            success: function (data) {
                funcCallback(data);
            },

            error: function (errorThrow) {
            }
        });

    },

    //Actualizar Formularios
    Actualizar = function (url, objFormularioModel) {
        $.ajax({
            url: url,
            type: "POST",
            datatype: 'json',
            data: JSON.stringify(objFormularioModel),
            contentType: 'application/json',

            success: function (data) {
            },
            error: function (errorThrow) {

            }
        });
    },


    //Busca todos los formularios
    Buscar = function (url) {

        $.get(url, function(result) {
            $('#divListado').html(result);
        });

    },



    //ObtenerUUID = function (varId) {

    //    $.get(varId, function (result) {
    //        $('')
    //    })
    //}




    //Función para guardar formulario mediante Ajax
    GuardarFormulario = function (objFormulario, url, funSuccess) {

            

        var objFormularioModel = {
            objFormularioModel: objFormulario
        };


        $.ajax({
            async: false,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objFormularioModel),
            contentType: 'application/json',

            success: function (data) {
                funSuccess(data);
            },
            statusCode: {
                404: function (content) { alert('cannot find resource'); },
                500: function (content) { alert('internal server error'); }
            },
            error: function (errorThrow) {

            }
        });
    },
    //Funcion que carga la vista del formulario
    CargarFormulario = function (UrlCreate, varIdFormulario, funFormulario) {
        var url = UrlCreate + "/" + varIdFormulario;
        $.get(url, function (result) {
            funFormulario(result);
        });
    }

    getIdTipoFormulario = function(url, objFormulario,funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            datatype: 'json',
            data: JSON.stringify(objFormulario),
            contentType: 'application/json',

            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {
                console.log(errorThrow);
            }
        });
    }

    var seccionesDeFormulario = function(url, objFormulario, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            datatype: 'json',
            data: JSON.stringify(objFormulario),
            contentType: 'application/json',

            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {
                console.log(errorThrow);
            }
        });
    }
    
    var llenarColumnasPadreEHijo = function (url, objGetColumnName, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objGetColumnName),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {
                console.log(errorThrow);
            }
        });
    }

    return {
        GuardarFormulario: GuardarFormulario,
        Guardar: Guardar,
        CargarFormulario: CargarFormulario,
        Buscar: Buscar,
        Actualizar: Actualizar,
        ObtenerUUID: ObtenerUUID,
        ObtenerUUIDForm: ObtenerUUIDForm,
        getIdTipoFormulario: getIdTipoFormulario,
        seccionesDeFormulario: seccionesDeFormulario,
        llenarColumnasPadreEHijo: llenarColumnasPadreEHijo,

    }
}