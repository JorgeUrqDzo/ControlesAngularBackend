var BusquedaService = function() {

    Buscar = function(url) {

            $.get(url, function(result) {
                $('#divLista').html(result);
            });
    },

        CargarBandeja = function(UrlCreate, varIdBandeja, funBandeja) {
        var url = UrlCreate + "/" + varIdBandeja;

        $.get(url, function(result) {
            funBandeja(result);
        });
        }

    var CargarXId = function (objBusquedaModel, url, funCallBack) {
        $.ajax({
            async: true,
            url: url,
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(objBusquedaModel),
            contentType: 'application/json',
            success: function (data) {
                funCallBack(data);
            },
            error: function (errorThrow) {

            }
        });
    }

    return {
                Buscar: Buscar,
                CargarBandeja: CargarBandeja,
                CargarXId: CargarXId

            }
        }

