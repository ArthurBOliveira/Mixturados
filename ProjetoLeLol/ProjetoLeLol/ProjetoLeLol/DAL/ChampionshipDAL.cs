using MySql.Data.MySqlClient;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.DAL
{
    public class ChampionshipDAL : BaseDAL
    {
        private TeamDAL _teamDAL = new TeamDAL();
        private PlayerDAL _playerDAL = new PlayerDAL();

        #region Championship
        public bool ChampionshipCreate(Championship c)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_championship_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_number_games", c.NumberOfGames));
            cmd.Parameters.Add(new MySqlParameter("p_start_date", c.StartDate));
            cmd.Parameters.Add(new MySqlParameter("p_prize", c.Prize));
            cmd.Parameters.Add(new MySqlParameter("p_actual", c.Current));
            cmd.Parameters.Add(new MySqlParameter("p_title", c.Title));
            cmd.Parameters.Add(new MySqlParameter("p_number_rounds", c.NumberOfRounds));
            cmd.Parameters.Add(new MySqlParameter("p_entry", c.Entry));
            cmd.Parameters.Add(new MySqlParameter("p_link", c.Link));
            cmd.Parameters.Add(new MySqlParameter("p_type", c.Type));

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

        public bool ChampionshipGameCreate(Championship c, Game g)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_championship_game_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", c.Id));
            cmd.Parameters.Add(new MySqlParameter("p_id_game", g.Id));

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

        public bool ChampionshipTeamCreate(Championship c, Team t)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_championship_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", c.Id));
            cmd.Parameters.Add(new MySqlParameter("p_id_team", t.Id));

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

        public bool ChampionshipTeamCreateTemporary(Championship c, Team t)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_championship_create_temporary", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", c.Id));
            cmd.Parameters.Add(new MySqlParameter("p_id_team", t.Id));

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

        public bool ChampionshipEnd(int id_champ_old, int id_champ_new, int id_winner, int id_second, int id_third, int id_fourth)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_championship_end", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship_old", id_champ_old));
            cmd.Parameters.Add(new MySqlParameter("p_id_championship_new", id_champ_new));

            cmd.Parameters.Add(new MySqlParameter("p_winner", id_winner));
            cmd.Parameters.Add(new MySqlParameter("p_second", id_second));
            cmd.Parameters.Add(new MySqlParameter("p_third", id_third));
            cmd.Parameters.Add(new MySqlParameter("p_fourth", id_fourth));

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

        public List<Championship> ChampionshipList()
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Championship> championships = new List<Championship>();
            Championship championship = new Championship();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.NumberOfGames = (int)reader.GetValue(1);
                    championship.StartDate = DateTime.Parse(reader.GetValue(2).ToString());
                    championship.Prize = reader.GetValue(3).ToString();
                    championship.Title = reader.GetValue(5).ToString();
                    championship.NumberOfRounds = (int)reader.GetValue(6);
                    championship.CurrentRound = (int)reader.GetValue(7);
                    championship.Started = reader.GetBoolean(15);
                    championship.Entry = reader.GetValue(16).ToString();
                    championship.Link = reader.GetValue(17).ToString();
                    championship.Type = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), reader.GetValue(18).ToString(), true);

                    championships.Add(championship);
                    championship = new Championship();
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

            return championships;
        }

        public Championship ChampionshipSelect(int id_champ)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));

            Championship championship = new Championship();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    championship.Winner = new Team();
                    championship.SecondPlace = new Team();
                    championship.ThirdPlace = new Team();
                    championship.FourthPlace = new Team();

                    championship.Id = (int)reader.GetValue(0);
                    championship.NumberOfGames = (int)reader.GetValue(1);
                    championship.StartDate = DateTime.Parse(reader.GetValue(2).ToString());
                    championship.Prize = reader.GetValue(3).ToString();
                    championship.Current = reader.GetBoolean(4);
                    championship.Title = reader.GetValue(5).ToString();
                    championship.NumberOfRounds = (int)reader.GetValue(6);
                    championship.CurrentRound = (int)reader.GetValue(7);
                    championship.Ended = reader.GetBoolean(20);
                    championship.Started = reader.GetBoolean(21);
                    championship.Entry = reader.GetValue(22).ToString();
                    championship.Link = reader.GetValue(23).ToString();
                    championship.Type = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), reader.GetValue(24).ToString(), true);

                    championship.Winner.Id = (int)reader.GetValue(8);
                    championship.Winner.Name = reader.GetValue(9).ToString();
                    championship.Winner.Tag = reader.GetValue(10).ToString();

                    championship.SecondPlace.Id = (int)reader.GetValue(11);
                    championship.SecondPlace.Name = reader.GetValue(12).ToString();
                    championship.SecondPlace.Tag = reader.GetValue(13).ToString();

                    championship.ThirdPlace.Id = (int)reader.GetValue(14);
                    championship.ThirdPlace.Name = reader.GetValue(15).ToString();
                    championship.ThirdPlace.Tag = reader.GetValue(16).ToString();

                    championship.FourthPlace.Id = (int)reader.GetValue(17);
                    championship.FourthPlace.Name = reader.GetValue(18).ToString();
                    championship.FourthPlace.Tag = reader.GetValue(19).ToString();

                    championship.LinkRiot = reader.GetValue(25).ToString();
                    championship.Html_btn = reader.GetValue(26).ToString();
                    championship.Html_list = reader.GetValue(27).ToString();
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

            return championship;
        }

        //TODO
        public bool ChampionshipUpdate(Championship c)
        {
            bool result = false;

            return result;
        }

        public Championship ChampionshipSelectCurrent()
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_select_current", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            Championship championship = new Championship();
            championship.Current = true;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.NumberOfGames = (int)reader.GetValue(1);
                    championship.StartDate = DateTime.Parse(reader.GetValue(2).ToString());
                    championship.Prize = reader.GetValue(3).ToString();
                    championship.Title = reader.GetValue(5).ToString();
                    championship.NumberOfRounds = (int)reader.GetValue(6);
                    championship.CurrentRound = (int)reader.GetValue(7);
                    championship.Started = reader.GetBoolean(15);
                    championship.Entry = reader.GetValue(16).ToString();
                    championship.Link = reader.GetValue(17).ToString();
                    championship.Type = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), reader.GetValue(18).ToString(), true);
                    championship.LinkRiot = reader.GetValue(19).ToString();
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

            return championship;
        }

        //TODO Criar a coluna isFull na tabela do Championship
        public bool ChampionshipIsFull(int id_champ, int numberTeams)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_championship_count", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));

            List<Team> teams = new List<Team>();
            Team team = new Team();

            try
            {
                conn.Open();
                int aux = Convert.ToInt32(cmd.ExecuteScalar());
                if (aux < numberTeams)
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

        public List<Team> ChampionshipTeamSelect(int id_champ)
        {
            MySqlCommand cmd = new MySqlCommand("proc_team_championship_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));

            List<Team> teams = new List<Team>();
            Team team = new Team();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int aux = (int)reader.GetValue(1);
                    team = _teamDAL.TeamSelect(aux);

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

        public List<Team> ChampionshipTeamSelectTemporary(int id_champ)
        {
            MySqlCommand cmd = new MySqlCommand("proc_team_championship_list_temporary", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));

            List<Team> teams = new List<Team>();
            Team team = new Team();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int aux = (int)reader.GetValue(1);
                    team = _teamDAL.TeamSelect(aux);

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

        public bool ChampionshipTeamSelectTeamVerify(int id_team, int id_champ)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_championship_team_verify", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));

            Championship champ = new Championship();

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

        public List<Game> ChampionshipGameSelect(int id_champ, int round)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_game_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));
            cmd.Parameters.Add(new MySqlParameter("p_round", round));

            List<Game> games = new List<Game>();
            Game game = new Game();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    game.Team1 = new Team();
                    game.Team2 = new Team();
                    game.Winner = new Team();
                    game.Loser = new Team();
                    game.Team1.Captain = new Player();
                    game.Team2.Captain = new Player();

                    game.Id = (int)reader.GetValue(0);
                    game.Date = Convert.ToDateTime(reader.GetValue(1));
                    game.Obs = reader.GetValue(2).ToString();
                    game.Round = (int)reader.GetValue(3);

                    game.Team1.Id = (int)reader.GetValue(4);
                    game.Team1.Name = reader.GetValue(5).ToString();
                    game.Team1.Tag = reader.GetValue(6).ToString();

                    game.Team2.Id = (int)reader.GetValue(7);
                    game.Team2.Name = reader.GetValue(8).ToString();
                    game.Team2.Tag = reader.GetValue(9).ToString();

                    game.Winner.Id = (int)reader.GetValue(10);
                    game.Winner.Name = reader.GetValue(11).ToString();
                    game.Winner.Tag = reader.GetValue(12).ToString();

                    game.Loser.Id = (int)reader.GetValue(13);
                    game.Loser.Name = reader.GetValue(14).ToString();
                    game.Loser.Tag = reader.GetValue(15).ToString();

                    game.IsFinal = reader.GetBoolean(16);
                    game.IsThird = reader.GetBoolean(17);
                    game.IdMatch = (int)reader.GetValue(18);

                    game.TCode = reader.GetString(19);

                    game.Team1.Captain.Id = (int)reader.GetValue(20);
                    game.Team2.Captain.Id = (int)reader.GetValue(21);

                    games.Add(game);
                    game = new Game();
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

            return games;
        }

        public List<Game> ChampionshipGameSelectFull(int id_champ)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_game_list_full", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));

            List<Game> games = new List<Game>();
            Game game = new Game();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    game.Team1 = new Team();
                    game.Team2 = new Team();
                    game.Winner = new Team();
                    game.Loser = new Team();

                    game.Id = (int)reader.GetValue(0);
                    game.Date = Convert.ToDateTime(reader.GetValue(1));
                    game.Obs = reader.GetValue(2).ToString();
                    game.Round = (int)reader.GetValue(3);

                    game.Team1.Id = (int)reader.GetValue(4);
                    game.Team1.Name = reader.GetValue(5).ToString();
                    game.Team1.Tag = reader.GetValue(6).ToString();

                    game.Team2.Id = (int)reader.GetValue(7);
                    game.Team2.Name = reader.GetValue(8).ToString();
                    game.Team2.Tag = reader.GetValue(9).ToString();

                    game.Winner.Id = (int)reader.GetValue(10);
                    game.Winner.Name = reader.GetValue(11).ToString();
                    game.Winner.Tag = reader.GetValue(12).ToString();

                    game.Loser.Id = (int)reader.GetValue(13);
                    game.Loser.Name = reader.GetValue(14).ToString();
                    game.Loser.Tag = reader.GetValue(15).ToString();

                    game.IsFinal = reader.GetBoolean(16);
                    game.IsThird = reader.GetBoolean(17);
                    game.IdMatch = (int)reader.GetValue(18);

                    games.Add(game);
                    game = new Game();
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

            return games;
        }

        public bool ChampionshipEndRound(Championship c)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_championship_end_turn", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", c.Id));
            cmd.Parameters.Add(new MySqlParameter("p_round", c.CurrentRound++));

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

        public void ChampionshipStart(int id_champ)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_start", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }

        public bool ChampionshipPlayerCreate(Championship c, Player player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_championship_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", c.Id));
            cmd.Parameters.Add(new MySqlParameter("p_id_player", player.Id));

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

        public List<Player> ChampionshipPlayerList(int id_champ)
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_championship_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));

            List<Player> players = new List<Player>();
            Player player = new Player();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int aux = (int)reader.GetValue(1);
                    player = _playerDAL.PlayerSelect(aux);

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

        public List<Player> ChampionshipPlayerListTemporary(int id_champ)
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_championship_list_temporary", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));

            List<Player> players = new List<Player>();
            Player player = new Player();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int aux = (int)reader.GetValue(1);
                    player = _playerDAL.PlayerSelect(aux);

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

        public bool ChampionshipPlayerSelectPlayerVerify(Player player, int id_champ)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_team_championship_player_verify", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));
            cmd.Parameters.Add(new MySqlParameter("p_id_player", player.Id));

            Championship champ = new Championship();

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

        public Championship ChampionshipSelectCurrentx5()
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_select_currentx5", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            Championship championship = new Championship();
            championship.Current = true;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.NumberOfGames = (int)reader.GetValue(1);
                    championship.StartDate = DateTime.Parse(reader.GetValue(2).ToString());
                    championship.Prize = reader.GetValue(3).ToString();
                    championship.Title = reader.GetValue(5).ToString();
                    championship.NumberOfRounds = (int)reader.GetValue(6);
                    championship.CurrentRound = (int)reader.GetValue(7);
                    championship.Started = reader.GetBoolean(15);
                    championship.Entry = reader.GetValue(16).ToString();
                    championship.Link = reader.GetValue(17).ToString();
                    championship.Type = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), reader.GetValue(18).ToString(), true);
                    championship.LinkRiot = reader.GetValue(19).ToString();
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

            return championship;
        }
        #endregion

        #region Game
        public int GameCreate(Game g)
        {
            int id_game = 0;

            MySqlCommand cmd = new MySqlCommand("proc_game_create", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_team1", g.Team1.Id));
            cmd.Parameters.Add(new MySqlParameter("p_id_team2", g.Team2.Id));
            cmd.Parameters.Add(new MySqlParameter("p_date", g.Date));
            cmd.Parameters.Add(new MySqlParameter("p_obs", g.Obs));
            cmd.Parameters.Add(new MySqlParameter("p_round", g.Round));
            cmd.Parameters.Add(new MySqlParameter("p_is_final", g.IsFinal));
            cmd.Parameters.Add(new MySqlParameter("p_is_third", g.IsThird));
            cmd.Parameters.Add(new MySqlParameter("p_tcode", g.TCode));

            try
            {
                conn.Open();
                id_game = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return id_game;
        }

        public Game GameSelect(int id_game)
        {
            MySqlCommand cmd = new MySqlCommand("proc_game_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_game", id_game));

            Game game = new Game();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    game.Id = (int)reader.GetValue(0);
                    game.Team1 = _teamDAL.TeamSelect((int)reader.GetValue(1));
                    game.Team2 = _teamDAL.TeamSelect((int)reader.GetValue(2));
                    game.Date = DateTime.Parse(reader.GetValue(3).ToString());
                    game.Winner = _teamDAL.TeamSelect((int)reader.GetValue(4));
                    game.Loser = _teamDAL.TeamSelect((int)reader.GetValue(5));
                    game.Obs = reader.GetValue(6).ToString();
                    game.Round = (int)reader.GetValue(7);
                    game.IsFinal = reader.GetBoolean(8);
                    game.IsThird = reader.GetBoolean(9);
                    game.IdMatch = (int)reader.GetValue(10);
                    game.TCode = reader.GetString(11);
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

            return game;
        }

        public bool GameEnd(Game g)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_game_end", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_game", g.Id));
            cmd.Parameters.Add(new MySqlParameter("p_winner", g.Winner.Id));
            cmd.Parameters.Add(new MySqlParameter("p_loser", g.Loser.Id));
            cmd.Parameters.Add(new MySqlParameter("p_obs", g.Obs));
            cmd.Parameters.Add(new MySqlParameter("p_id_match", g.IdMatch));

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

        public List<Game> ChampionshipGameSelectFullCurrent()
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_game_list_current", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Game> games = new List<Game>();
            Game game = new Game();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    game.Team1 = new Team();
                    game.Team2 = new Team();
                    game.Winner = new Team();
                    game.Loser = new Team();

                    game.Id = (int)reader.GetValue(0);
                    game.Date = Convert.ToDateTime(reader.GetValue(1));
                    game.Obs = reader.GetValue(2).ToString();
                    game.Round = (int)reader.GetValue(3);

                    game.Team1.Id = (int)reader.GetValue(4);
                    game.Team1.Name = reader.GetValue(5).ToString();
                    game.Team1.Tag = reader.GetValue(6).ToString();

                    game.Team2.Id = (int)reader.GetValue(7);
                    game.Team2.Name = reader.GetValue(8).ToString();
                    game.Team2.Tag = reader.GetValue(9).ToString();

                    game.Winner.Id = (int)reader.GetValue(10);
                    game.Winner.Name = reader.GetValue(11).ToString();
                    game.Winner.Tag = reader.GetValue(12).ToString();

                    game.Loser.Id = (int)reader.GetValue(13);
                    game.Loser.Name = reader.GetValue(14).ToString();
                    game.Loser.Tag = reader.GetValue(15).ToString();

                    game.IsFinal = reader.GetBoolean(16);
                    game.IsThird = reader.GetBoolean(17);

                    game.TCode = reader.GetString(18);

                    games.Add(game);
                    game = new Game();
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

            return games;
        }
        #endregion

        #region GameX1
        public Championship ChampionshipSelectCurrentx1()
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_select_currentx1", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            Championship championship = new Championship();
            championship.Current = true;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.NumberOfGames = (int)reader.GetValue(1);
                    championship.StartDate = DateTime.Parse(reader.GetValue(2).ToString());
                    championship.Prize = reader.GetValue(3).ToString();
                    championship.Title = reader.GetValue(5).ToString();
                    championship.NumberOfRounds = (int)reader.GetValue(6);
                    championship.CurrentRound = (int)reader.GetValue(7);
                    championship.Started = reader.GetBoolean(15);
                    championship.Entry = reader.GetValue(16).ToString();
                    championship.Link = reader.GetValue(17).ToString();
                    championship.Type = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), reader.GetValue(18).ToString(), true);
                    championship.LinkRiot = reader.GetValue(19).ToString();
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

            return championship;
        }

        public int GameX1Create(GameX1 g)
        {
            int id_game = 0;

            MySqlCommand cmd = new MySqlCommand("proc_gamex1_create", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player1", g.Player1.Id));
            cmd.Parameters.Add(new MySqlParameter("p_id_player2", g.Player2.Id));
            cmd.Parameters.Add(new MySqlParameter("p_date", g.Date));
            cmd.Parameters.Add(new MySqlParameter("p_obs", g.Obs));
            cmd.Parameters.Add(new MySqlParameter("p_round", g.Round));
            cmd.Parameters.Add(new MySqlParameter("p_is_final", g.IsFinal));
            cmd.Parameters.Add(new MySqlParameter("p_is_third", g.IsThird));
            cmd.Parameters.Add(new MySqlParameter("p_tcode", g.TCode));

            try
            {
                conn.Open();
                id_game = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return id_game;
        }

        public GameX1 GameX1Select(int id_game)
        {
            MySqlCommand cmd = new MySqlCommand("proc_gamex1_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_game", id_game));

            GameX1 game = new GameX1();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    game.Id = (int)reader.GetValue(0);
                    game.Player1 = _playerDAL.PlayerSelect((int)reader.GetValue(1));
                    game.Player2 = _playerDAL.PlayerSelect((int)reader.GetValue(2));
                    game.Date = DateTime.Parse(reader.GetValue(3).ToString());
                    game.Winner = _playerDAL.PlayerSelect((int)reader.GetValue(4));
                    game.Loser = _playerDAL.PlayerSelect((int)reader.GetValue(5));
                    game.Obs = reader.GetValue(6).ToString();
                    game.Round = (int)reader.GetValue(7);
                    game.IsFinal = reader.GetBoolean(8);
                    game.IsThird = reader.GetBoolean(9);
                    game.IdMatch = (int)reader.GetValue(10);
                    game.TCode = reader.GetString(11);
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

            return game;
        }

        public bool GameX1End(GameX1 g)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_gamex1_end", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_game", g.Id));
            cmd.Parameters.Add(new MySqlParameter("p_winner", g.Winner.Id));
            cmd.Parameters.Add(new MySqlParameter("p_loser", g.Loser.Id));
            cmd.Parameters.Add(new MySqlParameter("p_obs", g.Obs));
            cmd.Parameters.Add(new MySqlParameter("p_id_match", g.IdMatch));

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

        public List<GameX1> ChampionshipGameX1SelectFullCurrent()
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_gamex1_list_current", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<GameX1> games = new List<GameX1>();
            GameX1 game = new GameX1();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    game.Player1 = new Player();
                    game.Player2 = new Player();
                    game.Winner = new Player();
                    game.Loser = new Player();

                    game.Id = (int)reader.GetValue(0);
                    game.Date = Convert.ToDateTime(reader.GetValue(1));
                    game.Obs = reader.GetValue(2).ToString();
                    game.Round = (int)reader.GetValue(3);

                    game.Player1.Id = (int)reader.GetValue(4);
                    game.Player1.Name = reader.GetValue(5).ToString();
                    game.Player1.Nick = reader.GetValue(6).ToString();

                    game.Player2.Id = (int)reader.GetValue(7);
                    game.Player2.Name = reader.GetValue(8).ToString();
                    game.Player2.Nick = reader.GetValue(9).ToString();

                    game.Winner.Id = (int)reader.GetValue(10);
                    game.Winner.Name = reader.GetValue(11).ToString();
                    game.Winner.Nick = reader.GetValue(12).ToString();

                    game.Loser.Id = (int)reader.GetValue(13);
                    game.Loser.Name = reader.GetValue(14).ToString();
                    game.Loser.Nick = reader.GetValue(15).ToString();

                    game.IsFinal = reader.GetBoolean(16);
                    game.IsThird = reader.GetBoolean(17);

                    game.TCode = reader.GetString(18);

                    games.Add(game);
                    game = new GameX1();
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

            return games;
        }

        public bool ChampionshipGameX1Create(Championship c, GameX1 g)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_championship_gamex1_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", c.Id));
            cmd.Parameters.Add(new MySqlParameter("p_id_gamex1", g.Id));

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

        public List<GameX1> ChampionshipGameX1Select(int id_champ, int round)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_gamex1_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));
            cmd.Parameters.Add(new MySqlParameter("p_round", round));

            List<GameX1> games = new List<GameX1>();
            GameX1 game = new GameX1();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    game.Player1 = new Player();
                    game.Player2 = new Player();
                    game.Winner = new Player();
                    game.Loser = new Player();

                    game.Id = (int)reader.GetValue(0);
                    game.Date = Convert.ToDateTime(reader.GetValue(1));
                    game.Obs = reader.GetValue(2).ToString();
                    game.Round = (int)reader.GetValue(3);

                    game.Player1.Id = (int)reader.GetValue(4);
                    game.Player1.Name = reader.GetValue(5).ToString();
                    game.Player1.Nick = reader.GetValue(6).ToString();

                    game.Player2.Id = (int)reader.GetValue(7);
                    game.Player2.Name = reader.GetValue(8).ToString();
                    game.Player2.Nick = reader.GetValue(9).ToString();

                    game.Winner.Id = (int)reader.GetValue(10);
                    game.Winner.Name = reader.GetValue(11).ToString();
                    game.Winner.Nick = reader.GetValue(12).ToString();

                    game.Loser.Id = (int)reader.GetValue(13);
                    game.Loser.Name = reader.GetValue(14).ToString();
                    game.Loser.Nick = reader.GetValue(15).ToString();

                    game.IsFinal = reader.GetBoolean(16);
                    game.IsThird = reader.GetBoolean(17);

                    game.TCode = reader.GetString(19);

                    games.Add(game);
                    game = new GameX1();
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

            return games;
        }

        public List<GameX1> ChampionshipGameX1SelectFull(int id_champ)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_gamex1_list_full", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_championship", id_champ));

            List<GameX1> games = new List<GameX1>();
            GameX1 game = new GameX1();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    game.Player1 = new Player();
                    game.Player2 = new Player();
                    game.Winner = new Player();
                    game.Loser = new Player();

                    game.Id = (int)reader.GetValue(0);
                    game.Date = Convert.ToDateTime(reader.GetValue(1));
                    game.Obs = reader.GetValue(2).ToString();
                    game.Round = (int)reader.GetValue(3);

                    game.Player1.Id = (int)reader.GetValue(4);
                    game.Player1.Name = reader.GetValue(5).ToString();
                    game.Player1.Nick = reader.GetValue(6).ToString();

                    game.Player2.Id = (int)reader.GetValue(7);
                    game.Player2.Name = reader.GetValue(8).ToString();
                    game.Player2.Nick = reader.GetValue(9).ToString();

                    game.Winner.Id = (int)reader.GetValue(10);
                    game.Winner.Name = reader.GetValue(11).ToString();
                    game.Winner.Nick = reader.GetValue(12).ToString();

                    game.Loser.Id = (int)reader.GetValue(13);
                    game.Loser.Name = reader.GetValue(14).ToString();
                    game.Loser.Nick = reader.GetValue(15).ToString();

                    game.IsFinal = reader.GetBoolean(16);
                    game.IsThird = reader.GetBoolean(17);

                    game.TCode = reader.GetString(19);

                    games.Add(game);
                    game = new GameX1();
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

            return games;
        }
        #endregion

        #region League
        public bool LeagueTeamCreate(int id_league, int id_team, int points)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_league_team_create", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_league", id_league));
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));
            cmd.Parameters.Add(new MySqlParameter("p_points", points));

            try
            {
                conn.Open();
                cmd.ExecuteReader();
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

        public bool LeagueTeamUpdate(int id_league_team, int points)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_league_team_update", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_league_team", id_league_team));
            cmd.Parameters.Add(new MySqlParameter("p_points", points));

            try
            {
                conn.Open();
                cmd.ExecuteReader();
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

        public List<League_team> LeagueTeamList(int id_league)
        {
            MySqlCommand cmd = new MySqlCommand("proc_league_team_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_league", id_league));

            List<League_team> league_teams = new List<League_team>();
            League_team league_team = new League_team();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    league_team.Id = (int)reader.GetValue(0);
                    league_team.Id_league = (int)reader.GetValue(1);
                    league_team.Team = _teamDAL.TeamSelect((int)reader.GetValue(2));
                    league_team.Points = (int)reader.GetValue(3);

                    league_teams.Add(league_team);
                    league_team = new League_team();
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

            return league_teams;
        }

        public League_team LeagueTeamSelect(int id_league, int id_team)
        {
            MySqlCommand cmd = new MySqlCommand("proc_league_team_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_league", id_league));
            cmd.Parameters.Add(new MySqlParameter("p_id_team", id_team));

            League_team league_team = new League_team();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    league_team.Id = (int)reader.GetValue(0);
                    league_team.Id_league = (int)reader.GetValue(1);
                    league_team.Team = _teamDAL.TeamSelect((int)reader.GetValue(2));
                    league_team.Points = (int)reader.GetValue(3);
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

            return league_team;
        }

        public League LeagueCurrent()
        {
            MySqlCommand cmd = new MySqlCommand("proc_league_current", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            League league = new League();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    league.Id = (int)reader.GetValue(0);
                    league.Name = reader.GetString(1);
                    league.Prize = reader.GetString(2);
                    league.LinkRules = reader.GetString(4);
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

            return league;
        }

        public List<Championship> ChampionshipListByLeague(int p)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_list_by_league", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_league", p));

            List<Championship> championships = new List<Championship>();
            Championship championship = new Championship();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.NumberOfGames = (int)reader.GetValue(1);
                    championship.StartDate = DateTime.Parse(reader.GetValue(2).ToString());
                    championship.Prize = reader.GetValue(3).ToString();
                    championship.Title = reader.GetValue(5).ToString();
                    championship.NumberOfRounds = (int)reader.GetValue(6);
                    championship.CurrentRound = (int)reader.GetValue(7);
                    championship.Started = reader.GetBoolean(15);
                    championship.Entry = reader.GetValue(16).ToString();
                    championship.Link = reader.GetValue(17).ToString();
                    championship.Type = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), reader.GetValue(18).ToString(), true);

                    championships.Add(championship);
                    championship = new Championship();
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

            return championships;
        }

        public Championship ChampionshipSelectCurrentByLeague(int id_league)
        {
            MySqlCommand cmd = new MySqlCommand("proc_championship_select_current_by_league", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_league", id_league));

            Championship championship = new Championship();
            championship.Current = true;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    championship.Id = (int)reader.GetValue(0);
                    championship.NumberOfGames = (int)reader.GetValue(1);
                    championship.StartDate = DateTime.Parse(reader.GetValue(2).ToString());
                    championship.Prize = reader.GetValue(3).ToString();
                    championship.Title = reader.GetValue(5).ToString();
                    championship.NumberOfRounds = (int)reader.GetValue(6);
                    championship.CurrentRound = (int)reader.GetValue(7);
                    championship.Started = reader.GetBoolean(15);
                    championship.Entry = reader.GetValue(16).ToString();
                    championship.Link = reader.GetValue(17).ToString();
                    championship.Type = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), reader.GetValue(18).ToString(), true);
                    championship.LinkRiot = reader.GetValue(19).ToString();
                    championship.Html_btn = reader.GetValue(21).ToString();
                    championship.Html_list = reader.GetValue(22).ToString();

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

            return championship;
        }

        public League LeagueSelectById(int id_league)
        {
            MySqlCommand cmd = new MySqlCommand("proc_league_select_by_id", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_league", id_league));

            League league = new League();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    league.Id = (int)reader.GetValue(0);
                    league.Name = reader.GetString(1);
                    league.Prize = reader.GetString(2);
                    league.LinkRules = reader.GetString(4);
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

            return league;
        }
        #endregion
    }
}