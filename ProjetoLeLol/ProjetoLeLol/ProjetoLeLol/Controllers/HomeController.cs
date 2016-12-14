using Newtonsoft.Json;
using ProjetoLeLol.BLL;
using ProjetoLeLol.Models;
using ProjetoLeLol.Utility;
using ProjetoLeLol.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetoLeLol.Controllers
{
    public class HomeController : Controller
    {
        private PlayerBLL _playerBLL = new PlayerBLL();
        private TeamBLL _teamBLL = new TeamBLL();
        private NewsBLL _newsBLL = new NewsBLL();
        private ChampionshipBLL _champBLL = new ChampionshipBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        public ActionResult Index()
        {
            Championship c = _champBLL.ChampionshipSelectCurrentByLeague(1);
            Championship cx1 = _champBLL.ChampionshipSelectCurrentx1();
            List<Team> tm = _teamBLL.TeamMatcherList();
            List<Player> pm = _playerBLL.PlayerMatcherList();

            HomeVM hvm = new HomeVM(c, cx1, tm, pm);

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("index", "_Master", hvm);
            else
            {
                if (_sessionHelper.CurrentPlayer.Admin)
                    return View("index", "_MasterAdmin", hvm);
                else
                    return View("index", "_Master2", hvm);
            }            
        }

        public ActionResult About()
        {
            return View("About");            
        }

        public ActionResult Temp()
        {
            return View("Temp");
        }

        public ActionResult Vakinha()
        {
            return View("Vakinha");
        }

        public ActionResult Support()
        {
            return View("Support");
        }

        public ActionResult Test()
        {
            Championship c = _champBLL.ChampionshipSelectCurrentByLeague(1);

            HomeVM hvm = new HomeVM();
            hvm.CurrentChampionship = c;

            return View("Test", hvm);
        }

        [HttpPost]
        public ActionResult PlayerExit()
        {
            _sessionHelper.CurrentPlayer = null;

            return Json(true);
        }

        [HttpPost]
        public ActionResult TempIn()
        {
            string login = Request["txtLogin"];
            string senha = Request["txtSenha"];

            if (login == "Vp&9J#cY^+Qjc6qpbz%g" && senha == "6FMD7$&r2mTru5Ke!dAs")
            {
                _sessionHelper.Temp = true;

                return View("index");
            }

            return View("temp");
        }
    }
}