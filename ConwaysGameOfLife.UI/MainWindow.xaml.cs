using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ConwaysGameOfLife.UI
{
    public partial class MainWindow
    {
        readonly Grid grid = new Grid(50, 50);
        private bool stopRaised;

        public MainWindow()
        {
            InitializeComponent();
            grid.Initialise();

            Loaded += WindowUpdated;
            SizeChanged += WindowUpdated;
        }

        public void DrawGrid()
        {
            DrawGridLines();
            DrawCells();
        }

        private void BeginDraw()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += WaitInterval;
            worker.RunWorkerCompleted += IntervalComplete;
            worker.RunWorkerAsync();
        }

        private void IntervalComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            grid.Tick();
            DrawGrid();

            if (stopRaised)
            {
                stopRaised = false;
                return;
            }

            BeginDraw();
        }

        private void DrawCells()
        {
            for (var columnCount = 0; columnCount < grid.Width; columnCount++)
            {
                for (var rowCount = 0; rowCount < grid.Height; rowCount++)
                {
                    if (grid.CellAt(columnCount, rowCount).CurrentState != State.Alive) continue;

                    DrawCell(columnCount, rowCount);
                }
            }
        }

        private void DrawCell(int columnCount, int rowCount)
        {
            var cellRectangle = new Rectangle
                                    {
                                        Fill = Brushes.MidnightBlue,
                                        Width = GetCellWidth(),
                                        Height = GetCellHeight(),
                                        Margin = new Thickness(columnCount * GetCellWidth(), rowCount * GetCellHeight(), 0, 0)
                                    };
            cellRectangle.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Unspecified);
            PlayingField.Children.Add(cellRectangle);
        }

        private void DrawGridLines()
        {
            PlayingField.Children.Clear();

            for (var widthCount = 0; widthCount < grid.Width; widthCount++)
            {
                DrawGridLine(widthCount * GetCellWidth(), Orientation.Horizontal);
            }

            for (var heightCount = 0; heightCount < grid.Height; heightCount++)
            {
                DrawGridLine(heightCount * GetCellHeight(), Orientation.Vertical);
            }
        }

        private void DrawGridLine(double position, Orientation orientation)
        {
            var line = new Line
                           {
                               Stroke = Brushes.IndianRed,
                               StrokeThickness = 1,
                               X1 = orientation == Orientation.Horizontal ? position : 0,
                               X2 = orientation == Orientation.Horizontal ? position : PlayingField.ActualWidth,
                               Y1 = orientation == Orientation.Vertical ? position : 0,
                               Y2 = orientation == Orientation.Vertical ? position : PlayingField.ActualHeight,
                           };

            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            PlayingField.Children.Add(line);
        }

        private void TickClick(object sender, RoutedEventArgs e)
        {
            grid.Tick();
            DrawGrid();
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
            Tick.IsEnabled = false;
            BeginDraw();
        }

        private void StopClick(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
            Tick.IsEnabled = true;
            stopRaised = true;
        }

        private void UserClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(e.Source as IInputElement);
            var cell = grid.CellAt(
                Convert.ToInt32(Math.Floor(pos.X / GetCellWidth())),
                Convert.ToInt32(Math.Floor(pos.Y / GetCellHeight()))
                );

            cell.CurrentState = cell.CurrentState == State.Alive ? State.Dead : State.Alive;
            DrawGrid();
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            grid.Initialise();
            DrawGrid();
        }

        void WindowUpdated(object sender, RoutedEventArgs e)
        {
            DrawGrid();
        }

        private static void WaitInterval(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(300);
        }

        private double GetCellWidth()
        {
            return PlayingField.ActualWidth / grid.Width;
        }

        private double GetCellHeight()
        {
            return PlayingField.ActualHeight / grid.Height;
        }
    }
}

