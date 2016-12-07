
//var NezterControls = NezterControls || {};
    Controls = function (element, conf) {
        //Validaciones
        if (conf.UUID == undefined) throw "UUID no definido";
        if (conf.Action == undefined) throw "Action no definido";
        ///Objecto que contiene los tipos de controles
        this.enumTipos = {
            Texto: 1,
            Numerico: 2,
            Fecha: 3,
            CheckBoxList: 4,
            RadioButtonList: 5,
            List: 6,
            Multiseleccion: 7,
            Switch: 8,
            TextArea: 9
        };

        this.element = element;
        this.conf = conf;
        this.Sections = [];

        //Inicializa el proyecto
        this.init();

        return {
            getEditorType: $.proxy(this.getEditorType, this),
            getDataSource: $.proxy(this.getDataSource, this),
            init: $.proxy(this.init, this)
        }

    };
  


    $.fn.nzControl = function (conf) {

        if (conf != undefined) {
    
            $.data(this, "nzControls", new Controls(this, conf));

            return this;
        }
        else return undefined;
    };
