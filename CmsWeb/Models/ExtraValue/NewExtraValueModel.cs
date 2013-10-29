﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using CmsData;
using CmsWeb.Code;
using Dapper;
using UtilityExtensions;

namespace CmsWeb.Models.ExtraValues
{
    public class NewExtraValueModel
    {
        public int Id { get; set; }
        public Guid QueryId { get; set; }
        public string ExtraValueTable { get; set; }
        public string ExtraValueLocation { get; set; }

        [DisplayName("Name"), StringLength(20), Required]
        public string ExtraValueName { get; set; }

        [DisplayName("Type")]
        public CodeInfo ExtraValueType { get; set; }

        [DisplayName("Checkboxes Prefix")]
        public string ExtraValueBitPrefix { get; set; }

        [DisplayName("Type")]
        public CodeInfo AdhocExtraValueType { get; set; }

        [DisplayName("Text Value")]
        public string ExtraValueTextBox { get; set; }

        [DisplayName("Text Value"), UIHint("Textarea")]
        public string ExtraValueTextArea { get; set; }

        [DisplayName("Date Value")]
        public DateTime? ExtraValueDate { get; set; }

        [DisplayName("Checkbox Value")]
        public bool ExtraValueCheckbox { get; set; }

        [DisplayName("Integer Value")]
        public int ExtraValueInteger { get; set; }

        public bool RemoveAnyValue { get; set; }

        private string BitPrefix
        {
            get
            {
                if (ExtraValueBitPrefix.HasValue())
                    return ExtraValueBitPrefix + "-";
                return "";
            }
        }
        [DisplayNameAttribute("Codes")]
        public string ExtraValueCodes { get; set; }

        [DisplayNameAttribute("Checkboxes"), UIHint("Textarea")]
        public string ExtraValueCheckboxes { get; set; }

        public string VisibilityRoles { get; set; }

        public NewExtraValueModel(Guid id)
        {
            AdhocExtraValueType = new CodeInfo("Text", "AdhocExtraValueType");
            QueryId = id;
            ExtraValueTable = "People";
            ExtraValueLocation = "Adhoc";
        }

        public NewExtraValueModel(int id, string table, string location)
        {
            ExtraValueType = new CodeInfo("Text", "ExtraValueType");
            AdhocExtraValueType = new CodeInfo("Text", "AdhocExtraValueType");
            Id = id;
            ExtraValueTable = table;
            ExtraValueLocation = location;
        }
        public NewExtraValueModel() { }

        public void TryCheckIntegrity()
        {
            const string nameAlreadyExistsAsADifferentType = "name already exists as a different type";
            string type = ExtraValueLocation == "Adhoc" ? AdhocExtraValueType.Value : ExtraValueType.Value;
            switch (ExtraValueTable)
            {
                case "People":
                    if (type == "Bits")
                        foreach (var b in BitCodes().Where(b => DbUtil.Db.PeopleExtras.Any(ee => ee.Field == b && ee.Type != "Bit")))
                            throw new Exception(nameAlreadyExistsAsADifferentType.Fmt(b));
                    else
                        if (DbUtil.Db.PeopleExtras.Any(ee => ee.Field == ExtraValueName && ee.Type != type))
                            throw new Exception(nameAlreadyExistsAsADifferentType.Fmt(ExtraValueName));
                    break;
                case "Family":
                    if (type == "Bits")
                        foreach (var b in BitCodes().Where(b => DbUtil.Db.FamilyExtras.Any(ee => ee.Field == b && ee.Type != "Bit")))
                            throw new Exception(nameAlreadyExistsAsADifferentType.Fmt(b));
                    else
                        if (DbUtil.Db.FamilyExtras.Any(ee => ee.Field == ExtraValueName && ee.Type != type))
                            throw new Exception(nameAlreadyExistsAsADifferentType.Fmt(ExtraValueName));
                    break;
                case "Organization":
                    if (type == "Bits")
                        foreach (var b in BitCodes().Where(b => DbUtil.Db.OrganizationExtras.Any(ee => ee.Field == b && ee.Type != "Bit")))
                            throw new Exception(nameAlreadyExistsAsADifferentType.Fmt(b));
                    else
                        if (DbUtil.Db.OrganizationExtras.Any(ee => ee.Field == ExtraValueName && ee.Type != type))
                            throw new Exception(nameAlreadyExistsAsADifferentType.Fmt(ExtraValueName));
                    break;
                case "Meeting":
                    if (type == "Bits")
                        foreach (var b in BitCodes().Where(b => DbUtil.Db.MeetingExtras.Any(ee => ee.Field == b && ee.Type != "Bit")))
                            throw new Exception(nameAlreadyExistsAsADifferentType.Fmt(b));
                    else
                        if (DbUtil.Db.MeetingExtras.Any(ee => ee.Field == ExtraValueName && ee.Type != type))
                            throw new Exception(nameAlreadyExistsAsADifferentType.Fmt(ExtraValueName));
                    break;
            }
        }

        public List<string> BitCodes()
        {
            var codes = ExtraValueType.Value == "Bits"
                ? ExtraValueCheckboxes
                : ExtraValueType.Value == "Code"
                ? ExtraValueCodes
                : "";
            return codes.SplitLines().Select(ss => BitPrefix + ss).ToList();
        }

