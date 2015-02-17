<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="keywords.aspx.cs" Inherits="Wlniao.Wx.Keywords" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>关键字自动回复规则列表</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 12px 0px 12px;}
      .container-narrow h4 
      {
          font-family:微软雅黑; font-size:smaller; line-height:1em;
      }
      .container-narrow p 
      {
          font-family:楷体;
          color:#999999;
      }
      .pagerlist {
        margin: 0 auto;
        max-width: 680px;
      }
      .pagerlist ul
      {
          list-style:none;
      }
      .pagerlist li
      {
          clear:both;
          line-height:1.8em;
          border:none;
          border-bottom:1px dotted #eaeaea;
      }
      .pagerlist li em
      {
          float:right;
      }
      .pagerlist li i
      {
          color:#999999; padding-left:8px;
      }
      
      /*rule*/
    .rule_item{border:1px solid #E1E1E1; border-radius:3px; margin:20px; background-color:white;}
    .rule_content{border-bottom:1px solid #E1E1E1; padding:0 10px; height:30px; line-height:30px; background:#EEE;}
    .rule_content .data{font-weight:bold; color:#333;}
    .rule_desc{padding:10px 10px 7px 10px; border-bottom:2px #E1E1E1 solid; border-top:1px #FFF solid; background:#F9F9F9;}
    .rule_kw{display:inline-block; padding:0 10px; border-radius:3px; margin-bottom:3px; background:#E7E7E7; color:#888;}
    .fl{ float:left;}
    .fr{ float:right;}
    .clearfix{ clear:both;}

.news{width:678px;min-height:757px;float:left;padding:15px;}
.news li{width:665px;float:left;line-height:26px;background:url(../img/dian_2.gif) no-repeat left 11px;padding-left:13px;}
.news li a{float:left;font-size:14px;color:#333333;}
.news li em{float:right;color:#999999;}
.news ul{width:678px;float:left;margin-top:5px;padding-bottom:5px;background:url(../img/dian_1.gif) repeat-x left bottom;}

.page{width:678px;float:left;padding:10px 0px 23px 0px;text-align:center;}
.page a{margin:0px 2px;border:1px solid #CCDBE4;line-height:22px;color:#1044BA;display:inline-block;padding:0px 6px;}
.page span{margin:0px 2px;border:1px solid #CCDBE4;line-height:22px;color:#DBE1E6;display:inline-block;padding:0px 6px;}
.page em{margin:0px 2px;line-height:22px;color:#333333;display:inline-block;padding:0px 6px;}

    </style>
</head>
<body>
    <div class="container-narrow">
      <h3 style=" font-family:微软雅黑;">关键字自动回复&nbsp;<font  style=" font-size:12px;">【&nbsp;<a href="keyword.aspx" style=" font-size:12px;">添加关键字</a>&nbsp;·&nbsp;<a href="keywords.aspx?show=1" style=" font-size:12px;">系统关键字</a>&nbsp;】</font></h3>
      <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;设置用户向公众帐号发送你指定的关键字时，自动回复的消息</p>
    </div>
    <div class="pagerlist">
        <%=_ListStr%>
        <%=_PageBar%>
    </div>
    <script type="text/javascript">
        function Del(key) {
            if (confirm("删除操作不可恢复，确认要删除吗?")) {
                self.location.href = "keyword.aspx?method=del&kw=" + key;
            }
        }
    </script>
</body>
</html>
