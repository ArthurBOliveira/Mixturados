using MySql.Data.MySqlClient;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.DAL
{
    public class NoLagDAL : BaseDAL
    {
        public bool NoLagCreate(NoLag nl)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_no_lag_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", nl.Id_player));
            cmd.Parameters.Add(new MySqlParameter("p_skype", nl.Skype));
            cmd.Parameters.Add(new MySqlParameter("p_name", nl.Name));
            cmd.Parameters.Add(new MySqlParameter("p_email", nl.Email));
            cmd.Parameters.Add(new MySqlParameter("p_date_requested", DateTime.Now));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                result = true;

            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}