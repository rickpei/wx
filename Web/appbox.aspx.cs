using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class appbox : PageLogin
    {
        protected static string _clientid = cfgHelper.GetAppSettings("AppClientId");
        protected static string _sercet = cfgHelper.GetAppSettings("AppSercet");
        protected static string _sercetcheck = "no";
        protected string _userid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["iheight"]))
            {
                Response.Clear();
                Response.Write("<html>\n");
                Response.Write("<head>\n");
                Response.Write("<script type=\"text/javascript\">\n");
                Response.Write("    function pseth() {\n");
                Response.Write("        var iObj;\n");
                Response.Write("        try{\n");
                Response.Write("        iObj = parent.parent.parent.document.getElementById('appFramePage'); //A和main同域，所以可以访问节点\n");
                Response.Write("        }catch(e){}\n");
                Response.Write("        if(!iObj){\n");
                Response.Write("        iObj = parent.parent.document.getElementById('appFramePage'); //A和main同域，所以可以访问节点\n");
                Response.Write("        }\n");
                Response.Write("        iObj.style.height = \"" + Request["iheight"] + "px\"; //操作dom\n");
                Response.Write("    }\n");
                Response.Write("    pseth();\n");
                Response.Write("</script>\n");
                Response.Write("</head>\n");
                Response.Write("<body></body>\n");
                Response.Write("<html>");
                Response.End();
            }
            else
            {
                if (string.IsNullOrEmpty(_sercet))
                {
                    _userid = GetAccountGuid();
                }
                else
                {
                    _sercetcheck = "yes";
                    _userid = Encryptor.DesEncrypt(GetAccountGuid(), _sercet);
                }
            }
        }
    }
}