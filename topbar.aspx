<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topbar.aspx.cs" Inherits="Wlniao.Topbar" %>
document.title=document.title+'|微信互动管理';

document.body.style.paddingTop = '58px';
document.write('    <div class="navbar navbar-fixed-top" style="z-index:9999;">');
document.write('      <div class="navbar-inner">');
document.write('        <div class="container-fluid">');
document.write('		  <a class="brand" href="#" style="width:210px;"> <img alt="Charisma Logo" src="/static/logo20.png" /> <span>Weback<%=ShowName %></span></a>');
document.write('          <div class="btn-group pull-right">');
document.write('			<a class="btn btn-success" href="#">');
document.write('			  <i class="icon-bullhorn" id="bullhorn"></i> Weback系统内测活动开启了！');
document.write('			</a>');
document.write('          </div>');
document.write('          <div class="nav-collapse collapse">');
document.write('            <ul class="nav">');
//document.write('              <li><a href="/logout.aspx">注销</a></li>');
document.write('            </ul>');
document.write('          </div>');
document.write('        </div>');
document.write('      </div>');
document.write('    </div>');