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
using System.Runtime.CompilerServices;
using System.Configuration;
using System.Diagnostics;

//NuGet installed dependencies
using Newtonsoft.Json;

// User defined Libraries
using CommonLibrary;
using HelperFunctionLibrary;




namespace BuildCorrectionsList
{
    public partial class Form1 : Form
    {
        List<CorrectionPoint> CorrectionPoints = new List<CorrectionPoint>();
        DataTable cps = new DataTable();

        int formWidth, formHeight;
        static bool rideLoaded = false;
        static bool videoLoaded = false;

        GoldenCheetahRide oldRide = new GoldenCheetahRide();
        WMPLib.WMPPlayState cur_state = WMPLib.WMPPlayState.wmppsStopped;
        WMPLib.WMPPlayState prev_state = WMPLib.WMPPlayState.wmppsStopped;
        private int rowIndex;

        public Form1()
        {
            InitializeComponent();

            SizeTheForm();
            Form f = this;
        }

        public void SizeTheForm()
        {
            // Support multiple screens
            Screen screen = Screen.FromControl(this); //this is the Form class
            // no larger than screen size
            //this.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximumSize = new System.Drawing.Size(screen.Bounds.Width, screen.Bounds.Height);

            // no smaller than design time size
            //this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(900, 700);

            formWidth = this.MaximumSize.Width - 50;
            formHeight = this.MaximumSize.Height - 50;
            this.AutoSize = false;  //Form cannot be smaller than the panel size
            this.Size = new System.Drawing.Size(formWidth, formHeight);
            //Set form position in the screen - Top Left
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(screen.Bounds.Left, screen.Bounds.Top);
            //PositionTheFormComponents(formWidth, formHeight);

        }

        //public void PositionTheFormComponents(int w, int h)
        //{

        //    //gridCorrectionsList.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
        //    //GridView at the top 3rd part of the screen full width
        //    Point startLocation = new Point(5, 5);
        //    Size size = new Size(w - 25, (int)(h * 0.3));
        //    gridCorrectionsList.Location = startLocation;
        //    gridCorrectionsList.Size = size;

        //    //Media Player at middle 3rd, left half
        //    startLocation.Y =  startLocation.Y + size.Height + 5;
        //    axWindowsMediaPlayer1.Location = startLocation;
        //    size.Width = (w - 25) / 2;
        //    axWindowsMediaPlayer1.Size = size;

        //    // DataGridoldRide - notsyncedride bottom third full width
        //    startLocation.Y = startLocation.Y + size.Height + 5;
        //    size.Width = w - 25;
        //    size.Height= (int)(h * 0.3);
        //    DataGridOldRide.Location = startLocation;
        //    DataGridOldRide.Size = size;

        //    // Buttons and text boxes = middle third right end

        //}

