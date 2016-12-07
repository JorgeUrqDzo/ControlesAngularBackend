
    //var NezterControls = NezterControls || {};


    ///Obtiene el tipo de control de Devextreme en base al control configurado en la BD
    Controls.prototype.getEditorType = function (id, varObjDX) {
        var varTipo = "";
        switch (id) {
            case this.enumTipos.Texto:
                varTipo = "dxTextBox";
                
                break;
            case this.enumTipos.Numerico:
                varTipo = "dxNumberBox";
                varObjDX.validationRules = [{
                    type: 'numeric',
                    message: 'Formato invalido'
                }];
                break;
            case this.enumTipos.Fecha:
                varTipo = "dxDateBox";
                break;
            case this.enumTipos.CheckBoxList:
                break;
            case this.enumTipos.RadioButtonList:
                varTipo = "dxRadioGroup";
                break;
            case this.enumTipos.List:
                varTipo = "dxSelectBox";
                break;
            case this.enumTipos.Multiseleccion:
                break;
            case this.enumTipos.Switch:
                varTipo = "dxSwitch";
                break;
            case this.enumTipos.TextArea:
                varTipo = "dxTextArea";
                break;
            default:
                varTipo = "dxTextBox";
                break;
        }
       
        varObjDX.editorType = varTipo;
        return varObjDX;
    };

    ///Llama a la api para obtener la informacion
    Controls.prototype.getDataSource = function (funSuccess) {

        var varActionDataSource = this.conf.Action + "/" + this.conf.UUID;
        $.get(varActionDataSource, function (result) {
            funSuccess(result)
        });
    };


    ///Transforma el objeto control del servidor al objeto control requerido por Devextreme
    Controls.prototype.transformControlObject = function (varObj) {
        var context = this;
        var varObjDX = {
            label: {
                text: varObj.Nombre
            },
            dataField: varObj.NombreColumna,
            isRequired: varObj.Requerido,
            hint: varObj.Caption,
            editorOptions: {
                placeholder: varObj.Caption,
                maxLength: varObj.Longitud,
                value: varObj.ValorDefault,
                name: varObj.NombreColumna
                //mask : '$9999,\\0\\0'

            },
            cssClass: varObj.CssClass,
            helpText: varObj.HelpText
            //,validationRules: [{
            //    type: 'numeric',
            //    message: 'valor debe ser un numero'
            //}]
            

        };

        varObjDX = context.getEditorType(varObj.IdTipoControl, varObjDX);

        return varObjDX;
    };

    Controls.prototype.validationRule = function (varObj, varObjDX) {
        //Agrega el tipo
        if (varObj != undefined && varObjDX != undefined) {

        }

    };
    Controls.prototype.transformSectionObject = function (objSeccion, context) {

        var varObjDX = {
            //itemType: 'group',
            caption: objSeccion.Nombre,
            title: objSeccion.Nombre,
            colCount: 3,
            IdGrupo: objSeccion.IdGrupo,
            icon:objSeccion.Icono
        };

        varObjDX.items = [];
        for (var j = 0; j < objSeccion.LstModApiControl.length; j++) {
            //Asigna el objeto del control
            varObjDX.items.push(context.transformControlObject(objSeccion.LstModApiControl[j]));
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
        return indexes[0]
    };
    ///Inicializa el control
    Controls.prototype.init = function () {
        var context = this;

        this.getDataSource(function (result) {

            var objSectionDx = undefined, index = undefined;



            for (var i = 0; i < result.LstModApiSeccion.length; i++) {
                //Obtiene el objeto de la seccion
                objSectionDx = context.transformSectionObject(result.LstModApiSeccion[i], context);
                 

                if (objSectionDx.IdGrupo != 0) {
                    //Si es diferente de 0 valida si ya esta este grupo en el listado
                    index = context.buscarIndex(objSectionDx.IdGrupo, context)

                    if (index != undefined) {
                        context.Sections[index].tabs.push(objSectionDx);
                    }
                    else {
                        context.Sections.push(
                            {
                                itemType: 'tabbed',
                                IdGrupo: objSectionDx.IdGrupo,
                                tabs: [objSectionDx],
                            }
                        );
                    }
                }
                else {
                    objSectionDx.itemType = 'group';
                    context.Sections.push(objSectionDx);
                }
            }

            var formOptions = {
                colCount: 1,
                labelLocation: 'top',
                items: context.Sections
                //,customizeItem: function (item) {
                //    if (item.dataField == '33_Referencia1') {

                //        item.template = function (data, itemElement) {
                //            var value = data.editorOptions.value;
                //            //$('<input>').attr('type', 'checkbox').prop('checked', value).appendTo(itemElement);
                //            $(itemElement).html('test');
                //        }
                //    }
                //}
                
            };

            $(context.element).dxForm(
                formOptions
            );

            //$(".dx-datebox-date").dxDateBox({
            //    formatString: "dd/MM/yyyy"
            //});

            //$(".dx-numberbox").dxTextBox({
            //    mask: '####.00',
            //    placeholder: 'Enter Cost'
            //})

            //$("#txtFecha").dxDateBox({
            //    formatString: "dd/MM/yyyy"
            //});



            //$(".numerico").dxNumberBox({
            //    //max: totalproductQuantity,
            //    //min: 0,
            //    value: 16,
            //    showSpinButtons: true,
            //    onKeyPress: function (e) {
            //        var event = e.jQueryEvent,
            //            str = String.fromCharCode(event.keyCode);
            //        if (!/[0-9]/.test(str))
            //            event.preventDefault();
            //    }
            //});
            

        });

    };

