using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ExchangeService
{
    public class ConnectionListener
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        TcpListener tcpListener = new TcpListener(IPAddress.Any, 25440);

        public void listener()
        {
            try
            {
                tcpListener.Start();
                while (Service1.RunWhile)
                {
                    logger.Info("Listening...");
                    if(!tcpListener.Pending())
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    Socket socketForClient = tcpListener.AcceptSocket();
                    if (socketForClient.Connected)
                    {
                        NetworkStream stream = new NetworkStream(socketForClient)
                        {
                            ReadTimeout = 3000,
                            WriteTimeout = 3000,
                        };

                        BinaryReader r = new BinaryReader(stream);
                        string message = r.ReadString();
                        logger.Info("Received Communicate: " + message);
                        //Thread.Sleep(3000);
                        string response = "Response from Service.";
                        BinaryWriter w = new BinaryWriter(stream);
                        w.Write(response);
                    }
                }
                tcpListener.Stop();
            }
            catch(Exception ex)
            {
                logger.Error("Error: " + ex.Message);
            }
            logger.Info("Thread stoped...");
        }

        public void ListenerThread()
        {
            Thread t = new Thread(listener);
            t.Start();
        }
    }
}
