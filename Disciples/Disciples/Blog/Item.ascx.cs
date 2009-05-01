using System;
using System.Web;
using DiscData;
using System.Web.Security;
using System.Linq;
using System.Collections.Generic;

public partial class Blog_Item : System.Web.UI.UserControl
{
    private BlogPost post;
    public BlogPost BlogPost
    {
        get { return post; }
    }
    public bool SingleItem { get; set; }
    public string PostId
    {
        set
        {
            post = BlogPost.LoadFromId(value.ToInt());
        }
    }

    public string PermaLink;
    public string BlogLink;
    public string BlogLink0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (post == null)
            return;
        PermaLink = "/Blog/{0}.aspx".Fmt(post.Id);
        BlogLink0 = "/Blog/{0}".Fmt(post.BlogCached.Name);
        BlogLink = BlogLink0 + ".aspx";
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (post == null || (!post.BlogCached.IsMember && !post.BlogCached.IsPublic))
            Response.Redirect("/");

        Edit.NavigateUrl = "/Blog/Edit.aspx?id=" + post.Id;
        Edit.Visible = BlogPost.BlogCached.IsBlogger || Roles.IsUserInRole("Administrator");
        CategoryList.DataSource = BlogCategoryController.GetCategoryListFromCache(post.Id);
        CategoryList.DataBind();
    }
}