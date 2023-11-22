
var pagingOptions = {
    pageSize: 5,
    pageIndex: 1
};

var realEstateServices = {
    init: function () {
        // Thay đổi sự kiện cho checkbox "Check All" sử dụng .on()
        $(document).on('change', '#check-all', function () {
            $('.checkbox-item:visible').prop('checked', $(this).prop('checked'));
        });

        // Thay đổi sự kiện cho checkbox con sử dụng .on()
        $(document).on('change', '.checkbox-item', function () {
            if ($('.checkbox-item:visible:checked').length === $('.checkbox-item:visible').length) {
                $('#check-all').prop('checked', true);
            } else {
                $('#check-all').prop('checked', false);
            }
        });

        realEstateServices.loadData(true);
        realEstateServices.registerEvent();
    },
    loadData: function (changePageSize) {
        var pageIndex = Number($('#paging-current-index').val());
        var pageSize = pagingOptions.pageSize;
        var searchKey = $('#search-for-anything').val();

        var requestData = { pageIndex: pageIndex, pageSize: pageSize, searchKey: searchKey };

        $.ajax({
            url: '/AdminArea/RealEstate/LoadData',
            type: 'POST',
            dataType: 'json',
            data: requestData,
            success: function (response) {
                var html = '';
                var data = response.data;
                var formData = $('#data-template').html();
                $.each(data, function (i, item) {
                    html += Mustache.render(formData, {
                        Index: (pageIndex - 1) * pageSize + Number(i) + 1,
                        Id: item.id,
                        Street: item.street,
                        Price: item.price,
                        PostDate: item.postDate,
                        ExpireTime: item.expireTime,
                        Agent: item.agent,
                        Type: item.type,
                        Status: item.status
                    });
                });

                $('#real-estate-list-data').html(html);

                realEstateServices.paging(response.totalRow, function () {
                    realEstateServices.loadData();
                }, changePageSize);
            },
            error: function (err) {
                console.log(err);
            }
        });
    },

    paging: function (totalRow, callback, changePageSize) {
        var pageSize = pagingOptions.pageSize;
        var totalPage = Math.ceil(totalRow / pageSize);

        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            onPageClick: function (event, page) {
                $('#paging-current-index').val(page);
                pagingOptions.pageIndex = page;
                setTimeout(callback, 200);
            },
            first: false,
            last: false,
            next: '>>',
            prev: '<<',

        });
    },
    
    deleteMultipleRealEstate: function () {
        var selectedIds = [];

        // Lấy danh sách các id được chọn
        $('.checkbox-item:checked').each(function () {
            selectedIds.push($(this).val());
        });
        if (selectedIds.length > 0) {
            $.ajax({
                url: '/AdminArea/RealEstate/DeleteSelected',
                type: 'POST',
                dataType: 'json',
                data: { ids: selectedIds},
                success: function (response) {
                    if (response.isSuccess) {
                        realEstateServices.loadData(true);
                        swal("Thành công!", response.message, "success");
                    } else {
                        swal("Có lỗi!", response.message, "error");
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        } else {
            swal("Thông báo", "Vui lòng chọn ít nhất một bản ghi để xóa!", "info");
        }
    },


    disableRealEstate: function (form) {
        swal({
            title: "Xác nhận",
            text: "Xác nhận khóa bài đăng này?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Xác nhận",
            cancelButtonText: "Hủy bỏ",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    try {
                        $.ajax({
                            url: '/AdminArea/RealEstate/Disable',
                            type: 'POST',
                            headers: {
                                RequestVerificationToken:
                                    $('input:hidden[name="__RequestVerificationToken"]').val()
                            },
                            dataType: 'json',
                            data: new FormData(form),
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                if (response.isSuccess) {
                                    realEstateServices.loadData(true);
                                    setTimeout(function () {
                                        swal({
                                            title: "Thành công!",
                                            text: "Khóa thành công!",
                                            type: "success"
                                        });
                                    }, 200);
                                }
                                else {
                                    swal("Có lỗi!", "", "error");
                                }
                            },
                            error: function (err) {
                                alert(err);
                            }
                        });
                    } catch (ex) {
                        console.log(ex);
                    }
                } else {
                    swal("Hủy bỏ", "", "error");
                }
            });

        //prevent default form submit event
        return false;
    },

    enableRealEstate: function (form) {
        swal({
            title: "Xác nhận",
            text: "Xác nhận mở bài đăng này?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Xác nhận",
            cancelButtonText: "Hủy bỏ",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    try {
                        $.ajax({
                            url: '/AdminArea/RealEstate/Enable',
                            type: 'POST',
                            headers: {
                                RequestVerificationToken:
                                    $('input:hidden[name="__RequestVerificationToken"]').val()
                            },
                            dataType: 'json',
                            data: new FormData(form),
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                if (response.isSuccess) {
                                    realEstateServices.loadData(true);
                                    setTimeout(function () {
                                        swal({
                                            title: "Thành công!",
                                            text: "Mở thành công!",
                                            type: "success"
                                        });
                                    }, 200);
                                }
                                else {
                                    swal("Có lỗi!", "", "error");
                                }
                            },
                            error: function (err) {
                                alert(err);
                            }
                        });
                    } catch (ex) {
                        console.log(ex);
                    }
                } else {
                    swal("Hủy bỏ", "", "error");
                }
            });

        //prevent default form submit event
        return false;
    },

    deleteRealEstate: function (form) {
        swal({
            title: "Xác nhận",
            text: "Xác nhận xóa bài đăng này?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Xác nhận",
            cancelButtonText: "Hủy bỏ",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    try {
                        $.ajax({
                            url: '/AdminArea/RealEstate/Delete',
                            type: 'POST',
                            headers: {
                                RequestVerificationToken:
                                    $('input:hidden[name="__RequestVerificationToken"]').val()
                            },
                            dataType: 'json',
                            data: new FormData(form),
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                if (response.isSuccess) {
                                    realEstateServices.loadData(true);
                                    setTimeout(function () {
                                        swal({
                                            title: "Thành công!",
                                            text: "Xóa thành công!",
                                            type: "success"
                                        });
                                    }, 200);
                                }
                                else {
                                    swal("Có lỗi!", res.message, "error");
                                }
                            },
                            error: function (err) {
                                alert(err);
                            }
                        });
                    } catch (ex) {
                        console.log(ex);
                    }
                } else {
                    swal("Hủy bỏ", "", "error");
                }
            });

        //prevent default form submit event
        return false;
    },

    gotoIndex: function () {

    },

    registerEvent: function () {
        $('#search-for-anything').keyup(function () {
            realEstateServices.loadData(true);
        });
    }
};

realEstateServices.init();

