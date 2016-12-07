DevExpress.devices.current({ platform: "generic" });

$(function () {
    var changeTheme = function (theme) {
        var container = $("#full");
        $("." + theme + "-theme").addClass("active");
        container.empty();

        if (DevExpress.viz)
            DevExpress.viz.currentTheme("desktop", theme);

        DevExpress.ui.themes.current({
            theme: "generic." + theme,
            loadCallback: function () {
                container.append($("#html").text());
                jQuery.globalEval($("#jsCode").text());
            }
        });
    }

    $(".dark-theme").click(function () {
        $(".light-theme").removeClass("active");
        $(".pane").addClass("dark");
        changeTheme("dark");
    });

    $(".light-theme").click(function () {
        $(".dark-theme").removeClass("active");
        $(".pane").removeClass("dark");
        changeTheme("light")
    });
});