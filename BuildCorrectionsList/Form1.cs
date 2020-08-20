using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelperFunctionLibrary;

namespace BuildCorrectionsList
{
    public partial class Form1 : Form
    {
        List<CorrectionPoint> CorrectionPoints = new List<CorrectionPoint>();
        int formWidth, formHeight;
        public Form1()
        {
            InitializeComponent();

            SizeTheForm();

            DegreesRadiansConversion dr = new DegreesRadiansConversion(0, 0);
            double test = dr.DegToRad(180);
            Console.WriteLine("180 degrees = " + test.ToString());
        }

        public void SizeTheForm()
        {
            // Support multiple screens
            Screen screen = Screen.FromControl(this); //this is the Form class

            // no smaller than design time size
            //this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(800, 600);

            // no larger than screen size
            //this.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximumSize = new System.Drawing.Size(screen.Bounds.Width, screen.Bounds.Height);

            formWidth = this.MaximumSize.Width - 50;
            formHeight = this.MaximumSize.Height - 50;
            this.AutoSize = true;  //Form cannot be smaller than the panel size
            this.Size = new System.Drawing.Size(formWidth, formHeight);
            //Set form position in the screen
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(screen.Bounds.Left, screen.Bounds.Top);
        }

        public void CreateCorrectionList()
        {
            //CorrectionPoints = new List<CorrectionPoint>();


        }

        public void AddDataToList(string strData)
        {
            CorrectionPoint myPoint = new CorrectionPoint();
            string[] dataWords = strData.Split(',');
            myPoint.FileTimeInSecs = Convert.ToInt32(dataWords[0]);
            myPoint.Latitude = Convert.ToDouble(dataWords[1]);
            myPoint.Longitude = Convert.ToDouble(dataWords[2]);
            myPoint.VideoTimeInSecs = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);

            // Calculate distances


            CorrectionPoints.Add(myPoint);
            if (CorrectionPoints.Count == 1)
            {
                gridCorrectionsList.DataSource = CorrectionPoints;
            }
            //gridCorrectionsList.Update();
            gridCorrectionsList.Refresh();
            gridCorrectionsList.DataSource = null;
            gridCorrectionsList.DataSource = CorrectionPoints;
        }


        #region "Form Buttons"
        private void btnGetClipboardData_Click(object sender, EventArgs e)
        {
            string clipboardText = "";
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                clipboardText = Clipboard.GetText(TextDataFormat.Text);
                // Do whatever you need to do with clipboardText
                Console.WriteLine("I got :" + clipboardText);
                AddDataToList(clipboardText);
            }
        }
        private void btnLoadVideo_Click(object sender, EventArgs e)
        {
            string movie = @"C:\BikeAthlets\Peter Test\media\Chesco Training Loop - 4486.mp4";
            axWindowsMediaPlayer1.URL = movie;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }



        #endregion "Form Buttons" 

        #region "Event region"

        private void FormResizeEnded(object sender, EventArgs e)
        {
            SizeTheForm();
        }

        private void FolmLoaded(object sender, EventArgs e)
        {
            //showOnMonitor(1);
        }
        private void showOnMonitor(int showOnMonitor)
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            if (showOnMonitor >= sc.Length)
            {
                showOnMonitor = 0;
            }

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(sc[showOnMonitor].Bounds.Left, sc[showOnMonitor].Bounds.Top);
            // If you intend the form to be maximized, change it to normal then maximized.
            this.WindowState = FormWindowState.Normal;
            this.WindowState = FormWindowState.Normal;
        }

        private void MediaPlayerStateChanged(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            WMPLib.WMPPlayState cur_state = WMPLib.WMPPlayState.wmppsStopped;
            WMPLib.WMPPlayState prev_state = WMPLib.WMPPlayState.wmppsStopped;

            Console.WriteLine("state = " + axWindowsMediaPlayer1.playState);
            cur_state = axWindowsMediaPlayer1.playState;
            if (cur_state == prev_state) return;


            if (cur_state == WMPLib.WMPPlayState.wmppsPlaying && prev_state != WMPLib.WMPPlayState.wmppsPlaying)
            {
                //timer1.Start();
            }

            string state;
            switch (cur_state)
            {
                case WMPLib.WMPPlayState.wmppsStopped:
                    state = "Stopped";
                    //goon = true;
                    break;
                case WMPLib.WMPPlayState.wmppsPaused:
                    state = "Paused";

                    break;
                case WMPLib.WMPPlayState.wmppsPlaying:
                    state = "Playing";
                    break;
                case WMPLib.WMPPlayState.wmppsBuffering:
                    state = "Buffering";
                    break;
                case WMPLib.WMPPlayState.wmppsTransitioning:
                    state = "Transitioning";
                    break;
                case WMPLib.WMPPlayState.wmppsReady:
                    state = "Ready";
                    //goon = true;
                    break;
                default:
                    state = axWindowsMediaPlayer1.playState.ToString();
                    break;
            }
            prev_state = cur_state;
        }



        #endregion

    }
}
