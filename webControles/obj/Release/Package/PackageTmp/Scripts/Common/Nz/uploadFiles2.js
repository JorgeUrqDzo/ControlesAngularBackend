function uploadFile2(nombre) {
    var self = this;
    var requerimientos = [],
        panelType = {
            Large: 'large',
        },
        panelName = {
            LargeFiles: 'panelUploadLargeFiles'
        },
        urlApi = {
            urlInit2: 'UploadFile/InitControl2'
        },
        totalFiles = [];
        callbackResp = null;

    function UploadFileChunk(Chunk, FileName, InfoFile, IdRequisito) {

        var fullName = InfoFile.OnlyName + "." + InfoFile.tokenFile + "." + InfoFile.ExtensionArchivo;
        var FD = new FormData();

        FD.append('Id', '191983274');
        FD.append('FileName', InfoFile.NombreArchivo);
        FD.append('FullName', fullName);
        FD.append('FileExtension', InfoFile.ExtensionArchivo);
        FD.append('ContentType', InfoFile.DocumentContent);
        FD.append('Size', InfoFile.DocumentTamano);
        FD.append('tokenFile', InfoFile.tokenFile);
        FD.append('IsFisico', InfoFile.IsFisico);
        FD.append('file', Chunk, FileName);
        FD.append('TablaRelacion', base.TablaRelacion);
        FD.append('IdRelacion', base.IdRelacion);
        FD.append('IdUsuarioCreacion', base.IdUsuarioCreacion);

        $.ajax({
            type: "POST",
            url: config.domainApi + urlApi.urlInit2,
            contentType: false,
            processData: false,
            data: FD,
            success: function (response) {

                if (response.Success === true) {
                    var tabla = $("div#" + 'control' + config.panelElement + " > div.table");
                    $(tabla).find("table.table > tbody").html('');
                    $('#control' + config.panelElement).children().eq(1).val('');
                    totalFiles = [];
                   
                    $('#TablaLoad').hide();
                    alert("Documento se guardo con éxito");
                    if (callbackResp !== null && callbackResp !== undefined)
                        callbackResp();
                } 
            }
        });
    }

    function UploadFile(TargetFile, infoFile, IdRequisito) {

        var FileChunk = [];
        var file = TargetFile;
        var MaxFileSizeMB = 3;
        var BufferChunkSize = MaxFileSizeMB * (1024 * 1024);
        var ReadBuffer_Size = 1024;
        var FileStreamPos = 0;
        var EndPos = BufferChunkSize;
        var Size = file.size;
        var objetoFile = infoFile;

        while (FileStreamPos < Size) {
            FileChunk.push(file.slice(FileStreamPos, EndPos));
            FileStreamPos = EndPos;
            EndPos = FileStreamPos + BufferChunkSize;
        }

        var TotalParts = FileChunk.length;
        var PartCount = 0;

        while (chunk = FileChunk.shift()) {
            PartCount++;

            var FilePartName = objetoFile.OnlyName + "." + objetoFile.tokenFile + "." + objetoFile.ExtensionArchivo + ".part_" + PartCount + "." + TotalParts;
            UploadFileChunk(chunk, FilePartName, objetoFile, IdRequisito);
        }
    }
    function closeLoadEvent(evt) {
        var files = evt.target.files;

        for (var i = 0; i < files.length; i++) {
            var documento = files[i];
            totalFiles.push(documento);

        }
        _makeTableDocsLarge();
     
    }
         function getNumberDate() {
         var NumberDate = new Date();
         var strNumber = NumberDate.getTime().toString();
         return strNumber;
    }

    var _doTemplate = function (namePanel) {
        var panelTemplate = $('#' + namePanel).html()
            .replace('@Id', 'control' + config.panelElement)
            .replace('uploadFile', 'uploadFile' + config.panelElement)
            .replace('btnGuardar', 'btnGuardar' + config.panelElement)
            .replace('btnUpload', 'btnUpload' + config.panelElement)
            .replace('divSubirDocumentos', 'divSubirDocumentos' + config.panelElement)

        $('#' + config.panelElement).html('');
        $('#' + config.panelElement).html(panelTemplate);
        $('#btnGuardar' + config.panelElement).hide();
     
    }
     
    var _makeTableDocsLarge = function () {

        if (totalFiles.length >= 0) {
            var tabla = $("div#" + 'control' + config.panelElement + " > div.table");

            if (!$(tabla).length) {
                tabla = $("div#" + 'control' + config.panelElement);

                estructuraTabla = "";
                estructuraTabla += "<div class=\"table\">";
                estructuraTabla += "   <table id='table" + config.panelElement + "' class=\"table table-bordered\">";
                estructuraTabla += "       <tbody>";
                estructuraTabla += "       </tbody>";
                estructuraTabla += "   </table>";
                estructuraTabla += "</div>";

                $(tabla).append(estructuraTabla);
                tabla = $(tabla).find("div.table > table.table > tbody");
            }
            else {
                tabla = $(tabla).find("table.table > tbody").html('');
            }

            if ($(tabla).length) {

                for (var i = 0; i < totalFiles.length; i++) {
                    estructuraRenglon = "";
                    estructuraRenglon += "<tr id=\"" + i + "\" class=\"documento\">";
                    estructuraRenglon += "  <td>";
                    estructuraRenglon += "    <span id=\"" + i + "\" class=\"fa fa-times fa-lg\" rel=\"" + i + "\"></span>";
                    estructuraRenglon += "    <input type='hidden' id='flddoc' value=\"" + i + "\" />";
                    estructuraRenglon += "  </td>";
                    estructuraRenglon += "  <td class=\"namefile\">";
                    estructuraRenglon += "    <span>" + totalFiles[i].name + "</span>";
                    estructuraRenglon += "  </td>";
                    estructuraRenglon += "</tr>";

                    $(tabla).append(estructuraRenglon);
                }
                _setEventTableDocs();
            }
        }
    }

    var _setInfoDocument = function (documento) {
        var objetoFile = {};

        if (documento !== undefined) {
            var file = documento;
            var splitName = file.name.substring(file.name.lastIndexOf('/') + 1, file.name.lastIndexOf('.'));
            objetoFile.tokenFile = getNumberDate();
            objetoFile.OnlyName = splitName;
            objetoFile.NombreArchivo = file.name;
            objetoFile.ExtensionArchivo = file.name.substr((file.name.lastIndexOf('.') + 1));
            objetoFile.DocumentContent = file.type;
            objetoFile.DocumentTamano = file.size;
            objetoFile.IsFisico = (file.size > 10000000) ? true : false;
        }
        return objetoFile;
    }

    var _setDropDownListRequeriment = function (id, Elemento) {
        var combo = "";
       
        if (requerimientos.length > 0) {
            for (x in requerimientos) {
                var y = parseInt(x);
            }
        }
        return combo;
    }

    var _setEventGuardarLargeFiles = function (elementoButton) {
        $('#' + elementoButton).click(function () {

            var filess = totalFiles;
            var Requisitos = [];

            if (filess.length > 0)
                $('#TablaLoad').show();

            var tabla = $('#table' + config.panelElement);
            var trs = $('#table' + config.panelElement + ' tr ');

            [].forEach.call(trs, function (tr) {
                var row = tr;
                if (!isNaN($(row).children().eq(2).children().eq(0).attr('id')))
                    Requisitos.push($(row).children().eq(2).children().eq(0).attr('id'));
                else
                    Requisitos.push(0);
            });

            for (var i = 0; i < filess.length; i++) {
                var documento = filess[i];
                var Id = parseInt(Requisitos[i]);
                UploadFile(documento, _setInfoDocument(documento), Id); 
            }
        });
        
    }
    var _setEventLoadCloseFiles = function () {
    var inputFile = document.getElementById('uploadFile' + config.panelElement).addEventListener('change', closeLoadEvent, false);
    }

    var _setEventTableDocs = function () {

        $("div#" + 'control' + config.panelElement + "> div.table tbody tr").each(function (index) {
            var Id;

            $(this).children("td").each(function (index2) {
                switch (index2) {
                    case 0:
                        var elemento = $(this).children().eq(0);
                        var val = $(this).children().eq(1);
                        $(elemento).click(function () {
                            totalFiles.splice(parseInt($(val).val()), 1);
                            _makeTableDocsLarge();
                        });
                        break;
                    case 2:
                        $(this).change(function () {
                            var conte = _setObtenerControlRequisito(parseInt($(this).children().eq(0).val()));
                            var TdContenedor = $(this).find(":selected").parent().parent();
                            $(this).children().eq(0).remove();

                            TdContenedor.append(conte);
                        });
                        break;
                }
            });
        });
    }

    this.init = function (conf, objbase, callback) {

        if (conf !== 'undefined')
            config = conf;

        if (objbase !== 'undefined')
            base = objbase;

        switch (config.panelMode.toLowerCase()) {
          
            case panelType.Large:
                callbackResp = callback;
                _doTemplate(panelName.LargeFiles);
                _setEventGuardarLargeFiles('btnUpload' + config.panelElement);
                _setEventLoadCloseFiles();
                break;
           
            default: break;
        }
    }

}
