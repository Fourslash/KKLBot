using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace KanColleBotFinal.Missions
{
    [Serializable]
    public class SettingPair
    {
        public static List<SettingPair> GetList(Dictionary<int, Expedition> pairs)
        {
            List<SettingPair> lst = new List<SettingPair>();
            List<int> keys = new List<int>(pairs.Keys.ToArray());
                foreach (int key in keys)
                {
                    lst.Add(new SettingPair(key, pairs[key].ID));
                }

            return lst;
        }
        public SettingPair() { }
        public SettingPair(int fl, int exp)
        {
            fleetNum = fl;
            expNum = exp;
        }
        public int fleetNum;
        public int expNum;

    }

    class ExpeditionChanger
    {
        const string path = "ExpeditionSettings.xml";

        public static void loadFromFile()
        {
            //Dictionary<int, int> tmp = new Dictionary<int, int>();
            List<SettingPair> lst=new List<SettingPair>();
            try
            {
                if (!File.Exists(path))
                    using (File.Create(path)) ;
                var stream = new StreamReader(path);
                if (stream.BaseStream.Length != 0)
                {
                    var ser = new XmlSerializer(lst.GetType());
                    lst = (List<SettingPair>)ser.Deserialize(stream);
                }
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new AlgoritmicException("ExpeditionSettings are not loaded");
            }

            fleetToExp.Clear();
            foreach (SettingPair pair in lst)
            {
                Expedition exp = Dock.MyExpeditions.Find(x=>x.ID==pair.expNum);
                if (exp!=null)
                    fleetToExp[pair.fleetNum] = exp;
            }

        }

        static void saveToFile()
        {
            try
            {
                List<SettingPair> lst = SettingPair.GetList(fleetToExp);


                if (!File.Exists(path))
                    using (File.Create(path)) ;

                var serializer = new XmlSerializer(lst.GetType());
                var sw = new StreamWriter(path);
                serializer.Serialize(sw, lst);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new AlgoritmicException("cant save exp settings becaues of: " + ex.Message);
            }
        }


        static Dictionary<int, Missions.Expedition> fleetToExp = new Dictionary<int, Missions.Expedition>
        {
            {2,Dock.MyExpeditions.Find(x=>x.ID==6)},
            {3,Dock.MyExpeditions.Find(x=>x.ID==5)},
            {4,Dock.MyExpeditions.Find(x=>x.ID==3)},
        };

        public static string ChangeFleetExp(int Fleet, int ExpNumber)
        {
            string answ = "";
            try
            {
                if (Fleet > 4 || Fleet < 2)
                    return ("Cant change destination for this fleet");


                if (ExpNumber == 0)
                {
                    fleetToExp.Remove(Fleet);
                    return "";
                }

                if (new List<Expedition>(fleetToExp.Values.ToArray()).Find(x => x.ID == ExpNumber) != null)
                    return ("Another fleet currently on this expedition");
                Expedition exp = Dock.MyExpeditions.Find(x => x.ID == ExpNumber);
                if (exp == null)
                    return ("Cant find this expedition in list");
                fleetToExp[Fleet] = exp;


                answ = String.Format("Fleet {0} now linked to {1} expedition", Fleet, ExpNumber);
                saveToFile();
                LogWriter.WriteLogSucces(answ);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return answ; 

        }
        public static Dictionary<int, Missions.Expedition> FleetToExp
        {
            get { return fleetToExp; }
        }

    }
}
