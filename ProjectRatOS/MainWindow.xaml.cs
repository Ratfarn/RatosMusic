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
        static string curDir = Directory.GetCurrentDirectory();
        static DirectoryInfo dirExist = new DirectoryInfo($"{curDir}\\MusicFolder");

        public MainWindow()
        {
            InitializeComponent();
            //MusicTimeCount.Minimum = 0;
            UpdateMusic();
            //mediaPlay.Volume = (double)VolumeSlider.Value;
            mediaPlay.Volume = 1;
        }

        MediaElement mediaPlay = new MediaElement{LoadedBehavior = MediaState.Manual};

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
            UpdateMusic();
            Process.Start($"{curDir}\\MusicFolder");
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaPlay.Close();
            Application.Current.Shutdown();
        }

        private void UpdateMusic()
        {
            chooseMusic.ItemsSource = dirExist.GetFiles();
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
            mediaPlay.Pause();
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaPlay.Stop();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaPlay.Play();
        }

        private void chooseMusic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = chooseMusic.SelectedItem as string;
            if (item != null)
            {
                mediaPlay.Source = new Uri(item, UriKind.RelativeOrAbsolute);
                //mediaPlay.Play();
            }
        }

        //OpenFileDialog fileDialog = new OpenFileDialog
        //{
        //    Multiselect = false,
        //    DefaultExt = ".mp3"
        //};
        //bool? dialogOk = fileDialog.ShowDialog();
        //        if (dialogOk == true)
        //        {
        //            var musicFile = fileDialog.FileName;
        //fileNameBlock.Text = fileDialog.SafeFileName;
        //            mediaPlay.Open(new Uri(musicFile));
        //        }
    }
}
