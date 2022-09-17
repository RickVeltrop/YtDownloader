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
using System.Windows.Media.Animation;
using Microsoft.Win32;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace YtDownloader
{
    public partial class MainWindow : Window
    {
        // https://www.youtube.com/watch?v=dQw4w9WgXcQ

        string VideoInputPlaceholder = "Enter link here...";
        string CurrDownloadPath = @"";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DownloadBut_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string InputLink = LinkInputField.Text;
            if (!InputLink.Contains("youtube.com"))
            {
                MsgDialog ErrBox = new MsgDialog("Error!", "Invalid link!", "", "Okay");
                ErrBox.Top = WindowMain.Top + (WindowMain.Height / 4);
                ErrBox.Left = WindowMain.Left + (WindowMain.Width / 4);
                ErrBox.ShowDialog();
                return;
            }

            // If the selected download path does not exist
            if (!Directory.Exists(CurrDownloadPath))
            {
                MsgDialog ErrBox = new MsgDialog("Error!", "Selected file does not exist!", "", "Okay");
                ErrBox.Top = WindowMain.Top + (WindowMain.Height / 4);
                ErrBox.Left = WindowMain.Left + (WindowMain.Width / 4);
                ErrBox.ShowDialog();
                return;
            }

            YouTube yt = YouTube.Default;
            Video VidInfo = yt.GetVideo(InputLink);

            string Caption = "Download Confirmation";
            string Text = $"Are you sure you want to download video: \n`{VidInfo.FullName}`";
            string LButtonTxt = "Cancel";
            string RButtonTxt = "Confirm";

            MsgDialog DownConfDialog = new MsgDialog(Caption, Text, LButtonTxt, RButtonTxt);
            DownConfDialog.Top = WindowMain.Top + (WindowMain.Height / 4);
            DownConfDialog.Left = WindowMain.Left + (WindowMain.Width / 4);

            bool? DownConf = DownConfDialog.ShowDialog();
            if (DownConf is false or null) { return; }

            Console.WriteLine(CurrDownloadPath + VidInfo.FullName);
            File.WriteAllBytes(CurrDownloadPath + @"\" + VidInfo.FullName, VidInfo.GetBytes());
            Process.Start("explorer.exe", CurrDownloadPath);
        }

        // Link TextBox styles
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox TxtBx = (TextBox)sender;
            if (TxtBx.Text == VideoInputPlaceholder)
            {
                TxtBx.Text = "";
            }

            ColorAnimation BrdAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#EEE"), new Duration(TimeSpan.FromSeconds(0.2)));
            LinkInput.BorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BrdAnim);
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox TxtBx = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(TxtBx.Text))
            {
                TxtBx.Text = VideoInputPlaceholder;
            }

            ColorAnimation BrdAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF333333"), new Duration(TimeSpan.FromSeconds(0.2)));
            LinkInput.BorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BrdAnim);
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

        // Download button styles
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border bord = (Border)sender;

            ColorAnimation BgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF3B3B3B"), new Duration(TimeSpan.FromSeconds(0.2)));
            ColorAnimation FgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#EEE"), new Duration(TimeSpan.FromSeconds(0.2)));

            bord.Background.BeginAnimation(SolidColorBrush.ColorProperty, BgAnim);
            foreach (TextBlock TxtBox in ((Grid)bord.Child).Children)
            {
                TxtBox.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, FgAnim);
            }
        }
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border bord = (Border)sender;

            ColorAnimation BgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF2B2B2B"), new Duration(TimeSpan.FromSeconds(0.2)));
            ColorAnimation FgAnim = new ColorAnimation((Color)ColorConverter.ConvertFromString("#999"), new Duration(TimeSpan.FromSeconds(0.2)));

            bord.Background.BeginAnimation(SolidColorBrush.ColorProperty, BgAnim);
            foreach (TextBlock TxtBox in ((Grid)bord.Child).Children)
            {
                TxtBox.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, FgAnim);
            }
        }

        private void AdvSettings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border SetContain = (Border)sender;

            if (DropDown.Visibility == Visibility.Collapsed)
            {
                DropDown.Visibility = Visibility.Visible;
                ((TextBlock)SetContain.Child).Text = "⇡ Advanced ⇡";
            }
            else if (DropDown.Visibility == Visibility.Visible)
            {
                DropDown.Visibility = Visibility.Collapsed;
                ((TextBlock)SetContain.Child).Text = "⇣ Advanced ⇣";
            }
        }

        private void SetFileButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CommonOpenFileDialog SaveFilePicker = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                EnsurePathExists = true,
            };
            CommonFileDialogResult ChoseFolder = SaveFilePicker.ShowDialog();

            if (ChoseFolder == CommonFileDialogResult.Ok)
            {
                CurrDownloadPath = SaveFilePicker.FileName;
                SetFileInput.Text = CurrDownloadPath;
            }
        }
    }
}
