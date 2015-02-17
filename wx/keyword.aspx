<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="keyword.aspx.cs" ValidateRequest="false" Inherits="Wlniao.Wx.KeyWord" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>消息回复</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../static/common.js" type="text/javascript"></script>
    <script src="../static/emotions.js" type="text/javascript"></script>
    <style type="text/css">
        .container-narrow {margin: 0 auto;padding:0px 12px 0px 12px;}
      #main-column.member-center .content {
        width: auto;
        margin: auto;
        }
      #main-column.member-center .txt,#main-column.member-center .sel {
        margin-bottom:0px;
        }
        .form .txt {
        border: 1px solid #D5D5D5;
        border-radius: 3px;
        font-size: 14px;
        line-height: 20px;
        color: #333;
        }
        .txt 
        {
        height: 17px;
        font-size: 14px;
        padding: 9px 10px 7px 10px;
        border: none;
        color: #999;
        background: white url('../static/img/bg-form-txt.gif') top left repeat-x;
        }
        .form td .notice {
        font-size: 12px;
        color: #999;
        width:560px;
        margin-top: 3px;
        margin-bottom:0px;
        }

        .iconEmotion {
        background: url('../static/img/iconEmotion.png') no-repeat transparent;
        padding-left: 18px;
        display: inline-block;
        height: 18px;
        text-decoration: none;
        color: #84bce2;
        }
        .resconfig{ display:none;}

        /*emotions*/
        .emotions{position:absolute;top:30px;left:20px;border:1px solid #AAA;padding:5px;background-color:#FFF;z-index:9999999;display:none}
        .emotions table td{padding:1px;}
        .emotions table td div{background: url("https://res.mail.qq.com/zh_CN//images/mo/DEFAULT2/default.gif") no-repeat 0 0 scroll transparent;width:24px;height:24px;cursor:pointer; border:1px solid #dfe6f6;}
        .emotions table div:hover{border:1px solid blue;}
        .emotions .emotionsGif{position:absolute;top:-1px;left:430px;border:1px solid #AAA;padding:20px;background-color:#FFF;text-align:center;width:24px;height:24px}
        
        .txtmsgitem {padding: 5px 20px;background-color: #eee;padding: 10px 20px;border-radius: 3px;margin:10px 0 !important; box-shadow:0px 1px 3px #CCC;}
        .txtmsgitem span{ float:right;}
        .txtmsgitem span a{ font-family:微软雅黑; cursor:pointer;}
        
        .newsli{ border-bottom:1px solid #999999; clear:both; display:block; padding-bottom:8px;}
        .newslipic{float:left; height:100px;width:100px;border: 1px solid #B8B8B8;overflow: hidden;}
        .newslipic p{text-shadow: 0 1px 1px white;background: #F5F6F7;display: block;text-align: center;color: #666;letter-spacing: 5px;font-weight: bold;font-size: 22px;line-height: 100px;}
        .newslipic img{color: transparent;height:100px;width:100px;font-size: 0;vertical-align: middle;-ms-interpolation-mode: bicubic;}
        .newslitxt{ margin:0px 0px 0px 120px;}
        .newslitxt h4,.newslitxt div{  width:500px;word-wrap:break-word;}
        .newslitxt div{ display:none;}
        
</style>
</head>
<body>
    <div class="container-narrow">
        <h3 style=" font-family:微软雅黑;">自动回复规则 <a href="keywords.aspx" style=" font-size:12px;">返回列表</a></h3>
        <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
        <div id="main-column" class="row-fluid marketing member-center">            
            <div class="form">      
                <form action="" method="post">
				<table border="0" cellspacing="0" cellpadding="0" width="100%">
					<tbody>
                    <tr>
                    	<th valign="top" align="right">关键词：</th>
						<td><input type="text" id="keyword" name="keyword" class="txt grid-4 alpha pin" value="<%=keyword%>">
                        <div class="notice">删除，修改规则、关键字以及回复后，请提交规则以保存操作。</div></td>
                    </tr>
                    <tr>
                    	<th valign="top" align="right">模式：</th>
						<td>
                        <select id="msgmode" name="msgmode" class="sel" onchange="showModeTips();">
                            <option value="equal" selected="selected">完全等于上述关键字</option>
                            <option value="has">包含上述关键字</option>
                            <%--<option value="menus">自定义会话菜单</option>--%>
                        </select>
                        <div class="notice" id="msgmodetips">用户进行微信交谈时，对话中包含上述关键字就执行这条规则。</div></td>
                    </tr>
                    <tr>
                    	<th valign="top" align="right">描述：</th>
						<td>
                        <input type="text" name="description" class="txt grid-4 alpha pin" value="<%=description%>">
                        <div class="notice">您可以给这条规则起一个名字或描述一下功能, 方便下次修改和查看.</div>
                        </td>
                    </tr>
                    <tr>
                    	<th valign="top" align="right">类型：</th>
						<td>
                        <select id="msgtype" name="msgtype" class="sel" onchange="showMsgtypeTips();">
                            <option value="basic" selected="selected">基本文字回复</option>
                            <option value="news">混合图文回复</option>
                            <option value="music">基本语音回复</option>
                            <option value="api">API扩展接口</option>
                        </select>
                        <div class="notice" id="msgtypetips"></div></td>
                    </tr>
                    <tr class="resconfig textmsg">
						<th valign="top" align="right">回复：</th>
						<td>
                            <input type="button" value="添加内容" onclick="addText();" class="mt10 btn grid-2 alpha" />
                            <div class="notice">基本文字内容有多条时，将随机发送一条.</div>
                            <div id="txtmsglist" style=" min-height:20px;"></div>
                            <!-- Modal -->
                            <div id="txtModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="txtModalLabel" aria-hidden="true">
                                <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <h3 id="txtModalLabel">回复内容</h3>
                                </div>
                                <div class="modal-body">
							    <textarea name="textmsg" id="textmsg" cols="55"  class="txt content" style="width:510px;height:200px; "></textarea>
                                <div class="notice" style=" width:380px;">设置用户发送当前关键字时自动回复的内容。<a class="iconEmotion" href="javascript:;" inputid="textmsg">表情</a></div>
                                </div>
                                <div class="modal-footer">
                                <button class="btn" data-dismiss="modal" aria-hidden="true">关闭</button>
                                <button class="btn btn-primary" onclick="return txtSave();">确定</button>
                                </div>
                            </div>
						</td>
					</tr>
                    <tr class="resconfig musicmsg">
                    	<th valign="top" align="right">音乐标题：</th>
						<td>
                        <input type="text" id="musictitle" class="txt grid-4 alpha pin">
                        <div class="notice">&nbsp;</div>
                        </td>
                    </tr>
                    <tr class="resconfig musicmsg">
                    	<th valign="top" align="right">音乐连接：</th>
						<td>
                        <input type="text" id="musicurl" class="txt grid-4 alpha pin">
                        <div class="notice">音乐文件的连接地址 &nbsp;</div>
                        <div><span id="mp3MusicUrl"></span><span id="swfUploadMusic"></span></div>                        
                        </td>
                    </tr>
                    <tr class="resconfig musicmsg">
                    	<th valign="top" align="right">高品质连接：</th>
						<td>
                        <input type="text" id="musichdurl" class="txt grid-4 alpha pin">
                        <div class="notice">在Wifi环境下，会优先采用高品质文件</div>
                        </td>
                    </tr>
                    <tr class="resconfig musicmsg">
                    	<th valign="top" align="right">音乐描述：</th>
						<td>
                        <input type="text" id="musicdesc" class="txt grid-4 alpha pin">
                        <div class="notice">描述内容将出现在音乐名称下方，建议控制在20个汉字以内最佳</div>
                        </td>
                    </tr>

                    
                    <tr class="resconfig newsmsg">
                    	<th valign="top" align="right">回复：</th>
						<td>
                            <div id="newsModal" class="hide" style=" border:1px solid #eee; margin-bottom:18px;">
                                <div class="modal-header">
                                <button type="button" class="close" onclick="return newsCancel();">×</button>
                                <h3 id="newsModalLabel">图文消息设置</h3>
                                </div>
                                <div style=" padding-top:10px;">
                                <span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">标题:</span><input type="text" id="newstitle" class="txt grid-4 alpha pin" style=" width:373px;" />
                                <div style=" line-height:8px;">&nbsp;</div>
                                <span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">封面:</span>&nbsp;<img id="imgPicUrl" src="#" style=" width:120px;" /><span id="swfUploadPic"></span>
                                <div style=" line-height:8px;">&nbsp;</div>
                                <span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">描述:</span><input type="text" id="newsdesc" class="txt grid-4 alpha pin" style=" width:373px;" />
                                <div style=" line-height:8px;">&nbsp;</div>
                                <span style="width:90px; vertical-align:top; font-family:微软雅黑; display:inline-block; text-align:right;">内容:</span><div style=" display:inline-block;"><script id="ListContent" type="text/plain"></script></div><br />
                                <div class="notice" style=" clear:both; width:380px; padding-left:98px;">图文内容，支持部分HTML代码</div>
                                <span style="width:90px; font-family:微软雅黑; display:inline-block; text-align:right;">连接:</span><input type="text" id="newsurl" class="txt grid-4 alpha pin" style=" width:373px;" />
                                <div style=" line-height:8px;">&nbsp;</div>
                                </div>
                                <div class="modal-footer">
                                <button class="btn" onclick="return newsCancel();">取消</button>
                                <button class="btn btn-primary" onclick="return newsSave();">确定</button>
                                </div>
                            </div>
                            <div id="newsPlane" style=" background-color:#eeeeee; width:620px; padding:10px; margin-bottom:10px;">
                                <div class="newsli">
                                    <div style="float:left; height:160px;width:300px;border: 1px solid #B8B8B8;overflow: hidden;">
				                        <p id="newspicmainnull" style="text-shadow: 0 1px 1px white;background: #F5F6F7;display: block;text-align: center;color: #666;letter-spacing: 5px;font-weight: bold;font-size: 22px;line-height: 160px;">封面图片</p>
				                        <img id="newspicmain" src="" link="" style="height:160px;width:300px;color: transparent;font-size: 0;vertical-align: middle;-ms-interpolation-mode: bicubic;">
			                        </div>
                                    <div style="margin: 0 0 0 320px;">
				                        <h4 id="newstitlemain" style=" width:300px;word-wrap:break-word;"></h4>
				                        <div id="newsdescmain" style=" width:300px;word-wrap:break-word;"></div>
				                        <div id="newscontentmain" style=" display:none;"></div>
                                        <div><a href="javascript:;" onclick="newsEdit();">编辑</a></div>
			                        </div>
                                    <div class="clearfix">&nbsp;</div>
                                </div>
                                <div id="newsitemlist"></div>
                                <button class="btn" style=" margin-top:6px;" onclick="return newsItemAdd('','','','','');">添加内容</button>
                            </div>
                            
                        </td>
                    </tr>
                    <tr class="resconfig apimsg">
                    	<th valign="top" align="right">ApiUrl：</th>
						<td>
                        <input type="text" id="apiurl" class="txt grid-4 alpha pin" />
                        <div class="notice">请填写从API提供方获取的API接口地址</div>
                        </td>
                    </tr>
					<tr>
						<th></th>
						<td>
							<input name="submit" type="submit" value="提交" onclick="return checkConfig();" class="mt10 btn grid-2 alpha" />
                            <input name="method" type="hidden" value="save" />
                            <input name="config" id="config" type="hidden" value="<%=config %>" />
						</td>
					</tr>
				</tbody></table>
                </form>
            </div> 
        </div>
      
	    <div class="emotions" style="left: 807px; top: 409px; display: none;"><table cellspacing="0" cellpadding="0"><tbody><tr><td><div class="eItem" style="background-position:0px 0;" data-title="微笑" data-code="::)" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/0.gif"></div></td><td><div class="eItem" style="background-position:-24px 0;" data-title="撇嘴" data-code="::~" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/1.gif"></div></td><td><div class="eItem" style="background-position:-48px 0;" data-title="色" data-code="::B" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/2.gif"></div></td><td><div class="eItem" style="background-position:-72px 0;" data-title="发呆" data-code="::|" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/3.gif"></div></td><td><div class="eItem" style="background-position:-96px 0;" data-title="得意" data-code=":8-)" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/4.gif"></div></td><td><div class="eItem" style="background-position:-120px 0;" data-title="流泪" data-code="::&lt;" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/5.gif"></div></td><td><div class="eItem" style="background-position:-144px 0;" data-title="害羞" data-code="::$" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/6.gif"></div></td><td><div class="eItem" style="background-position:-168px 0;" data-title="闭嘴" data-code="::X" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/7.gif"></div></td><td><div class="eItem" style="background-position:-192px 0;" data-title="睡" data-code="::Z" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/8.gif"></div></td><td><div class="eItem" style="background-position:-216px 0;" data-title="大哭" data-code="::&#39;(" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/9.gif"></div></td><td><div class="eItem" style="background-position:-240px 0;" data-title="尴尬" data-code="::-|" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/10.gif"></div></td><td><div class="eItem" style="background-position:-264px 0;" data-title="发怒" data-code="::@" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/11.gif"></div></td><td><div class="eItem" style="background-position:-288px 0;" data-title="调皮" data-code="::P" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/12.gif"></div></td><td><div class="eItem" style="background-position:-312px 0;" data-title="呲牙" data-code="::D" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/13.gif"></div></td><td><div class="eItem" style="background-position:-336px 0;" data-title="惊讶" data-code="::O" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/14.gif"></div></td></tr><tr><td><div class="eItem" style="background-position:-360px 0;" data-title="难过" data-code="::(" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/15.gif"></div></td><td><div class="eItem" style="background-position:-384px 0;" data-title="酷" data-code="::+" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/16.gif"></div></td><td><div class="eItem" style="background-position:-408px 0;" data-title="冷汗" data-code=":--b" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/17.gif"></div></td><td><div class="eItem" style="background-position:-432px 0;" data-title="抓狂" data-code="::Q" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/18.gif"></div></td><td><div class="eItem" style="background-position:-456px 0;" data-title="吐" data-code="::T" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/19.gif"></div></td><td><div class="eItem" style="background-position:-480px 0;" data-title="偷笑" data-code=":,@P" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/20.gif"></div></td><td><div class="eItem" style="background-position:-504px 0;" data-title="可爱" data-code=":,@-D" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/21.gif"></div></td><td><div class="eItem" style="background-position:-528px 0;" data-title="白眼" data-code="::d" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/22.gif"></div></td><td><div class="eItem" style="background-position:-552px 0;" data-title="傲慢" data-code=":,@o" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/23.gif"></div></td><td><div class="eItem" style="background-position:-576px 0;" data-title="饥饿" data-code="::g" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/24.gif"></div></td><td><div class="eItem" style="background-position:-600px 0;" data-title="困" data-code=":|-)" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/25.gif"></div></td><td><div class="eItem" style="background-position:-624px 0;" data-title="惊恐" data-code="::!" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/26.gif"></div></td><td><div class="eItem" style="background-position:-648px 0;" data-title="流汗" data-code="::L" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/27.gif"></div></td><td><div class="eItem" style="background-position:-672px 0;" data-title="憨笑" data-code="::&gt;" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/28.gif"></div></td><td><div class="eItem" style="background-position:-696px 0;" data-title="大兵" data-code="::,@" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/29.gif"></div></td></tr><tr><td><div class="eItem" style="background-position:-720px 0;" data-title="奋斗" data-code=":,@f" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/30.gif"></div></td><td><div class="eItem" style="background-position:-744px 0;" data-title="咒骂" data-code="::-S" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/31.gif"></div></td><td><div class="eItem" style="background-position:-768px 0;" data-title="疑问" data-code=":?" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/32.gif"></div></td><td><div class="eItem" style="background-position:-792px 0;" data-title="嘘" data-code=":,@x" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/33.gif"></div></td><td><div class="eItem" style="background-position:-816px 0;" data-title="晕" data-code=":,@@" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/34.gif"></div></td><td><div class="eItem" style="background-position:-840px 0;" data-title="折磨" data-code="::8" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/35.gif"></div></td><td><div class="eItem" style="background-position:-864px 0;" data-title="衰" data-code=":,@!" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/36.gif"></div></td><td><div class="eItem" style="background-position:-888px 0;" data-title="骷髅" data-code=":!!!" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/37.gif"></div></td><td><div class="eItem" style="background-position:-912px 0;" data-title="敲打" data-code=":xx" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/38.gif"></div></td><td><div class="eItem" style="background-position:-936px 0;" data-title="再见" data-code=":bye" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/39.gif"></div></td><td><div class="eItem" style="background-position:-960px 0;" data-title="擦汗" data-code=":wipe" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/40.gif"></div></td><td><div class="eItem" style="background-position:-984px 0;" data-title="抠鼻" data-code=":dig" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/41.gif"></div></td><td><div class="eItem" style="background-position:-1008px 0;" data-title="鼓掌" data-code=":handclap" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/42.gif"></div></td><td><div class="eItem" style="background-position:-1032px 0;" data-title="糗大了" data-code=":&amp;-(" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/43.gif"></div></td><td><div class="eItem" style="background-position:-1056px 0;" data-title="坏笑" data-code=":B-)" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/44.gif"></div></td></tr><tr><td><div class="eItem" style="background-position:-1080px 0;" data-title="左哼哼" data-code=":&lt;@" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/45.gif"></div></td><td><div class="eItem" style="background-position:-1104px 0;" data-title="右哼哼" data-code=":@&gt;" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/46.gif"></div></td><td><div class="eItem" style="background-position:-1128px 0;" data-title="哈欠" data-code="::-O" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/47.gif"></div></td><td><div class="eItem" style="background-position:-1152px 0;" data-title="鄙视" data-code=":&gt;-|" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/48.gif"></div></td><td><div class="eItem" style="background-position:-1176px 0;" data-title="委屈" data-code=":P-(" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/49.gif"></div></td><td><div class="eItem" style="background-position:-1200px 0;" data-title="快哭了" data-code="::&#39;|" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/50.gif"></div></td><td><div class="eItem" style="background-position:-1224px 0;" data-title="阴险" data-code=":X-)" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/51.gif"></div></td><td><div class="eItem" style="background-position:-1248px 0;" data-title="亲亲" data-code="::*" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/52.gif"></div></td><td><div class="eItem" style="background-position:-1272px 0;" data-title="吓" data-code=":@x" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/53.gif"></div></td><td><div class="eItem" style="background-position:-1296px 0;" data-title="可怜" data-code=":8*" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/54.gif"></div></td><td><div class="eItem" style="background-position:-1320px 0;" data-title="菜刀" data-code=":pd" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/55.gif"></div></td><td><div class="eItem" style="background-position:-1344px 0;" data-title="西瓜" data-code=":&lt;W&gt;" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/56.gif"></div></td><td><div class="eItem" style="background-position:-1368px 0;" data-title="啤酒" data-code=":beer" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/57.gif"></div></td><td><div class="eItem" style="background-position:-1392px 0;" data-title="篮球" data-code=":basketb" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/58.gif"></div></td><td><div class="eItem" style="background-position:-1416px 0;" data-title="乒乓" data-code=":oo" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/59.gif"></div></td></tr><tr><td><div class="eItem" style="background-position:-1440px 0;" data-title="咖啡" data-code=":coffee" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/60.gif"></div></td><td><div class="eItem" style="background-position:-1464px 0;" data-title="饭" data-code=":eat" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/61.gif"></div></td><td><div class="eItem" style="background-position:-1488px 0;" data-title="猪头" data-code=":pig" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/62.gif"></div></td><td><div class="eItem" style="background-position:-1512px 0;" data-title="玫瑰" data-code=":rose" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/63.gif"></div></td><td><div class="eItem" style="background-position:-1536px 0;" data-title="凋谢" data-code=":fade" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/64.gif"></div></td><td><div class="eItem" style="background-position:-1560px 0;" data-title="示爱" data-code=":showlove" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/65.gif"></div></td><td><div class="eItem" style="background-position:-1584px 0;" data-title="爱心" data-code=":heart" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/66.gif"></div></td><td><div class="eItem" style="background-position:-1608px 0;" data-title="心碎" data-code=":break" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/67.gif"></div></td><td><div class="eItem" style="background-position:-1632px 0;" data-title="蛋糕" data-code=":cake" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/68.gif"></div></td><td><div class="eItem" style="background-position:-1656px 0;" data-title="闪电" data-code=":li" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/69.gif"></div></td><td><div class="eItem" style="background-position:-1680px 0;" data-title="炸弹" data-code=":bome" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/70.gif"></div></td><td><div class="eItem" style="background-position:-1704px 0;" data-title="刀" data-code=":kn" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/71.gif"></div></td><td><div class="eItem" style="background-position:-1728px 0;" data-title="足球" data-code=":footb" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/72.gif"></div></td><td><div class="eItem" style="background-position:-1752px 0;" data-title="瓢虫" data-code=":ladybug" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/73.gif"></div></td><td><div class="eItem" style="background-position:-1776px 0;" data-title="便便" data-code=":shit" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/74.gif"></div></td></tr><tr><td><div class="eItem" style="background-position:-1800px 0;" data-title="月亮" data-code=":moon" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/75.gif"></div></td><td><div class="eItem" style="background-position:-1824px 0;" data-title="太阳" data-code=":sun" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/76.gif"></div></td><td><div class="eItem" style="background-position:-1848px 0;" data-title="礼物" data-code=":gift" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/77.gif"></div></td><td><div class="eItem" style="background-position:-1872px 0;" data-title="拥抱" data-code=":hug" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/78.gif"></div></td><td><div class="eItem" style="background-position:-1896px 0;" data-title="强" data-code=":strong" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/79.gif"></div></td><td><div class="eItem" style="background-position:-1920px 0;" data-title="弱" data-code=":weak" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/80.gif"></div></td><td><div class="eItem" style="background-position:-1944px 0;" data-title="握手" data-code=":share" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/81.gif"></div></td><td><div class="eItem" style="background-position:-1968px 0;" data-title="胜利" data-code=":v" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/82.gif"></div></td><td><div class="eItem" style="background-position:-1992px 0;" data-title="抱拳" data-code=":@)" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/83.gif"></div></td><td><div class="eItem" style="background-position:-2016px 0;" data-title="勾引" data-code=":jj" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/84.gif"></div></td><td><div class="eItem" style="background-position:-2040px 0;" data-title="拳头" data-code=":@@" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/85.gif"></div></td><td><div class="eItem" style="background-position:-2064px 0;" data-title="差劲" data-code=":bad" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/86.gif"></div></td><td><div class="eItem" style="background-position:-2088px 0;" data-title="爱你" data-code=":lvu" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/87.gif"></div></td><td><div class="eItem" style="background-position:-2112px 0;" data-title="NO" data-code=":no" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/88.gif"></div></td><td><div class="eItem" style="background-position:-2136px 0;" data-title="OK" data-code=":ok" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/89.gif"></div></td></tr><tr><td><div class="eItem" style="background-position:-2160px 0;" data-title="爱情" data-code=":love" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/90.gif"></div></td><td><div class="eItem" style="background-position:-2184px 0;" data-title="飞吻" data-code=":&lt;L&gt;" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/91.gif"></div></td><td><div class="eItem" style="background-position:-2208px 0;" data-title="跳跳" data-code=":jump" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/92.gif"></div></td><td><div class="eItem" style="background-position:-2232px 0;" data-title="发抖" data-code=":shake" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/93.gif"></div></td><td><div class="eItem" style="background-position:-2256px 0;" data-title="怄火" data-code=":&lt;O&gt;" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/94.gif"></div></td><td><div class="eItem" style="background-position:-2280px 0;" data-title="转圈" data-code=":circle" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/95.gif"></div></td><td><div class="eItem" style="background-position:-2304px 0;" data-title="磕头" data-code=":kotow" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/96.gif"></div></td><td><div class="eItem" style="background-position:-2328px 0;" data-title="回头" data-code=":turn" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/97.gif"></div></td><td><div class="eItem" style="background-position:-2352px 0;" data-title="跳绳" data-code=":skip" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/98.gif"></div></td><td><div class="eItem" style="background-position:-2376px 0;" data-title="挥手" data-code=":oY" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/99.gif"></div></td><td><div class="eItem" style="background-position:-2400px 0;" data-title="激动" data-code=":#-0" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/100.gif"></div></td><td><div class="eItem" style="background-position:-2424px 0;" data-title="街舞" data-code=":hiphot" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/101.gif"></div></td><td><div class="eItem" style="background-position:-2448px 0;" data-title="献吻" data-code=":kiss" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/102.gif"></div></td><td><div class="eItem" style="background-position:-2472px 0;" data-title="左太极" data-code=":&lt;&amp;" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/103.gif"></div></td><td><div class="eItem" style="background-position:-2496px 0;" data-title="右太极" data-code=":&amp;&gt;" data-gifurl="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/104.gif"></div></td></tr></tbody></table><div class="emotionsGif" style=""><img src="http://res.mail.qq.com/zh_CN/images/mo/DEFAULT2/2.gif" alt="mo-色"></div></div>
    </div>
    <script src="../static/wln.js" type="text/javascript"></script>
    <script src="../static/SWFUpload/swfupload.js" type="text/javascript"></script>
    <script src="../static/jquery.jmp3.js" type="text/javascript"></script>
    <script src="../static/ueditor/editor_all.js" type="text/javascript"></script>
    <script src="../static/ueditor/editor_config.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showMsgtypeTips() {
            $('.resconfig').hide();
            var msgtype = $('#msgtype').val();
            if (msgtype == "basic") {
                $('.textmsg').show();
                $('#msgtypetips').html('一问一答得简单对话. 当访客的对话语句中包含指定关键字, 或对话语句完全等于特定关键字, 或符合某些特定的格式时. 系统自动应答设定好的回复内容.');
            } else if (msgtype == "news") {
                $('.newsmsg').show();
                $('#msgtypetips').html('一问一答得简单对话, 但是回复内容包括图片文字等更生动的媒体内容. 当访客的对话语句中包含指定关键字, 或对话语句完全等于特定关键字, 或符合某些特定的格式时. 系统自动应答设定好的图文回复内容.');
            } else if (msgtype == "music") {
                $('.musicmsg').show();
                $('#msgtypetips').html('在回复规则中可选择具有语音、音乐等音频类的回复内容，并根据用户所设置的特定关键字精准的返回给粉丝，实现一问一答得简单对话。');
            } else if (msgtype == "api") {
                $('.apimsg').show();
                $('#msgtypetips').html('通过API接口，接入应用中心的扩展功能或您自己定制的功能。');
            }
        }
        function showModeTips() {
            var msgmode = $('#msgmode').val();
            if (msgmode == "has") {
                $('#msgmodetips').html('用户进行微信交谈时，对话中包含上述关键字就执行这条规则。');
            } else if (msgmode == "equal") {
                $('#msgmodetips').html('用户进行微信交谈时，对话内容完全等于上述关键字才会执行这条规则。');
            } else {

            }
        }
        $('#msgmode').val('<%=msgmode %>');
        $('#msgtype').val('<%=msgtype %>');
        showMsgtypeTips();
        function checkConfig() {
            if($('#keyword').val()==''){
                    alert("请填写关键词");
                    $('#keyword').focus();
                    return false;
            }
            if ($('#msgtype').val() == 'basic') {
                var txt = '';
                $('.txtmsgitem em').each(function () {
                    if ($(this).html().length > 0) {
                        txt = txt + $(this).html() + '#@@@#';
                    }
                });
                if (txt.length > 0) {
                    $('#config').val(txt);
                    return true;
                } else {
                    $('#config').val(txt);
                    return true;
                }
            }else if ($('#msgtype').val() == 'news') {
                var txt = '';
                //txt=txt+$('#newstitlemain').html()+'#@@#';
                //txt=txt+$('#newsdescmain').html()+'#@@#';
                //txt=txt+$('#newspicmain').attr('src')+'#@@#';
                //txt=txt+$('#newspicmain').attr('link')+'#@@#';
                //txt=txt+$('#newscontentmain').html()+'#@@#';
                $('.newsli').each(function () {
                    var img=$(this).find('img').first();
                    var title=$(this).find('h4').first();
                    if(img&&$(title).html().length > 0){
                        if (txt.length > 0) {
                            txt = txt + '#@@@#';
                        }
                        txt=txt+$(title).html()+'#@@#';
                        txt=txt+$(title).next().html()+'#@@#';
                        txt=txt+$(img).attr('src')+'#@@#';
                        txt=txt+$(img).attr('link')+'#@@#';
                        txt=txt+$(title).next().next().html()+'#@@#';
                    }
                });
                $('#config').val(txt);
                return true;
            }else if($('#msgtype').val() == 'music'){            
                if($('#musictitle').val()==''){
                    alert("请填写音乐标题");
                    $('#musictitle').focus();
                    return false;
                }else if($('#musicurl').val()==''){
                    alert("请指定音乐连接地址");
                    $('#musicurl').focus();
                    return false;
                }
                var music='';
                music=music+ "musictitle#@@#"+$('#musictitle').val()+"#@@@#";
                music=music+ "musicurl#@@#"+$('#musicurl').val()+"#@@@#";
                music=music+ "musichdurl#@@#"+$('#musichdurl').val()+"#@@@#";
                music=music+ "musicdesc#@@#"+$('#musicdesc').val();
                $('#config').val(music);
                return true;
            }else if($('#msgtype').val() == 'api'){            
                if($('#apiurl').val()==''){
                    alert("请填写API的地址");
                    $('#apiurl').focus();
                    return false;
                }
                var api= 'Api:' + $('#apiurl').val();
                $('#config').val(api);
                return true;
            }
            return false;
        }
        var editTextInput;
        function addText() {
            $('#textmsg').val('');
            $('#txtModal').modal();
            editTextInput = null;
        }
        function txtEdit(e) {
            editTextInput = $(e).parent().next();
            var txt = $(e).parent().next().html();
            $('#textmsg').val(html_decode(txt));
            $('#txtModal').modal();
            //$(e).parent().parent().hide();
        }
        function txtDel(e) {
            if (confirm("删除操作不可恢复，确认删除吗？")) {
                $(e).parent().parent().remove();
                editTextInput = null;
            }
        }
        function txtSave() {
            if (editTextInput) {
                $(editTextInput).html($('#textmsg').val().replace(/\n/g,'<br>'));
            } else {
                txtItemAdd($('#textmsg').val().replace(/\n/g,'<br>'));
                $('#textmsg').val('');
            }
            $('#txtModal').modal('hide');
            return false;
        }
        function txtItemAdd(txt) {
            $('#txtmsglist').append('<div class="txtmsgitem"><span><a onclick="txtEdit(this);">编辑</a> <a onclick="txtDel(this);">删除</a></span><em>' + txt + '</em></div>');
        }
        function apiItemSet(url) {
            $('#apiurl').val(url);
        }
        window.UEDITOR_CONFIG.initialFrameHeight = 210;
        var ueListContent = UE.getEditor('ListContent');
        var editMain=true;
        function newsCancel(){
            $('#newsModal').hide();
            $('#newsPlane').show();
            return false;
        }
        function newsEdit(e) {
            $('#newsModal').show();
            $('#newsPlane').hide();
            editMain=true;
            $('#newstitle').val(html_decode($('#newstitlemain').html()));
            $('#newsdesc').val(html_decode($('#newsdescmain').html()));
            $('#newsurl').val($('#newspicmain').attr('link'));
            $('#imgPicUrl').attr('src',$('#newspicmain').attr('src'));
            
            ueListContent.setContent( html_decode($('#newscontentmain').html()));
            //ueListContent.setContent($('#newscontentmain').html());
        }
        var editNewsTitle;
        var editNewsDesc;
        var editNewsPic;
        var editNewsContent;
        function newsEditItem(e) {
            $('#newsModal').show();
            $('#newsPlane').hide();
            editMain=false;
            editNewsTitle=$(e).parent().parent().find('h4').first();
            editNewsDesc=editNewsTitle.next();
            editNewsContent=editNewsDesc.next();
            editNewsPic=$(e).parent().parent().parent().find('img').first();


            $('#newstitle').val(html_decode($(editNewsTitle).html()));
            $('#newsdesc').val(html_decode($(editNewsDesc).html()));
            $('#newsurl').val($(editNewsPic).attr('link'));
            $('#imgPicUrl').attr('src',$(editNewsPic).attr('src'));
            
            ueListContent.setContent( html_decode($(editNewsContent).html()));
            //ueListContent.setContent($(editNewsContent).html());
        }
        function newDel(e) {
            if (confirm("删除操作不可恢复，确认删除吗？")) {
                $(e).parent().parent().parent().remove();
                editMusicInput = null;
            }
        }
        function newsMainSet(title,desc,pic,link,content) {
                $('#newstitlemain').html(title);
                $('#newsdescmain').html(desc);
                if(pic&&pic.substring(0,7)!='http://'){
                    pic="<%=_dataurl %>" +pic;
                }
                if(pic!=''){
                    $('#newspicmain').attr('src',pic);
                    $('#newspicmainnull').hide();
                }
                $('#newspicmain').attr('link',link);
                //$('#newscontentmain').html(html_encode(content));
                $('#newscontentmain').html(content);
//                $('#newsModal').modal('hide');
                $('#newsModal').hide();
                $('#newsPlane').show();
            return false;
        }
        function newsItemAdd(title,desc,pic,link,content) {
            if(pic&&pic.length>0){
                if(pic.substring(0,7)!='http://'){
                    pic="<%=_dataurl %>" +pic;
                }
                $('#newsitemlist').append('<div class="newsli"><div class="newslipic"><p style="display:none;">缩略图</p><img src="'+pic+'" link="'+link+'"></div><div class="newslitxt"><h4>'+title+'</h4><div>'+desc+'</div><div>'+content+'</div><div style="display:block;"><a href="javascript:;" onclick="newsEditItem(this);">编辑</a>|<a href="javascript:;" onclick="newDel(this);">删除</a></div></div><div class="clearfix">&nbsp;</div></div>');
            }else{
                $('#newsitemlist').append('<div class="newsli"><div class="newslipic"><p>缩略图</p><img src="'+pic+'" link="'+link+'"></div><div class="newslitxt"><h4>'+title+'</h4><div>'+desc+'</div><div>'+content+'</div><div style="display:block;"><a href="javascript:;" onclick="newsEditItem(this);">编辑</a>|<a href="javascript:;" onclick="newDel(this);">删除</a></div></div><div class="clearfix">&nbsp;</div></div>');
            }
            return false;
        }
        function newsSave(){
            if($('#newstitle').val()==''){
                alert('标题不能为空');$('#newstitle').focus(); return false;
            }
            if(editMain){
                $('#newstitlemain').html($('#newstitle').val());
                if($('#imgPicUrl').attr('src')!=''){
                    $('#newspicmain').attr('src',$('#imgPicUrl').attr('src'));
                    $('#newspicmainnull').hide();
                }
                $('#newspicmain').attr('link',$('#newsurl').val());
                $('#newsdescmain').html($('#newsdesc').val());
                $('#newscontentmain').html(html_encode(ueListContent.getContent()));
                //$('#newscontentmain').html(ueListContent.getContent());
//                $('#newsModal').modal('hide');
                $('#newsModal').hide();
                $('#newsPlane').show();
            }else{
                $(editNewsTitle).html($('#newstitle').val());
                if($('#imgPicUrl').attr('src')!=''){
                    $(editNewsPic).attr('src',$('#imgPicUrl').attr('src'));
                    $(editNewsPic).prev().hide();
                }
                $(editNewsPic).attr('link',$('#newsurl').val());
                $(editNewsDesc).html($('#newsdesc').val());
                $(editNewsContent).html(html_encode(ueListContent.getContent()));
                //$(editNewsContent).html(ueListContent.getContent());
//                $('#newsModal').modal('hide');
                $('#newsModal').hide();
                $('#newsPlane').show();
            }
            return false;
        }
        function initMusic(musictitle,musicurl,musichdurl,musicdesc){
            $('#musictitle').val(musictitle);
            $('#musicurl').val(musicurl);
            $('#musichdurl').val(musichdurl);
            $('#musicdesc').val(musicdesc);
            $("#mp3MusicUrl").html('<span class="mp3">' + musicurl + '</span>');
            $(".mp3").jmp3();
        }
        wln.wlnUpload('swfUploadPic', wln.path + '../../upload.aspx?account=<%=_account %>&filetype=pic', uploadProgressUpload, uploadSuccessPic);
        wln.wlnUpload('swfUploadMusic', wln.path + '../../upload.aspx?account=<%=_account %>&filetype=audio', uploadProgressUpload, uploadSuccessMusic);
        function uploadProgressUpload() {
        }
        function uploadSuccessPic(fileobj, serverData) {
            var stringArray = serverData.split("|");
            if (stringArray[0] == "1") {
                $('#imgPicUrl').attr('src', "<%=_dataurl %>/" +stringArray[1]).show();
            }
            else {
                alert(stringArray[2]);
            }
        }
        function uploadSuccessMusic(fileobj, serverData) {
            var stringArray = serverData.split("|");
            if (stringArray[0] == "1") {
                $("#musicurl").val("/" +stringArray[1]);
                $("#mp3MusicUrl").html('<span class="mp3">' + "<%=_dataurl %>/" +stringArray[1] + '</span>');
                $(".mp3").jmp3();
            }
            else {
                alert(stringArray[2]);
            }
        }
        <%=_script %>
        <%=_scriptint %>
    </script>
</body>
</html>
