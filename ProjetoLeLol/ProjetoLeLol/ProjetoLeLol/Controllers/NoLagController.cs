using ProjetoLeLol.BLL;
using ProjetoLeLol.Models;
using ProjetoLeLol.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoLeLol.Controllers
{
    public class NoLagController : Controller
    {
        private NoLagBLL _noLagBLL = new NoLagBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        //
        // GET: /NoLag/

        public ActionResult Index()
        {
            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("error");
            else
                return View();
        }

        public ActionResult Confirmed()
        {
            return View("confirmed");
        }

        [HttpPost]
        public JsonResult Create()
        {
            bool result = false;

            if (_sessionHelper.CurrentPlayer.Id != 0)
            {
                NoLag nl = new NoLag(_sessionHelper.CurrentPlayer.Id, _sessionHelper.CurrentPlayer.Skype, _sessionHelper.CurrentPlayer.Name, _sessionHelper.CurrentPlayer.Email, DateTime.Now);

                result = _noLagBLL.NoLagCreate(nl);
            }

            return Json(result);
        }
    }
}