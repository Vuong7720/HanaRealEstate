var pagingOptions = {
    pageSize: 5,
    pageIndex: 1
};

var aboutUs = {
    init: function() {
        aboutUs.loadData(true);
    },

    loadData: function(changePageSize) {
        $.ajax({
            url: '/AboutUs/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                pageIndex: pagingOptions.pageIndex
            },
            success: function(response) {
                var html = '';
                var data = response.data;
                var formData = $('#data-template').html();
                $.each(data, function(i, item) {
                    html += Mustache.render(formData, {
                        Id: item.Id,
                        Content: item.Content
                    });
                });

                $('#about-us-data').html(html);
                aboutUs.paging(response.totalRow, function() {
                    aboutUs.loadData();
                }, changePageSize);
            },
            error: function(err) {
                console.log(err);
            }
        });
    },

    paging: function(totalRow, callback, changePageSize) {

        var totalPage = Math.ceil(totalRow / pagingConfig.pageSize);

        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            startPage: pagingConfig.pageIndex > totalPage ? totalPage : pagingConfig.pageIndex,
            visiblePages: 5,
            onPageClick: function(event, page) {
                pagingConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },

    showModal: function(id) {

    }

};

aboutUs.init();