        public string AddAsNewStandard()
        {
		    ExtraValueName = ExtraValueName.Replace('/', '-');
            var fields = Views.GetStandardExtraValues(ExtraValueTable);
            var existing = fields.SingleOrDefault(ff => ff.Name == ExtraValueName);
            if (existing != null)
                throw new Exception("field already exists");

            TryCheckIntegrity();

            var v = new Value
            {
                Name = ExtraValueName,
                Type = ExtraValueType.Value,
                Codes = BitCodes(),
                VisibilityRoles = VisibilityRoles
            };
            var i = Views.GetViewsView(ExtraValueTable, ExtraValueLocation);
            i.view.Values.Add(v);
            i.views.Save();
            return null;
        }


        public string AddAsNewAdhoc()
        {
            TryCheckIntegrity();
            if (Id > 0)
                return AddNewExtraValueToRecord();
            return AddNewExtraValueToSelectionFromQuery();
        }

        private string AddNewExtraValueToRecord()
        {
            var o = ExtraValueModel.TableObject(Id, ExtraValueTable);
            switch (AdhocExtraValueType.Value)
            {
                case "Code":
                    o.AddEditExtraValue(ExtraValueName, ExtraValueTextBox);
                    break;
                case "Text":
                    o.AddEditExtraData(ExtraValueName, ExtraValueTextArea);
                    break;
                case "Text2":
                    o.AddEditExtraData(ExtraValueName, ExtraValueTextArea);
                    break;
                case "Date":
                    o.AddEditExtraDate(ExtraValueName, ExtraValueDate);
                    break;
                case "Int":
                    o.AddEditExtraInt(ExtraValueName, ExtraValueInteger);
                    break;
                case "Bit":
                    o.AddEditExtraBool(ExtraValueName, ExtraValueCheckbox);
                    break;
            }
            DbUtil.Db.SubmitChanges();
            return null;
        }

        private string AddNewExtraValueToSelectionFromQuery()
        {
            var list = DbUtil.Db.PeopleQuery(QueryId).Select(pp => pp.PeopleId).ToList();

            switch (AdhocExtraValueType.Value)
            {
                case "Code":
                    return AddNewExtraValueCodes(list);
                case "Text":
                case "Text2":
                    return AddNewExtraValueDatums(list);
                case "Date":
                    return AddNewExtraValueDates(list);
                case "Int":
                    return AddNewExtraValueInts(list);
                case "Bit":
                    return AddNewExtraValueBools(list);
            }
            return null;
        }

        private string AddNewExtraValueCodes(IEnumerable<int> list)
        {
            foreach (var pid in list)
            {
                Person.AddEditExtraValue(DbUtil.Db, pid, ExtraValueName, ExtraValueTextBox);
                DbUtil.Db.SubmitChanges();
                DbUtil.DbDispose();
            }
            return null;
        }
        private string AddNewExtraValueDatums(IEnumerable<int> list)
        {
            foreach (var pid in list)
            {
                Person.AddEditExtraData(DbUtil.Db, pid, ExtraValueName, ExtraValueTextArea);
                DbUtil.Db.SubmitChanges();
                DbUtil.DbDispose();
            }
            return null;
        }
        private string AddNewExtraValueDates(IEnumerable<int> list)
        {
            foreach (var pid in list)
            {
                Person.AddEditExtraDate(DbUtil.Db, pid, ExtraValueName, ExtraValueDate);
                DbUtil.Db.SubmitChanges();
                DbUtil.DbDispose();
            }
            return null;
        }
        private string AddNewExtraValueInts(IEnumerable<int> list)
        {
            foreach (var pid in list)
            {
                Person.AddEditExtraInt(DbUtil.Db, pid, ExtraValueName, ExtraValueInteger);
                DbUtil.Db.SubmitChanges();
                DbUtil.DbDispose();
            }
            return null;
        }
        private string AddNewExtraValueBools(IEnumerable<int> list)
        {
            foreach (var pid in list)
            {
                Person.AddEditExtraBool(DbUtil.Db, pid, ExtraValueName, ExtraValueCheckbox);
                DbUtil.Db.SubmitChanges();
                DbUtil.DbDispose();
            }
            return null;
        }

        public void DeleteFromQuery()
        {
            var list = DbUtil.Db.PeopleQuery(QueryId).Select(pp => pp.PeopleId).ToList();

            var cn = new SqlConnection(Util.ConnectionString);
            cn.Open();
            if (RemoveAnyValue)
            {
                cn.Execute("delete from dbo.PeopleExtra where Field = @name and PeopleId in @ids",
                    new {name = ExtraValueName, ids = list});
                return;
            }
            switch (AdhocExtraValueType.Value)
            {
                case "Bit":
                    cn.Execute("delete from dbo.PeopleExtra where Field = @name and BitValue = @value and PeopleId in @ids",
                        new { name = ExtraValueName, value = ExtraValueCheckbox, ids = list });
                    break;
                case "Code":
                    cn.Execute("delete from dbo.PeopleExtra where Field = @name and StrValue = @value and PeopleId in @ids",
                        new { name = ExtraValueName, value = ExtraValueCheckbox, ids = list });
                    break;
                case "Text2":
                case "Text":
                    cn.Execute("delete from dbo.PeopleExtra where Field = @name and Data = @value and PeopleId in @ids",
                        new { name = ExtraValueName, value = ExtraValueTextArea, ids = list });
                    break;
                case "Date":
                    cn.Execute("delete from dbo.PeopleExtra where Field = @name and Date = @value and PeopleId in @ids",
                        new { name = ExtraValueName, value = ExtraValueDate, ids = list });
                    break;
                case "Int":
                    cn.Execute("delete from dbo.PeopleExtra where Field = @name and IntValue = @value and PeopleId in @ids",
                        new { name = ExtraValueName, value = ExtraValueInteger, ids = list });
                    break;
            }
        }
    }
}