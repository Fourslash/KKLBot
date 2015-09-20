using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleBotFinal.Frames;

namespace KanColleBotFinal
{
    class Map
    {
        public static void Init()
        {
            CurrentFrame=new StartFrame();
            Frame.FrameLoaded += Frame_FrameLoaded;
        }



        static void Frame_FrameLoaded(Frame fr)
        {
            LogWriter.WriteLogSucces(String.Format("Frame {0} loaded", fr.GetType()));
            CurrentFrame = fr;
            //if (CurrentFrame is MainMenuFrame)
            //{
            //    //Task.Factory.StartNew(() =>
            //    ////DescisionMaker.SendExpedition(Dock.Fleets.Find(x => x.ID == 3))
            //    //OpenExpeditions()
            //    //);
            //}


            //if (CurrentFrame is Expeditions)
            //{
            //    //(CurrentFrame as Expeditions).StartExpedition(1, 2, 3);
            //    //CurrentFrame.OpenMainMenu();
            //    Task.Factory.StartNew(() =>
            //        //DescisionMaker.SendExpedition(Dock.Fleets.Find(x => x.ID == 3))
            //    OpenMainMenu());
            //}
        }
        public static Frame CurrentFrame;

        public static void ReloadMainMenu()
        {
            if (CurrentFrame is MainMenuFrame)
                CurrentFrame.OpenMissionsMain();
            CurrentFrame.OpenMainMenu();
        }

        public static void OpenExpeditions()
        {
            try
            {
                CurrentFrame.OpenExpeditions();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
            }
        }
        public static void OpenMainMenu()
        {
            try
            {
                CurrentFrame.OpenMainMenu();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
            }
        }


    }
}
