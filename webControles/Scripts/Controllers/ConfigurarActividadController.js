var ConfigurarActividadController = function () {

    var objConfigurarActividadService = new ConfigurarActividadService();

    var varPaginaController = new PaginaController();

    var msj = new MessageCommon();

    var Buscar = function (url) {
        $('#TablaLoad').show();
        objConfigurarActividadService.Buscar(url);
        $('#TablaLoad').hide();
    }

    var CambioPagina = function () {
        varPaginaController.Inicializar(function (url) {
            url = url + '&&TextoBuscar=' + $('#txtTextoBusqueda').val();
            Buscar(url);
        });
    }

    var ArregloDatosTablaConfigurar = [];

    var CargarXId = function (url, datos) {
        $('#TablaLoad').show();
        objConfigurarActividadService.CargarXId(url,
            datos,
            function (data) {
                //Pintar informacion en los campos Proceso
                $("#txtProcesoNombre").val(data.Nombre);
                $("#txtProcesoDescripcion").val(data.Descripcion);
                $("#idEncProceso").val(data.IdEncProceso);

                //Pintar informacion en los campos Configuracion
                if (data.lstModDetActividad.length > 0) {
                    BorrarTbody();
                    DibujarTabla(data);
                } else {
                    BorrarTbody();
                }
                ActivarControles(true);
                $('#TablaLoad').hide();
            });
    }

    function CrearTbody(data) {
        $("#configTable tbody").html(data);
    }

    var ultimoIdDetActividad = 0;
    function DibujarTabla(data) {
        ArregloDatosTablaConfigurar = data.lstModDetActividad;
        var datosTabla = "";
        for (var i in data.lstModDetActividad) {
            var actividad = data.lstModDetActividad[i].objCatActividad.Actividad;
            var actividadSiguiente = data.lstModDetActividad[i].objCatActividad.ActividadSiguiente;
            var tipoProceso = data.lstModDetActividad[i].objCatTipoProceso.TipoProceso;
            var tipoProcesoSiguiente = data.lstModDetActividad[i].objCatTipoProceso.TipoProcesoSiguiente;
            //id de las tablas
            var idDetActividad = data.lstModDetActividad[i].IdDetActividad;
            //var idEncActividad = data.
            //lstModDetActividad[i].objCatActividad.IdEncActividad;
            var idTipoProceso = data.lstModDetActividad[i].objCatTipoProceso.IdTipoProceso;
            var idEncProceso = data.IdEncProceso;
            var idEncActividad = data.lstModDetActividad[i].IdEncActividad;
            var idActividadSiguiente = data.lstModDetActividad[i].IdEncActividadSiguiente;

            ultimoIdDetActividad = idDetActividad;

            datosTabla +=
                '<tr class="text-capitalize text-center">' +
                    '<td id="tipoProceso">' +
                        tipoProceso +
                    '</td>' +
                    '<td id="actividad">' +
                        actividad +
                    '</td>' +
                    '<td id="tipoProcesoSiguiente">' +
                        tipoProcesoSiguiente +
                    '</td>' +
                    '<td id="actividadSiguiente">' +
                        actividadSiguiente +
                    '</td>' +
                    '<td id="opciones">' +
                        '<input type="hidden" id="idArray" value="' + i + '">' +
                        '<input type="hidden" id="idDetActividad" value="' + idDetActividad + '">' +
                        '<input type="hidden" id="idEncActividad" value="' + idEncActividad + '">' +
                        '<input type="hidden" id="idTipoProceso" value="' + idTipoProceso + '">' +
                        '<input type="hidden" id="idEncProceso" value="' + idEncProceso + '">' +
                        '<input type="hidden" id="idEncActividadSiguiente" value="' + idActividadSiguiente + '">' +
                        '<button type="button" id="btnConfigEdit" class="btn btn-warning form-control"> ' +
                            '<span class="glyphicon glyphicon-pencil"></span>' +
                        '</button>&nbsp;' +
                        '<button type="button" id="btnConfigRemove" class="btn btn-danger form-control">' +
                            '<span class="glyphicon glyphicon-remove"></span>' +
                        '</button>' +
                    '</td>' +
                '</tr>';
        }
        CrearTbody(datosTabla);
    }

    function BorrarTbody() {
        $("#configTable tbody").html(" ");
        ArregloDatosTablaConfigurar = [];
    }

    var ActivarControles = function (activar) {
        if (activar) {
            $("#btnCancelar").show();
            $("#btnGuardar").show();
            $("#btnNuevo").css('display', 'none');

            $("#txtProcesoNombre").attr("disabled", false);
            $("#txtProcesoDescripcion").attr("disabled", false);

            $("#configuracionBtnAgregar").attr("disabled", false);
            $("#configuracionSelectActSig").attr("disabled", false);
            //$("#configuracionChkAprobacion").attr("disabled", false);
            $("#configuracionSelectActividad").attr("disabled", false);

        } else {

            $("#configuracionBtnActualizar").css('display', 'none');
            $("#configuracionBtnCancelar").css('display', 'none');
            $("#btnCancelar").css('display', 'none');
            $("#btnGuardar").css('display', 'none');
            $("#txtProcesoNombre").attr("disabled", true);
            $("#txtProcesoDescripcion").attr("disabled", true);
            $("#configuracionBtnAgregar").attr("disabled", true);
            $("#configuracionSelectActSig").attr("disabled", true);
            //$("#configuracionChkAprobacion").attr("disabled", true);
            $("#configuracionSelectActividad").attr("disabled", true);
            $("#btnNuevo").show();
        }
    }

    var cancelar = function () {
        $("#txtProcesoNombre").val("");
        $("#txtProcesoDescripcion").val("");
        $("#idEncProceso").val(0);
        BorrarTbody();
        ActivarControles(false);
    }

    var Nuevo = function () {
        ActivarControles(true);
    }

    var ActividadXTipo = function (url) {
        $('#TablaLoad').show();
        objConfigurarActividadService.ActividadXTipo(url, function (data) {

            var tipo = "";
            var id = "";
            for (var i in data) {
                if (tipo == data[i].objTipoProceso.TipoProceso) {
                    crearOpciones(data[i].Actividad, id, data[i].IdEncActividad);
                } else {
                    tipo = data[i].objTipoProceso.TipoProceso;
                    id = data[i].objTipoProceso.IdTipoProceso;
                    crearSelect(tipo, id);
                    crearOpciones(data[i].Actividad, id, data[i].IdEncActividad);
                }
            }

            function crearSelect(label, id) {
                $("#configuracionSelectActividad").append('<optgroup id="' + id + '" class="'  + id + '" label="' + label + '"></optgroup>');
                $("#configuracionSelectActSig").append('<optgroup id="' + id + '" class="2'  + id + '" label="' + label + '"></optgroup>');
            }
            function crearOpciones(text, id, value) {
                $("." + id).append('<option value="' + value + '">' + text + '</option>');
                $(".2" + id).append('<option value="' + value + '">' + text + '</option>');
            }

            $('#TablaLoad').hide();
        });
    }

    var AgregarDatosTabla = function (nuevo, id) {
        var tipoProceso = $("#configuracionSelectActividad :selected").parent().attr('label');
        var actividad = $("#configuracionSelectActividad :selected").text();
        var tipoProcesoSiguiente = $("#configuracionSelectActividad :selected").parent().attr('label');
        var actividadSiguiente = $("#configuracionSelectActSig :selected").text();
        var idTipoProceso = $("#configuracionSelectActividad :selected").parent().attr('id');
        var idEncActividad = $("#configuracionSelectActividad").val();
        var idEncActividadSiguiente = $("#configuracionSelectActSig").val();

        var objCatTipoProceso = {
            Activo: false,
            IdAreaProceso: "",
            IdTipoProceso: idTipoProceso,
            TipoProceso: tipoProceso,
            TipoProcesoSiguiente: tipoProcesoSiguiente
        };
        var objCatActividad = {
            Actividad: actividad,
            ActividadSiguiente: actividadSiguiente,
            IdEncActividad: "",
            IdTipoProceso: "",
            IniciaProceso: "",
            Orden: "",
            TiempoPromedioObjetivo: 0,
            UrlActividad: "",
            UrlDestinoAlTerminar: ""
        };


        var idEncProceso = $("#idEncProceso").val() == undefined ? 0 : $("#idEncProceso").val();

        var obj = {
            Aprobacion: $("#configuracionChkAprobacion").bootstrapSwitch('state'),
            IdDetActividad: ultimoIdDetActividad + 1,
            IdEncActividad: idEncActividad,
            IdEncActividadSiguiente: idEncActividadSiguiente,
            IdEncProceso: idEncProceso,
            objCatActividad: objCatActividad,
            objCatTipoProceso: objCatTipoProceso
        };

        if (nuevo)
            ArregloDatosTablaConfigurar.push(obj);
        else
            ArregloDatosTablaConfigurar[id] = obj;

        DibujarTabla(armarDataTabla(ArregloDatosTablaConfigurar));
    }

    function armarDataTabla(obj) {
        var idEncProceso = $("#idEncProceso").val() == undefined ? 0 : $("#idEncProceso").val();
        var data = {
            IdEncProceso: idEncProceso,
            Nombre: $("#txtProcesoNombre").val(),
            Descripcion: $("#txtProcesoDescripcion").val(),
            Activo: "",
            objDetActividad: "",
            lstModDetActividad: obj
        }
        return data;
    }

    var Guardar = function (url, url2) {
        
        $.validator.unobtrusive.parse($('#procesoForm'));
        if (!$("#procesoForm").valid()) {
            return false;
        } else {
            $('#TablaLoad').show();
            var nombre = $("#txtProcesoNombre").val();
            var descripcion = $("#txtProcesoDescripcion").val();
            if (nombre === "" || descripcion === "") {
                $("#liProceso").attr('active', true);
                msj.errorTitle("El Campo Nombre y Descripcion de la pestaña Proceso no deben ser vacios");
                $('#TablaLoad').hide();
                return false;
            } else {
                var datos = armarDataTabla(ArregloDatosTablaConfigurar);
                objConfigurarActividadService.Guardar(url,
                    datos,
                    function (data) {
                        if (!data.Error)
                            msj.success(data.Mensaje);
                        else
                            msj.error(data.Mensaje);
                        Buscar(url2);
                        cancelar();
                        $('#TablaLoad').hide();
                    });
            }
        }
    }

    var ObtenerPosicionPorId = function (Id) {
        var indexes = $.map(ArregloDatosTablaConfigurar, function (obj, index) {
            if (obj.IdDetActividad == Id) {
                return index;
            }
        });
        if (indexes != undefined) return indexes[0];
    }

    var Eliminar = function (id, remove) {
        var index = ObtenerPosicionPorId(id);
        if (remove) {
            ArregloDatosTablaConfigurar.splice(index, 1);
            DibujarTabla(armarDataTabla(ArregloDatosTablaConfigurar));
        } else {
            AgregarDatosTabla(false, index);
        }
    }

    var configEditar = function (idEncActividad, idEncActividadSiguiente, id) {
        $("#idSelected").val(id);
        $("#configuracionSelectActividad").val(idEncActividad);
        $("#configuracionSelectActSig").val(idEncActividadSiguiente);

        $("#configuracionBtnAgregar").css("display", 'none');
        $("#configuracionBtnActualizar").show();
        $("#configuracionBtnCancelar").show();

        var index = ObtenerPosicionPorId(id);
        var estadoArpobacion = ArregloDatosTablaConfigurar[index].Aprobacion;
        $("#configuracionChkAprobacion").bootstrapSwitch("state", estadoArpobacion);
    }

    return {
        Buscar: Buscar,
        CambioPagina: CambioPagina,
        CargarXId: CargarXId,
        Cancelar: cancelar,
        ActivarControles: ActivarControles,
        Nuevo: Nuevo,
        ActividadXTipo: ActividadXTipo,
        AgregarDatosTabla: AgregarDatosTabla,
        Guardar: Guardar,
        Eliminar: Eliminar,
        configEditar: configEditar,
    }
}