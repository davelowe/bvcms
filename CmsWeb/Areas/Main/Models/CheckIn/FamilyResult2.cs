﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Web.Mvc;
using System.Xml.Linq;
using UtilityExtensions;
using System.Linq;
using CmsData;

namespace CMSWeb.Models
{
    public class FamilyResult2 : ActionResult
    {
        int fid, campus, thisday, page;

        public FamilyResult2(int fid, int campus, int thisday, int page)
        {
            this.fid = fid;
            this.campus = campus;
            this.thisday = thisday;
            this.page = page;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/xml";
            var settings = new XmlWriterSettings();
            settings.Encoding = new System.Text.UTF8Encoding(false);

            using (var w = XmlWriter.Create(context.HttpContext.Response.OutputStream, settings))
            {
                w.WriteStartElement("Attendees");
                var m = new CheckInModel();
                var q = m.FamilyMembers(fid, campus, thisday);

                var count = q.Count();
                const int INT_PageSize = 10;
                var startrow = (page - 1) * INT_PageSize;
                if (count > startrow + INT_PageSize)
                    w.WriteAttributeString("next", (page + 1).ToString());
                else
                    w.WriteAttributeString("next", "");
                if (page > 1)
                    w.WriteAttributeString("prev", (page - 1).ToString());
                else
                    w.WriteAttributeString("prev", "");
                
                foreach (var c in q.Skip(startrow).Take(INT_PageSize))
                {
                    double leadtime = 0;
                    if (c.Hour.HasValue)
                    {
                        var midnight = c.Hour.Value.Date;
                        var now = midnight.Add(Util.Now2.TimeOfDay);
                        leadtime = c.Hour.Value.Subtract(now).TotalHours;
                    }
                    w.WriteStartElement("attendee");
                    w.WriteAttributeString("id", c.Id.ToString());
                    w.WriteAttributeString("name", c.DisplayName);
                    w.WriteAttributeString("first", c.First);
                    w.WriteAttributeString("last", c.Last);
                    w.WriteAttributeString("org", c.DisplayClass);
                    w.WriteAttributeString("orgid", c.OrgId.ToString());
                    w.WriteAttributeString("loc", c.Location);
                    w.WriteAttributeString("gender", c.gender.ToString());
                    w.WriteAttributeString("leadtime", leadtime.ToString());
                    w.WriteAttributeString("age", c.Age.ToString());
                    w.WriteAttributeString("numlabels", c.NumLabels.ToString());
                    w.WriteAttributeString("checkedin", c.CheckedIn.ToString());

                    w.WriteAttributeString("email", c.email);
                    w.WriteAttributeString("dob", c.dob);
                    w.WriteAttributeString("goesby", c.goesby);
                    w.WriteAttributeString("addr", c.addr);
                    w.WriteAttributeString("zip", c.zip);
                    w.WriteAttributeString("home", c.home);
                    w.WriteAttributeString("cell", c.cell);
                    w.WriteAttributeString("marital", c.marital.ToString());

                    w.WriteEndElement();
                }
                w.WriteEndElement();
            }
        }
    }
}