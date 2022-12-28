using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using System.Security.Cryptography;
using Assets.Network;

public class Network : MonoBehaviour
{
    public static Network instance = null;       // Manager ������ �̱������� ���
    static int count = 0;

    // �̱������� ���� ������ �����ϰ� �����ϰ� ����ϱ� ���� �ʱ�ȭ(?) ���
    private void Awake()
    {
        Debug.Log("Awake ùȣ��?");

        // Instance �� �����Ѵٸ� gameObject ����
        if (instance != null)
        {
            Debug.Log("Awake ��ȣ�� ����");
            count++;
            Debug.Log(count);
            return;
        }

        // ���۵� �� �ν��Ͻ� �ʱ�ȭ, ���� �Ѿ���� �����Ǳ� ���� ó��
        instance = this;
        Debug.Log("Awake ���ο� ����");
        count++;
        Debug.Log(count);

    }

    private Socket m_ClientSock = null;      // ��Ʈ��ũ ������ ����� �ȉ����
    private Socket m_CbSock;
    private byte[] m_RecvBuffer;
    private const int MAXSIZE = 4096;
    //private string HOST = "192.168.0.33";
    private string HOST = "221.163.91.111";
    private int PORT = 3000;

    private string sData;        // �������� ���ƿ��� ����?

    #region sData_Property
    public string MysData
    {
        set { sData = value; }
        get { return sData; }
    }
    #endregion

    // delegate void ctrl_Invoke(TextBox ctrl, string message, string Netmessage);

    // Start is called before the first frame update
    void Start()
    {
        sData = " ";
    }

    public void CreateSocket()
    {
        Debug.Log("Socket ���� ȣ��?");
        if (m_ClientSock == null)
        {

            Debug.Log("���� ����");
            m_RecvBuffer = new byte[MAXSIZE];
            m_ClientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            TryConnect();
        }
    }

    // �α��� ��ư ������ ������ ����õ�
    public void TryConnect()
    {
        try
        {
            Debug.Log("���� ����õ�?");
            m_ClientSock.BeginConnect(HOST, PORT, new AsyncCallback(ConnectCallBack), m_ClientSock);
        }
        catch (SocketException e)
        {
            Debug.Log("���� ���� �ȵ�");
            Debug.Log(e.Message);
            // DialogResult dr = MessageBox.Show("���ӿ� �����Ͽ����ϴ�. �ٽ� �õ��Ͻðڽ��ϱ�?", "Client", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            // if (dr == DialogResult.Yes)
            // {
            //     TryConnect();
            // }
            // else
            // {
            //     this.Close();
            // }
        }
    }

