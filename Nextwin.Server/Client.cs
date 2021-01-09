using Nextwin.Server.Protocol;
using System.Net.Sockets;

namespace Nextwin.Server
{
    internal class Client : SocketAsyncEventArgs
    {
        private Socket _socket;
        private IReceiver _receiver;

        public Client(Socket socket, IReceiver receiver)
        {
            _socket = socket;
            UserToken = socket;
            _receiver = receiver;
            Completed += CheckBuffer;
        }

        private void CheckBuffer(object sender, SocketAsyncEventArgs e)
        {
            if(_socket.Connected && BytesTransferred > 0)
            {
                byte[] receivedData = e.Buffer;
                _receiver.OnReceivedData(SerializableData.ReadMsgTypeFromBytes(receivedData), receivedData);
            }
            else
            {
                _receiver.OnClientDisconnect();
            }
        }
    }
}
