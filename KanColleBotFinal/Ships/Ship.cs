using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace KanColleBotFinal.Ships
{

    public enum ShipTypes
    {
        UNKNOW=-1,
        DE=1,
        DD=2,
        CL=3,
        CLT=4,
        CA=5,
        CAV=6,
        CVL=6,
        BB=9,
        FastBB=8,
        BBV=10,
        CV=11,
        B=12,
        SS=13,
        SSV=14,
        AE=15,
        AV=16,
        LHA=17,
        CVB=18,
        AR=19,
        AS=20,
        CLp=21,
        AO=22,
        XX=100,
    }

    [Serializable]
    public class Ship
    {
        public Ship() { }
        public Ship(dynamic shp) 
        {
            try
            {
                if (shp.IsDefined("api_afterbull") == false)
                    return;
                RemodelBulletCost = Convert.ToInt32(shp.api_afterbull);
                RemodelFuellCost = Convert.ToInt32(shp.api_afterfuel);
                RemodelLvlRec = Convert.ToInt32(shp.api_afterlv);
                IdAfterRemodel = Convert.ToInt32(shp.api_aftershipid);
                Rarity = Convert.ToInt32(shp.api_backs);
                BuildTime = Convert.ToInt32(shp.api_buildtime);
                BulletsMax = Convert.ToInt32(shp.api_bull_max);
                FuelMax = Convert.ToInt32(shp.api_fuel_max);
                GetMessageString = shp.api_getmes;
                FirepowerBase = Convert.ToInt32(shp.api_houg[0]);
                FirepowerMax = Convert.ToInt32(shp.api_houg[1]);
                ID_Ship = Convert.ToInt32(shp.api_id);
                Range = Convert.ToInt32(shp.api_leng);
                LuckBase = Convert.ToInt32(shp.api_luck[0]);
                // api_maxeq= shp.api_maxeq; //придумать чтото
                KanjiName = shp.api_name;
                TorpedoBase = Convert.ToInt32(shp.api_raig[0]);
                TorpedoMax = Convert.ToInt32(shp.api_raig[1]);
                SlotAmountAvalivable = Convert.ToInt32(shp.api_slot_num);
                CardNumber = Convert.ToInt32(shp.api_sortno);
                ArmorBase = Convert.ToInt32(shp.api_souk[0]);
                ArmorMax = Convert.ToInt32(shp.api_souk[1]);
                //= Convert.ToInt32(shp.api_soku); //?

                try
                {
                    ShipType = (ShipTypes)Convert.ToInt32(shp.api_stype);
                }
                catch (Exception ex)
                {
                    ShipType = (ShipTypes)(-1);
                }


                HiraganaName = shp.api_yomi;
                EnglishName = Translation.Translation.TranslateShip(KanjiName);
            }
                
            catch(Exception ex)
            {
                ID_Ship = -1;
                HiraganaName = "ERROR";
                KanjiName = "ERROR";

                LogWriter.WriteLog(ex.Message);

            }

        }
        public Ship Clone()
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);

            IFormatter formatter2 = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (Ship)formatter2.Deserialize(stream);
        }

        public void CreateExempl (dynamic shp)
        {
             try
            {
            Rarity = Convert.ToInt32(shp.api_backs);
            Bullets = Convert.ToInt32(shp.api_bull);
            Fuel = Convert.ToInt32(shp.api_fuel);
            Fatigue = Convert.ToInt32(shp.api_cond);
            //Expirience = Convert.ToInt32(shp.api_exp);//? почему 3 числа
            ID_Fleet = Convert.ToInt32(shp.api_id);
            Evasion = Convert.ToInt32(shp.api_kaihi[0]);
            EvasionWithEquip = Convert.ToInt32(shp.api_kaihi[1]);
            FirepowerTotal = Convert.ToInt32(shp.api_karyoku[0]);
            FirepowerTotalMax = Convert.ToInt32(shp.api_karyoku[1]);
            Range = Convert.ToInt32(shp.api_leng);
            IsLocked = Convert.ToBoolean(shp.api_locked);
            IsEquipLocked = Convert.ToBoolean(shp.api_locked_equip);
            LuckTotalBase = Convert.ToInt32(shp.api_lucky[0]);
            LuckTotalMax = Convert.ToInt32(shp.api_lucky[1]);
            Level = Convert.ToInt32(shp.api_lv);
            HP_Max = Convert.ToInt32(shp.api_maxhp);
            RepairCostSteel= Convert.ToInt32(shp.api_ndock_item[0]);
            RepairCostFuell = Convert.ToInt32(shp.api_ndock_item[1]);
            RepairTime = Convert.ToInt32(shp.api_ndock_time);
            HP = Convert.ToInt32(shp.api_nowhp);
            //Equipement= Convert.ToInt32(shp.api_slot);//
            SlotsAvaliable = Convert.ToInt32(shp.api_slotnum);
           // RepairTime = Convert.ToInt32(shp.api_ndock_time);
            RepairTime = Convert.ToInt32(shp.api_ndock_time);
            }
             catch (Exception ex)
             {
                 ID_Ship = -1;
                 HiraganaName = "ERROR";
                 KanjiName = "ERROR";

                 LogWriter.WriteLog(ex.Message);

             }
        }
        public int Bullets { get; set; }
        public int Fuel { get; set; }
        public int Evasion { get; set; }
        public int EvasionWithEquip { get; set; }
        public int FirepowerTotal { get; set; }
        public int FirepowerTotalMax { get; set; }
        public bool IsLocked { get; set; }
        public bool IsEquipLocked { get; set; }
        public int LuckTotalBase { get; set; }
        public int LuckTotalMax{ get; set; }
        public int Level { get; set; }
        public int Fatigue { get; set; }
        public int Expirience { get; set; }
        public int ID_Fleet { get; set; }
        public int HP_Max { get; set; }
        public int HP { get; set; }
        public int RepairCostSteel{ get; set; }
        public int RepairCostFuell { get; set; }
        public int RepairTime { get; set; }
        public int SlotsAvaliable{ get; set; }

        

        public int RemodelBulletCost { get; set; }
        public int RemodelFuellCost { get; set; }
        public int RemodelLvlRec { get; set; }
        public int IdAfterRemodel { get; set; }
        public int Rarity { get; set; }
        public int BuildTime { get; set; }
        public int BulletsMax { get; set; }
        public int FuelMax { get; set; }
        public string GetMessageString { get; set; }
        public int FirepowerBase { get; set; }
        public int FirepowerMax { get; set; }
        public int ID_Ship { get; set; }
        public int Range { get; set; }
        public int [] api_maxeq  { get; set; }
        public string KanjiName { get; set; }
        public int TorpedoBase { get; set; }
        public int TorpedoMax { get; set; }
        public int SlotAmountAvalivable { get; set; }
        public int CardNumber { get; set; }
        public int ArmorBase { get; set; }
        public int ArmorMax { get; set; }
        public ShipTypes ShipType { get; set; }
        public string HiraganaName { get; set; }
        public int LuckBase { get; set; }



        public int Remodel { get; set; }

        
        
        public bool IsBulletsFull
        {
                get
            {
                if (BulletsMax - Bullets == 0)
                    return true;
                else
                    return false;
            }
        }
        
        
        public bool IsFuelFull 
        {
            get
            {
                if (FuelMax - Fuel == 0)
                    return true;
                else
                    return false;
            }
        }
        public bool IsSuplied
        {
            get
            {
                if (IsBulletsFull == true && IsFuelFull == true)
                    return true;
                else
                    return false;
            }
        }

        
        public bool IsDamaged
        {
            get
            {
                if (HP_Max - HP == 0)
                    return false;
                else
                    return true;
            }
        }

        public bool IsInFleet
        {
            get
            {
                foreach (Fleet fl in Dock.Fleets)
                {
                    if (fl.Ships.FindIndex(x => x.ID_Fleet == this.ID_Fleet) != -1)
                        return true;
                }
                return false;
            }

        }

        
        public string EnglishName { get; set; }
        
        
        public List<Items.Item> Equipement;
        
        


    }
}
