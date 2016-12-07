
var ActividadController = function () {

    var varactividadService = new ActividadService();
    var varPaginaController = new PaginaController();
    var objMsj = new MessageCommon();

    Buscar = function (url) {
        $('#TablaLoad').show();
        varactividadService.Buscar(url);
        $('#TablaLoad').hide();
    },

   CambioPagina = function () {
       
       varPaginaController.Inicializar(function (url) {
           url = url + "&&TextoBuscar=" + $("#txtTextoBuscar").val();
           Buscar(url);
       });

   },

    Editar = function (url, varIdActividad) {
        varactividadService.CargarActividad(url, varIdActividad,
            function (result) {
                $('#divActividad').html(result);
                HabilitarControles(true);
                $('btnGuardar').show();
                $('btnCancelar').show();
               

            });
    },

 
    LimpiarCamposActividad= function () {

       
        $('#hiddenIdActividad1').val(0);
        $('#selTipoProceso').prop('selectedIndex', 0);
        $('#txtOrden').val('');
        $('#txtActividad').val('');       
        $('#txtUrlActividad').val('');
        $('#txtUrlDestino').val('');
        $('#txtTiempoPromedio').val('');

    },

    HabilitarControles = function (activar) {

        if (activar) {
            $('#selTipoProceso').attr('disabled', false);
            $('#txtOrden').attr('disabled', false);
            $('#txtActividad').attr('disabled', false);
            $('#txtUrlActividad').attr('disabled', false);
            $('#txtUrlDestino').attr('disabled', false);
            $('#txtTiempoPromedio').attr('disabled', false);
            $("#btnCancelar").removeClass("hide");
            $("#btnGuardar").removeClass("hide");
            $("#btnNuevo").css('display', 'none');
           

        }
        else {
            $('#selTipoProceso').attr('disabled', true);
            $('#txtOrden').attr('disabled', true);
            $('#txtActividad').attr('disabled', true);
            $('#txtUrlActividad').attr('disabled', true);
            $('#txtUrlDestino').attr('disabled', true);
            $('#txtTiempoPromedio').attr('disabled', true);
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
          LimpiarCamposActividad();
      },
 
   Guardar = function (url, objActividadModel, urlBuscar) {
      
        $.validator.unobtrusive.parse($('#formActividad'));
        if (!$("#formActividad").valid()) {
            return false;
        } else {
            $('#TablaLoad').show();
  
            varactividadService.Guardar(url, objActividadModel, urlBuscar, function(data, urlBuscar) {
                Buscar(urlBuscar);
                if (data.Error) {
                    
                    objMsj.success(data.Mensaje);
                 
                } else
                    objMsj.error(data.Mensaje);

                $('#TablaLoad').hide();
                LimpiarCamposActividad();
            });
        }
   },

    Activar = function (data) {
        if (data) {
            $("#btnActivar").html("Activar");
            $("#txtActivo").prop("checked", false);
        } else {
            $("#btnActivar").html("Desactivar");
            $("#txtActivo").prop("checked", true);
        }
    },

    NuevaActividad = function () {
        LimpiarCamposActividad();
        HabilitarControles(true);

     
        $('#btnCancelar').show();
        $('#btnGuardar').show();
    },
     ActualizarEstado = function (url, objActividadModel, urlBuscar) {
        $('#TablaLoad').show();
        varactividadService.Actualizar(url, objActividadModel, function (data) {
            varactividadService.ActualizarEstado(url, objActividadModel, urlBuscar, function (data, urlBuscar) {
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
        NuevaActividad: NuevaActividad,
        LimpiarCamposActividad: LimpiarCamposActividad,
        HabilitarControles: HabilitarControles,
        OcultarBotonesActivar: OcultarBotonesActivar,
        OcultarBotonesEdicion: OcultarBotonesEdicion,
        Cancelar: Cancelar,
        Guardar:Guardar, 
        Activar: Activar,
        ActualizarEstado: ActualizarEstado
    }
}