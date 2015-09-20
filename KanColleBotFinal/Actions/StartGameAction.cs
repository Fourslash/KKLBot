using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Actions
{
    class StartGameAction:StackAction
    {
        public override string info
        {
            get
            {
                return string.Format("Starting the game");
            }
        }
        public StartGameAction()
        {
            ExecuteDate = DateTime.Now;
        }
        public override void Execute()
        {
            Map.OpenMainMenu();
            //ActionStack.RemoveAction(this);
        }
    }
}
