using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Actions
{
    class CheckForReturns:StackAction
    {
        public override string info
        {
            get
            {
                return string.Format("Getting returned expedition at {0}",ExecuteDate.ToString("HH:mm:ss"));
            }
        }
        int delayMS=0;
        public CheckForReturns(DateTime dt)
        {
            ExecuteDate = dt;
        }
        public CheckForReturns(DateTime dt, int delay)
        {
            delayMS = delay;
            ExecuteDate = dt;
        }
        public CheckForReturns(DateTime dt, bool randomDelay)
        {
            if (randomDelay==true)
            {
                Random r = new Random();
                delayMS = r.Next(1 * 60, 15 * 60)*1000;
            }
            ExecuteDate = dt;
        }
        public override void Execute()
        {
            System.Threading.Thread.Sleep(delayMS);
            DescisionMaker.CheckForReturns();
            //ActionStack.RemoveAction(this);
        }
    }
}
