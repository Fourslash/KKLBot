using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.ResourceLogging
{
    class ChangeCounter
    {
        public int fuelChange=0;
        public int ammoChange = 0;
        public int steelChange=0;
        public int bxChange = 0;

        public int constrChange = 0;
        public int repChange=0;
        public int devChange = 0;

        public void Add(ResourseChange rc)
        {
            fuelChange += rc.fuelChange;
            ammoChange += rc.ammoChange;
            steelChange += rc.steelChange;
            bxChange += rc.bauxiteChange;
            devChange+= rc.developmentMaterialChange;
            constrChange+= rc.instantConstructionChange;
            repChange+= rc.instantRepairChange;
        }

    }
}
