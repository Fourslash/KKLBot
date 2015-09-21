using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.ResourceLogging
{
    [Serializable]
    public class ExpCompletedRecord : LoggerRecord
    {
        public Ships.Fleet fleet;
        public ExpCompletedRecord(Ships.Fleet fl, DateTime dt, ResourseChange ch):base(dt,ch)
        {
            fleet = fl;
        }
    }
}
