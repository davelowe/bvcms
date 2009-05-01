﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsData;
using UtilityExtensions;
using System.Text;
using System.Data.Linq.SqlClient;

namespace CMSWeb.Admin
{
    public partial class LastActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var dt = DateTime.Now;
            var q = from u in DbUtil.Db.Users
                    orderby u.LastActivityDate descending
                    select new { u.Person.Name, u.LastActivityDate, u.Host, u.UserId };
            var sb = new StringBuilder();

            sb.AppendFormat("<tr><td>&nbsp;</td><td colspan=\"2\">{0}</td></tr>".Fmt(DateTime.Now));
            var n = 0;
            foreach(var i in q)
            {
                n += 1;
                if (n < 40 || dt.Subtract(i.LastActivityDate.Value).TotalHours <= 24)
                    sb.AppendFormat("<tr><td><a href='{3}'>{0}</a></td><td>{1}</td><td>{2}</td></tr>",
                        i.Name, i.LastActivityDate, i.Host,
                        ResolveUrl("~/Admin/Activity.aspx?uid={0}".Fmt(i.UserId)));
            }
            Label1.Text = "<a href=\"/\">home</a><table>" + sb.ToString() + "</table>";
        }

        protected void disable_Click(object sender, EventArgs e)
        {
            System.IO.File.Move(Server.MapPath("~/App_Offline1.htm"), Server.MapPath("~/App_Offline.htm"));
            Response.Redirect("~/");
        }
    }
}