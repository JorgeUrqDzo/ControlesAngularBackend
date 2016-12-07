var Visualizador = (function () {

    var panelType = {
        Normal: "normal"
    }

    var panelName = {
        Normal: "panelTemplateNormal"
    }

    var _doTemplatePanel = function (namePanel) {
        var panelTemplate = $('#' + namePanel).html();
        $('#' + config.panelElement).html(panelTemplate);
    }

    var _getDocumentos = function (params) {

        $.ajax({
            async: false,
            url: "http://localhost:64632/api/Visualizador/GetDocuments" + params,
            success: function (result) {

                $("#ListaDoc").html(" ");

                if (result != "") {
                    $("#ListaDoc").html(result);
                    $("#ListaDoc span[rel='tooltip']").tooltip();
                    $($("div#ListaDoc > div > table > tbody > tr > td > span.btnSelectDocumento")[0]).click;
                }
            },
            failure: function (result) {
                $("#divListaDoc").html("Error :(");
            }
        });
    }
    var _goDownLoadPage = function () {
        $.ajax({
            async: false,
            type: "get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.IdDocumento != null) {
                    window.location.href = response.Pagina;
                }
            },
            error: function (response) {
                console.log("Ocurrio un error al intentar descargar el archivo.");
            }
        });
    }

    var _setDocContent = function (extencion, nombreArchivo, identificador) {

        var divCont = document.getElementById("ContentDocs");
        var contObject = "<embed type=\"{type}\" data=\"data:{type};base64," + nombreArchivo + "\" style=\"width: 100%; height: 80vh;\"></embed>";

        switch (extencion.toLowerCase()) {
            case "pdf":
            case "xml":
            case "txt":
                divCont.innerHTML = "<iframe src=\"http://localhost:64632/api/Visualizador/GetPdf?iddoc=" + identificador + "\" Style=\"width: 100%; height: 100vh;\"></iframe>";
                break;
            case "png":
            case "jpg":
            case "gif":
            case "bmp":
            case "jpeg":
                divCont.innerHTML = "<img src=\"data:image/" + extencion.replace(".", "") + ";base64," + nombreArchivo + "\" style=\"width: 100%; height: auto;\">";
                break;
            default:
                divCont.innerHTML = "Formato no soportado.";
                break;
        }

    }
        var _getDocumentById = function (IdTablaRelacion, IdRelacion) {
        var params;
            params = "?TipoConsulta=Id&IdTablaRelacion=" + IdTablaRelacion + "&IdRelacion=" + IdRelacion;
        _getDocumentos(params);
        }

    var _getDoc = function (IdDocumento) {
        var params = "?namemethod=cargarDocumentos&TipoConsulta=IdDoc&IdDocumento=" + IdDocumento;
        _getDocumentos(params);
    }

    var init = function (conf) {

        switch (config.panelMode.toLowerCase()) {
            case panelType.Normal:
                _doTemplatePanel(panelName.Normal);
                _getDocumentById(config.IdTablaRelacion, config.IdRelacion, config.IdActividad, conf.Modulo, conf.Codigo);
                break;
        }

    }

    return {
        Inicializar: init,
        showContentDoc: _setDocContent,
        downLoadDoc: _goDownLoadPage,
        showInner: _getDoc
    }

})();