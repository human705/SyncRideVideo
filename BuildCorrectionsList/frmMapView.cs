using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;

using CommonLibrary;
using BuildCorrectionsList.Properties;

namespace BuildCorrectionsList
{
    public partial class frmMapView : Form
    {

        //public static GoldenCheetahRide oldRide = new GoldenCheetahRide();
        //public static MapView myOSMMap = new MapView();
        MapView myOSMMap = new MapView();
        //Add selected markers overlay  
        GMapOverlay selectedMarkers = new GMapOverlay("SelectedMarkers");

        public frmMapView()
        {
            InitializeComponent();
        }

        private void frmMapView_Load(object sender, EventArgs e)
        {

            try
            {
                RestoreWindowLocation();

                //MapView myOSMMap = new MapView();
                myOSMMap._myMap = gMapControl1;
                //Get route data table
                myOSMMap._oldRideData = Form1.dtOldRide;

                //Correction table from form1
                myOSMMap._cps = Form1.cps;

                // Test LatLng = New York
                myOSMMap._initLatLng = new PointLatLng(40.730610, -73.935242);
                // Get route begin and set it as map center
                double myLat = (double)myOSMMap._oldRideData.Rows[0][6];
                double myLng = (double)myOSMMap._oldRideData.Rows[0][7];
                myOSMMap._initLatLng = new PointLatLng(myLat, myLng);
                myOSMMap._mapZoom = 17;
                myOSMMap.SetMapDefaults();

                //Add route overlay
                GMapOverlay routes = new GMapOverlay("routes");
                //Add markers overlay  
                GMapOverlay markers = new GMapOverlay("markers");
                myOSMMap._markers = markers;

                //Add axis for altitude chart
                List<double> AltitideXAxis = new List<double>();
                myOSMMap._altDataX = AltitideXAxis;
                List<double> AltitideYAxis = new List<double>();
                myOSMMap._altDataY = AltitideYAxis;

                GMapRoute route = new GMapRoute(myOSMMap.CreateFullRoute(), "GC route");
                route.Stroke = new Pen(Color.Red, 3);
                routes.Routes.Add(route);
                gMapControl1.Overlays.Add(routes);

                //Add markers to map
                gMapControl1.Overlays.Add(markers);

                //Add selected markers to map
                myOSMMap._selectedMarkers = selectedMarkers;
                myOSMMap.AddSelectedMarkersToMap();

                //Add selected Markers  overlay
                gMapControl1.Overlays.Add(selectedMarkers);


                gMapControl1.Refresh();

                //Add Altitude chart
                formsPlot1.plt.PlotScatter(AltitideXAxis.ToArray(), AltitideYAxis.ToArray());
                formsPlot1.Render();
                

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


        private void frmMapView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveWindowLocation();
            //Form1.timerQMapLocs.Stop();
        }


        private void RestoreWindowLocation ()
        {
            if (Properties.Settings.Default.w1Max)
            {
                Location = Properties.Settings.Default.w1Loc;
                WindowState = FormWindowState.Maximized;
                Size = Properties.Settings.Default.w1Size;
            }
            else if (Properties.Settings.Default.w1Min)
            {
                Location = Properties.Settings.Default.w1Loc;
                WindowState = FormWindowState.Minimized;
                Size = Properties.Settings.Default.w1Size;
            }
            else
            {
                Location = Properties.Settings.Default.w1Loc;
                Size = Properties.Settings.Default.w1Size;
            }
        }
        private void SaveWindowLocation ()
        {
            if (WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.w1Loc = RestoreBounds.Location;
                Properties.Settings.Default.w1Size = RestoreBounds.Size;
                Properties.Settings.Default.w1Max = true;
                Properties.Settings.Default.w1Min = false;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.w1Loc = Location;
                Properties.Settings.Default.w1Size = Size;
                Properties.Settings.Default.w1Max = false;
                Properties.Settings.Default.w1Min = false;
            }
            else
            {
                Properties.Settings.Default.w1Loc = RestoreBounds.Location;
                Properties.Settings.Default.w1Size = RestoreBounds.Size;
                Properties.Settings.Default.w1Max = false;
                Properties.Settings.Default.w1Min = true;
            }
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Find the index og the given LatLng in the table
        /// </summary>
        /// <param name="_lat"></param>
        /// <param name="_lng"></param>
        /// <returns>Possition if found, otherwise -1</returns>
        private int GeoLocIndexAt (double _lat, double _lng)
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




        private void gMapControl1_OnMarkerDoubleClick(GMapMarker item, MouseEventArgs e)
        {
            double _lat = item.Position.Lat;
            double _lng = item.Position.Lng;
            
            int _index = GeoLocIndexAt(_lat, _lng);
            string s = $"At time: {_index.ToString()} Lat: {_lat.ToString()} and LON: {_lng.ToString()}";
            if (_index >= 0)
            {
                Console.WriteLine(s);
                //Center Map on point 
                gMapControl1.Position = new PointLatLng(_lat, _lng);
                string mapLocation = $"{_index.ToString()},{_lat.ToString()},{_lng.ToString()}";
                if (mapLocation != "") // If we have a location, update data grid
                {
                    Form1.mapLocation = mapLocation;
                    Form1.mapLocs.Enqueue(mapLocation);
                    AddSelectedMarkerToRoute(_lat, _lng);
                }
            } else
            {
                MessageBox.Show("Index NOT FOUND for LAT: "+ _lat.ToString() + " and LON: " + _lng.ToString());
                return;
            }

            
            
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && (string)item.Tag == "red")
            {

                var selectedOption = MessageBox.Show("Delete selected marker?", "Delete marker", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                // If the no button was pressed ...
                if (selectedOption == DialogResult.Yes)
                {
                    selectedMarkers.Markers.Remove(item);
                    Console.WriteLine("Removing marker:" + item.ToString());
                }
                else if (selectedOption == DialogResult.No)
                {
                    return;
                    //MessageBox.Show("No is pressed!", "No Dialog", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    return;
                    //MessageBox.Show("Cancel is pressed", "Cancel Dialog", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }
    }
}
