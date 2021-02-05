using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class NetManager : MonoBehaviour
{
    
    private Socket customerSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
    private IPAddress clientIP = IPAddress.Parse(""); //输入服务器端IP
    private int port = 7788;
    private Thread t;
    private byte[] data = new byte[1024];
    private string message = "";
    public Text ChatCotent;
    public ScrollRect chatpanel;
    private int flag = 0;
    private void ConnectClient()
    {

    }
    private void Update()
    {
        if (message != "")
        {
            ChatCotent.text += message;
            message = "";
            flag = 5;
        }
        if(flag-- > 0) chatpanel.verticalNormalizedPosition = 0;
    }
    void Start()
    {
        customerSocket.Connect(new IPEndPoint(clientIP, port));
        t = new Thread(MyReceiveMessage);
        t.Start();
    }

    private void MyReceiveMessage()
    {
        while(!customerSocket.Poll(10,SelectMode.SelectRead))
        {
            int length = customerSocket.Receive(data);
            message = Encoding.UTF8.GetString(data,0,length) + "\n";
        }
    }

    public void MySendMessage(string message)
    {
        customerSocket.Send(Encoding.UTF8.GetBytes(message));
        Debug.Log(message);
    }
    private void OnDestroy()
    {
        t.Abort();
        customerSocket.Shutdown(SocketShutdown.Both);
        customerSocket.Close();
    }
}
