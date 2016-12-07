
    var MenuDinamicoService = function () {
        Cargar = function (action, objMenuDinamico, funSuccess) {
            var objAction = action + "/?tipoMenu=" + objMenuDinamico;
            $.get(objAction, function (result) {
                funSuccess(result)
            });
        }
        return {
            Cargar: Cargar
        }
    }