<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createsuccess.aspx.cs" Inherits="Wlniao.My.CreateSuccess" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>创建成功</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<div class="container" style="margin-top:100px">  
    <div class="well" style="width:580px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <h3>成功啦</h3>
                <p class="lead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;恭喜你，您的帐号已创建成功，接下来开始设置您的微信公众帐号吧！</p>
                <div class="control-group">
                    <div class="controls">
                        <button type="button" class="btn btn-primary" onclick="Goback();" style=" float:right;">现在开始</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
 </div>
 <script type="text/javascript">
     function Goback() {
         top.location.href = 'init.aspx';
     }
 </script>
</body>
</html>
