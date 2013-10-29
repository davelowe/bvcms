using System;
using System.Linq;
using UtilityExtensions;

namespace CmsData
{
	public partial class Meeting : ITableWithExtraValues
	{
		public MeetingExtra GetExtraValue(string field)
		{
			var ev = MeetingExtras.AsEnumerable().FirstOrDefault(ee => string.Compare(ee.Field, field, ignoreCase: true) == 0);
			if (ev == null)
			{
				ev = new MeetingExtra()
				{
					MeetingId = MeetingId,
					Field = field,
					
				};
				MeetingExtras.Add(ev);
			}
			return ev;
		}

		public void AddEditExtra(CMSDataContext Db, string field, string value, bool multiline = false)
		{
			var oev = Db.MeetingExtras.SingleOrDefault(oe => oe.MeetingId == MeetingId && oe.Field == field);
			if (oev == null)
			{
				oev = new MeetingExtra
					  {
						  MeetingId = MeetingId,
						  Field = field,
					  };
				Db.MeetingExtras.InsertOnSubmit(oev);
			}
			oev.Data = value;
			oev.DataType = multiline ? "text" : null;
		}
		public void AddToExtraData(string field, string value)
		{
			if (!value.HasValue())
				return;
			var ev = GetExtraValue(field);
			ev.DataType = "text";
			if (ev.Data.HasValue())
				ev.Data = value + "\n" + ev.Data;
			else
				ev.Data = value;
		}

		public string GetExtra(string field)
		{
			var oev = MeetingExtras.SingleOrDefault(oe => oe.MeetingId == MeetingId && oe.Field == field);
			if (oev == null)
				return null;
			return oev.Data;
		}
		public static Meeting FetchOrCreateMeeting(CMSDataContext Db, int OrgId, DateTime dt, bool? noautoabsents = null)
		{
			var meeting = (from m in Db.Meetings
						   where m.OrganizationId == OrgId && m.MeetingDate == dt
						   select m).FirstOrDefault();
			if (meeting == null)
			{
				var acr = (from s in Db.OrgSchedules
						   where s.OrganizationId == OrgId
						   where s.SchedTime.Value.TimeOfDay == dt.TimeOfDay
						   where s.SchedDay == (int)dt.DayOfWeek
						   select s.AttendCreditId).SingleOrDefault();
				meeting = new Meeting
				{
					OrganizationId = OrgId,
					MeetingDate = dt,
					CreatedDate = Util.Now,
					CreatedBy = Util.UserId1,
					GroupMeetingFlag = false,
					AttendCreditId = acr ?? 1,
                    NoAutoAbsents = noautoabsents
				};
				Db.Meetings.InsertOnSubmit(meeting);
				Db.SubmitChanges();
			}
			return meeting;
		}

        public void AddEditExtraValue(string field, string value)
        {
            if (!field.HasValue())
                return;
            if (!value.HasValue())
                return;
            var ev = GetExtraValue(field);
            ev.StrValue = value;
        }

        public void AddEditExtraData(string field, string value)
        {
            if (!value.HasValue())
                return;
            var ev = GetExtraValue(field);
            ev.Data = value;
        }

        public void AddEditExtraDate(string field, DateTime? value)
        {
            if (!value.HasValue)
                return;
            var ev = GetExtraValue(field);
            ev.DateValue = value;
        }

        public void AddEditExtraInt(string field, int value)
        {
            var ev = GetExtraValue(field);
            ev.IntValue = value;
        }

        public void AddEditExtraBool(string field, bool tf)
        {
            if (!field.HasValue())
                return;
            var ev = GetExtraValue(field);
            ev.BitValue = tf;
        }

        public void RemoveExtraValue(CMSDataContext Db, string field)
        {
            var ev = MeetingExtras.AsEnumerable().FirstOrDefault(ee => string.Compare(ee.Field, field, ignoreCase: true) == 0);
            if (ev != null)
                Db.MeetingExtras.DeleteOnSubmit(ev);
        }

        public static bool CheckExtraValueIntegrity(CMSDataContext Db, string type, string newfield)
        {
            return !Db.MeetingExtras.Any(ee => ee.Field == newfield && ee.Type != type);
        }
    }
}
