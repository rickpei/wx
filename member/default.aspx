<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Wlniao.Member._Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员卡管理</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    </style>
</head>
<body>
    <div style="margin: 0 auto;padding:10px 12px 0px 12px;">
        <div class="bs-docs-example">
            <ul id="myTab" class="nav nav-tabs">
                <li class="active"><a href="#setting" data-toggle="tab">会员卡设置</a></li>
                <li><a href="#tequan" data-toggle="tab">特权管理</a></li>
            </ul>
            <div>

                    <form class="form-horizontal" id="formedit" action="default.aspx" method="post">
                    <div class="modal-body">
                        <div style="margin: auto; width: 98%; color:#666;">
                            <h1 style=" font-size:21px; padding-left:68px; color:#111;">商家信息</h1>
                            <div class="control-group">
                                <label class="control-label" for="CompanyName">商户名称：</label>
                                <div class="controls">
                                    <input type="text" id="CompanyName" name="CompanyName" value="<%=CompanyName %>" placeHolder="" style=" width:210px;" />&nbsp;&nbsp;显示在会员卡底部的商户名称
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="KeyWords">关键字：</label>
                                <div class="controls">
                                    <input type="text" id="KeyWords" name="KeyWords" value="<%=KeyWords %>" placeHolder="如：card" style=" width:60px;" />&nbsp;&nbsp;用户输入此“关键词”将可以触发会员卡图文
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="Address">商家详细地址：</label>
                                <div class="controls">
                                    <input type="text" id="Address" name="Address" value="<%=Address %>" placeHolder="" style=" width:210px;" />&nbsp;&nbsp;将会显示在会员卡背面，不超过30个字
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="TelPhone">商家电话：</label>
                                <div class="controls">
                                    <input type="text" id="TelPhone" name="TelPhone" value="<%=TelPhone %>" placeHolder="" style=" width:210px;" />&nbsp;&nbsp;将会显示在会员卡背面，如021-12345678
                                </div>
                            </div>
                            <h1 style=" font-size:21px; padding-left:68px; color:#111;">卡片信息</h1>
                            <div class="control-group">
                                <label class="control-label" for="CardName">卡片名称：</label>
                                <div class="controls">
                                    <input type="text" id="CardName" name="CardName" value="<%=CardName %>" placeHolder="" style=" width:360px;" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">&nbsp;</label>
                                <div class="controls"><font color="red"><%=msg%></font></div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="default.aspx" class="btn">取消</a>
                        <button type="submit" class="btn btn-primary">保存设置</button>
                        <input type="hidden" name="action" value="save" />
                    </div>
                    </form>
            </div>
        </div>
    </div>
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</body>
<%=_script%>
</html>
