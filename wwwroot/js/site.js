// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function(){
    $(document).on('submit', '#addCategory', function(e){
        // Prevent the default action (in the case of an "a" tag, firing off an http request)
        e.preventDefault();
        // Make an async get request to the href attribute and run a function
        $.ajax({
            url: "AddCategoryUrl",
            method: 'POST',
            data: $(this).serialize(),
            success: function (res) {
                console.log(res)
                if(res.data) {
                    $("#errorMessage").html(res.data);
                } else {
                    location.reload();
                }
            },
            error: function(err) {
                console.log("error")
                console.log(err.data)
                alert(err.data)
                // $("#errorMessage").html("dasdasdasd");
                $("#errorMessage").append("<span>" + err.data + "</span>");
            }
        })
    })
})