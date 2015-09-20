using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Actions
{
    class SendExpAction:StackAction
    {
        public override string info
        {
            get
            {
                return string.Format("Sending {0} fleet on expedition №{1}",fleet.ID,Exp.ID);
            }
        }
        Ships.Fleet fleet;
        //int expeditionNumber;
        Missions.Expedition Exp;
        public SendExpAction(Ships.Fleet fl, Missions.Expedition exp)
        {
            fleet = fl;
            //expeditionNumber = expNumber;
            Exp = exp;
            ExecuteDate = DateTime.Now;
        }
        public override void Execute()
        {
            DescisionMaker.SendExpedition(fleet, Exp);
            if (!(ActionStack.actionList[1] is SendExpAction))
                Map.OpenMainMenu();
            //ActionStack.RemoveAction(this);
        }

    }
}
