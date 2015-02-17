<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weixinauth.aspx.cs" Inherits="Wlniao.Wx.WeixinAuth" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>公众帐号授权设置</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../static/zclip/jquery.zclip.min.js" type="text/javascript"></script>
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 12px 0px 12px;}
    </style>
</head>
<body>
<div class="container-narrow">
    <h3 style="font-family:微软雅黑;" ><a href="setting.aspx" style=" font-size:12px;">帐号接入信息</a>&nbsp;<a href="weixinsync.aspx" style=" font-size:12px;">同步密码设置</a>&nbsp;授权设置</h3> 
</div>
<div class="" style="margin-top:18px"> 
    <form action="" method="post" class="well form-horizontal" style="width:680px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <div class="control-group">
                    在使用部分高级功能前，需要在此处授权
                </div>
                <div class="control-group">
                    <label class="control-label">AppId</label>
                    <div class="controls">
                        <input type="text" style=" width:360px;" name="appid" value="<%=appid %>"  />
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">AppSecret</label>
                    <div class="controls">
                        <input type="password" style=" width:360px;" name="appsecret" value="<%=appsecret %>"  />
                    </div>
                </div>     
                <div class="control-group">
                    <label class="control-label"><font color="red">*</font></label>
                    <div class="controls">
                        <div class="notice">1. 要在微信公众平台“开发模式”下使用自定义菜单，首先要在公众平台申请自定义菜单使用的AppId和AppSecret；</div>
                    </div>
                </div>              

                <div class="control-group">
                    <label class="control-label">&nbsp;</label>
                    <div class="controls"><font color="red"><%=msg%></font></div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <input type="hidden" name="method" value="save" />
                        <button type="submit" class="btn btn-primary">保存设置</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
 </div>
</body>
<script type="text/javascript">
    $(function () {
        $("#apiurl button").zclip({
            path: '../static/zclip/ZeroClipboard.swf',
            copy: $('#apiurl input').val()
        });
        $("#token button").zclip({
            path: '../static/zclip/ZeroClipboard.swf',
            copy: $('#token input').val()
        });
    });
</script>
<%=_script%>
</html>
