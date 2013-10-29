using System;
using System.Collections.Generic;
using System.Linq;
using CmsData;
using CmsData.Registration;
using UtilityExtensions;
using System.Web.Mvc;
using CmsData.Codes;

namespace CmsWeb.Areas.Org.Models
{
    public class VolunteerCommitmentsModel
    {
        public string SmallGroup1 { get; set; }
        public string SmallGroup2 { get; set; }
        public bool SortByWeek { get; set; }

        public class NameId
        {
            public string Name { get; set; }
            public int PeopleId { get; set; }
            public int? Commitment { get; set; }
            public string CommitmentText
            {
                get
                {
                    return CmsData.Codes.AttendCommitmentCode.Lookup(Commitment);
                }
            }
        }
        public IEnumerable<DateTime> times;
        public string OrgName { get; set; }
        public int OrgId { get; set; }
        public bool IsLeader { get; set; }

        public class VolunteerInfo
        {
            public string Name { get; set; }
            public int commits { get; set; }
            public int PeopleId { get; set; }
            public int OrgId { get; set; }
        }
        public SelectList SmallGroups()
        {
            var q = from m in DbUtil.Db.MemberTags
                    where m.OrgId == OrgId
                    where m.OrgMemMemTags.Any()
                    orderby m.Name
                    select m.Name;
            var list = q.ToList();
            list.Insert(0, "(not specified)");
            return new SelectList(list);
        }
        public IEnumerable<VolunteerInfo> Volunteers()
        {
            var q = from om in DbUtil.Db.OrganizationMembers
                    where om.OrganizationId == OrgId
                    where SmallGroup1 == null || SmallGroup1 == "(not specified)"
                        || om.OrgMemMemTags.Any(mm => mm.MemberTag.Name == SmallGroup1)
                    where SmallGroup2 == null || SmallGroup2 == "(not specified)"
                        || om.OrgMemMemTags.Any(mm => mm.MemberTag.Name == SmallGroup2)
                    let commits = (from a in DbUtil.Db.Attends
                                   where a.MeetingDate > Util.Now.Date
                                   where a.OrganizationId == om.OrganizationId
                                   where a.PeopleId == om.PeopleId
                                   where a.Commitment == AttendCommitmentCode.Attending || a.Commitment == AttendCommitmentCode.Substitute
                                   select a).Count()
                    orderby om.Person.Name2
                    select new VolunteerInfo
                    {
                        Name = om.Person.Name,
                        commits = commits,
                        PeopleId = om.PeopleId,
                        OrgId = om.OrganizationId
                    };
            return q;
        }
        public DragDropInfo ddinfo { get; set; }
        public VolunteerCommitmentsModel(int id)
        {
            OrgName = (from o in DbUtil.Db.Organizations where o.OrganizationId == id select o.OrganizationName).Single();
            OrgId = id;
            times = from ts in Regsettings.TimeSlots.list
                    orderby ts.Time
                    select ts.Datetime(Sunday);

            IsLeader = OrganizationModel.VolunteerLeaderInOrg(id);
        }
        public class Slot
        {
            public bool IsLeader { get; set; }
            public int OrgId { get; set; }
            public DateTime Time { get; set; }
            public TimeSpan TimeOfDay { get { return Time.TimeOfDay; } }
            public long ticks { get { return Time.Ticks; } }
            public DateTime Sunday { get; set; }
            public int Week { get; set; }
            public bool Disabled { get; set; }
            public DateTime DayHour { get; set; }
            public List<NameId> Persons { get; set; }
            public int Count
            {
                get { return Persons.Count(pp => pp.Commitment == AttendCommitmentCode.Attending || pp.Commitment == AttendCommitmentCode.Substitute); }
            }
            public List<NameId> OrderedPersons()
            {
                return (from p in Persons
                        orderby CmsData.Codes.AttendCommitmentCode.Order(p.Commitment), p.Name
                        select p).ToList();
            }
            public int Limit { get; set; }
            public int MeetingId { get; set; }
        }

