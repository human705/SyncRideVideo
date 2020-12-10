
//#define DEBUG
//#define TRACE  
//#undef TRACE

//#if (DEBUG)
//Console.WriteLine("Debugging is enabled.");  
//#endif  
  
//#if (TRACE)  
//     Console.WriteLine("Tracing is enabled.");  
//#endif 

#define TESTS

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
//using System.Configuration;

using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;

//NuGet installed dependencies
using Newtonsoft.Json;

// User defined Libraries
using CommonLibrary;
using HelperFunctionLibrary;
using System.Threading;
using System.Collections;

namespace BuildCorrectionsList
{
    public partial class Form1 : Form
    {
        List<CorrectionPoint> CorrectionPoints = new List<CorrectionPoint>();
        public static DataTable cps = new DataTable();
        public static DataTable dtOldRide = new DataTable();
        public static double VideoLingthInSecs = 0;

        static string activeProjectName = "";
        static string activeProjectPath = "";

        int formWidth, formHeight;
        static bool rideLoaded = false;
        static bool videoLoaded = false;
        static public string mapLocation = "";
        //static public Queue<string> mapLocs = new Queue<string>();

        public static GoldenCheetahRide oldRide = new GoldenCheetahRide();
        public static GoldenCheetahRide newRide = new GoldenCheetahRide();
        WMPLib.WMPPlayState cur_state = WMPLib.WMPPlayState.wmppsStopped;
        WMPLib.WMPPlayState prev_state = WMPLib.WMPPlayState.wmppsStopped;
        private int rowIndex;
        
        //Timer to get the video duration 
        System.Windows.Forms.Timer getMovieDurationTimer = new System.Windows.Forms.Timer();

        bool projectStateChanged = false;

        GMapMarker selectedBlueMarker, selectedRedMarker;
        ContextMenuStrip PopupMenu = new ContextMenuStrip();

        public Form1()
        {
            InitializeComponent();

            //SizeTheForm();

        }

        //public void SizeTheForm()
        //{
        //    // Support multiple screens
        //    Screen screen = Screen.FromControl(this); //this is the Form class
        //    // no larger than screen size
        //    //this.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        //    this.MaximumSize = new System.Drawing.Size(screen.Bounds.Width, screen.Bounds.Height);

        //    // no smaller than design time size
        //    //this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
        //    this.MinimumSize = new System.Drawing.Size(900, 700);

        //    formWidth = this.MaximumSize.Width - 50;
        //    formHeight = this.MaximumSize.Height - 50;
        //    this.AutoSize = false;  //Form cannot be smaller than the panel size
        //    this.Size = new System.Drawing.Size(formWidth, formHeight);
        //    //Set form position in the screen - Top Left
        //    this.StartPosition = FormStartPosition.Manual;
        //    this.Location = new Point(screen.Bounds.Left, screen.Bounds.Top);
        //    //PositionTheFormComponents(formWidth, formHeight);

        //}

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
        //public void AddDataToList(string strData)
        //{
        //    CorrectionPoint myPoint = new CorrectionPoint();
        //    GeoLoc currentPoint = new GeoLoc(0,0);
        //    try
        //    {
        //        string[] dataWords = strData.Split(',');
        //        myPoint.FileTimeInSecs = Convert.ToInt32(dataWords[0]);
        //        myPoint.Latitude = Convert.ToDouble(dataWords[1]);
        //        myPoint.Longitude = Convert.ToDouble(dataWords[2]);
        //        myPoint.VideoTimeInSecs = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
        //        currentPoint = new GeoLoc(myPoint.Latitude, myPoint.Longitude);

        //        // Get distance from old ride (not yet sync'ed)
        //        if (CorrectionPoints.Count != 0)
        //        {
        //            //Can't calculate distance from start because rout is not always a straight line.
        //            //GeoLocMath geoLocMath = new GeoLocMath();
        //            //GeoLoc startPoint = new GeoLoc(CorrectionPoints[0].Latitude, CorrectionPoints[0].Longitude);
        //            //myPoint.DistanceFromStart = geoLocMath.CalculateDistanceBetweenGeoLocations(startPoint, currentPoint);
        //            myPoint.DistanceFromStart = oldRide.RIDE.SAMPLES[myPoint.FileTimeInSecs].KM;
        //        }

        //        CorrectionPoints.Add(myPoint);
        //        if (CorrectionPoints.Count == 1)
        //        {
        //            gridCorrectionsList.DataSource = CorrectionPoints;
        //        }

        //        gridCorrectionsList.Refresh();
        //        gridCorrectionsList.DataSource = null;
        //        gridCorrectionsList.DataSource = CorrectionPoints;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }

        //}

        #region "Form Buttons"

        //private void btnGetClipboardData_Click(object sender, EventArgs e)
        //{
        //    projectStateChanged = true;
        //    string clipboardText = "";

        //    if (!videoLoaded || !rideLoaded)
        //    {
        //        MessageBox.Show("Ride and/or video not loaded yet!");
        //        return;
        //    }
            

        //    if (cur_state == WMPLib.WMPPlayState.wmppsPaused || cur_state == WMPLib.WMPPlayState.wmppsPlaying)
        //    {
        //        if (mapLocation != "")
        //        {
        //            lblFromClipboard.Text = mapLocation;
        //            AddRowToTable(mapLocation);
        //        } else if (Clipboard.ContainsText(TextDataFormat.Text))
        //        {
        //            clipboardText = Clipboard.GetText(TextDataFormat.Text);
        //            // Do whatever you need to do with clipboardText
        //            lblFromClipboard.Text = clipboardText;
        //            Console.WriteLine("I got :" + clipboardText);
        //            AddRowToTable(clipboardText);

        //        } else
        //        {
        //            MessageBox.Show("No data from the Application or the clipboard!!");
        //            return;
        //        }

        //    } else
        //    {
        //        MessageBox.Show("Media player not playing or paused!!!");
        //    }
        //    btnCreateNewRide.Enabled = true;

        //    // Go to the bottom of the grid.
        //    gridCorrectionsList.FirstDisplayedCell = gridCorrectionsList.Rows[gridCorrectionsList.Rows.Count - 1].Cells[0];

