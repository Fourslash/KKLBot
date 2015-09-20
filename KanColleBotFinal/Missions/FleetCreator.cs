using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleBotFinal.Ships;

namespace KanColleBotFinal.Missions
{
    class FleetCreator
    {
        Expedition expedition;
        Fleet temp_fl = new Fleet();
        List<Ship> ShipsForThisExp;
        Dictionary<Requirement, Ship> Candidates;
        Dictionary<Requirement, List<Ship>> CandidatesArrays;


        public FleetCreator( Expedition exp)
        {
            expedition = exp;
            ShipsForThisExp = new List<Ship>( Dock.Ships.FindAll(x => x.IsInFleet == false).OrderBy(x=>x.FuelMax).ThenBy(x=>x.Level));
            Candidates = new Dictionary<Requirement, Ship>();
            foreach (var req in exp.Requirements)
            {
                Candidates.Add(req, new Ship { ID_Ship = -1 });
            }
        }


        public Fleet EquipForExpedition()
        {

            //1,2
           CreateReqShips();

            //3,4
            List<Requirement> Keys = new List<Requirement>(CandidatesArrays.Keys.ToArray());
            foreach (var key in Keys)
            {
                Ship tm = CandidatesArrays[key][0];
                Candidates[key] = tm;
                RemoveShip(tm);
            }

            //5
            if (IsFlagshipOk()==false)
            {
                //6
                List<Ship> ShipsForFlagship = ShipsForThisExp.FindAll(x => x.Level >= expedition.FlagshipLevel);
                if (ShipsForFlagship.Count == 0)
                    throw new AlgoritmicException(String.Format("Cant form fleet for the expedition {0}: Cant find flagsip",expedition.ID));
                foreach (var shp in ShipsForFlagship)
                {
                    foreach (var Req in expedition.Requirements)
                    {
                        if (shp.ShipType==Req.ShipType || Req.ShipType==ShipTypes.XX)
                        {
                            Ship ship_to_remove = Candidates[Req];
                            Candidates[Req] = shp;
                            RemoveShip(ship_to_remove);
                        }
                        if (IsFlagshipOk() == true)
                            break;
                        
                    }
                    if (IsFlagshipOk() == true)
                        break;
                }
                if (IsFlagshipOk() == false)
                    throw new AlgoritmicException(String.Format("Cant form fleet for the expedition {0}: Cant find flagsip", expedition.ID));
            }

            //7
            if (IsSumOk()==false)
            {
                //8
                while (ShipsForThisExp.Count!=0)
                {
                    int CurrentSum = Sum();
                    Ship CurrentCandidate = ShipsForThisExp[0];

                    List<Ship> CurrentCShips = new List<Ship>(Candidates.Values.ToArray());
                    CurrentCShips= new List<Ship>(CurrentCShips.OrderBy(x => x.Level));
                    foreach (var shp in CurrentCShips)
                    {
                        Requirement currentReq = Candidates.First(x => x.Value == shp).Key;
                        if (CurrentCandidate.Level>Candidates[currentReq].Level
                            && (CurrentCandidate.ShipType==currentReq.ShipType || currentReq.ShipType==ShipTypes.XX))
                        {
                            Candidates[currentReq] = CurrentCandidate;
                            break;
                        }
                    }
                    RemoveShip(CurrentCandidate);
                    if (IsSumOk() == true)
                        break;
                    if (ShipsForThisExp.Count==0)
                        throw new AlgoritmicException(String.Format("Cant form fleet for the expedition {0}: NOOOT ENOOUGH LEEVELS", expedition.ID));
                }
            }
            temp_fl.Ships = new List<Ship>(Candidates.Values.ToArray());
            temp_fl.Ships = new List<Ship>(temp_fl.Ships.OrderByDescending(x => x.Level));
            return temp_fl;
        }

        bool IsFlagshipOk()
        {
            List<Ship> TempLst = new List<Ship>(Candidates.Values.ToArray());
            TempLst = new List<Ship>(TempLst.OrderByDescending(x => x.Level));
            if( TempLst[0].Level < expedition.FlagshipLevel)
                return false;
            return true;
        }

        int Sum()
        {
            List<Ship> TempLst = new List<Ship>(Candidates.Values.ToArray());
            int sum = 0;
            foreach (var sh in TempLst)
            {
                sum += sh.Level;
            }
            return sum;
        }

        bool IsSumOk()
        {
            if (Sum() < expedition.SumOfLevels)
                return false;
            return true;
        }

        void RemoveShip(Ship shp)
        {
            ShipsForThisExp.Remove(shp);
            List<Requirement> Keys= new List<Requirement>(CandidatesArrays.Keys.ToArray());
            foreach (Requirement req in Keys)
            {
                CandidatesArrays[req].Remove(shp);
            }
        }

        void CreateReqShips()
        {
            CandidatesArrays = new Dictionary<Requirement, List<Ship>>();
            foreach (Requirement type in expedition.Requirements)
            {
                if (type.ShipType == ShipTypes.XX)
                    CandidatesArrays.Add(type, new List<Ship>(ShipsForThisExp.FindAll(x => x.Level > 0).OrderBy(x => x.FuelMax).ThenBy(x => x.Level).ToArray())); //KILL MEEEEE               
                else
                {
                    CandidatesArrays.Add(type, new List<Ship>(ShipsForThisExp.FindAll(x => x.ShipType == type.ShipType).OrderBy(x => x.FuelMax).ThenBy(x => x.Level).ToArray()));
                }
            }
        }


    }
}
