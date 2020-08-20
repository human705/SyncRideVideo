using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

//NuGet installed dependencies
using Newtonsoft.Json;

// User defined Libraries
using CommonLibrary;
using HelperFunctionLibrary;
using System.Runtime.CompilerServices;

namespace BuildCorrectionsList
{
    public partial class Form1 : Form
    {
        List<CorrectionPoint> CorrectionPoints = new List<CorrectionPoint>();
        int formWidth, formHeight;
        GoldenCheetahRide oldRide = new GoldenCheetahRide();
        public Form1()
        {
            InitializeComponent();

            SizeTheForm();

            


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
            this.AutoSize = false;  //Form cannot be smaller than the panel size
            this.Size = new System.Drawing.Size(formWidth, formHeight);
            //Set form position in the screen - Top Left
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(screen.Bounds.Left, screen.Bounds.Top);
            //PositionTheFormComponents(formWidth, formHeight);
        }

        public void PositionTheFormComponents(int w, int h)
        {

            //gridCorrectionsList.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
            //GridView at the top 3rd part of the screen full width
            Point startLocation = new Point(5, 5);
            Size size = new Size(w - 25, (int)(h * 0.3));
            gridCorrectionsList.Location = startLocation;
            gridCorrectionsList.Size = size;

            //Media Player at middle 3rd, left half
            startLocation.Y =  startLocation.Y + size.Height + 5;
            axWindowsMediaPlayer1.Location = startLocation;
            size.Width = (w - 25) / 2;
            axWindowsMediaPlayer1.Size = size;

            // DataGridoldRide - notsyncedride bottom third full width
            startLocation.Y = startLocation.Y + size.Height + 5;
            size.Width = w - 25;
            size.Height= (int)(h * 0.3);
            DataGridOldRide.Location = startLocation;
            DataGridOldRide.Size = size;

            // Buttons and text boxes = middle third right end

        }

        public void AddDataToList(string strData)
        {
            CorrectionPoint myPoint = new CorrectionPoint();
            string[] dataWords = strData.Split(',');
            myPoint.FileTimeInSecs = Convert.ToInt32(dataWords[0]);
            myPoint.Latitude = Convert.ToDouble(dataWords[1]);
            myPoint.Longitude = Convert.ToDouble(dataWords[2]);
            myPoint.VideoTimeInSecs = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
            GeoLoc currentPoint = new GeoLoc(myPoint.Latitude, myPoint.Longitude);

            // Calculate distances
            if (CorrectionPoints.Count != 0)
            {
                //Distance from start
                GeoLocMath geoLocMath = new GeoLocMath();
                GeoLoc startPoint = new GeoLoc(CorrectionPoints[0].Latitude, CorrectionPoints[0].Longitude);
                myPoint.DistanceFromStart = geoLocMath.CalculateDistanceBetweenGeoLocations(startPoint,currentPoint);
            }


            CorrectionPoints.Add(myPoint);
            if (CorrectionPoints.Count == 1)
            {
                gridCorrectionsList.DataSource = CorrectionPoints;
            }

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

        private void BtnLoadRide_Click(object sender, EventArgs e)
        {
            oldRide = null;
            DialogResult dr = new DialogResult();
            dr = openRideFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TxtbRideFileName.Text = openRideFileDialog.FileName;
                string injson = File.ReadAllText(openRideFileDialog.FileName);
                oldRide = JsonConvert.DeserializeObject<GoldenCheetahRide>(injson);
                DataGridOldRide.DataSource = oldRide.RIDE.SAMPLES;
                DataGridOldRide.Refresh();

            }
        }

        #endregion "Form Buttons" 

        #region "Event region"

        private void FormResizeEnded(object sender, EventArgs e)
        {
            //SizeTheForm();

        }

        private void FormResizing(object sender, EventArgs e)
        {
            int heightUnit = (this.Height - 15) / 3;
            
            Size size = new Size(0, 0);
            size.Width = this.Width - 30;
            size.Height = heightUnit;
            gridCorrectionsList.Size = size;
            gridCorrectionsList.Location = new Point(5, 5);

            DataGridOldRide.Size = size;
            DataGridOldRide.Location = new Point(5, (heightUnit * 2 + 15));

            size.Width = (int)(this.Width / 2);
            axWindowsMediaPlayer1.Size = size;
            axWindowsMediaPlayer1.Location = new Point(5, heightUnit + 10);
        }

        private void FormLoaded(object sender, EventArgs e)
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