        //}
        private void btnLoadVideo_Click(object sender, EventArgs e)
        {
            //string movie = @"C:\BikeAthlets\Peter Test\media\Chesco Training Loop - 4486.mp4";
            string movie = "";

            DialogResult dr = new DialogResult();
            dr = openFileMovieDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //lblLoadVideo.Text = openFileMovieDialog.FileName;
                VideoNameLabel1.Text = openFileMovieDialog.FileName;
                movie = openFileMovieDialog.FileName;
                axWindowsMediaPlayer1.URL = movie;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                videoLoaded = true;
                //VideoLingthInSecs = axWindowsMediaPlayer1.currentMedia.duration;
                //axWindowsMediaPlayer1.Ctlcontrols.stop();
                getMovieDurationTimer.Start();
            } else if (dr == DialogResult.Cancel)
            {
                //lblLoadVideo.Text = "";
                VideoNameLabel1.Text = "";
                videoLoaded = false;
                Close();
            }
        }

        /// <summary>
        /// Use the timer to get the video duration and update the label
        /// </summary>
        private void GetDuration(object sender, EventArgs e)
        {
            // public variable songDuration declared elsewhere
            VideoLingthInSecs = axWindowsMediaPlayer1.currentMedia.duration;
            if (VideoLingthInSecs > 0) 
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                getMovieDurationTimer.Stop();
                //lblVideoTimeTotal.Text += VideoLingthInSecs.ToString();
                TotVideoTimeToolStripLabel1.Text += " = " + VideoLingthInSecs.ToString();
            }
            
        }
        private void BtnLoadRide_Click(object sender, EventArgs e)
        {
            if (rideLoaded)
            {
                MessageBox.Show("Ride already loaded!");
                return;
            }
            oldRide = null;
            DialogResult dr = new DialogResult();
            dr = openRideFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //lblRideName.Text = openRideFileDialog.FileName;
                RideNameLabel1.Text = openRideFileDialog.FileName;
                string injson = File.ReadAllText(openRideFileDialog.FileName);
                oldRide = JsonConvert.DeserializeObject<GoldenCheetahRide>(injson);
                DataTableOperations dto = new DataTableOperations();
                dtOldRide = dto.LoadTableFromGCRideList(oldRide);
                //dto.UpdateRideListSamplesFromTable(dtOldRide, oldRide);
                rideLoaded = true;
                frmOldRide newFrmOldRide = new frmOldRide();
                newFrmOldRide.Show();
            }
            else if (dr == DialogResult.Cancel)
                MessageBox.Show("User clicked Cancel button");
        }

