var TipoAccion = "Agregar";
var BandejaColumnaController = function () {
    var objService = new BandejaColumnaService();
    var objBusquedaController = new BusquedaController();

    Guardar = function (action, urlBusqueda) {
        //var objModBandejaColumna = $('#formCreate').serialize();

        var objModBandejaColumna = {
            IdDetConfBandejaColumna: $("#hdfIdDetConfBandejaColumna").val(),
            IdEncConfBandeja: $("#varIdEncConfBandeja").val(),
            TituloColumna: $("#createBandejaTituloColumna").val(),
            ColumnaBD: $("#createBandejaColumnaSelect").val(),
            IdTipoColumna: $("#createBandejaColumnaIdTipoColumna").val(),
            Clase: $("#createBandejaColumnaClase").val(),
            Formato: $("#createBandejaColumnaFormato").val(),
            //TipoLink: $("#createBandejaTipoLink").val(),
            TipoLink: $("#createBandejaTipoLink").is(":checked"),
            PaginaLink: $("#createBandejaColumnaPaginaLink").val(),
            IdUsuarioCreacion: 1,
            IdUsuarioModificacion: 1,
            OrdenColumna: parseInt($("#hdfNumRegistros").val()) + 1
    };

        $.validator.unobtrusive.parse($('#formCreate'));

        if ($("#formCreate").valid()) {

            objService.Guardar(action,
                objModBandejaColumna,
                function (data) {
                    //Mostrar Loading
                    $('#TablaLoad').show();

                    if (data["Id"].length != 0) {
                        $("#ModalAltaColumna").modal('toggle');
                    }

                    Cargar(urlBusqueda + '/' + $("#varIdEncConfBandeja").val());
                    //Ocultar Loading
                    $('#TablaLoad').hide();
                });
        }
    };

    Editar = function (action, id, tabla) {
        //Mostrar Loading
        $('#TablaLoad').show();

        var url = action + '/' + id;

        objService.Cargar(url,
            function (data) {
                //if (data["IdDetConfBandejaColumna"] !== "undefined") {

                //    var bool = false;
                //    if (TipoAccion == "Eliminar") bool = true;
                //    BC.cssShadow(TipoAccion + " columna", TipoAccion, bool);
                //    cargarBandejaColumna(data);

                //}
                var status = data.TipoLink;

                $("#hdfIdDetConfBandejaColumna").val(data.IdDetConfBandejaColumna);
                $("#varIdEncConfBandeja").val(data.IdEncConfBandeja);
                $("#createBandejaTituloColumna").val(data.TituloColumna);
                $("#createBandejaColumnaSelect").val(data.ColumnaBD);
                $("#createBandejaColumnaIdTipoColumna").val(data.IdTipoColumna);
                $("#createBandejaColumnaClase").val(data.Clase);
                $("#createBandejaColumnaFormato").val(data.Formato);
                $("#createBandejaColumnaPaginaLink").val(data.PaginaLink);

                $("#createBandejaTipoLink").val(status);
                if (status) {
                    $("#createBandejaTipoLink").prop("checked", status);
                    $("#createBandejaTipoLink").bootstrapSwitch("state", status);
                }
                else {
                    $("#createBandejaTipoLink").prop("checked", status);
                    $("#createBandejaTipoLink").bootstrapSwitch("state", status);

                }
                //nzSwitch.SetNzSwitch();

                //Ocultar Loading
                $('#TablaLoad').hide();
            });
    };

    Eliminar = function (action, id, url) {
        //Mostrar Loading
        $('#TablaLoad').show();

        //var objModBandejaColumna = $('#formCreate').serialize();
        var objModBandejaColumna = {
            IdDetConfBandejaColumna: id
        };

        objService.Eliminar(action,
            objModBandejaColumna,
            function (data) {
                //if (data["Id"].length != 0) {
                //$("#ModalAltaColumna").modal('toggle');
                //location.reload();
                //}
                Cargar(url + '/' + $("#varIdEncConfBandeja").val());

                //Ocultar Loading
                $('#TablaLoad').hide();

            });
    };

    cargarBandejaColumna = function (data) {
        $("#TituloColumna").val(data.TituloColumna);
        $("#ColumnaBD").val(data["ColumnaBD"]);
        $("#IdTipoColumna").val(data.IdTipoColumna);
        $("#Clase").val(data.Clase);
        $("#PaginaLink").val(data.PaginaLink);
        $("#Formato").val(data.Formato);
        $("#IdDetConfBandejaColumna").val(data.IdDetConfBandejaColumna);
    };

    cssShadow = function (titulo, texto, Activar) {
        $('#tituloBandejaModal').text(titulo);
        $("#btnBandejaFiltroModalAceptar").text(texto);
        if (texto != "Agregar") $("#ModalAltaColumna").modal('toggle');
        $(".activado").attr('readonly', Activar);
    };

    VaciarCampos = function () {
        $("#TituloColumna").val("");
        $("#ColumnaBD").val("");
        $("#IdTipoColumna").val('1');
        $("#Clase").val("");
        $("#PaginaLink").val("");
        $("#Formato").val("");
        $("#IdDetConfBandejaColumna").val("");
    };

    Cargar = function (url) {
        //Mostrar Loading
        $('#TablaLoad').show();

        objService.Cargar(url,
            function (data) {
                $('#divListaBandeja').html(data);

                //Agrega propiedad DragDrop
                //$('#TableBandejas > tbody').sortable();
                //Mostrar Loading
                $('#TablaLoad').hide();

            });
    }



    var CargarLista = function (url, id) {
        //Mostrar Loading
        $('#TablaLoad').show();

        objService.CargarLista(url, id);

        ////Ocultar Loading
        //$('#TablaLoad').hide();
    }

    var cambiarOrden = function (url, id, ordenNuevo, urlBusqueda) {
        var objBandejaColumnaModel = {
            IdDetConfBandejaColumna: parseInt(id),
            OrdenColumna: ordenNuevo
        }
        objService.cambiarOrden(url, objBandejaColumnaModel, function (data) {
            console.log(data);
            Cargar(urlBusqueda + '/' + $("#varIdEncConfBandeja").val());
        });
    }

    return {
        Guardar: Guardar,
        Editar: Editar,
        Eliminar: Eliminar,
        cargarBandejaColumna: cargarBandejaColumna,
        cssShadow: cssShadow,
        VaciarCampos: VaciarCampos,
        Cargar: Cargar,
        CargarLista: CargarLista,
        cambiarOrden: cambiarOrden
    }
}