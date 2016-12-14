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
    public class TeamController : HomeController
    {
        private TeamBLL _teamBLL = new TeamBLL();
        private NotificationBLL _notificationBLL = new NotificationBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        //
        // GET: /Team/Create
        public ActionResult Create()
        {
            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("error");
            else
            {
                if (_teamBLL.TeamPlayerHasTeam(_sessionHelper.CurrentPlayer.Id))
                    return RedirectToAction("Profile", "Team");
                else
                {
                    ErrorView err = new ErrorView();

                    TeamCreateVM tcvm = new TeamCreateVM(err);

                    return View("Create", "_Master2", tcvm);
                }
            }
        }

        //
        // GET: /Team/Show
        public ActionResult Show()
        {
            List<Team> teams = new List<Team>();

            teams = _teamBLL.TeamAvailableSelect();

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("Show", "_Master", teams);
            else
                return View("Show", "_Master2", teams);
        }

        //
        // GET: /Team/Edit
        public ActionResult Edit()
        {
            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("error");
            else
            {
                Team t;

                if (_sessionHelper.CurrentPlayer.IsCaptain || _sessionHelper.CurrentPlayer.IsSubCaptain)
                    t = _teamBLL.TeamSelectByPlayerCap(_sessionHelper.CurrentPlayer.Id);
                else
                    t = _teamBLL.TeamSelectByPlayerMember(_sessionHelper.CurrentPlayer.Id);

                if (t.Id != 0)
                {
                    TeamEditVM tevm = new TeamEditVM(_sessionHelper.CurrentPlayer, t);

                    return View("Edit", "_Master2", tevm);
                }
                else
                    return RedirectToAction("Create", "Team");
            }
        }

        //
        // GET: /Team/Profile
        public ActionResult Profile()
        {
            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("error");
            else
            {
                Team t;

                if (_sessionHelper.CurrentPlayer.IsCaptain || _sessionHelper.CurrentPlayer.IsSubCaptain)
                    t = _teamBLL.TeamSelectByPlayerCap(_sessionHelper.CurrentPlayer.Id);
                else
                    t = _teamBLL.TeamSelectByPlayerMember(_sessionHelper.CurrentPlayer.Id);

                if (t.Id != 0)
                {
                    t.Players = _teamBLL.TeamListPlayer(t.Id);
                    TeamProfileVM tpvm = new TeamProfileVM(t, _sessionHelper.CurrentPlayer);
                    return View("Profile", "_Master2", tpvm);
                }
                return RedirectToAction("Create", "Team");
            }
        }

        [HttpGet]
        public ActionResult Profiles(int id_team)
        {
            Team t = new Team();

            t = _teamBLL.TeamSelect(id_team);

            t.Players = _teamBLL.TeamListPlayer(t.Id);

            TeamProfileVM tpvm = new TeamProfileVM(t, _sessionHelper.CurrentPlayer);

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("Profile", "_Master", tpvm);
            else
            {
                return View("Profile", "_Master2", tpvm);
            }
        }

        [HttpPost]
        public ActionResult NewTeam()
        {
            ErrorView err = new ErrorView();

            TeamCreateVM tcvm = new TeamCreateVM(err);

            string name = Request["txtName"];
            string tag = Request["txtTag"];
            string page = Request["txtPage"];
            string schedule = Request["selSchedule"];

            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6Lf7tAQTAAAAADkz1pwOVxH04e6WdLDTqRdmVDBf";

            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaValidator>(reply);

            if (captchaResponse.Success != "False")
            {
                if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(tag))
                {
                    if (!_teamBLL.TeamPlayerHasTeam(_sessionHelper.CurrentPlayer.Id))
                    {
                        if (!_teamBLL.TeamNameVerify(name))
                        {
                            if (!_teamBLL.TeamTagVerify(tag))
                            {
                                Team t = new Team(name, tag, page, schedule, DateTime.Now);

                                PlayerBLL p = new PlayerBLL();

                                t.Captain = _sessionHelper.CurrentPlayer;
                                t.SubCaptain = new Player();

                                if (_teamBLL.TeamCreate(t, _sessionHelper.CurrentPlayer.Id))
                                {
                                    err.HasError = true;

                                    err.MsgError = "Time cadastrado com sucesso!";
                                }
                                else
                                {
                                    err.HasError = true;

                                    err.MsgError = "Erro ao criar o time";
                                }
                            }
                            else
                            {
                                err.HasError = true;

                                err.MsgError = "Esta tag já foi cadastrada no nosso sistema";
                            }
                        }
                        else
                        {
                            err.HasError = true;

                            err.MsgError = "Este nome já foi cadastrado no nosso sistema";
                        }
                    }
                    else
                    {
                        err.HasError = true;

                        err.MsgError = "Você já possui um time, danadin!";
                    }
                }
                else
                {
                    err.HasError = true;

                    err.MsgError = "Por favor preencha os campos corretamente";
                }
            }
            else
            {
                err.HasError = true;

                err.MsgError = "Captcha inválido";
            }

            return View("Create", "_Master2", tcvm);
        }

        [HttpPost]
        public ActionResult EditTeam()
        {
            TeamEditVM tevm = new TeamEditVM();

            string name = Request["txtName"];
            string tag = Request["txtTag"];
            string page = Request["txtPage"];
            string schedule = Request["selSchedule"];

            Team t = _teamBLL.TeamSelectByPlayerCap(_sessionHelper.CurrentPlayer.Id);

            tevm.Team = t;
            tevm.CurrentPlayer = _sessionHelper.CurrentPlayer;

            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(tag))
            {
                Team te = new Team(name, tag, page, schedule, DateTime.Now);
                te.Id = t.Id;
                te.Captain = t.Captain;
                te.SubCaptain = t.SubCaptain;

                if (_teamBLL.TeamEdit(te))
                {
                    tevm.Err.HasError = true;
                    tevm.Team = te;
                    tevm.Err.MsgError = "Time editado com sucesso!";
                }
                else
                {
                    tevm.Err.HasError = true;

                    tevm.Err.MsgError = "Erro ao editar o time";
                }
            }
            else
            {
                tevm.Err.HasError = true;

                tevm.Err.MsgError = "Por favor preencha os campos corretamente";
            }

            return View("Edit", "_Master2", tevm);
        }

        [HttpPost]
        //Time pede pro jogador entrar
        public ActionResult PlayerCall(int id_player)
        {
            bool result = false;
            Team t = _teamBLL.TeamSelectByPlayerCap(_sessionHelper.CurrentPlayer.Id);
            if (t.Id != 0)
            {
                bool aux = t.SubCaptain.Id == 0 ? false : true;

                if (!_teamBLL.TeamIsFull(t.Id, aux) && !_teamBLL.TeamPlayerHasTeam(id_player))
                    result = _notificationBLL.TeamCallPlayer(id_player, _sessionHelper.CurrentPlayer.Id, _sessionHelper.CurrentPlayer.Name, t.Name);
            }

            return Json(result);
        }

        [HttpPost]
        //Jogador pede pra participar do time
        public ActionResult TeamCall(int id_team)
        {
            bool result = false;
            Team t = _teamBLL.TeamSelect(id_team);
            if (t.Id != 0)
            {
                bool aux = t.SubCaptain.Id == 0 ? false : true;

                if (!_teamBLL.TeamIsFull(t.Id, aux) && !_teamBLL.TeamPlayerHasTeam(_sessionHelper.CurrentPlayer.Id))
                    result = _notificationBLL.PlayerCallTeam(t.Captain.Id, _sessionHelper.CurrentPlayer.Id, _sessionHelper.CurrentPlayer.Name);
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult PlayerRem(int id_player, int id_team)
        {
            bool result = false;

            Team t = _teamBLL.TeamSelect(id_team);

            if (_sessionHelper.CurrentPlayer.Id == t.Captain.Id || _sessionHelper.CurrentPlayer.Id == t.SubCaptain.Id)
            {
                result = _teamBLL.TeamRemPlayer(id_player);

                if (result)
                    result = _notificationBLL.NotificationCreate(id_player, _sessionHelper.CurrentPlayer, "Você foi removido do seu time atual", NotificationType.alert);
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult TeamDelete(int id_team)
        {
            bool result = false;

            Team t = _teamBLL.TeamSelect(id_team);

            if (_sessionHelper.CurrentPlayer.Id == t.Captain.Id)
            {
                List<Player> players = _teamBLL.TeamListPlayer(t.Id);

                foreach (Player p in players)
                    _notificationBLL.NotificationCreate(p.Id, t.Captain, "Infelizmente seu time foi desfeito, você agora está disponível para entrar em outros times ou criar o seu próprio.", NotificationType.alert);

                result = _teamBLL.TeamDelete(id_team, t.Captain.Id);
                _sessionHelper.CurrentPlayer.IsCaptain = false;
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult TeamPromoteSub(int id_player, int id_team)
        {
            bool result = false;

            Team t = _teamBLL.TeamSelect(id_team);

            if (_sessionHelper.CurrentPlayer.Id == t.Captain.Id && t.SubCaptain.Id != 0)
            {
                result = _teamBLL.TeamPromoteSub(id_player, id_team);

                if (result)
                    result = _notificationBLL.NotificationCreate(id_player, _sessionHelper.CurrentPlayer, "Parabéns, você promovido a sub capitão do seu time!", NotificationType.alert);
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult TeamDemoteSub(int id_player, int id_team)
        {
            bool result = false;

            Team t = _teamBLL.TeamSelect(id_team);

            if (_sessionHelper.CurrentPlayer.Id == t.Captain.Id || _sessionHelper.CurrentPlayer.Id == t.SubCaptain.Id)
            {
                result = _teamBLL.TeamDemoteSub(id_player, id_team);

                if (result)
                {
                    result = _notificationBLL.NotificationCreate(id_player, _sessionHelper.CurrentPlayer, "Infelizmente você foi rebaixado de seu cargo como sub capitão do seu time!", NotificationType.alert);
                    _sessionHelper.CurrentPlayer.IsSubCaptain = false;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult TeamLeave()
        {
            bool result = false;

            Team t;

            if (_sessionHelper.CurrentPlayer.IsCaptain || _sessionHelper.CurrentPlayer.IsSubCaptain)
                t = _teamBLL.TeamSelectByPlayerCap(_sessionHelper.CurrentPlayer.Id);
            else
                t = _teamBLL.TeamSelectByPlayerMember(_sessionHelper.CurrentPlayer.Id);

            if (_sessionHelper.CurrentPlayer.Id != t.Captain.Id && _sessionHelper.CurrentPlayer.Id != t.SubCaptain.Id)
            {
                result = _teamBLL.TeamRemPlayer(_sessionHelper.CurrentPlayer.Id);

                if (result)
                {
                    result = _notificationBLL.NotificationCreate(t.Captain.Id, _sessionHelper.CurrentPlayer, "Infelizmente o jogador " + _sessionHelper.CurrentPlayer.Name + " saiu do seu time!", NotificationType.alert);
                    result = _notificationBLL.NotificationCreate(t.SubCaptain.Id, _sessionHelper.CurrentPlayer, "Infelizmente o jogador " + _sessionHelper.CurrentPlayer.Name + " saiu do seu time!", NotificationType.alert);
                }
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult TeamSelfDemote()
        {
            bool result = false;

            Team t = _teamBLL.TeamSelectByPlayerCap(_sessionHelper.CurrentPlayer.Id);

            if (_sessionHelper.CurrentPlayer.Id == t.SubCaptain.Id)
            {
                result = _teamBLL.TeamSelfDemote(_sessionHelper.CurrentPlayer.Id, t.Id);
                if (result)
                    result = _notificationBLL.NotificationCreate(t.Captain.Id, _sessionHelper.CurrentPlayer, "Infelizmente o jogador " + _sessionHelper.CurrentPlayer.Name + " abandonou o cargo de sub capitão", NotificationType.alert);
            }

            return Json(result);
        }
    }
}