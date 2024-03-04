using Microsoft.Win32;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Timers.Timer timer;
        public MainWindow()
        {
            InitializeComponent();
            ComboBoxTime.SelectedIndex = 0;
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            LoadProcesses();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LoadProcesses();
        }
        private void LoadProcesses()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                grid.ItemsSource = Process.GetProcesses();
            }));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int sec;
            if (ComboBoxTime.SelectedItem.ToString().Contains("1"))
            {
                sec = 1;
            }
            else if (ComboBoxTime.SelectedItem.ToString().Contains("2"))
            {
                sec = 2;
            }
            else
            {
                sec = 5;
            }
            StartTimer(sec);
        }
        private void StartTimer(int interval)
        {
            timer.Interval = interval * 1000;
            timer.Start();
        }
        private void StopTimer()
        {
            timer.Stop();
        }

        private void BtnStopRefreshing_Click(object sender, RoutedEventArgs e)
        {
            StopTimer();
        }

        private void BtnKillProc_Click(object sender, RoutedEventArgs e)
        {
            int procId = int.Parse((grid.SelectedItem as Process).Id.ToString());
            try
            {
                Process process = Process.GetProcessById(procId);
                process.Kill();
            }
            catch (Exception)
            {
                MessageBox.Show("This process does not exist!", "Process");
            }
        }

        private void BtnCreateProc_Click(object sender, RoutedEventArgs e)
        {
            string.IsNullOrEmpty(TextBoxInformation.Text);
            if(string.IsNullOrEmpty(TextBoxInformation.Text))
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                bool? result = fileDialog.ShowDialog();
                if (result == true)
                {
                    Process.Start(fileDialog.FileName);
                }
            }
            else
            {
                Process.Start(TextBoxInformation.Text);
            }
        }
    }
}