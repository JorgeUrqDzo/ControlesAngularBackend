var ActividadService = function () {

    Buscar = function (url) {

        $.get(url, function (result) {
            $('#divLista').html(result);
            nzSwitch.SetNzSwitch();
        });
    },

        CargarActividad = function (UrlCreate, varIdActividad, funActividad) {
        
            var url = UrlCreate + "/" + varIdActividad;

            $.get(url, function (result) {
                funActividad(result);
              
            });
        },
  

   Guardar = function (url, objActividadModel, urlBuscar, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objActividadModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data, urlBuscar);
            },
            error: function (errorThrow) {

            }
        });
    },

    Activar = function (url,objActividadModel, urlBuscar, funCallBack) {
      
        $.ajax({
            async: true,
            url: url,
            dataType: 'json',
            data: JSON.stringify(objActividadModel),
            type: "POST",
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data, urlBuscar);

            },
            error: function (errorThrow) {
                //Mensaje de error
            }
        });


    },
   Actualizar = function (url, objActividadModel, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objActividadModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    },

    ActualizarEstado = function (url, objActividadModel, urlBuscar, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objActividadModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data, urlBuscar);
            },
            error: function (errorThrow) {

            }
        });
    }


    return {
        Buscar: Buscar,
        CargarActividad: CargarActividad,
        Guardar: Guardar,
        Activar: Activar,
        ActualizarEstado: ActualizarEstado,
        Actualizar: Actualizar

    }
}
