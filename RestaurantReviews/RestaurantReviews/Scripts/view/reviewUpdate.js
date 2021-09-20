var reviewUpdatePage = {
    data: {},

    init: function (reviewID) {
        this.initData(reviewID);
        this.initEvents();
    },

    initData: function (reviewID) {
        this.data.reviewID = reviewID;
    },

    initEvents: function () {
        var self = this;

        $("button[data-cancel]").click(function () {
            window.location.href = "/Review";
        });

        $("button[data-save]").click(function () {
            var $ddl_Restaurant = $("#ddl_Restaurant");
            var $txt_Title = $("#txt_Title");
            var $txt_Description = $("#txt_Description");

            var isValid = $ddl_Restaurant[0].checkValidity();
            isValid &= $txt_Title[0].checkValidity();
            isValid &= $txt_Description[0].checkValidity();

            var restaurantID = $ddl_Restaurant.val();
            isValid &= (restaurantID != 0);

            var $divFailed = $("div[data-failed]");
            if (!isValid) {
                $divFailed.removeClass("hidden");
                $divFailed.html("Please fill out all required fields.");
                return;
            }

            var title = $txt_Title.val();
            var description = $txt_Description.val();

            var data = {
                "ID": self.data.reviewID,
                "RestaurantID": restaurantID,
                "Title": title,
                "Description": description
            };

            $.ajax({
                type: "PUT",
                url: "/api/Review",
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
                        window.location.href = "/Review/";
                    }
                }
            });
        });
    }
};
