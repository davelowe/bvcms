using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Community.CsharpSqlite;
using UtilityExtensions;
using System.Linq;
using CmsData;
namespace CmsData
{
    public partial class QueryBuilderClause2
    {
        public void Save(CMSDataContext Db)
        {
            var c = Db.LoadQueryById2(Id);
            if (c == null)
            {
                c = new Query 
                {
                    QueryId = Id,
                    Owner = Util.UserName,
                    Created = DateTime.Now, 
                    Name = Util.ScratchPad,
                };
                Db.Queries.InsertOnSubmit(c);
            }
            c.Modified = DateTime.Now; 
            c.Text = ToXml();
	        Db.SubmitChanges();
        }

        public string ToXml()
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding(false);
            var sb = new StringBuilder();
            using (var w = XmlWriter.Create(sb, settings))
                SendToWriter(w);
            return sb.ToString();
        }
        public void SendToWriter(XmlWriter w)
        {
            w.WriteStartElement("Condition");
            WriteAttributes(w);
            foreach (var qc in Clauses)
                qc.SendToWriter(w);
            w.WriteEndElement();
        }
        private void WriteAttributes(XmlWriter w)
        {
            w.WriteAttributeString("Id", Id.ToString());
            w.WriteAttributeString("Field", Field);
            if (Description.HasValue())
                w.WriteAttributeString("Description", Description);
            w.WriteAttributeString("Comparison", Comparison);
            if (TextValue.HasValue())
                w.WriteAttributeString("TextValue", TextValue);
            if (DateValue.HasValue)
                w.WriteAttributeString("DateValue", DateValue.ToString());
            if (CodeIdValue.HasValue())
                w.WriteAttributeString("CodeIdValue", CodeIdValue);
            if (StartDate.HasValue)
                w.WriteAttributeString("StartDate", StartDate.ToString());
            if (EndDate.HasValue)
                w.WriteAttributeString("EndDate", EndDate.ToString());
            if (Program > 0)
                w.WriteAttributeString("Program", Program.ToString());
            if (Division > 0)
                w.WriteAttributeString("Division", Division.ToString());
            if (Organization > 0)
                w.WriteAttributeString("Organization", Organization.ToString());
            if (Days > 0)
                w.WriteAttributeString("Days", Days.ToString());
            if (Quarters.HasValue())
                w.WriteAttributeString("Quarters", Quarters);
            if (Tags.HasValue())
                w.WriteAttributeString("Tags", Tags);
            if (Schedule > 0)
                w.WriteAttributeString("Schedule", Schedule.ToString());
            if (Age.HasValue)
                w.WriteAttributeString("Age", Age.ToString());
        }
        public static QueryBuilderClause2 Import(string text, string name = null, bool newGuids = false)
        {
            if (!text.HasValue())
                return CreateNewGroupClause();
            var x = XDocument.Parse(text);
            Debug.Assert(x.Root != null, "x.Root != null");
            var c = ImportClause(x.Root, null, newGuids);
            return c;
        }
        private static QueryBuilderClause2 ImportClause(XElement r, QueryBuilderClause2 p, bool newGuids)
        {
            var allClauses = p == null ? new Dictionary<Guid, QueryBuilderClause2>() : p.AllClauses;
            Guid? parentGuid = null;
            if (p != null)
                parentGuid = p.Id;
            var c = new QueryBuilderClause2
            {
                ParentId = parentGuid,
                Id = AttributeGuid(r, "Id"),
                Order = AttributeInt(r, "Order"),
                Field = Attribute(r, "Field"),
                Comparison = Attribute(r, "Comparison"),
                TextValue = Attribute(r, "TextValue"),
                DateValue = AttributeDate(r, "DateValue"),
                CodeIdValue = Attribute(r, "CodeIdValue"),
                StartDate = AttributeDate(r, "StartDate"),
                EndDate = AttributeDate(r, "EndDate"),
                Program = Attribute(r, "Program").ToInt(),
                Division = Attribute(r, "Division").ToInt(),
                Organization = Attribute(r, "Organization").ToInt(),
                Days = Attribute(r, "Days").ToInt(),
                Quarters = Attribute(r, "Quarters"),
                Tags = Attribute(r, "Tags"),
                Schedule = Attribute(r, "Schedule").ToInt(),
                Age = Attribute(r, "Age").ToInt(),
                Owner = Attribute(r, "Owner"),
                AllClauses = allClauses
            };
            c.AllClauses.Add(c.Id, c);
            if (newGuids)
                c.Id = Guid.NewGuid();
            if (c.Field == "Group")
                foreach (var rr in r.Elements())
                    ImportClause(rr, c, newGuids);
            return c;
        }
        private static string Attribute(XElement r, string attr, string def = null)
        {
            var a = r.Attributes(attr).FirstOrDefault();
            if (a == null)
                return def;
            return a.Value;
        }
        private static DateTime? AttributeDate(XElement r, string attr)
        {
            var a = r.Attributes(attr).FirstOrDefault();
            if (a == null)
                return null;
            return a.Value.ToDate();
        }
        private static int AttributeInt(XElement r, string attr)
        {
            var a = r.Attributes(attr).FirstOrDefault();
            if (a == null)
                return 0;
            return a.Value.ToInt();
        }
        private static Guid AttributeGuid(XElement r, string attr)
        {
            var a = r.Attributes(attr).FirstOrDefault();
            if (a == null)
                return Guid.NewGuid();
            return new Guid(a.Value);
        }
    }
}