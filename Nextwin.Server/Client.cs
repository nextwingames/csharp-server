using Nextwin.Server.Protocol;
using System.Net.Sockets;

namespace Nextwin.Server
{
    public class Client : SocketAsyncEventArgs
    {
        private int _clientIndex;
        private Socket _socket;
        private IReceiver _receiver;

        public Client(Socket socket, IReceiver receiver, int clientIndex)
        {
            _socket = socket;
            UserToken = socket;
            _receiver = receiver;
            // 이벤트 등록
            Completed += CheckBuffer;
            _clientIndex = clientIndex;
            // 비동기로 수신 시작
            _socket.ReceiveAsync(this);
        }

        private void CheckBuffer(object sender, SocketAsyncEventArgs e)
        {
            if(_socket.Connected && BytesTransferred > 0)
            {
                byte[] receivedData = e.Buffer;
                _receiver.OnReceivedData(SerializableData.ReadMsgTypeFromBytes(receivedData), receivedData, _clientIndex);
                // 다시 비동기로 수신 시작
                _socket.ReceiveAsync(e);
            }
            else
            {
                _receiver.OnClientDisconnect(_socket);
            }
        }
    }
}