        public IEnumerable<List<Slot>> FetchSlotWeeks()
        {
            if (SortByWeek)
                return from slot in FetchSlots()
                       group slot by slot.Sunday into g
                       where g.Any(gg => gg.Time > DateTime.Today)
                       orderby g.Key.WeekOfMonth(), g.Key
                       select g.OrderBy(gg => gg.Time).ToList();
            else
                return from slot in FetchSlots()
                       group slot by slot.Sunday into g
                       where g.Any(gg => gg.Time > DateTime.Today)
                       orderby g.Key
                       select g.OrderBy(gg => gg.Time).ToList();
        }
        public List<Slot> FetchSlotWeeks2(TimeSpan? ts = null)
        {
            if (ts == null)
                ts = times.First().TimeOfDay;
            if (SortByWeek)
                return (from slot in FetchSlots()
                        where slot.TimeOfDay == ts
                        where slot.Time > DateTime.Today
                        orderby slot.Week
                        select slot).ToList();
            else
                return (from slot in FetchSlots()
                        where slot.TimeOfDay == ts
                        where slot.Time > DateTime.Today
                        orderby slot.Time
                        select slot).ToList();

            /*
                       group slot by slot.TimeOfDay into g
                       where g.Any(gg => gg.Time > DateTime.Today)
                       orderby g.Key
                       select g.OrderBy(gg => gg.Week).ToList();
             */
        }
        public IEnumerable<Slot> FetchSlots()
        {
            var mlist = (from m in DbUtil.Db.Meetings
                         where m.MeetingDate > Util.Now.Date
                         where m.OrganizationId == OrgId
                         orderby m.MeetingDate
                         select m).ToList();
            var alist = (from a in DbUtil.Db.Attends
                         where a.MeetingDate > Util.Now.Date
                         where a.OrganizationId == OrgId
                         where a.Commitment != null
                         orderby a.MeetingDate
                         select new
                         {
                             a.MeetingId,
                             a.MeetingDate,
                             a.PeopleId,
                             a.Person.Name,
                             a.Commitment,
                         }).ToList();

            var list = new List<Slot>();
            for (var sunday = Sunday; sunday <= EndDt; sunday = sunday.AddDays(7))
            {
                var dt = sunday;
                {
                    var u = from ts in Regsettings.TimeSlots.list
                            orderby ts.Datetime()
                            let time = ts.Datetime(dt)
                            let meeting = mlist.SingleOrDefault(mm => mm.MeetingDate == time)
                            let needed = meeting != null ?
                                    (from e in meeting.MeetingExtras
                                     where e.Field == "TotalVolunteersNeeded"
                                     select e.Data).SingleOrDefault().ToInt2() : null
                            let meetingid = meeting != null ? meeting.MeetingId : 0
                            select new Slot()
                                    {
                                        OrgId = OrgId,
                                        IsLeader = IsLeader,
                                        Time = time,
                                        Sunday = dt,
                                        Week = dt.WeekOfMonth(),
                                        Disabled = time < DateTime.Now,
                                        Limit = needed.ToInt2() ?? ts.Limit ?? 0,
                                        Persons = (from a in alist
                                                   where a.MeetingDate == time
                                                   select new NameId
                                                   {
                                                       Name = a.Name,
                                                       PeopleId = a.PeopleId,
                                                       Commitment = a.Commitment
                                                   }).ToList(),
                                        MeetingId = meetingid
                                    };
                    list.AddRange(u);
                }
            }
            return list;
        }
        private CmsData.Organization _org;
        public CmsData.Organization Org
        {
            get
            {
                return _org ??
                    (_org = DbUtil.Db.Organizations.Single(oo => oo.OrganizationId == OrgId));
            }
        }
        private DateTime? _endDt;
        public DateTime EndDt
        {
            get
            {
                if (!_endDt.HasValue)
                {
                    var dt = Org.LastMeetingDate ?? DateTime.MinValue;
                    if (dt == DateTime.MinValue)
                        dt = DateTime.Today.AddMonths(7);
                    _endDt = dt;
                }
                return _endDt.Value;
            }
        }

