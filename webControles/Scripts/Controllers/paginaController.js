var PaginaController = function () {

    Inicializar = function (funPaginacion) {
        $(document).on('click', '#aPagina', function (event) {

            event.preventDefault();
            //GUARDAR NUMERO DE PAGINA
            var url = $(this).attr('href');
            funPaginacion(url);


        });


    }
    return {
        Inicializar: Inicializar
    }
}