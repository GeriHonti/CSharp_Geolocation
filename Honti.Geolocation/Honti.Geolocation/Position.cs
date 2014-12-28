using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honti.Geolocation
{
    public class Position
    {
        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public Position()
        {
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
