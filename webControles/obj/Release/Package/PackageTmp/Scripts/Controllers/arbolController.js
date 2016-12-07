

//Recibe el calBack que se ejecutara cuando se seleccione un nodo del Control
var ArbolController = function (strUrl, strUrlSeccion, strDivId, funNodeSelected) {



    var varArbolService = new ArbolService();
    var _funNodeSelected = funNodeSelected;
    var _DivId = strDivId;
    var _Url = strUrl;
    var _UrlSeccion = strUrlSeccion;

    var Inicializar = function (varIdFormulario) {

        if (varIdFormulario != undefined && varIdFormulario != null && varIdFormulario != '') {
            varArbolService.Inicializar(_Url, varIdFormulario, function (result) {
                $('#' + _DivId).treeview({
                    data: result,
                    levels: 2,
                    onNodeSelected: _funNodeSelected,
                    onChangePosition: function (event, id, order) {

                        var objSeccionController = new SeccionController();
                        objSeccionController.ActualizarOrden(_UrlSeccion, id, order);

                    }
                });

            });
            

           
        }
        

    }

    return {
        Inicializar: Inicializar
    }
}
