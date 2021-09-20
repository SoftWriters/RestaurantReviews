$(document).ready(function () {
    login.init();
});

var login = {
    init: function () {
        this.initEvents();
    },

    initEvents: function () {
        $("button[data-login]").click(function () {
            var username = $("#txt_Username").val();
            var password = $("#txt_Password").val();

            var data = {
                "Username": username,
                "Password": password
            };

            $.ajax({
                type: "POST",
                url: "/api/User/Login",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                success: function (response) {
                    if (response === 0) {
                        $("div[data-failed]").removeClass("hidden");
                    } else {
                        $("div[data-failed]").addClass("hidden");
                        window.location.href = "/Restaurant/";
                    }
                }
            });
        });

        $("button[data-signup]").click(function () {
            window.location.href = "/User/";
        });
    }
};
