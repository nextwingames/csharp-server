using Nextwin.Util;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Nextwin.Server
{
    public abstract class Server
    {
        private int _clientNum;
        protected Dictionary<int, Client> _clientList = null;

        public Server()
        {
            _clientNum = 1;
            _clientList = new Dictionary<int, Client>();
        }

        public void Listen()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, GetPort());
            listener.Start();

            AcceptClientAsync(listener);
        }

        protected async void AcceptClientAsync(TcpListener listener)
        {
            while(true)
            {
                Socket clientSocket = await listener.AcceptSocketAsync().ConfigureAwait(false);
                Print.Log($"New client connected: {clientSocket}");
                Client client = new Client(clientSocket, CreateReceiver(), _clientNum);
                _clientList.Add(_clientNum, client);
                _clientNum++;
            }
        }

        /// <summary>
        /// 포트 번호
        /// </summary>
        /// <returns></returns>
        protected abstract int GetPort();

        protected abstract IReceiver CreateReceiver();
    }
}
