var MessageCommon = function() {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    var info = function(message) {
        toastr.info(message);
    }

    var infoTitle = function (message, title) {
        toastr.info(message, title);
    }

    var error = function (message) {
        toastr.error(message);
    }

    var errorTitle = function (message, title) {
        toastr.error(message, title);
    }

    var warning = function (message) {
        toastr.warning(message);
    }

    var warningTitle = function (message, title) {
        toastr.warning(message, title);
    }

    var success = function (message) {
        toastr.success(message);
    }

    var successTitle = function (message, title) {
        toastr.success(message, title);
    }

    return {
        info: info,
        infoTitle: infoTitle,
        error: error,
        errorTitle: errorTitle,
        warning: warning,
        warningTitle: warningTitle,
        success: success,
        successTitle: successTitle
    }
}