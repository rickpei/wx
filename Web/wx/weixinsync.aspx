<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weixinsync.aspx.cs" Inherits="Wlniao.Wx.WeixinSync" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>公众帐号同步信息设置</title>
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
    <h3 style="font-family:微软雅黑;" ><a href="setting.aspx" style=" font-size:12px;">帐号接入信息</a>&nbsp;同步密码设置&nbsp;<a href="weixinauth.aspx" style=" font-size:12px;">授权设置</a></h3> 
</div>
<div class="" style="margin-top:18px"> 
    <form action="" method="post" class="well form-horizontal" style="width:680px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <div class="control-group">
                    <label class="control-label">微信公众登录用户</label>
                    <div class="controls">
                        <input type="text" style=" width:360px;" name="weixinmpaccount" value="<%=weixinmpaccount %>"  />
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">微信公众登录密码</label>
                    <div class="controls">
                        <input type="password" style=" width:360px;" name="weixinmppassword" value="<%=weixinmppassword %>"  />
                    </div>
                </div>     
                <div class="control-group">
                    <label class="control-label"><font color="red">*</font></label>
                    <div class="controls">
                        <div class="notice">1.此处填写的是您微信公众平台登录使用的帐号和密码；</div>
                        <div class="notice">2.通过此处设置,系统将会为您自动抓取部分信息；</div>
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
