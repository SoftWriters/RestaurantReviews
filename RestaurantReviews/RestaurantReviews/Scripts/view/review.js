$(document).ready(function () {
    reviewPage.init();
});

var reviewPage = {
    init: function () {
        this.initEvents();
    },

    initEvents: function () {
        var self = this;

        $("#btn_Add").click(function () {
            var restaurantID = $("#ddl_Restaurant").val();
            window.location.href = "/Review/Update?restaurantID=" + restaurantID;
        });

        $("#btn_Search").click(function () {
            var userID = $("#ddl_User").val();
            var restaurantID = $("#ddl_Restaurant").val();
            window.location.href = "/Review/?userID=" + userID + "&restaurantID=" + restaurantID;
        });

        $("#btn_Clear").click(function () {
            window.location.href = "/Review/";
        });

        $("#tbl_Reviews select[data-actions]").change(function () {
            var $select = $(this);
            var $tr = $select.parents("tr:eq(0)");
            var $tbody = $tr.parents("tbody:eq(0)");

            var reviewID = $select.attr("data-id");
            var action = $select.val();
            $select.val(0);

            if (action == "Update") {
                window.location.href = "/Review/Update?id=" + reviewID;
            } else if (action == "Delete") {
                var name = $tr.find("td[data-title]").html();
                var $dialog = $("<div><p>Are you sure you wish to delete the review <span class=\"bold\">" + name + "</span>?</p></div>");
                $dialog.dialog({
                    modal: true,
                    buttons: [
                        {
                            text: "Cancel",
                            class: "btn btn-lg",
                            click: function () {
                                $dialog.dialog("close");
                            }
                        },
                        {
                            text: "Delete",
                            class: "btn btn-lg btn-primary",
                            click: function () {
                                self.deleteReview(reviewID, function (response) {
                                    if (response !== -1) {
                                        // Delete current review row
                                        $tr.remove();
                                        
                                        // Reload the page if there are no reviews left in the current view
                                        var $allTr = $tbody.find("tr");
                                        if ($allTr.length === 0) {
                                            window.location.href = "/Review/";
                                        } else {
                                            $dialog.dialog("close");
                                        }
                                    }
                                });
                            }
                        }
                    ]
                });
            }
        });
    },

    deleteReview: function (reviewID, callback) {
        $.ajax({
            type: "DELETE",
            url: "/api/Review",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(reviewID),
            success: function (response) {
                if (callback)
                    callback(response);
            }
        });
    }
};
