using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.DAL
{
    public class BaseDAL
    {
        public string connectionString;
        public MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnUolProducao"].ConnectionString);

    }
}