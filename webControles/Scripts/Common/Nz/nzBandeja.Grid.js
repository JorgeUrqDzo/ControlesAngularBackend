
///Inicializa el control
Bandeja.prototype.init = function () {
    var context = this;


    this.getDataSource(function (result) {

        //LImpia los elementos
        $(context.element.selector + " .ContentPagination").html('')
        $(context.element).html('');
        $(context.tableCont).html('');
        if (result.consulta === undefined || result.consulta === null) {
            $(context.element).html(context.mensajeNoResultados);
        }
        else {


            //for (var i = 0; i < result.consulta.length; i++)
            context.DatosConsulta = result.consulta;
            context.ColumnasAMostrar = [];
            for (var j = 0; j < result.bandejaColumnba.length; j++) {
                context.ColumnasAMostrar.push({
                    nombreColumna: result.bandejaColumnba[j]["ColumnaBD"],
                    IdTipoColumna: result.bandejaColumnba[j]["IdTipoColumna"],
                    PaginaLink: result.bandejaColumnba[j]["PaginaLink"],
                    parametros: result.LinkParametros,
                    TituloColumna: result.bandejaColumnba[j]["TituloColumna"],
                    clase: result.bandejaColumnba[j]["Clase"],
                    TipoLink: result.bandejaColumnba[j]["TipoLink"]
                });
            }
            context.registroPorPagina = result.bandeja.NumeroPaginas;
            context.paginacionDatos = result.paginacion;
            context.tableCont.addClass(result.bandeja.ClaseBandeja);
            context.numRegistros = result.consulta[0].Registros;
            context.printTable(context);


        }
        if (context.conf.Complete !== undefined)
            context.conf.Complete();

    });
};

Bandeja.prototype.initFiltros = function (funCallback) {


    //if (this.conf.ActionFormularioUUID !== undefined) {

    var ActionFormularioUUID = this.conf.Action + this.ActionFormulario;
    var ActionControles = this.conf.Action;
    var context = this;
    $.ajax({
        type: 'POST',
        async: true,
        traditional: true,
        url: ActionFormularioUUID,
        dataType: 'json',
        data: JSON.stringify({ UUID: this.conf.UUID }),
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            if (result !== undefined && result !== "") {

                context.FiltrosGenerados = true;

                //Inicializa los filtros pasando el UUID del formulario
                $('#' + context.conf.ContenedorFiltros).nzControl({
                    Action: ActionControles,
                    UUID: result,
                    Complete: function () {
                        funCallback();
                    }
                });

            }
        },
        error: function (result) {
        }
    });

    //}



};
Bandeja.prototype.asignarFiltros = function () {

    //Indica si se generaron los filtros cuando se inicio el control
    if (this.FiltrosGenerados) {

        this.conf.filtros = $('#' + this.conf.ContenedorFiltros).nzControl("Get");
    }

};

///Llama a la api para obtener la informacion
Bandeja.prototype.getDataSource = function (funSuccess) {

    //Asigna los filtros
    this.asignarFiltros();
    var varAction = this.conf.Action + this.ActionBandeja;
    $.ajax({
        type: 'POST',
        traditional: true,
        url: varAction,
        dataType: 'json',
        data: JSON.stringify({ UUID: this.conf.UUID, filtros: this.conf.filtros, pagina: this.pagina }),
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            funSuccess(result);
        },
        error: function (result) {
            funSuccess(result);
        }
    });
};
Bandeja.prototype.printTable = function (context) {
    var valor;
    var link = "";
    var header = $('<thead></thead>');
    var row = $('<tr ></tr>');
    for (var i = 0; i < context.ColumnasAMostrar.length; i++)
        row.append($('<th></th>').addClass('bar').text(context.ColumnasAMostrar[i].TituloColumna));
    header.append(row);
    context.tableCont.append(header);
    for (var j = 0; j < context.DatosConsulta.length; j++) {
        row = $('<tr></tr>');
        for (var i = 0; i < context.ColumnasAMostrar.length; i++) {
            if (context.DatosConsulta[j][context.ColumnasAMostrar[i].nombreColumna]) valor = context.DatosConsulta[j][context.ColumnasAMostrar[i].nombreColumna];
            else valor = context.ColumnasAMostrar[i].nombreColumna;


            if (!context.ColumnasAMostrar[i].TipoLink) {
                link = context.ColumnasAMostrar[i].PaginaLink;
            }
            else {
                link = context.DatosConsulta[j][context.ColumnasAMostrar[i].PaginaLink];
            }

            row.append($('<td></td>').append(context.TipoColumna(context, valor, context.ColumnasAMostrar[i].IdTipoColumna, link, context.ColumnasAMostrar[i].parametros, context.ColumnasAMostrar[i].clase, context.DatosConsulta[j])));
        }
        context.tableCont.append(row);
    }
    context.element.append(context.tableCont);
    context.element.append(context.Paginacion(context.paginacionDatos, context.pagina, context));
};

