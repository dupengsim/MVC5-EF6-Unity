/*
 *  用户管理列表界面中的JS脚本
 */

$(function () {
    // 提示信息初始化
    $("[data-toggle='tooltip']").tooltip();
    // 搜索点击事件
    $("#Search_btn").click(function () {
        var _searchKey = $("#searchKey").val().trim();
        var jumpurl = "";
        if (_searchKey.length <= 0)
            jumpurl = "/user/index?page=" + GetQueryString("page") + "&key=" + _searchKey;
        else
            jumpurl = "/user/index?page=1&key=" + _searchKey;
        window.location.href = jumpurl;
    });
    // 搜索框回车事件
    $("#searchKey").keydown(function (e) {
        if (e.keyCode == 13) {
            $("#Search_btn").click();
        }
    })

    // 公司下拉框选择事件
    $("#Company").change(function () {
        var selectedText = $(this).children("option:selected").text();
        $("#CompanyName").val(selectedText);
    })

    // 添加操作
    $("#btnAdd").click(function () {
        window.location.href = "/user/add";
    });

    // 保存（添加|修改）
    $("#btnSave").click(function () {
        var jsondata = $("#frmEditor").serialize();
        $.ajax({
            url: "/user/add",
            type: "post",
            data: jsondata,
            dataType: "json",
            cache: false,
            success: function (msg) {
                if (msg.StatusCode == 100) {
                    flushPage();
                }
            }
        });
    });

    // 返回
    $("#btnBack").click(function () {
        flushPage();
    });

});

// 删除单个用户
function deleteUser(userId) {
    bootbox.confirm("您确认删除选定的用户吗？", function (result) {
        if (result) {
            $.getJSON("/user/delete", { id: userId }, function (msg) {
                bootbox.alert(msg.Message, function () {
                    flushPage();
                });
            });
        }
    });
}

// 激活|冻结账号
function activeAccount(sender) {
    var _userState = $(sender).attr("data-state");
    var _userId = $(sender).attr("data-userId");
    $.getJSON("/user/update", { id: _userId, state: _userState }, function (msg) {
        bootbox.alert(msg.Message, function () {
            flushPage();
        });
    });
}

//url获取参数
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]); return "";
}

// 刷新当前页面列表
function flushPage() {
    var jumpurl = "/user/index?page=" + GetQueryString("page");
    window.location.href = jumpurl;
}