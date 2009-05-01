﻿/* Author: David Carroll
 * Copyright (c) 2008, 2009 Bellevue Baptist Church 
 * Licensed under the GNU General Public License (GPL v2)
 * you may not use this code except in compliance with the License.
 * You may obtain a copy of the License at http://bvcms.codeplex.com/license 
 */
using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using UtilityExtensions;
using CMSPresenter;
using CmsData;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace CMSWeb.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pager1.PageSize = Util.GetPageSizeCookie();
            pager2.PageSize = Util.GetPageSizeCookie();
        }

        protected void ListView1_ItemCreated(object sender, ListViewItemEventArgs e)
        {
                //var r = e.Item as ListViewDataItem;
                //var d = r.DataItem as User;
                //if ((selectedId.HasValue && d.Id == selectedId.Value) || r.DisplayIndex == TaskList.SelectedIndex)
        }

        protected void ButtonCreateNewRole_Click(object sender, EventArgs e)
        {
            if (TextBoxCreateNewRole.Text.HasValue())
            {
                Roles.CreateRole(TextBoxCreateNewRole.Text);
                TextBoxCreateNewRole.Text = "";
            }
        }

        protected void ObjectDataSourceMembershipUser_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                CheckNewUser.IsValid = false;
                LabelInsertMessage.Text = e.Exception.InnerException.Message + " Insert Failed";
                LabelInsertMessage.ForeColor = System.Drawing.Color.Red;
                e.ExceptionHandled = true;
            }
            else
            {
                ListView1.Sort("CreationDate", SortDirection.Descending);
                TextBox1.Text = string.Empty;
                ListView1.DataBind();
                ListView1.SelectedIndex = 0;
            }
        }

        public void SearchForUsers(object sender, EventArgs e)
        {
            ListView1.DataBind();
        }

        protected void CheckNewUser_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Deselect")
                ListView1.SelectedIndex = -1;
        }

        protected void ListView1_Sorting(object sender, ListViewSortEventArgs e)
        {
            ListView1.SelectedIndex = -1;
        }

        protected void ListView1_ItemDeleted(object sender, ListViewDeletedEventArgs e)
        {
			ListView1.SelectedIndex = -1;
        }

        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            ListView1.SelectedIndex = -1;
        }

        protected void RolesCheckBoxList_DataBound(object sender, EventArgs e)
        {
            var cbl = sender as CheckBoxList;
            var di = cbl.Parent as ListViewDataItem;
            var u = di.DataItem as User;
            var roles = u.Roles;
            foreach (ListItem li in cbl.Items)
                li.Selected = roles.Contains(li.Text);
        }

        protected void RolesCheckBoxList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbl = sender as CheckBoxList;
            var di = cbl.Parent as ListViewDataItem;
            var UserId = ListView1.DataKeys[di.DisplayIndex].Value.ToInt();
            var user = DbUtil.Db.Users.Single(u => u.UserId == UserId);
            var checkedRoles = new List<string>();
            foreach (ListItem li in cbl.Items)
                if (li.Selected)
                    checkedRoles.Add(li.Text);
            user.Roles = checkedRoles.ToArray();
            DbUtil.Db.SubmitChanges();
        }
        protected void AddSelectedPerson_Click(object sender, EventArgs e)
        {
            var p = SearchDialog.SelectedPeople().First();
            var UserId = ListView1.SelectedValue.ToInt();
            var user = DbUtil.Db.Users.Single(u => u.UserId == UserId);
            var lvi = ListView1.Items[ListView1.SelectedIndex] as ListViewItem;
            user.PeopleId = p.PeopleId;
            DbUtil.Db.SubmitChanges();
        }
    }
}