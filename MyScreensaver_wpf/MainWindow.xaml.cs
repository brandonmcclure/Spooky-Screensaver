using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyScreensaver_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TransformGroup group = new TransformGroup();
            double width = this.MainGrid.RenderSize.Width;
            DoubleAnimation animation = new DoubleAnimation((width / 2) * -1, width / 2 + logo.ActualWidth, new Duration(new TimeSpan(0, 0, 0, 10)));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            TranslateTransform tt = new TranslateTransform(-logo.ActualWidth * 2, 0);
            logo.RenderTransform = group;
            logo.Width = 200;
            logo.Height = 200;
            group.Children.Add(tt);
            tt.BeginAnimation(TranslateTransform.XProperty, animation);
            WindowState = WindowState.Maximized;
           // Mouse.OverrideCursor = Cursors.None;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
