using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using CmsData;
using CmsData.Registration;
using CmsWeb.Models;
using Elmah;
using UtilityExtensions;
using System.Collections.Generic;
using CmsData.Codes;

namespace CmsWeb.Areas.OnlineReg.Controllers
{
	[ValidateInput(false)]
	public partial class OnlineRegController : CmsController
	{
#if DEBUG
		private int INT_timeout = 1600000;
#else
        private int INT_timeout = DbUtil.Db.Setting("RegTimeout", "180000").ToInt();
#endif

		// Main page
		public ActionResult Index(int? id, bool? testing, int? o, int? d, string email, bool? nologin, bool? login, string registertag, bool? showfamily)
		{
#if DEBUG
			var om = DbUtil.Db.OrganizationMembers.SingleOrDefault(mm => mm.OrganizationId == 89469 && mm.PeopleId == 828612);
			if (om != null)
			{
				om.Drop(DbUtil.Db, false);
				DbUtil.Db.SubmitChanges();
			}
#endif
			Util.NoCache(Response);
			if (!id.HasValue)
				return Content("no organization");
			var m = new OnlineRegModel { orgid = id };
			if (m.org == null && m.masterorg == null)
				return Content("invalid registration");

			if (m.masterorg != null)
			{
				if (!OnlineRegModel.UserSelectClasses(m.masterorg).Any())
					return Content("no classes available on this org");
			}
			else if (m.org != null)
			{
				if ((m.org.RegistrationTypeId ?? 0) == RegistrationTypeCode.None)
					return Content("no registration allowed on this org");
			}
			m.URL = Request.Url.OriginalString;

			SetHeaders(m);

#if DEBUG
			m.username = "trecord";
			m.testing = true;
#else
            m.testing = testing;
#endif
			if (Util.ValidEmail(email) || login != true)
				m.nologin = true;

			if (m.nologin)
				m.CreateList();
			else
				m.List = new List<OnlineRegPersonModel>();

			if (Util.ValidEmail(email))
				m.List[0].email = email;

			var pid = 0;
			if (registertag.HasValue())
			{
				var guid = registertag.ToGuid();
				if (guid == null)
					return Content("invalid link");
				var ot = DbUtil.Db.OneTimeLinks.SingleOrDefault(oo => oo.Id == guid.Value);
				if (ot == null)
					return Content("invalid link");
#if DEBUG
#else
                if (ot.Used)
                    return Content("link used");
#endif
				if (ot.Expires.HasValue && ot.Expires < DateTime.Now)
					return Content("link expired");
				var a = ot.Querystring.Split(',');
				pid = a[1].ToInt();
				m.registertag = registertag;
			}
			else if (User.Identity.IsAuthenticated)
			{
				pid = Util.UserPeopleId ?? 0;
			}

			if (pid > 0)
			{
				//m.List = new List<OnlineRegPersonModel>();
				m.UserPeopleId = pid;
				OnlineRegPersonModel p = null;
				if (showfamily != true)
				{
					p = m.LoadExistingPerson(pid, 0);
					p.ValidateModelForFind(ModelState, m);
					p.LoggedIn = true;
					if (m.masterorg == null)
					{
						if (m.List.Count == 0)
							m.List.Add(p);
						else
							m.List[0] = p;
					}
				}
				if (!ModelState.IsValid)
					return View(m);

				if (m.masterorg != null && m.masterorg.RegistrationTypeId == RegistrationTypeCode.ManageSubscriptions2)
				{
					TempData["ms"] = m.UserPeopleId;
					return Redirect("/OnlineReg/ManageSubscriptions/{0}".Fmt(m.masterorgid));
				}
				if (m.org != null && m.org.RegistrationTypeId == RegistrationTypeCode.ManageGiving)
				{
					TempData["mg"] = m.UserPeopleId;
					return Redirect("/OnlineReg/ManageGiving/{0}".Fmt(m.orgid));
				}
				if (m.org != null && m.org.RegistrationTypeId == RegistrationTypeCode.OnlinePledge)
				{
					TempData["mp"] = m.UserPeopleId;
					return Redirect("/OnlineReg/ManagePledge/{0}".Fmt(m.orgid));
				}
				if (m.org != null && m.org.RegistrationTypeId == RegistrationTypeCode.ChooseVolunteerTimes)
				{
					TempData["ps"] = m.UserPeopleId;
					return Redirect("/OnlineReg/ManageVolunteer/{0}".Fmt(m.orgid));
				}
				if (m.org != null && m.org.RegistrationTypeId == RegistrationTypeCode.SpecialJavascript)
				{
					TempData["ps"] = m.UserPeopleId;
					return Redirect("/OnlineReg/SpecialRegistration/{0}".Fmt(m.orgid));
				}
				if (showfamily != true && p.org != null && p.Found == true)
				{
					p.IsFilled = p.org.OrganizationMembers.Count() >= p.org.Limit;
					if (p.IsFilled)
						ModelState.AddModelError(m.GetNameFor(mm => mm.List[0].Found), "Sorry, but registration is closed.");
					if (p.Found == true)
						p.FillPriorInfo();
					CheckSetFee(m, p);
					m.History.Add("index, pid={0}, !showfamily, p.org, found=true".Fmt(pid));
					return View(m);
				}
				m.History.Add("index, pid=" + pid);
				return View(m);
			}
			m.History.Add("index");
			return View(m);
		}
		// authenticate user
		[HttpPost]
		public ActionResult Login(OnlineRegModel m)
		{
			var ret = AccountModel.AuthenticateLogon(m.username, m.password, Session, Request);
			if (ret is string)
			{
				ModelState.AddModelError("authentication", ret.ToString());
				return FlowList(m, "Login");
			}
			Session["OnlineRegLogin"] = true;
			var user = ret as User;
			if (m.orgid == Util.CreateAccountCode)
				return Content("/Person/Index/" + Util.UserPeopleId);

			m.CreateList();
			m.UserPeopleId = user.PeopleId;

			if (m.ManagingSubscriptions())
			{
				TempData["ms"] = Util.UserPeopleId;
				return Content("/OnlineReg/ManageSubscriptions/{0}".Fmt(m.masterorgid));
			}
			if (m.ChoosingSlots())
			{
				TempData["ps"] = Util.UserPeopleId;
				return Content("/OnlineReg/ManageVolunteer/{0}".Fmt(m.orgid));
			}
			if (m.OnlinePledge())
			{
				TempData["mp"] = Util.UserPeopleId;
				return Content("/OnlineReg/ManagePledge/{0}".Fmt(m.orgid));
			}
			if (m.ManageGiving())
			{
				TempData["mg"] = Util.UserPeopleId;
				return Content("/OnlineReg/ManageGiving/{0}".Fmt(m.orgid));
			}
			if (m.UserSelectsOrganization())
				m.List[0].ValidateModelForFind(ModelState, m);
			m.List[0].LoggedIn = true;
			m.History.Add("login");
			return FlowList(m, "Login");
		}
		// Register without logging in
		[HttpPost]
		public ActionResult NoLogin(OnlineRegModel m)
		{
			m.nologin = true;
			m.CreateList();
			m.History.Add("nologin");
			return FlowList(m, "NoLogin");
		}
		[HttpPost]
		public ActionResult YesLogin(OnlineRegModel m)
		{
			m.History.Add("yeslogin");
			m.nologin = false;
			m.List = new List<OnlineRegPersonModel>();
#if DEBUG
			m.username = "trecord";
#endif
			return FlowList(m, "NoLogin");
		}
		[HttpPost]
		public ActionResult Register(int id, OnlineRegModel m)
		{
			m.History.Add("Register");
			int index = m.List.Count - 1;
			if (m.List[index].classid.HasValue)
				m.classid = m.List[index].classid;
			var p = m.LoadExistingPerson(id, index);
			p.ValidateModelForFind(ModelState, m, selectfromfamily: true);
			if (!ModelState.IsValid)
				return FlowList(m, "Register");
			m.List[index] = p;
			if (p.ManageSubscriptions() && p.Found == true)
			{
				//p.OtherOK = true;
				return FlowList(m, "Register");
			}
			if (p.org != null && p.Found == true)
			{
				p.IsFilled = p.org.OrganizationMembers.Count() >= p.org.Limit;
				if (p.IsFilled)
					ModelState.AddModelError(m.GetNameFor(mm => mm.List[m.List.IndexOf(p)].Found), "Sorry, but registration is closed.");
				if (p.Found == true)
					p.FillPriorInfo();
				//if (!p.AnyOtherInfo())
				//p.OtherOK = true;
				return FlowList(m, "Register");
			}
			if (p.ShowDisplay() && p.org != null && p.ComputesOrganizationByAge())
				p.classid = p.org.OrganizationId;
			return FlowList(m, "Register");
		}
		[HttpPost]
		public ActionResult Cancel(int id, OnlineRegModel m)
		{
			m.History.Add("Cancel id=" + id);
			m.List.RemoveAt(id);
			if (m.List.Count == 0)
				m.List.Add(new OnlineRegPersonModel
				{
					orgid = m.orgid,
					masterorgid = m.masterorgid,
					LoggedIn = m.UserPeopleId.HasValue,
#if DEBUG
					first = "Another",
					last = "Child",
					dob = "12/1/02",
					email = "karen@bvcms.com",
#endif
				});
			return FlowList(m, "Cancel");
		}
		[HttpPost]
		public ActionResult ShowMoreInfo(int id, OnlineRegModel m)
		{
			m.History.Add("ShowMoreInfo id=" + id);
			DbUtil.Db.SetNoLock();
			var p = m.List[id];
			p.ValidateModelForFind(ModelState, m);
			if (p.org != null && p.Found == true)
			{
				p.IsFilled = p.org.OrganizationMembers.Count() >= p.org.Limit;
				if (p.IsFilled)
					ModelState.AddModelError(m.GetNameFor(mm => mm.List[id].dob), "Sorry, but registration is closed.");
				if (p.Found == true)
					p.FillPriorInfo();
				return FlowList(m, "ShowMoreInfo");
			}
			if (!p.whatfamily.HasValue && (id > 0 || p.LoggedIn == true))
			{
				ModelState.AddModelError(m.GetNameFor(mm => mm.List[id].whatfamily), "Choose a family option");
				return FlowList(m, "ShowMoreInfo");
			}
			switch (p.whatfamily)
			{
				case 1:
					var u = DbUtil.Db.LoadPersonById(m.UserPeopleId.Value);
					p.address = u.PrimaryAddress;
					p.city = u.PrimaryCity;
					p.state = u.PrimaryState;
					p.zip = u.PrimaryZip.FmtZip();
					break;
				case 2:
					var pb = m.List[id - 1];
					p.address = pb.address;
					p.city = pb.city;
					p.state = pb.state;
					p.zip = pb.zip;
					break;
				default:
#if DEBUG
					p.address = "235 Riveredge Cv.";
					p.city = "Cordova";
					p.state = "TN";
					p.zip = "38018";
					p.gender = 1;
					p.married = 10;
					p.homephone = "9017581862";
#endif
					break;
			}
			p.ShowAddress = true;
			return FlowList(m, "ShowMoreInfo");
		}
		[HttpPost]
		public ActionResult PersonFind(int id, OnlineRegModel m)
		{
			m.History.Add("PersonFind id=" + id);
			if (id >= m.List.Count)
				return FlowList(m, "PersonFind");
			DbUtil.Db.SetNoLock();
			var p = m.List[id];
			if (p.IsValidForNew)
				return ErrorResult(m, new Exception("Unexpected onlinereg state: IsValidForNew is true and in PersonFind"), "PersonFind, unexpected state");

			if (p.classid.HasValue)
			{
				m.orgid = p.classid;
				m.classid = p.classid;
				p.orgid = p.classid;
			}
			p.PeopleId = null;
			p.ValidateModelForFind(ModelState, m);
			if (p.Found == true && m.org != null)
			{
				var setting = settings[m.org.OrganizationId];
				if (setting.AllowReRegister)
				{
					var om = m.org.OrganizationMembers.SingleOrDefault(mm => mm.PeopleId == p.PeopleId);
					if (om != null)
					{
						m.ConfirmReregister();
						DbUtil.Db.SubmitChanges();
						ViewData["email"] = m.List[0].person.EmailAddress;
						ViewData["orgname"] = m.org.OrganizationName;
						ViewData["timeout"] = INT_timeout;
						return View("ConfirmReregister");
					}
				}
			}
			if (p.ManageSubscriptions()
				 || p.OnlinePledge()
				 || p.ManageGiving()
				 || m.ChoosingSlots())
//				 || m.org.RegistrationTypeId == RegistrationTypeCode.SpecialJavascript)
			{
				p.OtherOK = true;
			}
			else if (p.org != null)
			{
				p.IsFilled = p.org.OrganizationMembers.Count() >= p.org.Limit;
				if (p.IsFilled)
					ModelState.AddModelError(m.GetNameFor(mm => mm.List[id].dob), "Sorry, but registration is closed.");
				if (p.Found == true)
					p.FillPriorInfo();
			}
			if (p.org != null && p.ShowDisplay() && p.ComputesOrganizationByAge())
				p.classid = p.org.OrganizationId;

			CheckSetFee(m, p);

			return FlowList(m, "PersonFind");
		}

