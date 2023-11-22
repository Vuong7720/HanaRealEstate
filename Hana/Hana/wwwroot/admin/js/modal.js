showInPopup = (url, title) => {
    try {
        $.ajax({
            type: "GET",
            url: url,
            success: function (res) {
                $('#modal-form .modal-body').html(res);
                $('#modal-form .modal-title').html(title);
                $('#modal-form').modal('show');

            }
        });
    }
    catch (ex) {
        console.log(ex);
    }

};

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html);
                    $('#modal-form .modal-body').html('');
                    $('#modal-form .modal-title').html('');
                    $('#modal-form').modal('hide');
                }
                else
                    $('#modal-form .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err);
            }
        });

        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex);
    }
};



jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                },
                error: function (err) {
                    console.log(err);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    }
    //prevent default form submit event
    return false;
};

jQueryAjaxDisable = form => {
    if (confirm('Are you sure to disable this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isSuccess) {
                        $('#view-all').html(res.html);
                    }
                    else {
                        alert("Already disabled!");
                    }

                },
                error: function (err) {
                    alert("Something wrong! Try again!");
                    console.log(err);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    }
    //prevent default form submit event
    return false;
};

removePictureAjax = form => {
    if (confirm('Are you sure to remove this image ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isSuccess) {

                        $('#view-pictures').html(res.html);
                        alert("Success");
                    }
                    else {
                        alert("Something wrong!");
                        console.log(res);
                    }

                },
                error: function (err) {
                    alert("Something wrong! Try again!");
                    console.log(err);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    }
    //prevent default form submit event
    return false;
};


$(function () {
    $("#spinder").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#spinder").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#spinder").addClass('hide');
    });
});
