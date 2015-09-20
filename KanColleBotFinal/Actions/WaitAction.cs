using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Actions
{
    class WaitAction:StackAction
    {
        bool isStarted = false;
        const int MIN_WORK_TIME_HOURS=7;
        const int MAX_WORK_TIME_HOURS = 10;
        const int MIN_NAP_TIME_HOURS = 3;
        const int MAX_NAP_TIME_HOURS = 5;
        int time_ms;

        public override string info
        {
            get
            {
                if (isStarted==true)
                    return string.Format("Naping for {0} more minutes",tsLeft.TotalMinutes);
                else
                    return string.Format("Naping for {0} minutes at {1}", new TimeSpan(0,0,0,0,time_ms).TotalMinutes,ExecuteDate.ToString("HH:mm:ss"));
            }
        }
        TimeSpan tsLeft
        {
            get
            {
                DateTime dt = ExecuteDate.AddMilliseconds(time_ms);
                TimeSpan ts =  dt-DateTime.Now;
                return ts;
            }
        }

        public WaitAction(DateTime dt,int tm)
        {
            time_ms = tm;
            ExecuteDate = DateTime.Now;
        }
        public WaitAction()
        {
            Random r = new Random();
            int time = r.Next(MIN_WORK_TIME_HOURS* 60 * 60, MAX_WORK_TIME_HOURS * 60 * 60);
            time_ms = r.Next(MIN_NAP_TIME_HOURS * 60 * 60, MAX_NAP_TIME_HOURS * 60 * 60)*1000;
            ExecuteDate = DateTime.Now.AddSeconds(time);
        }
        
        public override void Execute()
        {
            isStarted = true;
            LogWriter.WriteLogSucces("Started nap");
            System.Threading.Thread.Sleep(time_ms);
            LogWriter.WriteLogSucces("Ended nap");
            //ActionStack.RemoveAction(this);
        }
    }
}
