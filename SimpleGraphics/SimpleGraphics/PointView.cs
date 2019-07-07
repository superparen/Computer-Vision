using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleGraphics.Models;

namespace SimpleGraphics
{
    public class PointView : INotifyPropertyChanged
    {
        private double[][] points;
        public double[][] Points
        {
            get => points;
            set
            {
                points = value;
                OnPropertyChanged();
            }
        }

        public static Func<double[], double[]> Proection;

        private double[][] coordinates;
        public double[][] Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value;
                OnPropertyChanged();
            }
        }

        private double[] rotateVector;
        public double[] RotateVector
        {
            get => rotateVector;
            set
            {
                rotateVector = value;
                OnPropertyChanged();
            }
        }
        public void Rotate()
        {
            if (RotateVector[0] != 0)
                Points = Points.Select(p => PointTransformation3D.RotateX(p, RotateVector[0] * Math.PI / 180d)).ToArray();
            if (RotateVector[1] != 0)
                Points = Points.Select(p => PointTransformation3D.RotateY(p, RotateVector[1] * Math.PI / 180d)).ToArray();
            if (RotateVector[2] != 0)
                Points = Points.Select(p => PointTransformation3D.RotateZ(p, RotateVector[2] * Math.PI / 180d)).ToArray();
        }

        private RelayCommand rotateCommand;
        public ICommand RotateCommand
        {
            get => rotateCommand;
        }

        private double[] scalingVector;
        public double[] ScalingVector
        {
            get => scalingVector;
            set
            {
                scalingVector = value;
                OnPropertyChanged();
            }
        }
        public void Scale()
        {
            Points = Points.Select(p => PointTransformation3D.Scaling(p, ScalingVector[0], ScalingVector[1], ScalingVector[2])).ToArray();
        }

        private RelayCommand scaleCommand;
        public ICommand ScaleCommand
        {
            get => scaleCommand;
        }

        private double[] translationVector;
        public double[] TranslationVector
        {
            get => translationVector;
            set
            {
                translationVector = value;
                OnPropertyChanged();
            }
        }
        public void Translation()
        {
            Points = Points.Select(p => PointTransformation3D.Translation(p, TranslationVector[0], TranslationVector[1], TranslationVector[2])).ToArray();
        }

        private RelayCommand translationCommand;
        public ICommand TranslationCommand
        {
            get => translationCommand;
        }

       
        
        private double[] perspectiveVector;
        public double[] PerspectiveVector
        {
            get => perspectiveVector;
            set
            {
                perspectiveVector = value;
                OnPropertyChanged();
            }
        }
        public void Perspective()
        {
            double p = PerspectiveVector[0] == 0 ? 0d : -1 / PerspectiveVector[0],
                q = PerspectiveVector[1] == 0 ? 0d : -1 / PerspectiveVector[1],
                r = PerspectiveVector[2] == 0 ? 0d : -1 / PerspectiveVector[2];
            Points = Points.Select(pt => PointTransformation3D.PerspectiveProection(pt, p, q, r))
                .Select(pt => pt.Select(c => c / pt[3]).ToArray()).ToArray();
            Coordinates = Coordinates.Select(pt => PointTransformation3D.PerspectiveProection(pt, -p, q, r))
                .Select(pt => pt.Select(c => c / pt[3]).ToArray()).ToArray();
        }

        private RelayCommand perspectiveCommand;
        public ICommand PerspectiveCommand
        {
            get => perspectiveCommand;
        }

        private RelayCommand obliqueCommand;
        public ICommand ObliqueCommand
        {
            get => obliqueCommand;
        }
        public void ObliqueProection()
        {
            Proection = o =>
            {
                o = PointTransformation3D.ObliqueProection(o, 1, 20 * Math.PI / 180);
                o[0] += 300;
                o[1] = 300 - o[1];
                return o;
            };
            OnPropertyChanged("Points");
            OnPropertyChanged("Coordinates");
        }

        private RelayCommand isometricCommand;
        public ICommand IsometricCommand
        {
            get => isometricCommand;
        }
        public void IsometricProection()
        {
            Proection = o =>
            {
                o = PointTransformation3D.RotateY(o, -45 * Math.PI / 180);
                o = PointTransformation3D.RotateX(o, 35.26439 * Math.PI / 180);
                o[0] += 300;
                o[1] = 300 - o[1];
                return o;
            };
            OnPropertyChanged("Points");
            OnPropertyChanged("Coordinates");
        }

        public PointView()
        {
            rotateVector = new double[] { 0, 0, 0 };
            rotateCommand = new RelayCommand(o => Rotate());

            scalingVector = new double[] { 1, 1, 1 };
            scaleCommand = new RelayCommand(o => Scale());

            translationVector = new double[] { 0, 0, 0 };
            translationCommand = new RelayCommand(o => Translation());

            perspectiveVector = new double[] { 1000, 1000, 1000 };
            perspectiveCommand = new RelayCommand(o => Perspective());

            points = new double[][]
            {
                new double[]{ 0, 0, 0, 1 },
                new double[]{ 100, 0, 0, 1 },
                new double[]{ 100, 100, 0, 1 },
                new double[]{ 0, 100, 0, 1 },
                new double[]{ 0, 0, 100, 1 },
                new double[]{ 100, 0, 100, 1 },
                new double[]{ 100, 100, 100, 1 },
                new double[]{ 0, 100, 100, 1 },
            };

            coordinates = new double[][]
            {
                new double[] { 0, 0, 0, 1 },
                new double[] { 200, 0, 0, 1 },
                new double[] { 0, 200, 0, 1 },
                new double[] { 0, 0, 200, 1 }
            };

            isometricCommand = new RelayCommand(o => IsometricProection());
            obliqueCommand = new RelayCommand(o => ObliqueProection());
            Proection = o =>
            {
                o = PointTransformation3D.RotateY(o, -45 * Math.PI / 180);
                o = PointTransformation3D.RotateX(o, 35.26439 * Math.PI / 180);
                o[0] += 300;
                o[1] = 300 - o[1];
                return o;
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
