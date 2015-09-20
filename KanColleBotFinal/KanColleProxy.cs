using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;
using System.IO;
using Codeplex.Data;

namespace KanColleBotFinal
{
    class KanColleProxy:IDisposable
    {
        public delegate void UI(string jsonString, string apiString);
        public static event UI NewDataCollected;

        public KanColleProxy()
        {
            FiddlerStart();
        }
        public void Dispose()
        {
            LogWriter.WriteLog("Shutting down Fiddler");
            Fiddler.FiddlerApplication.Shutdown();
        }
        void FiddlerStart()
        {
            try
            {

                #region settings
                FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);
                FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.Default;
                //FiddlerCoreStartupFlags.
                Fiddler.FiddlerApplication.Startup(0, oFCSF);
                #endregion

                Fiddler.FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
                Fiddler.FiddlerApplication.BeforeResponse += delegate(Fiddler.Session oS)
                {

                    
                   // if (oS.RequestHeaders.ToString().Contains("kcsapi") || oS.ResponseHeaders.ToString().Contains("text/plain"))
                    if (oS.PathAndQuery.StartsWith("/kcsapi") && oS.oResponse.MIMEType.Equals("text/plain"))
                    {
                        oS.utilDecodeResponse();

                        using (Stream receiveStream = new MemoryStream(oS.ResponseBody))
                        {
                            using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                            {
                                string jsonStr = readStream.ReadToEnd().Remove(0, 7);
                                string apiStr = oS.PathAndQuery;
                                //var json = DynamicJson.Parse(str);
                                NewDataCollected(jsonStr,apiStr);

                            }
                        }

                    }
                };
                LogWriter.WriteLogSucces("Fiddler started");
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
                throw ex;
            }
            
        }

        void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (oSession.LocalProcess.ToLower().Contains("skypebot2"))
            {
                oSession.Ignore();
            }
        }
        //~ KanColleProxy()
        //{
        //    Fiddler.FiddlerApplication.Shutdown();
        //}
    }
}
