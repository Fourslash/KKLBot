using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleBotFinal.Ships;

namespace KanColleBotFinal.Missions
{
    public class Requirement
    {
        public int id { get; set; }
        public ShipTypes ShipType { get; set; }

    }

    public class Expedition
    {

        public static List<Expedition> GetAllExpeditions()
        {
            List<Expedition> lst = new List<Expedition>
            {

                //1
                new Expedition
                {
                    ID=1,
                    SumOfLevels=0,
                    FlagshipLevel=1,
                    Requirements= new List<Requirement>
                    {
                        new Requirement {id=1, ShipType=ShipTypes.XX},
                        new Requirement {id=2, ShipType=ShipTypes.XX},
                    }

                },

                //2
                new Expedition
                {
                    ID=2,
                    SumOfLevels=0,
                    FlagshipLevel=2,
                     Requirements= new List<Requirement>
                    {
                        new Requirement {id=1, ShipType=ShipTypes.XX},
                        new Requirement {id=2, ShipType=ShipTypes.XX},
                        new Requirement {id=3, ShipType=ShipTypes.XX},
                        new Requirement {id=4, ShipType=ShipTypes.XX},
                    }

                },

                //3
                new Expedition
                {
                    ID=3,
                    FlagshipLevel=3,
                    SumOfLevels=0,
                     Requirements= new List<Requirement>
                    {
                        new Requirement {id=1, ShipType=ShipTypes.XX},
                        new Requirement {id=2, ShipType=ShipTypes.XX},
                        new Requirement {id=3, ShipType=ShipTypes.XX},
                    }
                },
                //4
                new Expedition
                {
                    ID=4,
                    FlagshipLevel=3,
                    SumOfLevels=0,
                    Requirements= new List<Requirement>
                    {
                        new Requirement {id=1, ShipType=ShipTypes.CL},
                        new Requirement {id=2, ShipType=ShipTypes.DD},
                        new Requirement {id=3, ShipType=ShipTypes.DD},
                    }
                },

                //5
                new Expedition
                {
                    ID=5,
                    FlagshipLevel=3,
                    SumOfLevels=0,
                     Requirements= new List<Requirement>
                    {
                        new Requirement {id=1, ShipType=ShipTypes.CL},
                        new Requirement {id=2, ShipType=ShipTypes.DD},
                        new Requirement {id=3, ShipType=ShipTypes.DD},
                        new Requirement {id=4, ShipType=ShipTypes.XX},
                    }
                },

                //6
                new Expedition
                {
                    ID=6,
                    FlagshipLevel=4,
                    SumOfLevels=0,
                     Requirements= new List<Requirement>
                    {
                        new Requirement {id=1, ShipType=ShipTypes.XX},
                        new Requirement {id=2, ShipType=ShipTypes.XX},
                        new Requirement {id=3, ShipType=ShipTypes.XX},
                        new Requirement {id=4, ShipType=ShipTypes.XX},
                    }
                },



                //37 TEST
                 new Expedition
                {
                    ID=37,
                    FlagshipLevel=30,
                    SumOfLevels=120,
                     Requirements= new List<Requirement>
                    {
                        new Requirement {id=1, ShipType=ShipTypes.DD},
                        new Requirement {id=2, ShipType=ShipTypes.DD},
                        new Requirement {id=3, ShipType=ShipTypes.DD},
                        new Requirement {id=4, ShipType=ShipTypes.DD},
                        new Requirement {id=5, ShipType=ShipTypes.DD},
                        new Requirement {id=6, ShipType=ShipTypes.CL},
                    }
                },



            };
            return lst;
        }


        public int ID { get; set; }
        public int FlagshipLevel { get; set; }
        public int SumOfLevels { get; set; }
        public List<Requirement> Requirements;
        public int PageInList
        {
            get
            {
                return ((ID - 1) / 8) + 1;
            }
        }
        public int RowInPage
        {
            get
            {
                return ((ID - 1) % 8) + 1;
            }
        }
        

    }
}
