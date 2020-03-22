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
        private BinaryReader r = null;
        private TcpClient client = null;

        public Communication()
        {
        }

        public void SearchPort()
        {
            if(client==null)
                client = new TcpClient();
            if(!client.Connected)
            {
                try
                {
                    int port = 25440;
                    client.Connect("Senti", port);
                    NetworkStream stream = client.GetStream();
                    r = new BinaryReader(stream);
                    stream.ReadTimeout = 2000;
                    w = new BinaryWriter(stream);
                    w.Write("Monitor przesyła wiadomość.");
                    string response = r.ReadString();
                    logger.Info("Odpowiedź: " + response);
                    stream.Close();
                    client.Close();
                    client = null;
                }
                catch(Exception ex)
                {
                    logger.Error("Błąd podczas komunikacji z usługą: " + ex.Message);
                }
            }
        }
    }
}
