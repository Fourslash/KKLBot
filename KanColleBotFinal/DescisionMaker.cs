using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleBotFinal.Frames;
using KanColleBotFinal.Items;
using KanColleBotFinal.Ships;
using KanColleBotFinal.Missions;

namespace KanColleBotFinal
{
    class DescisionMaker
    {
        
        KanColleProxy proxy;





        public DescisionMaker()
        {
            proxy = new KanColleProxy();
            Map.Init();
            Dock.Init();
        }

        public static void CheckForReturns()
        {
            if (Map.CurrentFrame is MainMenuFrame)
            {
                if ((Map.CurrentFrame as MainMenuFrame).isExpReturned() == false)
                    Map.ReloadMainMenu();
            }
            else
                Map.OpenMainMenu();
            
            while ((Map.CurrentFrame as MainMenuFrame).isExpReturned()==true)
            {
                LogWriter.WriteLogSucces(string.Format("Returned expeditions detected. Updating info"));
                (Map.CurrentFrame as MainMenuFrame).GetReturnedExp();
            }
        }
        public static void AddExpeditions()
        {
            if (Dock.Fleets == null)
                return;

            List<int> keys = new List<int>(ExpeditionChanger.FleetToExp.Keys.ToArray());
            foreach (int key in keys)
            {
                if (Dock.Fleets.Find(x => x.ID == key).MissionStatus == 0)
                {
                    ActionStack.AddAction(new Actions.SendExpAction(Dock.Fleets.Find(x => x.ID == key), ExpeditionChanger.FleetToExp[key]));
                    LogWriter.WriteLogSucces(string.Format("{0} fleet will be sent to {1} expedition", key, ExpeditionChanger.FleetToExp[key].ID));
                }
            }
        }

        public static void SendExpedition(Fleet fl, Missions.Expedition exp)
        {
            
            //переписать
            //int page = ((expNumber - 1) / 8) + 1;
            //int line = ((expNumber - 1) % 8) + 1;
            if (fl.IsOkForExp(exp)==false)
            {

                try
                {
                   
                    LogWriter.WriteLogSucces(String.Format("Fleet {0} will be reequiped for expedition {1}", fl.ID, exp.ID));
                    fl.ClearShips();
                    fl = Dock.Fleets[fl.ID - 1];

                    Missions.FleetCreator flk = new Missions.FleetCreator(exp);
                    Fleet tmp = flk.EquipForExpedition();

                    fl.FillFleetWithShips(tmp);
                    fl = Dock.Fleets[fl.ID - 1];

                    if (fl.IsOkForExp(exp) == false)
                        throw new AlgoritmicException("Strange thing happened i dunno");
                    LogWriter.WriteLogSucces(String.Format("Fleet {0} successfully equiped for expedition {1}", fl.ID, exp.ID));
                }
                catch (Exception ex)
                {
                    LogWriter.WriteLogOnException(ex);
                    return;
                }
            }

            if (fl.IsSuplied == false)
            {
                HttpSender.SupplyFleet(fl);
                Map.ReloadMainMenu();
            }
            Map.OpenExpeditions();
            (Map.CurrentFrame as Expeditions).StartExpedition(exp.PageInList, exp.RowInPage, fl.ID);
           // Map.OpenMainMenu();
            
        }

        public static DateTime UnixTimeStampToDateTime( double unixTimeStamp )
{
    
    // Unix timestamp is seconds past epoch
    System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
    dtDateTime = dtDateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
    return dtDateTime;
}
    }
}
