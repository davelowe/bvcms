using System.Collections.Generic;
using CmsData;

namespace CmsWeb.Areas.Search.Models
{
    public partial class QueryModel
    {
        public bool RightPanelVisible { get { return ComparePanelVisible; } }
        public bool ComparePanelVisible { get { return fieldMap.Name != "MatchAnything"; } }

        public bool TextVisible { get { return texttypes.Contains(fieldMap.Type); } }
        public bool NumberVisible { get { return numbertypes.Contains(fieldMap.Type); } }
        public bool IntegerVisible { get { return integertypes.Contains(fieldMap.Type); } }
        public bool CodeVisible { get { return codetypes.Contains(fieldMap.Type); } }
        public bool DateVisible { get { return datetypes.Contains(fieldMap.Type); } }

        public bool ProgramVisible { get { return fieldMap.HasParam("Program"); } }
        public bool DivisionVisible { get { return fieldMap.HasParam("Division"); } }
        public bool EndDateVisible { get { return fieldMap.HasParam("EndDate"); } }
        public bool StartDateVisible { get { return fieldMap.HasParam("StartDate"); } }
        public bool OrganizationVisible { get { return fieldMap.HasParam("Organization"); } }
        public bool ScheduleVisible { get { return fieldMap.HasParam("Schedule"); } }
        public bool CampusVisible { get { return fieldMap.HasParam("Campus"); } }
        public bool OrgTypeVisible { get { return fieldMap.HasParam("OrgType"); } }
        public bool DaysVisible { get { return fieldMap.HasParam("Days"); } }
        public bool AgeVisible { get { return fieldMap.HasParam("Age"); } }
        public bool SavedQueryVisible { get { return fieldMap.HasParam("SavedQueryIdDesc"); } }
        public bool MinistryVisible { get { return fieldMap.HasParam("Ministry"); } }
        public bool QuartersVisible { get { return fieldMap.HasParam("Quarters"); } }
        public bool TagsVisible { get { return fieldMap.HasParam("Tags"); } }
        public bool PmmLabelsVisible { get { return fieldMap.HasParam("PmmLabels"); } }

        private List<FieldType> texttypes = new List<FieldType>() 
        { 
            FieldType.String, 
            FieldType.StringEqual, 
            FieldType.StringEqualOrStartsWith 
        };
        private List<FieldType> codetypes = new List<FieldType>() 
        { 
            FieldType.Bit,
            FieldType.NullBit,
            FieldType.Code,
            FieldType.NullCode,
            FieldType.CodeStr
        };
        private List<FieldType> integertypes = new List<FieldType>() 
        { 
            FieldType.Integer,
            FieldType.IntegerEqual,
            FieldType.IntegerSimple,
            FieldType.NullInteger,
        };
        private List<FieldType> numbertypes = new List<FieldType>() 
        { 
            FieldType.NullNumber,
            FieldType.Number,
            FieldType.NumberLG
        };
        private List<FieldType> datetypes = new List<FieldType>() 
        { 
            FieldType.Date,
            FieldType.DateSimple,
        };
    }
}