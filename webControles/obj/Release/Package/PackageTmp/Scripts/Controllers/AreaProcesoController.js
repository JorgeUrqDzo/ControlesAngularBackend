var AreaProcesoController = function () {

    var objAreaProcesoService = new AreaProcesoService();
    var varPaginaController = new PaginaController();
    var objMsj = new MessageCommon();

    var CrearAreaProceso = function (url, objAreaProcesoModel, urlBusqueda) {
        
        $.validator.unobtrusive.parse($('#FrmCrear'));
        if (!$("#FrmCrear").valid()) {
            return false;
        } else {
            $('#TablaLoad').show();
            objAreaProcesoService.CrearAreaProceso(url, objAreaProcesoModel,urlBusqueda, function (data, urlBusqueda) {
                ControlesReset();
                Buscar(urlBusqueda);
                if (data.Error){
                    objMsj.success(data.Mensaje);
                }
                else
                    objMsj.error(data.Mensaje);
                $('#TablaLoad').hide();
            });
        }
    }

    var CargarXId = function (url, objAreaProcesoModel) {
        $('#TablaLoad').show();
        objAreaProcesoService.CargarXId(url, objAreaProcesoModel, function (data) {
            ActivarControles(true);
            $("#idCreateAreaProceso").val(data.IdAreaProceso);
            $("#txtCreateAreaProceso").val(data.AreaProceso);
            $("#chkCreateActivo").prop("checked", data.Activo);
            if (data.Activo) {
                $("#btnCreateActivar").html("Desactivar");
            } else {
                $("#btnCreateActivar").html("Activar");
            }
            $('#TablaLoad').hide();
        });
    }

    var ActualizarEstado = function (url, objAreaProcesoModel, urlBusqueda) {
        $('#TablaLoad').show();
        objAreaProcesoService.Actualizar(url, objAreaProcesoModel, function (data) {
            objAreaProcesoService.ActualizarEstado(url, objAreaProcesoModel, urlBusqueda, function (data, urlBusqueda) {
                Buscar(urlBusqueda);
                if (data.Error) {
                    objMsj.success(data.Mensaje);
                }
                else
                    objMsj.error(data.Mensaje);
                $('#TablaLoad').hide();
            });
        });
    }


    var ActivarControles = function (activar) {
        if (activar) {
            $("#idCreateAreaProceso").attr('disabled', false);
            $("#txtCreateAreaProceso").attr('disabled', false);
            $("#chkCreateActivo").attr('disabled', false);

            $("#btnCreateCancelar").removeClass('hide');
            $("#btnCreateGuardar").removeClass('hide');
            $("#btnCreateActivar").removeClass('hide');

            $("#btnCreateCancelar").show();
            $("#btnCreateGuardar").show();
            $("#btnCreateNuevo").css('display', 'none');

        } else {
            $("#idCreateAreaProceso").attr('disabled', true);
            $("#txtCreateAreaProceso").attr('disabled', true);
            $("#chkCreateActivo").attr('disabled', true);

            //$("#btnCreateCancelar").css('display', 'none');
            //$("#btnCreateGuardar").css('display', 'none');
            //$("#btnCreateActivar").css('display', 'none');

            $("#btnCreateCancelar").addClass('hide');
            $("#btnCreateGuardar").addClass('hide');
            $("#btnCreateActivar").addClass('hide');

            $("#btnCreateNuevo").show();
        }
    }

    var ControlesReset = function () {
        $("#idCreateAreaProceso").val("0");
        $("#txtCreateAreaProceso").val("");
        $("#chkCreateActivo").val("");
        ActivarControles(false);
    }

    var Activar = function (data) {
        if (data) {
            $("#btnCreateActivar").html("Activar");
            $("#chkCreateActivo").prop("checked", false)
        } else {
            $("#btnCreateActivar").html("Desactivar");
            $("#chkCreateActivo").prop("checked", true)
        }
    }

    var Buscar = function (url) {
        $('#TablaLoad').show();
        objAreaProcesoService.Buscar(url);
        $('#TablaLoad').hide();
    }

    var CambioPagina = function () {
        varPaginaController.Inicializar(function (url) {
            url = url + '&&TextoBuscar=' + $('#txtTextoBusqueda').val();
            Buscar(url);
        });

    }

    return {
        CrearAreaProceso: CrearAreaProceso,
        CargarXId: CargarXId,
        ActualizarEstado: ActualizarEstado,
        ActivarControles: ActivarControles,
        ControlesReset: ControlesReset,
        Activar: Activar,
        Buscar: Buscar,
        CambioPagina: CambioPagina
    }
}