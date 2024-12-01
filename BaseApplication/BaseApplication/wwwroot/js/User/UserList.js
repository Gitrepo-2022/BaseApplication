var SortColm = "FirstName";
var SortOrder = "asc";

$(window).on("load", function (e) {
    //UserPagination(1);
    $('#UserList').DataTable({
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        "aLengthMenu": [1000]
    });
    $(".dataTables_length").hide();
    $(".dataTables_filter").hide();
    $("#UserList_info").hide();
    $("#UserList_paginate").hide();
})

function saveuser() {
    
    debugger;
        $.ajax({
            url: '/account/CreateUser',
            type: 'POST',
            dataType: 'json', //make sure your service is actually returning json here
            //contentType: 'application/json',
            success: function (res) {
                //here data is whatever your WebService.asmx/getList returned
                //populate your dropdown here with your $.each w/e
            },
            failure: function (res) {
                debugger;
            },
            error: function (res) {
                debugger
            }
        });
}

function UserPagination(pageIndex) {
    $(".overlay").removeClass("hide");
    var Datas = {
        "PageIndex": pageIndex,
        "SortOrder": SortOrder,
        "SortColm": SortColm,
        "FirstName": $("#SearchUserFirstName").val(),
        "LastName": $("#SearchUserLastName").val(),
        "Email": $("#SearchUserEmail").val(),
        "PageSize": 20
    }
    $.ajax({
        type: "get",
        url: window.location.origin + "/User/GetUserList",
        data: Datas,
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            $(".overlay").addClass("hide");
            // toastr.error(errormessage);
        },
        error: function (response) {
            $(".overlay").addClass("hide");
            // toastr.error(sessionexpire);
            setTimeout(function () {
                window.location.href = "/User/Index"
            }, 1000);
        }
    });
}
function OnSuccess(response) {
    debugger;
    $(".overlay").addClass("hide");
    $("#UserList tr:last-child").removeAttr("style").clone(true);
    $("#UserList tbody").empty();
    var template = $('#templateUserList').html();
    Mustache.parse(template);
    var rendered = "";
    if (response.users != null && response.users.length > 0) {
        var data = response.users;
        $.each(data, function (e, v) {
            //var propertyvalue = "inline-block";
            //if (this.currentUser) {
            //    propertyvalue = "none";
            //}
            //var address = v.address + ((v.city == null || v.city == "") ? "" : "," + v.city) + ((v.state == null || v.state == "") ? "" : "," + v.state);
            rendered += Mustache.render(template, { FirstName: this.firstName, LastName: this.lastName, Email: this.email, id: this.id });
        });
        $('#listUser').html(rendered);
    }
    else {
        $("#listUser").html(`<tr><td colspan='6' class="text-center">No Record Found.</td></tr>`);
    }
    $(".Pager").ASPSnippets_Pager({
        ActiveCssClass: "current",
        PagerCssClass: "pager",
        PageIndex: response.pageIndex,
        PageSize: response.pageSize,
        RecordCount: response.totalRecordCount
    });
    $(".Pager .page").click(function () {
        UserPagination(parseInt($(this).attr('page')));
    });
}

$(".TbCol").click(function () {
    SortColm = $(this).data("sort-col");
    if (SortOrder === 'asc') {
        SortOrder = 'desc';
        $(this).addClass('desc');
        $(this).removeClass('asc');
    }
    else {
        SortOrder = 'asc';
        $(this).addClass('asc');
        $(this).removeClass('desc');
    }
    UserPagination(1);
});
function ConfirmDelete(id) {
    $.confirm({
        title: "Confirm",
        content: `Are you sure want to delete this Note!`,
        buttons: {
            confirm: function () {
                $.post("/User/DeleteUserById", { id: id }, ConfirmDeletecallback2);
            },
            cancel: function () {
            },
        }
    });
}

function ConfirmDeletecallback2(result) {
    //toastr.success(deletetoastr);
    UserPagination(1);
}

$("#Reset").click(function () {
    $('#SearchUserFirstName').val('');
    $("#SearchUserLastName").val('');
    $("#SearchUserEmail").val('');
    UserPagination(1);
});

function seachOnEnter() {
    if (event.keyCode == 13) {
        event.preventDefault();
        UserPagination(1);
    }
}
