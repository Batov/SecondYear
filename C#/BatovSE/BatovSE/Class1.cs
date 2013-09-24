using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatovSE
{
    class GeomLib
    {
        public static bool isInsideTriagle(int x0, int y0, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            int a = (x1 - x0) * (y2 - y1) - (x2 - x1) * (y1 - y0);
            int b = (x2 - x0) * (y3 - y2) - (x3 - x2) * (y2 - y0);
            int c = (x3 - x0) * (y1 - y3) - (x1 - x3) * (y3 - y0);
            if ((a > 0 && b > 0 && c > 0) || (a <= 0 && b <= 0 && c <= 0))
            {
                return true;
            }
            else return false;


        }
    }
}
