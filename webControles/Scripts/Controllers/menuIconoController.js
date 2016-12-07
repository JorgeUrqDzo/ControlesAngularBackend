var MenuIconoController = function () {

    var objMenuIconoService = new MenuIconoService();
    var msj = new MessageCommon();

    var getIconos = function (url) {
      // $('#TablaLoad').show();
        objMenuIconoService.getIconos(url, function (data) {
            $('.selectpicker').selectpicker({
                size: 8
            });
           
            for (var i in data) {
                $("#selIdMenuIcono").append('<option data-icon="' + data[i].Icono + '" value="' + data[i].IdMenuIcono + '"></option>');
             
            }
         
           
        //    $('#TablaLoad').hide();
          $('.selectpicker').selectpicker('refresh')
        });
    }

    return{
        getIconos: getIconos
}
}