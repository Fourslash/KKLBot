using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Frames
{
    class MainMenuFrame : Frame
    {
        static System.Drawing.Color IsLoadedColor = System.Drawing.Color.FromArgb(81, 199, 203);  //7 134
        static System.Drawing.Color MissionsMainButton = System.Drawing.Color.FromArgb(254, 254, 254);  //198 249  
        public override bool isLoaded
        {
            get
            {
                if (isExpReturned()==true)
                    GetReturnedExp();
                return CheckColor(GetPixelColor(7, 134), IsLoadedColor);
            }
        }

        private bool isMissionMainReady()
        {
                return CheckColor(GetPixelColor(198, 249), MissionsMainButton);
        }


        private Frame MissionsMainClick()
        {
            LogWriter.WriteLog("Clicking Missions Main");
            SimulateClick(198, 249);
            return new MissionsMain();
        }

        public override void OpenExpeditions()
        {
            WaitForReady(isMissionMainReady);
            ChangeFrame(MissionsMainClick);
            Map.OpenExpeditions();
        }
        public override void OpenMissionsMain()
        {
            WaitForReady(isMissionMainReady);
            ChangeFrame(MissionsMainClick);
        }
        public override void OpenMainMenu()
        {
            return;
        }

       // static System.Drawing.Color ExpeditionsReturnedColor = System.Drawing.Color.FromArgb(234, 228, 211);  //569 261
        static System.Drawing.Color ExpeditionsReturnedColor= System.Drawing.Color.FromArgb(57, 177, 160);  //521 27
        static System.Drawing.Color ExpRet2 = System.Drawing.Color.FromArgb(254, 231, 48);  //520 261
        static System.Drawing.Color ExpRet3 = System.Drawing.Color.FromArgb(32, 33, 35);  //75 425
        public bool isExpReturned()
        {
            return CheckColor(GetPixelColor(521, 27), ExpeditionsReturnedColor);
        }
        public bool isExpResults2()
        {
            return CheckColor(GetPixelColor(520, 261), ExpRet2);
        }
        public bool isExpResults3()
        {
            return CheckColor(GetPixelColor(75, 424), ExpRet3);
        }

        public void GetReturnedExp()
        {
            System.Threading.Thread.Sleep(3000);
            SimulateClick(569, 261);
            System.Threading.Thread.Sleep(7000);
            WaitForReady(isExpResults2);
            SimulateClick(569, 261);
            System.Threading.Thread.Sleep(3000);
            WaitForReady(isExpResults3);
            SimulateClick(569, 261);
            WaitForReady();
        }

        //public void MissionsMain()
        //{
        //    WaitForReady(isMissionMainReady);
        //    ChangeFrame(MissionsMainClick);
        //    //Task.Factory.StartNew(() => 
        //}
        //public void Expeditions()
        //{
        //    MissionsMain();
        //    Map.OpenExpeditions();
        //    // Task.Factory.StartNew(() => 
        //}
    }
}
