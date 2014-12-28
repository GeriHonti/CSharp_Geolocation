using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honti.Geolocation
{
    public class AngleConverter
    {
        public double ConvertDegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public double ConvertRadiansToDegrees(double angle)
        {
            return 180.0 * angle / Math.PI;
        }
    }
}
