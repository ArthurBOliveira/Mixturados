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
    public class AdminController : Controller
    {
        private PlayerBLL _playerBLL = new PlayerBLL();
        private TeamBLL _teamBLL = new TeamBLL();
        private NotificationBLL _notification = new NotificationBLL();
        private ChampionshipBLL _championshipBLL = new ChampionshipBLL();
        private EventBLL _eventBLL = new EventBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);
        
        //
        // GET: /Admin/player
        public ActionResult players()
        {
            if (_sessionHelper.CurrentPlayer.Admin)
            {
                List<Player> p = _playerBLL.PlayerAvailableSelectAdmin();

                return View("players");
            }
            else
                return View("error");
        }

        //
        // GET: /Admin/champs
        public ActionResult champ()
        {
            if (_sessionHelper.CurrentPlayer.Admin)
            {
                List<Game> games = _championshipBLL.ChampionshipGameSelectFullCurrent();

                return View("champ", games);
            }
            else
                return View("error");
        }

        public ActionResult eventCreate()
        {
            if (_sessionHelper.CurrentPlayer.Admin)
                return View("eventCreate", "_MasterAdmin");
            else
                return View("error");
        }

        [HttpPost]
        public JsonResult eventNew(string name, string adress, string date, string link)
        {
            bool result = false;

            if (_sessionHelper.CurrentPlayer.Admin)
            {
                Event e = new Event(name, adress, date, link);

                result = _eventBLL.EventCreate(e);

            }

            return Json(result);
        }
    }
}
