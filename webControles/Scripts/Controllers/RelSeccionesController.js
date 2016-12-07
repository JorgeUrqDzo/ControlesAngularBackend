var RelSeccionesController = function () {
    var objRelSeccionesService = new RelSeccionesService();
    var msj = new MessageCommon();
    var cargarRelaciones = function (url, obj) {
        //Mostrar Loading
        $('#TablaLoad').show();
        var idFromulario = $("#hdfIdFormularioGeneral").val();
        var objModRelSecciones = {
            IdFormulario : idFromulario
        }
        objRelSeccionesService.cargarRelaciones(url, objModRelSecciones,
            function (data) {
                var relTotal = data.length;
                $("#hdfTotalRelaciones").val(relTotal);
                llenarTablaRelaciones(data, true, obj);
                //Ocultar Loading
                $('#TablaLoad').hide();
            });

    }

    var guardarRelacion = function (url, obj) {


        var tablaPadre = $("#relTablaPadre").val();
        var tablaHijo = $("#relTablaHijo").val();
        var keyPadre = $("#relColumnaPadrePK").val();
        var keyHijo = $("#relColumnaHijoFK").val();
        var idFromulario = $("#hdfIdFormularioGeneral").val();
        var objRelSecciones = {
            IdSeccionPadre: tablaPadre,
            IdSeccionHijo: tablaHijo,
            KeyPadre: keyPadre,
            KeyHijo: keyHijo,
            IdFormulario: idFromulario
        };

        if (tablaPadre == "0" || tablaHijo == "0" || keyPadre == "0" || keyHijo == "0") {
            msj.error("Debe completar los campos para establecer la relacion");
            resetModalAgregarRelacion();
        } else if (tablaPadre == tablaHijo) {
            msj.error("No puedes crear un relacion a la misma tabla");
            resetModalAgregarRelacion();
        } else {
            //Mostrar Loading
            $('#TablaLoad').show();
        objRelSeccionesService.guardarRelacion(url,
                objRelSecciones,
                function (data) {
                    if (data.MsgError == "") {
                        msj.success("Relacion Agregada con Exito");
                        var arr = [];
                        objRelSecciones.IdRelSecciones = data.Id;
                        objRelSecciones.Orden = parseInt($("#hdfTotalRelaciones").val()) + 1;
                        $("#hdfTotalRelaciones").val(parseInt($("#hdfTotalRelaciones").val()) + 1);
                        arr.push(objRelSecciones);
                        llenarTablaRelaciones(arr, false, obj);
                        resetModalAgregarRelacion();
                        //Ocultar Loading
                        $('#TablaLoad').hide();
                    } else {
                        msj.errorTitle(data.MsgError, "Error");
                        //Ocultar Loading
                        $('#TablaLoad').hide();
                    }
                });
        }
    }

    var resetModalAgregarRelacion = function () {

        $("#relTablaHijo").val("0");
        $("#relTablaPadre").val("0");

        $("#relColumnaHijoFK").prop("disabled", true);
        $("#relColumnaHijoFK").html('');
        $("#relColumnaHijoFK").append('<option value="0">Seleccione...</option>');

        $("#relColumnaPadrePK").prop("disabled", true);
        $("#relColumnaPadrePK").html('');
        $("#relColumnaPadrePK").append('<option value="0">Seleccione...</option>');
    }

    function llenarTablaRelaciones(data, borrar, arr) {
        if(borrar)
            $("#tablaRelaciones > tbody").html("");
        for (var i in data) {
            var d = data[i];
            $("#tablaRelaciones > tbody")
                .append('' +
                    '<tr>' +
                        '<td>' +
                            '<button id="subirOrden" type="button" class="btn btn-sm btn-success"><span class="glyphicon glyphicon-arrow-up icon-click up-icon" title="Arriba"></span></button>'+
                            '<button id="bajarOrden" type="button" class="btn btn-sm btn-danger"><span class="glyphicon glyphicon-arrow-down icon-click down-icon" title="Abajo"></span></button>' +
                            '<input type="hidden" id="hdfOrdenActual" value="' + d.Orden + '">' +
                        '</td>' +
                        '<td>' +
                            arr[d.IdSeccionPadre] +
                        '</td>' +
                        '<td>' +
                            d.KeyPadre +
                        '</td>' +
                        '<td>' +
                            arr[d.IdSeccionHijo] +
                        '</td>' +
                        '<td>' +
                            d.KeyHijo +
                        '</td>' +
                        '<td> ' +
                            '<button class="btn btn-danger" id="btnEliminarRelacion"><span class="glyphicon glyphicon-remove"></span></button> ' +
                            '<input type="hidden" id="hdfIdSeccion" value="' + d.IdRelSecciones + '"/>' +
                        '</td>' +
                '</tr>');
        }
    }

    var eliminarRelacion = function(url, idRelSeccion, relacion, obj) {
        objRelSeccion = {
            IdRelSecciones : idRelSeccion
        }
        //Mostrar Loading
        $('#TablaLoad').show();
        objRelSeccionesService.eliminarRelacion(url,
            objRelSeccion,
            function (data) {
                if (data.length > 0) {
                    msj.warning("Relacion Eliminada");
                    //relacion.remove();
                    $("#hdfTotalRelaciones").val(parseInt($("#hdfTotalRelaciones").val()) - 1);
                    llenarTablaRelaciones(data, true, obj);
                    //Ocultar Loading
                    $('#TablaLoad').hide();
                } else {
                    msj.error("Error al Eliminar la Relacion");
                    //Ocultar Loading
                    $('#TablaLoad').hide();
                }
            });
    }

    var cambiarOrden = function (url, id, ordenNuevo, obj, urlCargar) {
        var objRelSecciones = {
            IdRelSecciones: parseInt(id),
            Orden: ordenNuevo
        }
        objRelSeccionesService.cambiarOrden(url, objRelSecciones, function (data) {
            console.log(data);
            cargarRelaciones(urlCargar,obj);
        });
    }

    return {
        cargarRelaciones: cargarRelaciones,
        guardarRelacion: guardarRelacion,
        eliminarRelacion:eliminarRelacion,
        resetModalAgregarRelacion: resetModalAgregarRelacion,
        cambiarOrden: cambiarOrden
    }
}