        public void AddRowToTable(string clipboardData)
        {
            DataRow dr = cps.NewRow();
            try
            {
                string[] rowValues = clipboardData.Split(',');
                dr["FileTimeInSecs"] = Convert.ToInt32(rowValues[0]);
                dr["VideoTimeInSecs"] = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                dr["Latitude"] = Convert.ToDouble(rowValues[1]);
                dr["Longitude"] = Convert.ToDouble(rowValues[2]);
                dr["DistanceFromStart"] = oldRide.RIDE.SAMPLES[Convert.ToInt32(rowValues[0])].KM;
                dr["DistanceFromPrevious"] = 0.0;
                cps.Rows.Add(dr); //add other rows

                gridCorrectionsList.DataSource = null;
                gridCorrectionsList.DataSource = cps;
                gridCorrectionsList.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        public void AddDataToList(string strData)
        {
            CorrectionPoint myPoint = new CorrectionPoint();
            GeoLoc currentPoint = new GeoLoc(0,0);
            try
            {
                string[] dataWords = strData.Split(',');
                myPoint.FileTimeInSecs = Convert.ToInt32(dataWords[0]);
                myPoint.Latitude = Convert.ToDouble(dataWords[1]);
                myPoint.Longitude = Convert.ToDouble(dataWords[2]);
                myPoint.VideoTimeInSecs = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                currentPoint = new GeoLoc(myPoint.Latitude, myPoint.Longitude);

                // Get distance from old ride (not yet sync'ed)
                if (CorrectionPoints.Count != 0)
                {
                    //Can't calculate distance from start because rout is not always a straight line.
                    //GeoLocMath geoLocMath = new GeoLocMath();
                    //GeoLoc startPoint = new GeoLoc(CorrectionPoints[0].Latitude, CorrectionPoints[0].Longitude);
                    //myPoint.DistanceFromStart = geoLocMath.CalculateDistanceBetweenGeoLocations(startPoint, currentPoint);
                    myPoint.DistanceFromStart = oldRide.RIDE.SAMPLES[myPoint.FileTimeInSecs].KM;
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        #region "Form Buttons"

        private void btnGetClipboardData_Click(object sender, EventArgs e)
        {
            string clipboardText = "";

            if (!videoLoaded || !rideLoaded)
            {
                MessageBox.Show("Ride and/or video not loaded yet!");
                return;
            }
            
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                if (cur_state == WMPLib.WMPPlayState.wmppsPaused || cur_state == WMPLib.WMPPlayState.wmppsPlaying)
                {
                    clipboardText = Clipboard.GetText(TextDataFormat.Text);
                    // Do whatever you need to do with clipboardText
                    lblFromClipboard.Text = clipboardText;
                    Console.WriteLine("I got :" + clipboardText);

                    //AddDataToList(clipboardText);
                    AddRowToTable(clipboardText);
                } else
                {
                    MessageBox.Show("Media player not playing or paused!!!");
                }

            } else
            {
                MessageBox.Show("No data in the clipboard!!");
            }
        }
        private void btnLoadVideo_Click(object sender, EventArgs e)
        {
            //string movie = @"C:\BikeAthlets\Peter Test\media\Chesco Training Loop - 4486.mp4";
            string movie = "";

            DialogResult dr = new DialogResult();
            dr = openFileMovieDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                lblLoadVideo.Text = openFileMovieDialog.FileName;
                movie = openFileMovieDialog.FileName;
                axWindowsMediaPlayer1.URL = movie;
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                videoLoaded = true;
            }
        }

        private void BtnLoadRide_Click(object sender, EventArgs e)
        {
            oldRide = null;
            DialogResult dr = new DialogResult();
            dr = openRideFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                lblRideName.Text = openRideFileDialog.FileName;
                string injson = File.ReadAllText(openRideFileDialog.FileName);
                oldRide = JsonConvert.DeserializeObject<GoldenCheetahRide>(injson);
                DataGridOldRide.DataSource = oldRide.RIDE.SAMPLES;
                DataGridOldRide.Refresh();
                rideLoaded = true;
            }
        }

        private void btnCreateLatLons_Click(object sender, EventArgs e)
        {
            //string LatLonFileName = @"C:\coding\json\LatLonArray.txt";
            string LatLonFileName = "";
            FileStream llofstream;
            StreamWriter llwriter;
            try 
            {
                if (!rideLoaded)
                {
                    MessageBox.Show("No ride is loaded!", "ERROR");
                    return;
                }
                SaveFileDialog saveFileLatLonsDialog1 = new SaveFileDialog();
                saveFileLatLonsDialog1.InitialDirectory = @"C:\";
                saveFileLatLonsDialog1.Title = "Save text Files";
                saveFileLatLonsDialog1.CheckFileExists = false;
                saveFileLatLonsDialog1.CheckPathExists = true;
                saveFileLatLonsDialog1.DefaultExt = "txt";
                saveFileLatLonsDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileLatLonsDialog1.FilterIndex = 1;
                saveFileLatLonsDialog1.RestoreDirectory = true;
                if (saveFileLatLonsDialog1.ShowDialog() == DialogResult.OK)
                {
                    LatLonFileName = saveFileLatLonsDialog1.FileName;
                    llofstream = new FileStream(LatLonFileName, FileMode.Create);
                    llwriter = new StreamWriter(llofstream);
                    llwriter.AutoFlush = true;
                    int cnt = 0;
                    foreach (var item in oldRide.RIDE.SAMPLES)
                    {
                        llwriter.Write(item.LAT + "," + item.LON);
                        if (cnt != oldRide.RIDE.SAMPLES.Count() - 1)
                        {
                            llwriter.WriteLine(",");
                            cnt++;
                        }
                    }
                    llwriter.Close();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
}

        private void btnSetVideoPositions_Click(object sender, EventArgs e)
        {
            string str;
            int rowIndex = gridCorrectionsList.CurrentCell.RowIndex;
            str = gridCorrectionsList.Rows[rowIndex].Cells[1].Value.ToString();
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = Convert.ToDouble(str);
            axWindowsMediaPlayer1.Ctlcontrols.play();

            //MessageBox.Show("Video time = " + str);
        }

        private void btnVideoReverse_Click(object sender, EventArgs e)
        {
            ChangeVideoPosition(-1);
        }

        private void btnVideoAdvance_Click(object sender, EventArgs e)
        {
            ChangeVideoPosition(1);
        }

        #endregion "Form Buttons" 

        #region "Helper methods"

        public void ChangeVideoPosition (int direction)
        {
            double videoTimeChange = 0.0;
            try
            {
                if (txtbVideoTimeChange.Text == "")
                {
                    videoTimeChange = 0.0;
                }
                else
                {
                    videoTimeChange = Convert.ToDouble(txtbVideoTimeChange.Text);
                }

                if (videoTimeChange > 0)
                {
                    switch (direction)
                    {
                        case 1:
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition += videoTimeChange;
                            break;
                        case -1:
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition -= videoTimeChange;
                            break;
                        default:
                            MessageBox.Show("Invalid direction entered.");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter an amount in seconds to reverse video");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        #endregion




        #region "Events Region"

        private void UserDeleteRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int rowIndex = gridCorrectionsList.CurrentCell.RowIndex;
            MessageBox.Show("Deleting row " + rowIndex.ToString());
            //gridCorrectionsList.Rows.RemoveAt(rowIndex);
        }

        private void FormResizeEnded(object sender, EventArgs e)
        {
            //SizeTheForm();

        }

        private void FormResizing(object sender, EventArgs e)
        {
            int menuHeight = 20;
            int formRows = 2;
            int screenPadding = 45;  // Needed to prevent bottom control from rolling off the screen
            int gutter = 3;
            int nextY = 0;
            int heightUnit = (this.Height - (menuHeight + screenPadding)) / formRows;

            //Get current monitor size
            Screen screen = Screen.FromControl(this); //this is the Form class
            this.MaximumSize = new System.Drawing.Size(screen.Bounds.Width, screen.Bounds.Height);
            //this.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            //ROW 1 (one column)
            Size size = new Size(0, 0);
            size.Width = this.Width - 30;
            size.Height = heightUnit;
            gridCorrectionsList.Size = size;
            nextY += menuHeight;
            gridCorrectionsList.Location = new Point(5, nextY);

            //ROW 2 (column 1)
            size.Width = (int)(this.Width / 2);
            axWindowsMediaPlayer1.Size = size;
            nextY += gutter + heightUnit;
            axWindowsMediaPlayer1.Location = new Point(5, nextY);

            //ROW 2 (column 2)
            //flowLayoutPanelLabels.Width = 270;
            int nextX = axWindowsMediaPlayer1.Size.Width + gutter + 5;
            flowLayoutPanelLabels.Location = new Point(nextX,nextY);
            lblFromClipboard.Height = 41;

            //ROW 2 (column 3)
            //flowLayoutPanelButtons.Width = 220;
            //btnLoadRide.Width = 210;
            nextX += flowLayoutPanelLabels.Size.Width + gutter + 5;
            flowLayoutPanelButtons.Location = new Point(nextX, nextY);

            //ROW 3
            //size.Width = this.Width - 30;
            //DataGridOldRide.Size = size;
            //nextY += gutter + heightUnit;
            //DataGridOldRide.Location = new Point(5, nextY);
            DataGridOldRide.Visible = false;
        }

        private void FormLoaded(object sender, EventArgs e)
        {
            //showOnMonitor(1);
            DataTableOperations dto = new DataTableOperations();
            cps = dto.CreateCorrectionPointsTable();

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

        #endregion "Events Region"

        #region "Menu actions"
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create dictionary
            DictionalyProcessing dp = new CommonLibrary.DictionalyProcessing();
            Dictionary<string, object> projectData = new Dictionary<string, object>();
            //dp.dict = projectData;
            //projectData = dp.LoadDictionaryFromCSV(@"C:\coding\tcx-gpx\SyncRideVideo\data\ProjectData.csv");

            // Save corrections list
            TextConnector tc = new TextConnector();
            string fullPath = tc.FullFilePath("CorrectionData.csv");
            DataTableOperations dto = new DataTableOperations();
            dto.WriteCorrectionPointstoCSV(fullPath, cps);
            //tc.SaveListToFile(fullPath, CorrectionPoints);
 
            //Save Video file and video position
            dp.AddUpdateKey("VideoFileName", lblLoadVideo.Text, projectData);
            double videpPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            dp.AddUpdateKey("VideoPosition", videpPosition.ToString(), projectData);

            int selectedMarker = 0;

            axWindowsMediaPlayer1.Ctlcontrols.currentMarker = selectedMarker;

            // TODO Save video position

            //Save ride file
            dp.AddUpdateKey("RideFileName", lblRideName.Text, projectData);

            //Save dictionary to file
            fullPath = tc.FullFilePath("ProjectData.csv");
            //projectData = dp.dict;
            dp.WriteDictionaryToCSV(projectData, fullPath);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //TextConnector correctionData = new TextConnector();
            //string fullPath = correctionData.FullFilePath("CorrectionData.csv");
            //CorrectionPoints = correctionData.ConvertToCorrectionPointsList(correctionData.LoadFile(fullPath));
            //gridCorrectionsList.DataSource = CorrectionPoints;
            //gridCorrectionsList.Refresh();

            //Use table rather than List
            DataTableOperations dto = new DataTableOperations();
            
            TextConnector tc = new TextConnector();
            string fullPath = tc.FullFilePath("CorrectionData.csv");
            dto.LoadCorrectionPointsFromCSV(fullPath, cps);
            gridCorrectionsList.DataSource = cps;
            gridCorrectionsList.Refresh();



            //Load Dictionaly from file
            fullPath = tc.FullFilePath("ProjectData.csv");
            // Create dictionary
            DictionalyProcessing dp = new CommonLibrary.DictionalyProcessing();
            Dictionary<string, object> projectData = dp.LoadDictionaryFromCSV(fullPath);

            //Load Video
            //string video = ConfigurationManager.AppSettings["videoPath"];
            //string video = @"C:\BikeAthlets\Peter Test\media\Chesco Training Loop - 4486.mp4";
            string video = dp.GetAnyValue<string>("VideoFileName", projectData);
            lblLoadVideo.Text = video;
            axWindowsMediaPlayer1.URL = video;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = Convert.ToDouble(dp.GetAnyValue<string>("VideoPosition", projectData));
            axWindowsMediaPlayer1.Ctlcontrols.play();
            
            videoLoaded = true;
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            //
            // TODO - Add Video position
            //


            //Load Ride
            //string fn = @"C:\coding\json\CHesco GPX.json";
            string rideName = dp.GetAnyValue<string>("RideFileName", projectData);
            string injson = File.ReadAllText(rideName);
            lblRideName.Text = rideName;
            oldRide = JsonConvert.DeserializeObject<GoldenCheetahRide>(injson);
            DataGridOldRide.DataSource = oldRide.RIDE.SAMPLES;
            DataGridOldRide.Refresh();
            rideLoaded = true;

        }

        private void clearGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridCorrectionsList.DataSource = null;
            gridCorrectionsList.Refresh();
            CorrectionPoints = new List<CorrectionPoint>();

        }

        #endregion "Menu actions"

        #region "State Changes"
        private void MediaPlayerStateChanged(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //WMPLib.WMPPlayState cur_state = WMPLib.WMPPlayState.wmppsStopped;
            //WMPLib.WMPPlayState prev_state = WMPLib.WMPPlayState.wmppsStopped;

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

        #endregion "State Changes"


        private void contextMenuStripCorrectionsGrid_Click(object sender, EventArgs e)
        {
            if (!this.gridCorrectionsList.Rows[this.rowIndex].IsNewRow)
            {
                MessageBox.Show("Not implemented yet!");
                //this.gridCorrectionsList.Rows.RemoveAt(this.rowIndex);
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gridCorrectionsList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.gridCorrectionsList.Rows[e.RowIndex].Selected = true;
                this.rowIndex = e.RowIndex;
                this.gridCorrectionsList.CurrentCell = this.gridCorrectionsList.Rows[e.RowIndex].Cells[1];
                this.contextMenuStripCorrectionsGrid.Show(this.gridCorrectionsList, e.Location);
                contextMenuStripCorrectionsGrid.Show(Cursor.Position);
            }
        }
    }
}
