$(document).ready(function () {
    $(document).on("click", "#slider-table .delete-slider-btn", function (event) {
        event.preventDefault();
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `slider/delete?id=${id}`,
            success: function (response) {
                button.closest('.slider-data').remove();
            },
        });
    });
});