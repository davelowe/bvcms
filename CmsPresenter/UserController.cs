/* Author: David Carroll
 * Copyright (c) 2008, 2009 Bellevue Baptist Church 
 * Licensed under the GNU General Public License (GPL v2)
 * you may not use this code except in compliance with the License.
 * You may obtain a copy of the License at http://bvcms.codeplex.com/license 
 */
using System;
using System.Web.Security;
using System.Collections.Generic;
using System.ComponentModel;
using UtilityExtensions;
using System.Linq;
using CmsData;

namespace CMSPresenter
{
    [DataObject(true)]
    public class UserController
    {
        private CMSDataContext Db;
        public UserController()
        {
            Db = new CMSDataContext(Util.ConnectionString);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void Delete(int UserId)
        {
            var user = Db.Users.SingleOrDefault(u => u.UserId == UserId);
            if (user == null)
                return;
            Db.UserCanEmailFors.DeleteAllOnSubmit(user.UsersICanEmailFor);
			Db.UserCanEmailFors.DeleteAllOnSubmit(user.UsersICanEmailFor);
			Db.Preferences.DeleteAllOnSubmit(user.Preferences);
            Db.ActivityLogs.DeleteAllOnSubmit(user.ActivityLogs);
			foreach (var f in user.VolunteerFormsUploaded)
				f.UploaderId = null;
            Db.SubmitChanges();
            Membership.DeleteUser(user.Username, true);
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(bool IsApproved, bool MustChangePassword, bool IsLockedOut, int PeopleId, string EmailAddress, string Username, string PasswordSetOnly, int UserId)
        {
            var user = Db.Users.Single(u => u.UserId == UserId);
            if (PasswordSetOnly.HasValue())
            {
                CMSMembershipProvider.provider.AdminOverride = true;
                var mu = CMSMembershipProvider.provider.GetUser(user.Username, false);
                mu.UnlockUser();
                mu.ChangePassword(mu.ResetPassword(), PasswordSetOnly);
                user.TempPassword = PasswordSetOnly;
                CMSMembershipProvider.provider.AdminOverride = false;
            }
            user.Username = Username;
            user.EmailAddress = EmailAddress;
            user.IsApproved = IsApproved;
            user.MustChangePassword = MustChangePassword;
            if (user.IsLockedOut ^ IsLockedOut)
                user.LastLockedOutDate = DateTime.Now;
            user.IsLockedOut = IsLockedOut;
            if (PeopleId > 0)
                user.PeopleId = PeopleId;
            Db.SubmitChanges();
        }
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(bool IsApproved, bool MustChangePassword, bool IsLockedOut, int PeopleId, string EmailAddress, string Username, string PasswordSetOnly)
        {
            int? pid = null;
            if (PeopleId > 0)
                pid = PeopleId;

            CMSMembershipProvider.provider.AdminOverride = true;
            var user = CMSMembershipProvider.provider.NewUser(
                Username,
                PasswordSetOnly,
                EmailAddress,
                IsApproved,
                pid);
            CMSMembershipProvider.provider.AdminOverride = false;
            user.MustChangePassword = MustChangePassword;
            DbUtil.Db.Users.InsertOnSubmit(user);
            DbUtil.Db.SubmitChanges();
        }

        private int count;
        public int Count(string name, string sortExpression, int startIndex, int maximumRows)
        {
            return count;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IEnumerable<User> GetUsers(string name, string sortExpression, int startIndex, int maximumRows)
        {
            var q = from u in Db.Users
                    select u;

            if (name.HasValue())
                q = q.Where(u => u.Name2.StartsWith(name));
            count = q.Count();

            if (!sortExpression.HasValue())
                sortExpression = "IsOnLine";

            switch (sortExpression)
            {
                case "CreationDate":
                    q = q.OrderBy(u => u.CreationDate);
                    break;
                case "Name":
                    q = q.OrderBy(u => u.Name2);
                    break;
                case "Host":
                    q = q.OrderBy(u => u.Host);
                    break;
                case "LastActivityDate":
                    q = q.OrderBy(u => u.LastActivityDate);
                    break;
                case "IsApproved":
                    q = q.OrderBy(u => u.IsApproved);
                    break;
                case "Username":
                    q = q.OrderBy(u => u.Username);
                    break;
                case "MustChangePassword":
                    q = q.OrderBy(u => u.MustChangePassword);
                    break;
                case "CreationDate DESC":
                    q = q.OrderByDescending(u => u.CreationDate);
                    break;
                case "Name DESC":
                    q = q.OrderByDescending(u => u.Name2);
                    break;
                case "Host DESC":
                    q = q.OrderByDescending(u => u.Host);
                    break;
                case "LastActivityDate DESC":
                case "IsOnLine":
                case "IsOnLine DESC":
                    q = q.OrderByDescending(u => u.LastActivityDate);
                    break;
                case "IsApproved DESC":
                    q = q.OrderByDescending(u => u.IsApproved);
                    break;
                case "Username DESC":
                    q = q.OrderByDescending(u => u.Username);
                    break;
                case "MustChangePassword DESC":
                    q = q.OrderByDescending(u => u.MustChangePassword);
                    break;
            }
            q = q.Skip(startIndex).Take(maximumRows);

            return q;
        }
    }
}