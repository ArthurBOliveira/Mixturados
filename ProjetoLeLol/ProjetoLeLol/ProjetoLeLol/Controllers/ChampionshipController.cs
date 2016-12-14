using ProjetoLeLol.BLL;
using ProjetoLeLol.Models;
using ProjetoLeLol.Utility;
using ProjetoLeLol.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoLeLol.Controllers
{
    public class ChampionshipController : Controller
    {
        private ChampionshipBLL _championshipBLL = new ChampionshipBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        public ActionResult View1()
        {
            return View("view1");
        }

        //
        // GET: /Championship/Current
        public ActionResult Current(int id_league)
        {
            Championship c = _championshipBLL.ChampionshipSelectCurrentByLeague(id_league);

            if (c.Type == ChampionshipType.X1)
            {
                ChampProfileVM cpvm = new ChampProfileVM(_sessionHelper.CurrentPlayer, c);

                if (_sessionHelper.CurrentPlayer.Id == 0)
                    return View("profilex1", "_Master", cpvm);
                else
                    return View("profilex1", "_Master2", cpvm);
            }
            else
            {
                ChampProfileVM cpvm = new ChampProfileVM(_sessionHelper.CurrentPlayer, c);

                if (_sessionHelper.CurrentPlayer.Id == 0)
                    return View("profile", "_Master", cpvm);
                else
                    return View("profile", "_Master2", cpvm);
            }
        }

        //
        // GET: /Championship/Profile
        public ActionResult Profiles(int id_champ)
        {
            Championship c = _championshipBLL.ChampionshipSelect(id_champ);

            ChampProfileVM cpvm = new ChampProfileVM(_sessionHelper.CurrentPlayer, c);

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("profile", "_Master", cpvm);
            else
                return View("profile", "_Master2", cpvm);
        }

        //
        // GET: /Championship/ProfileX1
        public ActionResult ProfilesX1(int id_champ)
        {
            Championship c = _championshipBLL.ChampionshipSelect(id_champ);

            ChampProfileVM cpvm = new ChampProfileVM(_sessionHelper.CurrentPlayer, c);

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("profilex1", "_Master", cpvm);
            else
                return View("profilex1", "_Master2", cpvm);
        }

        //
        // GET: /Championship/GamesList
        public ActionResult GamesList(int id_champ)
        {
            Championship c = _championshipBLL.ChampionshipSelect(id_champ);
            ChampGamesVM cgvm = new ChampGamesVM();
            cgvm.CurrentPlayer = _sessionHelper.CurrentPlayer;

            if (c.Started)
            {
                List<List<Game>> games = new List<List<Game>>();

                for (int i = c.NumberOfRounds; i > 0; i--)
                    games.Add(_championshipBLL.ChampionshipGameSelect(id_champ, i));

                cgvm.Games = games;
                cgvm.Champ = c;
            }
            else
            {
                cgvm.Teams = _championshipBLL.ChampionshipTeamList(id_champ);
                if (c.Type == ChampionshipType.Pago)
                    cgvm.TempTeams = _championshipBLL.ChampionshipTeamListTemporary(id_champ);
                cgvm.Champ = c;
            }

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("GameList", "_Master", cgvm);
            else
                return View("GameList", "_Master2", cgvm);
        }

        //
        // GET: /Championship/GamesX1List
        public ActionResult GamesX1List(int id_champ)
        {
            Championship c = _championshipBLL.ChampionshipSelect(id_champ);
            ChampGamesX1VM cgvm = new ChampGamesX1VM();
            cgvm.CurrentPlayer = _sessionHelper.CurrentPlayer;

            if (c.Started)
            {
                List<List<GameX1>> games = new List<List<GameX1>>();

                for (int i = c.NumberOfRounds; i > 0; i--)
                    games.Add(_championshipBLL.ChampionshipGameX1Select(id_champ, i));

                cgvm.GamesX1 = games;
                cgvm.Champ = c;
            }
            else
            {
                cgvm.Players = _championshipBLL.ChampionshipPlayersList(id_champ);
                cgvm.TempPlayers = _championshipBLL.ChampionshipPlayersListTemporary(id_champ);
                cgvm.Champ = c;
            }

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("GameX1List", "_Master", cgvm);
            else
                return View("GameX1List", "_Master2", cgvm);
        }

        //
        // GET: /Championship/Create
        public ActionResult Create()
        {
            if (_sessionHelper.CurrentPlayer.Admin)
                return View("Create");
            else
                return View("Error");
        }

        //
        // GET: /Championship/Edit
        public ActionResult Edit()
        {
            return View("Edit");
        }

        //
        // GET: /Championship/Show
        public ActionResult Show(int id_league)
        {
            ShowChampX5 sc5 = new ShowChampX5();
            sc5.League = _championshipBLL.LeagueSelectById(id_league);
            sc5.LeagueTeams = _championshipBLL.LeagueTeamList(sc5.League.Id);
            sc5.Champs = _championshipBLL.ChampionshipListByLeague(sc5.League.Id);

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("show", "_Master", sc5);
            else
                return View("show", "_Master2", sc5);
        }

        //
        // GET: /Championship/Show
        public ActionResult ShowAdmin()
        {
            if (_sessionHelper.CurrentPlayer.Admin)
            {
                List<Championship> c = _championshipBLL.ChampionshipList();

                return View("showAdmin", c);
            }
            else
                return View("error");
        }

        //
        // GET: /Championship/League
        public ActionResult League()
        {
            LeagueVM lvm = new LeagueVM();

            lvm.League = _championshipBLL.LeagueCurrent();
            lvm.LeagueTeams = _championshipBLL.LeagueTeamList(lvm.League.Id);

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("league", "_Master", lvm);
            else
                return View("league", "_Master2", lvm);
        }

        //
        // GET: /Championship/csgo
        public ActionResult csgo()
        {
            return View("csgo", _sessionHelper.CurrentPlayer);
        }

        [HttpPost]
        public JsonResult GameEnd(int id_game, int id_team, string obs)
        {
            bool result = false;

            if (_sessionHelper.CurrentPlayer.Admin)
            {
                result = _championshipBLL.GameEnd(id_game, id_team, obs);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult GameEndPlayer(int id_game)
        {
            bool result = false;

            Game g = _championshipBLL.GameSelect(id_game);

            if (_sessionHelper.CurrentPlayer.Id == g.Team1.Captain.Id || _sessionHelper.CurrentPlayer.Id == g.Team2.Captain.Id)
            {
                int id_team = _sessionHelper.CurrentPlayer.Id == g.Team1.Captain.Id ? g.Team1.Id : g.Team2.Id;

                result = _championshipBLL.GameEnd(id_game, id_team, "Vitória normal entregue pelo vencedor");
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult ChampionshipEndRound(int id_champ)
        {
            bool result = false;
            Championship c = _championshipBLL.ChampionshipSelect(id_champ);

            if (_sessionHelper.CurrentPlayer.Admin)
            {
                result = _championshipBLL.ChampionshipEndRound(c.Id);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult ChampionshipEnd(int id_champ_old, int id_champ_new)
        {
            bool result = false;

            if (_sessionHelper.CurrentPlayer.Admin)
            {
                result = _championshipBLL.ChampionshipEnd(id_champ_old, id_champ_new);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult ChampionshipStart(int id_champ)
        {
            bool result = false;
            Championship c = _championshipBLL.ChampionshipSelect(id_champ);

            if (_sessionHelper.CurrentPlayer.Admin)
            {
                if(c.Type == ChampionshipType.X1)
                    result = _championshipBLL.ChampionshipStartX1(c);
                else
                    result = _championshipBLL.ChampionshipStart(c.Id);
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult TeamEntry()
        {
            int id_champ = int.Parse(Request["id_champ"]);

            Championship c = _championshipBLL.ChampionshipSelect(id_champ);

            ChampProfileVM cpvm = new ChampProfileVM(_sessionHelper.CurrentPlayer, c);

            if (_sessionHelper.CurrentPlayer.Id != 0)
            {
                if (c.Type == ChampionshipType.X1)
                {
                    cpvm.Err = _championshipBLL.ChampionshipPlayerCreate(_sessionHelper.CurrentPlayer, c);
                }
                else
                {
                    if (_sessionHelper.CurrentPlayer.IsCaptain || _sessionHelper.CurrentPlayer.IsSubCaptain)
                    {
                        cpvm.Err = _championshipBLL.ChampionshipTeamCreate(_sessionHelper.CurrentPlayer.Id, c);
                    }
                }

                return View("profile", "_Master2", cpvm);
            }
            else
                return View("profile", "_Master", cpvm);
        }

        [HttpPost]
        public JsonResult CreateChamp(int games, DateTime date, string prize, string actual, string title, int rounds, string entry, string link, string type)
        {
            bool result = false;

            Championship c = new Championship(games, date, prize, actual, title, rounds, entry, link, type);

            if (_sessionHelper.CurrentPlayer.Admin)
                result = _championshipBLL.ChampionshipCreate(c);

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetResult()
        {
            string tst = "hue";

            return Json(tst);
        }
    }
}
