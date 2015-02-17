using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class ChartData : PageLogin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime temp = DateTime.Now.AddDays(-7);
                string account = GetAccountGuid();
                int xid = 0;
                System.Text.StringBuilder series = new System.Text.StringBuilder();
                System.Text.StringBuilder graph1 = new System.Text.StringBuilder();
                System.Text.StringBuilder graph2 = new System.Text.StringBuilder();
                series.Append("\n<series>");
                graph1.Append("\n<graph gid=\"1\">");
                graph2.Append("\n<graph gid=\"2\">");
                while (temp < DateTime.Now)
                {
                    try
                    {
                        series.AppendFormat("\n<value xid=\"{0}\">{1}</value>", xid, temp.ToString("M月d日"));

                        var ints = Wlniao.ServiceWeixin.GetCount(account, temp.ToString("yyMMdd"));
                        graph1.AppendFormat("\n<value xid=\"{0}\">{1}</value>", xid, ints[0]);
                        graph2.AppendFormat("\n<value xid=\"{0}\">{1}</value>", xid, ints[2]);
                    }
                    catch
                    {
                    }
                    temp = temp.AddDays(1);
                    xid++;
                }
                series.Append("\n</series>");
                graph1.Append("\n</graph>");
                graph2.Append("\n</graph>");

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("\n<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                sb.Append("\n<chart>");
                sb.Append(series.ToString());
                sb.Append("\n<graphs>");
                sb.Append(graph1.ToString());
                sb.Append(graph2.ToString());
                sb.Append("\n</graphs>");
                sb.Append("\n</chart>");

                Response.Clear();
                Response.Write(sb.ToString());
                Response.End();
            }
        }
    }
}