using ProjetoLeLol.DAL;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.BLL
{
    public class NoLagBLL
    {
        private NoLagDAL _noLagDAL = new NoLagDAL();

        public bool NoLagCreate(NoLag nl)
        {
            return _noLagDAL.NoLagCreate(nl);
        }
    }
}