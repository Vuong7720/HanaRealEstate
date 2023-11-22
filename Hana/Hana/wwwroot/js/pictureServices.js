var pictureServices = {
    init: function () {
        pictureServices.loadPictures();
    },

    loadPictures: function () {

        var id = $('#Id').val();
        $.ajax({
            url: '/AdminArea/Picture/LoadData',
            type: 'GET',
            dataType: 'json',
            data: { realEstateId: id },
            success: function (response) {
                console.log("Status la: " + response.status);
                if (response.status) {
                    if (response.count === 0) {
                        var emptyMessage = '&nbsp Hiện chưa có hình ảnh nào';
                        $('#no-image').html(emptyMessage);
                        $("#view-pictures").html("");
                    }
                    else {
                        var data = response.data;
                        var html = '';
                        var template = $('#picture-template').html();
                        $.each(data, function (i, item) {
                            // check if file is local
                            let displayUrl = item.url;
                            let fileName = item.pictureName;

                            //fileName not null
                            if (fileName) {
                                if (fileName.indexOf('local-picture') === 0) {
                                    displayUrl = "/images/" + item.url;
                                }
                            }

                            html += Mustache.render(template, {
                                PictureId: item.id,
                                PictureUrl: displayUrl
                            });
                        });

                        $('#view-pictures').html(html);
                    }
                }
                else {
                    console.log(response.status);
                }
            }
        });
    },

    removePicture: function (id) {
        if (confirm('Xác nhận xóa hình này?') === true) {

            $.ajax({
                url: '/AdminArea/Picture/Remove',
                type: 'POST',
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                data: { pictureId: id },
                success: function (response) {
                    if (response.status) {
                        pictureServices.loadPictures();
                        setTimeout(function () {
                            alert("Xóa thành công!!!");
                        }, 350);
                    }
                    else {
                        alert("Lỗi xảy ra, vui lòng thử lại!!!");
                    }
                }
            });
        }
    }
};

pictureServices.init();