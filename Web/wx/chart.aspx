<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chart.aspx.cs" Inherits="Wlniao.Wx.Chart" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>公众帐号统计信息</title>
    <link href="../static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/jquery.js" type="text/javascript"></script>
    <script src="../static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../static/amline/swfobject.js" type="text/javascript"></script>
</head>
<body>
<div class="" style="margin-top:18px; text-align:center;">  
                            <div style=" z-index:1; height:398px;" >
	                        <div id="flashcontent">
		                        <strong>数据报表正在加载中</strong>
	                        </div>
                            </div>
                            <div style=" text-align:center; clear:both; padding:0px; width:100%;">
                                <p>新增粉丝：<%=ints[0] %> 人 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 总粉丝数：<%=ints[1] %> 人 </p>
                                <p>今日推送：<%=ints[2] %> 次 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 总共推送：<%=ints[3] %> 次 </p>
                            </div>
 </div>
</body>
    <script type="text/javascript">
		// <![CDATA[
        var so = new SWFObject("../static/amline/amline.swf", "amline", "720", "400", "8", "#FFFFFF");
        so.addVariable("path", "../static/amline/");
        so.addVariable("settings_file", encodeURIComponent("../static/amline/amline_settings.xml"));                // you can set two or more different settings files here (separated by commas)
        so.addVariable("data_file", encodeURIComponent("chartdata.aspx"));

        //	so.addVariable("chart_data", encodeURIComponent("data in CSV or XML format"));                    // you can pass chart data as a string directly from this file
        //	so.addVariable("chart_settings", encodeURIComponent("<settings>...</settings>"));                 // you can pass chart settings as a string directly from this file
        //	so.addVariable("additional_chart_settings", encodeURIComponent("<settings>...</settings>"));      // you can append some chart settings to the loaded ones
        //  so.addVariable("loading_settings", "LOADING SETTINGS");                                           // you can set custom "loading settings" text here
        //  so.addVariable("loading_data", "LOADING DATA");                                                   // you can set custom "loading data" text here
        //	so.addVariable("preloader_color", "#999999");
        //  so.addVariable("error_loading_file", "ERROR LOADING FILE");                                   // you can set custom "error loading file" text here

        so.addVariable("wmmode", "transparent");
        so.addVariable("wmmode", "opaque");
        so.write("flashcontent");
        // ]]>
    </script>
</html>
