<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Wlniao._Default" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>微信公众帐号管理系统 - Weback</title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="static/bootstrap/css/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="res/wlniao-style.css" />
    <link rel="stylesheet" href="res/wlniao-iframe.css" />
    <link rel="stylesheet" href="res/wln/font/font-wlniao.css" />
</head>
<body style=" overflow:hidden;">
<div id="header"><h1><a href="#" class="tip-bottom" title="Weback">Weback</a></h1></div>
<div id="user-nav" class="navbar navbar-inverse">
  <ul class="nav">
    <li class=""><a title="" href="javascript:GotoPage('wx/setting.aspx');"><i class="icon icon-cog"></i> <span class="text">帐号接入信息</span></a></li>
    <li class=""><a title="" href="javascript:GotoPage('wx/chart.aspx');"><i class="icon icon-th"></i> <span class="text">报表统计</span></a></li>
    <li class=""><a title="" href="javascript:GotoPage('my/changepwd.aspx');"><i class="icon icon-key"></i> <span class="text">修改密码</span></a></li>
    <li class=""><a title="" href="logout.aspx"><i class="icon-info-sign"></i> <span class="text">注销登录</span></a></li>
  </ul>
  <div style="clear:both;"></div>
</div>
<div id="sidebar">
  <ul>
    <li class="menuli"><a href="javascript:GotoPage('wx/setting.aspx');"><i class="icon icon-cog"></i> <span>帐号接入信息</span></a></li>
    <li class="menuli"><a href="javascript:GotoPage('wx/responsemsg.aspx');"><i class="icon icon-random"></i> <span>首次关注&默认回复</span></a></li>
    <li class="menuli"><a href="javascript:GotoPage('wx/keywords.aspx');"><i class="icon icon-link"></i> <span>关键字自动回复</span></a></li>
    <li class="menuli"><a href="javascript:GotoPage('wx/menuset.aspx');"><i class="icon icon-pushpin"></i> <span>自定义菜单</span></a></li>
    <li class="menuli"><a href="javascript:GotoPage('cms/site.aspx');"><i class="icon icon-list-alt"></i> <span>微网站</span></a></li>
    <li class="menuli"><a href="javascript:GotoPage('appbox.aspx');"><i class="icon icon-list-alt"></i> <span>应用中心</span></a></li>
  </ul>
</div>
<div id="content" style="margin-top:-38px;"></div>
<div class="row-fluid">
  <div id="footer" class="span12"> 2013 &copy; <a href="http://weback.wlniao.com/" target="_blank">Weback</a> &nbsp;&nbsp; 技术支持：<a href="http://www.wlniao.com/" target="_blank">Wlniao Studio</a> </div>
</div>
<script src="res/jquery.min.js"></script>
<script src="res/wlniao.js"></script>
<script src="res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script type="text/javascript">
    var iHeight = 0;
    function init() {
        var winHeight = $(window).height();
        iHeight = winHeight - 70;
        $("#content").height(iHeight);
    }
    init();
    var frameid = "iframepage";
    function setFrameHeight(height) {
        try {
            document.getElementById(frameid).height = height;
        } catch (e) { }
    }
    function iFrameHeight() {
        var ifm = document.getElementById(frameid);
        try {
            var subWeb = document.frames ? document.frames[frameid].document : ifm.contentDocument;
            if (ifm != null) {
                if (subWeb == null) {
                    ifm.height = ifm.ownerDocument.body.scrollHeight;
                    ifm.scrolling = "auto";
                } else {
                    ifm.height = subWeb.body.scrollHeight;
                }
            }
        } catch (e) { }
    }
    function GotoPage(url) {
        var now = new Date();
        var html = '<iframe id="' + frameid + '" src="' + url + '" frameborder="0" marginheight="0" marginwidth="0" scrolling="auto" width="100%" height="' + iHeight + '"></iframe> ';
        document.getElementById('content').innerHTML = html;
    }
    function showTips(msg, icon, url) {
        var _icon = 1;
        if (icon) {
            _icon = icon;
        }
        ZENG.msgbox.show('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + msg + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;', _icon, 1800);
        if (url) {
            setTimeout(GotoPage(url), 100);
        }
    }
    GotoPage('wx/chart.aspx');
</script>
<link href="static/tips/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="static/tips/tips.js"></script>
</body>
</html>

