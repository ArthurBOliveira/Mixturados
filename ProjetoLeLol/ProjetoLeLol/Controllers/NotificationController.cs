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
            if (_sessionHelper.CurrentPlayer.Id != 0)
            {
                List<Notification> n = _notifBLL.NotificationList(_sessionHelper.CurrentPlayer);

                //List<Notification> n = new List<Notification>();
                //Notification _n = new Notification();
                //_n.Sender = _sessionHelper.CurrentPlayer;
                //_n.Receiver = _sessionHelper.CurrentPlayer;
                //_n.Text = "Super teste from chaos!!!";
                //_n.Type = NotificationType.teamCalling;
                //n.Add(_n);

                return View(n);
            }
            else
                return View("error");

        }

        [HttpPost]
        public ActionResult AnswerNotif(int id_notif, bool resp)
        {
            Notification n = _notifBLL.NotificationSelect(id_notif);

            if (n.Receiver.Id == _sessionHelper.CurrentPlayer.Id)
            {
                bool result = _notifBLL.NotificationAnswer(n, resp);

                return Json(result);
            }
            else
                return Json(false);            
        }

        [HttpPost]
        public ActionResult DelNotif(int id_notif)
        {
            Notification n = _notifBLL.NotificationSelect(id_notif);

            if (n.Receiver.Id == _sessionHelper.CurrentPlayer.Id)
            {
                bool result = _notifBLL.NotificationDelete(id_notif);

                return Json(result);
            }
            else
                return Json(false);
        }
    }
}
