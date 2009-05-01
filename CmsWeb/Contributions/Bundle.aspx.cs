﻿/* Author: David Carroll
 * Copyright (c) 2008, 2009 Bellevue Baptist Church 
 * Licensed under the GNU General Public License (GPL v2)
 * you may not use this code except in compliance with the License.
 * You may obtain a copy of the License at http://bvcms.codeplex.com/license 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsData;
using UtilityExtensions;
using CMSPresenter;

namespace CMSWeb.Contributions
{
    public partial class Bundle : System.Web.UI.Page
    {
        public BundleHeader bundleheader;
        DateTime? postingdt;
        protected void Page_Load(object sender, EventArgs e)
        {
            postingdt = this.QueryString<DateTime?>("dt");
            if (!postingdt.HasValue)
                postingdt = Util.Now;
            var id = this.QueryString<int?>("Id");
            bundleheader = DbUtil.Db.BundleHeaders.SingleOrDefault(b => b.BundleHeaderId == id);
            if (bundleheader == null)
                Response.EndShowMessage("no bundle");
            Delete.Enabled = bundleheader.BundleStatusId == (int)BundleHeader.StatusCode.Open;
            EditUpdateButton1.Enabled = Delete.Enabled;
            if (User.IsInRole("Admin"))
                EditUpdateButton1.Enabled = true;
            if (Delete.Enabled && !Page.IsPostBack && this.QueryString<int?>("edit").HasValue)
                EditUpdateButton1.Editing = true;

            ReqTotalCash.Enabled = EditUpdateButton1.Editing;
            ReqTotalChecks.Enabled = EditUpdateButton1.Editing;
            ReqTotalEnv.Enabled = EditUpdateButton1.Editing;

            EditUpdateButton1.DataBind();
            BundleId.Text = bundleheader.BundleHeaderId.ToString();

            var ctl = new BundleController();
            TotalItems.Text = ctl.TotalItems(bundleheader.BundleHeaderId).ToString("c");
            TotalHeader.Text = ctl.TotalHeader(bundleheader.BundleHeaderId).ToString("c");
        }

        protected void EditUpdateButton1_Click(object sender, EventArgs e)
        {
            if (EditUpdateButton1.Updating)
            {
                if (bundleheader.BundleStatusId == (int)BundleHeader.StatusCode.Closed)
                {
                    foreach (var d in bundleheader.BundleDetails)
                        d.Contribution.PostingDate = postingdt;
                }
                DbUtil.Db.SubmitChanges();
                EditUpdateButton1.DataBind();
            }
        }

        protected void NewBundle_Click(object sender, EventArgs e)
        {
            var ctl = new BundleController();
            var b = ctl.NewBundle();
            Response.Redirect("~/Contributions/Bundle.aspx?id={0}&edit=1".Fmt(b.BundleHeaderId));
        }

        protected void ListView1_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var r = e.Item as ListViewDataItem;
                var d = r.DataItem as ContributionInfo;
                if (d != null)
                {
                    var ctl = e.Item.FindControl("StatusLabel") as Label;
                    if (d.NotIncluded)
                        ctl.Style.Add("color", "Red");
                }
            }
        }

        protected void Delete_Click(object sender, ImageClickEventArgs e)
        {
            var q = from d in bundleheader.BundleDetails
                    select d.Contribution;
            DbUtil.Db.Contributions.DeleteAllOnSubmit(q);
            DbUtil.Db.BundleDetails.DeleteAllOnSubmit(bundleheader.BundleDetails);
            DbUtil.Db.BundleHeaders.DeleteOnSubmit(bundleheader);

            DbUtil.Db.SubmitChanges();
            Response.Redirect("~/Contributions/Bundles.aspx");
        }
    }
}