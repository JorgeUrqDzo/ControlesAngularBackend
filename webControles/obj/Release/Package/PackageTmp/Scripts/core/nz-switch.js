var NzSwitch = function () {

    var setNzSwitch = function () {
        $(function (argument) {

            //    $('[type="checkbox"]').removeAttr("data-on-color");
            //    $('[type="checkbox"]').removeAttr("data-size");
            //    $('[type="checkbox"]').removeAttr("data-on-text");
            //    $('[type="checkbox"]').removeAttr("data-off-text");

            //    $('[type="checkbox"]').attr("data-on-color", "success");
            //    $('[type="checkbox"]').attr("data-size", "small");
            //    $('[type="checkbox"]').attr("data-on-text", "Sí");
            //    $('[type="checkbox"]').attr("data-off-text", "No");
            $('[type="checkbox"]').bootstrapSwitch();

            $('[type="checkbox"]').bootstrapSwitch('size', 'mini');
            $('[type="checkbox"]').bootstrapSwitch('onText', 'SI');
            $('[type="checkbox"]').bootstrapSwitch('offText', 'NO');
            $('[type="checkbox"]').bootstrapSwitch('onColor', 'success');
            $('[type="checkbox"]').bootstrapSwitch('offColor', 'default');

        });
    };

    return {
        SetNzSwitch: setNzSwitch
    }

}

var nzSwitch = new NzSwitch();
nzSwitch.SetNzSwitch();