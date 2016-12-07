var SeccionControlController = function () {
    var objSeccionControlService = new SeccionControlService();
    var varMensajeController = new MensajeController();

    var lstSeccionControl = undefined;
    var varBotonEditar = "<button type='button' class='btn btn-success' title='Editar' id='btnEditarSeccionControl' style='cursor: pointer' key='[key]'><i class='fa fa-pencil'></i></button>";
    var varBotonEliminar = "<button type='button' class='btn btn-danger' title='Eliminar' id='btnConfEliminarSecControl' style='cursor: pointer' key='[key]'><i class='fa fa-times'></i></button>";;
    var varContGeneral = 1;

    var LlenarObjetoGrid = function () {

        var intOrden = 1;
        if (lstSeccionControl != undefined && lstSeccionControl.length > 0) {
            intOrden = lstSeccionControl.length + 1;
        }


        var objEncDatasource = {

            BaseDatos: $('#chkBDSeccionControl').bootstrapSwitch('state'),
            Consulta: $('#txtConsultaBandeja').val(),
            ValueField: $('#txtCampoValor').val(),
            TextField: $('#txtCampoTexto').val(),
            Valores: $('#txtCapturaValores').val()
        }

        var objSeleccionControl = {
            NombreColumna: $("#dbColumnNames").val(),
            Nombre: $('#txtNombreSeccionControl').val(),
            IdTipoControl: $('#selTipoControl').val(),
            TipoControl: $('#selTipoControl :selected').text(),
            Longitud: ($('#txtLongitudSeccionControl').val() != '')
                ? parseInt($('#txtLongitudSeccionControl').val())
                : 0,
            Formato: '',
            Requerido: $('#chkRequeridoSeccionControl').is(':checked'),
            IdSeccionControl: $('#hdfIdSeccionControl').val(),
            ValorDefault: $('#txtValorDefaultSeccionControl').val(),
            Caption: $('#txtCaptionSeccionControl').val(),
            HelpText: $('#txtHelpTextSeccionControl').val(),
            TextoSeleccionado: $('#txtTextoSeleccionadoSeccionControl').val(),
            TextoNoSeleccionado: $('#txtTextoNoSeleccionadoSeccionControl').val(),
            TextAreaHeight: ($('#txtTextAreaHeightSeccionControl').val() != '')
                ? parseInt($('#txtTextAreaHeightSeccionControl').val())
                : 0,
            Orden: intOrden,
            objEncDataSource: objEncDatasource,
            IdSeccionControlPadre: $("#hdfIdSeccionControlPadre").val()
        };



        return objSeleccionControl;
    }

    var LimpiarCamposCaptura = function () {

        $('#txtNombreSeccionControl').val('');
        $('#txtLongitudSeccionControl').val('0');
        $('#txtFormatoSeccionControl').val('');
        $('#txtNombreSeccionControl').focus();
        $('#hdfIdSeccionControl').val('0');
        $('#btnAgregarControl').val('Agregar');
        $('#txtIdGrupo').val();

        $("#selTipoControl").val("");

        $("#txtConsultaBandeja").val("");
        $("#txtCampoTexto").val("");
        $("#txtCampoValor").val("");
        $('#txtValorDefaultSeccionControl').val('');
        $('#txtCaptionSeccionControl').val('');
        $('#txtHelpTextSeccionControl').val('');
        $('#txtTextoSeleccionadoSeccionControl').val('');
        $('#txtTextoNoSeleccionadoSeccionControl').val('');
        $('#txtTextAreaHeightSeccionControl').val('0');
        $('#divSwitch').hide();
        $('#divTextArea').hide();
        $('#divOrigenDatos').hide();
        $("#hdfIdSeccionControlPadre").val(0);


    }

    var LimpiarCampos = function () {

        LimpiarCamposCaptura();
        lstSeccionControl = undefined;
        $('#tbSeccionControl > tbody').html('');
    }

    var DibujarGrid = function () {
        $("#selectCampoPadre").html("");
        $("#selectCampoPadre").append('<option>(Ninguno)</option>');
        if (lstSeccionControl != undefined) {
            var varBody = "";
            var objRenglon = undefined;
            for (var i = 0; i < lstSeccionControl.length; i++) {
                objRenglon = lstSeccionControl[i];
                varBody += "<tr>";
                varBody += "<td>" + objRenglon.Orden + "</td>";
                varBody += "<td>" + objRenglon.Nombre + "</td>";
                varBody += "<td>" + objRenglon.TipoControl + "</td>";
                varBody += "<td>" + objRenglon.ValorDefault + "</td>";
                varBody += "<td>" + objRenglon.Longitud + "</td>";
                varBody += "<td>" + ((objRenglon.Requerido) ? "SI" : "NO") + "</td>";
                //varBody += "<td>" + objRenglon.IdSeccionControlPadre + "</td>";
                varBody += "<td>" +
                    varBotonEditar.replace("[key]", objRenglon.IdSeccionControl) +
                    varBotonEliminar.replace("[key]", objRenglon.IdSeccionControl) +
                    "</td>";

                varBody += "</tr>";

                if (objRenglon.TipoControl == "List" || objRenglon.TipoControl == "RadioButtonList") {
                    $("#selectCampoPadre").append('<option class="' + objRenglon.objEncDataSource.ValueField + '" value="' + objRenglon.IdSeccionControl + '">' + objRenglon.Nombre + '</option>');
                }
            }

            $('#tbSeccionControl > tbody').html(varBody);
            $('#tbSeccionControl > tbody').sortable(

                );
            //$('#tbSeccionControl').dataTable({


            //    search: false,
            //    order : 1

            //});
        }
    }

    var AsignarPosicion = function () {
        //Obtiene los elementos contenidos en el grid
        if (lstSeccionControl != undefined) {
            var varTD = undefined;
            var varId = undefined;
            var lstSeccionControlNueva = [];
            var intPosicion = 0;

            $('#tbSeccionControl > tbody > tr').each(function () {

                varId = $($(this).find('td')[0]).html();
                intPosicion = ObtenerPosicionPorOrden(varId);
                lstSeccionControlNueva.push(lstSeccionControl[intPosicion]);

            });

            lstSeccionControl = lstSeccionControlNueva;
        }
        //Ordena la lista
    };

    var ObtenerPosicionPorId = function (IdSeccionControl) {

        var indexes = $.map(lstSeccionControl, function (obj, index) {
            if (obj.IdSeccionControl == IdSeccionControl) {
                return index;
            }
        });

        if (indexes != undefined) return indexes[0];
    }
    var ObtenerPosicionPorOrden = function (intOrden) {

        var indexes = $.map(lstSeccionControl, function (obj, index) {
            if (obj.Orden == intOrden) {
                return index;
            }
        });

        if (indexes != undefined) return indexes[0];
    }

    var AgregarElemento = function () {
        AsignarPosicion();
        if (lstSeccionControl == undefined) {
            lstSeccionControl = [];
        }
        var objSeccionControl = LlenarObjetoGrid();

        if (objSeccionControl.IdSeccionControl != 0) {

            //Edita el que tenga el id seleccionado
            var varIndex = ObtenerPosicionPorId(objSeccionControl.IdSeccionControl);
            //asigna el orden anterior
            objSeccionControl.Orden = lstSeccionControl[varIndex].Orden;
            lstSeccionControl[varIndex] = objSeccionControl;
        }
        else {

            //Cuando es nuevo
            objSeccionControl.IdSeccionControl = varContGeneral;
            lstSeccionControl.push(objSeccionControl);
        }

        varContGeneral++;
        DibujarGrid();
        LimpiarCamposCaptura();
    }


    var AgregarControl = function () {
        $.validator.unobtrusive.parse($('#formSeccionControl'));
        if ($("#formSeccionControl").valid()) {
            AgregarElemento();
        }
    }

    var CancelarControl = function () {
        LimpiarCamposCaptura();
    }

    var EditarSeccionControl = function (varId) {

        if (lstSeccionControl != undefined) {

            var varIndex = ObtenerPosicionPorId(varId);
            var objSeleccionControl = lstSeccionControl[varIndex];

            try {
                $('#txtNombreSeccionControl').val(objSeleccionControl.Nombre);
                $('#selTipoControl').val(objSeleccionControl.IdTipoControl);
                $('#txtLongitudSeccionControl').val(objSeleccionControl.Longitud);
                $('#txtFormatoSeccionControl').val(objSeleccionControl.Formato);
                $('#chkRequeridoSeccionControl').prop('checked', objSeleccionControl.Requerido);
                $('#chkRequeridoSeccionControl').bootstrapSwitch('state', objSeleccionControl.Requerido);
                $('#hdfIdSeccionControl').val(varId);
                $('#btnAgregarControl').val('Guardar');

                $('#txtValorDefaultSeccionControl').val(objSeleccionControl.ValorDefault);
                $('#txtCaptionSeccionControl').val(objSeleccionControl.Caption);
                $('#txtHelpTextSeccionControl').val(objSeleccionControl.HelpText);
                $('#txtTextoSeleccionadoSeccionControl').val(objSeleccionControl.TextoSeleccionado);
                $('#txtTextoNoSeleccionadoSeccionControl').val(objSeleccionControl.TextoNoSeleccionado);
                $('#txtTextAreaHeightSeccionControl').val(objSeleccionControl.TextAreaHeight);

                $('#dbColumnNames').val(objSeleccionControl.NombreColumna);

                $('#divSwitch').hide();
                $('#divTextArea').hide();
                $('#divOrigenDatos').hide();
                $('#divOrigenDatosBD').hide();
                $('#divOrigenDatosEstatico').hide();

                if (objSeleccionControl.objEncDataSource != undefined && objSeleccionControl.objEncDataSource != null) {


                    $('#txtConsultaBandeja').val(objSeleccionControl.objEncDataSource.Consulta);
                    $('#txtCampoTexto').val(objSeleccionControl.objEncDataSource.TextField);
                    $('#txtCampoValor').val(objSeleccionControl.objEncDataSource.ValueField);
                    $('#txtCapturaValores').val(objSeleccionControl.objEncDataSource.Valores);
                    $("#hdfIdSeccionControlPadre").val(objSeleccionControl.IdSeccionControlPadre);
                    $("#selectCampoPadre").val(objSeleccionControl.IdSeccionControlPadre);

                    if (objSeleccionControl.objEncDataSource.BaseDatos) {
                        $('#divOrigenDatosBD').show();
                    } else {
                        $('#divOrigenDatosEstatico').show();
                    }
                    $('#chkBDSeccionControl').bootstrapSwitch('state', objSeleccionControl.objEncDataSource.BaseDatos);
                }

                $('#chkBDSeccionControl').off('switchChange.bootstrapSwitch');
                $('#chkBDSeccionControl').on('switchChange.bootstrapSwitch', function (event, state) {
                    var bolEstado = $(this).bootstrapSwitch('state')
                    $('#divOrigenDatosBD').hide();
                    $('#divOrigenDatosEstatico').hide();
                    if (bolEstado) {
                        //Si es verdadero muestra el div de origen de datos
                        $('#divOrigenDatosBD').show();

                    }
                    else {
                        //Si no es verdadero muestra el div de valores
                        $('#divOrigenDatosEstatico').show();
                    }
                });

                switch (parseInt(objSeleccionControl.IdTipoControl)) {
                    case enumTipoControl.CheckBoxList:
                    case enumTipoControl.RadioButtonList:
                    case enumTipoControl.List:
                    case enumTipoControl.Multiseleccion:
                        $('#divOrigenDatos').show();
                        break;
                    case enumTipoControl.Switch:
                        $('#divSwitch').show();
                        break;
                    case enumTipoControl.TextArea:
                        $('#divTextArea').show();
                        break;
                }



            } catch (ex) { }
        }

    }

    var ConfirmarEliminarSeccionControl = function (varId) {

        if (lstSeccionControl != undefined) {

            $('#hdfIdSeccionControl').val(varId);
            $('#modalSeccionControl').modal('show');
        }


    }

    var EliminarSeccionControl = function () {

        if (lstSeccionControl != undefined) {
            AsignarPosicion();
            var varId = $('#hdfIdSeccionControl').val();
            //Confirmacion
            var varIndex = ObtenerPosicionPorId(varId);

            lstSeccionControl.splice(varIndex, 1);
            $('#modalSeccionControl').modal('hide');
            LimpiarCamposCaptura();
            DibujarGrid();
        }


    }

    var InicializarEventos = function () {

        $(document).on('click', '#btnEditarSeccionControl', function (event) {
            var varId = $(this).attr('key');
            EditarSeccionControl(varId);
        });
        $(document).on('click', '#btnConfEliminarSecControl', function (event) {
            var varId = $(this).attr('key');
            ConfirmarEliminarSeccionControl(varId);
        });
        $(document).on('click', '#btnEliminarSeccionControl', function (event) {
            EliminarSeccionControl();
        });

        $('#modalSeccionControl').on('hidden.bs.modal', function (e) {
            $('#hdfIdSeccionControl').val('0')
        });

        $(document).on('click', '#btnAgregarControl', function (event) {
            AgregarControl();
        });

        $(document).on('click', '#btnCancelarControl', function (event) {
            CancelarControl();
        });

    }

    var GetSeccionController = function () {
        //Antes de regresar el listado asigna la posicion correcta
        AsignarPosicion();
        return lstSeccionControl;
    }

    var Cargar = function (Url) {

        objSeccionControlService.Cargar(Url, function (result) {

            if (result != undefined) {
                //Transdorma el listado de result al listado usado en este controlador
                lstSeccionControl = result;
                DibujarGrid();

            }
        });
    }

    var generarControlesSugeridos = function (Url, tablaSeleccionada) {
        $('#TablaLoad').show();
        objSeccionControlService.cargarControles(Url,
            tablaSeleccionada,
            function (data) {
                generarControles(data);
            });
    }


    var generarControles = function (data) {
        $('#TablaLoad').hide();
        //Confirmacion de usario para reemplazar los controles actuales por los sugeridos
        if (lstSeccionControl == undefined) {
            crearControles(data);
        } else if (lstSeccionControl.length > 0) {
            $("#modalControlesSugeridos").modal('show');
            $(document)
                .on("click",
                    "#btnConfirmacionControlesSugeridos",
                    function () {
                        crearControles(data);
                    });
        } else {
            crearControles(data);
        }
    }

    function crearControles(data) {
        lstSeccionControl = undefined;
        LimpiarCamposCaptura();
        $("#modalControlesSugeridos").modal('hide');
        $('#TablaLoad').show();
        for (var ctrl = 1; ctrl < data.length ; ctrl++) {
            AsignarPosicion();

            var BaseDatos = "";
            var Consulta = "";
            var ValueField = "";
            var TextField = "";
            var Valores = "";

            var name = data[ctrl].Nombre;
            //Verificar si el nombre contiene la palabra Id
            if (name.includes("Id")) {
                //Quitar la palabra Id
                name = name.substring(2, name.lenght);
            }
            //Convertir del formato CammelCase a formato Normal
            name = name
                  .replace(/([A-Z])/g, ' $1')
                  .replace(/^./,
                      function (str) {
                          return str.toUpperCase();
                      });

            if (data[ctrl].Nombre.includes("Id") && data[ctrl].Tipo == "int" && data[ctrl].SchemaTablaRelacion != "" && data[ctrl].TablaRelacion != "") {
                var nombre = data[ctrl].Nombre;
                tipo = "List";
                idTipo = "6";
                BaseDatos = true;
                Consulta = "SELECT * FROM " + data[ctrl].SchemaTablaRelacion + "." + data[ctrl].TablaRelacion;
                ValueField = nombre;
                TextField = nombre.substring(2, nombre.lenght);
            } else {
                var nombreYtipo = getTipoControl(data[ctrl].Tipo);
                var tipo = nombreYtipo[0];
                var idTipo = nombreYtipo[1];
            }
            if (lstSeccionControl == undefined) {
                lstSeccionControl = [];
            }


            var intOrden = 1;
            if (lstSeccionControl != undefined && lstSeccionControl.length > 0) {
                intOrden = lstSeccionControl.length + 1;
            }

            //llenar objeto
            var objSeccionControl = {
                NombreColumna: data[ctrl].Nombre,
                Nombre: name,
                IdTipoControl: idTipo,
                TipoControl: tipo,
                Longitud: data[ctrl].Longitud,
                Formato: '',
                Requerido: data[ctrl].Null,
                IdSeccionControl: $('#hdfIdSeccionControl').val(),
                ValorDefault: "",
                Caption: "",
                HelpText: "",
                TextoSeleccionado: "",
                TextoNoSeleccionado: "",
                TextAreaHeight: 3,
                Orden: intOrden,
                objEncDataSource: {
                    BaseDatos: BaseDatos,
                    Consulta: Consulta,
                    ValueField: ValueField,
                    TextField: TextField,
                    Valores: Valores
                },
                IdSeccionControlPadre: "0"
            };

            objSeccionControl.IdSeccionControl = varContGeneral;
            lstSeccionControl.push(objSeccionControl);
            varContGeneral++;

            DibujarGrid();
        }
        $('#TablaLoad').hide();
    }

    function getTipoControl(control) {
        var tipo = [];
        switch (control) {

            case "int":
                tipo[0] = "Numerico";
                tipo[1] = "2";
                break;
            case "varchar":
                tipo[0] = "Texto";
                tipo[1] = "1";
                break;

            case "text":
                tipo[0] = "TextArea";
                tipo[1] = "9";
                break;

            case "bit":
                tipo[0] = "Switch";
                tipo[1] = "8";
                break;

            case "datetime":
                tipo[0] = "Fecha";
                tipo[1] = "3";
                break;

            default:
                tipo[0] = "Texto";
                tipo[1] = "1";
        }

        return tipo;
    }

    return {
        AgregarControl: AgregarControl,
        CancelarControl: CancelarControl,
        InicializarEventos: InicializarEventos,
        GetSeccionController: GetSeccionController,
        LimpiarCampos: LimpiarCampos,
        Cargar: Cargar,
        generarControlesSugeridos: generarControlesSugeridos
    }

}