using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.ResourceLogging
{
    [Serializable]
    public class ResupplyRecord : LoggerRecord
    {
        public ResupplyRecord(Ships.Fleet fl,DateTime dt, ResourseChange ch):base(dt,ch)
        {
            fleet = fl;
        }
        public Ships.Fleet fleet;
    }
}
