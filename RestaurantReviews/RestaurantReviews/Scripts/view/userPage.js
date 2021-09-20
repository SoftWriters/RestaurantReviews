var userPage = {
    data: {},

    init: function (userID) {
        this.initData(userID);
        this.initEvents();
    },

    initData: function (userID) {
        this.data.userID = userID;
    },

    initEvents: function () {
        var self = this;

        $("button[data-cancel]").click(function () {
            window.location.href = "/Restaurant";
        });

        $("button[data-save]").click(function () {
            var $txt_Username = $("#txt_Username");
            var $txt_FirstName = $("#txt_FirstName");
            var $txt_LastName = $("#txt_LastName");
            var $txt_Password = $("#txt_Password");
            var $txt_PasswordRepeat = $("#txt_PasswordRepeat");

            var isValid = $txt_Username[0].checkValidity();
            isValid &= $txt_FirstName[0].checkValidity();
            isValid &= $txt_LastName[0].checkValidity();
            isValid &= $txt_Password[0].checkValidity();
            isValid &= $txt_PasswordRepeat[0].checkValidity();

            var $divFailed = $("div[data-failed]");
            if (!isValid) {
                $divFailed.removeClass("hidden");
                $divFailed.html("Please fill out all required fields.");
                return;
            }

            var username = $txt_Username.val();
            var firstName = $txt_FirstName.val();
            var lastName = $txt_LastName.val();
            var password = $txt_Password.val();
            var passwordRepeat = $txt_PasswordRepeat.val();

            if (password !== passwordRepeat) {
                $divFailed.removeClass("hidden");
                $divFailed.html("Password and Repeat Password do not match.");
                return;
            }

            var data = {
                "ID": self.data.userID,
                "Username": username,
                "FirstName": firstName,
                "LastName": lastName,
                "Password": password
            };

            $.ajax({
                type: "PUT",
                url: "/api/User",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                success: function (response) {
                    if (response === -1) {
                        let $divFailed = $("div[data-failed]");
                        $divFailed.removeClass("hidden");
                        $divFailed.html("Incorrect user!");
                    } else {
                        $("div[data-failed]").addClass("hidden");
                        window.location.href = "/Restaurant/";
                    }
                }
            });
        });
    }
};
