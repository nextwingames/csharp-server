using Nextwin.Util;
using System.Net;
using System.Net.Sockets;

namespace Nextwin.Server
{
    public abstract class Server
    {
        public void Listen()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, GetPort());
            listener.Start();

            AcceptClientAsync(listener);
        }

        private async void AcceptClientAsync(TcpListener listener)
        {
            while(true)
            {
                Socket clientSocket = await listener.AcceptSocketAsync().ConfigureAwait(false);
                Print.Log($"New client connected: {clientSocket}");
                Client client = new Client(clientSocket, CreateReceiver());
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
