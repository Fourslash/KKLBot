using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleBotFinal.Ships;
using KanColleBotFinal.Items;
using KanColleBotFinal.Frames;
using KanColleBotFinal.ResourceLogging;
using Codeplex.Data;

namespace KanColleBotFinal
{
    class Dock
    {

        public static void Init()
        {
            KanColleProxy.NewDataCollected += KanColleProxy_NewDataCollected;
            
        }

        static void KanColleProxy_NewDataCollected(string jsonString, string apiString)
        {
            UpdateData(jsonString, apiString);
            LogWriter.WriteLogOnRequest(jsonString, apiString);
        }

        static List<Ship> AllTheShips = new List<Ship>();

        #region recouses

        public static ResourceLogging.ResourseChange ChangeResourses (int newFuel, int newAmmo, int newSteel,
            int newBaxite, int newInstantConstruction, int newInstantRepair, int newDevelopmentMaterial)
        {

            if (newFuel==0)
                newFuel=Fuel;
            if (newAmmo==0)
                newAmmo=Ammo;
            if (newSteel==0)
                newSteel=Steel;
            if (newBaxite==0)
                newBaxite=Bauxite;
            if (newInstantConstruction==0)
                newInstantConstruction=InstantConstruction;
            if (newInstantRepair==0)
                newInstantRepair=InstantRepair;
            if (newDevelopmentMaterial==0)
                newDevelopmentMaterial=DevelopmentMaterial;


            ResourceLogging.ResourseChange rc = new ResourceLogging.ResourseChange
            {
                oldFuel = Fuel,
                oldAmmo = Ammo,
                oldBauxite = Bauxite,
                oldSteel = Steel,
                oldDevelopmentMaterial = DevelopmentMaterial,
                oldInstantConstruction = InstantConstruction,
                oldInstantRepair = InstantRepair,
                newAmmo = newAmmo,
                newBauxite = newBaxite,
                newFuel = newFuel,
                newSteel = newSteel,
                newInstantRepair = newInstantRepair,
                newInstantConstruction = newInstantConstruction,
                newDevelopmentMaterial = newDevelopmentMaterial,
            };

            Fuel=newFuel;
            Ammo=newAmmo;
            Steel = newSteel;
            Bauxite = newBaxite;
            InstantRepair = newInstantRepair;
            InstantConstruction = newInstantConstruction;
            DevelopmentMaterial = newDevelopmentMaterial;
            return rc;

        }

        static int Fuel;
        static int Ammo;
        static int Steel;
        static int Bauxite;

        static int InstantConstruction;
        static int InstantRepair;
        static int DevelopmentMaterial;

        //static int Fuel
        //{
        //    get
        //    {
        //        return fuel;
        //    }
        //    set
        //    {
        //        fuel=value;
        //    }
        //}

        //static int Ammo
        //{
        //    get 
        //    {
        //        return ammo;
        //    }
        //}
        //static int Steel
        //{
        //    get
        //    {
        //        return steel;
        //    }
        //}


        //static int Bauxite
        //{
        //    get
        //    {
        //        return bauxite;
        //    }
        //}

        //static int InstantConstruction
        //{
        //    get
        //    {
        //        return instantConstruction;
        //    }
        //}

        //static int InstantRepair
        //{
        //    get
        //    {
        //        return instantRepair;
        //    }
        //}

        //static int DevelopmentMaterial
        //{
        //    get
        //    {
        //        return developmentMaterial;
        //    }
        //}



        public static string resourceInfo
        {
            get
            {
                string res = string.Empty;
                res += string.Format("Fuel: {0}\n",Fuel);
                res += string.Format("Ammo: {0}\n",Ammo);
                res += string.Format("Steel: {0}\n",Steel);
                res += string.Format("Bauxite: {0}\n",Bauxite);
                res += string.Format("Instant Repair: {0}\n",InstantRepair);
                res += string.Format("Instant Construction: {0}\n",InstantConstruction);
                res += string.Format("Development Material: {0}\n",DevelopmentMaterial);
                return res;
            }
        }
        #endregion


        public static List<Missions.Expedition> MyExpeditions = Missions.Expedition.GetAllExpeditions();

        public static List<Ship> Ships = new List<Ship>();
        public static List<Ship> FavoriteShips
        {
            get
            {
                return Ships.FindAll(x => x.IsLocked == true);
            }
        }

        public static List<Fleet> Fleets = new List<Fleet>();

        static List<Item> Items = new List<Item>();

       // static Frame CurrentFrame = new StartFrame();

        static void UpdateFleets(dynamic FleetsInfo)
        {
            Fleets=new List<Fleet>();
            foreach (var obj in FleetsInfo)
            {
                Fleets.Add(new Fleet(obj));
            }

        }

