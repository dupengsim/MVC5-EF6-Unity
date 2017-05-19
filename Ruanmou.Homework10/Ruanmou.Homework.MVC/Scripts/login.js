
/*用户登录界面中的js*/

$(function () {
    
    // Ajax登录逻辑
    $("#btnAjaxLogin").click(function () {
        $.ajax({
            url: '/login/ajaxlogin',
            type: 'POST',
            dataType: 'json',
            data: $(".form-horizontal").serialize(),
            success: function (res) {
                if (res.StatusCode == 100) {
                    var reurl = $("#Reurl_hide").val();
                    window.location.href = reurl || "/user/index";//登录成功
                }  else {
                    alert(res.Message);
                }
            }
        });
    })
})