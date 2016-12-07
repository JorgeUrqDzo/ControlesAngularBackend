(function ($) {
    var MenuDinamico = function (element, conf) {
        var contenedor = element;
        var parametros = conf;
        cargarMenu = function () {
            var action = parametros.accion,
                tipoMenu = parametros.tipoMenu;
            Cargar(action, tipoMenu, function (data) {
                var padres = [], hijos = [];
                
                //se definen los padres del menu
                contenedor.append('<ul class="sidebar-menu selectpicker" id="menuDinamico"></ul>');
                for (var i = 0; i < data.length; i++)
                    if (data[i].IdMenuPadre == 0) {
                        padres.push(data[i].IdMenu);
                        $("#menuDinamico").append('<li class="treeview"><a href="' + data[i].Url + '" id="liPadre-' + data[i].IdMenu + '"><i class="glyphicon ' + data[i].Icono + '"></i> <span>' + data[i].Descripcion + '</span></a><ul id="menuHijos-' + data[i].IdMenu + '"  style="display: none;"></ul> </li>');
                    }
                
                //se definen los hijos del menu
                for (var i = 0; (i < data.length) && (padres.length < data.length); i++)
                    for (var j = 0; j < padres.length; j++)
                        if (data[i].IdMenuPadre != 0 && data[i].IdMenuPadre == padres[j]) {
                            hijos.push(data[i].IdMenu);
                            
                            $("#liPadre-" + padres[j]).append(' <i class="fa fa-angle-left pull-right"></i> ');
                            $("#menuHijos-" + padres[j]).addClass("treeview-menu menu-open");
                            $("#menuHijos-" + padres[j]).append('<li><a href="' + data[i].Url + '"  id="liHijo-' + data[i].IdMenu + '"><i class="glyphicon ' + data[i].Icono + '"></i><span>' + data[i].Descripcion + '</span></a> <ul id="menuNietos-' + data[i].IdMenu + '" style="display: none;"></ul> </li>');
                        }
                //se definen los nietos del menu
                for (var i = 0; i < data.length && (hijos.length + padres.length) < data.length; i++)
                    for (var j = 0; j < hijos.length; j++)
                        if (data[i].IdMenuPadre != 0 && data[i].IdMenuPadre == hijos[j]) {
                            $("#liHijo-" + hijos[j]).append(' <i class="fa fa-angle-left pull-right"></i> ');
                            $("#menuNietos-" + hijos[j]).addClass("treeview-menu menu-open");
                            $("#menuNietos-" + hijos[j]).append('<li><a href="' + data[i].Url + '"><i class="glyphicon ' + data[i].Icono + '"></i><span>' + data[i].Descripcion + '</span></a> <ul id="menuNieto-' + data[i].IdMenu + '"></ul> </li>');//*/ 
                        }
            });
        },
        Cargar = function (action, objMenuDinamico, funSuccess) {
            var objAction = action + "/?tipoMenu=" + objMenuDinamico;
            $.get(objAction, function (result) {
                funSuccess(result)
            });
        }
        return {
            cargarMenu: cargarMenu
        }
    };
    $.fn.nzMenuDinamico = function (conf) {
        if (conf != undefined) {
            g = new MenuDinamico(this, conf);
            g.cargarMenu();
            return this;
        }
        else return undefined;
    };
})( jQuery );

