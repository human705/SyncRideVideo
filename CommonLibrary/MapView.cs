using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using System.Data;

namespace CommonLibrary
{
    public class MapView
    {
        public GMapControl _myMap { get; set; }
        public PointLatLng _initLatLng { get; set; }
        public DataTable _oldRideData { get; set; }
        public GMapOverlay _markers { get; set; }
        public GMapOverlay _selectedMarkers { get; set; }
        public int _mapZoom { get; set; }
        public DataTable _cps { get; set; }

        public List<double> _altDataX { get; set; }
        public List<double> _altDataY { get; set; }
        public List<double> _slope { get; set; }

        private void AddAltitudeChartData (double _x, double _alt, double _slp)
        {
            double t = (double)_x;
            _altDataX.Add(t);
            _altDataY.Add(_alt);
            _slope.Add(_slp);
        }

        private void AddMarkerToRoutePoint(double _lat, double _lng, double _alt, int _sec, double _km)
        {
            //Add marker
            GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(_lat, _lng),
                GMarkerGoogleType.blue_small);

            // Marker tooltip
                        marker.ToolTipText = "Time:" + _sec.ToString() + "\nLAT:" 
                        + _lat.ToString() + "\nLNG:" + _lng.ToString() + "\nALT:" 
                        + _alt.ToString() + "\nMeters:" + (_km*1000).ToString();
            marker.Tag = "blue";
            marker.ToolTip.Fill = Brushes.Black;
            marker.ToolTip.Foreground = Brushes.White;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(20, 5);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            // Add to marker overlay
            _markers.Markers.Add(marker);
        }



        /// <summary>
        /// Add red markers for the existing correction points list
        /// </summary>
        public void AddSelectedMarkersToMap ()
        {
            GMapMarker _redMarker;
            try
                
            {
                double _lat, _lng;
                foreach (DataRow dr in _cps.Rows)
                {
                    _lat = (double)dr.ItemArray[2];
                    _lng = (double)dr.ItemArray[3];
                    _redMarker = new GMarkerGoogle(new PointLatLng(_lat, _lng),GMarkerGoogleType.red_small);
                    _redMarker.Tag = "red";
                    _selectedMarkers.Markers.Add(_redMarker);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<PointLatLng> CreateFullRoute ()
        {
            List<PointLatLng> _points = new List<PointLatLng>();
            List<GeoLocPoint> _geoPoints = new List<GeoLocPoint>();
            try
            {
                double _lat, _lng, _alt, _km, _slp;
                int _sec;

                if (_oldRideData != null)
                {
                    foreach (DataRow dr in _oldRideData.Rows)
                    {
                        //SAMPLE newSample = new SAMPLE();
                        //newSample.SECS = (int)dr["secs"];
                        //newSample.KM = (double)dr["km"];
                        //newSample.CAD = (int)dr["cad"];
                        //newSample.KPH = (double)dr["kph"];
                        //newSample.HR = (double)dr["hr"];
                        //newSample.ALT = (double)dr["alt"];
                        //newSample.LAT = (double)dr["lat"];
                        //newSample.LON = (double)dr["lon"];
                        //newSample.SLOPE = (double)dr["slope"];
                        _km = (double)dr["km"];
                        _sec = (int)dr["secs"];
                        _alt = (double)dr["alt"];
                        _lat = (double)dr["lat"];
                        _lng = (double)dr["lon"];
                        _slp = (double)dr["slope"];

                        _geoPoints.Add(new GeoLocPoint(_lat, _lng, _alt, _sec));
                        _points.Add(new PointLatLng(_lat, _lng));
                        AddMarkerToRoutePoint(_lat, _lng, _alt, _sec, _km);
                        AddAltitudeChartData(_km, _alt, _slp);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _points;
        }
        public void SetMapDefaults()
        {
            //Set Map provider
            //_myMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            // Hybrid map
            _myMap.MapProvider = GMap.NET.MapProviders.GoogleChinaHybridMapProvider.Instance;
            //Satellite map
            //_myMap.MapProvider = GMap.NET.MapProviders.GoogleChinaSatelliteMapProvider.Instance;

            //OSM
            //_myMap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;

            //HERE maps
            //_myMap.MapProvider = GMap.NET.MapProviders.HereMapProvider.Instance;
            //GMapProviders.HereMap.AppId = "85zcRChsXvg1bNBFwRxG";

            // Do not cache map info
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            // When internat access is unavailable use cache
            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;

            // Hide red cross
            _myMap.ShowCenter = false;

            //Set map zoom control
            _myMap.MinZoom = 1;
            _myMap.MaxZoom = 20;
            if (_mapZoom >= 1 && _mapZoom <= 20)
            {
                _myMap.Zoom = _mapZoom; 
            } else
            {
                _myMap.Zoom = 17;
            }

            //Set map postition based on LAT and LON
            _myMap.Position = _initLatLng;

            
        }


    }

    public struct GeoLocPoint
    {
        public double _lat { get; set; }
        public double _lng { get; set; }
        public double _alt { get; set; }
        public int _sec { get; set; }

        public GeoLocPoint(double x, double y, double z, int t)
        {
            _lat = x;
            _lng = y;
            _alt = z;
            _sec = t;
        }

    }
}
