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
    public class EmailController : Controller
    {
        private PlayerBLL _playerBLL = new PlayerBLL();

        //
        // GET: /Email/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string subject, string body)
        {
            bool result = false;

            EmailHelper eh = new EmailHelper();

            /*List<Player> players = _playerBLL.PlayerAvailableSelect();

            foreach (Player p in players)
            {
                result = eh.SendEmail(p.Email, subject, body);
            }*/

            result = eh.SendEmail("arthurboliveira93@gmail.com", subject, body);

            return Json(result);
        }
    }
}
