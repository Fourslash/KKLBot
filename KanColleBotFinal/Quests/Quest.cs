using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Quests
{

    public enum QuestState {OnList=1,Activated=2,Completed=3 }
    public enum QuestType {OneTime=1, Daily=2, Weekly=3, Monthly=4 }

    class Quest
    {
        public Quest (dynamic qst)
        {
            ID = Convert.ToInt32(qst.api_no);
            State = (QuestState)Convert.ToInt32(qst.api_state);
            Type = (QuestType)Convert.ToInt32(qst.api_type);
            NameJP = qst.api_title;
            DescriptionJP = qst.api_detail;
            /*load translation*/

        }
        public QuestState State { get; set; }
        public QuestType Type { get; set; }
        public int ID { get; set; }
        public string NameJP { get; set; }
        public string NameEN { get; set; }
        public string DescriptionJP { get; set; }
        public string DescriptionEN { get; set; }
        
    }
}
