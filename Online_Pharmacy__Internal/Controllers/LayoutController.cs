using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Pharmacy__Internal.Controllers
{
    public class LayoutController : Controller
    {
        public ActionResult GetHeader()
        {
            return PartialView("~/Views/Shared/_HeaderPartialPage.cshtml");
        }

        public ActionResult GetLeftMenu()
        {
            return PartialView("~/Views/Shared/_LeftMenuPartialPage.cshtml");
        }
        public ActionResult GetRightBar()
        {
            return PartialView("~/Views/Shared/_RightBarPartialPage.cshtml");
        }

        public ActionResult GetFooter()
        {
            return PartialView("~/Views/Shared/_FooterPartialPage.cshtml");
        }

    }
}