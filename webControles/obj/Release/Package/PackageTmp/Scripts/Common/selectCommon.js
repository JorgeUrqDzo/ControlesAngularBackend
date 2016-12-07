(function() {
    //Constructor
    this.SelectCommon = function() {

        this.Contenedor = null;
        this.LstObjects = null;
        this.CampoValue = null;
        this.CampoText = null;
        this.FirstElementText = null;
        this.FirstElementValue = null;
        this.Success = function() {
        };
        this.Error = function(result) {
        };
    }; //Crea la funcion Load y recibe de parametro un objeto
    SelectCommon.prototype.Load = function (params) {
        function asignarValoresEnviados(source, properties) {
            var property;
            for (property in properties) {
                //Si contiene esta propiedad
                if (source.hasOwnProperty(property)) {
                    source[property] = properties[property];
                }
            }
            //return source;
        }

        asignarValoresEnviados(this, arguments[0]);

        //Validaciones del objeto
        if (!(arguments[0] && typeof arguments[0] === "object")) {

            this.Error("El parámetro enviado no es un objeto");
            return false;
        }
        if (this.LstObjects == null) {
            this.Error("No se proporciono Origen de datos");
            return false;
        }
        if (this.CampoValue == null || this.CampoValue === "") {
            this.Error("CampoValue no puede ir vacío");
            return false;
        }
        if (this.CampoText == null || this.CampoText === "") {
            this.Error("CampoText no puede ir vacío");
            return false;
        }

        // Asigna los valores pasados a las propiedades de ClsNegSelect
        try {
            //Se obtiene el elemento en base al nombre enviado

            var varSelect = document.getElementById(this.Contenedor);

            ///////////////////////VALIDACIONES DE CAMPOS ENVIADOS/////////////////////////
            if (typeof varSelect == 'undefined' || varSelect == null) {
                this.Error("No se encontró el contenedor");
                return false;
            }

            //Borra el contenido del control
            var varContenido = '', varId = '', varText = '';
            var varObjeto = null;
            varSelect.innerHTML = '';

            if (this.FirstElementText !== null) {
                if (this.FirstElementValue === null) {
                    this.FirstElementValue = '';
                }

                varContenido += "<option value= '" + this.FirstElementValue + "' >" + this.FirstElementText + " </option> ";
            }

            //this.LstObjects = JSON.parse(this.LstObjects);
            if (this.LstObjects != undefined && this.LstObjects.length > 0) {
            if (!this.LstObjects[0].hasOwnProperty(this.CampoValue)) {
                    this.Error("El listado no contiene una propiedad llamada " + this.CampoValue);
                    return false;
                }
                if (!this.LstObjects[0].hasOwnProperty(this.CampoText)) {
                    this.Error("El listado no contiene una propiedad llamada " + this.CampoText);
                    return false;
                }

                //recorre el listado de objetos
                for (var i = 0; i < this.LstObjects.length; i++) {
                    //asigna el objeto a la variable
                    varObjeto = this.LstObjects[i];

                    //Asigna las propiedades
                    if (varObjeto.hasOwnProperty(this.CampoValue)) {
                        varId = varObjeto[this.CampoValue];
                    }
                    if (varObjeto.hasOwnProperty(this.CampoText)) {
                        varText = varObjeto[this.CampoText];
                    }

                    varContenido += "<option value= '" + varId + "' >" + varText + " </option> ";
                    varSelect.disabled = false;
                }
            }

            varSelect.innerHTML = varContenido;
            this.Success();
            return true;
        } catch (ex) {
            this.Error("Error Catch" + ex);
        }
    };
}());