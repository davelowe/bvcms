﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CmsData;
using System.Text;
using UtilityExtensions;
using System.Text.RegularExpressions;

namespace CmsWeb.Models
{
    public partial class OnlineRegModel
    {
        public void EnrollAndConfirm()
        {
            var ti = Transaction;
            var elist = new List<string>();
            if (UserPeopleId.HasValue)
                elist.Add(user.EmailAddress);
            var participants = new StringBuilder();
            for (var i = 0; i < List.Count; i++)
            {
                var p = List[i];
                if (p.IsNew)
                {
                    Person uperson = null;
                    switch (p.whatfamily)
                    {
                        case 1:
                            uperson = DbUtil.Db.LoadPersonById(UserPeopleId.Value);
                            break;
                        case 2:
                            if (i > 0)
                                uperson = List[i - 1].person;
                            break;
                    }
                    p.AddPerson(uperson, p.org.EntryPointId ?? 0);
                }

                if (!elist.Contains(p.email))
                    elist.Add(p.email);

                if (!p.IsNew)
                    if (p.person.EmailAddress.HasValue())
                        if (!elist.Contains(p.person.EmailAddress))
                            elist.Add(p.person.EmailAddress);
                participants.Append(p.ToString());
            }
            var p0 = List[0].person;
            if (this.user != null)
                p0 = user;

            var emails = string.Join(",", elist.ToArray());
            string paylink = string.Empty;
            var amtdue = TotalAmountDue();
            var amtpaid = Amount();

            var pids2 = new List<TransactionPerson>();
            foreach (var p in List)
                pids2.Add(new TransactionPerson
                {
                    PeopleId = p.PeopleId.Value,
                    Amt = p.AmountToPay() + p.AmountDue(),
                    OrgId = orgid,
                });

            ti.Emails = emails;
            ti.Participants = participants.ToString();
            ti.TransactionDate = DateTime.Now;
            ti.TransactionPeople.AddRange(pids2);

            var estr = HttpUtility.UrlEncode(Util.Encrypt(ti.Id.ToString()));
            paylink = Util.ResolveServerUrl("/OnlineReg/PayAmtDue?q=" + estr);

            var pids = pids2.Select(pp => pp.PeopleId);

            var details = new StringBuilder("<table>");
            for (var i = 0; i < List.Count; i++)
            {
                var p = List[i];

                var q = from pp in DbUtil.Db.People
                        where pids.Contains(pp.PeopleId)
                        where pp.PeopleId != p.PeopleId
                        select pp.Name;
                var others = string.Join(",", q.ToArray());

                others += "(Total due {0:c})".Fmt(amtdue);
                var om = p.Enroll(ti.TransactionId, paylink, testing, others);
                details.AppendFormat(@"
<tr><td colspan='2'><hr/></td></tr>
<tr><th valign='top'>{0}</th><td>
{1}
</td></tr>", i + 1, p.PrepareSummaryText());

                om.RegisterEmail = p.email;
                if (p.org.GiveOrgMembAccess == true)
                {
                    CmsData.Group g = null;
                    if (org.GroupToJoin.HasValue())
                        g = CmsData.Group.LoadByName(org.GroupToJoin);

                    if (p.person.Users.Count() == 0)
                    {
                        p.IsNew = false;
                        p.CreateAccount();
                    }
                    foreach (var u in p.person.Users)
                    {
                        var list = u.Roles.ToList();
                        if (!list.Contains("Access"))
                            list.Add("Access");
                        if (!list.Contains("OrgMembersOnly"))
                            list.Add("OrgMembersOnly");
                        u.SetRoles(DbUtil.Db, list.ToArray());
                        if (org.GroupToJoin.HasValue())
                        {
                            g.SetMember(u, true);
                            u.DefaultGroup = g.Name;
                        }
                    }
                    DbUtil.Db.SubmitChanges();
                }
                OnlineRegPersonModel.CheckNotifyDiffEmails(p.person,
                    p.org.EmailAddresses,
                    p.email,
                    p.org.OrganizationName,
                    p.org.PhoneNumber);
                if (p.CreatingAccount == true && (p.org.GiveOrgMembAccess ?? false) == false)
                    p.CreateAccount();
            }
            details.Append("\n</table>\n");
            DbUtil.Db.SubmitChanges();

            string DivisionName = null;
            if (div != null)
                DivisionName = div.Name;
            else if (org != null)
                DivisionName = org.DivisionName;

            string OrganizationName = null;
            if (div != null)
                OrganizationName = "";
            else if (org != null)
                OrganizationName = org.OrganizationName;
            if (!OrganizationName.HasValue())
                OrganizationName = DivisionName;

            string EmailSubject = null;
            if (div != null)
                EmailSubject = div.EmailSubject;
            else if (org != null)
                EmailSubject = org.EmailSubject;

            string EmailMessage = null;
            if (div != null)
                EmailMessage = div.EmailMessage;
            else if (org != null)
                EmailMessage = org.EmailMessage;

            string EmailAddresses = null;
            if (div != null)
                EmailAddresses = List[0].org.EmailAddresses;
            else if (org != null)
                EmailAddresses = org.EmailAddresses;

            string Location = null;
            if (div != null)
                Location = List[0].org.Location;
            else if (org != null)
                Location = org.Location;

            var subject = Util.PickFirst(EmailSubject, "no subject");
            var message = Util.PickFirst(EmailMessage, "no message");
            message = message.Replace("{first}", p0.PreferredName);
            message = message.Replace("{tickets}", List[0].ntickets.ToString());
            message = message.Replace("{division}", DivisionName);
            message = message.Replace("{org}", OrganizationName);
            message = message.Replace("{location}", Location);
            message = message.Replace("{cmshost}", Util.CmsHost);
            message = message.Replace("{details}", details.ToString());
            message = message.Replace("{paid}", amtpaid.ToString("c"));
            message = message.Replace("{participants}", details.ToString());
            if (amtdue > 0)
                message = message.Replace("{paylink}", "<a href='{0}'>Click this link to pay balance of {1:C}</a>.".Fmt(paylink, amtdue));
            else
                message = message.Replace("{paylink}", "You have a zero balance.");

            var smtp = Util.Smtp();
            Util.Email(smtp, EmailAddresses, emails, subject, message);
            foreach (var p in List)
                Util.Email(smtp, p.person.EmailAddress, p.org.EmailAddresses, "{0}".Fmt(Header),
@"{0} has registered for {1}<br/>Feepaid: {2:C}<br/>AmountDue: {3:C}
<pre>{4}</pre>"
               .Fmt(p.person.Name, Header, p.AmountToPay(), p.AmountDue(), p.PrepareSummaryText()));
        }
        public void UseCoupon(string TransactionID)
        {
            string matchcoupon = @"Coupon\((?<coupon>.*)\)";
            if (Regex.IsMatch(TransactionID, matchcoupon, RegexOptions.IgnoreCase))
            {
                var match = Regex.Match(TransactionID, matchcoupon, RegexOptions.IgnoreCase);
                var coup = match.Groups["coupon"];
                var coupon = "";
                if (coup != null)
                    coupon = coup.Value.Replace(" ", "");
                if (coupon != "Admin")
                {
                    var c = DbUtil.Db.Coupons.Single(cp => cp.Id == coupon);
                    c.RegAmount = Amount();
                    c.Used = DateTime.Now;
                    c.PeopleId = List[0].PeopleId.Value;
                }
            }
        }
        public void ConfirmManageSubscriptions()
        {
            var p = List[0];
            if (p.IsNew)
                p.AddPerson(null, EntryPointForDiv());
            if (p.CreatingAccount == true)
                p.CreateAccount();
            p.SendOneTimeLink(
                ManageSubsModel.StaffEmail(divid.Value),
                Util.ServerLink("/OnlineReg/ManageSubscriptions/"));
        }
        public int EntryPointForDiv()
        {
            var q = from o in DbUtil.Db.Organizations
                    where o.DivOrgs.Any(dd => dd.DivId == divid)
                    where o.RegistrationTypeId != (int)Organization.RegistrationEnum.None
                    where o.EntryPointId > 0
                    select o.EntryPointId;
            return q.FirstOrDefault() ?? 0;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("orgid: {0}<br/>\n", this.orgid);
            sb.AppendFormat("divid: {0}<br/>\n", this.divid);
            sb.AppendFormat("userid: {0}<br/>\n", this.UserPeopleId);
            foreach (var li in List)
            {
                sb.AppendFormat("--------------------------------\nList: {0}<br/>\n", li.index);
                sb.Append(li.ToString());
            }
            return sb.ToString();
        }
    }
}