using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KanColleBotFinal.Translation
{
    class Translation
    {
        static ShipsTranslation ShipTranslation;
        static QuestsTranslations QuestsTranslation;
         static List<QuestsTranslationsQuestTranslations> QuestItems
        {
            get
            {
                return new List<QuestsTranslationsQuestTranslations>(QuestsTranslation.QuestTranslations.ToArray());
            }
        }
        static List<ShipsTranslationShipTranslation> ShipItems
        {
            get
            {
                return new List<ShipsTranslationShipTranslation>(ShipTranslation.Items.ToArray());
            }
        }

        public static string TranslateShip(string kanji)
        {
            if (ShipTranslation==null)
                return "TRANSLATION NOT LOADED";
            int t =ShipItems.FindIndex(x=>x.JPName==kanji);
            if (t==-1)
                return "TRANSLATION NOT FOUND";
            else
                return ShipItems[t].TRName;
        }

        public static string TranslateQuestName(string kanji)
        {
            if (QuestsTranslation==null)
                return "TRANSLATION NOT LOADED";
            int t = QuestItems.FindIndex(x => x.JPName == kanji);
            if (t == -1)
                return "TRANSLATION NOT FOUND";
            else
                return QuestItems[t].TRName;
        }
        public static string TranslateQuestDescription(string kanji)
        {
            if (QuestsTranslation == null)
                return "TRANSLATION NOT LOADED";
            int t = QuestItems.FindIndex(x => x.JPDetail == kanji);
            if (t == -1)
                return "TRANSLATION NOT FOUND";
            else
                return QuestItems[t].TRDetail;
        }


        public static void LoadTranslations()
        {
            LoadQuestTranslation();
            LoadShipTranslation();
        }

        static void LoadQuestTranslation()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(QuestsTranslations));
                using (XmlReader reader = XmlReader.Create("QuestsTranslations.xml"))
                {
                    QuestsTranslation = (QuestsTranslations)ser.Deserialize(reader);
                }
                LogWriter.WriteLogSucces("QuestTranslations translations loaded");
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
                ShipTranslation = null;
            }
        }
        static void LoadShipTranslation()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(ShipsTranslation));
                using (XmlReader reader = XmlReader.Create("Ships.xml"))
                {
                    ShipTranslation = (ShipsTranslation)ser.Deserialize(reader);
                }
                LogWriter.WriteLogSucces("Ship translations loaded");
            }
            catch(Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
                ShipTranslation = null;
            }
        }

    }
}
