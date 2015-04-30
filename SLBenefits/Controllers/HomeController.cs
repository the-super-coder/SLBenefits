using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SLBenefits.Core.Helper;
using SLBenefits.Core.Service;
using SLBenefits.Core;
using SLBenefits.Core;
namespace SLBenefits.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

    }
}