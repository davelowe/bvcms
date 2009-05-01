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
using CMSPresenter;
using UtilityExtensions;
using CmsData;

namespace CMSWeb
{
    public partial class EnrollmentControlReport : System.Web.UI.Page
    {
        private CodeValueController CVController = new CodeValueController();
        protected void Page_Load(object sender, EventArgs e)
        {
            Run.Enabled = User.IsInRole("Attendance");
            if (!IsPostBack)
            {
                DivOrg.DataSource = CVController.OrgDivTags();
                DivOrg.DataBind();
            }
        }
        protected void DivOrg_SelectedIndexChanged(object o, EventArgs e)
        {
            DivOrg.Items.FindByValue("0").Enabled = false;
            SubDivOrg.SelectedIndex = -1;
            SubDivOrg.DataSource = CVController.OrgSubDivTags(DivOrg.SelectedValue.ToInt());
            SubDivOrg.DataBind();
            BindSchedule();
        }

        protected void SubDivOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSchedule();
        }
        private void BindSchedule()
        {
            var divid = SubDivOrg.SelectedValue.ToInt();
            var progid = DivOrg.SelectedValue.ToInt();
            var q1 = from o in DbUtil.Db.Organizations
                     where o.DivOrgs.Any(t => t.DivId == divid) || divid == 0
                     where o.DivOrgs.Any(t => t.Division.ProgId == progid)
                     select o.OrganizationId;
            var q = from s in DbUtil.Db.WeeklySchedules
                    where s.Organizations.Any(o => q1.Contains(o.OrganizationId))
                    select new CodeValueItem
                    {
                        Id = s.Id,
                        Value = s.Description,
                    };
            var list = q.ToList();
            list.Insert(0, new CodeValueItem { Id = 0, Value = "(not specified)" });
            Schedule.DataSource = list;
            Schedule.DataBind();
        }
    }
}