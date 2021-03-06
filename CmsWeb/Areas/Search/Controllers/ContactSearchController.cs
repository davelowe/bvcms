using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using CmsWeb.Areas.Search.Models;
using UtilityExtensions;

namespace CmsWeb.Areas.Search.Controllers
{
    [RouteArea("Search", AreaUrl = "ContactSearch2")]
    public class ContactSearchController : CmsStaffController
    {
        [GET("ContactSearch2")]
        public ActionResult Index()
        {
            Response.NoCache();
            var m = new ContactSearchModel();
            m.Pager.Set("/ContactSearch2/Results");

            m.GetFromSession();
            return View(m);
        }
        [POST("ContactSearch2/Results/{page?}/{size?}/{sort?}/{dir?}")]
        public ActionResult Results(int? page, int? size, string sort, string dir, ContactSearchModel m)
        {
            m.Pager.Set("/ContactSearch2/Results", page, size, sort, dir);
            m.SaveToSession();
            return View(m);
        }
        [POST("ContactSearch2/Clear")]
        public ActionResult Clear()
        {
            var m = new ContactSearchModel();
            m.ClearSession();
            return Redirect("/ContactSearch2");
        }
        [POST("ContactSearch2/ConvertToQuery")]
        public ActionResult ConvertToQuery(ContactSearchModel m)
        {
            var gid = m.ConvertToQuery();
            return Redirect("/Query/{0}".Fmt(gid));
        }
        [POST("ContactSearch2/ContactTypeQuery/{id:int}")]
        public ActionResult ContactTypeQuery(int id)
        {
            var gid = ContactSearchModel.ContactTypeQuery(id);
            return Redirect("/Query/{0}".Fmt(gid));
        }
        [POST("ContactSearch2/ContactorSummary")]
        public ActionResult ContactorSummary(ContactSearchModel m)
        {
            var q = m.ContactorSummary();
            return new DataGridResult(q);
        }
        [POST("ContactSearch2/ContactSummary")]
        public ActionResult ContactSummary(ContactSearchModel m)
        {
            var q = m.ContactSummary();
            return new DataGridResult(q);
        }

        [POST("ContactSearch2/ContactTypeTotals")]
        public ActionResult ContactTypeTotals(ContactSearchModel m)
        {
            ViewBag.candelete = m.CanDeleteTotal();
            var q = m.ContactTypeTotals();
            return View(q);
        }

        [Authorize(Roles = "Developer")]
        public ActionResult DeleteContactsForType(int id)
        {
            ContactSearchModel.DeleteContactsForType(id);
            return Redirect("/ContactSearch/ContactTypeTotals");
        }
    }
}
