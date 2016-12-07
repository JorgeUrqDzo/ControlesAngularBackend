var TipoAccion = "Agregar";
var BandejaFiltros = function () {
    var objService = new BandejaFiltrosService();

    Guardar = function (action) {
        var objModBandejaFiltros = $('#formCreate').serialize();
        $.validator.unobtrusive.parse($('#formCreate'));
        if($("#formCreate").valid())
            objService.Guardar(action, objModBandejaFiltros, function (data) {
                if (data["Id"].length != 0)
                {
                    $("#ModalAltaFiltros").modal('toggle');
                    location.reload();
                }
            });
    },
    Editar = function (action, id, tabla) {
        objService.Cargar(action, function (data) {
            if (data["IdDentConfBandejaFiltros"] !== "undefined")
            {
                var bool = false;
                if (TipoAccion == "Eliminar") bool = true;
                BF.cssShadow(TipoAccion + " filtro", TipoAccion, bool);
                cargarBandejaColumna(data);
            }
        });
    },
    Eliminar = function (action) {
        var objModBandejaFiltros = $('#formCreate').serialize();
        objService.Eliminar(action, objModBandejaFiltros, function (data) {
            if (data["Id"].length != 0){
                $("#ModalAltaFiltros").modal('toggle');
                location.reload();
            }
        });
    },

    cargarBandejaColumna = function (data) {
        $("#IdDentConfBandejaFiltros").val(data["IdDentConfBandejaFiltros"]);
        $("#Filtro").val(data["Filtro"]);
        $("#IdTipoControl").val(data["IdTipoControl"]);
    },
    cssShadow = function (titulo, texto, Activar) {
        $('#tituloBandejaModal').text(titulo);
        $("#btnBandejaFiltroModalAceptar").text(texto);
        if (texto != "Agregar") $("#ModalAltaFiltros").modal('toggle');
        $(".activado").attr('readonly', Activar);
    },
    VaciarCampos = function () {
        $("#Filtro").val("");
        $("#IdTipoControl").val("1");
    }
    return {
        Guardar: Guardar,
        Editar: Editar,
        Eliminar: Eliminar,
        cargarBandejaColumna: cargarBandejaColumna,
        cssShadow: cssShadow,
        VaciarCampos: VaciarCampos,
        CambioPagina: CambioPagina
    }
}

