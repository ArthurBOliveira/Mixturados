using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class Team
    {
        private int _id;
        private string _name;
        private string _tag;
        private DateTime _dateCreated;
        private Player _captain;
        private Player _subCaptain;
        private List<Player> _players;
        private string _page;
        private Schedule _schedule;
        private string _img;

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

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        public Player Captain
        {
            get { return _captain; }
            set { _captain = value; }
        }

        public Player SubCaptain
        {
            get { return _subCaptain; }
            set { _subCaptain = value; }
        }

        public List<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        public string Page
        {
            get { return _page; }
            set { _page = value; }
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

        #endregion

        #region Builders
        public Team() { }

        public Team(string name, string tag, string page, string schedule, DateTime dateCreated)
        {
            Name = String.IsNullOrEmpty(name) ? "" : name;
            Tag = String.IsNullOrEmpty(tag) ? "" : tag;
            DateCreated = dateCreated;
            Page = String.IsNullOrEmpty(page) ? "" : page;;
            if (!String.IsNullOrEmpty(schedule))
                Schedule = (Schedule)Enum.Parse(typeof(Schedule), schedule, true); 
        }
        #endregion

        #region Methods
        #endregion

    }
}