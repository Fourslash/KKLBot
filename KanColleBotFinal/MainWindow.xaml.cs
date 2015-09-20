#define OFF

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;



namespace KanColleBotFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Settings set = new Settings();
        public static MainWindow link;
        public MainWindow()
        {
            InitializeComponent();
//#if DEBUG
            ConsoleManager.Show();
            //var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            //dispatcherTimer.Tick += dispatcherTimer_Tick;
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer.Start();
//#endif

            set.Read();
            Missions.ExpeditionChanger.loadFromFile();
            Translation.Translation.LoadShipTranslation();
            SeijaCommunicator.Init();
            HttpSender.SetInfo(set.adress, set.api);
            LogWriter.StartSession();
            LogWriter.WriteLog("Initialising");
            DescisionMaker dmk = new DescisionMaker();
            _webbrowser.Source = new Uri(HttpSender.apiString);
            link = this;
        }

        Color GetNormalColor (System.Drawing.Color clr)
        {
            return Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
        }

        public void UpdateRectangles(System.Drawing.Color clr1, System.Drawing.Color clr2 )
        {
            MouseGrid.Background = new SolidColorBrush(GetNormalColor(clr1));
            _lbColorCurrent.Content=clr1.ToString();
            ControlGrid.Background = new SolidColorBrush(GetNormalColor(clr2));
            _lbColorExample.Content = clr2.ToString();
        }
        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Mouse.Capture(this);

            _lb_X.Content = "X:" + (int)Mouse.GetPosition(_canvas).X;
            _lb_Y.Content = "Y:" + (int)Mouse.GetPosition(_canvas).Y;
            //_lb_X.Content = "X:" + PointToScreen(Mouse.GetPosition(this)).X;
            //_lb_Y.Content = "Y:" + PointToScreen(Mouse.GetPosition(this)).Y;
            _lb_X_Copy.Content = (int)MainWindow.link.Left + (int)MainWindow.link._canvas.Margin.Left + (int)Mouse.GetPosition(_canvas).X+set.x_const /*+ 8*/;
            _lb_Y_Copy.Content = (int)MainWindow.link.Top + (int)MainWindow.link._canvas.Margin.Top + (int)Mouse.GetPosition(_canvas).Y + set.y_const /*+ 30*/;
            System.Drawing.Color clr1 = Frames.Frame.GetPixelColor((int)Mouse.GetPosition(_canvas).X, (int)Mouse.GetPosition(_canvas).Y);
            MouseGrid.Background = new SolidColorBrush(GetNormalColor(clr1));
            _lbColorCurrent.Content = clr1.ToString();
            Mouse.Capture(null);
            

        }

    



      




        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Fiddler.FiddlerApplication.Shutdown();
            LogWriter.WriteLog("Shutting down Fiddler from win closing");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShipChangeTest_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
    {
        //HttpSender.ChangeShipInFleet(Dock.Fleets[2], 0, -1);
        //Dock.Fleets[1].ClearShips();
       // DescisionMaker.SendExpedition(Dock.Fleets[1], Dock.MyExpeditions.Find(x => x.ID == 37));

        HttpSender.GetQuestList(5);
    }));
           
        }



        


    }
}
