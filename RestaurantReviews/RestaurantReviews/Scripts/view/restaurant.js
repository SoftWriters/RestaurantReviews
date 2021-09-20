$(document).ready(function () {
    restaurantPage.init();
});

var restaurantPage = {
    init: function () {
        this.initEvents();
    },

    initEvents: function () {
        $("#btn_Add").click(function () {
            window.location.href = "/Restaurant/Update";
        });

        $("#btn_Search").click(function () {
            var city = $("#txt_City").val();
            window.location.href = "/Restaurant/?City=" + city;
        });

        $("#btn_Clear").click(function () {
            window.location.href = "/Restaurant/";
        });

        $("#tbl_Restaurants td[data-id]").click(function () {
            var $td = $(this);
            var restaurantID = $td.attr("data-id");
            window.location.href = "/Review/?RestaurantID=" + restaurantID;
        });

        $("#tbl_Restaurants select[data-actions]").change(function () {
            var $select = $(this);
            var restaurantID = $select.attr("data-id");
            var action = $select.val();
            $select.val(0);

            if (action == "Update") {
                window.location.href = "/Restaurant/Update?id=" + restaurantID;
            }
        });
    }
};
