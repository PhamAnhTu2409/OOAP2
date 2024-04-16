using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NonFlyweightGarage
{
    class Car
    {
        private readonly Brush _color;

        public Car(Brush color)
        {
            _color = color;
        }

        public void Display(Canvas canvas, double x, double y)
        {
            Ellipse carBody = new Ellipse
            {
                Width = 50,
                Height = 20,
                Fill = _color
            };

            Canvas.SetLeft(carBody, x);
            Canvas.SetTop(carBody, y);

            canvas.Children.Add(carBody);
        }
    }

    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random();
        private readonly Dictionary<string, Brush> _carColors = new Dictionary<string, Brush>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            string type = TypeTextBox.Text;
            double x = _random.NextDouble() * (GarageCanvas.ActualWidth - 50);
            double y = _random.NextDouble() * (GarageCanvas.ActualHeight - 20);

            SolidColorBrush color = new SolidColorBrush(Color.FromRgb((byte)_random.Next(256), (byte)_random.Next(256), (byte)_random.Next(256)));
            _carColors[type] = color;
            
            Car car = new Car(_carColors[type]);
            car.Display(GarageCanvas, x, y);
        }
    }
}
