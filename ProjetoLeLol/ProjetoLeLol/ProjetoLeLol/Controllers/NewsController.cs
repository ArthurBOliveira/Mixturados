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
    public class NewsController : Controller
    {
        private NewsBLL _newsBLL = new NewsBLL();
        private SessionHelper _sessionHelper = new SessionHelper(System.Web.HttpContext.Current.Session);

        //
        // GET: /News/
        public ActionResult Create()
        {
            if (_sessionHelper.CurrentPlayer.Admin)
                return View("index");
            else
                return View("Error");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(string title, string content, string link, string img)
        {
            if (_sessionHelper.CurrentPlayer.Admin)
            {
                News n = new News(title, DateTime.Now, content, link, img);

                bool result = _newsBLL.NewsCreate(n, _sessionHelper.CurrentPlayer.Nick);

                return Json(result);
            }
            else
                return View("Error");            
        }

        [HttpGet]
        public ActionResult Profile(int id_news)
        {
            News n = new News();

            n = _newsBLL.NewSelect(id_news);

            if (_sessionHelper.CurrentPlayer.Id == 0)
                return View("Profile", "_Master", n);
            else
                return View("Profile", "_Master2", n);
        }
    }
}
