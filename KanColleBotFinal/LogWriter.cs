using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace KanColleBotFinal
{
    class LogWriter
    {

        const string path = "log.txt";


        static void checkFile()
        {

            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                }
            }
        }

        public static void StartSession()
        {
            checkFile();
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("=========================================================");
            }
        }
        public static void WriteLogSucces(string str)
        {
            WriteLog(str);
            WriteConsole(str, ConsoleColor.Green);

        }



        public static void WriteConsole(string str, ConsoleColor clr)
        {
            string log = DateTime.Now.ToString("d.MM HH:mm:ss ") + str;
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = clr;
            Console.WriteLine(log);
            Console.ForegroundColor = oldColor;
        }
        
        public static void WriteLog(string str)
        {
            checkFile();
            string log = DateTime.Now.ToString("d.MM HH:mm:ss ") + str;
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(log);
            }
        }
        public static void WriteLogOnException(Exception ex)
        {
            if (ex is AlgoritmicException)
            {
                WriteLog(string.Format("IMPORTANT! Exception occured in {0}. Exception message {1}", ex.Source, ex.Message));
                WriteConsole(string.Format("IMPORTANT! Exception occured in {0}. Exception message {1}", ex.Source, ex.Message), ConsoleColor.Red);
                SeijaCommunicator.SayToSeija(TEREZITYFY(string.Format("Exception occured in {0}. Exception message {1}", ex.Source, ex.Message)));
            }
            else
            {
                WriteLog(string.Format("Exception occured in {0}. Exception message{1}", ex.Source, ex.Message));
                WriteConsole(string.Format("Exception occured in {0}. Exception message{1}", ex.Source, ex.Message), ConsoleColor.DarkYellow);
            }
        }
        public static void WriteLogOnRequest(string jsonString, string apiString)
        {
            WriteLog("Got response from " +apiString);
        }
        public static string TEREZITYFY(string str)
        {
            string tmp = str.ToUpper();
            tmp = tmp.Replace("A", "4");
            tmp = tmp.Replace("I", "1");
            tmp = tmp.Replace("E", "3");
            tmp = tmp.Replace("FOR", "4");

            return tmp;
        }
    }
}
