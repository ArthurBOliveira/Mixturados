using Newtonsoft.Json;
using ProjetoLeLol.BLL;
using ProjetoLeLol.Models;
using ProjetoLeLol.Utility;
using ProjetoLeLol.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ProjetoLeLol.Controllers
{
    public class PlayerController : Controller
    {
        private PlayerBLL _playerBLL = new PlayerBLL();
        private NotificationBLL _notifBLL = new NotificationBLL();
        private TeamBLL _teamBLL = new TeamBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        //
        // GET: /Player/Create
        public ActionResult Create()
        {
            //Temp
            //if (_sessionHelper.Temp)
            //{
            ErrorView err = new ErrorView();

            PlayerVM pvm = new PlayerVM(err);

            return View("Create", pvm);
            //}
            //else
            //return RedirectToAction("Temp", "Home");
        }

        //
        // GET: /Player/Edit
        public ActionResult Edit()
        {
            //Temp
            //if (_sessionHelper.Temp)
            //{
            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("error");
            else
            {
                PlayerEditVM pevm = new PlayerEditVM(_sessionHelper.CurrentPlayer);

                return View("Edit", "_Master2", pevm);
            }
            //}
            //else
            //    return RedirectToAction("Temp", "Home");
        }

        //
        // GET: /Player/Profile
        public ActionResult Profile()
        {
            ////Temp
            //if (_sessionHelper.Temp)
            //{
            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("error");
            else
            {
                Team t = new Team();

                if (_sessionHelper.CurrentPlayer.IsCaptain || _sessionHelper.CurrentPlayer.IsSubCaptain)
                    t = _teamBLL.TeamSelectByPlayerCap(_sessionHelper.CurrentPlayer.Id);
                else
                    t = _teamBLL.TeamSelectByPlayerMember(_sessionHelper.CurrentPlayer.Id);

                PlayerProfileVM ppvm = new PlayerProfileVM(_sessionHelper.CurrentPlayer, _sessionHelper.CurrentPlayer, t);

                DateTime today = DateTime.Today;
                int age = today.Year - _sessionHelper.CurrentPlayer.Birthday.Year;
                if (_sessionHelper.CurrentPlayer.Birthday > today.AddYears(-age)) age--;

                ppvm.Age = age.ToString() + " anos";

                return View("Profile", "_Master2", ppvm);
            }
            //}
            //else
            //    return RedirectToAction("Temp", "Home");
        }

        //
        // GET: /Player/Confirmation
        public ActionResult Confirmation()
        {
            //Temp
            //if (_sessionHelper.Temp)
            return View("Confirmation");
            //else
            //    return RedirectToAction("Temp", "Home");
        }

        //
        // GET: /Player/Login
        public ActionResult Login()
        {
            //Temp
            //if (_sessionHelper.Temp)
            //{
            ErrorView err = new ErrorView();

            return View("Login", "_Master3", err);
            //}
            //else
            //    return RedirectToAction("Temp", "Home");
        }

        //
        // GET: /Player/Show
        public ActionResult Show()
        {
            //Temp
            //if (_sessionHelper.Temp)
            //{
            List<Player> players = new List<Player>();

            players = _playerBLL.PlayerAvailableSelect();

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("Show", "_Master", players);
            else
                return View("Show", "_Master2", players);
            //}
            //else
            //    return RedirectToAction("Temp", "Home");
        }

        //
        // GET: /Player/ChangePass
        public ActionResult ChangePass()
        {
            //if (_sessionHelper.Temp)
            //{
            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("error");
            else
            {
                return View("ChangePass", "_Master2");
            }
            //}
            //else
            //    return RedirectToAction("Temp", "Home");
        }

        //
        // GET: /Player/ChangePass
        public ActionResult Forget()
        {
            return View("ForgetPass");
        }

        [HttpGet]
        public ActionResult Profiles(int id_player)
        {
            Player p = _playerBLL.PlayerSelect(id_player);
            Team t = new Team();

            if (p.IsCaptain || p.IsSubCaptain)
                t = _teamBLL.TeamSelectByPlayerCap(p.Id);
            else
                t = _teamBLL.TeamSelectByPlayerMember(p.Id);

            PlayerProfileVM ppvm = new PlayerProfileVM(_sessionHelper.CurrentPlayer, p, t);

            DateTime today = DateTime.Today;
            int age = today.Year - p.Birthday.Year;
            if (p.Birthday > today.AddYears(-age)) age--;

            ppvm.Age = age.ToString() + " anos";

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("Profile", "_Master", ppvm);
            else
                return View("Profile", "_Master2", ppvm);
        }

        [HttpPost]
        public ActionResult NewPlayer()
        {
            bool result = false;
            ErrorView err = new ErrorView();

            string name = Request["txtName"];
            string nick = Request["txtNick"];
            string email = Request["txtEmail"];
            string password = Request["txtPassword"];
            string conf = Request["txtConf"];
            string birthday = Request["txtDate"];
            string skype = Request["txtSkype"];
            string img = Request["txtImg"];
            string role1 = Request["selRole1"];
            string role2 = Request["selRole2"];
            string state = Request["selState"];
            string schedule = Request["selSchedule"];
            string schedule2 = Request["selSchedule2"];
            string champ = Request["selChamp"];

            img = img == "" ? "/Images/porosemfundo.png" : img;

            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6Lf7tAQTAAAAADkz1pwOVxH04e6WdLDTqRdmVDBf";

            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaValidator>(reply);

            if (captchaResponse.Success != "False")
            {
                if (Validate(name, nick, email, password, conf, birthday, skype))
                {
                    if (password == conf)
                    {
                        if (password.IndexOf(':') == -1)
                        {
                            if (!_playerBLL.PlayerVerifyEmail(email))
                            {
                                Player p = new Player(name, nick, password, birthday, email, skype, role1, role2, champ, state, schedule, schedule2, img);

                                result = _playerBLL.PlayerCreate(p);
                                if (!result)
                                {
                                    err.HasError = true;
                                    err.MsgError = "O nick que voce colocou já foi cadastrado anteriormente, se você é o dono dele, entre em contato conosco através deste email: contato@mixturadoslol.com.br e resolveremos isso o mais breve possível!";
                                }
                                else
                                {
                                    err.HasError = true;
                                    err.MsgError = "Usuário Cadastrado com sucesso! Você já pode efetuar o login agora.";
                                }
                            }
                            else
                            {
                                err.HasError = true;
                                err.MsgError = "Este email já foi cadastrado no nosso sistema";
                            }
                        }
                        else
                        {
                            err.HasError = true;
                            err.MsgError = "Senha não pode possuir o caractere ':'";
                        }
                    }
                    else
                    {
                        err.HasError = true;
                        err.MsgError = ViewBag.PlayerCreateError = "Senhas não conferem";
                    }
                }
                else
                {
                    err.HasError = true;
                    err.MsgError = ViewBag.PlayerCreateError = "Por favor preencha os campos corretamente";
                }
            }
            else
            {
                err.HasError = true;
                err.MsgError = ViewBag.PlayerCreateError = "Captcha inválido";
            }
            PlayerVM pvm = new PlayerVM(err);

            return View("Create", pvm);
        }

        [HttpPost]
        public ActionResult EditPlayer(string name, string birthday, string skype, string role1, string role2, string champ, string state, string schedule, string schedule2, string img)
        {
            img = img == "" ? "/Images/porosemfundo.png" : img;

            Player p = new Player(name, "", "", birthday, "", skype, role1, role2, champ, state, schedule, schedule2, img);

            p.Id = _sessionHelper.CurrentPlayer.Id;

            bool result = _playerBLL.PlayerEdit(p);

            _sessionHelper.CurrentPlayer.Name = p.Name;
            _sessionHelper.CurrentPlayer.Email = p.Email;
            _sessionHelper.CurrentPlayer.Birthday = p.Birthday;
            _sessionHelper.CurrentPlayer.Skype = p.Skype;
            _sessionHelper.CurrentPlayer.Role1 = p.Role1;
            _sessionHelper.CurrentPlayer.Role2 = p.Role2;
            _sessionHelper.CurrentPlayer.Champion = p.Champion;
            _sessionHelper.CurrentPlayer.Img = img;

            return Json(result);
        }

        [HttpPost]
        public JsonResult ForgetPassword(string oldPass, string newPass)
        {
            bool result = false;

            if (newPass.IndexOf(':') == -1)
            {
                result = _playerBLL.PlayerChangePassword(_sessionHelper.CurrentPlayer.Id, oldPass, newPass);
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult PlayerLogin()
        {
            ErrorView err = new ErrorView();

            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6Lf7tAQTAAAAADkz1pwOVxH04e6WdLDTqRdmVDBf";

            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaValidator>(reply);

            if (captchaResponse.Success != "False")
            {
                string login = Request["txtLogin"];
                string password = Request["txtPassword"];

                if (!String.IsNullOrEmpty(login) && !String.IsNullOrEmpty(password))
                {
                    _sessionHelper.CurrentPlayer = null;

                    _sessionHelper.CurrentPlayer = _playerBLL.PlayerLogin(login, password);

                    if (_sessionHelper.CurrentPlayer.Id != 0)
                    {
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        err.HasError = true;

                        err.MsgError = "Login ou senha incorretos";
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

            return View("Login", err);
        }

        [HttpPost]
        public JsonResult PlayerCheckNotif()
        {
            bool result = false;

            result = _notifBLL.NotificationHasNew(_sessionHelper.CurrentPlayer.Id);

            return Json(result);
        }

        [HttpPost]
        public JsonResult PlayerStatistics()
        {
            bool result = false;

            if (_sessionHelper.CurrentPlayer.Admin == true)
                result = _playerBLL.PlayerStatisticsUploadAll();

            return Json(result);
        }

        [HttpPost]
        public JsonResult PlayerUpdateNick()
        {
            bool result = false;

            if (_sessionHelper.CurrentPlayer.Admin == true)
                result = _playerBLL.PlayerNickUpdateAll();

            return Json(result);
        }

        [HttpPost]
        public JsonResult Forget(string subject)
        {
            bool result = false;

            result = _playerBLL.PlayerForgetPassword(subject);

            return Json(result);
        }

        private bool Validate(string name, string nick, string email, string password, string conf, string birthday, string skype)
        {
            bool result = true;

            if (String.IsNullOrEmpty(name))
            {
                result = false;
            }
            if (String.IsNullOrEmpty(nick))
            {
                result = false;
            }
            if (String.IsNullOrEmpty(email))
            {
                result = false;
            }
            if (String.IsNullOrEmpty(password))
            {
                result = false;
            }
            if (String.IsNullOrEmpty(conf))
            {
                result = false;
            }
            DateTime aux;
            if (!DateTime.TryParse(birthday, out aux))
            {
                result = false;
            }
            if (String.IsNullOrEmpty(skype))
            {
                result = false;
            }
            return result;
        }
    }
}