Bandeja.prototype.Paginacion = function (objModPaginacion, Pagina, context) {
    var Model = new PaginacionModelApi("Busqueda", "Lista", objModPaginacion.Paginas, Pagina, objModPaginacion.TotalRegistros, context.registroPorPagina);
    if (Model.MostrarPaginacion) {
        var ul = $('<ul></ul>').addClass("pagination"),
            li = $('<li></li>'),
            a = $('<a role="menuitem" tabindex="-1" class="paginaClick"></a>').data("Pagina", 1).text("<");
        li.append(a);
        ul.append(li);
        for (var i = 0; i < Model.lstPaginas.length; i++) {
            if (Model.PaginaActual === Model.lstPaginas[i]) {
                li = $('<li></li>').addClass("active");
                a = $('<a role="menuitem" tabindex="-1" class="paginaClick"></a>').data("Pagina", Model.lstPaginas[i]).text(Model.lstPaginas[i]);
            }
            else {
                li = $('<li></li>');
                a = $('<a role="menuitem" tabindex="-1" class="paginaClick"></a>').data("Pagina", Model.lstPaginas[i]).text(Model.lstPaginas[i]);
            }
            li.append(a);
            ul.append(li);
        }
        li = $('<li></li>');
        a = $('<a role="menuitem" tabindex="-1" class="paginaClick"></a>').data("Pagina", Model.TotalPaginas).text(">");
        li.append(a);
        ul.append(li);
        context.tablePaginacion.html('');
        context.tablePaginacion.append($('<div></div>').addClass("active").text(context.numRegistros + " registros encontrados"));
        context.tablePaginacion.append(ul);
        $(document).off("click", context.element.selector + " .paginaClick");
        $(document).on("click", context.element.selector + " .paginaClick", function (e) {
            context.pagina = $(this).data("Pagina");
            context.DatosConsulta = [];
            context.ColumnasAMostrar = [];
            context.init();
        });
    }
    return context.tablePaginacion;
}

Bandeja.prototype.TipoColumna = function (context, valor, tipoColumna, link, parametros, clase, datos) {
    var result = '';
    switch (tipoColumna) {
        case context.tipoColumna.Texto:
            result = $('<p></p>').addClass(clase).text(valor);
            break;
        case context.tipoColumna.Button:
            result = $('<a></a>').addClass(clase).text(valor);
            break;
        case context.tipoColumna.Link:
            if (parametros !== null && parametros !== undefined) {
                if (parametros[0].IdTipoParametro === 1)
                    result = $('<a href="' + link + "/?" + context.getLinkParametros(parametros, valor, datos) + '"></a>').addClass(clase).text(valor);
                else
                    result = $('<a class="postlink"></a>').data("link", link).data("valor", valor).data("parametros", parametros).data("datos", datos).addClass(clase).text(valor);
            }
            break;
        case context.tipoColumna.Imagen:
            result = $('<img class="dxDataGridImage" src="' + valor + '"/>');
            break;
        case context.tipoColumna.Imagen64:
            result = $('<img src="data:image/png;base64,' + valor + '"/>').addClass("dxDataGridImage");
            break;
        case context.tipoColumna.CheckBox:
            if (valor === true) result = $('<input type="checkbox" checked>').addClass(clase);
            else result = $('<input type="checkbox">').addClass(clase);
            break;
    }
    return result;
}
Bandeja.prototype.getLinkParametros = function (linkParametros, valor, datos) {
    var strParametros = '';
    for (var i = 0; i < linkParametros.length; i++) {
        if (linkParametros[i].Valor !== '')
            strParametros += linkParametros[i].Parametro + '=' + linkParametros[i].Valor;
        else
            strParametros += linkParametros[i].Parametro + '=' + datos[linkParametros[i].Parametro];
        if (i !== (linkParametros.length - 1))
            strParametros += '&';
    }
    return strParametros;
}
function postLinkParametros(PostParametros, valor, link, datos) {
    var inputs = '';
    for (var i = 0; i < PostParametros.length; i++)
        if (PostParametros[i].Valor !== '')
            inputs += '<input type="hidden" name="' + PostParametros[i].Parametro + '" value="' + PostParametros[i].Valor + '" />';
        else
            inputs += '<input type="hidden" name="' + PostParametros[i].Parametro + '" value="' + datos[PostParametros[i].Parametro] + '" />';
    $("body").append('<form action="' + link + '" method="post" class="poster">' + inputs + '</form>');
    $(".poster").submit();
}
$(document).on("click", ".postlink", function (e) {
    postLinkParametros($(this).data("parametros"), $(this).data("valor"), $(this).data("link"), $(this).data("datos"));
});


