﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CmsData;
using UtilityExtensions;

namespace CmsWeb.Code
{
    [Serializable]
    public class CodeInfo : IModelViewModelObject
    {
        private string name;
        private string value;

        [NonSerialized]
        private IEnumerable<SelectListItem> items;

        public CodeInfo()
        {

        }
        public CodeInfo(string name) : this(null, name) { }
        public CodeInfo(object value, string name)
        {
            if (value != null)
                Value = value.ToString();
            Name = name;
        }
        public CodeInfo(object value, IEnumerable<CodeValueItem> items)
        {
            if (value != null)
                Value = value.ToString();
            Items = items.ToSelect();
        }
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public IEnumerable<SelectListItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (!value.HasValue())
                    return;
                name = value;
                var cv = new CodeValueModel();
                switch (value)
                {
                    case "State":
                        Items = cv.StateList().ToSelect("Code");
                        break;
                    case "Country":
                        Items = cv.CountryList().ToSelect("Value");
                        break;
                    default:
                        var getlist = cv.GetType().GetMethod(value + "List");
                        if (getlist != null)
                        {
                            var list = getlist.Invoke(cv, null);
                            if(list as IEnumerable<CodeValueItem> != null)
                                Items = ((IEnumerable<CodeValueItem>) getlist.Invoke(cv, null)).ToSelect();
                            else if (list as SelectList != null)
                                Items = ((SelectList) getlist.Invoke(cv, null));
                        }
                        else
                            Items = new List<CodeValueItem>().ToSelect();
                        break;
                }
            }
        }

        public override string ToString()
        {
            if (Items == null)
                return Value;
            var i = Items.SingleOrDefault(ii => ii.Value == Value);
            if (i == null)
                return "";
            return i.Text;
        }

        public string CopyToModel(PropertyInfo vm, object model, PropertyInfo[] modelProps, bool track)
        {
            string altname = vm.Name + "Id";
            var attr = vm.GetAttribute<FieldInfoAttribute>();
            if (attr != null && attr.IdField.HasValue())
                altname = attr.IdField;
            var mid = modelProps.FirstOrDefault(mm => mm.Name == altname || mm.Name == vm.Name);
            if (mid == null)
                return string.Empty;
            if (track)
            {
                var changes = new StringBuilder();
                if (mid.PropertyType == typeof(int?))
                    if (Value == "0")
                        model.UpdateValue(changes, altname, null);
                    else
                        model.UpdateValue(changes, altname, Value.ToInt());
                return changes.ToString();
            }
            mid.SetPropertyFromText(model, Value);
            return string.Empty;
        }

        public void CopyFromModel(PropertyInfo vm, object model, PropertyInfo[] modelProps)
        {
            string altname = vm.Name + "Id";
            var fiattr = vm.GetAttribute<FieldInfoAttribute>();
            if (fiattr != null && fiattr.IdField.HasValue())
                altname = fiattr.IdField;
            var mid = modelProps.FirstOrDefault(mm => mm.Name == altname || mm.Name == vm.Name);
            var midvalue = mid.GetValue(model, null);
            if (mid.PropertyType == typeof(int?) || mid.PropertyType == typeof(int))
                Value = midvalue.ToInt().ToString();
            else if (midvalue == null)
                Value = null;
            else
                Value = midvalue.ToString();
            Name = vm.Name;
        }
    }
}