//        private void btnCreateLatLons_Click(object sender, EventArgs e)
//        {
//            //string LatLonFileName = @"C:\coding\json\LatLonArray.txt";
//            string LatLonFileName = "";
//            FileStream llofstream;
//            StreamWriter llwriter;
//            try 
//            {
//                if (!rideLoaded)
//                {
//                    MessageBox.Show("No ride is loaded!", "ERROR");
//                    return;
//                }
//                SaveFileDialog saveFileLatLonsDialog1 = new SaveFileDialog();
//                saveFileLatLonsDialog1.InitialDirectory = @"C:\";
//                saveFileLatLonsDialog1.Title = "Save text Files";
//                saveFileLatLonsDialog1.CheckFileExists = false;
//                saveFileLatLonsDialog1.CheckPathExists = true;
//                saveFileLatLonsDialog1.DefaultExt = "txt";
//                saveFileLatLonsDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
//                saveFileLatLonsDialog1.FilterIndex = 1;
//                saveFileLatLonsDialog1.RestoreDirectory = true;
//                if (saveFileLatLonsDialog1.ShowDialog() == DialogResult.OK)
//                {
//                    LatLonFileName = saveFileLatLonsDialog1.FileName;
//                    llofstream = new FileStream(LatLonFileName, FileMode.Create);
//                    llwriter = new StreamWriter(llofstream);
//                    llwriter.AutoFlush = true;
//                    int cnt = 0;
//                    foreach (var item in oldRide.RIDE.SAMPLES)
//                    {
//                        llwriter.Write(item.LAT + "," + item.LON);
//                        if (cnt != oldRide.RIDE.SAMPLES.Count() - 1)
//                        {
//                            llwriter.WriteLine(",");
//                            cnt++;
//                        }
//                    }
//                    llwriter.Close();
//                }

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error: " + ex.Message);
//            }
//}

        private void btnSetVideoPositions_Click(object sender, EventArgs e)
        {
            string str;
            if (rideLoaded && videoLoaded)
            {
                int rowIndex = gridCorrectionsList.CurrentCell.RowIndex;
                str = gridCorrectionsList.Rows[rowIndex].Cells[1].Value.ToString();
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = Convert.ToDouble(str);
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void btnVideoReverse_Click(object sender, EventArgs e)
        {
            ChangeVideoPosition(-1);
        }

        private void btnVideoAdvance_Click(object sender, EventArgs e)
        {
            ChangeVideoPosition(1);
        }

        private void btnShowOldRide_Click(object sender, EventArgs e)
        {
            if (rideLoaded)
            {
                //ShowOldRideData(oldRide);
                frmOldRide newFrmOldRide = new frmOldRide();
                newFrmOldRide.Show();
            }
            else
            {
                MessageBox.Show("No Ride loaded!");
            }

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

        /// <summary>
        /// Method called by pressing the delete key with a selected row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserDeleteRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //int rowIndex = gridCorrectionsList.CurrentCell.RowIndex;
            //MessageBox.Show("Deleting row " + rowIndex.ToString());
            //gridCorrectionsList.Rows.RemoveAt(rowIndex);
        }

        private void FormResizeEnded(object sender, EventArgs e)
        {
            //SizeTheForm();

        }

        private void FormResizing(object sender, EventArgs e)
        {
            //int menuHeight = 20;
            //int formRows = 2;
            //int screenPadding = 45;  // Needed to prevent bottom control from rolling off the screen
            //int gutter = 3;
            //int nextY = 0;
            //int heightUnit = (this.Height - (menuHeight + screenPadding)) / formRows;

            ////Get current monitor size
            //Screen screen = Screen.FromControl(this); //this is the Form class
            //this.MaximumSize = new System.Drawing.Size(screen.Bounds.Width, screen.Bounds.Height);
            ////this.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            ////ROW 1 (one column)
            //Size size = new Size(0, 0);
            //size.Width = this.Width - 30;
            //size.Height = heightUnit;
            //gridCorrectionsList.Size = size;
            //nextY += menuHeight;
            //gridCorrectionsList.Location = new Point(5, nextY);

            ////ROW 2 (column 1)
            //size.Width = (int)(this.Width / 2);
            //axWindowsMediaPlayer1.Size = size;
            //nextY += gutter + heightUnit;
            //axWindowsMediaPlayer1.Location = new Point(5, nextY);

            ////ROW 2 (column 2)
            ////flowLayoutPanelLabels.Width = 270;
            //int nextX = axWindowsMediaPlayer1.Size.Width + gutter + 5;
            //flowLayoutPanelLabels.Location = new Point(nextX,nextY);
            //lblFromClipboard.Height = 30;
            //lblLoadVideo.Height = 30;
            //lblRideName.Height = 30;
            //btnLoadRide.Height = 30;
            //btnLoadVideo.Height = 30;
            //btnGetClipboardData.Height = 30;

            //btnVideoReverse.Height = 20;
            //btnVideoAdvance.Height = 20;
            //txtbVideoTimeChange.Height = 20;

            //lblVideoTimeInSecs.Height = 20;
            //btnSetVideoPositions.Height = 40;

            //lblVideoTimeTotal.Height = 20;

            //btnShowOldRide.Height = 30;

            ////ROW 2 (column 3)
            ////flowLayoutPanelButtons.Width = 220;
            ////btnLoadRide.Width = 210;
            //nextX += flowLayoutPanelLabels.Size.Width + gutter + 5;
            //flowLayoutPanelButtons.Location = new Point(nextX, nextY);

            ////ROW 3
            ////size.Width = this.Width - 30;
            ////DataGridOldRide.Size = size;
            ////nextY += gutter + heightUnit;
            ////DataGridOldRide.Location = new Point(5, nextY);
            ////DataGridOldRide.Visible = false;
        }

        private void FormLoaded(object sender, EventArgs e)
        {

            RestoreWindowLocation();
            //Clear Map Locations Q
            //mapLocs.Clear();
            //showOnMonitor(1);
            DataTableOperations dto = new DataTableOperations();
            cps = dto.CreateCorrectionPointsTable();
            getMovieDurationTimer.Tick += new EventHandler(GetDuration);
            getMovieDurationTimer.Interval = 100;
            btnCreateNewRide.Enabled = false;

            //LoadProjectState();
            //Load list of recent files 
            LoadRecentList();
            foreach (string item in MRUlist)
            {
                //populating menu
                ToolStripMenuItem fileRecent =
                 new ToolStripMenuItem(item, null, RecentFile_click);
                //add the menu to "recent" menu
                recentToolStripMenuItem.DropDownItems.Add(fileRecent);
            }

            //Start the queue timer
            //timerQMapLocs.Interval = 500;
            //timerQMapLocs.Start();

            BuildMapContextMenu();

            //BuildMapContextMenu();

        }
        //private void showOnMonitor(int showOnMonitor)
        //{
        //    Screen[] sc;
        //    sc = Screen.AllScreens;
        //    if (showOnMonitor >= sc.Length)
        //    {
        //        showOnMonitor = 0;
        //    }

        //    this.StartPosition = FormStartPosition.Manual;
        //    this.Location = new Point(sc[showOnMonitor].Bounds.Left, sc[showOnMonitor].Bounds.Top);
        //    // If you intend the form to be maximized, change it to normal then maximized.
        //    this.WindowState = FormWindowState.Normal;

        //}

        #endregion "Events Region"

        #region "Menu actions"
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            projectStateChanged = true;
            if (projectStateChanged)
            {
                DialogResult dialogAnswer = MessageBox.Show("Save changes to the project?",
                    "Save changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (dialogAnswer == DialogResult.Yes)
                {
                    saveProjectState();
                }
            } else
            {
                MessageBox.Show("No changes to the project. Save not needed.");
            }

        }


        /// <summary>
        /// Show dialog to save existing project changes
        /// </summary>
        private void saveProjectChanges()
        {
            DialogResult dialogAnswer = MessageBox.Show("Save changes to the project before loading?",
                "Project changed",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (dialogAnswer == DialogResult.Yes)
            {
                saveProjectState();
                LoadProjectState();
            }
            else if (dialogAnswer == DialogResult.No)
            {
                LoadProjectState();
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectStateChanged)
            {
                saveProjectChanges();
            } 
            LoadExistingProject();
        }

        private void clearGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gridCorrectionsList.DataSource = null;
            //gridCorrectionsList.Refresh();
            //CorrectionPoints = new List<CorrectionPoint>();

        }

        #endregion "Menu actions"

        #region "Media State Changes"
        private void MediaPlayerStateChanged(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //WMPLib.WMPPlayState cur_state = WMPLib.WMPPlayState.wmppsStopped;
            //WMPLib.WMPPlayState prev_state = WMPLib.WMPPlayState.wmppsStopped;

            //Console.WriteLine("state = " + axWindowsMediaPlayer1.playState);
            cur_state = axWindowsMediaPlayer1.playState;
            if (cur_state == prev_state) return;

            string state;
            switch (cur_state)
            {
                case WMPLib.WMPPlayState.wmppsStopped:
                    state = "Stopped";
                    timer1.Stop();
                    //goon = true;
                    break;
                case WMPLib.WMPPlayState.wmppsPaused:
                    state = "Paused";
                    lblVideoTimeInSecs.Text = "Video Secs: " + axWindowsMediaPlayer1.Ctlcontrols.currentPosition.ToString();
                    break;
                case WMPLib.WMPPlayState.wmppsPlaying:
                    state = "Playing";
                    timer1.Start();
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

        #endregion "Media State Changes"


        /// <summary>
        /// Execute delete action when the GRID is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripCorrectionsGrid_Click(object sender, EventArgs e)
        {
            //if (!this.gridCorrectionsList.Rows[this.rowIndex].IsNewRow)
            //{
            //    MessageBox.Show("Not implemented yet!");
            //    //this.gridCorrectionsList.Rows.RemoveAt(this.rowIndex);
            //}
        }


        /// <summary>
        /// Grid context menu DELETE click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Clicked on DELETE context menu item!");
            int rowIndex = gridCorrectionsList.CurrentCell.RowIndex;
            //MessageBox.Show("Deleting row " + rowIndex.ToString());

            double lat = (double)gridCorrectionsList.Rows[rowIndex].Cells["Latitude"].Value;
            double lng = (double)gridCorrectionsList.Rows[rowIndex].Cells["Longitude"].Value;

            foreach (GMapMarker marker in selectedMarkers.Markers)
            {
                if (marker.Tag.ToString() == "red" && marker.Position.Lat == lat && marker.Position.Lng == lng)
                {
                    selectedRedMarker = marker;
                }
            }

            //Find the Marker GeoLoc from the table
            //int _index = MarkerGeoLocIndexAt(_lat, _lng);

            //Delete selected Marker from overlay
            selectedMarkers.Markers.Remove(selectedRedMarker);

            //Delete marker from the table
            gridCorrectionsList.Rows.RemoveAt(rowIndex);
            gridCorrectionsList.Refresh();
            projectStateChanged = true;
        }


        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Show context menu created in design view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                lblVideoTimeInSecs.Text = "Video Secs: " + axWindowsMediaPlayer1.Ctlcontrols.currentPosition.ToString();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (projectStateChanged)
            {
                DialogResult dialogAnswer = MessageBox.Show("Save changes to the project before exiting?",
                    "Project changed",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (dialogAnswer == DialogResult.Yes)
                {
                    saveProjectState();
                }
            }
            SaveWindowLocation();
        }

        private void btnCreateNewRide_Click(object sender, EventArgs e)
        {
            double fromKm = -1;
            double toKm = -1;
            int videoTime = -1;
            int startMarkerTime = -1;
            int endMarkerTime = -1;
            int oldRideCnt = 0;
            int newRideCnt = 0;
            //List<SAMPLE> newRideList = new List<SAMPLE>();
            newRide.RIDE = new RIDE();
            newRide.RIDE.TAGS = new TAGS();
            //Initialize TAGS 
            newRide.RIDE.STARTTIME = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss") + " UTC ";
            newRide.RIDE.TAGS.Filename = "Chesco test-sync.json";
            newRide.RIDE.TAGS.SourceFilename = "Chesco test-sync.json";
            newRide.RIDE.TAGS.WorkoutCode = "Chesco test-sync";
            newRide.RIDE.SAMPLES = new List<SAMPLE>();

            List<SAMPLE> _testSegment = new List<SAMPLE>();

            for (int i = 0; i < cps.Rows.Count; i++)
            {
                try
                {
                    if (i > 0)  // Read the first 2 records so we can perform calculations between 2 points
                    {
                        fromKm = Convert.ToDouble(cps.Rows[i - 1]["DistanceFromStart"]);
                        toKm = Convert.ToDouble(cps.Rows[i]["DistanceFromStart"]);
                        videoTime = Convert.ToInt32(cps.Rows[i]["VideoTimeInSecs"]) - Convert.ToInt32(cps.Rows[i - 1]["VideoTimeInSecs"]);
                        //Debug.WriteLine(fromKm.ToString() + "," + toKm.ToString() + "," + videoTime.ToString());
                        NewRideProcessing newRideProcessing = new NewRideProcessing();
                        startMarkerTime = newRideProcessing.FindTimeForDistance(fromKm, ref oldRide);
                        endMarkerTime = newRideProcessing.FindTimeForDistance(toKm, ref oldRide);
                        //Debug.WriteLine(startMarkerTime.ToString() + "," + endMarkerTime.ToString());



                        if (startMarkerTime == -1 || endMarkerTime == -1)
                        {
                            MessageBox.Show("Error converting distance to time at row #: " + i.ToString());
                            return;
                        }

                        // Initalize the segment creation
                        CreateListForSegment createListForSegment = new CreateListForSegment(oldRide.RIDE.SAMPLES,
                            //newRide.RIDE.SAMPLES,
                            startMarkerTime,
                            endMarkerTime,
                            videoTime);

                        if (videoTime > (endMarkerTime - startMarkerTime))
                        {

                            createListForSegment.AddPointsToListProcess();
                            createListForSegment.AppendToDestList(newRide.RIDE.SAMPLES);

                            //Adding points to ride
                            //_testSegment = newRideProcessing.AddPointsToNewRide(videoTime, startMarkerTime, endMarkerTime, 
                            //    ref oldRideCnt, ref newRideCnt, oldRide, ref newRide);
                        }
                        else if (videoTime < (endMarkerTime - startMarkerTime))
                        {
                            //Removing points from ride
                            _testSegment = newRideProcessing.RemovePointsFromNewRide(videoTime, startMarkerTime, endMarkerTime, 
                                ref oldRideCnt, ref newRideCnt, oldRide, ref newRide);
                        } else if (videoTime == (endMarkerTime - startMarkerTime))
                        {

                            createListForSegment.AppendToDestList(newRide.RIDE.SAMPLES);

                            //_testSegment = newRideProcessing.MoveAllPointsToNewRide(videoTime, startMarkerTime, endMarkerTime, 
                            //    ref oldRideCnt, ref newRideCnt, oldRide, ref newRide);
                        }

                        // Copy items to newRide.RIDE.SAMPLES
                        _testSegment = createListForSegment.mTempList;

#if (TESTS)
                        // TESTS
                        Debug.Write("Record: " + i.ToString());

                        int rTime = Convert.ToInt32(cps.Rows[i]["FileTimeInSecs"]);
                        int vTime = -1;
                        if (i == 1) { vTime = videoTime; } else { vTime = videoTime - 1; }

                        //int vTime = Convert.ToInt32(cps.Rows[i]["VideoTimeInSecs"]);

                        bool checkLon, checkLat, checkKM, checkItemCount; 
                        checkLon = oldRide.RIDE.SAMPLES[rTime].LON == _testSegment[vTime].LON;
                        checkLat = oldRide.RIDE.SAMPLES[rTime].LAT == _testSegment[vTime].LAT;
                        checkKM = oldRide.RIDE.SAMPLES[rTime].KM == _testSegment[vTime].KM;


                        if (i == 1)
                        {
                            checkItemCount = vTime + 1 == _testSegment.Count(); 
                        } else
                        {
                            checkItemCount = vTime + 1 == _testSegment.Count();
                        }

                        if (checkLon && checkLat && checkKM && checkItemCount)
                        {
                            Debug.WriteLine(" Check PASSED ");
                        }
                        else
                        {
                            Debug.WriteLine(" *** Check FAILED *** ");
                        }

                        DataTableOperations dto = new DataTableOperations();
                        DataTable dt = new DataTable();
                        dt = dto.LoadTableFromList(newRide.RIDE.SAMPLES);
                        //Debug.WriteLine("Calling AddPointsToRide with parms: " + videoTime.ToString() + "," + startMarkerTime.ToString() +
                        //    "," + endMarkerTime.ToString() + ","+ oldRideCnt.ToString() + ", oldRide");  

#endif
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " \n \n row #: " + i.ToString() +"\n" + ex.StackTrace);
                    return;
                    //throw;
                }
            }
            //Re sequence list based on SECS
            ReSequenceSecsInRide(ref newRide);
            ShowNewRideData(newRide.RIDE.SAMPLES);
            WriteRideJSON(newRide);
        }

        private void ReSequenceSecsInRide(ref GoldenCheetahRide thisRide)
        {
            for (int j = 0; j < thisRide.RIDE.SAMPLES.Count; j++)
            {
                thisRide.RIDE.SAMPLES[j].SECS = j;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteRideJSON(GoldenCheetahRide thisRide)
        {
            //string outpath = @"C:\coding\tcx-gpx\SyncRideVideo\data\AlpeDHuez.json";
            string outPath = $"{ activeProjectPath }\\{ activeProjectName }.json";

            //Initialize TAGS 
            thisRide.RIDE.STARTTIME = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss") + " UTC ";
            thisRide.RIDE.RECINTSECS = 1;
            thisRide.RIDE.DEVICETYPE = "SyncRideVideo";
            thisRide.RIDE.IDENTIFIER = "TEST ID";
            thisRide.RIDE.TAGS.Filename = $"{ activeProjectName }.json";
            thisRide.RIDE.TAGS.SourceFilename = $"{ activeProjectName }.json";
            thisRide.RIDE.TAGS.WorkoutCode = $"{ activeProjectName }";

            string outjson = JsonConvert.SerializeObject(thisRide,
                Formatting.Indented,
            new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            File.WriteAllText(outPath, outjson);
        }

        /// <summary>
        /// Show the new ride in a grid
        /// </summary>
        private void ShowNewRideData(List<SAMPLE> thisNewRide)
        {
            //using (Form form = new Form())
            //{
            //} // Dispose form
            Form frmNewRideData = new Form();
            DataGridView dgViewNewRide = new DataGridView();
            dgViewNewRide.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgViewNewRide.DataSource = thisNewRide;
            dgViewNewRide.Refresh();
            frmNewRideData.Controls.Add(dgViewNewRide);
            dgViewNewRide.Location= new Point(5, 5);
            dgViewNewRide.Size = new System.Drawing.Size(800, 600);
            dgViewNewRide.AutoSize = false;
            frmNewRideData.AutoSize = true;
            frmNewRideData.Text = "New Ride Data";
            frmNewRideData.Size = new Size(820, 650);
            frmNewRideData.BackColor = Color.Red;
            //form.ShowDialog(this);
            frmNewRideData.Show(this);

        }



        private void  ShowOldRideData (DataTable thisOldRide)
        {
            Form frmSlowOldRideData = new Form();
            DataGridView dgViewOldRide = new DataGridView();
            dgViewOldRide.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgViewOldRide.DataSource = thisOldRide;
            dgViewOldRide.Refresh();
            frmSlowOldRideData.Controls.Add(dgViewOldRide);
            dgViewOldRide.Location = new Point(5, 5);
            dgViewOldRide.Size = new System.Drawing.Size(800, 600);
            dgViewOldRide.AutoSize = false;
            frmSlowOldRideData.AutoSize = true;
            frmSlowOldRideData.Text = "Old Ride Data";
            frmSlowOldRideData.Size = new Size(820, 650);
            frmSlowOldRideData.BackColor = Color.Blue;

            Button btnReWriteSecs = new Button();
            btnReWriteSecs.Text = "ReWrite Secs";
            btnReWriteSecs.Click += new EventHandler( this.btnReWriteSecs_click);
            btnReWriteSecs.BackColor = Color.Gray;
            frmSlowOldRideData.Controls.Add(btnReWriteSecs);
            btnReWriteSecs.Location = new Point(5,605);
            ////form.ShowDialog(this);
            frmSlowOldRideData.Show(this);
            //(new Thread(() => frmSlowOldRideData.ShowDialog())).Start();
        }

        /// <summary>
        /// Re-Write seconds from 0 to the length of the ride
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnReWriteSecs_click(object sender, EventArgs e)
        {
            //Get the button clicked
            Button btn = sender as Button;
            MessageBox.Show(btn.Name + " clicked"); // display button details
        }
        //private void btnReSequence_Click(object sender, EventArgs e)
        //{
        //    if (rideLoaded)
        //    {
        //    }
        //}


        private void saveProjectState()
        {
            if (activeProjectName == "")
            {
                CreateNewProject();
            }

            // Create dictionary
            DictionalyProcessing dp = new CommonLibrary.DictionalyProcessing();
            Dictionary<string, object> projectData = new Dictionary<string, object>();

            // Save corrections list
            TextConnector tc = new TextConnector();
            string fullPath = tc.FullFilePath("CorrectionData.csv", activeProjectName);
            DataTableOperations dto = new DataTableOperations();
            dto.WriteCorrectionPointstoCSV(fullPath, cps);
            //tc.SaveListToFile(fullPath, CorrectionPoints);

            //Save Video file and video position
            //dp.AddUpdateKey("VideoFileName", lblLoadVideo.Text, projectData);
            dp.AddUpdateKey("VideoFileName", VideoNameLabel1.Text, projectData);
            double videpPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            dp.AddUpdateKey("VideoPosition", videpPosition.ToString(), projectData);

            //Save ride file
            //dp.AddUpdateKey("RideFileName", lblRideName.Text, projectData);
            dp.AddUpdateKey("RideFileName", RideNameLabel1.Text, projectData);

            //Save dictionary to file
            fullPath = tc.FullFilePath("ProjectData.csv", activeProjectName);
            //projectData = dp.dict;
            dp.WriteDictionaryToCSV(projectData, fullPath);
        }

        private void correctionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CreateProjtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewProject();
            rideLoaded = false;
        }

        /// <summary>
        /// Show dialog for the user to pick a project to open
        /// </summary>
        private void LoadExistingProject()
        {
            string projectPath = ConfigurationManager.AppSettings["filePath"];
            DialogResult dr = new DialogResult();
            SelectFolderBrowserDialog.SelectedPath = projectPath;
            dr = SelectFolderBrowserDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                activeProjectPath = SelectFolderBrowserDialog.SelectedPath.ToString();
                DirectoryInfo dinfo = new DirectoryInfo(SelectFolderBrowserDialog.SelectedPath.ToString());
                activeProjectName = dinfo.Name;
                this.Text += " --- " + activeProjectName;
                LoadProjectState();
                SaveRecentFile(activeProjectName);
            }
            else if (dr == DialogResult.Cancel)
                MessageBox.Show("User clicked Cancel button");
        }

        private void CreateNewProject()
        {
            string projectPath = ConfigurationManager.AppSettings["filePath"];
            string projectName = "Project1";
            if (InputBox("New project", "New project name:", ref projectName) == DialogResult.OK)
            {
                //MessageBox.Show("Value = " + value.ToString());
                if (!Directory.Exists(projectPath + "\\" + projectName.ToString()))
                {
                    Directory.CreateDirectory(projectPath + "\\" + projectName.ToString());
                    activeProjectName = projectName;
                    activeProjectPath = projectPath;
                    this.Text += " - " + projectName;
                }
                else
                {
                    MessageBox.Show($"Project { projectName } exists. Use a ddifferent name");
                }
            }
        }


        /// <summary>
        /// Load selected project state if a project is selected.
        /// If a project is not selected, it will open a dialog
        /// </summary>
        private void LoadProjectState()
        {
            if (activeProjectName == "")
            {
                LoadExistingProject();
            }

            //Use table rather than List
            DataTableOperations dto = new DataTableOperations();
            TextConnector tc = new TextConnector();
            string fullPath = tc.FullFilePath("CorrectionData.csv", activeProjectName);
            if (!File.Exists(fullPath))
            {
                MessageBox.Show("Correction data file not found.");
                return;
            }

            activeProjectPath = tc.FullProjPath(activeProjectName);
            dto.LoadCorrectionPointsFromCSV(fullPath, cps);
            gridCorrectionsList.DataSource = cps;
            gridCorrectionsList.Refresh();

            if (gridCorrectionsList.Rows.Count > 0)
            {
                gridCorrectionsList.FirstDisplayedCell = gridCorrectionsList.Rows[gridCorrectionsList.Rows.Count - 1].Cells[0]; 
            }

            //Load Dictionaly from file
            fullPath = tc.FullFilePath("ProjectData.csv", activeProjectName);
            // Create dictionary
            DictionalyProcessing dp = new CommonLibrary.DictionalyProcessing();
            Dictionary<string, object> projectData = dp.LoadDictionaryFromCSV(fullPath);
            if (!File.Exists(fullPath))
            {
                MessageBox.Show("Correction data file not found.");
                return;
            }

            //Load Video
            string video = dp.GetAnyValue<string>("VideoFileName", projectData);
            //lblLoadVideo.Text = video;
            VideoNameLabel1.Text = video;
            axWindowsMediaPlayer1.URL = video;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = Convert.ToDouble(dp.GetAnyValue<string>("VideoPosition", projectData));
            axWindowsMediaPlayer1.Ctlcontrols.play();

            videoLoaded = true;
            //axWindowsMediaPlayer1.Ctlcontrols.pause();
            getMovieDurationTimer.Start();

            //Load Ride
            string rideName = dp.GetAnyValue<string>("RideFileName", projectData);
            string injson = File.ReadAllText(rideName);
            //lblRideName.Text = rideName;
            RideNameLabel1.Text = rideName;
            oldRide = JsonConvert.DeserializeObject<GoldenCheetahRide>(injson);
            dtOldRide = dto.LoadTableFromGCRideList(oldRide);
            //dto.UpdateRideListSamplesFromTable(dtOldRide, oldRide);
            LoadAndShowMap();
            rideLoaded = true;

            btnCreateNewRide.Enabled = true;
        }


        MapView myMap = new MapView();
        //Add selected markers overlay  
        GMapOverlay selectedMarkers = new GMapOverlay("SelectedMarkers");

        private void LoadAndShowMap()
        {
            try
            {
                RestoreWindowLocation();

                //MapView myOSMMap = new MapView();
                myMap._myMap = gMapControl1;
                //Get route data table
                myMap._oldRideData = Form1.dtOldRide;

                //Correction table from form1
                myMap._cps = Form1.cps;

                // Test LatLng = New York
                myMap._initLatLng = new PointLatLng(40.730610, -73.935242);
                // Get route begin and set it as map center
                double myLat = (double)myMap._oldRideData.Rows[0][6];
                double myLng = (double)myMap._oldRideData.Rows[0][7];
                myMap._initLatLng = new PointLatLng(myLat, myLng);
                myMap._mapZoom = 17;
                myMap.SetMapDefaults();

                //Add route overlay
                GMapOverlay routes = new GMapOverlay("routes");
                //Add markers overlay  
                GMapOverlay markers = new GMapOverlay("markers");
                myMap._markers = markers;

                //Add axis for altitude chart
                List<double> AltitideXAxis = new List<double>();
                myMap._altDataX = AltitideXAxis;
                List<double> AltitideYAxis = new List<double>();
                myMap._altDataY = AltitideYAxis;

                GMapRoute route = new GMapRoute(myMap.CreateFullRoute(), "GC route");
                route.Stroke = new Pen(Color.Red, 3);
                routes.Routes.Add(route);
                gMapControl1.Overlays.Add(routes);

                //Add markers to map
                gMapControl1.Overlays.Add(markers);

                //Add selected markers to map
                myMap._selectedMarkers = selectedMarkers;
                myMap.AddSelectedMarkersToMap();

                //Add selected Markers  overlay
                gMapControl1.Overlays.Add(selectedMarkers);

                gMapControl1.Refresh();

                //Change Map drag button 
                gMapControl1.DragButton = MouseButtons.Left;

                ////Add Altitude chart
                //formsPlot1.plt.PlotScatter(AltitideXAxis.ToArray(), AltitideYAxis.ToArray());
                //formsPlot1.Render();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Add a red marker at the LatLng and add it to the selected markers layer
        /// </summary>
        /// <param name="_lat"></param>
        /// <param name="_lng"></param>
        private void AddSelectedMarkerToRoute(double _lat, double _lng)
        {
            //Add marker
            GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(_lat, _lng),
                GMarkerGoogleType.red_small);
            // Add to marker overlay
            marker.Tag = "red";
            selectedMarkers.Markers.Add(marker);
            //selectedMarkers.Markers.Remove(marker);
        }

        /// <summary>
        /// Return the index of a Marker with the given LatLng in the table
        /// </summary>
        /// <param name="_lat"></param>
        /// <param name="_lng"></param>
        /// <returns>Possition if found, otherwise -1</returns>
        private int MarkerGeoLocSecIndexAt(double _lat, double _lng)
        {
            int _index = -1;
            int _sec;
            double _currLat, _currLng;
            try
            {
                DataTable _dt = new DataTable();
                _dt = Form1.dtOldRide;

                foreach (DataRow dr in _dt.Rows)
                {
                    _sec = (int)dr["secs"];
                    _currLat = (double)dr["lat"];
                    _currLng = (double)dr["lon"];

                    if (_currLat == _lat && _currLng == _lng)
                    {
                        _index = _sec;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            return _index;
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }


        ///MRUList

        Queue<string> MRUlist = new Queue<string>();
        int MRUnumber = 5;
        private void SaveRecentFile(string path)
        {
            //clear all recent list from menu
            recentToolStripMenuItem.DropDownItems.Clear();
            LoadRecentList(); //load list from file
            if (!(MRUlist.Contains(path))) //prevent duplication on recent list
                MRUlist.Enqueue(path); //insert given path into list
                                       //keep list number not exceeded the given value
            while (MRUlist.Count > MRUnumber)
            {
                MRUlist.Dequeue();
            }
            foreach (string item in MRUlist)
            {
                //create new menu for each item in list
                ToolStripMenuItem fileRecent = new ToolStripMenuItem
                             (item, null, RecentFile_click);
                //add the menu to "recent" menu
                recentToolStripMenuItem.DropDownItems.Add(fileRecent);
            }
            //writing menu list to file
            //create file called "Recent.txt" located on app folder
            string projectPath = ConfigurationManager.AppSettings["filePath"];
            if (!String.IsNullOrEmpty(projectPath))
            {
                StreamWriter stringToWrite =
                    new StreamWriter(projectPath + "\\Recent.txt");
                foreach (string item in MRUlist)
                {
                    stringToWrite.WriteLine(item); //write list to stream
                }
                stringToWrite.Flush(); //write stream to file
                stringToWrite.Close(); //close the stream and reclaim memory 
            } else
            {
                MessageBox.Show("Call to appsetings for filepath returned an empty string!");
                return;
            }
        }

        private void LoadRecentList()
        {//try to load file. If file isn't found, do nothing
            MRUlist.Clear();
            try
            {
                //read file stream
                string projectPath = ConfigurationManager.AppSettings["filePath"];
                if (File.Exists(projectPath + "\\Recent.txt"))
                {
                    StreamReader listToRead =
                    new StreamReader(projectPath + "\\Recent.txt");
                    string line;
                    while ((line = listToRead.ReadLine()) != null) //read each line until end of file
                        MRUlist.Enqueue(line); //insert to list
                    listToRead.Close(); //close the stream
                } else
                {
                    using (StreamWriter w = File.AppendText(projectPath + "\\Recent.txt")) ;
                }

            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Show Map Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If a ride is loaded show the form and start the timer to monitor the queue for correction points 
            if (rideLoaded) 
            {
                frmMapView newFrmMapView = new frmMapView();
                newFrmMapView.Show();
                //timerQMapLocs.Interval = 500;
                //timerQMapLocs.Start();
            }
            else
            {
                MessageBox.Show("No Ride loaded!");
            }
        }

        private void RecentFile_click(object sender, EventArgs e)
        {
            //just load a file
            activeProjectName = sender.ToString();
            if (projectStateChanged)
            {
                saveProjectChanges();
            }
            LoadProjectState();
        }



        private void RestoreWindowLocation()
        {
            if (Properties.Settings.Default.w0Max)
            {
                Location = Properties.Settings.Default.w0Loc;
                WindowState = FormWindowState.Maximized;
                Size = Properties.Settings.Default.w0Size;
            }
            else if (Properties.Settings.Default.w0Min)
            {
                Location = Properties.Settings.Default.w0Loc;
                WindowState = FormWindowState.Minimized;
                Size = Properties.Settings.Default.w0Size;
            }
            else
            {
                Location = Properties.Settings.Default.w0Loc;
                Size = Properties.Settings.Default.w0Size;
            }
        }

        //private void timerQMapLocs_Tick(object sender, EventArgs e)
        //{
        //    ProcessMapLocsQ();
        //}

        //private void ProcessMapLocsQ()
        //{
        //    while (mapLocs.Count > 0)
        //    {
        //        string m = mapLocs.Dequeue();
        //        AddRowToTable(m);
        //        Console.WriteLine("Object in queue: " + m);
        //        // Go to the bottom of the grid.
        //        gridCorrectionsList.FirstDisplayedCell = gridCorrectionsList.Rows[gridCorrectionsList.Rows.Count - 1].Cells[0];
        //        projectStateChanged = true;
        //    }
        //}

        private void gMapControl1_OnMarkerDoubleClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                double _lat = item.Position.Lat;
                double _lng = item.Position.Lng;

                int _index = MarkerGeoLocSecIndexAt(_lat, _lng);
                string s = $"At time: {_index.ToString()} Lat: {_lat.ToString()} and LON: {_lng.ToString()}";
                if (_index >= 0)
                {
                    Console.WriteLine(s);
                    //Center Map on point 
                    gMapControl1.Position = new PointLatLng(_lat, _lng);
                    mapLocation = $"{_index.ToString()},{_lat.ToString()},{_lng.ToString()}";
                    if (mapLocation != "") // If we have a location, update data grid
                    {
                        //mapLocs.Enqueue(mapLocation);
                        AddRowToTable(mapLocation);
                        AddSelectedMarkerToRoute(_lat, _lng);
                        // Go to the bottom of the grid.
                        gridCorrectionsList.FirstDisplayedCell = gridCorrectionsList.Rows[gridCorrectionsList.Rows.Count - 1].Cells[0];
                        projectStateChanged = true;
                    }
                }
                else
                {
                    MessageBox.Show("Index NOT FOUND for LAT: " + _lat.ToString() + " and LON: " + _lng.ToString());
                    return;
                } 
            }
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            selectedBlueMarker = item;
            if (e.Button == System.Windows.Forms.MouseButtons.Right && (string)item.Tag == "red")
            {
                //selectedMarkers.Markers.Remove(selectedMarker);
                selectedRedMarker = item;
                PopupMenu.Show(Cursor.Position);
            }
        }

        private void BuildMapContextMenu()
        {
            //ContextMenuStrip PopupMenu = new ContextMenuStrip();

            PopupMenu.BackColor = Color.OrangeRed;
            PopupMenu.ForeColor = Color.Black;
            PopupMenu.Text = "Context Menu";
            PopupMenu.Font = new Font("Georgia", 16);


            //The following code snippet adds a ContextMenuStrip control to the current Form and displays it when you right click on the Form.
            //this.ContextMenuStrip = PopupMenu;
            //PopupMenu.Show();

            PopupMenu.Name = "PopupMenu";
            PopupMenu.Font = new Font("Georgia", 16);
            PopupMenu.BackColor = Color.OrangeRed;
            PopupMenu.ForeColor = Color.Black;

            // Create the Delete Menu Item
            ToolStripMenuItem DeleteMenuItem = new ToolStripMenuItem("Delete");
            DeleteMenuItem.BackColor = Color.OrangeRed;
            DeleteMenuItem.ForeColor = Color.Black;
            DeleteMenuItem.Text = "Delete Marker";
            DeleteMenuItem.Font = new Font("Georgia", 16);
            DeleteMenuItem.TextAlign = ContentAlignment.BottomRight;
            DeleteMenuItem.ToolTipText = "Delete selected marker";

            //Add event Handler for new item
            PopupMenu.Items.Add(DeleteMenuItem);
            DeleteMenuItem.Click += new System.EventHandler(this.DeleteMapMenuItemClick);

            // Add background image --OPTIONAL
            //FileMenu.Image = Image.FromFile("C:\\Images\\Garden.jpg");
            //FileMenu.BackgroundImage = Image.FromFile("C:\\Images\\Garden.jpg");
            //FileMenu.BackgroundImageLayout = ImageLayout.Tile;

            // Rotate menu -- OPTIONAL
            //FileMenu.TextDirection = ToolStripTextDirection.Vertical90;

            // Create the GoogleMap Menu Item
            ToolStripMenuItem GoogleMapMenuItem = new ToolStripMenuItem("Google Map");
            GoogleMapMenuItem.BackColor = Color.OrangeRed;
            GoogleMapMenuItem.ForeColor = Color.Black;
            GoogleMapMenuItem.Text = "Open GoogleMap";
            GoogleMapMenuItem.Font = new Font("Georgia", 16);
            GoogleMapMenuItem.TextAlign = ContentAlignment.BottomRight;
            GoogleMapMenuItem.ToolTipText = "Open Google Map at the marker coordinates";

            //Add event Handler for new item
            PopupMenu.Items.Add(GoogleMapMenuItem);
            GoogleMapMenuItem.Click += new System.EventHandler(this.GoogleMapMenuItemClick);

            PopupMenu.GripStyle = ToolStripGripStyle.Visible;
        }

        private void DeleteMapMenuItemClick(object sender, EventArgs e)
        {
            //MessageBox.Show("Delete menu item clicked");

            if (selectedRedMarker.Tag.ToString() == "red")
            {
                var selectedOption = MessageBox.Show("Delete selected marker?", "Delete marker", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                // If the yes button was pressed ...
                if (selectedOption == DialogResult.Yes)
                {
                    selectedMarkers.Markers.Remove(selectedRedMarker);
                    Console.WriteLine("Removing marker at: " + selectedRedMarker.Position.Lat.ToString() + ", " + selectedRedMarker.Position.Lng.ToString());
                    //Console.WriteLine("Removing marker:");
                    DeleteGridRowAtGeoLoc(selectedRedMarker.Position.Lat, selectedRedMarker.Position.Lng);
                    projectStateChanged = true;
                }
                else if (selectedOption == DialogResult.No)
                {
                    selectedRedMarker = null;
                    return;
                    //MessageBox.Show("No is pressed!", "No Dialog", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    selectedRedMarker = null;
                    return;
                    //MessageBox.Show("Cancel is pressed", "Cancel Dialog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }

        }

        private void DeleteGridRowAtGeoLoc(double lat, double lng)
        {
            try
            {
                int gridIndex = MarkerGeoLocRowIndexAt(lat, lng);
                //Delete marker from the table
                if (gridIndex > 0)
                {
                    //MessageBox.Show($"Deleting grid row: { gridIndex.ToString() }");
                    gridCorrectionsList.Rows.RemoveAt(gridIndex); 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private int MarkerGeoLocRowIndexAt(double _lat, double _lng)
        {
            int _index = -1;
            int _row = 0;
            double _currLat, _currLng;
            try
            {
                //DataTable _dt = new DataTable();
                //_dt = Form1.dtOldRide;

                foreach (DataRow dr in cps.Rows)
                {
                    _currLat = (double)dr["Latitude"];
                    _currLng = (double)dr["Longitude"];
                    if (_currLat == _lat && _currLng == _lng)
                    {
                        _index = _row;
                    }
                    _row++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _index;
        }



        private void GoogleMapMenuItemClick(object sender, EventArgs e)
        {
            //MessageBox.Show("File menu item clicked");
            string _sLat = selectedBlueMarker.Position.Lat.ToString();
            string _sLng = selectedBlueMarker.Position.Lng.ToString();
            string _url = $"https://www.google.com/maps/@{ _sLat },{ _sLng },18.5z";
            //selectedMarker = null;
            System.Diagnostics.Process.Start(_url);
        }

        private void gMapControl1_OnMapClick(PointLatLng pointClick, MouseEventArgs e)
        {
            //MessageBox.Show("Clicked on MAP", "Cancel Dialog", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        private void SaveWindowLocation()
        {
            if (WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.w0Loc = RestoreBounds.Location;
                Properties.Settings.Default.w0Size = RestoreBounds.Size;
                Properties.Settings.Default.w0Max = true;
                Properties.Settings.Default.w0Min = false;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.w0Loc = Location;
                Properties.Settings.Default.w0Size = Size;
                Properties.Settings.Default.w0Max = false;
                Properties.Settings.Default.w0Min = false;
            }
            else
            {
                Properties.Settings.Default.w0Loc = RestoreBounds.Location;
                Properties.Settings.Default.w0Size = RestoreBounds.Size;
                Properties.Settings.Default.w0Max = false;
                Properties.Settings.Default.w0Min = true;
            }
            Properties.Settings.Default.Save();
        }







    }
}
