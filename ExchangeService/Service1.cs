using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using NLog;

namespace ExchangeService
{
    public partial class Service1 : ServiceBase
    {
        public static bool RunWhile = true;

        ConnectionListener listener = new ConnectionListener();
        Logger logger = LogManager.GetCurrentClassLogger();
        Timer timer = new Timer();

        public Service1()
        {
            InitializeComponent();
            timer.Interval = 2000;
            timer.Elapsed += new ElapsedEventHandler(timerTick);
        }

        protected override void OnStart(string[] args)
        {
            //timer.Start();
            logger.Info("Service started...");
            RunWhile = true;
            listener.ListenerThread();
        }

        protected override void OnStop()
        {
            //timer.Stop();
            RunWhile = false;
            logger.Info("Service stoped...");
        }

        private void timerTick(object obj,ElapsedEventArgs e)
        {
            logger.Info("Tick...");
        }
    }
}
