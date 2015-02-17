<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="navjs.aspx.cs" Inherits="Wlniao.navjs" %>
<%if(_style==1){ %>
window.onload = function () {
    var navigator = '<a id="navigator" href="javascript:navToggle();" style="width:66px;height:33px;background:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIwAAABQCAYAAADLPJ1qAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAALzSURBVHhe7d1NiE5hGMbx6z3zbUikbDS+dpSUZEENhRVKiWhKlBLlK7spC0W2LDUrhdla+VxQmhjzUSJrKQsrCwtEXI85i+ntLNxxzpzT/K/6LXXuui/P87yrESGVZlwanJAuvZLu2wQqN2rDL6WN+UrqGQ95xN66LL9QG0/Tf+B8RfXIG6nbg420DYoacWkue1WtmY3NcTwQZWmA59JVr6vLsj+Lm4t4kP3tg6G+7nhfXtsC67TqTxwPMd0+FOrrmTTVJ63w6hZZOm2qK82ktKloKNTbSf846ZFWeYWpNOmkqSb+RXShaCDU23W1bvapc0t+0qTrqZo3jQtzu2gg1NuoWmPd6tjTpa4NC6VlXmW6msqPP/6ofRjU3z3pXa+yoR51bM+vpnTKlP+W8QkzUTQQ6u2B9KFH2QmfMnt9yqxfLC3xOjtmtlpiXJjXRQOh3h5LH12Y0z5lDvots7lfWu51ln8tUZhmSoXpVnYmXUu96tyWP359O5UcCtNMeWHOujBH/Y7Z0Sut9Drdm5JDYZopFaZP2TlfS8dcmJ2zHr7lhsI0U16Y83lhdrkwq71OCoNiswpz3IXZ7cKs8TopDIpRGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGIRQGITMZWHeFw2Eensifaq8MGPS0qJhUH8vpK9rlQ1XWphxaV/RMGiGU2qNVFoYX0ejRYOgGe6qNV1ZYfzBAfsxewA0i2+In1uVXayqMLfaB0DzpD/lV3phJqVDRR9HM11T60pphfExNuiPfG7/KJrLv5i+3JAOe739M1v+T/Ej94B9K/ooms0HwfeH0lC+6n/LlLTOReHPDc8Dac9p3/nq/z7+xwPprUJR5qe09/ytOpBXghBCCCGEEEIIIYQQQgghhBBCCKlVpN/FUdSxGUg5xQAAAABJRU5ErkJggg==) no-repeat 0 0;-webkit-background-size:70px auto;background-size:70px auto;font-family:微软雅黑,Helvetica;color:#fff;text-decoration: none;font-size:16px;line-height:32px;text-align:center;position:absolute;top:-36px;left:5px;" >导航</a>\
            <ul id="navigator_ul" style="margin:0px;padding:0px;display:none;">\
                <li style="float:left;width:25%;height:80px;box-sizing:border-box;border-right:1px solid #080808;border-bottom :1px solid #080808;position:relative;text-align:center;overflow:hidden;">    <a style="font-family:微软雅黑,Helvetica;color:#fff;font-size:15px;text-decoration:none;display:block;height:80px;line-height:130px;background:url(<%=list[0].Src %>) no-repeat center 8;-webkit-background-size:48px auto;background-size:48px auto;" href="<%=list[0].Value %>"><%=list[0].Title %></a></li>\
                <li style="float:left;width:25%;height:80px;box-sizing:border-box;border-right:1px solid #080808;border-bottom :1px solid #080808;position:relative;text-align:center;overflow:hidden;border-left:1px solid #575757;">    <a style="font-family:微软雅黑,Helvetica;color:#fff;font-size:15px;text-decoration:none;display:block;height:80px;line-height:130px;background:url(<%=list[1].Src %>) no-repeat center 8;-webkit-background-size:48px auto;background-size:48px auto;" href="<%=list[1].Value %>"><%=list[1].Title %></a></li>\
                <li style="float:left;width:25%;height:80px;box-sizing:border-box;border-right:1px solid #080808;border-bottom :1px solid #080808;position:relative;text-align:center;overflow:hidden;border-left:1px solid #575757;">    <a style="font-family:微软雅黑,Helvetica;color:#fff;font-size:15px;text-decoration:none;display:block;height:80px;line-height:130px;background:url(<%=list[2].Src %>) no-repeat center 8;-webkit-background-size:48px auto;background-size:48px auto;" href="<%=list[2].Value %>"><%=list[2].Title %></a></li>\
                <li style="float:left;width:25%;height:80px;box-sizing:border-box;border-right:1px solid #080808;border-bottom :1px solid #080808;position:relative;text-align:center;overflow:hidden;border-left:1px solid #575757;">    <a style="font-family:微软雅黑,Helvetica;color:#fff;font-size:15px;text-decoration:none;display:block;height:80px;line-height:130px;background:url(<%=list[3].Src %>) no-repeat center 8;-webkit-background-size:48px auto;background-size:48px auto;" href="<%=list[3].Value %>"><%=list[3].Title %></a></li>\
                <li style="float:left;width:25%;height:80px;box-sizing:border-box;border-right:1px solid #080808;border-bottom :1px solid #080808;position:relative;text-align:center;overflow:hidden;border-top:1px solid #575757;">    <a style="font-family:微软雅黑,Helvetica;color:#fff;font-size:15px;text-decoration:none;display:block;height:80px;line-height:130px;background:url(<%=list[4].Src %>) no-repeat center 8;-webkit-background-size:48px auto;background-size:48px auto;" href="<%=list[4].Value %>"><%=list[4].Title %></a></li>\
                <li style="float:left;width:25%;height:80px;box-sizing:border-box;border-right:1px solid #080808;border-bottom :1px solid #080808;position:relative;text-align:center;overflow:hidden;border-top:1px solid #575757;border-left:1px solid #575757;">    <a style="font-family:微软雅黑,Helvetica;color:#fff;font-size:15px;text-decoration:none;display:block;height:80px;line-height:130px;background:url(<%=list[5].Src %>) no-repeat center 8;-webkit-background-size:48px auto;background-size:48px auto;" href="<%=list[5].Value %>"><%=list[5].Title %></a></li>\
                <li style="float:left;width:25%;height:80px;box-sizing:border-box;border-right:1px solid #080808;border-bottom :1px solid #080808;position:relative;text-align:center;overflow:hidden;border-top:1px solid #575757;border-left:1px solid #575757;">    <a style="font-family:微软雅黑,Helvetica;color:#fff;font-size:15px;text-decoration:none;display:block;height:80px;line-height:130px;background:url(<%=list[6].Src %>) no-repeat center 8;-webkit-background-size:48px auto;background-size:48px auto;" href="<%=list[6].Value %>"><%=list[6].Title %></a></li>\
                <li style="float:left;width:25%;height:80px;box-sizing:border-box;border-right:1px solid #080808;border-bottom :1px solid #080808;position:relative;text-align:center;overflow:hidden;border-top:1px solid #575757;border-left:1px solid #575757;">    <a style="font-family:微软雅黑,Helvetica;color:#fff;font-size:15px;text-decoration:none;display:block;height:80px;line-height:130px;background:url(<%=list[7].Src %>) no-repeat center 8;-webkit-background-size:48px auto;background-size:48px auto;" href="<%=list[7].Value %>"><%=list[7].Title %></a></li>\
            </ul>';
    var childNode = document.createElement("nav");
    childNode.setAttribute("class", "mod-menu");
    childNode.setAttribute("style", "background:rgba(34,34,34,0.9);border-top:3px solid #c30000;position:fixed;bottom:0;z-index:100;width:100%;-webkit-transition: all 0.2s ease-in-out;-moz-transition: all 0.2s ease-in-out;-ms-transition: all 0.2s ease-in-out; -o-transition: all 0.2s ease-in-out;transition: all 0.2s ease-in-out;-webkit-tap-highlight-color:rgba(0,0,0,0);");
    childNode.innerHTML = navigator;
    document.body.appendChild(childNode);
}
function navToggle() {
    var navigator_ul = document.getElementById('navigator_ul');
    if (navigator_ul.style.display == 'none') {
        navigator_ul.style.display = '';
    } else {
        navigator_ul.style.display = 'none';
    }
}
<%}else if(_style==2){ %>
    document.write(unescape("%3Cscript src='/BaseData/Style/zepto.min.js' type='text/javascript'%3E%3C/script%3E"));
    document.write(unescape("%3Cscript src='/wsite/minibar1/minibar.js' type='text/javascript'%3E%3C/script%3E"));
    window.onload = function () {
        var navigator = '<div class=\"phone\"><div class=\"plug-phone\"><div class=\"plug-menu bgcolor\"><span class=\"close\"></span></div><div class=\"bgcolor plug-btn plug-btn1 close\"><a href=\"#\"><span class=\"p-icon\"></span></a></div><div class=\"bgcolor plug-btn plug-btn2 close\"><a href=\"#\"><span class=\"p-icon\"></span></a></div><div class=\"bgcolor plug-btn plug-btn3 close\"><a href=\"#\"><span class=\"p-icon\"></span></a></div><div class=\"bgcolor plug-btn plug-btn4 close\"><a href=\"#\"><span class=\"p-icon\"></span></a></div></div></div>';
        var childNode = document.createElement("div");
        childNode.innerHTML = navigator;
        document.body.appendChild(childNode);
    
    }
    var __ele;
    setTimeout(function (){
            var scripts = document.getElementsByTagName("script");
            for (var i = 0, l = scripts.length; i < l; i++) {
                var src = scripts[i].src;
                if (src.indexOf("minibar.js") != -1) {
                    __ele = scripts[i];
                    break;
                }
            }
            var __css = '<link href="/wsite/minibar1/css.css" rel="stylesheet" type="text/css" />';
            $(__ele).after(__css);
            //path
            $(".plug-menu").click(function () {
                var span = $(this).find("span");
                if (span.attr("class") == "open") {
                    span.removeClass("open");
                    span.addClass("close");
                    $(".plug-btn").removeClass("open");
                    $(".plug-btn").addClass("close");
                } else {
                    span.removeClass("close");
                    span.addClass("open");
                    $(".plug-btn").removeClass("close");
                    $(".plug-btn").addClass("open");
                }
            });
            $(".plug-menu").on('touchmove', function (event) { event.preventDefault(); })
        }, 800);
<%} %>