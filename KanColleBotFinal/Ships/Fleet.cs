using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace KanColleBotFinal.Ships
{
     [Serializable]
    public class Fleet
    {
        public Fleet()
        {
            ID = -1;
        }
        public Fleet(dynamic fleet)
        {
            ID = Convert.ToInt32(fleet.api_id);
            MissionStatus  = Convert.ToInt32(fleet.api_mission[0]);


            MissionNumber  = Convert.ToInt32(fleet.api_mission[1]);
            MissionEndTime = DescisionMaker.UnixTimeStampToDateTime( Convert.ToDouble(fleet.api_mission[2])/1000);
            if (MissionStatus == 1)
                ActionStack.AddExpCheckAction(MissionEndTime);
            MissionUnknown = Convert.ToInt32(fleet.api_mission[3]);
            var ships = fleet.api_ship;
            Ships = new List<Ship>();
            foreach (var shp in ships)
            {
                int fID=Convert.ToInt32(shp);
                if (fID!=-1)
                    Ships.Add(Dock.Ships.Find(x=>x.ID_Fleet==fID));
            }
        }
        public Fleet Clone()
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);

            IFormatter formatter2 = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (Fleet)formatter2.Deserialize(stream);
        }




        public void FillFleetWithShips(Fleet fl)
        {
            int i=0;
            foreach (Ship sh in fl.Ships)
            {
                HttpSender.ChangeShipInFleet(this,i , sh.ID_Fleet);
                i++;
                System.Threading.Thread.Sleep(1000);
            }
            Map.ReloadMainMenu();

        }
        public void ClearShips()
        {
            foreach (Ship shp in Ships)
            {
                HttpSender.ChangeShipInFleet(this, 0, -1);
                System.Threading.Thread.Sleep(1000);
            }
            Map.ReloadMainMenu();

        }

        public bool IsOkForExp(Missions.Expedition exp )
        {
            if (Ships.Count == 0)
                return false;

            if (Ships[0].Level < exp.FlagshipLevel)
                return false;
            int sumLevel = 0;
            foreach (Ship shp in Ships)
                sumLevel += shp.Level;
            if (sumLevel < exp.SumOfLevels)
                return false;


            Fleet tmp = this.Clone();
            foreach (var type in exp.Requirements)
            {
                if (type.ShipType != ShipTypes.XX)
                {
                    if (tmp.Ships.FindIndex(x => x.ShipType == type.ShipType) == -1)
                        return false;
                    else
                    {
                        tmp.Ships.Remove(tmp.Ships.Find(x => x.ShipType == type.ShipType));
                    }
                }
                else
                {
                    if (tmp.Ships.Count == 0)
                        return false;
                    else
                        tmp.Ships.Remove(tmp.Ships[0]);
                }
            }
            return true;

        }



        public bool IsSuplied
        {
            get
            {
                if (Ships.Count(x => x.IsSuplied == false) == 0)
                    return true;
                else
                    return false;
            }
        }

        public string SupplyStatus
        {
            get
            {
                if (IsSuplied == true)
                    return "supplied";
                else
                    return "not supplied";

            }
        }

        public string returnTime
        {
            get
            {
                if (MissionEndTime.Date.CompareTo(DateTime.Now.Date) == 0)
                    return MissionEndTime.ToString("HH:mm:ss");
                else
                    return MissionEndTime.ToString("d MMM HH:mm:ss");
            }
        }

        public string Status
        {
            get
            {
                switch (MissionStatus)
                {
                    case 0:
                        {
                            return string.Format("Fleet №{0} is free and {1}", ID, SupplyStatus);
                        }
                    case 1:
                        {
                            return string.Format("Fleet №{0} is on {1} expedition. Return time: {2}",ID, MissionNumber, returnTime);
                        }
                    case 2:
                        {
                            return string.Format("Fleet №{0} returned from {1} expedition", ID, MissionNumber);
                        }
                    default:
                        {
                            return string.Format("something wrong with this fleet");
                        }
                }
            }
        }

        public int ID { get; set; }
        public List<Ship> Ships { get; set; }
        public int MissionStatus { get; set; }
        public int MissionNumber { get; set; }
        public DateTime MissionEndTime { get; set; }
        public int MissionUnknown { get; set; }



    }
}
