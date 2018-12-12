using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Geometry
{
    public class Point
    {
        public float X;
        public float Y;

        public Point()
        {

        }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"[{X:N2}, {Y:N2}]";
        }
    }
}
