﻿/* Author: David Carroll
 * Copyright (c) 2008, 2009 Bellevue Baptist Church 
 * Licensed under the GNU General Public License (GPL v2)
 * you may not use this code except in compliance with the License.
 * You may obtain a copy of the License at http://bvcms.codeplex.com/license 
 */
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text;
using UtilityExtensions;
using CMSPresenter;
using CmsData;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections.Generic;
using System.Web;

namespace CMSWeb
{
    public partial class AddFamilyMembersDialog : System.Web.UI.Page
    {
        public int FamilyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            FamilyId = Request.QueryString["id"].ToInt();
            if (FamilyId == 0)
                throw new Exception("Cannot visit AddFamilyMembersDialog this way");
            if (!IsPostBack)
                PersonSearchDialogController.ResetSearchTags();
            Parameters.SearchButtonClicked += new EventHandler(SearchButton_Click);
            Parameters.ClearButtonClicked += new EventHandler(Parameters_ClearButtonClicked);
            AddNew1.Enabled = User.IsInRole("Edit");
        }

        void Parameters_ClearButtonClicked(object sender, EventArgs e)
        {
            ListView1.Visible = false;
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ListView1.Visible = true;
            var ctl = new PersonSearchDialogController();
            ListView1.DataSource = ctl.FetchSearchList(Parameters, false);
            ListView1.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string ToggleTag(int PeopleId, string controlid)
        {
            return SearchDialog.ToggleTag(PeopleId, controlid);
        }
        protected void AddSelectedFamilyMembers_Click(object sender, EventArgs e)
        {
            var fids = new List<int>();
            var f = DbUtil.Db.Families.Single(fam => fam.FamilyId == FamilyId);
            foreach (var p in SearchDialog.SelectedPeople())
            {
                if (f.People.Count(per => per.PositionInFamilyId == 10) < 2)
                    p.PositionInFamilyId = 10;
                else
                    p.PositionInFamilyId = 20;
                fids.Add(p.FamilyId);
                p.FamilyId = FamilyId;
            }
            DbUtil.Db.SubmitChanges();

            foreach (var fid in fids)
                if (DbUtil.Db.People.Count(p => p.FamilyId == fid) == 0)
                {
                    f = DbUtil.Db.Families.SingleOrDefault(fam => fam.FamilyId == fid);
                    if (f != null)
                    {
                        DbUtil.Db.Families.DeleteOnSubmit(f);
                        DbUtil.Db.SubmitChanges();
                    }
                }
            this.Page.ClientScript.RegisterStartupScript(typeof(AddContacteeDialog),
                "closeThickBox", "self.parent.AddSelected();", true);
        }
        protected void AddNew1_Click(object sender, EventArgs e)
        {
            if (!Parameters.Name.HasValue())
                CustomValidator1.IsValid = false;
            else
            {
                CustomValidator1.IsValid = PersonSearchDialogController
                    .AddNewPerson(Parameters.Name,
                                   Parameters.DOB,
                                   FamilyId,
                                   Parameters.Gender,
                                   (int)Person.OriginCode.NewFamilyMember,
                                   null);
            }

            if (!CustomValidator1.IsValid)
                return;
            var ctl = new PersonSearchDialogController();
            ListView1.DataSource = ctl.FetchSearchList(Parameters, false);
            ListView1.DataBind();
        }
    }
}