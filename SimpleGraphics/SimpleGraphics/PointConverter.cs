using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;

namespace SimpleGraphics
{
    public class PointToXConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return PointView.Proection((double[])value)[0];
            //return new PointCollection(((double[][])value).Select(p => PointTransformation3D.RotateY(p, -45 * Math.PI / 180))//.Select(p => PointTransformation3D.IsometricProection(p))
            //    .Select(p => PointTransformation3D.RotateX(p, 35.26439 * Math.PI / 180))
            //    .Select(p => new Point(p[0] + 200, 200 - p[1])));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class PointToYConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return PointView.Proection((double[])value)[1];
            //return new PointCollection(((double[][])value).Select(p => PointTransformation3D.RotateY(p, -45 * Math.PI / 180))//.Select(p => PointTransformation3D.IsometricProection(p))
            //    .Select(p => PointTransformation3D.RotateX(p, 35.26439 * Math.PI / 180))
            //    .Select(p => new Point(p[0] + 200, 200 - p[1])));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
