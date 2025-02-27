﻿$(document).ready(function () {
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
    $(document).on("click", "#about-table .delete-about-btn", function (event) {
        event.preventDefault();
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `about/delete?id=${id}`,
            success: function (response) {
                button.closest('.about-data').remove();
            },
        });
    });
    $(document).on("click", "#category-table .delete-category-btn", function (event) {
        event.preventDefault();
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `category/delete?id=${id}`,
            success: function (response) {
                button.closest('.category-data').remove();
            },
        });
    });
    $(document).on("click", "#course-table .delete-course-btn", function (event) {
        event.preventDefault();
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `course/delete?id=${id}`,
            success: function (response) {
                button.closest('.course-data').remove();
            },
        });
    });
    $(document).on("click", "#instructor-table .delete-instructor-btn", function (event) {
        event.preventDefault();
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `instructor/delete?id=${id}`,
            success: function (response) {
                button.closest('.instructor-data').remove();
            },
        });
    });
    $(document).on("click", "#student-table .delete-student-btn", function (event) {
        event.preventDefault();
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `student/delete?id=${id}`,
            success: function (response) {
                button.closest('.student-data').remove();
            },
        });
    });
    $(document).on("click", "#contact-table .delete-contact-btn", function (event) {
        event.preventDefault();
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `contact/delete?id=${id}`,
            success: function (response) {
                button.closest('.contact-data').remove();
            },
        });
    });
});