﻿/* Author: David Carroll
 * Copyright (c) 2008, 2009 Bellevue Baptist Church 
 * Licensed under the GNU General Public License (GPL v2)
 * you may not use this code except in compliance with the License.
 * You may obtain a copy of the License at http://bvcms.codeplex.com/license 
 */
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using UtilityExtensions;
using CMSPresenter;
using System.Web.Configuration;
using CmsData;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Data.Linq;

namespace CMSWeb
{
    public partial class OrganizationSearch : System.Web.UI.Page
    {
        class OrgSearchInfo
        {
            public string Name { get; set; }
            public string OrgDiv { get; set; }
            public string Sched { get; set; }
            public string Status { get; set; }
        }
        private const string STR_OrgSearch = "OrgSearch";

        protected void Page_Load(object sender, EventArgs e)
        {
            var site = (CMSWeb.Site)Page.Master;
            site.ScriptManager.EnablePageMethods = true;
            if (!IsPostBack)
            {
                Status.SelectedValue = "30"; // 30=Active
                if (Session[STR_OrgSearch].IsNotNull())
                {
                    var os = Session[STR_OrgSearch] as OrgSearchInfo;
                    NameSearch.Text = os.Name;
                    OrgDivisions.SelectedValue = os.OrgDiv;
                    SetCreateMeetingDefaults(os.Sched.ToInt());
                    Schedule.SelectedValue = os.Sched;
                    Status.SelectedValue = os.Status;
                    OrganizationGrid.Visible = true;
                }
                GridPager.SetPageSize(OrganizationGrid);
                NameSearch.Focus();
            }
            else
                SetCreateMeetingDefaults(Schedule.SelectedValue.ToInt());

            ManageOrgTags.Visible = User.IsInRole("OrgTagger");
            var col = OrganizationGrid.Columns[OrganizationGrid.Columns.Count - 1];
            col.Visible = ManageOrgTags.Visible;
            RollsheetRpt.Enabled = User.IsInRole("Attendance");
            MeetingsLink.NavigateUrl = "~/Meetings.aspx?progid={0}&divid={1}&schedid={2}&name={3}"
                .Fmt(0, OrgDivisions.SelectedValue, Schedule.SelectedValue.ToInt(), Server.UrlEncode(NameSearch.Text));
            MeetingsLink.Visible = RollsheetRpt.Visible;
        }

        private void SetCreateMeetingDefaults(int id)
        {
            if (id == 0)
            {
                MeetingDate.Text = Util.Now.Date.ToShortDateString();
                MeetingTime.Text = "8:00 AM";
            }
            else
            {
                var schedule = DbUtil.Db.WeeklySchedules.Single(s => s.Id == id);
                var dt = Util.Now.Date;
                dt = dt.AddDays(-(int)dt.DayOfWeek); // prev sunday
                dt = dt.AddDays((int)schedule.Day);
                if (dt < Util.Now.Date)
                    dt = dt.AddDays(7);
                MeetingDate.Text = dt.ToShortDateString();
                MeetingTime.Text = schedule.MeetingTime.ToShortTimeString();
            }
        }

        private void SaveToSession()
        {
            Session[STR_OrgSearch] = new OrgSearchInfo
            {
                Name = NameSearch.Text,
                OrgDiv = OrgDivisions.SelectedValue,
                Sched = Schedule.SelectedValue,
                Status = Status.SelectedValue,
            };
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (NameSearch.Text.StartsWith("M."))
                CreateMeeting(NameSearch.Text);
            OrganizationGrid.Visible = true;
            SaveToSession();
        }

        protected void ExportExcel_Click(object sender, EventArgs e)
        {
            var ctl = new OrganizationSearchController();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=CMSOrganizations.xls");
            Response.Charset = "";
            this.EnableViewState = false;
            var d = ctl.FetchOrganizationExcelList(NameSearch.Text,
                OrgDivisions.SelectedValue.ToInt(),
                Schedule.SelectedValue.ToInt(),
                Status.SelectedValue.ToInt());
            var dg = new DataGrid();
            dg.DataSource = d;
            dg.DataBind();
            dg.RenderControl(new HtmlTextWriter(Response.Output));
            Response.End();
        }

