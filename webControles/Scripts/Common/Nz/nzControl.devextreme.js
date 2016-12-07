
//var NezterControls = NezterControls || {};
var customControls = [];
var padreHijo = [];
var selectBoxArray = [];
var dataSourceCustomize = new DevExpress.data.DataSource({ store: [], paginate: false });

///Obtiene el tipo de control de Devextreme en base al control configurado en la BD
Controls.prototype.getEditorType = function (objControl, varObjDX) {
    var strTipo = '';
    switch (objControl.IdTipoControl) {
        case this.enumTipos.Texto:
            strTipo = "dxTextBox";
            break;
        case this.enumTipos.Numerico:
            strTipo = "dxNumberBox";
            break;
        case this.enumTipos.Fecha:
            strTipo = "dxDateBox";
            //varObjDX.editorOptions.width = 200;
            break;
        case this.enumTipos.CheckBoxList:
            strTipo = "dxTagBox";
            varObjDX.editorOptions.dataSource = objControl.ObjDataSource.DataSource;
            varObjDX.editorOptions.valueExpr = objControl.ObjDataSource.ValueField;
            varObjDX.editorOptions.displayExpr = objControl.ObjDataSource.TextField;
            varObjDX.editorOptions.showSelectionControls = true;
            varObjDX.editorOptions.applyValueMode = "useButtons";
            //varObjDX.editorOptions.deferRendering = false;
            break;
        case this.enumTipos.RadioButtonList:
            strTipo = "dxRadioGroup";
            varObjDX.editorOptions.dataSource = objControl.ObjDataSource.DataSource;
            varObjDX.editorOptions.valueExpr = objControl.ObjDataSource.ValueField;
            varObjDX.editorOptions.displayExpr = objControl.ObjDataSource.TextField;
            break;
        case this.enumTipos.List:
            strTipo = "dxSelectBox";
            //Optiene datasource
            varObjDX.editorOptions.dataSource = objControl.ObjDataSource.DataSource;
            varObjDX.editorOptions.valueExpr = objControl.ObjDataSource.ValueField;
            varObjDX.editorOptions.displayExpr = objControl.ObjDataSource.TextField;
            varObjDX.editorOptions.searchEnabled = true;
            varObjDX.validationRules.push(
            {
                type: "pattern",
                pattern: "[1-9]",
                message: "Seleccione un Campo."
            });
            var idControl = objControl.IdSeccionControl;
            var idPadre = objControl.IdSeccionControlPadre;
            var value = objControl.ValorDefault;

            //var field = objControl.ObjDataSource.ValueField;



            if (idPadre != 0) {
                var control =
                {
                    IdSeccionControl: idControl,
                    IdSeccionControlPadre: idPadre,
                    value: value
                }
                padreHijo.push(control);
                //varObjDX.editorOptions.dataSource = dataSourceCustomize;

                if (varObjDX.editorOptions.dataSource == null)
                    varObjDX.editorOptions.dataSource = dataSourceCustomize;
                else {
                    dataSourceCustomize.store()._array = [];
                    for (var obj in objControl.ObjDataSource.DataSource) {

                        //Alimenta el DataSourceCustomize con el resultado que arroja la API
                        dataSourceCustomize.store().insert(objControl.ObjDataSource.DataSource[obj]);
                    }
                    dataSourceCustomize.load();
                    varObjDX.editorOptions.dataSource = dataSourceCustomize;
                }
            }


            if (isNaN(objControl.ValorDefault))
                varObjDX.editorOptions.value = objControl.ValorDefault;
            else
                varObjDX.editorOptions.value = parseInt(objControl.ValorDefault);

            break;
        case this.enumTipos.Multiseleccion:
            strTipo = "dxTagBox";
            varObjDX.editorOptions.dataSource = objControl.ObjDataSource.DataSource;
            varObjDX.editorOptions.valueExpr = objControl.ObjDataSource.ValueField;
            varObjDX.editorOptions.displayExpr = objControl.ObjDataSource.TextField;
            //varObjDX.editorOptions.deferRendering = false;

            var idControl = objControl.IdSeccionControl;
            var idPadre = objControl.IdSeccionControlPadre;
            //var field = objControl.ObjDataSource.ValueField;

            if (idPadre != 0) {
                var control =
                {
                    IdSeccionControl: idControl,
                    IdSeccionControlPadre: idPadre,
                }
                padreHijo.push(control);
                //varObjDX.editorOptions.dataSource = dataSourceCustomize;

                if (varObjDX.editorOptions.dataSource == null)
                    varObjDX.editorOptions.dataSource = dataSourceCustomize;
                else {
                    dataSourceCustomize.store()._array = [];
                    for (var obj in objControl.ObjDataSource.DataSource) {

                        //Alimenta el DataSourceCustomize con el resultado que arroja la API
                        dataSourceCustomize.store().insert(objControl.ObjDataSource.DataSource[obj]);
                    }
                    dataSourceCustomize.load();
                    varObjDX.editorOptions.dataSource = dataSourceCustomize;
                }

            }
            break;
        case this.enumTipos.Switch:
            strTipo = "dxSwitch";
            varObjDX.editorOptions.onText = objControl.TextoSeleccionado;
            varObjDX.editorOptions.offText = objControl.TextoNoSeleccionado;
            break;
        case this.enumTipos.TextArea:
            strTipo = "dxTextArea";
            varObjDX.editorOptions.height = objControl.TextAreaHeight;
            //varObjDX.editorOptions.colSpan = 3;
            break;

        default:
            strTipo = "dxTextBox";
            //strTipo = "otroControl";
            //varObjDX.editorOptions.value = "!@#!@#!@";
            break;
    }

    varObjDX.editorType = strTipo;
    return varObjDX;
};

