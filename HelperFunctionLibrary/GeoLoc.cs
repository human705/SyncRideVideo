using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperFunctionLibrary
{
    public class GeoLoc
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeoLoc(double lat, double lon)
        {
            this.Latitude = lat;
            this.Longitude = lon;
        }

    }
}
