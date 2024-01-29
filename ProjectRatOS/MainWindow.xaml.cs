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
        //static string fileInfo = Path.Combine(curDir, "\\MusicFolder");
        //static DirectoryInfo parentDir = fileInfo.Directory.Parent;
        //static string nowFolder = parentDir.FullName;

        public MainWindow()
        {
            InitializeComponent();
            MusicNameCount.Minimum = 0;
            UpdateMusic();
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
            //var message = MessageBox.Show("Для добавления своей музыки нужно перейти в папку 'MusicFolder'", "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Information);
            //int i = 0;
            //if (message == MessageBoxResult.OK)
            //{
            //    i++;
            //    if (i > 0)
            //    {
            //    }
            //}
            Process.Start(curDir);
            UpdateMusic();

        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UpdateMusic()
        {
            //для проверки записывать $"{curDir}\\MusicFolder"(путь)
            if (Directory.Exists($"{curDir}\\MusicFolder"))
            {
                DirectoryInfo dirExist = new DirectoryInfo($"{curDir}\\MusicFolder");
                MusicNameCount.Maximum = dirExist.GetFiles().Count();
                MusicCount.Content = $"Музыки в папке: {dirExist.GetFiles().Count()}";
            }
            else 
            {
                Directory.CreateDirectory($"{curDir}\\MusicFolder");
            }
        }
    }
}
