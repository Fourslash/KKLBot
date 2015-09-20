using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


using System.Runtime.InteropServices;
namespace KanColleBotFinal.Frames
{
    

    class Frame
    {

        public virtual bool isLoaded
        {
            get
            {
                return false;
            }
        }

        #region Click
        [DllImport("User32")]
        private static extern bool SetCursorPos(int X, int Y);
        protected static void setCursor(int X, int Y)
        {
                            MainWindow.link.Dispatcher.Invoke((Action)(() =>
    {
        int rX = (int)MainWindow.link.Left + (int)MainWindow.link._canvas.Margin.Left + X + MainWindow.set.x_const;
        int rY = (int)MainWindow.link.Top + (int)MainWindow.link._canvas.Margin.Top + Y + MainWindow.set.y_const;
            SetCursorPos(rX, rY);
    }));
           
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        protected void SimulateClick(int x, int y)
        {
            try
            {
                setCursor(x, y);
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                Thread.Sleep(500);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                //DoMouseClick();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
            }

        }
        #endregion
        public delegate void smp(Frame fr);
        public static event smp FrameLoaded;

        protected const int TRY_COUNT = 15;
        protected const int SLEEP_MS = 1000;

        //protected void ChangeFrame( Func<Frame> Click)
        //{
        //    Thread.Sleep(3000);
        //    int count = 0;
        //    while (isLoaded == false)
        //    {
        //        LogWriter.WriteLog(String.Format("Button ready check failed [{0}]. Waiting {1} MS and trying again", count + 1, SLEEP_MS));
        //        if (count == TRY_COUNT)
        //            throw new AlgoritmicException("Button ready check failed in" + this.GetType());
        //        Thread.Sleep(SLEEP_MS);
        //        count++;
        //    }
        //    Frame tmp = Click();
        //    count = 0;
        //    while (tmp.isLoaded == false)
        //    {
        //        LogWriter.WriteLog(String.Format("Frame loaded check failed [{0}]. Waiting {1} MS and trying again", count + 1, SLEEP_MS));
        //        if (count == TRY_COUNT)
        //            throw new AlgoritmicException("Frame loaded check failed in" + tmp.GetType());
        //        Thread.Sleep(SLEEP_MS);
        //        count++;
        //    }
        //    FrameLoaded(tmp);
        //}


        public virtual void OpenExpeditions()
        {
            throw new AlgoritmicException(String.Format("OpenExpeditions is not set for {0}", this.GetType()));
        }

        public virtual void OpenMainMenu()
        {
            throw new AlgoritmicException(String.Format("OpenMainMenu is not set for {0}", this.GetType()));
        }


        public virtual void OpenMissionsMain()
        {
            throw new AlgoritmicException(String.Format("OpenMissionsMain is not set for {0}", this.GetType()));
        }
        protected void WaitForReady(Func<bool> Check)
        {
            int count = 0;
            while (Check() == false)
            {

                LogWriter.WriteLog(String.Format("Button ready check failed [{0}]. Waiting {1} MS and trying again", count + 1, SLEEP_MS));
                if (count == TRY_COUNT)
                    throw new AlgoritmicException("Frame check failed in " + this.GetType());
                Thread.Sleep(SLEEP_MS);
                count++;
            }
        }
        protected void WaitForReady()
        {
            int count = 0;
            while (isLoaded== false)
            {

                LogWriter.WriteLog(String.Format("Button ready check failed [{0}]. Waiting {1} MS and trying again", count + 1, SLEEP_MS));
                if (count == TRY_COUNT)
                    throw new AlgoritmicException("Button ready check failed in " + this.GetType());
                Thread.Sleep(SLEEP_MS);
                count++;
            }
        }

        protected void ChangeFrame(Func<Frame> Click)
        {
            //Thread.Sleep(3000);
            int count = 0;
            
            Frame tmp=Click();
            count = 0;
            while (tmp.isLoaded== false)
            {
                LogWriter.WriteLog(String.Format("Frame loaded check failed [{0}]. Waiting {1} MS and trying again", count + 1, SLEEP_MS));
                if (count == TRY_COUNT)
                    throw new AlgoritmicException("Frame loaded check failed in " + tmp.GetType());
                Thread.Sleep(SLEEP_MS);
                count++;
            }
            FrameLoaded(tmp);
        }

        #region GetColor
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        /*protected*/ public static System.Drawing.Color GetPixelColor(int x, int y)
        {
            System.Drawing.Color color= System.Drawing.Color.FromArgb(0,0,0);
            try
            {
                int rX,rY;
                MainWindow.link.Dispatcher.Invoke((Action)(() =>
    {
        rX = (int)MainWindow.link.Left + (int)MainWindow.link._canvas.Margin.Left + x + MainWindow.set.x_const;
        rY = (int)MainWindow.link.Top + (int)MainWindow.link._canvas.Margin.Top + y + MainWindow.set.y_const;
        setCursor(x, y);
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, rX, rY);
            ReleaseDC(IntPtr.Zero, hdc);
            color = System.Drawing.Color.FromArgb((int)(pixel & 0x000000FF),
                  (int)(pixel & 0x0000FF00) >> 8,
                  (int)(pixel & 0x00FF0000) >> 16);
    }));
            
            }
            catch (Exception ex)
            {
                LogWriter.WriteLogOnException(ex);
            }
            return color;
        }
        #endregion

        protected static bool CheckColor(System.Drawing.Color color1, System.Drawing.Color color2)
        {
     MainWindow.link.Dispatcher.Invoke((Action)(() =>
    {
            MainWindow.link.UpdateRectangles(color1, color2);
    }));
     LogWriter.WriteLog("Comparing colors");
            if (Math.Abs(color1.B - color2.B) > 5 ||
                Math.Abs(color1.G - color2.G) > 5 ||
                Math.Abs(color1.R - color2.R) > 5)
                return false;
            return true;
        }

    }
}
