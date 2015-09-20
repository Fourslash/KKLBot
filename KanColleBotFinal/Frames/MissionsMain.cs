using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Frames
{
    class MissionsMain:Frame
    {
        static System.Drawing.Color MissionsMainLoadedColor = System.Drawing.Color.FromArgb(143, 181, 189);  // 567 123 
        static System.Drawing.Color ExpColor = System.Drawing.Color.FromArgb(255, 157, 69);  // 693 138
        public override bool isLoaded
        {
            get
            {
                return CheckColor(GetPixelColor(567, 123), MissionsMainLoadedColor);
            }
        }

        bool isExpButtonReady()
        {
            return CheckColor(GetPixelColor(693, 138), ExpColor);
        }
        private Frame ExpeditionClick()
        {
            LogWriter.WriteLog("Clicking Expeditions");
            SimulateClick(693, 138);
            return new Expeditions();
        }

        public override void OpenExpeditions()
        {
             WaitForReady(isExpButtonReady);
             ChangeFrame(ExpeditionClick);
        }

        private Frame MainMenuClick()
        {
            LogWriter.WriteLog("Clicking main menu");
            SimulateClick(40, 40);
            return new MainMenuFrame();
        }
        public override void OpenMainMenu()
        {
            ChangeFrame(MainMenuClick);
        }

        //public void Expeditions()
        //{
        //   WaitForReady(isExpButtonReady);
        //   ChangeFrame(ExpeditionClick);
        //    // Task.Factory.StartNew(() => 
        //}
    }
}
