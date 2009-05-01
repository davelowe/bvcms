using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Net.Mail;
using BellevueTeachers;

namespace BTeaData
{
    public partial class Group
    {
        private const string CONTEXTUSER = "ContextUser";
        private const string BLOGGER = "Blogger";
        private const string ADMIN = "Admin";
        private const string MEMBER = "Member";

        public static User ContextUser
        {
            get
            {
                if (HttpContext.Current.Items.Contains(CONTEXTUSER))
                    return HttpContext.Current.Items[CONTEXTUSER] as User;
                else
                    return Util.CurrentUser;
            }
            set
            {
                if (value == null)
                    HttpContext.Current.Items.Remove(CONTEXTUSER);
                else
                    HttpContext.Current.Items[CONTEXTUSER] = value;
            }
        }

        public static Group LoadById(int id)
        {
            return DbUtil.Db.Groups.SingleOrDefault(m => m.Id == id);
        }
        public static Group LoadByName(string name)
        {
            return DbUtil.Db.Groups.SingleOrDefault(m => m.Name == name);
        }
        public static Group LoadByRole(string role)
        {
            try
            {
                string groupid = Regex.Match(role, "R(\\d+)-.*", RegexOptions.Multiline).Groups[1].Value;
                int id = int.Parse(groupid);
                return DbUtil.Db.Groups.SingleOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Bad role name for group", ex);
            }
        }
        public void AddInvitation(string passcode)
        {
            var i = new Invitation();
            i.Password = passcode;
            i.Expires = DateTime.Now.AddMonths(2);
            Invitations.Add(i);
        }

        public bool HasWelcomeText
        {
            get { return ContentId.HasValue; }
        }

        public void SetAdmin(User user, bool value)
        {
            SetRole(user, ADMIN, value);
        }
        public bool IsAdmin
        {
            get { return IsUserAdmin(ContextUser); }
        }
        public bool IsUserAdmin(User user)
        {
            return IsUserInRole(user, ADMIN);
        }
        public void SetBlogger(User user, bool value)
        {
            SetRole(user, BLOGGER, value);
        }
        public bool IsBlogger
        {
            get { return IsUserBlogger(ContextUser); }
        }
        public bool IsUserBlogger(User user)
        {
            return IsUserInRole(user, BLOGGER);
        }
        public void SetMember(User user, bool value)
        {
            SetRole(user, MEMBER, value);
        }
        public bool IsMember
        {
            get { return IsUserMember(ContextUser); }
        }
        public bool IsUserMember(User user)
        {
            return IsUserInRole(user, MEMBER);
        }
        public bool IsUserInRole(User user, string role)
        {
            return GetRoleUser(user, role) != null;
        }
        public UserRole GetRoleUser(User user, string role)
        {
            var q = from ru in DbUtil.Db.UserRoles
                    where ru.UserId == user.UserId && ru.Role.RoleName == role
                    where ru.Role.GroupId == this.Id
                    select ru;
            return q.SingleOrDefault();
        }
        public static Content GetNewWelcome()
        {
            var w = ContentService.GetContent("default2_welcome");
            var welcome = new Content();
            welcome.Body = w.Body;
            welcome.ContentName = "groupwelcometext";
            welcome.CreatedBy = HttpContext.Current.User.Identity.Name;
            welcome.CreatedOn = DateTime.Now;
            welcome.Title = w.Title;
            DbUtil.Db.Contents.InsertOnSubmit(welcome);
            return welcome;
        }
        public static void InsertWithRolesOnSubmit(string name)
        {
            var g = new Group();
            g.Name = name;
            g.CreateRole(MEMBER);
            g.CreateRole(ADMIN);
            g.CreateRole(BLOGGER);
            g.WelcomeText = GetNewWelcome();
            DbUtil.Db.Groups.InsertOnSubmit(g);
        }
        private Role GetRole(string name)
        {
            return Roles.SingleOrDefault(r => r.RoleName == name);
        }
        private void CreateRole(string name)
        {
            if (GetRole(name) != null)
                return;
            var role = new Role();
            role.RoleName = name;
            this.Roles.Add(role);
        }
        public void DeleteWithRoleOnSubmit()
        {
            foreach (var r in Roles)
                DbUtil.Db.UserRoles.DeleteAllOnSubmit(r.UserRoles);
            DbUtil.Db.Roles.DeleteAllOnSubmit(Roles);
            DbUtil.Db.Invitations.DeleteAllOnSubmit(this.Invitations);
            DbUtil.Db.Contents.DeleteOnSubmit(this.WelcomeText);
            DbUtil.Db.Groups.DeleteOnSubmit(this);
        }
        private void AddUserToRole(User user, string role)
        {
            var r = GetRole(role);
            var ru = new UserRole();
            ru.UserId = user.UserId;
            r.UserRoles.Add(ru);
        }
        private void RemoveUserFromRole(User user, string role)
        {
            var ru = GetRoleUser(user, role);
            DbUtil.Db.UserRoles.DeleteOnSubmit(ru);
        }
        private void SetRole(User user, string rolename, bool value)
        {
            bool inrole = IsUserInRole(user, rolename);
            if (value && !inrole)
                AddUserToRole(user, rolename);
            else if (!value && inrole)
                RemoveUserFromRole(user, rolename);
        }
        public static IEnumerable<Group> FetchAllGroups()
        {
            return FetchGroups(GroupType.Member, FetchType.AllGroups);
        }
        public static IEnumerable<Group> FetchAllGroupsWhereAdmin()
        {
            return from g in FetchAllGroups()
                   where g.IsAdmin || HttpContext.Current.User.IsInRole("Administrator")
                   select g;
        }
        public static IEnumerable<Group> FetchUserGroups()
        {
            return FetchGroups(GroupType.Member, FetchType.UserOnly);
        }
        public static IEnumerable<Group> FetchAdminGroups()
        {
            return FetchGroups(GroupType.Admin, FetchType.UserOnly);
        }
        private static IEnumerable<Group> FetchGroups(GroupType gtype, FetchType FetchType)
        {
            if (FetchType == FetchType.AllGroups)
                return new GroupController().FetchAll();
            string role = GroupPostfix(gtype);
            bool isadmin = HttpContext.Current.User.IsInRole("Administrator");
            return from g in DbUtil.Db.Groups
                   where isadmin
                       || g.Roles.Any(r => r.RoleName == role
                           && r.UserRoles.Any(ur => ur.UserId == ContextUser.UserId))
                   select g;
        }
        public static IEnumerable<int> FetchIdsForUser()
        {
            return FetchIds(GroupType.Member);
        }
        public static IEnumerable<int> FetchIdsForAdmin()
        {
            return FetchIds(GroupType.Admin);
        }
        private static IEnumerable<int> FetchIds(GroupType gtype)
        {
            return FetchGroups(gtype, FetchType.UserOnly).Select(g => g.Id);
        }

