using System.Net.Sockets;

namespace Nextwin.Server
{
    public interface IReceiver
    {
        /// <summary>
        /// 데이터를 수신했을 때 호출되는 함수
        /// </summary>
        /// <param name="msgType">받은 데이터의 종류</param>
        /// <param name="receivedData">직렬화된 데이터</param>
        void OnReceivedData(int msgType, byte[] receivedData, int clientIndex);

        /// <summary>
        /// 클라이언트가 접속을 해제했을 때 호출되는 함수
        /// </summary>
        void OnClientDisconnect(Socket socket);
    }
}