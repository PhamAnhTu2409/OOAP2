using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlyweightGarage
{
    // Flyweight interface
    interface IVehicle
    {
        void Display(Canvas canvas, double x, double y);
    }

    // Concrete flyweight
    class Car : IVehicle
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

    // Flyweight factory
    class VehicleFactory
    {
        private readonly Dictionary<string, IVehicle> _vehicles = new Dictionary<string, IVehicle>();
        private readonly Dictionary<string, Brush> _colors = new Dictionary<string, Brush>();
        private readonly Random _random = new Random();

        public IVehicle GetVehicle(string key)
        {
            if (!_vehicles.ContainsKey(key))
            {
                // Create a random color for the vehicle type if not exists
                Brush color = GetRandomColor();
                _colors[key] = color;
                _vehicles[key] = new Car(color);
            }
            return _vehicles[key];
        }

        private SolidColorBrush GetRandomColor()
        {
            return new SolidColorBrush(Color.FromRgb((byte)_random.Next(256), (byte)_random.Next(256), (byte)_random.Next(256)));
        }
    }

    public partial class MainWindow : Window
    {
        private readonly VehicleFactory _vehicleFactory = new VehicleFactory();
        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            double x = _random.NextDouble() * (GarageCanvas.ActualWidth - 50);
            double y = _random.NextDouble() * (GarageCanvas.ActualHeight - 20);

            string type = TypeTextBox.Text;
            IVehicle vehicle = _vehicleFactory.GetVehicle(type);
            vehicle.Display(GarageCanvas, x, y);
        }
    }
}