        private DateTime? _sunday;
        public DateTime Sunday
        {
            get
            {
                if (!_sunday.HasValue)
                {
                    var dt = Org.FirstMeetingDate ?? DateTime.MinValue;
                    if (dt == DateTime.MinValue || dt < DateTime.Today)
                        dt = DateTime.Today;
                    _sunday = dt.AddDays(-(int)dt.DayOfWeek);
                }
                return _sunday.Value;
            }
        }
        private Settings _regsettings;
        public Settings Regsettings
        {
            get
            {
                return _regsettings ??
                    (_regsettings = new Settings(Org.RegSetting, DbUtil.Db, OrgId));
            }
        }
        public void ApplyDragDrop(
            string target,
            int? week,
            DateTime? time,
            DragDropInfo i)
        {
            List<int> volids = null;

            switch (i.source)
            {
                case "nocommits":
                    volids = (from p in Volunteers()
                              where p.commits == 0
                              select p.PeopleId).ToList();
                    break;
                case "commits":
                    volids = (from p in Volunteers()
                              where p.commits > 0
                              select p.PeopleId).ToList();
                    break;
                case "all":
                    volids = (from p in Volunteers()
                              select p.PeopleId).ToList();
                    break;
                case "registered":
                case "person":
                    volids = new List<int>() { i.pid.ToInt() };
                    break;
                default:
                    return;
            }

            if (target == "week")
            {
                var slots = (from s in FetchSlots()
                             where s.Time.TimeOfDay == time.Value.TimeOfDay
                             where s.Week == week || week == 0
                             select s).ToList();
                foreach (var PeopleId in volids)
                {
                    if (i.source == "registered")
                        DropFromAll(PeopleId);
                    foreach (var s in slots)
                        Attend.MarkRegistered(DbUtil.Db, OrgId, PeopleId, s.Time,
                            AttendCommitmentCode.Attending, AvoidRegrets: true);
                }
            }
            else if (target == "meeting")
            {
                foreach (var PeopleId in volids)
                {
                    if (i.source == "registered")
                        DropFromMeeting(i.mid.Value, PeopleId);
                    Attend.MarkRegistered(DbUtil.Db, OrgId, PeopleId, time.Value,
                        AttendCommitmentCode.Attending, AvoidRegrets: true);
                }

            }
            else if (target == "clear")
            {
                foreach (var PeopleId in volids)
                {
                    if (i.source == "registered")
                        DropFromMeeting(i.mid.Value, PeopleId);
                    else
                        DropFromAll(PeopleId);
                }
            }
        }
        private void DropFromMeeting(int meetingid, int peopleid)
        {
            DbUtil.Db.ExecuteCommand("DELETE FROM dbo.SubRequest WHERE EXISTS(SELECT NULL FROM Attend a WHERE a.AttendId = AttendId AND a.MeetingId = {0} AND a.PeopleId = {1})", meetingid, peopleid);
            DbUtil.Db.ExecuteCommand("DELETE dbo.Attend WHERE MeetingId = {0} AND PeopleId = {1}", meetingid, peopleid);
        }
        private void DropFromAll(int peopleid)
        {
            DbUtil.Db.ExecuteCommand("DELETE FROM dbo.SubRequest WHERE EXISTS(SELECT NULL FROM Attend a WHERE ISNULL(Commitment, 1) = 1 AND a.AttendId = AttendId AND a.OrganizationId = {0} AND a.MeetingDate > {1} AND a.PeopleId = {2})", OrgId, Sunday, peopleid);
            DbUtil.Db.ExecuteCommand("DELETE dbo.Attend WHERE OrganizationId = {0} AND MeetingDate > {1} AND PeopleId = {2} AND ISNULL(Commitment, 1) = 1", OrgId, Sunday, peopleid);
        }
    }
    public class DragDropInfo
    {
        public string source { get; set; }
        public int? pid { get; set; }
        public int? mid { get; set; }
    }
}
