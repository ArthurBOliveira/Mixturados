using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ProjetoLeLol.Utility
{
    public class LogError
    {
        public static void GenerateError(string className, string methodName, string logError)
        {
            string fileName = String.Format("{0}" + @"Logs\" + DateTime.Now.ToString("dd-MM-yyyy--hh-mm-ss") + "-" + className + "-" + methodName + ".txt", AppDomain.CurrentDomain.BaseDirectory);

            // Check that the file doesn't already exist. If it doesn't exist, create
            if (!System.IO.File.Exists(fileName))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                {
                    file.WriteLine(logError);
                    file.Flush();
                    file.Dispose();
                    file.Close();
                }
            }
        }
    }
}