function FillDropdown_PropertType(Type) {
    debugger;
    var ddlCustomers = $("#ddlCustomers");
    ddlCustomers.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: '@Url.Action("GetPropertyTypes", "Property")',
        dataType: "Json",
        type: "POST",
        cache: false,
        data: { Type: Type },
        success: function (response) {
            debugger;
            ddlCustomers.empty().append('<option selected="selected" value="0">All Home Types</option>');
            $.each(response, function () {
                ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}




$(".Search").click(function () {
    debugger;
    var Type = $("input[name='Type']:checked").val();
    var HomeType = $("#ddlCustomers").val();
    var Keyword = $('#main-listing-keyword').val();
    var Status = $('#listing-status').val();
    var Bedroom = $('#listing-bedroom').val();
    var Bathroom = $('#listing-bathroom').val();
    $.ajax({
        url: '@Url.Action("Index", "Property")',
        dataType: "html",
        type: "POST",
        cache: false,
        data: { Type: Type, IsList: $(this).attr('id'), HomeType: HomeType, Keyword: Keyword, Status: Status, Bedroom: Bedroom, Bathroom: Bathroom },
        success: function (data) {
            debugger;
            $("#grid-list").html('');
            $("#grid-list").html(data);
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
});