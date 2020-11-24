﻿using System;
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
        public int _mapZoom { get; set; }

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
            marker.ToolTip.Fill = Brushes.Black;
            marker.ToolTip.Foreground = Brushes.White;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(20, 5);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            // Add to marker overlay
            _markers.Markers.Add(marker);
        }

        public List<PointLatLng> CreateFullRoute ()
        {
            List<PointLatLng> _points = new List<PointLatLng>();
            List<GeoLocPoint> _geoPoints = new List<GeoLocPoint>();
            try
            {
                double _lat, _lng, _alt, _km;
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
                        _alt = (double)dr["lat"];
                        _lat = (double)dr["lat"];
                        _lng = (double)dr["lon"];

                        _geoPoints.Add(new GeoLocPoint(_lat, _lng, _alt, _sec));
                        _points.Add(new PointLatLng(_lat, _lng));
                        AddMarkerToRoutePoint(_lat, _lng, _alt, _sec, _km);

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
            _myMap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;

            // Do not cache map info
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            // When internat access is unavailable use cache
            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;

            // Hide red cross
            _myMap.ShowCenter = false;

            //Set map zoom control
            _myMap.MinZoom = 1;
            _myMap.MaxZoom = 18;
            if (_mapZoom > 1 && _mapZoom < 18)
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