        static void UpdateShips(dynamic ShipArray)
        {
            Ships = new List<Ship>();
            foreach (var obj in ShipArray)
            {
                Ship tmp = AllTheShips.Find(x => x.ID_Ship == Convert.ToInt32(obj.api_ship_id)).Clone();
                tmp.CreateExempl(obj);
                Ships.Add(tmp);
            }
        }

        static void UpdateAllShips(dynamic ShipArray)
        {
            AllTheShips = new List<Ship>();
            foreach (var obj in ShipArray)
            {
                AllTheShips.Add(new Ship(obj));
            }
        }

        public static void UpdateData(string response, string apiLink)
        {

            try
            {
                var json = DynamicJson.Parse(response);
                

                switch (apiLink)
                {
                    case "/kcsapi/api_start2":
                        {
                            var allShips = json.api_data.api_mst_ship;
                            UpdateAllShips(allShips);


                            if (!(Map.CurrentFrame is StartFrame))
                                throw new AlgoritmicException("Error on start (wrong frame)");

                            else
                            {
#if !DEBUG 
                                ActionStack.AddAction(new Actions.StartGameAction());

                                Task.Factory.StartNew(() =>
                                ActionStack.StartBot()
                                );
#endif
                            }

                            


                          
                            break;
                        }
                    case "/kcsapi/api_req_member/get_incentive":
                        {
                            break;
                        }
                    case "/kcsapi/api_get_member/basic":
                        {
                            break;
                        }
                    case "/kcsapi/api_get_member/furniture":
                        {
                            break;
                        }
                    case "/kcsapi/api_get_member/slot_item":
                        {
                            break;
                        }
                    case "/kcsapi/api_get_member/useitem":
                        {
                            break;
                        }
                    case "/kcsapi/api_get_member/kdock":
                        {
                            break;
                        }
                    case "/kcsapi/api_get_member/unsetslot":
                        {
                            break;
                        }
                    case "/kcsapi/api_req_hensei/change":
                        {
                            int result = Convert.ToInt32(json.api_result);
                            if (result != 1)
                                throw new AlgoritmicException("Error on changing ships in fleet");

                            
                            break;
                        }
                    case "/kcsapi/api_req_hokyu/charge":
                        {
                            int result = Convert.ToInt32(json.api_result);
                            if (result != 1)
                                throw new AlgoritmicException("Error on suplying ships in fleet");
                            var FuelTemp = Convert.ToInt32(json.api_data.api_material[0]);
                            var AmmoTemp = Convert.ToInt32(json.api_data.api_material[1]);
                            var SteelTemp = Convert.ToInt32(json.api_data.api_material[2]);
                            var BauxiteTemp = Convert.ToInt32(json.api_data.api_material[3]);
                            var rc= ChangeResourses(FuelTemp, AmmoTemp, SteelTemp, BauxiteTemp, 0, 0, 0);

                            Fleet fleet = new Fleet();
                            try
                            {
                                foreach (var node in json.api_data.api_ship)
                                {
                                    int id = Convert.ToInt32(node.api_id);
                                    if (id == -1)
                                        continue;
                                    fleet = Fleets.Find(x => x.Ships.Count(y => y.ID_Fleet == id) > 0);
                                    if (fleet.ID > 0)
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                fleet = new Fleet();
                            }
                            ResLogger.AddRecord(new ResupplyRecord(fleet, DateTime.Now, rc));
                            break;
                        }
                    case "/kcsapi/api_port/port":
                        {
                           // Map.OpenExpeditions();
                            //
                            var ships = json.api_data.api_ship;
                            UpdateShips(ships);
                            /////
                            var flts=json.api_data.api_deck_port;
                            UpdateFleets(flts);
                            /////
                            Fuel = Convert.ToInt32(json.api_data.api_material[0].api_value);
                            Ammo = Convert.ToInt32(json.api_data.api_material[1].api_value);
                            Steel = Convert.ToInt32(json.api_data.api_material[2].api_value);
                            Bauxite = Convert.ToInt32(json.api_data.api_material[3].api_value);
                            InstantConstruction = Convert.ToInt32(json.api_data.api_material[4].api_value);
                            InstantRepair = Convert.ToInt32(json.api_data.api_material[5].api_value);
                            DevelopmentMaterial = Convert.ToInt32(json.api_data.api_material[6].api_value);

                            //HttpSender.SupplyFleet(Dock.Fleets.Find(x => x.ID == 2));
                            break;
                        }
                    case "/kcsapi/api_get_member/material":
                        {

                            ///////
                            //var FuelTemp = Convert.ToInt32(json.api_data[0].api_value);
                            //var AmmoTemp = Convert.ToInt32(json.api_data[1].api_value);
                            //var SteelTemp = Convert.ToInt32(json.api_data[2].api_value);
                            //var BauxiteTemp = Convert.ToInt32(json.api_data[3].api_value);
                            //var InstantConstructionTemp = Convert.ToInt32(json.api_data[4].api_value);
                            //var InstantRepairTemp = Convert.ToInt32(json.api_data[5].api_value);
                            //var DevelopmentMaterialTemp = Convert.ToInt32(json.api_data[6].api_value);
                            //ChangeResourses(FuelTemp, AmmoTemp, SteelTemp, BauxiteTemp, InstantConstructionTemp, InstantRepairTemp, DevelopmentMaterialTemp);
                            Fuel = Convert.ToInt32(json.api_data[0].api_value);
                            Ammo = Convert.ToInt32(json.api_data[1].api_value);
                            Steel = Convert.ToInt32(json.api_data[2].api_value);
                            Bauxite = Convert.ToInt32(json.api_data[3].api_value);
                            InstantConstruction = Convert.ToInt32(json.api_data[4].api_value);
                            InstantRepair = Convert.ToInt32(json.api_data[5].api_value);
                            DevelopmentMaterial = Convert.ToInt32(json.api_data[6].api_value);
                            
                            break;
                        }
                    case "/kcsapi/api_get_member/questlist":
                        {

                            DescisionMaker.QuestProcesser.UpdatePageInfo(json.api_data);
                            break;

                        }
                    case "/kcsapi/api_req_mission/start":
                        {

                            DateTime dt = DescisionMaker.UnixTimeStampToDateTime(Convert.ToDouble(json.api_data.api_complatetime) / 1000);
                            ActionStack.AddExpCheckAction(dt);
                            break;
                        }
                    case "/kcsapi/api_req_mission/result": //Экспа пройдена
                        {

                            int result = Convert.ToInt32(json.api_result);
                            if (result != 1)
                                throw new AlgoritmicException("Error on exp return result");
                            int expResult = Convert.ToInt32(json.api_clear_result);
                            if (expResult != 1)
                                throw new AlgoritmicException("Expedition failed!");

                            var FuelTemp = Convert.ToInt32(json.api_data.api_material[0]);
                            var AmmoTemp = Convert.ToInt32(json.api_data.api_material[1]);
                            var SteelTemp = Convert.ToInt32(json.api_data.api_material[2]);
                            var BauxiteTemp = Convert.ToInt32(json.api_data.api_material[3]);
                            var rc= ChangeResourses(FuelTemp, AmmoTemp, SteelTemp, BauxiteTemp, 0, 0, 0);
                            //List<int> ships = new List<int>();
                            Fleet fleet = new Fleet();
                            try
                            {
                                foreach (var node in json.api_data.api_ship_id)
                                {
                                    int id = Convert.ToInt32(node);
                                    if (id == -1)
                                        continue;
                                    fleet = Fleets.Find(x => x.Ships.Count(y => y.ID_Fleet == id) > 0);
                                    if (fleet.ID > 0)
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                fleet = new Fleet();
                            }
                            ResLogger.AddRecord(new ExpCompletedRecord(fleet, DateTime.Now, rc));
                            break;
                        }
                    case "/kcsapi/api_req_quest/clearitemget": //Квест пройден
                        {
                            int InstantRepairTemp=0;
                            int InstantConstructionTemp=0;
                            int DevelopmentMaterialTemp=0;
                            foreach (var node in json.api_data.api_bonuses)
                            {
                                try
                                {
                                    if (node.api_count)
                                    {
                                        int id = Convert.ToInt32(node.api_item.api_id);
                                        switch(id)
                                        {
                                            case 5: InstantConstructionTemp = Convert.ToInt32(node.api_count); break;
                                            case 6: InstantRepairTemp = Convert.ToInt32(node.api_count); break; 
                                            case 7: DevelopmentMaterialTemp = Convert.ToInt32(node.api_count); break;

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }

                            var FuelTemp = Convert.ToInt32(json.api_data.api_material[0]);
                            var AmmoTemp = Convert.ToInt32(json.api_data.api_material[1]);
                            var SteelTemp = Convert.ToInt32(json.api_data.api_material[2]);
                            var BauxiteTemp = Convert.ToInt32(json.api_data.api_material[3]);
                            var rc=ChangeResourses(FuelTemp, AmmoTemp, SteelTemp, BauxiteTemp, InstantConstructionTemp, InstantRepairTemp, DevelopmentMaterialTemp);
                            ResLogger.AddRecord(new QuestCompletedRecord(DateTime.Now, rc));
                            break;
                        }
                    default:
                        {
                            //throw exception here
                            LogWriter.WriteLog("Met unknown api path: "+apiLink);
                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
            }
        }

        

    }
}
