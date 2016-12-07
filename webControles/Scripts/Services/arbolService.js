var ArbolService = function () {

    Inicializar = function (url, varIdFormulario, funSuccess) {
        var urlFinal = url+ "/" + varIdFormulario;
        $.get(urlFinal, function (result) {
            funSuccess(result);
        });
    }
    return {
        Inicializar: Inicializar
    }
}