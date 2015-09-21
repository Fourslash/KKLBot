using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace KanColleBotFinal.ResourceLogging
{
    class ResLogger
    {
        static List<LoggerRecord> lst = new List<LoggerRecord>();
        static List<LoggerRecord> lstToSave;
        const string path= "ResLogging.xml";
        static void Save()
        {
            try
            {
                if (!File.Exists(path))
                    using (File.Create(path)) ;

                var serializer = new XmlSerializer(lst.GetType());
                var sw = new StreamWriter(path);
                serializer.Serialize(sw, lst);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new AlgoritmicException("cant save ResLog becaues of: " + ex.Message);
            }
        }
        public static void Load()
        {
            //Dictionary<int, int> tmp = new Dictionary<int, int>();

            try
            {
                if (!File.Exists(path))
                    using (File.Create(path)) ;
                var stream = new StreamReader(path);
                if (stream.BaseStream.Length != 0)
                {
                    var ser = new XmlSerializer(lst.GetType());
                    lst = (List<LoggerRecord>)ser.Deserialize(stream);
                }
                stream.Close();
                LogWriter.WriteLogSucces("Resourse log loaded successfully");
            }
            catch (Exception ex)
            {
                throw new AlgoritmicException("cant load ResLog becaues of: " + ex.Message);
            }
        }

        public static void AddRecord(LoggerRecord rec)
        {
            lst.Add(rec);
            Save();
        }


        static string getResChangesString(List<LoggerRecord> range)
        {
            string result = "";
            ChangeCounter cc = new ChangeCounter();
            foreach (var r in range)
                cc.Add(r.Change);
            result += string.Format("Fuel: {0}, \n", cc.fuelChange);
            result += string.Format("Ammo: {0}, \n", cc.ammoChange);
            result += string.Format("Steel: {0}, \n", cc.steelChange);
            result += string.Format("Bauxite: {0}, \n", cc.bxChange);
            result += string.Format("Instant construction: {0}, \n", cc.constrChange);
            result += string.Format("Instant repair: {0}, \n", cc.repChange);
            result += string.Format("Developement material: {0}, \n", cc.devChange);
            return result;
        }




        public static string getBasicInfo()
        {
            List<LoggerRecord> range = lst.FindAll(x => x.Date >= DateTime.Today);

            string result = "";
            int quests = range.Count(x => x.GetType() == typeof(QuestCompletedRecord));
            int resupplyes = range.Count(x => x.GetType() == typeof(ResupplyRecord));
            int exps = range.Count(x => x.GetType() == typeof(ExpCompletedRecord));
            result += string.Format("Completed quests: {0}, \n", quests);
            result += string.Format("Completed expeditions: {0}, \n", exps);
            result += string.Format("Resupplies: {0}, \n", resupplyes);
            result += "---------\n";
            result += getResChangesString(range);
            return result;
        }
    }
}
