var realEstate = {
    init: function() {
        realEstate.loadDistricts();
        realEstate.registerEvent();
    },
    loadData: function() {

    },

    loadDistricts: function() {
        var cityId = $("#City").val();
        var currentDistrictId = $("#District").val();
        $.ajax({
            url: '/RealEstate/GetDistricts',
            type: 'GET',
            dataType: 'json',
            data: { cityId: cityId, currentDistrictId: currentDistrictId },
            success: function(response) {
                if (response.status) {
                    console.log(response.message);
                    var data = response.data;
                    var html = '';
                    var template = $('#district-template').html();
                    $.each(data, function(i, item) {
                        html += Mustache.render(template, {
                            DistrictId: item.id,
                            DistrictName: item.districtName
                        });
                    });
                    $('#District').html(html);
                    $("#District").val(response.current);
                    console.log("Current district: " + $("#District").val());
                    $('#District').niceSelect('update');
                }
                else {
                    console.log(response.message);
                }
            },
            error: function(ex) {
                console.log(ex);
            }
        });
    },
    registerEvent: function() {
        $("#City").change(function() {
            realEstate.loadDistricts();
        });
    }
};

realEstate.init();
