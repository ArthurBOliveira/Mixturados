using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class Player
    {
        private int _id;        
        private string _name;
        private string _nick;
        private string _password;
        private DateTime _birthday;
        private DateTime _dateCreated;
        private string _email;
        private string _skype;
        private Roles _role1;
        private Roles _role2;
        private Champions _champion;
        private bool _isCaptain;
        private bool _isSubCaptain;
        private long _idRiot;
        private bool _admin;
        private States _state;
        private Schedule _schedule;
        private Schedule _schedule2;
        private string _img;
        private PlayerStatistics _playerStatistics;

        #region Attributes
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
          get { return _name; }
          set { _name = value; }
        }
        
        public string Nick
        {
          get { return _nick; }
          set { _nick = value; }
        }
        
        public string Password
        {
          get { return _password; }
          set { _password = value; }
        }
        
        public DateTime DateCreated
        {
          get { return _dateCreated; }
          set { _dateCreated = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Skype
        {
            get { return _skype; }
            set { _skype = value; }
        }

        public Roles Role1
        {
            get { return _role1; }
            set { _role1 = value; }
        }

        public Roles Role2
        {
            get { return _role2; }
            set { _role2 = value; }
        }

        public Champions Champion
        {
            get { return _champion; }
            set { _champion = value; }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        public bool IsCaptain
        {
            get { return _isCaptain; }
            set { _isCaptain = value; }
        }

        public bool IsSubCaptain
        {
            get { return _isSubCaptain; }
            set { _isSubCaptain = value; }
        }

        public long IdRiot
        {
            get { return _idRiot; }
            set { _idRiot = value; }
        }

        public bool Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }

        public States State
        {
            get { return _state; }
            set { _state = value; }
        }

        public Schedule Schedule
        {
            get { return _schedule; }
            set { _schedule = value; }
        }

        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }

        public Schedule Schedule2
        {
            get { return _schedule2; }
            set { _schedule2 = value; }
        }

        public PlayerStatistics PlayerStatistics
        {
            get { return _playerStatistics; }
            set { _playerStatistics = value; }
        }
        #endregion

        #region Builders
        public Player() { }

        public Player(string name,
                      string nick, 
                      string password,
                      string birthday,
                      string email, 
                      string skype, 
                      string role1,
                      string role2,
                      string champion,
                      string state,
                      string schedule,
                      string schedule2,
                      string img)
        {
            Name = String.IsNullOrEmpty(name) ? "" : name;
            Nick = String.IsNullOrEmpty(nick) ? "" : nick;
            Password = String.IsNullOrEmpty(password) ? "" : password;
            Birthday = DateTime.Parse(String.IsNullOrEmpty(birthday) ? "" : birthday);
            Email = String.IsNullOrEmpty(email) ? "" : email;
            Skype = String.IsNullOrEmpty(skype) ? "" : skype;
            Img = String.IsNullOrEmpty(img) ? "" : img;
            if (!String.IsNullOrEmpty(champion))
                Champion = (Champions)Enum.Parse(typeof(Champions), champion, true);

            if (!String.IsNullOrEmpty(role1))
                Role1 = (Roles)Enum.Parse(typeof(Roles), role1, true);

            if (!String.IsNullOrEmpty(role2))
                Role2 = (Roles)Enum.Parse(typeof(Roles), role2, true);

            if (!String.IsNullOrEmpty(state))
                State = (States)Enum.Parse(typeof(States), state, true);

            if (!String.IsNullOrEmpty(schedule))
                Schedule = (Schedule)Enum.Parse(typeof(Schedule), schedule, true);

            if (!String.IsNullOrEmpty(schedule2))
                Schedule2 = (Schedule)Enum.Parse(typeof(Schedule), schedule2, true);
        }
        #endregion  

        #region Methods

        #endregion
    }
}