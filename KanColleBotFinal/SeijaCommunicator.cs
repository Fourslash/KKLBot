using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourslashNettest;

namespace KanColleBotFinal
{
    class SeijaCommunicator
    {
        static Listener lst;
        static Sender snd;
        public static void Init()
        {
            lst = new Listener(11224);
            snd = new Sender(11223, "localhost");
            lst.NewStringCollected += lst_NewStringCollected;
            Task.Factory.StartNew(() =>
           lst.Start()
           );
            LogWriter.WriteLogSucces("SeijaCommunicator started");


        }

        static void lst_NewStringCollected(string NewData)
        {
             try
            {
            GetCommand(NewData);
            }
             catch (Exception ex)
             {
                 LogWriter.WriteLogOnException(ex);
             }

        }
        public static void SayToSeija(String str)
        {
            try
            {
                snd.SendString(str);
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
            }

        }

        static void GetCommand(string str)
        {
            List<string> strings = new List<string>( str.Split(' '));
            if (strings.Count  <1)
                throw new Exception("error on getting string from Seija");
            string conference = strings[0];
            string command = strings[1];
            switch (command)
            {
                case "hi":
                    {
                        string answer = string.Join(" ", new string[] {"%"+conference,"HI >:]"});
                        SayToSeija(answer);
                        break;
                    }
                case "get_status":
                    {
                        string result= string.Empty;
                        if (Dock.Fleets==null || Dock.Fleets.Count!=4)
                            result = LogWriter.TEREZITYFY("Fleets are not loaded");
                        else
                        {
                            //result += LogWriter.TEREZITYFY(ActionStack.currentAction.info) + "\n\n";
                            foreach (Ships.Fleet fl in Dock.Fleets)
                            {
                                result += LogWriter.TEREZITYFY(fl.Status);
                                result += "\n";
                            }
                        }
                        string answer = string.Join(" ", new string[] { "%" + conference,result});
                        SayToSeija(answer);
                        break;
                    }
                case "quests":
                    {
                        string result="";
                        List<Quests.Quest> lst = DescisionMaker.QuestProcesser.QuestInfo;
                        if (lst.Count == 0)
                            result = "Can't see any active quests";
                        else
                        {
                            foreach(var quest in lst)
                            {
                                result += quest.Info;
                                result += "\n";
                            }
                        }

                        result = LogWriter.TEREZITYFY(result);
                        string answer = string.Join(" ", new string[] { "%" + conference, result });
                        SayToSeija(answer);
                        break;
                    }
                case "actions":
                    {
                        string result = LogWriter.TEREZITYFY(ActionStack.getInfo);
                        string answer = string.Join(" ", new string[] { "%" + conference, result });
                        SayToSeija(answer);
                        break;
                    }
                case "materials":
                    {
                        string result = LogWriter.TEREZITYFY(Dock.resourceInfo);
                        string answer = string.Join(" ", new string[] { "%" + conference, result });
                        SayToSeija(answer);
                        break;
                    }

                case "stats":
                    {
                        string result = LogWriter.TEREZITYFY(ResourceLogging.ResLogger.getBasicInfo());
                        string answer = string.Join(" ", new string[] { "%" + conference, result });
                        SayToSeija(answer);
                        break;
                    }
                case "fleet_exp":
                    {
                        int fleet, exp;
                        string answer="Failed";
                        try
                        {
                            fleet = Convert.ToInt32(strings[2]);
                            exp = Convert.ToInt32(strings[3]);
                            answer = Missions.ExpeditionChanger.ChangeFleetExp(fleet, exp);
                        }

                        catch (AlgoritmicException ex)
                        {
                            answer = ex.Message;
                        }
                        catch (FormatException ex)
                        {
                            answer = "error: bad arguments";
                        }
                        finally
                        {
                            string result = LogWriter.TEREZITYFY(answer);
                            answer = string.Join(" ", new string[] { "%" + conference, result });
                            SayToSeija(answer);
                        }
                        break;
                    }
                case "shutdown":
                    {
                        string answer = string.Join(" ", new string[] { "%" + conference, LogWriter.TEREZITYFY("Alright whatever >:/") });
                        SayToSeija(answer);
                        System.Windows.Application.Current.Shutdown();
                        break;
                    }
                default:
                    {
                        string answer = string.Join(" ", new string[] { "%" + conference, "I DONT G3T IT >:/" });
                        SayToSeija(answer);
                        break;
                    }

            }

        }
    }
}
