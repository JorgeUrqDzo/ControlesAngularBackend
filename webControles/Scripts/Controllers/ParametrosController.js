var ParametrosController = function () {

    var objParametrosService = new ParametrosService();
    var msj = new MessageCommon();

    var insertarParametro = function (url, objDatosParametros, urlParametros, idEncConfP) {
        //console.log(objDatosParametros);

        $.validator.unobtrusive.parse($('#frmParametros'));
        if (!$("#frmParametros").valid()) {
            return false;
        } else {
            objParametrosService.Insertar(url, objDatosParametros, function (data) {
                $("#ModalAltaParametros").modal('toggle');
                $("#IdDetConfBandejaLinkParametro").val("0"),
                $("#txtParametroNombre").val(""),
                $("#txtParametroValor").val(""),
                $("#txtParametroTipo").val(1),
                $("#txtParametroTipoEnvio").val(1)
                msj.success("Parametros Agregados exitosamente");
                //location.reload();
                CargarParametrosXIdEncConfBandeja(urlParametros, idEncConfP);
            });
            
        };
    }


    var actualizarParametro = function () {

    }

    var eliminarParametro = function (url, objDatosParametros, urlParametros, idEncConfP) {
        objParametrosService.Eliminar(url, objDatosParametros, function (data) {
            msj.success("Se ha eliminado el parametro correctamente");
            //location.reload();
            CargarParametrosXIdEncConfBandeja(urlParametros, idEncConfP);
        });
    }

    var cargarParametroXId = function (url, objDatosParametros) {
        objParametrosService.CargarXId(url, objDatosParametros, function (data) {
            $("#ModalAltaParametros").modal('toggle');
            $("#IdDetConfBandejaLinkParametro").val(data.IdDetConfBandejaLinkParametro),
            $("#txtParametroNombre").val(data.Parametro),
            $("#txtParametroValor").val(data.Valor),
            $("#txtParametroTipo").val(data.IdTipoParametro),
            $("#txtParametroTipoEnvio").val(data.IdTipoEnvioParametro)
        });
    }

    var CargarParametrosXIdEncConfBandeja = function(url, id) {
        objParametrosService.CargarParametrosXIdEncConfBandeja(url,
            id,
            function (data) {
                var resul = data.lstParametrosModel;
                $("#tablaParametros").html(" ");
                for (var i in resul) {
                    var pos = resul[i];
                    pintarTablaParametros(pos["IdDetConfBandejaLinkParametro"], pos["Parametro"], pos["Valor"], pos["TipoParametro"], pos["IdEncConfBandeja"]);
                }
            });
    }

    function pintarTablaParametros(IdDetConfBandejaLinkParametro, Parametro, Valor, IdTipoParametro, IdEncConfBandeja) {
        var row = '<tr data-id="'+IdDetConfBandejaLinkParametro+'">' +
            '<td>'+Parametro+'</td>' +
            '<td>'+Valor+'</td>' +
            '<td>'+IdTipoParametro+'</td>' +
            '<td>' +
            '<input type="hidden" value="'+IdEncConfBandeja+' id = "IdEncConfBandeja"/>' +
            '<input type="button" id="btnEditarParametro" value="Editar" class="btn btn-success" />' +
            '<input type="button" id="btnEliminarParametro" value="Eliminar" class="btn btn-danger" />' +
            '</td>' +
            '</tr>';
        $("#tablaParametros").append(row);
    }

    return {
        Insertar: insertarParametro,
        Actualizar: actualizarParametro,
        Eliminar: eliminarParametro,
        CargarXId: cargarParametroXId,
        CargarParametrosXIdEncConfBandeja: CargarParametrosXIdEncConfBandeja
    }
}