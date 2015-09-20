using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Actions
{
    class IdleAction:StackAction
    {
        public override string info
        {
            get
            {
                return string.Format("Idle");
            }
        }

        public override void Execute()
        {
            throw new AlgoritmicException("Cant execute idle action");
        }
    }
}
