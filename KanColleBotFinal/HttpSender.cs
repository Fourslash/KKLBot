using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace KanColleBotFinal
{
    class HttpSender
    {
        static string BaseAddres = string.Empty;
        static string apiToken = string.Empty;
        static string RefererHeader
        {
            get
            {
                return @"http://" + BaseAddres + @"/kcs/mainD2.swf?api_token=" + apiToken + @"/[[DYNAMIC]]/1";
            }
        }
        public static string apiString
        {
            get
            {
                return @"http://" + BaseAddres + @"/kcs/mainD2.swf?api_token=" + apiToken;
            }
        }


        public static void SetInfo(string ip, string api)
        {
            BaseAddres = ip;
            apiToken = api;

        }

        public static void ChangeShipInFleet(Ships.Fleet fl, int pos, int NewShip)
        {
            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct.Add("api_ship_id", NewShip.ToString());
            dct.Add("api_token", apiToken);
            dct.Add("api_ship_idx", pos.ToString());
            dct.Add("api_verno", "1");
            dct.Add("api_id", fl.ID.ToString());
            SendRequest("/kcsapi/api_req_hensei/change", dct);
        }
        public static void SupplyFleet(Ships.Fleet fl)
        {
            List<int> ships = new List<int>();
            foreach (Ships.Ship s in fl.Ships.FindAll(x => x.IsSuplied == false) )
                ships.Add(s.ID_Fleet);
            if (ships.Count == 0)
                return;

            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct.Add("api_token", apiToken);
            dct.Add("api_onslot", "1");
            dct.Add("api_kind", "3");
            dct.Add("api_id_items", string.Join(",", ships));
            dct.Add("api_verno", "1");
            SendRequest("/kcsapi/api_req_hokyu/charge", dct);
        }
        public static void GetQuestList (int page)
        {
            if (page < 1 || page > 5)
                throw new AlgoritmicException("Incorrect page");
            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct.Add("api_token", apiToken);
            dct.Add("api_verno", "1");
            dct.Add("api_page_no", page.ToString());
            var answer = SendRequest("kcsapi/api_get_member/questlist", dct);

        }


        static HttpResponseMessage SendRequest(string apiFunc, Dictionary<string, string> data)
        {
            if (BaseAddres == string.Empty || apiToken == string.Empty)
                throw new Exception("Connection info is not set");

            try
            {
                using (HttpClient ht = new HttpClient())
                {

                    System.Net.ServicePointManager.Expect100Continue = false;
                    ht.BaseAddress = new Uri("http://" + BaseAddres);



                    ht.DefaultRequestHeaders.Add("Accept", "*/*");
                    ht.DefaultRequestHeaders.Add("Accept-Language", "ru-RU");
                    ht.DefaultRequestHeaders.Add("Referer", RefererHeader);
                    ht.DefaultRequestHeaders.Add("x-flash-version", "18,0,0,232");

                    ht.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                    ht.DefaultRequestHeaders.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)");
                    ht.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                    ht.DefaultRequestHeaders.Add("Pragma", "no-cache");


                    var formContent = new FormUrlEncodedContent(data);
                    var r = ht.PostAsync(apiFunc, formContent).Result;
                    LogWriter.WriteLog("Sending request to "+apiFunc);
                    return r;

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
            }
            return null;
        }
    }
}
