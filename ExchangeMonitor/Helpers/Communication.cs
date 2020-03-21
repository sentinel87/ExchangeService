using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using NLog;

namespace ExchangeMonitor.Helpers
{
    public class Communication
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private BinaryWriter w = null;

        public Communication()
        {
        }

        public void SearchPort()
        {
            TcpClient client = new TcpClient();

            if(!client.Connected)
            {
                try
                {
                    int port = 25440;
                    client.Connect("Senti", port);
                    NetworkStream stream = client.GetStream();
                    //stream.WriteTimeout = 4000;
                    w = new BinaryWriter(stream);
                    w.Write("Monitor przesyła wiadomość.");
                    stream.Close();
                    client.Close();
                }
                catch(Exception ex)
                {
                    logger.Error("Błąd podczas komunikacji z usługą: " + ex.Message);
                }
            }
        }
    }
}
