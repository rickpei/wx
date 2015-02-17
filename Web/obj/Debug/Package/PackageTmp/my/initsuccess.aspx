<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="initsuccess.aspx.cs" Inherits="Wlniao.My.InitSuccess" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>设置成功</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<div class="" style="margin-top:100px">  
    <form action="../wx/setting.aspx" method="post" class="well form-horizontal" style="width:580px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <h3>操作提示</h3>
                <p class="lead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您的微信帐号设置成功但尚未接入，你可以使用如下信息在公众平台进行接入认证！</p>
                <div style=" background-color:#fafafa; padding:12px 18px; margin-bottom:21px; border:1px dashed #cccccc; ">
                    <h1 style=" clear:both; display:inline-block; width:90px; text-align:right; font-family:微软雅黑; font-size:14px; line-height:1em; margin:12px 8px 8px 8px;">接口地址</h1>
                    <span>http://<%=_website%>/wxapi.aspx?wx=<%=_account%></span>
                    <br />
                    <h1 style=" clear:both; display:inline-block; width:90px; text-align:right; font-family:微软雅黑; font-size:14px; line-height:1em; margin:12px 8px 8px 8px;">Token</h1>
                    <span><%=weixintoken%></span>
                    <br />
                </div>
                <div class="control-group">
                    <div class="controls">
                        <button type="button" class="btn btn-primary" onclick="Goback();" style=" float:right;">返回管理</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
 </div>
 <script type="text/javascript">
     function Goback() {
         top.location.href = '/default.aspx';
     }
 </script>
</body>
</html>
