var PaginacionModelApi = function(strController, strAction, intTotalPaginas, IntPaginaActual, intTotalElementos, intLimitePaginas)
{
    this.TotalPaginas;
    this.LimitePaginas;
    this.PaginaActual;
    this.TotalElementos;
    this.LstPaginas = [];
    this.MostrarPaginacion;
    this.PrimeraHoja;
    this.UltimaHoja;
    this.PaginasVista;
    this.Controller;
    this.Action;
    this.PaginacionModel(strController, strAction, intTotalPaginas, IntPaginaActual, intTotalElementos, intLimitePaginas);
    return {
        lstPaginas: this.LstPaginas,
        MostrarPaginacion: this.MostrarPaginacion,
        PaginaActual:this.PaginaActual,
        Controller: this.Controller,
        Action: this.Action,
        TotalPaginas:this.TotalPaginas
    }
}
PaginacionModelApi.prototype.PaginacionModel = function (strController, strAction, intTotalPaginas, IntPaginaActual, intTotalElementos, intLimitePaginas) {
    var context = this;
    context.Controller = strController;
    context.Action = strAction;
    context.TotalElementos = intTotalElementos;
    context.PaginaActual = IntPaginaActual;
    context.TotalPaginas = intTotalPaginas;
    context.LimitePaginas = intLimitePaginas;
    context.PaginasVista = 10;
    context.CalcularPaginas(context);

};
PaginacionModelApi.prototype.CalcularPaginas = function (context) {
    var intPaginaInicial = 1;
    var intPaginasTotales = 1;
    //Caso 1.- Solo una pagina, entonces no se crea la paginación
    if (context.TotalPaginas > 1) {
        var intTotal = context.PaginaActual / context.PaginasVista;
        intPaginaInicial = Math.floor(intTotal) * context.PaginasVista;
        if (intPaginaInicial == 0)
            intPaginaInicial = 1;
        if ((intPaginaInicial + context.PaginasVista) > context.TotalPaginas)
            intPaginasTotales = context.TotalPaginas;
        else
            intPaginasTotales = intPaginaInicial + context.PaginasVista;
        context.AgregarPaginas(context, intPaginaInicial, intPaginasTotales);
    }
    else 
        context.MostrarPaginacion = false;
};
PaginacionModelApi.prototype.AgregarPaginas = function (context, intPaginaInicial, intTotalPaginas) {
    if (!(intPaginaInicial == 1 && intTotalPaginas == 1)) {
        context.MostrarPaginacion = true;
        for (var i = intPaginaInicial; i <= intTotalPaginas; i++)
            context.LstPaginas.push(i);
    }
    else
        context.MostrarPaginacion = false;
};
