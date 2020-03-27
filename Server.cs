using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace WebShareC
{

    // 대략적인 흐름도
    /*  서버 시작 -> 클라이언트 접속 -> 클라이언트에게 파일경로,형식,크기 전송 -> 클라이언트로 파일리스트 인덱스 번호 수신 -> 클라이언트로 파일 전송 */
    
    class Server
    {

        List<string> Testing = new List<string>(); // 파일 경로 리스트

        Int32 INDEX = 0;

        void TempMethod() // 파일경로(?)
        {
            Testing = BlackCoffeeShare.FILEPATH; // Partial 클래스
            INDEX = BlackCoffeeShare.FILENUM;
        }

        Socket server;
        List<Socket> clientSockList = new List<Socket>(); // 클라이언트 temporary list

        private AsyncCallback AcceptHandler;
        private AsyncCallback ReceiveHandler;
        private AsyncCallback SendHandler;

        class DataObject
        {
          //  public Int32 DataType;
            public Byte[] Buffer;
            public string FileName;
            public Socket WorkingSocket;
            public DataObject(Int32 bufferSize) //Type : 전송 구분  BufferSize 사이즈 지정
            {
                this.Buffer = new Byte[bufferSize];
                //this.DataType = Type;
            }
        }

        public Server() // 서버 시작 생성자
        {
            callbackInit();
            serverInit(5000); // 매개변수로 포트번호 지정
           // callbackInit();

        }
        private void callbackInit() //callback 함수
        {
            AcceptHandler = new AsyncCallback(AccepctCallback);
            ReceiveHandler = new AsyncCallback(ReceiveCallback);
            SendHandler = new AsyncCallback(SendCallback);
            
        }
        public void serverInit(int port) // 포트, 아이피지정
        {
            IPEndPoint ipv4 = new IPEndPoint(IPAddress.Any, port);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipv4); // ipv4로 포트와 아이피주소 바인드
            server.Listen(20); // 20대기열
            server.BeginAccept(AccepctCallback, server); // 비동기함수 시작
            //server.BeginSend();
        }
        private void AccepctCallback(IAsyncResult ar)
        {
            Socket Client = server.EndAccept(ar); 

            DataObject DO = new DataObject(1024);
            DO.WorkingSocket = Client;
            clientSockList.Add(server.EndAccept(ar));
            server.BeginSend(DO.Buffer, 0, DO.Buffer.Length, SocketFlags.None, SendHandler, DO);
           // Client.BeginReceive(DO.Buffer, 0, DO.Buffer.Length, SocketFlags.None, ReceiveHandler, DO);
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            DataObject DO = ar.AsyncState as DataObject;

            Int32 recvBytes = DO.WorkingSocket.EndReceive(ar);

            if (recvBytes > 0)
            {
                
            }
        
        }
        private void SendCallback(IAsyncResult ar)
        {
            DataObject DO = ar.AsyncState as DataObject;

            Int32 sentBytes = DO.WorkingSocket.EndSend(ar);

            if (sentBytes > 0)
            {
                MessageBox.Show(":");
            }
        }
    }
}
