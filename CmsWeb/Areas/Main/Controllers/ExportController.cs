﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CmsWeb.Models;
using CmsData;
using UtilityExtensions;
using System.Linq.Dynamic;

namespace CmsWeb.Areas.Main.Controllers
{
    public class ExportController : Controller
    {
        public ActionResult UpdatePeople(int id)
        {
            return new UpdatePeopleModel(id);
        }
        public ActionResult MucketyMap(int id)
        {
            return new MucketyMapResult(id);
        }
        public ActionResult QueryBits(int id)
        {
            return new QueryBitsExcelResult(id);
        }
        [Authorize(Roles="Finance")]
        [Authorize(Roles="Admin")]
        public ActionResult Contributions(int id, string start, string end, bool? totals)
        {
            return new ContributionsExcelResult(id, start, end, totals ?? false);
        }
    }
}