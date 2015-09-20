using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.Frames
{
    class Expeditions:Frame
    {
        static System.Drawing.Color IsLoadedColor = System.Drawing.Color.FromArgb(57, 61, 57);  //284 141  
        static System.Drawing.Color MissionCheckColor = System.Drawing.Color.FromArgb(116, 187, 177);  //
        static System.Drawing.Color MissionClickColor = System.Drawing.Color.FromArgb(24, 124, 132);  //715 442
        static System.Drawing.Color FleetSelectLoadedColor = System.Drawing.Color.FromArgb(239, 230, 216);  //710 431

        static System.Drawing.Color FleetReady = System.Drawing.Color.FromArgb(51, 51, 51);  //424 126 || 449 117
        static System.Drawing.Color FleetSelected = System.Drawing.Color.FromArgb(35, 159, 159);  //430 117 || 448 120

        static System.Drawing.Color StartButtonColor = System.Drawing.Color.FromArgb(240, 158, 43); //559 430
        static System.Drawing.Color FleetSentColor= System.Drawing.Color.FromArgb(234, 228, 211);  //569 261
        
        int zone = 1;



        private void OpenZone(int ZoneNum)
        {
            if (zone == ZoneNum)
                return;
            //x=75+60*ZoneNum
            SimulateClick(75+60*ZoneNum, 435);
            zone = ZoneNum;
            System.Threading.Thread.Sleep(1500);
        }

        void SelectFleet(int FleetNum)
        {
            if (FleetNum == 2)
            {
                LogWriter.WriteLogSucces("Second fleet selected");
                return;
            }
            else if (FleetNum == 3)
            {
                WaitForReady(IsThridFleetReady);
                SimulateClick(424, 126);
                WaitForReady(IsThridFleetSelected);
                LogWriter.WriteLogSucces("Thrid fleet selected");

            }
            else if (FleetNum == 4)
            {
                WaitForReady(IsFourthFleetReady);
                SimulateClick(449, 117);
                WaitForReady(IsFourthFleetSelected);
                LogWriter.WriteLogSucces("Fourth fleet selected");
            }
            else
                throw new AlgoritmicException(String.Format("Cant select fleet #{0} for the expedition",FleetNum));
        }
        private void SelectMission(int missionInPage)
        {
            int y = 140 + 30 * missionInPage;
            Random r = new Random();
            int x = 305 + r.Next(0, 130);
            WaitIsMissionSelectReady(x, y);
            SimulateClick(x, y);
            WaitForReady(IsMissonClickReady);
            SimulateClick(715, 442);
            WaitForReady(IsFleetSelectLoaded);
        }
        void StartMission()
        {
            WaitForReady(IsStartButtonReady);
            SimulateClick(559, 430);
            System.Threading.Thread.Sleep(8500);
            WaitForReady(IsFleetSent);
            LogWriter.WriteLogSucces("Expedition startred");
            
        }

        #region CheckFunktions
        bool IsStartButtonReady()
        {
            return CheckColor(GetPixelColor(559, 430), StartButtonColor);
        }
        bool IsFleetSent()
        {
            return CheckColor(GetPixelColor(569, 261), FleetSentColor);
        }
        bool IsThridFleetReady()
        {
            return CheckColor(GetPixelColor(424, 126), FleetReady);
        }
        bool IsThridFleetSelected()
        {
            return CheckColor(GetPixelColor(430, 117), FleetSelected);
        }
        bool IsFourthFleetReady()
        {
            return CheckColor(GetPixelColor(449, 117), FleetReady);
        }
        bool IsFourthFleetSelected()
        {
            return CheckColor(GetPixelColor(448, 120), FleetSelected);
        }

        bool IsFleetSelectLoaded()
        {
            return CheckColor(GetPixelColor(710, 431), FleetSelectLoadedColor);
        }
        bool IsMissonClickReady()
        {
            return CheckColor(GetPixelColor(715, 442), MissionClickColor);
        }
        #endregion


        public void StartExpedition(int ZoneNum, int MissionNum, int FleetNum)
        {
            try
            {
                OpenZone(ZoneNum);
                SelectMission(MissionNum);
                SelectFleet(FleetNum);
                StartMission();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void WaitIsMissionSelectReady(int x, int y)
        {
            
            int count = 0;
            while (CheckColor(GetPixelColor(x, y), MissionCheckColor) == false)
            {
                LogWriter.WriteLog(String.Format("Button ready check failed [{0}]. Waiting {1} MS and trying again", count + 1, SLEEP_MS));
                if (count == TRY_COUNT)
                    throw new AlgoritmicException("Button ready check failed in" + this.GetType());
                System.Threading.Thread.Sleep(SLEEP_MS);
                count++;
            }
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
        public override bool isLoaded
        {
            get
            {
                return CheckColor(GetPixelColor(284, 141), IsLoadedColor);
            }
        }
    }
}
