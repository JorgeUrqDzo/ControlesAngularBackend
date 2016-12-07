var EncConfBandejaController = function () {

    var objBandejaService = new EncConfBandejaService();
    var msj = new MessageCommon();

    var ProbarConsulta = function (url) {

        var palabrasClaves = [
            'DELETE',
            'INSERT',
            'ALTER',
            'DROP',
            'CREATE',
            'TRUNCATE ',
            'REPLACE',
            'UPDATE'
        ];

        var objModBandeja = {
            IdEncConfBandeja: $("#idForm").val(),
            Nombre: $("#txtBandejaNombre").val(),
            Descripcion: $("#txtBandejaDescripcion").val(),
            NumeroPaginas: $("#txtBandejaNumeroPaginas").val(),
            ClaseBandeja: $("#txtClaseBandeja").val(),
            Consulta: $("#txtAreaBandejaConsulta").val(),
            IdTipoConsulta:$("#selTipoConsulta").val(),
            IdUsuarioCreacion: 0,
            IdUsuarioModificacion: 0
        };
        if ($("#selTipoConsulta").val() == 1) {

            for (var clave in palabrasClaves) {
                var p = palabrasClaves[clave];
                var sql = objModBandeja.Consulta.toUpperCase();
                if (sql.indexOf(p) != -1) {
                    msj.errorTitle("Solo se permiten consultas con SELECT", "Consulta inválida");
                }
            }


            if (objModBandeja.Consulta.toUpperCase().indexOf('SELECT') != -1) {

                //Validar consulta
                validarConsulta(objModBandeja, url, function (data) {
                    if (data.Error) {
                        msj.errorTitle("Revise la sintaxis de la Consulta SQL");
                        //return false;
                    }
                    else {
                        msj.success(data.Mensaje);
                        //return true;
                    }
                });
            }
        } else if ($("#selTipoConsulta").val() == 2) {
            //msj.info("tipo de consulta store procedure");
            palabrasClaves.push("SELECT");

            for (var clave in palabrasClaves) {
                var p = palabrasClaves[clave];
                var sql = objModBandeja.Consulta.toUpperCase();
                if (sql.indexOf(p) != -1) {
                    msj.errorTitle("Solo se permiten llamadas a Store Procedures");
                }
            }
            palabrasClaves.pop("SELECT");
        }
    }; // End Metodo Probar Consulta

    validarConsulta = function (objModBandeja, url, funCallBack) {
        objBandejaService.validarConsulta(objModBandeja, url, funCallBack);
    };
    var InsertarFormulario = function (url, urlValidar, indiceTabs, funCallBack) {

        var objModBandeja = {
            IdEncConfBandeja: $("#varIdEncConfBandeja").val(),
            Nombre: $("#txtBandejaNombre").val(),
            Descripcion: $("#txtBandejaDescripcion").val(),
            NumeroPaginas: $("#txtBandejaNumeroPaginas").val(),
            ClaseBandeja: $("#txtClaseBandeja").val(),
            Consulta: $("#txtAreaBandejaConsulta").val(),
            IdUsuarioCreacion: 0,
            IdUsuarioModificacion: 0,
            IdFormulario: $("#txtIdTipoFormularioEncConfBandeja").val(),
            IdTipoConsulta:$("#selTipoConsulta").val()
        };


        $.validator.unobtrusive.parse($('#frmBandeja'));
        if (!$("#frmBandeja").valid()) {
            return false;
        } else {
            //Validar consulta
            validarConsulta(objModBandeja, urlValidar, function (data) {
                if (data.Error) {
                    //msj.errorTitle("Revise la sintaxis de la Consulta SQL");
                    msj.errorTitle(data.Mensaje);
                }
                else {
                    //Mostrar Loading
                    $('#TablaLoad').show();
                    objBandejaService.InsertarForm(objModBandeja,
                        url,
                        function (result) {
                            //msj.success("Se ha creado el formulario exitosamente");
                            //$("#idForm").val(result.Id);
                            //$("#hdfIdEncConfBandejaBCol").val(result.Id);
                            $($('#ulTabBandeja a')[indiceTabs+1]).tab('show');
                            $("#varIdTipoConsulta").val($("#selTipoConsulta").val());
                            if (result.Id > 0)
                                $("#varIdEncConfBandeja").val(result.Id);
                            //Ocultar Loading
                            $('#TablaLoad').hide();
                            funCallBack();
                        });
                    //Muestra el tab 2

                    //$("#idForm").val("0"),
                    //$("#txtBandejaNombre").val(""),
                    //$("#txtBandejaDescripcion").val(""),
                    //$("#txtBandejaNumeroPaginas").val(""),
                    //$("#txtAreaBandejaConsulta").val("");
                }
            });
        }
    }; // End Metodo InsertarFormulario

    var Editar = function (id, url) {
        //Mostrar Loading
        $('#TablaLoad').show();

        LimpiarCampos(true);
        $('#btnNuevo').hide();
        $('#btnCancelar').show();
        var datos = {
            IdEncConfBandeja: id,
        };
        objBandejaService.CargarXId(datos, url, function (data) {
            $("#idForm").val(data.IdEncConfBandeja),
            $("#txtBandejaNombre").val(data.Nombre),
            $("#txtBandejaDescripcion").val(data.Descripcion),
            $("#txtClaseBandeja").val(data.ClaseBandeja),
            $("#txtBandejaNumeroPaginas").val(data.NumeroPaginas),
            $("#txtAreaBandejaConsulta").val(data.Consulta);
            $("#selTipoConsulta").val(data.IdTipoConsulta);
            $("#txtIdTipoFormularioEncConfBandeja").val(data.IdFormulario);
            $("#txtAreaBandejaConsulta").attr("disabled", false);
            $("#varIdTipoConsulta").val(data.IdTipoConsulta);
        });

        //Ocultar Loading
        $('#TablaLoad').hide();

    }; // End Funcion editar

    var LimpiarCampos = function (activar) {
        $("#idForm").val("0");
        $("#txtBandejaNombre").val("");
        $("#txtBandejaDescripcion").val("");
        $("#txtBandejaNumeroPaginas").val("");
        $("#txtAreaBandejaConsulta").val("");
        $("#txtIdTipoFormularioEncConfBandeja").val(" ");
        $("#selTipoConsulta").val(" ");
        $("#varIdTipoConsulta").val(0);
        $("#txtClaseBandeja").val("");

        if (activar) {
            $("#txtBandejaNombre").attr("disabled", false);
            $("#txtBandejaDescripcion").attr("disabled", false);
            $("#txtBandejaNumeroPaginas").attr("disabled", false);
            $("#txtClaseBandeja").attr("disabled", false);
            //$("#txtAreaBandejaConsulta").attr("disabled", false);
            $("#btnBandejaProbar").attr("disabled", false);
            $("#btnBandejaSiguiente").attr("disabled", false);
            $("#txtIdTipoFormularioEncConfBandeja").prop("disabled", false);
            $("#selTipoConsulta").prop("disabled", false);
        }
        else {
            $("#txtBandejaNombre").attr("disabled", true);
            $("#txtBandejaDescripcion").attr("disabled", true);
            $("#txtBandejaNumeroPaginas").attr("disabled", true);
            $("#txtClaseBandeja").attr("disabled", true);
            $("#txtAreaBandejaConsulta").attr("disabled", true);
            $("#btnBandejaProbar").attr("disabled", true);
            $("#btnBandejaSiguiente").attr("disabled", true);
            $("#txtIdTipoFormularioEncConfBandeja").attr("disabled", "disabled");
            $("#selTipoConsulta").attr("disabled", "disabled");

        }
    };

    //Obtener nombre de columnas
    var getColumnas = function (id, url) {

        //url = /bandeja/getColumnas/
        var datos = {
            IdEncConfBandeja: id
        };
        //Mostrar Loading
        $('#TablaLoad').show();
        objBandejaService.CargarNombresColumnas(url, datos, function (data) {

            for (var i in data) {
                var text = data[i].NombreColumna;
                var value = text;
                //console.log(data[i].NombreColumna + "id = > " + value );
                $("#createBandejaColumnaSelect").append('<option value="' + value + '">' + text + '</option>');
            }
            //Ocultar Loading
            $('#TablaLoad').hide();
        });
    };
    return {
        ProbarConsulta: ProbarConsulta,
        InsertarFormulario: InsertarFormulario,
        Editar: Editar,
        getColumnas: getColumnas,
        LimpiarCampos: LimpiarCampos
    };
}