///Llama a la api para obtener la informacion
Controls.prototype.getDataSource = function (funSuccess) {
    var varActionDataSource = this.conf.Action + this.ActionGet + this.conf.UUID + "?key=" + this.conf.Key;
    $.get(varActionDataSource, function (result) {
        funSuccess(result);

    });
};

///Transforma el objeto control del servidor al objeto control requerido por Devextreme
Controls.prototype.transformControlToDX = function (objControl) {
    var context = this;
    var varObjDX = {
        label: {
            text: objControl.Nombre
        },
        dataField: objControl.IdSeccionControl,
        isRequired: objControl.Requerido,
        //dxValidator: {
        //    //adapter:{
        //    //    getValue: function () {
        //    //        return ;
        //    //    },
        //    //},
        //    validationRules: [{
        //        type: "required",
        //        message: "Campo Requerido"
        //    }]
        //},
        validationRules: [{
            type: "required",
            message: "Campo Requerido"
        }],
        hint: objControl.Caption,
        editorOptions: {
            placeholder: objControl.Caption,
            maxLength: objControl.Longitud,
            value: objControl.ValorDefault,
            name: objControl.NombreColumna,
            control: objControl.IdTipoControl
        },
        cssClass: objControl.CssClass,
        helpText: objControl.HelpText
    };

    varObjDX = this.getEditorType(objControl, varObjDX);

    return varObjDX;
};

Controls.prototype.transformSectionToDX = function (objSeccion, context) {

    var varObjDX = {
        caption: objSeccion.Nombre,
        title: objSeccion.Nombre,
        colCount: objSeccion.Columnas,
        IdGrupo: objSeccion.IdGrupo,
        icon: objSeccion.Icono
    };

    varObjDX.items = [];
    for (var j = 0; j < objSeccion.LstModApiControl.length; j++) {
        //Asigna el objeto del control
        varObjDX.items.push(context.transformControlToDX(objSeccion.LstModApiControl[j]));
    }

    return varObjDX;
};

//busca el index en el que se encuentra el grupo
Controls.prototype.buscarIndex = function (idGrupo, context) {

    var indexes = $.map(context.Sections, function (obj, index) {
        if (obj.IdGrupo == idGrupo) {
            return index;
        }
    });
    return indexes[0];
};

Controls.prototype.aplicarFormatoFecha = function (result) {

    if (result.FormatoFecha != undefined) {
        $(".dx-datebox-date").dxDateBox({
            formatString: result.FormatoFecha
        });
    }
    else {
        //Formato de fecha default
        $(".dx-datebox-date").dxDateBox({
            formatString: "dd/MM/yyyy"
        });
    }
};

