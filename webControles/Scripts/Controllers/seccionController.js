var actions = [];

var SeccionController = function (objArbolControllerPar) {

    var UrlArbolGeneral = '';

    var varSeccionService = new SeccionService();
    var varMensajeController = new MensajeController();
    var objArbolController = objArbolControllerPar;
    var messageCommon = new MessageCommon();
    var objSeccionControlController = new SeccionControlController();


    var GuardarGrupo = function (action) {

        var objNuevoGrupo = {

            Grupo: $('#txtNombreGrupo').val(),
            IdFormulario: $('#hdfIdFormularioGeneral').prop('value')

        }

        varSeccionService.GuardarGrupo(action, objNuevoGrupo, function (data) {
            //Cierra modal y recarga el listado de grupos
            $('#modalAltaGrupo').modal('hide');
            CargarGrupos($('#hdfIdFormularioGeneral').prop('value'));


        });
    }


    var GuardarSeccion = function (url, IdFormulario) {

        $.validator.unobtrusive.parse($('#formGuardarSeccion'));
        if (!$("#formGuardarSeccion").valid()) {
            return false;
        } else {
            var intIdGrupo = 0;
            if ($('#txtIdGrupo').val() != undefined && $('#txtIdGrupo').val() != '') {
                intIdGrupo = $('#txtIdGrupo').val();
            }

            //Modelos de formulario
            var objSeccionesModel = {
                Nombre: $("#txtNombreSeccion").val(),
                IdTipoSeccion: $("#selTipoSeccion").val(),
                IdSeccion: $("#hdfIdSeccion").val(),
                IdFormulario: IdFormulario,
                IdGrupo: intIdGrupo,
                Columnas: parseInt($('#txtColumnasSeccion').val()),
                IdSeccionIcono: $("#txtIcono").val(),
                Tabla: $("#dbTablesNames").val(),
                primaryKeyName: $("#txtPrimaryKeyName").val()

            };

            var objSeccionControlViewModel = {
                LstSeccionControl: objSeccionControlController.GetSeccionController(),
                objSeccionesModel: objSeccionesModel
            };



            //Instancia a servicios para ejecutar codigo ajax
            var objSeccionService = new SeccionService();
            $('#TablaLoad').show();
            var result = objSeccionService.GuardarSeccion(
                objSeccionControlViewModel, url,
                function (result) {
                    varMensajeController.Mensaje(result);
                    objArbolController.Inicializar(IdFormulario);
                    NuevaSeccion();

                    $('#txtIdGrupo').attr("disabled", true),
                    $("#txtIcono").attr("disabled", true),
                    $('#txtColumnasSeccion').val(""),
                    $("#txtNombreSeccion").val(""),
                    $("#dbTablesNames").val("Sin Asignar"),
                    $("#txtPrimaryKeyName").val("");

                    $('#TablaLoad').hide();
                });
        }
    }

    var Editar = function (url, urlSeccionControl, varIdSeccion, urlGetDbTablesName, urlGetTableColumnName) {
        //Reemplazada el contenido del DIV con la información devuelta a la función
        $('#TablaLoad').show();
        varSeccionService.CargarSeccion(url,
            varIdSeccion,
            function (result) {
                $('#divSecciones').html(result);
                $('#divTituloSecciones').show();
                nzSwitch.SetNzSwitch();

                //Carga los controles contenidos en la seccion
                objSeccionControlController.Cargar(urlSeccionControl + "/" + varIdSeccion);
                getDBTablesName(urlGetDbTablesName, urlGetTableColumnName);

                activarControlesTipoFormulario();

                $('#TablaLoad').hide();
            }
        );
    }


    var AgregarGrupo = function () {



    }



    var NuevaSeccion = function () {
        //Limpia Seccion
        $('#txtNombreSeccion').val('');

        //Limpia controles
        objSeccionControlController.LimpiarCampos();

    }
    var InicializarSeccion = function (url, urlGetDbTablesName, urlArbol) {
        UrlArbolGeneral = urlArbol;
        //Carga la vista de seccion y la injecta en el div
        varSeccionService.CargarSeccion(url, 0,
          function (result) {
              $('#divSecciones').html(result);
              $('#divTituloSecciones').show();
              nzSwitch.SetNzSwitch();
              objSeccionControlController.LimpiarCampos();
              getDBTablesName(urlGetDbTablesName);
              
              activarControlesTipoFormulario();


          });


    }

    var CargarGrupos = function (intIdFormulario) {

        var objSelectCommon = new SelectCommon();
        var varResultado = varSeccionService.CargarGrupos(intIdFormulario, function (data) {

            objSelectCommon.Load({
                Contenedor: 'txtIdGrupo',
                LstObjects: data,
                CampoValue: 'Llave',
                CampoText: 'Titulo',
                Success: function () {

                },
                Error: function (result) {
                    alert(result);
                }
            });


        });
    }



    var InicializarEventos = function (url) {

        objSeccionControlController.InicializarEventos();
        $(document).on("click", "#idBtnGenerarControles", function () {
            var tabla = $("#dbTablesNames").val();
            objSeccionControlController.generarControlesSugeridos(url, tabla);
        });
    }

    var ActualizarOrden = function (action, intIdSeccion, intOrden) {

        var objGrupo = {

            IdSeccion: intIdSeccion,
            Orden: intOrden

        }

        varSeccionService.ActualizarOrden(action, objGrupo, function (data) {

        });
    }

    var getDBTablesName = function (url, urlGetTableColumnName) {
        varSeccionService.getDBTablesName(url,
            function (data) {
                //console.log(data);
                for (var i in data) {
                    //console.log(data[i].tableName);
                    var tableName = data[i].tableName;
                    $("#dbTablesNames").append('<option value="' + tableName + '">' + tableName + '</option>');
                }

                var tableName = $("#hidenTablaName").val();
                if (tableName == "" || tableName == undefined) {
                    $("#dbTablesNames").val("Sin Asignar");
                    //Desactivar Generacion de controles
                    $("#idBtnGenerarControles").prop("disabled", true);
                } else {
                    $("#dbTablesNames").val(tableName);
                    getTableColumnName(urlGetTableColumnName);

                    //Activar Generacion de controles
                    $("#idBtnGenerarControles").prop("disabled", false);
                }

            });
    }

    var getTableColumnName = function (url) {
        var tableSelected = $("#dbTablesNames").val();

        if (tableSelected === "Sin Asignar") {
            $("#dbColumnNames").html(" ");
            $("#dbColumnNames").append('<option value="Sin Asignar">(Ninguna)</option>');
            //Desactivar Generacion de controles
            $("#idBtnGenerarControles").prop("disabled", true);
        } else {
            var objGetColumnName = {
                columnName: tableSelected
            };
            varSeccionService.getTableColumnName(url,
                objGetColumnName,
                function (data) {
                    $("#dbColumnNames").html(" ");
                    for (var i in data) {
                        //console.log(data[i].columnName);
                        var columnName = data[i].columnName;
                        $("#dbColumnNames").append('<option value="' + columnName + '">' + columnName + '</option>');
                    }

                    //Activar Generacion de controles
                    $("#idBtnGenerarControles").prop("disabled", false);
                });
        }
    }

    function activarControlesTipoFormulario() {

        if ($("#tipoFormularioId").val() == "2") {
            $(".tipoFormulario1").addClass("hide");
        } else if ($("#tipoFormularioId").val() == "1") {
            $(".tipoFormulario1").removeClass("hide");
        }

        if ($("#tipoFormularioId").val() == 1) {
            $("#valorIdTipoFormulario")
                .html(
                    '<label class="control-label">Columnas de Tabla Seleccionada:</label>' +
                    '<select id="dbColumnNames" class="form-control">' +
                    '<option value="Sin Asignar">(Ninguna)</option>' +
                    '</select>'
                );
        } else if ($("#tipoFormularioId").val() == 2) {
            $("#valorIdTipoFormulario")
                .html(
                    '<label class="control-label">Nombre Parámetro:</label>' +
                    '<input type="text" id="dbColumnNames" class="form-control"></input>'
                );
        }
    }

    return {
        GuardarSeccion: GuardarSeccion,
        Editar: Editar,
        InicializarSeccion: InicializarSeccion,
        InicializarEventos: InicializarEventos,
        CargarGrupos: CargarGrupos,
        GuardarGrupo: GuardarGrupo,
        ActualizarOrden: ActualizarOrden,
        getDBTablesName: getDBTablesName,
        getTableColumnName: getTableColumnName,
    }
}