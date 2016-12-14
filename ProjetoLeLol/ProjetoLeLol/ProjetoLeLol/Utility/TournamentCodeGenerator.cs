using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Utility
{
    public class TournamentCodeGenerator
    {
        public static string GenarateCode(string gamename, string gamepass)
        {
            string map = "map11";                

            string mode = "pick6";

            string spec = "specALL";

            string teamsize = "team5";

            string report = "http://www.mixturadoslol.com.br/";
            
            string id = "123";

            string basepart = "{\"name\":\"" + gamename + "\",\"extra\":\",\"password\":\"" + gamepass + "\",\"report\":\"" + report + "\"}";
            string basepart2 = "{\"name\":\"" + gamename + "\",\"extra\":\"" + id + "\",\"password\":\"" + gamepass + "\",\"report\":\"" + report + "\"}";

            byte[] byt = System.Text.Encoding.UTF8.GetBytes(basepart2);
            string base64 = System.Convert.ToBase64String(byt);
            string finalcode = "pvpnet://lol/customgame/joinorcreate/" + map + "/" + mode + "/" + teamsize + "/" + spec + "/" + base64;

            return finalcode;
        }

        public static string GenarateCodeX1(string gamename, string gamepass)
        {
            string map = "map12";

            string mode = "pick6";

            string spec = "specALL";

            string teamsize = "team1";

            string report = "http://www.mixturadoslol.com.br/";

            string id = "123";

            string basepart = "{\"name\":\"" + gamename + "\",\"extra\":\",\"password\":\"" + gamepass + "\",\"report\":\"" + report + "\"}";
            string basepart2 = "{\"name\":\"" + gamename + "\",\"extra\":\"" + id + "\",\"password\":\"" + gamepass + "\",\"report\":\"" + report + "\"}";

            byte[] byt = System.Text.Encoding.UTF8.GetBytes(basepart2);
            string base64 = System.Convert.ToBase64String(byt);
            string finalcode = "pvpnet://lol/customgame/joinorcreate/" + map + "/" + mode + "/" + teamsize + "/" + spec + "/" + base64;

            return finalcode;
        }
    }
}