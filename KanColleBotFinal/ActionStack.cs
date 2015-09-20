using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleBotFinal.Actions;
using System.Threading;

namespace KanColleBotFinal
{
    class ActionStack
    {

        static StackAction currentAction;
        public static StackAction CurrentAction
        {
            get
            {
                return currentAction;
            }
            private set
            {
                currentAction = value;
            }
        }
        const int WAITTIMEMS = 5000;
        public static List<StackAction> actionList = new List<StackAction>();
        public static string getInfo
        {
            get
            {
                int i=0;
                string result = "Current action: " + currentAction.info + "\n\n";
                foreach (StackAction st in actionList)
                {
                    result += string.Format("{0}) {1}\n",i,st.info);
                    i++;
                }
                return result;
            }
        }
        static void RemoveAction(StackAction act)
        {
           if (actionList.Remove(act)==false)
           throw new Exception("Cant remove action from stack after execurion");
        }
        public static void StartBot()
        {
            while(true)
            {
                actionList.Sort((x, y) => x.ExecuteDate.CompareTo(y.ExecuteDate));
                if (actionList.Count==0 ||isItTime(actionList[0].ExecuteDate)==false)
                {
                    if (actionList.Count(x => x.GetType() == typeof(WaitAction)) == 0)
                        AddAction(new WaitAction());


                    CurrentAction = new IdleAction();
                    DescisionMaker.AddExpeditions();
                    System.Threading.Thread.Sleep(WAITTIMEMS);

                   // continue;
                }
                else
                {
                    //if (actionList.Count(x => x.isExecuting == true) == 0)
                    //{
                    try
                    {
                        StackAction act = actionList[0];
                        act.isExecuting = true;
                        CurrentAction = act;
                        //LogWriter.WriteLogSucces("REMOVING ACTION");
                        RemoveAction(act);
                        //LogWriter.WriteLogSucces("STARTING ACTION");
                        act.Execute();
                    }
                    catch (Exception ex)
                    {
                        LogWriter.WriteLogOnException(ex);
                    }
                       
                    //}
                }
            }
        }
        static bool isItTime(DateTime date)
        {

            if (date.CompareTo(DateTime.Now) <= 0)
                return true;
            else
                return false;
        }
        public static void AddExpCheckAction(DateTime dt)
        {
            if (actionList.Count(x=>(x.ExecuteDate==dt)&&x.GetType()==typeof(CheckForReturns))==0)
            {
                AddAction(new CheckForReturns(dt,true));
            }

        }

        public static void AddAction(StackAction act)
        {
            try
            {
                actionList.Add(act);
                actionList.Sort((x,y)=>x.ExecuteDate.CompareTo(y.ExecuteDate));
                LogWriter.WriteLogSucces(string.Format("Action {0} added to stack. Time of execution: {1}", act.GetType(), act.ExecuteDate.ToString("hh:mm:ss")));
            }
            catch(Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
                AddAction(act);
            }
        }
        public static void Clear()
        {
            try
            {
                actionList.Clear();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
                throw new AlgoritmicException("Cant clear action stack on restart");
            }
        }
       
    }
}
