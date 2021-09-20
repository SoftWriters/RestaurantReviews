var restaurantUpdatePage = {
    data: {},

    init: function (restaurantID) {
        this.initData(restaurantID);
        this.initEvents();
    },

    initData: function (restaurantID) {
        this.data.restaurantID = restaurantID;
    },

    initEvents: function () {
        var self = this;
        
        $("button[data-cancel]").click(function () {
            window.location.href = "/Restaurant";
        });

        $("button[data-save]").click(function () {
            var $txt_Name = $("#txt_Name");
            var $txt_City = $("#txt_City");
            var $txt_Description = $("#txt_Description");

            var isValid = $txt_Name[0].checkValidity();
            isValid &= $txt_City[0].checkValidity();
            isValid &= $txt_Description[0].checkValidity();

            var $divFailed = $("div[data-failed]");
            if (!isValid) {
                $divFailed.removeClass("hidden");
                $divFailed.html("Please fill out all required fields.");
                return;
            }

            var name = $txt_Name.val();
            var city = $txt_City.val();
            var description = $txt_Description.val();

            var data = {
                "ID": self.data.restaurantID,
                "Name": name,
                "City": city,
                "Description": description
            };

            $.ajax({
                type: "PUT",
                url: "/api/Restaurant",
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