        protected void OrganizationData_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is int)
                GridCount.Text = e.ReturnValue.ToString();
        }

        protected void NewSearch_Click(object sender, EventArgs e)
        {
            Session.Remove(STR_OrgSearch);
            Response.Redirect("~/OrganizationSearch.aspx");
        }

        protected void MakeNewTag_Click(object sender, EventArgs e)
        {
            var Db = DbUtil.Db;
            var div = Db.Divisions.SingleOrDefault(d => d.Name == TagName.Text);

            var a = Tags.SelectedValue.SplitStr(":");
            var progid = a[0].ToInt();
            var divid = a[1].ToInt();

            if (div == null) // must be a new tag
            {
                div = new Division { Name = TagName.Text, ProgId = progid};
                if (progid == 0) // if a parent div was not specified then use misc program
                {
                    var mp = Db.Programs.SingleOrDefault(t => t.Name == DbUtil.MiscTagsString);
                    if (mp == null) // is there not an existing misc program?
                    {
                        mp = new Program { Name = DbUtil.MiscTagsString };
                        Db.Programs.InsertOnSubmit(mp);
                    }
                    div.Program = mp; // set the parent tag
                }
                Db.Divisions.InsertOnSubmit(div);
                Db.SubmitChanges();
                OrgDivisions.DataBind();
                Tags.DataBind();
            }
        }

        protected void RenameTag_Click(object sender, EventArgs e)
        {
            var a = Tags.SelectedValue.SplitStr(":");
            var progid = a[0].ToInt();
            var divid = a[1].ToInt();
            
            var div = DbUtil.Db.Divisions.Single(d => d.Id == divid);
            div.Name = TagName.Text;
            DbUtil.Db.SubmitChanges();
            OrgDivisions.DataBind();
            Tags.DataBind();
        }

        protected void DeleteTag_Click(object sender, EventArgs e)
        {
            var a = Tags.SelectedValue.SplitStr(":");
            var progid = a[0].ToInt();
            var divid = a[1].ToInt();

            var div = DbUtil.Db.Divisions.Single(d => d.Id == divid);
            DbUtil.Db.DivOrgs.DeleteAllOnSubmit(div.DivOrgs);
            DbUtil.Db.BFCSummaryOrgTags.DeleteAllOnSubmit(div.BFCSummaryOrgTags);
            DbUtil.Db.Divisions.DeleteOnSubmit(div);
            DbUtil.Db.SubmitChanges();
            OrgDivisions.DataBind();
            Tags.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string ToggleTag(int OrganizationId, int tagid, string controlid)
        {
            if (tagid == 0)
                return "";
            var Db = DbUtil.Db;
            var organization = Db.Organizations.SingleOrDefault(o => o.OrganizationId == OrganizationId);
            var r = new ToggleTagReturn { ControlId = controlid };
            r.HasTag = organization.ToggleTag(tagid);
            Db.SubmitChanges();
            var jss = new DataContractJsonSerializer(typeof(ToggleTagReturn));
            var ms = new MemoryStream();
            jss.WriteObject(ms, r);
            return Encoding.Default.GetString(ms.ToArray());
        }

        protected void OrganizationGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var b = e.Row.FindControl("TagUntag") as LinkButton;
                var d = e.Row.DataItem as OrganizationInfo;
                b.Enabled = User.IsInRole("Edit");
                if (b.Enabled)
                    b.OnClientClick = "PageMethods.ToggleTag({0},{1},this.id,ToggleTagCallback); return false;"
                        .Fmt(d.OrganizationId, OrganizationSearchController.TagSubDiv(Tags.SelectedValue));
                //if (e.Row.RowIndex == 0)
                //{
                //    var organization = DbUtil.Db.Organizations.Single(a => a.OrganizationId == d.OrganizationId);
                //    if (organization.ScheduleId != null)
                //    {
                //        var dt = Util.Now.Date;
                //        dt = dt.AddDays(-(int)dt.DayOfWeek); // prev sunday
                //        dt = dt.AddDays((int)organization.WeeklySchedule.Day);
                //        if (dt < Util.Now.Date)
                //            dt = dt.AddDays(7);
                //        MeetingDate.Text = dt.ToShortDateString();
                //        MeetingTime.Text = organization.WeeklySchedule.MeetingTime.ToShortTimeString();
                //    }
                //    else

                //        MeetingDate.Text = Util.Now.Date.ToShortDateString();
                //    MeetingTime.Text = "8:00 AM";
                //}
            }
        }

        protected void Tags_SelectedIndexChanged(object sender, EventArgs e)
        {
			var a = Tags.SelectedValue.SplitStr(":");
			var progid = a[0].ToInt();
			var divid = a[1].ToInt();
			progdivid.Text = "prog: {0}, div: {1}".Fmt(progid, divid);
			OrganizationGrid.DataBind();
        }
        private void CreateMeeting(string meetingcode)
        {
            var a = meetingcode.SplitStr(".");
            var orgid = a[1].ToInt();
            var organization = DbUtil.Db.LoadOrganizationById(orgid);
            if (organization == null)
                return;
            var dt = new DateTime(
                a[2].Substring(4, 2).ToInt() + 2000,
                a[2].Substring(0, 2).ToInt(),
                a[2].Substring(2, 2).ToInt(),
                a[2].Substring(6, 2).ToInt(),
                a[2].Substring(8, 2).ToInt(),
                0);
            var newMtg = DbUtil.Db.Meetings.SingleOrDefault(m => m.OrganizationId == orgid && m.MeetingDate == dt);
            if (newMtg == null)
            {
                newMtg = new CmsData.Meeting
                {
                    CreatedDate = Util.Now,
                    CreatedBy = DbUtil.Db.CurrentUser.UserId,
                    OrganizationId = orgid,
                    GroupMeetingFlag = false,
                    Location = organization.Location,
                    MeetingDate = dt,
                };
                DbUtil.Db.Meetings.InsertOnSubmit(newMtg);
                DbUtil.Db.SubmitChanges();
                DbUtil.LogActivity("Creating new meeting for {0}".Fmt(dt));
           }
            Response.Redirect("~/Meeting.aspx?edit=1&id=" + newMtg.MeetingId);
        }
    }
}