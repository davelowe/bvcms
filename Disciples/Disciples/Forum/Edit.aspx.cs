using System;
using DiscData;

public partial class Forum_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int? id = Request.QueryString<int?>("id");
        if (!id.HasValue)
            Response.Redirect("/");
        var entry = ForumEntry.LoadById(id.Value);

        if (Request.QueryString<bool>("reply"))
            EditForumEntry1.Reply = entry;
        else
            EditForumEntry1.Entry = entry;

        EditForumEntry1.CancelUrl = "/Forum/Thread/{0}.aspx?selected={1}"
            .Fmt(entry.ThreadId, entry.Id);

        ((BellevueTeachers.Site)Master).AddCrumb("Topics", "/Forum/{0}.aspx", entry.ForumId)
          .Add("Thread", EditForumEntry1.CancelUrl);
    }
}