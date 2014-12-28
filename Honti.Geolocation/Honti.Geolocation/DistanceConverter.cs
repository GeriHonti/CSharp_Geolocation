using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honti.Geolocation
{
    public class DistanceConverter
    {
        public double ConvertMilesToKilometers(double miles)
        {
            return miles * 1.609344;
        }

        public double ConvertKilometersToMiles(double kilometers)
        {
            return kilometers * 0.621371192;
        }
    }
}
