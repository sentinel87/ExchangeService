using System;
using System.Collections.Generic;
using System.Linq;
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
using ExchangeMonitor.Helpers;
using System.Threading;
using NLog;

namespace ExchangeMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private Communication communication = new Communication();
        private bool Connecting = false;
        private Thread MonitorThread = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            Connecting = true;
            MonitorThread = new Thread(MonitorReceiver);
            MonitorThread.Start();
        }

        private void MonitorReceiver()
        {
            logger.Info("Wątek zaczyna pracę.");
            while (Connecting)
            {
                communication.SearchPort();
                Thread.Sleep(5000);
            }
            logger.Info("Wątek zakończył pracę.");
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            Connecting = false;
        }
    }
}
