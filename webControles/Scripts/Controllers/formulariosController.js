var actions = [];
var nombresTablas = {};
var nombresPK = {};
var nombresSecciones = {};
var FormularioController = function (objArbolControllerPar) {


    var IdFormualrioGeneral = 0;
    var lstFormulario = undefined;

    var InitActions = function (varAction) {
        actions = varAction;
    };
    var varFormularioService = new FormulariosService();
    var varMensajeController = new MensajeController();;
    var varPaginaController = new PaginaController();
    var objArbolController = objArbolControllerPar;




    //Recibe el Id del formulario y llena los controles
    var Editar = function (url, varIdFormulario) {
        //Reemplazada el contenido del DIV con la información devuelta a la función 
        varFormularioService.CargarFormulario(url, varIdFormulario,
            function (result) {
                $('#divFormulario').html(result);
                HabilitarControles(true);
                $('#btnCancelar').show();

            }
         )
    }
    //Inicializa todo para dar de alta un nuevo Formulario
    var NuevoFormulario = function () {
        //LimpiarCampos();
        HabilitarControles(true);
        //Mostrar boton Cancelar


        $('#btnCancelar').show();
        //$('#btnGuardar').show();
    }


    var HabilitarControles = function (habilitado) {
        if (habilitado) {
            $('#txtNombreFormulario').attr('disabled', false);
            $('#txtDescripcionFormulario').attr('disabled', false);
        }
        else {
            $('#txtNombreFormulario').attr('disabled', true);
            $('#txtDescripcionFormulario').attr('disabled', true);
        }
    }
    var CambioPagina = function () {
        if (varPaginaController == undefined) varPaginaController = new PaginaController();
        varPaginaController.Inicializar(function (url) {
            url = url + "&&TextoBusqueda=" + $("#txtTextoBusqueda").val();
            Buscar(url);
            //TODO: Crear metodo para busquesa de formulario
        });

    }

    var ObtenerUUID = function (url, varId) {
        //url = "/Formularios/ObtenerUUID/";
        var objFormularioService = FormulariosService();
        objFormularioService.ObtenerUUID(url, varId);

    }


    var GuardarFormulario = function (url) {

        $.validator.unobtrusive.parse($('#formGuardarFormulario'));
        if (!$("#formGuardarFormulario").valid()) {
            return false;
        } else {
            $('#TablaLoad').show();

            NombreFormulario = $("#txtNombreFormulario").val();
            DescripcionFormulario = $("#txtDescripcionFormulario").val();
            IdFormulario = $("#hdfIdFormulario").val();
            IdTipoFormulario = $("#txtIdTipoFormulario").val();
            FormatoFecha = $("#txtIdFormatoFecha").val();
            //Modelos de formulario
            //Encabezado
            var objFormularioModel = {
                Nombre: NombreFormulario,
                Descripcion: DescripcionFormulario,
                IdFormulario: IdFormulario,
                IdTipoFormulario: IdTipoFormulario,
                FormatoFecha: FormatoFecha,
            };


            //Instancia a servicios para ejecutar codigo ajax
            var objFormulariosService = new FormulariosService();
            var result = objFormulariosService.GuardarFormulario(
                objFormularioModel, url,
                function (result) {
                    $('#TablaLoad').hide();
                    varMensajeController.Mensaje(result.ObjMensajeModel);
                    if (!result.ObjMensajeModel.Error) {
                        IdFormualrioGeneral = result.ObjMensajeModel.Id;
                        $("#hdfIdFormulario").val(result.ObjMensajeModel.Id);
                        $("#hdfIdFormularioGeneral").val(result.ObjMensajeModel.Id);
                        $("#hdfUUIDFormulario").val(result.UUID);

                        //LimpiarCampos();
                        objArbolController.Inicializar(IdFormualrioGeneral);
                        $('#modalAltaFormulario').modal('hide');
                    }
                });
        }
    }


    var ConfirmarActivoFormularioControl = function (varId) {
        $('#hdfIdFormularioRow').val(varId);
        $('#modalFormularioControl').modal('show');

    }

    //función para actualizar el formulario
    var ActualizarFormulario = function (varId) {
        var objFormulariosService = new FormulariosService();
        $('#modalFormularioControl').modal('hide');
        url = "/Formularios/Actualizar/";
        IdFormularioGeneral = varId;

        var objFormularioModel = {
            Activo: true,
            Descripcion: "h",
            IdFormulario: IdFormularioGeneral,
            Nombre: "h",
        };
        objFormulariosService.Actualizar(url, objFormularioModel);

    }




    var InicializarEventos = function () {


        //$(document).on('click', '#btnConfEliminarSecControl', function (event) {
        //    var varId = $(this).attr('key');
        //    ConfirmarEliminarSeccionControl(varId);
        //});
        $(document).on('click', '#btnEliminarFormulario', function (event) {

            alert(nooo);
            //EliminarSeccionControl();
        });


    }


    var InicializarFormulario = function (url, urlUUID, id) {



        //asigna el id
        if (id != undefined && id != null && id != '') {
            IdFormualrioGeneral = id;
            $('#hdfIdFormularioGeneral').val(id);
            //var idTipoForm = $("#hdfIdTipoFormularioRow").val();
            //$('#tipoFormularioId').val(idTipoForm);
        };

        //Si el id es igual a 0 entonces muestra el modal
        if (IdFormualrioGeneral == 0) {

            varFormularioService.CargarFormulario(url, 0,
            function (result) {
                $('#divFormularioModal').html(result);
                //Muestra modal
                $('#modalAltaFormulario').modal('show');

            });
        }
        else {
            ObtenerUUIDForm(urlUUID, IdFormualrioGeneral);
            //Carga el formulario
            objArbolController.Inicializar(IdFormualrioGeneral);

        }

    }

    Buscar = function (url) {

        varFormularioService.Buscar(url);

    }
,
    ObtenerUUIDForm = function (url, varId) {
        var objFormularioService = FormulariosService();
        objFormularioService.ObtenerUUIDForm(url, varId, function (data) {

            $('#hdfUUIDFormulario').val(data);
        });
    }

    getIdTipoFormulario = function (url, id) {
        objFormulario = {
            IdFormulario: id
        }
        var objFormularioService = FormulariosService();
        objFormularioService.getIdTipoFormulario(url, objFormulario, function (data) {
            $("#tipoFormularioId").val(data.IdTipoFormulario);
        });
    }

    var seccionesDeFormulario = function (url, idForm) {
        $("#RelacionesMsjError")
                        .text("Debe haber mas de una seccion relacionada a una tabla de la BD y agregada al formulario para poder generar relaciones");
        objFormularioModel = {
            IdFormulario: idForm
        }

        varFormularioService.seccionesDeFormulario(url,
            objFormularioModel,
            function (data) {
                if (data.length > 1) {
                    if (data[0].IdTipoFormulario == 1) {
                        //Mostrar Modal de Relaciones
                        $('#modalRelaciones').modal('show');

                        //llenar dropdown Padre e hijo con las secciones 
                        $("#relTablaPadre").html('');
                        $("#relTablaPadre").append('<option value="0">Seleccione...</option>');
                        $("#relTablaHijo").html('');
                        $("#relTablaHijo").append('<option value="0">Seleccione...</option>');
                        for (var i in data) {
                            datos = data[i];
                            var idSeccion = datos.IdSeccion;
                            var nombreSeccion = datos.Nombre;
                            var indice = datos.Tabla.indexOf('.');
                            var schema = datos.Tabla.substring(indice, -1);
                            var tabla = datos.Tabla.substring(indice + 1, datos.Tabla.lenght);

                            nombresTablas[idSeccion.toString()] = datos.Tabla;
                            nombresPK[idSeccion.toString()] = datos.primaryKeyName;
                            nombresSecciones[idSeccion.toString()] = nombreSeccion;

                            $("#relTablaPadre")
                                .append('<option value="' +
                                    idSeccion +
                                    '">' +
                                    nombreSeccion +
                                    ' (' +
                                    tabla +
                                    ')' +
                                    '</option>');

                            $("#relTablaHijo")
                                .append('<option value="' +
                                    idSeccion +
                                    '">' +
                                    nombreSeccion +
                                    ' (' +
                                    tabla +
                                    ')' +
                                    '</option>');
                        }
                    } else {
                        //Mostar Alerta de que las secciones deben tener una tabla de bd asociada
                        $('#modalRelacionesAdvertencia').modal('show');
                    }
                    //obtener columnas y llenar dorpdowns padre e hijo
                } else {
                    //Mostar Alerta de que se requieren mas de 1 seccion para relacionar
                    $('#modalRelacionesAdvertencia').modal('show');
                }

            });
    }

    var llenarColumnasPadreEHijo = function (url, tabla, idDropDown, isPadre) {
        if (isPadre) {
            $("#" + idDropDown).text(tabla);
        } else {
            var objGetColumnName = {
                columnName: tabla
            };
            //Mostrar Loading
            $('#TablaLoad').show();
            varFormularioService.llenarColumnasPadreEHijo(url,
                objGetColumnName,
                function(data) {
                    $("#" + idDropDown).html('');
                    $("#" + idDropDown).append('<option value="0">Seleccione...</option>');
                    for (var i in data) {
                        var datos = data[i].columnName;
                        $("#" + idDropDown).append('<option value="' + datos + '">' + datos + '</option>');
                    }
                    $("#" + idDropDown).prop("disabled", false);

                    //Ocultar Loading
                    $('#TablaLoad').hide();
                });
        }
    }

    return {
        ActualizarFormulario: ActualizarFormulario,
        GuardarFormulario: GuardarFormulario,
        CambioPagina: CambioPagina,
        Editar: Editar,
        NuevoFormulaio: NuevoFormulario,
        InicializarFormulario: InicializarFormulario,
        IdFormualrioGeneral: IdFormualrioGeneral,
        Buscar: Buscar,
        ConfirmarActivoFormularioControl: ConfirmarActivoFormularioControl,
        ObtenerUUID: ObtenerUUID,
        ObtenerUUIDForm: ObtenerUUIDForm,
        getIdTipoFormulario: getIdTipoFormulario,
        seccionesDeFormulario: seccionesDeFormulario,
        llenarColumnasPadreEHijo: llenarColumnasPadreEHijo,
        nombresTablas: nombresTablas,
        nombresPK:nombresPK,
        nombresSecciones: nombresSecciones,
    }
}