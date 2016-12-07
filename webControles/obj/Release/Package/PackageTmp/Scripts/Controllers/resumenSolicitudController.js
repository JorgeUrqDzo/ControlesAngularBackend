var ResumenSolicitudController = function () {
    var objMessageCommon = new MessageCommon();

    var onErrorGetInfo = function (error) {
        objMessageCommon.errorTitle("Ocurrió un error al consultar la información.", "Error");
    }

    var initActions = function (objactions) {
        actions = objactions;
    }

    var initConfigVisualizador = function (configuracion) {
        config = configuracion;
    }

    var initFolio = function (objactions) {
        folio = objactions;
    }

    var cargarPartialGridPdfSolicitud = function (strFolio, container) {

        var objResumenSolicitudService = new ResumenSolicitudService();

        objResumenSolicitudService.CargarPartialGridPdfSolicitud(strFolio, actions.actionUrlGridSolicitudPdf, function (result) {

            container.html(result);
        });
    }

    var cargarPartialAnexo = function (container) {

        var objResumenSolicitudService = new ResumenSolicitudService();

        objResumenSolicitudService.CargarPartialAnexoSolicitud(actions.actionUrlAnexoSolicitud, function (result) {
            container.html(result);
        });
    }

    var getDocumentosInbox = function () {
        var objResumenSolicitudService = new ResumenSolicitudService();
        objResumenSolicitudService.GetDocumentosInbox(actions.actionDocumentosInbox, folio, function (result) {
            $("#divInboxDocumentos").html(result);
        }, function (error) {
            onErrorGetInfo(error);
        });
    }


    return {
        InitFolio: initFolio,
        InitActions: initActions,
        CargarPartialGridPdfSolicitud: cargarPartialGridPdfSolicitud,
        CargarPartialAnexo: cargarPartialAnexo,
        InitConfigVisualizador: initConfigVisualizador,
        getDocumentosInbox: getDocumentosInbox
    }

}