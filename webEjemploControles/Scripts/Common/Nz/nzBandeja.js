(function ($) {
    Bandeja = function (element, conf) {
        if (conf.UUID === undefined) throw "UUID no definido";
        if (conf.Action === undefined) throw "Action no definido";
        this.tipoColumna = {
            Texto: 1,
            Button: 2,
            Link: 3,
            Imagen: 4,
            Imagen64: 5,
            CheckBox:6
        };
        this.pagina = 1;
        this.registroPorPagina = conf.RegistrosPorPagina;
        this.filtros = conf.filtros;
        //this.contenedorFiltros = conf.contenedorFiltros;
        this.element = element;
        this.conf = conf;
        this.DatosConsulta = [];
        this.ColumnasAMostrar = undefined;
        this.paginacionDatos;
        this.numRegistros;
        this.mensajeNoResultados = '<center>No se encontraron resultados</center>';;
        this.tableCont = $('<table class="tableBandejas"></table>');
        this.tablePaginacion = $('<div class="ContentPagination" style="text-align: right;"></div>');
        this.Search = undefined;
        this.FiltrosGenerados = false;

        this.ActionFormulario = '/Formulario/Get';
        this.ActionBandeja = '/Grid/Get';
        
        var context = this;
        if (this.initFiltros !== undefined) {
            this.initFiltros(function () {
                context.init();
            });
        }
        else {
            this.init();
        }
        
        return {
            DatosConsulta: $.proxy(this.DatosConsulta, this),
            ColumnasAMostrar: $.proxy(this.ColumnasAMostrar, this),
            init: $.proxy(this.init, this),
            getDataSource: $.proxy(this.getDataSource, this)
        }
    };
   
    $.fn.nzBandeja = function (conf)
    {
        if (arguments[0] !== undefined && typeof arguments[0] === 'object') {

            $(this.selector + " .tableBandejas").remove();
            $(this.selector + " .ContentPagination").remove();
            this.data("nzBandeja", new Bandeja(this, arguments[0]));

            return this;
        }
        else if (arguments[0] === "Search") {
            //$(this.selector + " .tableBandejas").remove();
            //$(this.selector + " .ContentPagination").remove();
            var objForm = $(this).data("nzBandeja");
            //se pasa el contexto
            objForm.init();
        }
    };
})(jQuery);