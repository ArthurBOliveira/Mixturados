using Newtonsoft.Json.Linq;
using ProjetoLeLol.DAL;
using ProjetoLeLol.Models;
using ProjetoLeLol.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ProjetoLeLol.BLL
{
    public class ChampionshipBLL
    {
        private ChampionshipDAL _championshipDAL = new ChampionshipDAL();
        private TeamBLL _teamBLL = new TeamBLL();

        #region Championship
        public bool ChampionshipCreate(Championship c)
        {
            return _championshipDAL.ChampionshipCreate(c);
        }

        public bool ChampionshipGameCreate(Championship c, Game g)
        {
            return _championshipDAL.ChampionshipGameCreate(c, g);
        }

        public ErrorView ChampionshipTeamCreate(int id_player, Championship c)
        {
            ErrorView err = new ErrorView();

            if (_championshipDAL.ChampionshipIsFull(c.Id, c.NumberOfGames))
            {
                Team t = _teamBLL.TeamSelectByPlayerCap(id_player);
                bool aux = t.SubCaptain.Id == 0 ? false : true;

                if (_teamBLL.TeamCompleted(t.Id, aux))
                {
                    if (!_championshipDAL.ChampionshipTeamSelectTeamVerify(t.Id, c.Id))
                    {
                        if (c.Type == ChampionshipType.Grátis)
                        {
                            if (_championshipDAL.ChampionshipTeamCreate(c, t))
                            {
                                err.HasError = true;
                                err.MsgError = "Seu time foi cadastrado com sucesso neste campeonato!";
                            }
                            else
                            {
                                err.HasError = true;
                                err.MsgError = "Erro!";
                            }
                        }
                        else
                        {
                            if (_championshipDAL.ChampionshipTeamCreateTemporary(c, t))
                            {
                                err.HasError = true;
                                err.MsgError = "Aguardando comprovação de pagamento.";
                            }
                            else
                            {
                                err.HasError = true;
                                err.MsgError = "Erro!";
                            }
                        }
                    }
                    else
                    {
                        err.HasError = true;
                        err.MsgError = "Seu time já está cadastrado neste campeonato!";
                    }
                }
                else
                {
                    err.HasError = true;
                    err.MsgError = "Seu time está incompleto!";
                }
            }
            else
            {
                err.HasError = true;
                err.MsgError = "Infelimente não existe mais vagas para este campeonato, fique de olho que temos campeonatos todo fim de semana!";
            }

            return err;
        }

        public bool ChampionshipEnd(int id_champ_old, int id_champ_new)
        {
            int id_winner = 0, id_second = 0, id_third = 0, id_fourth = 0;
            Championship c = _championshipDAL.ChampionshipSelect(id_champ_old);

            c.Games = _championshipDAL.ChampionshipGameSelect(c.Id, c.NumberOfRounds);

            foreach (Game g in c.Games)
            {
                if (g.IsFinal)
                {
                    id_winner = g.Winner.Id;
                    id_second = g.Loser.Id;
                }
                if (g.IsThird)
                {
                    id_third = g.Winner.Id;
                    id_fourth = g.Loser.Id;
                }
            }

            LeagueUpdate(id_winner, id_second, id_third, id_fourth);

            return _championshipDAL.ChampionshipEnd(id_champ_old, id_champ_new, id_winner, id_second, id_third, id_fourth);
        }

        public List<Championship> ChampionshipList()
        {
            return _championshipDAL.ChampionshipList();
        }

        public Championship ChampionshipSelect(int id_champ)
        {
            return _championshipDAL.ChampionshipSelect(id_champ);
        }

        public bool ChampionshipUpdate(Championship c)
        {
            return _championshipDAL.ChampionshipUpdate(c);
        }

        public bool ChampionshipEndRound(int id_champ)
        {
            bool result = false;

            Championship c = _championshipDAL.ChampionshipSelect(id_champ);

            c.Games = _championshipDAL.ChampionshipGameSelect(c.Id, c.CurrentRound);

            c.EndRound();

            foreach (Game g in c.Games)
            {
                g.Id = _championshipDAL.GameCreate(g);
                result = _championshipDAL.ChampionshipGameCreate(c, g);
            }

            result = _championshipDAL.ChampionshipEndRound(c);

            return result;
        }

        public bool ChampionshipStart(int id_champ)
        {
            bool result = false;

            Championship c = _championshipDAL.ChampionshipSelect(id_champ);

            _championshipDAL.ChampionshipStart(id_champ);

            c.Teams = _championshipDAL.ChampionshipTeamSelect(id_champ);

            c.StartTournament();

            foreach (Game g in c.Games)
            {
                g.Id = _championshipDAL.GameCreate(g);
                result = _championshipDAL.ChampionshipGameCreate(c, g);
            }

            return result;
        }

        public Championship ChampionshipSelectCurrent()
        {
            return _championshipDAL.ChampionshipSelectCurrent();
        }

        public List<Team> ChampionshipTeamList(int id_champ)
        {
            return _championshipDAL.ChampionshipTeamSelect(id_champ);
        }

        public List<Team> ChampionshipTeamListTemporary(int id_champ)
        {
            return _championshipDAL.ChampionshipTeamSelectTemporary(id_champ);
        }

        public ErrorView ChampionshipPlayerCreate(Player player, Championship c)
        {
            ErrorView err = new ErrorView();

            if (_championshipDAL.ChampionshipIsFull(c.Id, c.NumberOfGames))
            {
                if (!_championshipDAL.ChampionshipPlayerSelectPlayerVerify(player, c.Id))
                {
                    if (_championshipDAL.ChampionshipPlayerCreate(c, player))
                    {
                        err.HasError = true;
                        err.MsgError = "Você foi cadastrado com sucesso neste campeonato!";
                    }
                    else
                    {
                        err.HasError = true;
                        err.MsgError = "Erro!";
                    }
                }
                else
                {
                    err.HasError = true;
                    err.MsgError = "Você já está cadastrado neste campeonato!";
                }
            }
            else
            {
                err.HasError = true;
                err.MsgError = "Infelimente não existe mais vagas para este campeonato, fique de olho que temos diversos campeonatos!";
            }

            return err;
        }

        public List<Player> ChampionshipPlayersList(int id_champ)
        {
            return _championshipDAL.ChampionshipPlayerList(id_champ);
        }

        public List<Player> ChampionshipPlayersListTemporary(int id_champ)
        {
            return _championshipDAL.ChampionshipPlayerListTemporary(id_champ);
        }

        public bool ChampionshipStartX1(Championship c)
        {
            bool result = false;

            _championshipDAL.ChampionshipStart(c.Id);

            c.Players = _championshipDAL.ChampionshipPlayerList(c.Id);

            c.StartTournamentX1();

            foreach (GameX1 g in c.GamesX1)
            {
                g.Id = _championshipDAL.GameX1Create(g);
                result = _championshipDAL.ChampionshipGameX1Create(c, g);
            }

            return result;
        }

        public Championship ChampionshipSelectCurrentx5()
        {
            return _championshipDAL.ChampionshipSelectCurrentx5();
        }
        #endregion

        #region Game
        public int GameCreate(Game g)
        {
            return _championshipDAL.GameCreate(g);
        }

        public Game GameSelect(int id_game)
        {
            return _championshipDAL.GameSelect(id_game);
        }

        public bool GameEnd(int id_game, int id_team_winner, string obs)
        {
            bool result = false;

            Team winner = new Team();
            winner.Id = id_team_winner;

            Team loser = new Team();

            Game g = _championshipDAL.GameSelect(id_game);

            g.Winner = winner;
            g.Obs = obs;

            if (g.Team1.Id == id_team_winner)
                loser.Id = g.Team2.Id;
            else
                loser.Id = g.Team1.Id;

            g.Loser = loser;

            g.IdMatch = GameStatsIdMatch(g);

            result = _championshipDAL.GameEnd(g);

            return result;
        }

        public List<Game> ChampionshipGameSelect(int id_champ, int round)
        {
            return _championshipDAL.ChampionshipGameSelect(id_champ, round);
        }

        public List<Game> ChampionshipGameSelectFullCurrent()
        {
            return _championshipDAL.ChampionshipGameSelectFullCurrent();
        }

        private int GameStatsIdMatch(Game g)
        {
            int result = 0;

            WebRequest request = WebRequest.Create("https://br.api.pvp.net/api/lol/br/v1.3/game/by-summoner/" + g.Team1.Captain.IdRiot + "/recent?api_key=a7a1b04f-5788-4caa-87ea-0e691d0a6547");
            //WebRequest request = WebRequest.Create("https://br.api.pvp.net/api/lol/br/v1.3/game/by-summoner/" + 454146 + "/recent?api_key=a7a1b04f-5788-4caa-87ea-0e691d0a6547");
            request.ContentType = "application/json; charset=utf-8";

            try
            {
                string rawJson;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    rawJson = sr.ReadToEnd();
                }

                JObject json = JObject.Parse(rawJson);

                result = int.Parse(json["games"][0]["gameId"].ToString());
            }
            catch (Exception e)
            {
                result = 0;
            }

            return result;
        }

        //TODO
        private bool GameStatsDownload(Game g)
        {
            bool result = false;
            GameStats gs = new GameStats();
            gs.Id_game = g.Id;

            //WebRequest request = WebRequest.Create("https://br.api.pvp.net/api/lol/br/v1.3/game/by-summoner/" + g.Team1.Captain.IdRiot + "/recent?api_key=a7a1b04f-5788-4caa-87ea-0e691d0a6547");
            WebRequest request = WebRequest.Create("https://br.api.pvp.net/api/lol/br/v1.3/game/by-summoner/" + 454146 + "/recent?api_key=a7a1b04f-5788-4caa-87ea-0e691d0a6547");
            request.ContentType = "application/json; charset=utf-8";

            try
            {
                string rawJson;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    rawJson = sr.ReadToEnd();
                }

                JObject json = JObject.Parse(rawJson);

                gs.Id_match = int.Parse(json["games"][0]["gameId"].ToString());
            }
            catch (Exception e)
            {
                result = false;
            }

            request = WebRequest.Create("https://br.api.pvp.net/api/lol/br/v2.2/match/" + gs.Id_match + "?api_key=a7a1b04f-5788-4caa-87ea-0e691d0a6547");

            try
            {
                string rawJson;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    rawJson = sr.ReadToEnd();
                }

                JObject json = JObject.Parse(rawJson);

                gs.Link = response.ResponseUri.AbsoluteUri;
                gs.Json = json;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }

        //TODO
        private List<GamePlayerStats> GameStatsFillPlayes(GameStats gs, Game g)
        {
            List<GamePlayerStats> result = new List<GamePlayerStats>();

            return result;
        }

        private GamePlayerStats GamePlayerStatsDownloads(Player p, GameStats gs)
        {
            GamePlayerStats gps = new GamePlayerStats();

            WebRequest request = WebRequest.Create("https://br.api.pvp.net/api/lol/br/v1.3/game/by-summoner/" + p.IdRiot + "/recent?api_key=a7a1b04f-5788-4caa-87ea-0e691d0a6547");
            request.ContentType = "application/json; charset=utf-8";

            try
            {
                string rawJson;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    rawJson = sr.ReadToEnd();
                }

                JObject json = JObject.Parse(rawJson);

                gps.Id_player = p.Id;
                gps.Id_gameStats = gs.Id;
                gps.Link = request.RequestUri.AbsoluteUri;
                gps.Json = json;
            }
            catch (Exception e)
            {
                gps = null;
            }

            return gps;
        }
        #endregion

        #region League
        public List<League_team> LeagueTeamList(int id_league)
        {
            return _championshipDAL.LeagueTeamList(id_league);
        }

        public League LeagueCurrent()
        {
            return _championshipDAL.LeagueCurrent();
        }

        private void LeagueUpdate(int id_winner, int id_second, int id_third, int id_fourth)
        {
            int winner = 10; int second = 7; int third = 5; int fourth = 3;

            League l = _championshipDAL.LeagueCurrent();

            League_team lt = _championshipDAL.LeagueTeamSelect(l.Id, id_winner);
            if (lt.Id != 0)
                _championshipDAL.LeagueTeamUpdate(lt.Id, (lt.Points + winner));
            else
                _championshipDAL.LeagueTeamCreate(l.Id, id_winner, winner);

            lt = _championshipDAL.LeagueTeamSelect(l.Id, id_second);
            if (lt.Id != 0)
                _championshipDAL.LeagueTeamUpdate(lt.Id, (lt.Points + second));
            else
                _championshipDAL.LeagueTeamCreate(l.Id, id_second, second);

            lt = _championshipDAL.LeagueTeamSelect(l.Id, id_third);
            if (lt.Id != 0)
                _championshipDAL.LeagueTeamUpdate(lt.Id, (lt.Points + third));
            else
                _championshipDAL.LeagueTeamCreate(l.Id, id_second, third);

            lt = _championshipDAL.LeagueTeamSelect(l.Id, id_fourth);
            if (lt.Id != 0)
                _championshipDAL.LeagueTeamUpdate(lt.Id, (lt.Points + fourth));
            else
                _championshipDAL.LeagueTeamCreate(l.Id, id_second, fourth);
        }

        public List<Championship> ChampionshipListByLeague(int p)
        {
            return _championshipDAL.ChampionshipListByLeague(p);
        }

        public Championship ChampionshipSelectCurrentByLeague(int id_league)
        {
            return _championshipDAL.ChampionshipSelectCurrentByLeague(id_league);
        }

        public League LeagueSelectById(int id_league)
        {
            return _championshipDAL.LeagueSelectById(id_league);
        }
        #endregion

        #region GameX1
        public Championship ChampionshipSelectCurrentx1()
        {
            return _championshipDAL.ChampionshipSelectCurrentx1();
        }

        public int GameX1Create(GameX1 g)
        {
            return _championshipDAL.GameX1Create(g);
        }

        public GameX1 GameX1Select(int id_game)
        {
            return _championshipDAL.GameX1Select(id_game);
        }

        public bool GameX1End(GameX1 g)
        {
            return _championshipDAL.GameX1End(g);
        }

        public List<GameX1> ChampionshipGameX1SelectFullCurrent()
        {
            return _championshipDAL.ChampionshipGameX1SelectFullCurrent();
        }

        public bool ChampionshipGameX1Create(Championship c, GameX1 g)
        {
            return _championshipDAL.ChampionshipGameX1Create(c, g);
        }

        public List<GameX1> ChampionshipGameX1Select(int id_champ, int round)
        {
            return _championshipDAL.ChampionshipGameX1Select(id_champ, round);
        }

        public List<GameX1> ChampionshipGameX1SelectFull(int id_champ)
        {
            return _championshipDAL.ChampionshipGameX1SelectFull(id_champ);
        }
        #endregion
    }
}