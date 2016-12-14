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
    public class NotificationController : Controller
    {
        private NotificationBLL _notifBLL = new NotificationBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        //
        // GET: /Notification/
        public ActionResult Index()
        {
            List<Notification> notifs = _notifBLL.NotificationList(_sessionHelper.CurrentPlayer);

            return View(notifs);
        }

        [HttpPost]
        public JsonResult Answer(int id, bool answer)
        {
            bool result = false;

            Notification n = _notifBLL.NotificationSelect(id);

            if (n.Receiver.Id == _sessionHelper.CurrentPlayer.Id)
            {
                result = _notifBLL.NotificationAnswer(n, answer);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult Del(int id)
        {
            bool result = false;

            Notification n = _notifBLL.NotificationSelect(id);

            if (n.Receiver.Id == _sessionHelper.CurrentPlayer.Id)
            {
                result = _notifBLL.NotificationDelete(n.Id);
            }

            return Json(result);
        }

    }
}
