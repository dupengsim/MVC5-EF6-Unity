
/*
  *  菜单管理js
 */

// 初始化参数设置
var setting = {
    view: {
        addHoverDom: addHoverDom,
        removeHoverDom: removeHoverDom,
        selectedMulti: false
    },
    edit: {
        enable: true,
        editNameSelectAll: true,
        showRemoveBtn: showRemoveBtn,
        showRenameBtn: showRenameBtn
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        beforeEditName: beforeEditName,
        beforeRemove: beforeRemove,
        beforeRename: beforeRename
    }
};

var log, className = "dark";
// 点击编辑按钮时触发
function beforeEditName(treeId, treeNode) {
    className = (className === "dark" ? "" : "dark");
    var zTree = $.fn.zTree.getZTreeObj("treeMenu");
    zTree.selectNode(treeNode);
    $("#btnSubmit").hide();
    $("#btnEdit").show();
    $("#myModalLabel").text("编辑菜单");
    $('#myModal').modal('show');
    $("#ParentId").val(treeNode.pId == null ? 0 : treeNode.pId);
    $("#NodeId").val(treeNode.id);
    $("#Name").val(treeNode.name);
    $("#Description").val(treeNode.Description);
    $("#Url").val(treeNode.Url);
    $("#SourcePath").val(treeNode.SourcePath);
    $("#MenuLevel").val(treeNode.MenuLevel);
    $("#Sort").val(treeNode.Sort);
}
// 点击删除按钮时触发
function beforeRemove(treeId, treeNode) {
    className = (className === "dark" ? "" : "dark");
    var zTree = $.fn.zTree.getZTreeObj("treeMenu");
    zTree.selectNode(treeNode);
    return deleteNode(treeId, treeNode);
}
// 编写删除的后台逻辑
function deleteNode(treeId, treeNode) {
    $.post("/menu/delete", { id: treeNode.id }, function (data) {
        var jsonData = eval("(" + data + ")");
        alert(jsonData.Message);
    });
    return true;
}
// 修改名称后触发
function beforeRename(treeId, treeNode, newName) {
    className = (className === "dark" ? "" : "dark");
    if (newName.length == 0) {
        alert("节点名称不能为空.");
        var zTree = $.fn.zTree.getZTreeObj("treeMenu");
        setTimeout(function () { zTree.editName(treeNode) }, 10);
        return false;
    }
    //return renameNode(treeId, treeNode, newName);
}
// 编写修改节点名称的后台逻辑
function renameNode(treeId, treeNode, newName) {
    $.post("/menu/update", { id: treeNode.id, newName: newName }, function (data) {
        var jsonData = eval("(" + data + ")");
        alert(jsonData.Message);
    });
    return true;
}
// 鼠标滑动时所有节点显示删除的按钮
function showRemoveBtn(treeId, treeNode) {
    //return !treeNode.isFirstNode;
    return true;
}
// 鼠标滑动时所有节点显示编辑的按钮
function showRenameBtn(treeId, treeNode) {
    //return !treeNode.isLastNode;
    return true;
}
// 鼠标滑动时增加显示添加的按钮
var newCount = 1;
var treeNodeNew;
function addHoverDom(treeId, treeNode) {
    var sObj = $("#" + treeNode.tId + "_span");
    if (treeNode.editNameFlag || $("#addBtn_" + treeNode.id).length > 0) return;
    var addStr = "<span class='button add' id='addBtn_" + treeNode.id
        + "' title='添加子节点' onfocus='this.blur();'></span>";
    sObj.after(addStr);
    // 添加按钮
    var btn = $("#addBtn_" + treeNode.id);
    if (btn) btn.bind("click", function () {
        treeNodeNew = treeNode;
        $("#TreeId").val(100 + newCount);
        $("#ParentId").val(treeNode.id).attr("readonly", "readonly");
        addNode(treeId, treeNode);
    });
};

function addNode(treeId, treeNode) {
    $('#myModal').modal('show');
    $("#btnSubmit").show();
    $("#btnEdit").hide();
    $("#myModalLabel").text("添加新菜单");
    return false;
}

// 鼠标滑出时移除添加的按钮
function removeHoverDom(treeId, treeNode) {
    $("#addBtn_" + treeNode.id).unbind().remove();
};
function selectAll() {
    var zTree = $.fn.zTree.getZTreeObj("treeMenu");
    zTree.setting.edit.editNameSelectAll = $("#selectAll").attr("checked");
}

$(document).ready(function () {
    $.getJSON("/menu/getmenutreelist", function (data) {
        $.fn.zTree.init($("#treeMenu"), setting, data);
    });
    $("#selectAll").bind("click", selectAll);
    // 默认隐藏表单
    $('#myModal').modal('hide');
    // 表单提交
    $("#btnSubmit").click(function () {
        $.ajax({
            url: "/menu/add",
            type: "post",
            data: $("#frmEditor").serialize(),
            cache: false,
            success: function (msg) {
                var jsonData = eval("(" + msg + ")");
                if (jsonData.StatusCode == 100) {
                    var zTree = $.fn.zTree.getZTreeObj("treeMenu");
                    zTree.addNodes(treeNodeNew, { id: $("#TreeId").val(), pId: $("#ParentId").val(), name: $("#Name").val() });
                    $("#btnClose").click();
                    treeNodeNew = "";// 释放临时保存的treeNode信息
                }
            }
        });
    });

    // 表单提交
    $("#btnEdit").click(function () {
        $.ajax({
            url: "/menu/updatemodel",
            type: "post",
            data: $("#frmEditor").serialize(),
            cache: false,
            success: function (msg) {
                var jsonData = eval("(" + msg + ")");
                if (jsonData.StatusCode == 100) {
                    window.location.reload();
                }
            }
        });
    });
});