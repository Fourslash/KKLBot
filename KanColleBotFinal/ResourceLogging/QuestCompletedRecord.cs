using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.ResourceLogging
{
    [Serializable]
    public class QuestCompletedRecord : LoggerRecord
    {
        public QuestCompletedRecord(DateTime dt, ResourseChange ch):base(dt,ch)
        {

        }
    }
}
