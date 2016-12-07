var actions = [];


var BusquedaController = function () {

    var varbusquedaService  = new BusquedaService();
    var varPaginaController = new PaginaController();

     Buscar = function (url) {
         //Mostrar Loading
         $('#TablaLoad').show();
         varbusquedaService.Buscar(url);

         //Ocultar Loading
        $('#TablaLoad').hide();
    },

    CambioPagina = function() {
        if (varPaginaController == undefined) varPaginaController = new PaginaController();
        varPaginaController.Inicializar(function(url) {
            url = url + "&&TextoBuscar=" + $("#txtTextoBuscar").val();
            Buscar(url);
        });

    }

    Editar = function (url, varIdBandeja) {
        //Mostrar Loading
        $('#TablaLoad').show();
        varbusquedaService.CargarBandeja(url, varIdBandeja, 
            function(result) {
                $('#divLista').html(result);
                //Ocultar Loading
                $('#TablaLoad').hide();
        });
    }


    return{
        
        Buscar: Buscar,
        Editar: Editar,
        CambioPagina: CambioPagina
    }
}