		private ActionResult ErrorResult(OnlineRegModel m, Exception ex, string errorDisplay)
		{
			var d = new ExtraDatum { Stamp = Util.Now };
			d.Data = Util.Serialize<OnlineRegModel>(m);
			DbUtil.Db.ExtraDatas.InsertOnSubmit(d);
			DbUtil.Db.SubmitChanges();
			var ex2 = new Exception("{0}, {2}".Fmt(errorDisplay, d.Id, Util.ServerLink("/OnlineReg/RegPeople/") + d.Id), ex);
			ErrorSignal.FromCurrentContext().Raise(ex2);
			TempData["error"] = errorDisplay;
			return Content("/Error/");
		}

		// Set suggested giving fee for an indidividual person
		private static void CheckSetFee(OnlineRegModel m, OnlineRegPersonModel p)
		{
			if (m.OnlineGiving() && p.setting.ExtraValueFeeName.HasValue())
			{
				var f = CmsWeb.Models.OnlineRegPersonModel.Funds().SingleOrDefault(ff => ff.Text == p.setting.ExtraValueFeeName);
				var evamt = p.person.GetExtra(p.setting.ExtraValueFeeName).ToDecimal();
				if (f != null && evamt > 0)
					p.FundItem[f.Value.ToInt()] = evamt;
			}
		}
		[HttpPost]
		public ActionResult SubmitNew(int id, OnlineRegModel m)
		{
			m.History.Add("SubmitNew id=" + id);
			var p = m.List[id];
			p.ValidateModelForNew(ModelState);

			if (ModelState.IsValid)
			{
				if (m.ManagingSubscriptions())
				{
					p.IsNew = true;
					m.ConfirmManageSubscriptions();
					ViewData["ManagingSubscriptions"] = true;
					ViewData["CreatedAccount"] = m.List[0].CreatingAccount;
					DbUtil.Db.SubmitChanges();
					ViewData["email"] = m.List[0].person.EmailAddress;
					ViewData["orgname"] = m.masterorg.OrganizationName;
					ViewData["URL"] = m.URL;
					ViewData["timeout"] = INT_timeout;
					return View("ConfirmManageSub");
				}
				if (m.OnlinePledge())
				{
					p.IsNew = true;
					m.ConfirmManagePledge();
					ViewData["CreatedAccount"] = m.List[0].CreatingAccount;
					DbUtil.Db.SubmitChanges();
					ViewData["email"] = m.List[0].person.EmailAddress;
					ViewData["orgname"] = m.org.OrganizationName;
					ViewData["URL"] = m.URL;
					ViewData["timeout"] = INT_timeout;
					SetHeaders(m);
					return View("ConfirmManagePledge");
				}
				if (m.ManageGiving())
				{
					p.IsNew = true;
					m.ConfirmManageGiving();
					ViewData["CreatedAccount"] = m.List[0].CreatingAccount;
					DbUtil.Db.SubmitChanges();
					ViewData["email"] = m.List[0].person.EmailAddress;
					ViewData["orgname"] = m.org.OrganizationName;
					ViewData["URL"] = m.URL;
					ViewData["timeout"] = INT_timeout;
					SetHeaders(m);
					return View("ConfirmManageGiving");
				}
				if (p.org == null && p.ComputesOrganizationByAge())
					ModelState.AddModelError(m.GetNameFor(mm => mm.List[id].Found), "Sorry, cannot find an appropriate age group");
				else if (!p.ManageSubscriptions())
				{
					p.IsFilled = p.org.OrganizationMembers.Count() >= p.org.Limit;
					if (p.IsFilled)
						ModelState.AddModelError(m.GetNameFor(mm => mm.List[id].dob), "Sorry, that age group is filled");
				}
				p.IsNew = true;
			}
			p.IsValidForExisting = ModelState.IsValid == false;
			if (p.IsNew)
				p.FillPriorInfo();
			if (p.org != null && p.ShowDisplay() && p.ComputesOrganizationByAge())
				p.classid = p.org.OrganizationId;
			//if (!p.AnyOtherInfo())
			//    p.OtherOK = ModelState.IsValid;
			return FlowList(m, "SubmitNew");
		}
		[HttpPost]
		public ActionResult SubmitOtherInfo(int id, OnlineRegModel m)
		{
			m.History.Add("SubmitOtherInfo id=" + id);
			if (m.List.Count <= id)
				return Content("<p style='color:red'>error: cannot find person on submit other info</p>");
			m.List[id].ValidateModelForOther(ModelState);
			return FlowList(m, "SubmitOtherInfo");
		}
		[HttpPost]
		public ActionResult AddAnotherPerson(OnlineRegModel m)
		{
			m.History.Add("AddAnotherPerson");
			m.ParseSettings();
			if (!ModelState.IsValid)
				return FlowList(m, "AddAnotherPerson");
#if DEBUG2
            m.List.Add(new OnlineRegPersonModel
            {
                divid = m.divid,
                orgid = m.orgid,
                masterorgid = m.masterorgid,
                first = "Bethany",
                last = "Carroll",
                //bmon = 1,
                //bday = 29,
                //byear = 1987,
                dob = "1/29/87",
                email = "davcar@pobox.com",
                phone = "9017581862".FmtFone(),
                LoggedIn = m.UserPeopleId.HasValue,
            });
#else
			m.List.Add(new OnlineRegPersonModel
			{
				orgid = m.orgid,
				masterorgid = m.masterorgid,
				LoggedIn = m.UserPeopleId.HasValue,
			});
#endif
			return FlowList(m, "AddAnotherPerson");
		}
		[HttpPost]
		public ActionResult AskDonation(OnlineRegModel m)
		{
			m.History.Add("AskDonation");
			if (m.List.Count == 0)
				return Content("Can't find any registrants");
			RemmoveLastRegistrantIfEmpty(m);
			SetHeaders(m);
			return View(m);
		}

