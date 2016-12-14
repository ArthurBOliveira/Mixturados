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
    public class EventController : Controller
    {
        private EventBLL _eventBLL = new EventBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        //
        // GET: /Event/List

        public ActionResult List()
        {
            List<Event> events = _eventBLL.EventList();

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("List", "_Master", events);
            else
                return View("List", "_Master2", events);
        }

        public ActionResult Vakinha()
        {
            return View("Vakinha");
        }

        public ActionResult MixturaViagem()
        {
            return View("MixturaViagem");
        }

    }
}
