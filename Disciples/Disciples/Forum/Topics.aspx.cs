using System;
using DiscData;
using System.Web;

public partial class Forum_Topics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int? forumid = Request.QueryString<int?>("forumid");
        if (!forumid.HasValue)
            Response.Redirect("/");
        var f = Forum.LoadFromId(forumid.Value);
        if (f == null || !f.IsMember)
            Response.Redirect("/");
        AddEntry.NavigateUrl = "/Forum/NewEntry.aspx?id=" + forumid.Value;
        ((BellevueTeachers.Site)Master).AddCrumb("Topics", "/Forum/{0}.aspx", forumid);
    }
}