		private static void RemmoveLastRegistrantIfEmpty(OnlineRegModel m)
		{
			if (!m.last.IsNew && !m.last.Found == true)
				m.List.Remove(m.last);
			if (!(m.last.IsValidForNew || m.last.IsValidForExisting))
				m.List.Remove(m.last);
		}

		[HttpPost]
		public ActionResult CompleteRegistration(OnlineRegModel m)
		{
			m.History.Add("CompleteRegistration");
			if (m.AskDonation() && !m.donor.HasValue && m.donation > 0)
			{
				SetHeaders(m);
				ModelState.AddModelError("donation",
					 "Please indicate a donor or clear the donation amount");
				return View("AskDonation", m);
			}

			if (m.List.Count == 0)
				return Content("Can't find any registrants");

			RemmoveLastRegistrantIfEmpty(m);

			var d = new ExtraDatum { Stamp = Util.Now };
			d.Data = Util.Serialize<OnlineRegModel>(m);
			DbUtil.Db.ExtraDatas.InsertOnSubmit(d);
			DbUtil.Db.SubmitChanges();
			DbUtil.LogActivity("Online Registration: {0} ({1})".Fmt(m.Header, d.Id));

			if (m.PayAmount() == 0 && (m.donation ?? 0) == 0 && !m.Terms.HasValue())
				return RedirectToAction("Confirm",
					 new
					 {
						 id = d.Id,
						 TransactionID = "zero due",
					 });

			var terms = Util.PickFirst(m.Terms, "");
			if (terms.HasValue())
				ViewData["Terms"] = terms;

			SetHeaders(m);
			if (m.PayAmount() == 0 && m.Terms.HasValue())
			{
				return View("Terms", new PaymentModel
					 {
						 Terms = m.Terms,
						 _URL = m.URL,
						 _timeout = INT_timeout,
						 PostbackURL = Util.ServerLink("/OnlineReg/Confirm/" + d.Id),
					 });
			}

			ViewBag.timeout = INT_timeout;
			ViewBag.Url = m.URL;

			var om =
				 DbUtil.Db.OrganizationMembers.SingleOrDefault(
					  mm => mm.OrganizationId == m.orgid && mm.PeopleId == m.List[0].PeopleId);
			m.ParseSettings();

			if (om != null && m.settings[m.orgid.Value].AllowReRegister == false)
			{
				return Content("You are already registered it appears");
			}


			var pf = PaymentForm.CreatePaymentForm(m);
			pf.DatumId = d.Id;
			pf.FormId = Guid.NewGuid();
			if (OnlineRegModel.GetTransactionGateway() == "serviceu")
				return View("Payment", pf);
			return View("ProcessPayment", pf);
		}
		[HttpPost]
		public JsonResult CityState(string id)
		{
			var z = DbUtil.Db.ZipCodes.SingleOrDefault(zc => zc.Zip == id);
			if (z == null)
				return Json(null);
			return Json(new { city = z.City.Trim(), state = z.State });
		}
		private Dictionary<int, Settings> _settings;
		public Dictionary<int, Settings> settings
		{
			get
			{
				if (_settings == null)
					_settings = HttpContext.Items["RegSettings"] as Dictionary<int, Settings>;
				return _settings;
			}
		}
		public class CurrentRegistration
		{
			public string OrgName { get; set; }
			public int OrgId { get; set; }
			public string RegType { get; set; }
		}
		public ActionResult Current()
		{
			var q = from o in DbUtil.Db.Organizations
					  where o.LastMeetingDate == null || o.LastMeetingDate < DateTime.Today
					  where o.RegistrationTypeId > 0
					  where o.OrganizationStatusId == 30
					  where !(o.RegistrationClosed ?? false)
					  select new { o.FullName2, o.OrganizationId, o.RegistrationTypeId, o.LastMeetingDate, o.OrgPickList };
			var list = q.ToList();
			var q2 = from i in list
						where !list.Any(ii => (ii.OrgPickList ?? "0").Split(',').Contains(i.OrganizationId.ToString()))
						orderby i.OrganizationId
						select new CurrentRegistration
								 {
									 OrgName = i.FullName2,
									 OrgId = i.OrganizationId,
									 RegType = RegistrationTypeCode.Lookup(i.RegistrationTypeId)
								 };
			return View(q2);
		}
		public ActionResult Timeout(string ret)
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			ViewBag.Url = ret;
			return View();
		}

		private ActionResult FlowList(OnlineRegModel m, string function)
		{
			try
			{
				var view = ViewExtensions2.RenderPartialViewToString2(this, "Flow/List", m);
				return Content(view);
			}
			catch (Exception ex)
			{
				return ErrorResult(m, ex, "In " + function + ex.Message);
			}
		}
	}
}
