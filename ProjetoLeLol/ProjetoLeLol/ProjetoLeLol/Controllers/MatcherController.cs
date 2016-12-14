using ProjetoLeLol.BLL;
using ProjetoLeLol.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoLeLol.Controllers
{
    public class MatcherController : Controller
    {
        private TeamBLL _teamBLL = new TeamBLL();

        //
        // GET: /Matcher/
        public ActionResult Index()
        {
            MatcherVM mvm = new MatcherVM();

            mvm.Teams = _teamBLL.TeamMatcherList();

            return View();
        }

    }
}
