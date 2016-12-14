using ProjetoLeLol.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class Championship
    {
        private int _id;
        private int _numberOfGames;
        private DateTime _startDate;
        private List<Team> _teams;
        private List<Game> _games;
        private List<GameX1> _gamesx1;
        private Team _winner;
        private Team _secondPlace;
        private Team _thirdPlace;
        private Team _fourthPlace;
        private int _numberOfRounds;
        private int _currentRound;
        private string _title;
        private string _prize;
        private bool _current;
        private bool _isFull;
        private bool _ended;
        private int games;
        private DateTime _date;
        private string _actual;
        private int _rounds;
        private bool _started;
        private string _entry;
        private string _link;
        private ChampionshipType _type;
        private string _linkRiot;
        private List<Player> _players;
        private string _html_btn;
        private string _html_list;

        #region Attributes
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int NumberOfGames
        {
            get { return _numberOfGames; }
            set { _numberOfGames = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public List<Team> Teams
        {
            get { return _teams; }
            set { _teams = value; }
        }

        public List<Game> Games
        {
            get { return _games; }
            set { _games = value; }
        }

        public Team Winner
        {
            get { return _winner; }
            set { _winner = value; }
        }

        public Team SecondPlace
        {
            get { return _secondPlace; }
            set { _secondPlace = value; }
        }

        public Team ThirdPlace
        {
            get { return _thirdPlace; }
            set { _thirdPlace = value; }
        }

        public Team FourthPlace
        {
            get { return _fourthPlace; }
            set { _fourthPlace = value; }
        }

        public int NumberOfRounds
        {
            get { return _numberOfRounds; }
            set { _numberOfRounds = value; }
        }

        public int CurrentRound
        {
            get { return _currentRound; }
            set { _currentRound = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Prize
        {
            get { return _prize; }
            set { _prize = value; }
        }

        public bool Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public bool IsFull
        {
            get { return _isFull; }
            set { _isFull = value; }
        }

        public bool Ended
        {
            get { return _ended; }
            set { _ended = value; }
        }

        public bool Started
        {
            get { return _started; }
            set { _started = value; }
        }

        public string Entry
        {
            get { return _entry; }
            set { _entry = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public ChampionshipType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string LinkRiot
        {
            get { return _linkRiot; }
            set { _linkRiot = value; }
        }

        public List<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        public List<GameX1> GamesX1
        {
            get { return _gamesx1; }
            set { _gamesx1 = value; }
        }

        public string Html_btn
        {
            get { return _html_btn; }
            set { _html_btn = value; }
        }

        public string Html_list
        {
            get { return _html_list; }
            set { _html_list = value; }
        }
        #endregion

        #region Builders
        public Championship() { }

        public Championship(int games, DateTime date, string prize, string current, string title, int rounds, string entry, string link, string type)
        {
            NumberOfGames = games;
            StartDate = date;
            Prize = prize;
            Current = current == "on" ? true : false;
            Title = title;
            NumberOfRounds = rounds;
            Entry = entry;
            Link = link;
            Type = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), type, true);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calcula o numero de jogos e de rodadas do campeonato.
        /// </summary>
        public void CalculateNumberOfGames()
        {
            int numberOFGames = this.Teams.Count;
            this.NumberOfGames = numberOFGames;

            this.NumberOfRounds = (int) Math.Log(this.Teams.Count, 2) + 1;
        }

        /// <summary>
        /// Quando o torneio for começar, embaralha os times e gera a primeira rodada.
        /// </summary>
        public void StartTournament()
        {
            CurrentRound = 1;
            Random random = new Random();
            List<Team> teamGames = Teams;
            Games = new List<Game>();

            for(int i = 0; i < teamGames.Count; i++)
            {
                int randomNumber = random.Next(0, teamGames.Count);
                Team aux = teamGames[i];
                teamGames[i] = teamGames[randomNumber];
                teamGames[randomNumber] = aux;
            }

            for(int i = 0; i < Teams.Count; i = i + 2)
            {
                string tCode = TournamentCodeGenerator.GenarateCode(Title + " - partida " + i, "pass");

                Game aux = new Game(teamGames[i], teamGames[i + 1], CurrentRound, false, false, tCode);

                aux.Date = StartDate;
                aux.Obs = "";

                Games.Add(aux);
                aux = null;
            }
        }

        /// <summary>
        /// Quando o torneio for começar, embaralha os times e gera a primeira rodada.
        /// </summary>
        public void StartTournamentX1()
        {
            CurrentRound = 1;
            Random random = new Random();
            List<Player> playerGames = Players;
            GamesX1 = new List<GameX1>();

            for (int i = 0; i < playerGames.Count; i++)
            {
                int randomNumber = random.Next(0, playerGames.Count);
                Player aux = playerGames[i];
                playerGames[i] = playerGames[randomNumber];
                playerGames[randomNumber] = aux;
            }

            for (int i = 0; i < Players.Count; i = i + 2)
            {
                string tCode = TournamentCodeGenerator.GenarateCodeX1(Title + " - partida " + i, "pass");

                GameX1 aux = new GameX1(playerGames[i], playerGames[i + 1], CurrentRound, false, false, tCode);

                aux.Date = StartDate;
                aux.Obs = "";

                GamesX1.Add(aux);
                aux = null;
            }
        }

        /// <summary>
        /// Método para ser executado no final de cada rodada. Define os jogos da próxima rodada ou define o vencedor.
        /// </summary>
        public void EndRound()
        {
            CurrentRound++;
            List<Game> gamesAux = new List<Game>();

            if (CurrentRound == NumberOfRounds)
            {

                string tCode = TournamentCodeGenerator.GenarateCode(Title + " - final", "pass");
                Game Champion = new Game(Games[0].Winner, Games[1].Winner, CurrentRound, true, false, tCode);                

                Champion.Date = StartDate;
                Champion.Obs = "";

                gamesAux.Add(Champion);

                string tCode1 = TournamentCodeGenerator.GenarateCode(Title + " - Disputa 3", "pass");
                Game Third = new Game(Games[0].Loser, Games[1].Loser, CurrentRound, false, true, tCode1);

                Third.Date = StartDate;
                Third.Obs = "";

                gamesAux.Add(Third);

                Games = gamesAux;

                return;
            }

            int numberOfGames = (Games.Count / 2);            

            for (int i = 0; i < numberOfGames * 2; i = i + 2)
            {
                string tCode = TournamentCodeGenerator.GenarateCode(Title + " - partida " + i, "pass");
                Game aux = new Game(Games.Where(x => x.Round == CurrentRound - 1).ElementAt(i).Winner, Games.Where(x => x.Round == CurrentRound - 1).ElementAt(i + 1).Winner, CurrentRound, false, false, tCode);

                aux.Date = StartDate;
                aux.Obs = "";

                gamesAux.Add(aux);
            }

            Games = gamesAux;
        }

        /// <summary>
        /// Método para ser executado no final de cada rodada. Define os jogos da próxima rodada ou define o vencedor.
        /// </summary>
        public void EndRoundX1()
        {
            CurrentRound++;
            List<GameX1> gamesAux = new List<GameX1>();

            if (CurrentRound == NumberOfRounds)
            {

                string tCode = TournamentCodeGenerator.GenarateCodeX1(Title + " - final", "pass");
                GameX1 Champion = new GameX1(GamesX1[0].Winner, GamesX1[1].Winner, CurrentRound, true, false, tCode);

                Champion.Date = StartDate;
                Champion.Obs = "";

                gamesAux.Add(Champion);

                string tCode1 = TournamentCodeGenerator.GenarateCodeX1(Title + " - Disputa 3", "pass");
                GameX1 Third = new GameX1(GamesX1[0].Loser, GamesX1[1].Loser, CurrentRound, false, true, tCode1);

                Third.Date = StartDate;
                Third.Obs = "";

                gamesAux.Add(Third);

                GamesX1 = gamesAux;

                return;
            }

            int numberOfGames = (Games.Count / 2);

            for (int i = 0; i < numberOfGames * 2; i = i + 2)
            {
                string tCode = TournamentCodeGenerator.GenarateCode(Title + " - partida " + i, "pass");
                GameX1 aux = new GameX1(GamesX1.Where(x => x.Round == CurrentRound - 1).ElementAt(i).Winner, GamesX1.Where(x => x.Round == CurrentRound - 1).ElementAt(i + 1).Winner, CurrentRound, false, false, tCode);

                aux.Date = StartDate;
                aux.Obs = "";

                gamesAux.Add(aux);
            }

            GamesX1 = gamesAux;
        }
        #endregion
    }
}