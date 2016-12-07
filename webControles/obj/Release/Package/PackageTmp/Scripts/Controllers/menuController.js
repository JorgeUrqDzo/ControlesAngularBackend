var MenuController = function () {

    var varmenuService = new MenuService();
    var varPaginaController = new PaginaController();
    var objMsj = new MessageCommon();
    var objMenuIcono = new MenuIconoController();


    Buscar = function (url) {
        $('#TablaLoad').show();
        varmenuService.Buscar(url);
        $('#TablaLoad').hide();
    },

   CambioPagina = function () {

       varPaginaController.Inicializar(function (url) {
           url = url + "&&TextoBuscar=" + $("#txtTextoBuscar").val();
           Buscar(url);
       });

   },

    Editar = function (url, varIdMenu, urlIcono) {
        varmenuService.CargarMenu(url, varIdMenu,
            function (data) {
                $('#divMenu').html(data);
                objMenuIcono.getIconos(urlIcono);
                var IdIcono = $('#txtIdMenuIcono').val();
                var Icono = $('#txtIcono').val();
                $("#selIdMenuIcono").append('<option data-icon="' + Icono + '" value="' + IdIcono + '"></option>');
                $('.selectpicker').selectpicker('refresh');
                HabilitarControles(true);
                $('btnGuardar').show();
                $('btnCancelar').show();
                $('Paginas').show();

            });
    },
   
    


    LimpiarCamposMenu = function () {

        $('#hiddenIdMenu1').val(0);
        $('#selTipoMenu').prop('selectedIndex', 0);
        $('#txtDescripcion').val('');
        $('#txtMenu').val('');
        $('#txtUrl').val('');
        $('#selIdMenuIcono').prop('selectedIndex', 0);
        $('#txtIdMenuPadre').val('');
     

     
    },

    HabilitarControles = function (activar) {
    
        if (activar) {
            $('#selTipoMenu').attr('disabled', false);
            $('#txtDescripcion').attr('disabled', false);
            $('#txtMenu').attr('disabled', false);
            $('#txtUrl').attr('disabled', false);         
            $('#txtIdMenuPadre').attr('disabled', false);
            $('#selIdMenuIcono').attr('disabled', false);
            $("#btnCancelar").removeClass("hide");
            $("#btnGuardar").removeClass("hide");
            $("#btnNuevo").css('display', 'none');

        }
        else {
            $('#selTipoMenu').attr('disabled', true);
            $('#txtDescripcion').attr('disabled', true);
            $('#txtMenu').attr('disabled', true);
            $('#txtUrl').attr('disabled', true);
            $('#txtIdMenuPadre').attr('disabled', true);
            $('#selIdMenuIcono').attr('disabled', true);
            $("#btnCancelar").addClass("hide");
            $("#btnGuardar").addClass("hide");
            $("#btnCancelar").show();
            $("#btnGuardar").show();
            $("#btnNuevo").show();
            
        }

    },


     OcultarBotonesEdicion = function () {

         $('#btnCancelar').css('display', 'none');
         $('#btnGuardar').css('display', 'none');
         $('#btnNuevo').css('display', 'none');


     },
    OcultarBotonesActivar = function () {
        $('#btnActivar').css('display', 'none');
        $('#btnDesactivar').css('display', 'none');
    },

      Cancelar = function () {

          OcultarBotonesEdicion();
          OcultarBotonesActivar();
          $('#btnNuevo').show();
          LimpiarCamposMenu();
          HabilitarControles(false);
         
      },

   Guardar = function (url, objMenuModel, urlBuscar) {
    
       $.validator.unobtrusive.parse($('#formMenu'));
       if (!$("#formMenu").valid()) {
           return false;
       } else {
           $('#TablaLoad').show();
           varmenuService.Guardar(url, objMenuModel, urlBuscar, function (data, urlBuscar) {
               Buscar(urlBuscar);
               if (data.Error) {

                   objMsj.success(data.Mensaje);

               } else
                   objMsj.error(data.Mensaje);
               $('#TablaLoad').hide();
               LimpiarCamposMenu();
               var url2 = "/Menu/Create";
               $("#txtIdMenuPadre").load(url2);
           });
       }
    },



         Activar = function (data) {
             if (data) {
                 $("#btnActivar").html("Activar");
                 $("#txtActivo").prop("checked", false)
             } else {
                 $("#btnActivar").html("Desactivar");
                 $("#txtActivo").prop("checked", true)
             }
         },

    NuevoMenu = function () {
        LimpiarCamposMenu();
        HabilitarControles(true);
        $('#btnCancelar').show();
        $('#btnGuardar').show();
    }

    ActualizarEstado = function (url, objMenuModel, urlBuscar) {
        $('#TablaLoad').show();
        varmenuService.Actualizar(url, objMenuModel, function (data) {
            varmenuService.ActualizarEstado(url, objMenuModel, urlBuscar, function (data, urlBuscar) {
                Buscar(urlBuscar);
                if (data.Error) {
                    objMsj.success(data.Mensaje);
                }
                else
                    objMsj.error(data.Mensaje);
              $('#TablaLoad').hide();
            });
        });
    }

   
    return {

        Buscar: Buscar,
        Editar: Editar,
        CambioPagina: CambioPagina,
        NuevoMenu: NuevoMenu,
        LimpiarCamposMenu: LimpiarCamposMenu,
        HabilitarControles: HabilitarControles,
        OcultarBotonesActivar: OcultarBotonesActivar,
        OcultarBotonesEdicion: OcultarBotonesEdicion,
        Cancelar: Cancelar,
        Guardar: Guardar, 
        ActualizarEstado: ActualizarEstado,
        Activar: Activar,

    }
}