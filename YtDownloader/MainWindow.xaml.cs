using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoLibrary;

namespace YtDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // https://www.youtube.com/watch?v=dQw4w9WgXcQ

        string PlaceholderText = "Enter link here...";

        string DefaultPath = @"D:\TEST\";
        string uri = "";

        // Returns file name for saved files
        private string ConstructFileName(YouTubeVideo video)
        {
            return $"{video.Info.Title}" + video.FileExtension;
        }

        // Download and saves a youtube video
        private void SaveVideoToDisk()
        {
            YouTube yt = YouTube.Default;
            YouTubeVideo video = yt.GetVideo(uri);
            File.WriteAllBytes(DefaultPath + ConstructFileName(video), video.GetBytes());
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox TxtBx = (TextBox)sender;
            if (TxtBx.Text == PlaceholderText)
            {
                TxtBx.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox TxtBx = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(TxtBx.Text))
            {
                TxtBx.Text = PlaceholderText;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox TxtBx = (TextBox)sender;
            if (e.Key != Key.Enter) { return; }

            uri = TxtBx.Text;

            SaveVideoToDisk();
        }

        private void CloseBut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinBut_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
