﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CmsData;
using CmsWeb.Code;
using Dapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using Newtonsoft.Json;
using UtilityExtensions;

namespace CmsWeb.Models.ExtraValues
{
    public class ExtraValueModel
    {
        public string Location { get; set; }
        public int Id { get; set; }
        public string Table { get; set; }
        public string OtherLocations;

        public CodeInfo ExtraValueType { get; set; }
        public string Name { get; set; }
        public string CodeStrings { get; set; }
        public string ValueText { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool? ValueBit { get; set; }
        public int? ValueInt { get; set; }

        public ExtraValueModel(int id, string table)
            : this(id, table, null)
        {
        }

        public ExtraValueModel(string table)
            : this(0, table, null)
        {
        }

        public ExtraValueModel(string table, string location)
            : this(0, table, location)
        {
        }

        public ExtraValueModel(int id, string table, string location)
        {
            Id = id;
            Location = location;
            Table = table;
        }

        public static string HelpLink(string page)
        {
            return Util.HelpLink("_AddExtraValue_{0}".Fmt(page));
        }

        public IEnumerable<ExtraValue> ListExtraValues()
        {
            IEnumerable<ExtraValue> q = null;
            switch (Table)
            {
                case "People":
                    q = from ee in DbUtil.Db.PeopleExtras
                        where ee.PeopleId == Id
                        select new ExtraValue(ee, this);
                    break;
                case "Family":
                    q = from ee in DbUtil.Db.FamilyExtras
                        where ee.FamilyId == Id
                        select new ExtraValue(ee, this);
                    break;
                case "Organization":
                    q = from ee in DbUtil.Db.OrganizationExtras
                        where ee.OrganizationId == Id
                        select new ExtraValue(ee, this);
                    break;
                case "Meeting":
                    q = from ee in DbUtil.Db.MeetingExtras
                        where ee.MeetingId == Id
                        select new ExtraValue(ee, this);
                    break;
                default:
                    q = new List<ExtraValue>();
                    break;
            }
            return q.ToList();
        }

        private ITableWithExtraValues TableObject()
        {
            return TableObject(Id, Table);
        }

        public static ITableWithExtraValues TableObject(int id, string table)
        {
            switch (table)
            {
                case "People":
                    return DbUtil.Db.LoadPersonById(id);
                case "Organization":
                    return DbUtil.Db.LoadOrganizationById(id);
                case "Family":
                    return DbUtil.Db.Families.SingleOrDefault(f => f.FamilyId == id);
                //                                case "Meeting":
                //                                    return DbUtil.Db.Meetings.SingleOrDefault(m => m.MeetingId == Id);
                default:
                    return null;
            }
        }

        public IEnumerable<Value> GetExtraValues()
        {
            var realExtraValues = ListExtraValues();

            if (Location.ToLower() == "adhoc")
            {
                var standardExtraValues = Views.GetStandardExtraValues(Table);
                return from v in realExtraValues
                       join f in standardExtraValues on v.Field equals f.Name into j
                       from f in j.DefaultIfEmpty()
                       where f == null
                       // only adhoc values
                       where !standardExtraValues.Any(ff => ff.Codes.Any(cc => cc == v.Field))
                       orderby v.Field
                       select Value.AddField(f, v, this);
            }

            return from f in Views.GetStandardExtraValues(Table, Location)
                   join v in realExtraValues on f.Name equals v.Field into j
                   from v in j.DefaultIfEmpty()
                   orderby f.Order
                   select Value.AddField(f, v, this);
        }

        //        public List<SelectListItem> ExtraValueCodes()
        //        {
        //            var q = from e in DbUtil.Db.PeopleExtras
        //                    where e.StrValue != null || e.BitValue != null
        //                    group e by new { e.Field, val = e.StrValue ?? (e.BitValue == true ? "1" : "0") }
        //                        into g
        //                        select g.Key;
        //            var list = q.ToList();
        //
        //            var ev = Views.GetStandardExtraValues(Table);
        //            var q2 = from e in list
        //                     let f = ev.SingleOrDefault(ff => ff.Name == e.Field)
        //                     where f == null || f.UserCanView()
        //                     orderby e.Field, e.val
        //                     select new SelectListItem()
        //                            {
        //                                Text = e.Field + ":" + e.val,
        //                                Value = e.Field + ":" + e.val,
        //                            };
        //            return q2.ToList();
        //        }
        public Dictionary<string, string> Codes(string name)
        {
            var f = Views.GetStandardExtraValues(Table).Single(ee => ee.Name == name);
            return f.Codes.ToDictionary(ee => ee, ee => ee);
        }

        public string CodesJson(string name)
        {
            var f = Views.GetStandardExtraValues(Table).Single(ee => ee.Name == name);
            var q = from c in f.Codes
                    select new { value = c, text = c };
            return JsonConvert.SerializeObject(q.ToArray());
        }

        private class AllBitsCheckedOrNot
        {
            public string Name { get; set; }
            public bool Checked { get; set; }
        }

        private IEnumerable<AllBitsCheckedOrNot> ExtraValueBits(string name)
        {
            var f = Views.GetStandardExtraValues(Table).Single(ee => ee.Name == name);
            var list = ListExtraValues().Where(pp => f.Codes.Contains(pp.Field)).ToList();
            var q = from c in f.Codes
                    join e in list on c equals e.Field into j
                    from e in j.DefaultIfEmpty()
                    select new AllBitsCheckedOrNot()
                    { 
                        Name = c, 
                        Checked = (e != null && (e.BitValue ?? false)) 
                    };
            return q;
        }

        public string DropdownBitsJson(string name)
        {
            var f = Views.GetStandardExtraValues(Table).Single(ee => ee.Name == name);
            var list = ListExtraValues().Where(pp => f.Codes.Contains(pp.Field)).ToList();
            var q = from c in f.Codes
                    join e in list on c equals e.Field into j
                    from e in j.DefaultIfEmpty()
                    select new 
                    { 
                        value = c, 
                        text = c 
                    };
            return JsonConvert.SerializeObject(q.ToArray());
        }

        public string ListBitsJson(string name)
        {
            var f = Views.GetStandardExtraValues(Table).Single(ee => ee.Name == name);
            var list = ListExtraValues().Where(pp => f.Codes.Contains(pp.Field)).ToList();
            var q = from c in f.Codes
                    join e in list on c equals e.Field into j
                    from e in j.DefaultIfEmpty()
                    where e != null && e.BitValue == true
                    select c;
            return JsonConvert.SerializeObject(q.ToArray());
        }

        public void EditExtra(string type, string name, string value)
        {
            var record = TableObject();
            if (record == null)
                return;
            if (value == null)
                value = HttpContext.Current.Request.Form["value[]"];
            switch (type)
            {
                case "Code":
                    record.AddEditExtraValue(name, value);
                    break;
                case "Data":
                case "Text":
                case "Text2":
                    record.AddEditExtraData(name, value);
                    break;
                case "Date":
                    {
                        DateTime dt;
                        if (DateTime.TryParse(value, out dt))
                            record.AddEditExtraDate(name, dt);
                        else
                            record.RemoveExtraValue(DbUtil.Db, name);
                    }
                    break;
                case "Int":
                    record.AddEditExtraInt(name, value.ToInt());
                    break;
                case "Bit":
                    if (value == "True")
                        record.AddEditExtraBool(name, true);
                    else
                        record.RemoveExtraValue(DbUtil.Db, name);
                    break;
                case "Bits":
                    {
                        var existingBits = ExtraValueBits(name);
                        var newCheckedBits = value.Split(',');
                        foreach (var currentBit in existingBits)
                        {
                            if (newCheckedBits.Contains(currentBit.Name))
                                if (!currentBit.Checked)
                                    record.AddEditExtraBool(currentBit.Name, true);
                            if (!newCheckedBits.Contains(currentBit.Name))
                                if (currentBit.Checked)
                                    record.RemoveExtraValue(DbUtil.Db, currentBit.Name);
                        }
                        break;
                    }
            }
            DbUtil.Db.SubmitChanges();
        }

        public static void NewExtra(int id, string field, string type, string value)
        {
            field = field.Replace('/', '-');
            var v = new PeopleExtra { PeopleId = id, Field = field };
            DbUtil.Db.PeopleExtras.InsertOnSubmit(v);
            switch (type)
            {
                case "string":
                    v.StrValue = value;
                    break;
                case "text":
                    v.Data = value;
                    break;
                case "date":
                    DateTime dt;
                    DateTime.TryParse(value, out dt);
                    v.DateValue = dt;
                    break;
                case "int":
                    v.IntValue = value.ToInt();
                    break;
            }
            DbUtil.Db.SubmitChanges();
        }

        public void DeleteStandard(string name, bool removedata)
        {
            var i = Views.GetViewsViewValue(Table, name);
            i.view.Values.Remove(i.value);
            i.views.Save();

            if (!removedata)
                return;
            var cn = DbUtil.Db.Connection;
            cn.Open();
            if (i.value.Codes.Count == 0)
                cn.Execute("delete from dbo.{0}Extra where Field = @name".Fmt(Table), new { name });
            else
                cn.Execute("delete from dbo.{0}Extra where Field in @codes".Fmt(Table), new { codes = i.value.Codes });
        }

        public void Delete(string name)
        {
            var o = TableObject();
            o.RemoveExtraValue(DbUtil.Db, name);
            DbUtil.Db.SubmitChanges();
        }

        public void ApplyOrder(Dictionary<string, int> orders)
        {
            var i = Views.GetViewsView(Table, Location);
            var q = from v in i.view.Values
                    join o in orders on v.Name equals o.Key
                    orderby o.Value
                    select v;
            i.view.Values = q.ToList();
            int n = 1;
            foreach (var v in i.view.Values)
                v.Order = n++;
            i.views.Save();
        }

        public void SwitchMultiline(string name)
        {
            var i = Views.GetViewsViewValue(Table, name);
            i.value.Type = i.value.Type == "Text" ? "Text2" : "Text";
            i.views.Save();
        }
    }
}
