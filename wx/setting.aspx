<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setting.aspx.cs" Inherits="Wlniao.Wx.Setting" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>帐号接入信息</title>
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
    <h3 style="font-family:微软雅黑;" >帐号接入信息&nbsp;<a href="weixinsync.aspx" style=" font-size:12px;">同步密码设置</a>&nbsp;<a href="weixinauth.aspx" style=" font-size:12px;">授权设置</a></h3>
</div>
<div class="" style="margin-top:18px">  
    <form action="setting.aspx" method="post" class="well form-horizontal" style="width:680px;margin:0px auto;">
        <div class="row">
            <div class="span7">
                <div class="control-group">
                    <label class="control-label">公众号名称</label>
                    <div class="controls">
                        <input type="text" style=" width:360px;" name="weixinname" value="<%=weixinname %>" placeholder="填写你微信公众帐号的名称" />
                        <p style=" font-size:12px; color:#666666;">填写你微信公众帐号的名称</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">微信原始帐号</label>
                    <div class="controls">
                        <input type="text" style=" width:360px;" name="fristaccount" value="<%=fristaccount %>" placeholder="格式如：gh_daec1921b1a7" />
                        <p style=" font-size:12px; color:#666666;">微信公众帐号的原ID串，<a href="http://doc.wlniao.com/doc/weback/help_weixinaccount.html" target="_blank">怎么查看微信的原始帐号？</a></p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">易信号</label>
                    <div class="controls">
                        <input type="text" style=" width:360px;" name="yixinhao" value="<%=yixinhao %>" placeholder="格式如：dc4d9aaf635ece12" />
                        <%--<p style=" font-size:12px; color:#666666;">微信公众帐号的原ID串，<a href="http://doc.wlniao.com/doc/weback/help_weixinaccount.html" target="_blank">怎么查看微信的原始帐号？</a></p>--%>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label"><font color="red">接口URL（微信）</font></label>
                    <div class="controls" id="apiurlwx">
                        <input type="text" readonly="readonly" style=" width:292px;" value="http://<%=_website%>/wxapi.aspx?a=<%=_account%>#wx" /><button class="btn">复制</button>
                        <p style=" font-size:12px; color:#666666;">设置“微信公众平台接口”配置信息中的接口地址</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label"><font color="red">接口URL（易信）</font></label>
                    <div class="controls" id="apiurlyx">
                        <input type="text" readonly="readonly" style=" width:292px;" value="http://<%=_website%>/wxapi.aspx?a=<%=_account%>#yx" /><button class="btn">复制</button>
                        <p style=" font-size:12px; color:#666666;">请在易信公众平台的高级功能>>开发者模式>>申请开通中的Url处填写此地址</p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label"><font color="red">Token</font></label>
                    <div class="controls" id="token">
                        <input type="text" style=" width:292px;" name="token" value="<%=token %>" /><button class="btn">复制</button>
                        <p style=" font-size:12px; color:#666666;">token用于数据加密,长度为3到32位英文或者数字.请妥善保管, Token 泄露将可能被窃取或篡改微信平台的操作数据.<a href="setting.aspx?changetoken=true">生成新的</a></p>
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
        $("#apiurlwx button").zclip({
            path: '../static/zclip/ZeroClipboard.swf',
            copy: $('#apiurlwx input').val()
        });
        $("#apiurlyx button").zclip({
            path: '../static/zclip/ZeroClipboard.swf',
            copy: $('#apiurlyx input').val()
        });
        $("#token button").zclip({
            path: '../static/zclip/ZeroClipboard.swf',
            copy: $('#token input').val()
        });
    });
</script>
<%=_script%>
</html>
