$(document).ready(function () {
    $(document).on("click", "#information-table .delete-info-btn", function (event) {
        event.preventDefault();
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `information/delete?id=${id}`,
            success: function (response) {
                button.closest('.info-data').remove();
            },
        });
    });
});