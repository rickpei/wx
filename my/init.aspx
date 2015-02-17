<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="init.aspx.cs" Inherits="Wlniao.My.Init" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>初始设置</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<div style="margin-top:20px">  
    <form action="init.aspx?do=save" method="post" class="well form-horizontal" style="width:580px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <h3>初始设置</h3>
                <div class="control-group">
                    <label class="control-label">微信名称</label>
                    <div class="controls">
                        <input type="text" name="weixinname" style=" width:292px;" value="<%=weixinname %>" placeholder="请输入公众帐号的名称,用于标识" />
                        <p style=" font-size:12px; color:#666666;">填写你微信公众帐号的名称</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">微信原始号</label>
                    <div class="controls">
                        <input type="text" name="weixinaccount" style=" width:292px;" value="<%=weixinaccount %>" placeholder="格式如：gh_daec1921b1a7" />
                        <p style=" font-size:12px; color:#666666;">微信公众帐号的原ID串，<a href="http://doc.wlniao.com/doc/weback/help_weixinaccount.html" target="_blank">怎么查看微信的原始帐号？</a></p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">&nbsp;</label>
                    <div class="controls"><%=msg%></div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <button type="submit" class="btn btn-primary">保存设置</button>
                        <button type="submit" class="btn">不知道怎么设置，直接跳过</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
 </div>
</body>
</html>
