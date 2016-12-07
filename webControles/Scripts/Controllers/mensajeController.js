var MensajeController = function () {
    var messageCommon = new MessageCommon();

    MensajeError = function (Mensaje) {
        var varMensaje =
       "<div class='alert alert-danger' role='alert'> " +
       "<a href='#' class='close' data-dismiss='alert'>&times;</a>" +
        "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'></span> " +
         "<span class='sr-only'>Error:</span>" +
         Mensaje +
       "</div>";
        return varMensaje;
    },
    MensajeExito = function (Mensaje) {

        messageCommon.success(Mensaje);
    },
    MostrarMensajeExito = function (Mensaje) {
        toastr.success(Mensaje);
    },
    MostrarMensajeError = function (Mensaje) {
        var strDivMensaje = MensajeError(Mensaje);
        $('#' + divName).html(strDivMensaje);
    },
    Mensaje = function (mensajeModel) {

        var strDivMensaje = "";
        if (mensajeModel.Error) {
            MensajeError(mensajeModel.Mensaje);
        }
        else {
            MensajeExito(mensajeModel.Mensaje);
        }

    }

    return {
        Mensaje: Mensaje,
        MostrarMensajeExito: MensajeExito,
        MostrarMensajeError: MensajeError
       
    }
}