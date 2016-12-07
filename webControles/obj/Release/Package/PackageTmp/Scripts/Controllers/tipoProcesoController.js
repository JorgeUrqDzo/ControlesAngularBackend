var TipoProcesoController = function () {

    var vartipoProcesoService = new TipoProcesoService();
    var varPaginaController = new PaginaController();
    var objMsj = new MessageCommon();

    
    Buscar = function (url) {
        $('#TablaLoad').show();
        vartipoProcesoService.Buscar(url);
        $('#TablaLoad').hide();
    },

   CambioPagina = function () {
       
       varPaginaController.Inicializar(function (url) {
           url = url + "&&TextoBuscar=" + $("#txtTextoBuscar").val();
           Buscar(url);
       });

   },

    Editar = function (url, varIdProceso) {
        vartipoProcesoService.CargarProceso(url, varIdProceso,
            function (result) {
                $('#divProceso').html(result);
                HabilitarControles(true);
                $('btnGuardar').show();
                $('btnCancelar').show();
               

            });
    },

    LimpiarCamposProceso= function () {

        $('#hiddenIdTipoProceso1').val(0);
        $('#txtIdAreaProceso').prop('selectedIndex', 0);
        $('#txtTipoProceso').val('');       
    },

    HabilitarControles = function (activar) {

        if (activar) {
            $('#txtIdAreaProceso').attr('disabled', false);
            $('#txtTipoProceso').attr('disabled', false);
            $("#btnCancelar").removeClass("hide");
            $("#btnGuardar").removeClass("hide");
            $("#btnNuevo").css('display', 'none');
          
        }
        else {
            $('#txtIdAreaProceso').attr('disabled', true);
            $('#txtTipoProceso').attr('disabled', true);
            $("#btnCancelar").addClass("hide");
            $("#btnGuardar").addClass("hide");
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
          HabilitarControles(false);
          LimpiarCamposProceso();
      },
 
   Guardar = function (url, objTipoProcesoModel, urlBuscar) {
      
       $.validator.unobtrusive.parse($('#formTipoProceso'));
       if (!$("#formTipoProceso").valid()) {
           return false;
       } else {
           $('#TablaLoad').show();
           vartipoProcesoService.Guardar(url, objTipoProcesoModel, urlBuscar, function(data, urlBuscar) {
               Buscar(urlBuscar);
               if (data.Error) {
                    
                   objMsj.success(data.Mensaje);
                 
               } else
                   objMsj.error(data.Mensaje);
               $('#TablaLoad').hide();
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

    NuevoProceso = function () {
        LimpiarCamposProceso();
        HabilitarControles(true);

     
        $('#btnCancelar').show();
        $('#btnGuardar').show();
    },

        ActualizarEstado = function (url, objTipoProcesoModel, urlBuscar) {
        $('#TablaLoad').show();
        vartipoProcesoService.Actualizar(url, objTipoProcesoModel, function (data) {
            vartipoProcesoService.ActualizarEstado(url, objTipoProcesoModel, urlBuscar, function (data, urlBuscar) {
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
        NuevoProceso: NuevoProceso,
        LimpiarCamposProceso: LimpiarCamposProceso,
        HabilitarControles: HabilitarControles,
        OcultarBotonesActivar: OcultarBotonesActivar,
        OcultarBotonesEdicion: OcultarBotonesEdicion,
        Cancelar: Cancelar,
        Guardar: Guardar,
        Activar: Activar,
        ActualizarEstado: ActualizarEstado

    }



}