var MenuService = function () {

    Buscar = function (url) {

        $.get(url, function (result) {
            $('#divLista').html(result);
            nzSwitch.SetNzSwitch();


        });
    },

   CargarMenu = function (UrlCreate, datos, funMenu) {

            var url = UrlCreate + "/" + datos;

            $.get(url, function (data) {
                funMenu(data)
            });
   },

     Cargar = function (action, funSucces) {
         $.get(action, null, function (data) {
             funSucces(data);
         });
     },
   Guardar = function (url, objMenuModel, urlBuscar, funCallBack) {
       $.ajax({
           async: true,
           url: url,
           type: "POST",
           dataType: 'json',
           data: JSON.stringify(objMenuModel),
           contentType: 'application/json',
           success: function (data) {
               funCallBack(data, urlBuscar);
           },
           error: function (errorThrow) {

           }
       });
   },
    Activar = function (url, objMenuModel, urlBuscar, funCallBack) {

        $.ajax({
            async: true,
            url: url,
            dataType: 'json',
            data: JSON.stringify(objMenuModel),
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
   Actualizar = function (url, objMenuModel, funCallBack) {
       $.ajax({
           async: true,
           url: url,
           type: "POST",
           dataType: 'json',
           data: JSON.stringify(objMenuModel),
           contentType: 'application/json',
           success: function (data) {
               funCallBack(data);
           },
           error: function (errorThrow) {

           }
       });
   },

    ActualizarEstado = function (url, objMenuModel, urlBuscar, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objMenuModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data, urlBuscar);
            },
            error: function (errorThrow) {

            }
        });
    }, 
    CargarXId = function (url, objeto, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objeto),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    },
    CargarIconos = function (url, objMenuModel, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objMenuModel),
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
        CargarMenu: CargarMenu,
        Guardar: Guardar,
        Actualizar: Actualizar,
        ActualizarEstado: ActualizarEstado,
        CargarIconos: CargarIconos,
        Cargar : Cargar

    }
}