///Inicializa el control
Controls.prototype.init = function () {
    var context = this;

    this.getDataSource(function (result) {

        var objSectionDX = undefined, index = undefined;
        context.conf.objFormulario = result;
        for (var i = 0; i < result.LstModApiSeccion.length; i++) {
            //Obtiene el objeto de la seccion
            objSectionDX = context.transformSectionToDX(result.LstModApiSeccion[i], context);

            if (objSectionDX.IdGrupo != 0) {
                //Si es diferente de 0 valida si ya esta este grupo en el listado
                index = context.buscarIndex(objSectionDX.IdGrupo, context);
                if (index != undefined) {
                    context.Sections[index].tabs.push(objSectionDX);
                }
                else {
                    context.Sections.push(
                        {
                            itemType: 'tabbed',
                            IdGrupo: objSectionDX.IdGrupo,
                            tabs: [objSectionDX],
                            tabPanelOptions: {///Hace que se cargue todo aunque el tab no se este mostrando
                                deferRendering: false
                            }
                        }
                    );
                }
            }
            else {
                objSectionDX.itemType = 'group';
                context.Sections.push(objSectionDX);
            }
        }
        console.log(context.Sections);
        var formOptions = {
            colCount: 1,
            labelLocation: 'top',
            items: context.Sections,
            customizeItem: function (item) {
                //$("#dxTextBox").dxTextBox;
                if (item.editorType == "dxSelectBox") {
                    var id = item.dataField;
                    selectBoxArray.push(id);
                }

                for (var i in padreHijo) {

                    if (padreHijo[i].IdSeccionControlPadre == item.dataField) {
                        //console.log("IdHijo=>" + padreHijo[i].IdSeccionControl);
                        //console.log("IdPadre=>" + item.dataField);

                        //Agregar envento onChange al dropdown padres
                        item.editorOptions.onValueChanged = function (e) {
                            //obtiene el valor que se selecciono en el dropdown padre
                            var valorSeleccionado = e.value;
                            var varActionDataSource = context.conf.Action + context.ActionDataSource +
                                "?Id=" +
                                padreHijo[i].IdSeccionControl +
                                "&&Key=" +
                                valorSeleccionado;

                            $.get(varActionDataSource,
                                function (result) {
                                    dataSourceCustomize.store()._array = [];
                                    //Alimenta el DataSourceCustomize con el resultado que arroja la API
                                    for (var val in result.DataSource)
                                        dataSourceCustomize.store().insert(result.DataSource[val]);
                                    dataSourceCustomize.load();

                                    var idElementoDx = (context.element).dxForm('instance')
                               .getEditor(padreHijo[i].IdSeccionControl)._options;

                                    //idElementoDx.selectedItem = result.DataSource[0];

                                    (context.element).dxForm('instance')
                                        .getEditor(padreHijo[i].IdSeccionControl)
                                        ._options.value = 0;
                                    $("#" + idElementoDx.attr.id).val("Seleccione...");
                                });
                        }
                    }
                }


                //if (false) {
                if (item.dataField == "463") {
                    item.template = function (formOptions, itemElement) {

                        var control = formOptions.editorOptions.control;
                        var value = formOptions.editorOptions.value;

                        //crear objeto para agregar a arreglo global customControls
                        control = {
                            id: item.dataField,
                        }
                        //Agregar id a arreglo global customControls
                        customControls.push(control);

                        //$('<input>').attr('type', 'Text', 'value', control).appendTo(itemElement);
                        itemElement.append('<input type="text" id="' + item.dataField + '" value="' + value + '"/>');


                        //itemElement.dxValidator = {
                        //    validationRules: [{
                        //        type: "custom",
                        //        message: "Campo Requerido"
                        //    }]
                        //};


                    }

                    //if (item.dataField == "369") {
                    //item.template = function (data, itemElement) {
                    //    var value = data.editorOptions.value;
                    //    $('<input>').attr('type', 'password').appendTo(itemElement);
                    //}

                }
            }
        };

        console.log(formOptions);
        $(context.element).dxForm(
            formOptions
        );
        console.log($(context.element).dxForm());
        context.aplicarFormatoFecha(context.conf.objFormulario);
        //Asigna tamaño del 100% al control de fecha
        $('.dx-datebox').width('100%');
        if (context.conf.Complete != undefined) {

            context.conf.Complete();
        }

        for (var i in selectBoxArray) {
            var id = selectBoxArray[i];
            var idElementoDx = (context.element).dxForm('instance')
                               .getEditor(id)._options;
            if (idElementoDx.value == 0) {
                (context.element).dxForm('instance')
                                        .getEditor(id)
                                        ._options.value = 0;
                $("#" + idElementoDx.attr.id).val("Seleccione...");
            }

        }


        /*
        Agregar lo siguuente en formOptions para generar un custom control
        customizeItem: function (item) {
                if (item.dataField !== 'Italics')
                    return;
                item.template = function (data, itemElement) {
                    var value = data.editorOptions.value;
                    $('<input>').attr('type', 'checkbox').prop('checked', value).appendTo(itemElement);
                }
            }
        */

    });

};

