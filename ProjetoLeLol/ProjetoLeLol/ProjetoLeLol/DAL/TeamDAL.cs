using MySql.Data.MySqlClient;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.DAL
{
    public class TeamDAL : BaseDAL
    {
        private PlayerDAL _playerDAL = new PlayerDAL();

        public int TeamCreate(Team t)
        {
            int result = 0;

            MySqlCommand cmd = new MySqlCommand("proc_team_create", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_name", t.Name));
            cmd.Parameters.Add(new MySqlParameter("p_dateCreate", t.DateCreated));
            cmd.Parameters.Add(new MySqlParameter("p_tag", t.Tag));
            cmd.Parameters.Add(new MySqlParameter("p_page", t.Page));
            cmd.Parameters.Add(new MySqlParameter("p_timeTable", t.Schedule.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_captain", t.Captain.Id));
            cmd.Parameters.Add(new MySqlParameter("p_subCaptain", t.SubCaptain.Id));

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
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

        public bool TeamUpdateToCaptain(int id_player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_update_captain", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

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

        public bool TeamDemoteToCaptain(int id_player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_demote_captain", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

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

        public bool teamDelete(int id_team)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_delete", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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

        public bool teamAddPlayer(int id_team, int id_player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_team_create", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));
            cmd.Parameters.Add(new MySqlParameter("p_date_created", DateTime.Now));

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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

        public bool teamRemPlayer(int id_player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_team_rem_player", conn);

            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));
            cmd.Parameters.Add(new MySqlParameter("p_date_ended", DateTime.Now));

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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

        public List<Team> TeamAvailabeSelect()
        {
            MySqlCommand cmd = new MySqlCommand("proc_team_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Team> teams = new List<Team>();
            Team team = new Team();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    team.Id = (int)reader.GetValue(0);
                    team.Name = reader.GetValue(1).ToString();
                    team.DateCreated = (DateTime)reader.GetValue(2);
                    team.Tag = reader.GetValue(3).ToString();
                    team.Page = reader.GetValue(4).ToString();
                    team.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(5).ToString());
                    team.Captain = _playerDAL.PlayerSelect((int)reader.GetValue(6));
                    team.SubCaptain = _playerDAL.PlayerSelect((int)reader.GetValue(7));

                    teams.Add(team);
                    team = new Team();
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return teams;
        }

        public bool TeamEdit(Team t)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_update", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_team", t.Id));
            cmd.Parameters.Add(new MySqlParameter("p_name", t.Name));
            cmd.Parameters.Add(new MySqlParameter("p_tag", t.Tag));
            cmd.Parameters.Add(new MySqlParameter("p_page", t.Page));
            cmd.Parameters.Add(new MySqlParameter("p_timeTable", t.Schedule.ToString()));

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

        public Team TeamSelect(int id_team)
        {
            MySqlCommand cmd = new MySqlCommand("proc_team_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
           
            Team team = new Team();

            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    team.Id = (int)reader.GetValue(0);
                    team.Name = reader.GetValue(1).ToString();
                    team.DateCreated = (DateTime)reader.GetValue(2);
                    team.Tag = reader.GetValue(3).ToString();
                    team.Page = reader.GetValue(4).ToString();
                    team.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(5).ToString());
                    team.Captain = _playerDAL.PlayerSelect((int)reader.GetValue(6));
                    team.SubCaptain = _playerDAL.PlayerSelect((int)reader.GetValue(7));
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return team;
        }

        public Team TeamSelectByPlayerCap(int id_player)
        {
            MySqlCommand cmd = new MySqlCommand("proc_team_select_by_player", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
           
            Team team = new Team();

            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    team.Id = (int)reader.GetValue(0);
                    team.Name = reader.GetValue(1).ToString();
                    team.DateCreated = DateTime.Parse(reader.GetValue(2).ToString());
                    team.Tag = reader.GetValue(3).ToString();
                    team.Page = reader.GetValue(4).ToString();
                    team.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(5).ToString());
                    team.Captain = _playerDAL.PlayerSelect((int)reader.GetValue(6));
                    team.SubCaptain = _playerDAL.PlayerSelect((int)reader.GetValue(7));
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return team;
        }

        public Team TeamSelectByPlayerMember(int id_player)
        {
            MySqlCommand cmd = new MySqlCommand("proc_team_select_by_player_member", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            Team team = new Team();

            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    team.Id = (int)reader.GetValue(0);
                    team.Name = reader.GetValue(1).ToString();
                    team.DateCreated = DateTime.Parse(reader.GetValue(2).ToString());
                    team.Tag = reader.GetValue(3).ToString();
                    team.Page = reader.GetValue(4).ToString();
                    team.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(5).ToString());
                    team.Captain = _playerDAL.PlayerSelect((int)reader.GetValue(6));
                    team.SubCaptain = _playerDAL.PlayerSelect((int)reader.GetValue(7));
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return team;
        }

        public List<Player> TeamListPlayer(int id_team)
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_team_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Player> players = new List<Player>();
            Player player = new Player();

            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    player = _playerDAL.PlayerSelect((int)reader.GetValue(1));

                    players.Add(player);
                    player = new Player();
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return players;
        }

        public bool playerTeamDeleteByTeam(int id_team)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_team_delete_team", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));
            cmd.Parameters.Add(new MySqlParameter("p_date_ended", DateTime.Now));

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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

        public bool TeamPromoteSub(int id_player, int id_team)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_promote", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));            

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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

        public bool TeamDemoteSub(int id_team)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_demote", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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

        public bool TeamPlayerHasTeam(int id_player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_select_by_player", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Player> players = new List<Player>();
            Player player = new Player();

            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = true;
                }
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

        public bool PlayerPromoteSub(int id_player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_promote", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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

        public bool PlayerDemoteSub(int id_player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_demote", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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

        public bool TeamIsFull(int id_team, bool hasSub)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_team_count", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));

            List<Team> teams = new List<Team>();
            Team team = new Team();

            try
            {
                conn.Open();
                int aux = Convert.ToInt32(cmd.ExecuteScalar());
                aux = hasSub ? (aux+1) : aux;
                aux++;
                if (aux >= 7)
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

        public bool TeamCompleted(int id_team, bool hasSub)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_team_count", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));

            try
            {
                conn.Open();
                int aux = Convert.ToInt32(cmd.ExecuteScalar());
                aux = hasSub ? (aux+1) : aux;
                aux++;
                if (aux >= 5)
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

        public bool TeamNameVerify(string name)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_verify_name", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new MySqlParameter("p_name", name));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = true;
                }
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

        public bool TeamTagVerify(string tag)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_verify_tag", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new MySqlParameter("p_tag", tag));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = true;
                }
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

        public List<Team> TeamMatcherList()
        {
            MySqlCommand cmd = new MySqlCommand("proc_team_matcher_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Team> teams = new List<Team>();
            Team team = new Team();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    team.Id = (int)reader.GetValue(0);
                    team.Name = reader.GetValue(1).ToString();
                    team.DateCreated = (DateTime)reader.GetValue(2);
                    team.Tag = reader.GetValue(3).ToString();
                    team.Page = reader.GetValue(4).ToString();
                    team.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(5).ToString());
                    team.Captain = _playerDAL.PlayerSelect((int)reader.GetValue(6));
                    team.SubCaptain = _playerDAL.PlayerSelect((int)reader.GetValue(7));

                    teams.Add(team);
                    team = new Team();
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return teams;
        }

        public bool TeamMatcherUpdate(int id_team, bool matcher)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_update", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));
            cmd.Parameters.Add(new MySqlParameter("p_matcher", matcher));

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