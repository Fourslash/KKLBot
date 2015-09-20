using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Actions
{
    class CheckQuestAction:StackAction
    {
        public override string info
        {
            get
            {
                return string.Format("Checking new quests {0}", ExecuteDate.ToString("HH:mm:ss"));
            }
        }
        public CheckQuestAction(DateTime dt)
        {

            ExecuteDate = dt;
        }
         public override void Execute()
        {
            Map.OpenMainMenu();
            DescisionMaker.QuestProcesser.CheckQuests();

            //ActionStack.RemoveAction(this);
        }
        
    }
}
