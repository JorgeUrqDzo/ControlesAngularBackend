var TipoProcesoService = function () {

    Buscar = function (url) {

        $.get(url, function (result) {
            $('#divLista').html(result);
            nzSwitch.SetNzSwitch();
        });
    },

        CargarProceso = function (UrlCreate, varIdProceso, funProceso) {

            var url = UrlCreate + "/" + varIdProceso;

            $.get(url, function (result) {
                funProceso(result);

            });
        },

   Guardar = function (url, objTipoProcesoModel, urlBuscar, funCallBack) {
       $.ajax({
           async: true,
           url: url,
           type: "POST",
           dataType: 'json',
           data: JSON.stringify(objTipoProcesoModel),
           contentType: 'application/json',
           success: function (data) {
               funCallBack(data, urlBuscar);
           },
           error: function (errorThrow) {

           }
       });
   },

    Activar = function (url, objTipoProcesoModel, urlBuscar, funCallBack) {

        $.ajax({
            async: true,
            url: url,
            dataType: 'json',
            data: JSON.stringify(objTipoProcesoModel),
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
   Actualizar = function (url, objTipoProcesoModel, funCallBack) {
       $.ajax({
           async: true,
           url: url,
           type: "POST",
           dataType: 'json',
           data: JSON.stringify(objTipoProcesoModel),
           contentType: 'application/json',
           success: function (data) {
               funCallBack(data);
           },
           error: function (errorThrow) {

           }
       });
   },

    ActualizarEstado = function (url, objTipoProcesoModel, urlBuscar, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objTipoProcesoModel),
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
        CargarProceso: CargarProceso,
        Guardar: Guardar,
        Actualizar: Actualizar,
        ActualizarEstado: ActualizarEstado

    }
}