Controls.prototype.save = function (objControl, funcCallBack) {

    ////Obtener los cambios de valor del custom control y actualizar su valor en el modelo
    //for (var i in $(objControl).dxForm('instance').option('formData')) {
    //    for (var j in customControls) {
    //        var idCustomControls = customControls[j].id;
    //        if (i == idCustomControls) {
    //            $(objControl).dxForm('instance').updateData(i, $("#" + idCustomControls).val());
    //        }
    //    }
    //}

    if ($(objControl).dxForm('instance').validate().isValid) {

        var context = this;
        var varAction = this.conf.Action + this.ActionSave;
        var data = $(objControl).dxForm('instance').option('formData');

        var key = this.conf.Key;

        var arr = [];

        for (var i in data) {
            datos =
            {
                IdControl: i,
                Value: data[i]
            };
            //console.log(datos.IdControl + ' ' + datos.Value);
            arr.push(datos);
        }
        if (key != undefined) {

            datos =
            {
                IdControl: 0,
                Value: key
            };

            arr.push(datos);
        }

        $.ajax({
            async: true,
            url: varAction,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(arr),
            contentType: 'application/json',
            success: function (dataSuccess) {

                if (funcCallBack != undefined) {
                    funcCallBack(dataSuccess);
                }
            },
            error: function (errorThrow) {
                //console.log(errorThrow);
                if (funcCallBack !== undefined) {
                    funcCallBack(errorThrow);
                }
            }
        });
    }
};

Controls.prototype.get = function (objDiv) {

    var filtros = []
    //{ nomnbre: 'TextoBuscar', valor: '' }
    var objCampos = undefined;
    var objControl = undefined;
    var configuration = this.conf;
    var objFormData = $(objDiv).dxForm('instance').option('formData');

    if (this.conf !== undefined && this.conf.objFormulario !== undefined && this.conf.objFormulario.LstModApiSeccion !== undefined) {
        //$('#divBusqueda').dxForm('instance').option('formData');
        for (var i = 0; i < this.conf.objFormulario.LstModApiSeccion.length; i++) {
            for (var j = 0; j < this.conf.objFormulario.LstModApiSeccion[i].LstModApiControl.length; j++) {
                objControl = this.conf.objFormulario.LstModApiSeccion[i].LstModApiControl[j];

                switch (objControl.IdTipoControl) {
                    case this.enumTipos.Fecha:
                        objCampos = {
                            nombre: objControl.NombreColumna
                            //valor: (objFormData[objControl.IdSeccionControl] == '') ? undefined : objFormData[objControl.IdSeccionControl]
                        };
                        break;
                    case this.enumTipos.RadioButtonList:
                    case this.enumTipos.List:
                    case this.enumTipos.Multiseleccion:
                        objCampos = {
                            nombre: objControl.NombreColumna,
                            valor: isNaN(objFormData[objControl.IdSeccionControl]) ? 0 : objFormData[objControl.IdSeccionControl]
                        };
                        break;
                    default://Todos los demas
                        objCampos = {
                            nombre: objControl.NombreColumna,
                            valor: objFormData[objControl.IdSeccionControl]
                        };
                        break;

                }

                filtros.push(objCampos);
            }
        }
    }
    return filtros;

};