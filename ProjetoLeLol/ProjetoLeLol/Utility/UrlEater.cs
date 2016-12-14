using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ProjetoLeLol.Utility
{
    public class UrlEater
    {
        private string _url;
        private string _html;

        public UrlEater(string url)
        {
            //_html = new System.Net.WebClient().DownloadString(url);

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.KeepAlive = true;
            webrequest.Method = "GET";
            webrequest.ContentType = "text/html";
            webrequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //webrequest.Connection = "keep-alive";
            //webrequest.Host = "cat.sabresonicweb.com";
            webrequest.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            //webrequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:18.0) Gecko/20100101 Firefox/18.0";

            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();

            Console.Write(webresponse.StatusCode);
            Stream receiveStream = webresponse.GetResponseStream();


            Encoding enc = System.Text.Encoding.GetEncoding(1252);//1252
            StreamReader loResponseStream = new
              StreamReader(receiveStream, enc);

            string Response = loResponseStream.ReadToEnd();

            loResponseStream.Close();
            webresponse.Close();

        }
    }
}