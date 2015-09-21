using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.ResourceLogging
{
    [Serializable]
    public abstract class LoggerRecord
    {
        public DateTime Date;
        public ResourseChange Change;
        public LoggerRecord(DateTime dt, ResourseChange ch)
        {
            Date = dt;
            Change = ch;
        }


    }
}
