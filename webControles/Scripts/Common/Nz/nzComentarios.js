var Comentarios = function (element, conf) {
    var element = element;
    var conf = conf;
    var url = url;

    cargarComentarios = function () {
        var action = conf.action,
        IdTablaDocumento = conf.IdTablaDocumento;
        IdDocumento = conf.IdDocumento;
        IdUsuario = conf.IdUsuario;

        Cargar(action, IdTablaDocumento, IdDocumento, IdUsuario, function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) 
                if (data[i].IdComentario != 0) {
                    $("#tablaComentarios").append('<tbody><tr><td class="glyphicon glyphicon-user active" width="80%">' + "  Usuario: " + '' + data[i].Usuario + '</td><td class="glyphicon glyphicon-time active" width="20%">' + data[i].FechaCreacion + '</td></tr><tr><td class="glyphicon glyphicon-comment" width="80%">' + " " + '' + data[i].Comentario + '</td></tr></tbody>');
                }
           
        });

    },        
      Cargar = function (action, obj1, obj2, obj3, funSuccess) {
          var objAction = action + "?IdTablaDocumento=" + obj1 + "&IdDocumento=" + obj2 + "&IdUsuario=" + obj3;
          $.get(objAction, function (result) {
              funSuccess(result)
          });
      }
 
           return {
               cargarComentarios: cargarComentarios,
           }
};
$.fn.nzComentarios = function (conf) {
  
    
        if (conf != undefined) {
            g = new Comentarios(this, conf);
            g.cargarComentarios();
            return this;
        }
        else return undefined;
        
};