    // �� ��ǻ�Ͱ� ������ �������� ������ -> �����κ��� ��ȯ(���Ῡ��)
    private void ConnectCallBack(IAsyncResult IAR)
    {
        try
        {
            Socket tempSocket = (Socket)IAR.AsyncState;
            IPEndPoint svEP = (IPEndPoint)tempSocket.RemoteEndPoint;

            if (m_ClientSock.Connected == true)
                Debug.Log("���� ���� ���� ");

            // MessageBox.Show("���� ����", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);

            tempSocket.EndConnect(IAR);
            m_CbSock = tempSocket;
            m_CbSock.BeginReceive(m_RecvBuffer, 0, m_RecvBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallBack), m_CbSock);
        }
        catch (SocketException e)
        {
            if (e.SocketErrorCode == SocketError.NotConnected)
            {
                Debug.Log("���� ���� ���� ");
                // MessageBox.Show("���� ���� ����", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TryConnect();
            }
        }
    }

    public void PacketSend(ClientPacket cp)
    {
        SocketAsyncEventArgs sendEventArgs = new SocketAsyncEventArgs();
        if (sendEventArgs == null)
        {
            //MessageBox.Show("SocketAsyncEventArgs is null");
            return;
        }

        sendEventArgs.Completed += SendComplected;
        sendEventArgs.UserToken = this;

        byte[] sendData = cp.Serialize();

        sendEventArgs.SetBuffer(sendData, 0, sendData.Length);

        bool pending = m_ClientSock.SendAsync(sendEventArgs);
        if (!pending)
        {
            SendComplected(null, sendEventArgs);
        }
    }

    private void SendComplected(object sender, SocketAsyncEventArgs e)
    {

    }

    // �α��� ��ư ������ �������� ������ Ȯ�� �� ��ȯ�ϴ� ������
    private void OnReceiveCallBack(IAsyncResult IAR)
    {
        Debug.Log("OnReceiveCallBack �Լ� ȣ��");

        try
        {
            Socket tempSock = (Socket)IAR.AsyncState;
            int nReadSize = tempSock.EndReceive(IAR);

            if (nReadSize != 0)
            {
                var sp = ServerPacket.Deserialize(m_RecvBuffer);

                PacketCheck(sp);

                //sData = Encoding.Unicode.GetString(m_RecvBuffer, 0, nReadSize);

                //Debug.Log("�����κ��� �α��� ��� ����");
                //Debug.Log("sdata : " + sData);

                //// MessageBox.Show(sData);
                //if (sData == "�α��� ����")
                //{
                //    Debug.Log("�α��� ����");
                //    // SetInvisible();
                //    // Form3 lobbyForm = new Form3();
                //    // lobbyForm.SetForm(this);
                //    // lobbyForm.UserID = textBox1.Text;
                //    // lobbyForm.ShowDialog();
                //}
            }

            Receive();
        }
        catch (SocketException e)
        {
            if (e.SocketErrorCode == SocketError.ConnectionReset)
            {
                TryConnect();
            }
        }
    }

    // ���Ͽ� �����Ͱ� ���ö����� ���?
    public void Receive()
    {
        m_CbSock.BeginReceive(m_RecvBuffer, 0, m_RecvBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallBack), m_CbSock);
    }

    // �α��� �ҋ��� ������
    // ���̵� ��й�ȣ�� �������ٰ� ���� �õ�
    // message ���ٰ� ID + PW ���� �־ �����ؾ���
    public void TrySend(string message)
    {
        Debug.Log("������?");

        try
        {
            Debug.Log("try ������?");
            if (m_ClientSock.Connected)
            {
                Debug.Log("������ ������ ������? ���۽õ�");
                byte[] buffer = Encoding.Unicode.GetBytes(message);
                m_ClientSock.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), message);
            }
        }
        catch (SocketException e)
        {
            Debug.Log("���۽���?");
            // MessageBox.Show("���� ����", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // TrySend() ���� ���� message�� ���� �� �Լ�?
    private void SendCallBack(IAsyncResult IAR)
    {
        string message = (string)IAR.AsyncState;

        Debug.Log("���� �Ϸ�?");
        // MessageBox.Show("���� �Ϸ�", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // TrySend() ���� �� �б�
    //public void SelectMessage(LogData data, string button)
    //{
    //    string message = "";


    //    if (button == "Login_Button")          // �α��� ��ư ��������
    //    {
    //        Debug.Log("�α��� ��ư Ŭ�� -> ");

    //        SHA256Managed sha256Managed = new SHA256Managed();
    //        byte[] encrptBytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(data.MyPW));

    //        string encryptString = Convert.ToBase64String(encrptBytes);
    //        message = "@login@" + "@id@" + data.MyID + "@pw@" + encryptString;
    //        SendPacket(data.MyID, encryptString);
    //    }
    //    else if (button == "CheckID_Button")     // ���̵� �ߺ� Ȯ��
    //    {
    //        Debug.Log("���̵� �ߺ� Ȯ�� -> ");

    //        message = "@idcheck@" + data.MyID;
    //    }
    //    else if (button == "Make_Button")     // ȸ������
    //    {
    //        Debug.Log("ȸ������ -> ");

    //        SHA256Managed sha256Managed = new SHA256Managed();
    //        byte[] encrptBytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(data.MyPW));

    //        string encryptString = Convert.ToBase64String(encrptBytes);
    //        message = "@signup@" + "@id@" + data.MyID + "@pw@" + encryptString;
    //        SendPacket(data.MyID, encryptString);
    //    }

    //    //TrySend(message);
    //}

    private void PacketCheck(ServerPacket sp)
    {
        if (sp.m_packetType == PacketType.Login)
        {
            if (sp.m_isSuccess)
            {
                //m_form1.SetInvisible();
                //Form3 lobbyForm = new Form3();
                //lobbyForm.SetForm(m_form1);
                //lobbyForm.UserID = m_userId;
                //lobbyForm.ShowDialog();

                // �α��� ���� ó��
                Debug.Log("�α��� ����");
                sData = "�α��� ����";
            }
            else
            {
                Debug.Log("�α��� ����");
                sData = "�α��� ����";
                // MessageBox.Show("���̵� �Ǵ� ��й�ȣ�� �߸��Ǿ����ϴ�.");
            }
        }

        else if (sp.m_packetType == PacketType.IdCheck)
        {
            if (sp.m_isSuccess)
            {
                Debug.Log("���̵� ���� ����");
                sData = "���̵� ���� ����";
                //MessageBox.Show("���̵� ������ �����մϴ�.");
            }
            else
            {
                Debug.Log("���̵� �ߺ�");
                sData = "���̵� �ߺ�";
                //MessageBox.Show("���̵� �ߺ��Դϴ�.");
            }
        }

        else if (sp.m_packetType == PacketType.SignUp)
        {
            if (sp.m_isSuccess)
            {
                //MessageBox.Show("ȸ������ ����");
                //m_form2.FormClose();
                Debug.Log("ȸ������ ����");
            }
            else
            {
                //MessageBox.Show("���̵� �Ǵ� ��й�ȣ�� Ȯ�����ּ���.");
                Debug.Log("ȸ������ ����");
            }
        }

        else if (sp.m_packetType == PacketType.DuplicationCheck)
        {
            if (sp.m_isSuccess)
            {
                // true
            }
            else
            {
                //MessageBox.Show("�ߺ� IP���� ���ӵǾ����ϴ�.");
                //m_form1.FormClose();
                Debug.Log("�ߺ� IP���� ���ӵǾ����ϴ�.");
            }
        }
    }

    // m_ClientSock�� �� ����� üũ
    public bool CheckServerConnect()
    {
        if (m_ClientSock == null)           // ���� ���� X
        {

            return false;
        }
        else                               // ���� ���� O
        {
            return true;
        }
    }
}
