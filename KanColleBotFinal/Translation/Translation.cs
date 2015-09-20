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
        static List<ShipsTranslationShipTranslation> ShipItems
        {
            get
            {
                return new List<ShipsTranslationShipTranslation>(ShipTranslation.Items.ToArray());
            }
        }

        public static string TranslateShip(String kanji)
        {
            if (ShipTranslation==null)
                return "TRANSLATION NOT LOADED";
            int t =ShipItems.FindIndex(x=>x.JPName==kanji);
            if (t==-1)
                return "TRANSLATION NOT FOUND";
            else
                return ShipItems[t].TRName;
        }
        public static void LoadShipTranslation()
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