        private static string GroupPostfix(GroupType type)
        {
            switch (type)
            {
                case GroupType.Member:
                    return MEMBER;
                case GroupType.Admin:
                    return ADMIN;
                case GroupType.Blogger:
                    return BLOGGER;
                default:
                    return MEMBER;
            }
        }
        public static IEnumerable<User> GetUsersInGroup(int groupid)
        {
            return from u in DbUtil.Db.Users
                   where u.UserRoles.Any(ur => ur.Role.GroupId == groupid)
                   select u;
        }
        public static IEnumerable<User> GetUsersInRole(string role)
        {
            return from u in DbUtil.Db.Users
                   where u.UserRoles.Any(ur => ur.Role.RoleName == role)
                   select u;
        }
        public IEnumerable<User> GetUsersInRole(GroupType gtype)
        {
            var role = GroupPostfix(gtype);
            return from r in Roles
                   from ru in r.UserRoles
                   where r.RoleName == role
                   select ru.User;
        }
        public void NotifyNewUser(string newuserid)
        {
            var smtp = new SmtpClient();
            var n = 0;
            var u = Util.GetUser(newuserid);
            var subject = "New user in Group: " + Name;
            var body = "<br>--<br>{0}, {1} {2} is a new user in group={3} with id={4} and birthday={5:d}.<br>--<br>"
                    .Fmt(u.EmailAddress, u.FirstName, u.LastName, Name, newuserid, u.BirthDay);
            var from = new MailAddress("bbcms01@bellevue.org");

            foreach (var mu in GetUsersInRole(GroupType.Admin))
            {
                var to = new MailAddress(mu.EmailAddress, mu.FirstName + " " + mu.LastName);
                var msg = new MailMessage(from, to);
                msg.ReplyTo = new MailAddress(u.EmailAddress);
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;
                if (n % 20 == 0)
                    smtp = new SmtpClient();
                n++;
                smtp.Send(msg);
            }
        }
    }
    public enum FetchType
    {
        AllGroups,
        UserOnly
    }

    public class UserGroupInfo
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool IsMember { get; set; }
        public bool IsBlogger { get; set; }
        public bool IsAdmin { get; set; }
    }
    public class GroupController
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public IEnumerable<Group> FetchAll()
        {
            return DbUtil.Db.Groups;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IEnumerable<Group> FetchAllGroups()
        {
            return Group.FetchAllGroups();
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IEnumerable<Group> FetchAllGroupsWhereAdmin()
        {
            return Group.FetchAllGroupsWhereAdmin();
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IEnumerable<Group> FetchUserGroups()
        {
            return Group.FetchUserGroups();
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IEnumerable<Group> FetchAdminGroups()
        {
            return Group.FetchAdminGroups();
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteGroup(string Name)
        {
            var group = DbUtil.Db.Groups.SingleOrDefault(g => g.Name == Name);
            group.DeleteWithRoleOnSubmit();
            DbUtil.Db.SubmitChanges();
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IEnumerable<User> GetUsersInGroup(int id)
        {
            return Group.GetUsersInGroup(id);
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IEnumerable<UserGroupInfo> GetUsers(int groupid)
        {
            var g = Group.LoadById(groupid);
            var q = from u in DbUtil.Db.Users
                    orderby u.LastName, u.FirstName
                    select new UserGroupInfo
                    {
                        Name = u.LastName + ", " + u.FirstName,
                        UserName = u.Username,
                        IsAdmin = g.IsUserAdmin(u),
                        IsBlogger = g.IsUserBlogger(u),
                        IsMember = g.IsUserMember(u)
                    };
            return q;
        }

    }
    public enum GroupType
    {
        Admin,
        Blogger,
        Member
    }
}