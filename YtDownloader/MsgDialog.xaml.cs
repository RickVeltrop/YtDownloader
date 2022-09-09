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

namespace YtDownloader
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MsgDialog : Window
    {
        public MsgDialog()
        {
            InitializeComponent();
        }

        // Menu Buttons
        private void CloseBut_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void MinBut_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Window dragging
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        // Minimalize Button styles
        private void MinBut_MouseEnter(object sender, MouseEventArgs e)
        {
            Border But = (Border)sender;
            TextBlock TxtBox = (TextBlock)But.Child;

            ColorAnimation BgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF3B3B3B"), new Duration(TimeSpan.FromSeconds(0.2)));
            ColorAnimation FgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#EEE"), new Duration(TimeSpan.FromSeconds(0.2)));

            But.Background.BeginAnimation(SolidColorBrush.ColorProperty, BgAnim);
            TxtBox.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, FgAnim);
        }
        private void MinBut_MouseLeave(object sender, MouseEventArgs e)
        {
            Border But = (Border)sender;
            TextBlock TxtBox = (TextBlock)But.Child;

            ColorAnimation BgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF2B2B2B"), new Duration(TimeSpan.FromSeconds(0.2)));
            ColorAnimation FgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#999"), new Duration(TimeSpan.FromSeconds(0.2)));

            But.Background.BeginAnimation(SolidColorBrush.ColorProperty, BgAnim);
            TxtBox.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, FgAnim);
        }

        // Close Button styles
        private void CloseBut_MouseEnter(object sender, MouseEventArgs e)
        {
            Border But = (Border)sender;
            TextBlock TxtBox = (TextBlock)But.Child;

            ColorAnimation BgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#AA0000"), new Duration(TimeSpan.FromSeconds(0.2)));
            ColorAnimation FgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#EEE"), new Duration(TimeSpan.FromSeconds(0.2)));

            But.Background.BeginAnimation(SolidColorBrush.ColorProperty, BgAnim);
            TxtBox.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, FgAnim);
        }
        private void CloseBut_MouseLeave(object sender, MouseEventArgs e)
        {
            Border But = (Border)sender;
            TextBlock TxtBox = (TextBlock)But.Child;

            ColorAnimation BgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF2B2B2B"), new Duration(TimeSpan.FromSeconds(0.2)));
            ColorAnimation FgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#999999"), new Duration(TimeSpan.FromSeconds(0.2)));

            But.Background.BeginAnimation(SolidColorBrush.ColorProperty, BgAnim);
            TxtBox.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, FgAnim);
        }
    }
}

