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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

namespace ProjectRatOS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i = 0;
        static string curDir = Directory.GetCurrentDirectory();
        DirectoryInfo dirExist = new DirectoryInfo($"{curDir}\\MusicFolder");
        //static string fileInfo = Path.Combine(curDir, "\\MusicFolder");
        //static DirectoryInfo parentDir = fileInfo.Directory.Parent;
        //static string nowFolder = parentDir.FullName;

        public MainWindow()
        {
            InitializeComponent();
            MusicTimeCount.Minimum = 0;
            UpdateMusic();
            mediaPlayWin.Volume = (double)VolumeSlider.Value;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // Отмена закрытия приложения
        }
        private void DragMe(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                //exception
            }
        }
        private void LoadMusic_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(curDir);
            UpdateMusic();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayWin.Close();
            Application.Current.Shutdown();
        }

        private void UpdateMusic()
        {
            //для проверки существования $"{curDir}\\MusicFolder"(путь)
            if (Directory.Exists($"{curDir}\\MusicFolder"))
            {
                MusicCount.Content = $"Музыки в папке: {dirExist.GetFiles().Count()}";
            }
            else 
            {
                Directory.CreateDirectory($"{curDir}\\MusicFolder");
            }
        }

        private void pauseBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayWin.Pause();
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayWin.Stop();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                mediaPlayWin.Play();
            }
            catch (Exception)
            {
                MessageBox.Show("файл не подходит","ВНИМАНИЕ");
            }
        }

        private void mediaPlayWin_MediaOpened(object sender, RoutedEventArgs e)
        {
            MusicTimeCount.Maximum = mediaPlayWin.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void mediaPlayWin_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaPlayWin.Stop();
        }

        private void plusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (i > (dirExist.GetFiles().Count() - 1))
            { i = 0; }
        }

        private void minusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (i < 0)
            { i = (dirExist.GetFiles().Count() - 1); }
        }
    }
}
