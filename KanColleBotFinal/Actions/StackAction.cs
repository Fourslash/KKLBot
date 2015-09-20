using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Actions
{
    abstract class StackAction
    {
        public virtual string info
        {
            get
            {
                return "nothing to see here";
            }
        }
        public bool isExecuting = false;
        public DateTime ExecuteDate;
        public virtual void Execute()
        {
            throw new AlgoritmicException(String.Format("Execute is not set for {0}", this.GetType()));
        }

    }
}
