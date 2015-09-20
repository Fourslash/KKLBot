using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace KanColleBotFinal
{
    [Serializable]
    public class Settings
    {
        public string adress="123";
        public string api="234";
        public int x_const=3;
        public int y_const=3;
        const string path = "Settings.xml";
        public void Write()
        {
            if (!File.Exists(path))
                using (File.Create(path)) ;
            var serializer = new XmlSerializer(typeof(Settings));
            var sw = new StreamWriter(path);
            serializer.Serialize(sw, this);
            sw.Close();
        }
        public void Read()
        {
            try
            {
                if (!File.Exists(path))
                    using (File.Create(path)) ;
                var stream = new StreamReader(path);
                if (stream.BaseStream.Length != 0)
                {
                    var ser = new XmlSerializer(typeof(Settings));
                    CopyValues((Settings)ser.Deserialize(stream));
                }
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new AlgoritmicException("Settings are not loaded");
            }
        }
        void CopyValues(Settings st)
        {
            adress = st.adress;
            api = st.api;
            x_const = st.x_const;
            y_const = st.y_const;
        }
    }
}
