var Controls = function (element, conf) {
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
        TextArea: 9,
        SubirDocumentos: 10
    };

    this.element = element;
    this.conf = conf;
    this.Sections = [];
    this.ActionSave = '/Controles/Guardar/';
    this.ActionGet = '/Controles/Get/';
    this.ActionDataSource = '/DataSource/Get/';
    //Inicializa el proyecto
    this.init();

    return {
        getEditorType: $.proxy(this.getEditorType, this),
        getDataSource: $.proxy(this.getDataSource, this),
        init: $.proxy(this.init, this),
        save: $.proxy(this.save, this),
        get: $.proxy(this.get, this)
    }

};



$.fn.nzControl = function () {

    if (arguments[0] != undefined && typeof arguments[0] === 'object') {

        this.data("nzControls", new Controls(this, arguments[0]));

        return this;
    }
    else if (arguments[0] === "Save") {
        //Ejecuta el metodo de guardado
        var objForm = $(this).data("nzControls");
        //se pasa el contecto
        objForm.save(this, arguments[1]);

    }
    else if (arguments[0] == "Get" || typeof arguments[1] === 'object') {
        //Ejecuta el metodo de guardado
        var objForm = $(this).data("nzControls");
        //se pasa el contecto

        return objForm.get(this);

    }
    else return undefined;
};






