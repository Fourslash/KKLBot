using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace KanColleBotFinal.Frames
{
    class StartFrame:Frame
    {
        static System.Drawing.Color StartButtonColor = System.Drawing.Color.FromArgb(47, 165, 167); //600? 400  47, 168, 167)
        public override bool isLoaded
        {
            get
            {
                return CheckColor(GetPixelColor(600, 400), StartButtonColor);
            }
        }
        private Frame StartGameClick()
        {
            LogWriter.WriteLog("Clicking Start Game");
            SimulateClick(600, 400);
            return new MainMenuFrame();
        }
        public override void OpenMainMenu()
        {
            WaitForReady();
            ChangeFrame(StartGameClick);
            //Task.Factory.StartNew(() => 
        }
        public override void OpenExpeditions()
        {
            OpenMainMenu();
            Map.OpenExpeditions();
        }

    }
}
