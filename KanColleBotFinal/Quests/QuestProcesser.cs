using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace KanColleBotFinal.Quests
{
    class QuestProcesser
    {

        List<int> FindThoseQuests = new List<int>
        {
            402,
            403,
            404,
        };
        
      public List<Quest> QuestInfo
        {
            get
            {
                List<Quest> lst = new List<Quest>();
                List<int> keys = new List<int>(Pages.Keys.ToArray());
                foreach (var key in keys)
                {
                    foreach (var quest in Pages[key])
                    {
                        if (quest.State != QuestState.OnList)
                            lst.Add(quest);
                    }
                }
                return lst;
            }
        }

        List<Quest> CompletedQuests = new List<Quest>();
        List<Quest> QuestToGet = new List<Quest>();
        Dictionary<int, List<Quest>> Pages = new Dictionary<int, List<Quest>>();
        int maxPgaes;
        int currentPage;
        public void UpdatePageInfo (dynamic page)
        {
            lock (this)
            {
                maxPgaes = Convert.ToInt32(page.api_page_count);
                currentPage = Convert.ToInt32(page.api_disp_page);
                List<Quest> qst = new List<Quest>();
                foreach (dynamic quest in page.api_list)
                {
                    qst.Add(new Quest(quest));
                }
                Pages[currentPage] = qst;
            }
        }

        List<Quest> FindQuestsToTake(int page)
        {
            List<Quest> lst = new List<Quest>();
            foreach (Quest qst in Pages[page])
            {
                if (FindThoseQuests.Contains(qst.ID))
                    lst.Add(qst);
            }
            return lst;

        }

        public void CheckQuests()
        {
            int crt = 1;
            do
            {
                HttpSender.GetQuestList(crt);
                Thread.Sleep(1000);
                crt = currentPage;

                QuestToGet.Clear();
                QuestToGet = FindQuestsToTake(crt);
                TakeQuests(crt);

                CompletedQuests.Clear();
                CompletedQuests = Pages[crt].FindAll(x => x.State == QuestState.Completed);
                CompleteQuests(crt);

                crt++;

            }
            while (currentPage != maxPgaes);
        }

        void TakeQuests(int page)
        {
            foreach (Quest qst in QuestToGet)
            {
                HttpSender.StartQuest(qst.ID);
                Thread.Sleep(1000);
                HttpSender.GetQuestList(currentPage);
            }
        }
        void CompleteQuests(int page)
        {
            foreach (Quest qst in CompletedQuests)
            {
                HttpSender.CompleteQuest(qst.ID);
                Thread.Sleep(1000);
                HttpSender.GetMaterials();
                Thread.Sleep(1000);
                HttpSender.GetQuestList(currentPage);
            }
        }

    }
}
