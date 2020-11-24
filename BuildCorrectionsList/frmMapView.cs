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

        public frmMapView()
        {
            InitializeComponent();
        }

        private void frmMapView_Load(object sender, EventArgs e)
        {

            try
            {
                RestoreWindowLocation();

                MapView myOSMMap = new MapView();
                myOSMMap._myMap = gMapControl1;
                //Get route data table
                myOSMMap._oldRideData = Form1.dtOldRide;
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

                GMapRoute route = new GMapRoute(myOSMMap.CreateFullRoute(), "GC route");
                route.Stroke = new Pen(Color.Red, 3);
                routes.Routes.Add(route);
                gMapControl1.Overlays.Add(routes);

                //Add markers to map
                gMapControl1.Overlays.Add(markers);
                gMapControl1.Refresh();


            }
            catch (Exception)
            {
                throw;
            }

        }

        private void frmMapView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveWindowLocation();
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

    }
}
