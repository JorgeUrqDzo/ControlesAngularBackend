var nzActividades = function () {

    SiguienteActividad = function (url, intIdTablaDocumento, intIdDocumento, intIdEncActividad, intIdEncProceso, bolAprobado, funCallback) {

        ///Hace el movimiento de las actividades
        var objModApiActividadProceso = {
            IdTablaDocumento: intIdTablaDocumento,
            IdDocumento: intIdDocumento,
            IdEncActividad: intIdEncActividad,
            IdEncProceso: intIdEncProceso,
            Aprobado: bolAprobado
        };

        $.ajax({
            type: 'POST',
            url: url+'/ActividadProceso/Save',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(objModApiActividadProceso),
            dataType: 'json',
            success: function (result) {
                funCallback(result);
            },
            error: function () {

            }
        });

    }
    return {
        SiguienteActividad: SiguienteActividad

    }


}