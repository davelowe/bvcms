using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using CmsData;
using CmsWeb.Code;
using Dapper;
using UtilityExtensions;

namespace CmsWeb.Areas.People.Models
{
    public class ContactModel : IValidatableObject
    {
        private bool? canViewComments;
        private string _incomplete;

        [NoUpdate]
        public int ContactId { get; set; }
        public DateTime ContactDate { get; set; }

        [DisplayName("Attempted/Not Available")]
        public bool NotAtHome { get; set; }
        [DisplayName("Left Note Card")]
        public bool LeftDoorHanger { get; set; }
        public bool LeftMessage { get; set; }
        public bool ContactMade { get; set; }
        public bool GospelShared { get; set; }
        [DisplayName("Prayer Request Received")]
        public bool PrayerRequest { get; set; }
        public bool GiftBagGiven { get; set; }

        [UIHint("TextArea")]
        public string Comments { get; set; }
        public string LimitToRole { get; set; }

        public CodeInfo ContactType { get; set; }
        public CodeInfo ContactReason { get; set; }
        public CodeInfo Ministry { get; set; }

        internal Contact contact;
        private void LoadContact(int id)
        {
            var u = DbUtil.Db.CurrentUser;
            var roles = u.UserRoles.Select(uu => uu.Role.RoleName.ToLower()).ToArray();
            var ManagePrivateContacts = HttpContext.Current.User.IsInRole("ManagePrivateContacts");
            var q = from c in DbUtil.Db.Contacts
                   where (c.LimitToRole ?? "") == "" || roles.Contains(c.LimitToRole) || ManagePrivateContacts
                   where c.ContactId == id
                   select c;
            contact = q.SingleOrDefault();
            if (contact == null)
                return;

            ContactId = id;
            MinisteredTo = new ContacteesModel(id);
            Ministers = new ContactorsModel(id);
            MinisteredTo.CanViewComments = CanViewComments;
            Ministers.CanViewComments = CanViewComments;
        }

        public ContactModel()
        {
        }
        public ContactModel(int id)
            : this()
        {
            LoadContact(id);
            if(contact != null)
                this.CopyPropertiesFrom(contact);
        }

        public bool CanView;
        public ContacteesModel MinisteredTo { get; set; }
        public ContactorsModel Ministers { get; set; }

        public void UpdateContact()
        {
            LoadContact(ContactId);
            this.CopyPropertiesTo(contact);
            DbUtil.Db.SubmitChanges();
        }
        public static void DeleteContact(int cid)
        {
            var cn = new SqlConnection(Util.ConnectionString);
            cn.Open();
            cn.Execute(@"
                delete contactees where ContactId = @cid;
                delete contactors where ContactId = @cid;
                update task set CompletedContactId = NULL WHERE CompletedContactId = @cid;
                update task set SourceContactId = NULL WHERE CompletedContactId = @cid;
                delete contact where ContactId = @cid;
                ", new { cid });
        }

        public int AddNewTeamContact()
        {
            var c = new Contact
            {
                ContactDate = DateTime.Now.Date,
                MinistryId = contact.MinistryId,
                CreatedBy = Util.UserId1,
                CreatedDate = DateTime.Now,
                ContactTypeId = contact.ContactTypeId,
                ContactReasonId = contact.ContactReasonId,
            };
            var q = from cor in DbUtil.Db.Contactors
                    where cor.ContactId == contact.ContactId
                    select cor;
            foreach (var p in q)
                c.contactsMakers.Add(new Contactor { PeopleId = p.PeopleId });
            DbUtil.Db.Contacts.InsertOnSubmit(c);
            DbUtil.Db.SubmitChanges();
            return c.ContactId;
        }

        public bool CanViewComments
        {
            get
            {
                if (canViewComments.HasValue)
                    return canViewComments.Value;

                if (!Util2.OrgLeadersOnly)
                {
                    canViewComments = true;
                    return true;
                }

                var q = from c in DbUtil.Db.Contactees
                        where c.ContactId == ContactId
                        select c.PeopleId;
                var q2 = from c in DbUtil.Db.Contactors
                         where c.ContactId == ContactId
                         select c.PeopleId;
                var a = q.Union(q2).ToArray();

                Tag tag = DbUtil.Db.OrgLeadersOnlyTag2();
                canViewComments = tag.People(DbUtil.Db).Any(p => a.Contains(p.PeopleId));
                return canViewComments.Value;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Ministry.Value == "0")
                results.Add(ModelError("Ministry is required", "MinistryId"));

            if (ContactType.Value == "0")
                results.Add(ModelError("ContactType is required", "ContactTypeId"));

            if (ContactReason.Value == "0")
                results.Add(ModelError("ContactReason is required", "ContactReasonId"));

            return results;
        }
        private static ValidationResult ModelError(string message, string field)
        {
            return new ValidationResult(message, new[] { field });
        }

        public string Incomplete
        {
            get
            {
                if (_incomplete == null)
                    _incomplete = GetIncomplete();
                return _incomplete;
            }
        }

        private string GetIncomplete()
        {
            if(contact == null)
                LoadContact(ContactId);
            var sb = new StringBuilder();
            Append(Ministry.Value == "0", sb, "no ministry");
            Append(ContactType.Value == "0", sb, "no type");
            Append(ContactReason.Value == "0", sb, "no reason");
            Append(Ministers.Count() == 0, sb, "no contactors");
            Append(MinisteredTo.Count() == 0, sb, "no contactees");
            if (sb.Length > 0)
                return sb.ToString();
            return "";
        }
        private void Append(bool tf, StringBuilder sb, string text)
        {
            if (!tf)
                return;
            if (sb.Length > 0)
                sb.Append(", ");
            sb.Append(text);
        }
    }
}