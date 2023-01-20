using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Network;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Mang 10. 19
/// 
/// �÷��̾ ���������� ��� �� �� �� ������ �����ϱ� ���� struct
/// </summary>
[System.Serializable]
public class LogData
{
    // �÷��̾� ȸ�� ���� �� �ʿ��� ���� / 'm_IDName' �� ��� ������ �־�� �� ������
    private string m_IDName;
    // �÷��̾� ȸ�� ���� �� �ʿ��� �н�����
    private string m_PWName;

    #region LogData_Property

    // ����Ƽ property
    public string MyID
    {
        get { return m_IDName; }
        set { m_IDName = value; }
    }

    public string MyPW
    {
        get { return m_PWName; }
        set { m_PWName = value; }
    }
    #endregion
}

/// <summary>
/// Mang 11. 3
/// 
/// �÷��̾��� ȸ�����Կ� ���� Ŭ����
/// </summary>
public class SignInInfo : MonoBehaviour
{
    LogData m_NowLogInfo;                                       // ���� �Է��ϴ� �����͸� ��� ������ ����

    public TMP_InputField m_InputID;                            // �÷��̾ �Է��� ID
    public TMP_InputField m_InputPW;                            // �÷��̾ �Է��� PW
    public TMP_InputField m_InputCheckPW;                       // ��й�ȣ Ȯ��

    public TextMeshProUGUI m_PopupNotice;                       // �ȳ����� ����� ����

    private Network network;                 // �������� Ȯ���� �����͸� ���� ����

    private string serverStr = "";

    public string ServerStr
    {
        get { return serverStr; }
        set
        {
            serverStr = value;
            if (serverStr == "���̵� �ߺ�" || serverStr == "���̵� ���� ����")
            {
                IdCheckResult();
            }
            
        }
    }

    // �Լ��� ����Ǿ����� �Ǻ��� bool ����
    // bool m_isSaved = false;

    private void Awake()
    {
        network = Network.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_NowLogInfo = new LogData();       // ��� �α��� �����Ͱ� ����� ��ųʸ� �ʱ�ȭ?
        serverStr = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if (network.MysData != " " && (network.MysData == "���̵� �ߺ�" || network.MysData == "���̵� ���� ����"))
        {
            ServerStr = network.MysData;
            network.MysData = " ";
        }
    }

    public void CheckID()
    {
        m_NowLogInfo.MyID = m_InputID.text;

        // ������ ���� ��������
        // ���⼭ selectMessage �Լ� �ҷ��� ������ �־���� �ҰͰ��� ���⼭ �̸��� �־��ֵ��� ����

        if (network.CheckServerConnect() == true && m_InputID.text != "")        // ���� ���� �������� && ��ĭ�� �ƴҶ�
        {
            // ���⼭ ��Ʈ��ũ - ����Ʈ �޼��� �Լ��� �ҷ����ҰŰ�����
            // network.SelectMessage(m_NowLogInfo, this.gameObject.name);
            ClientPacket cp = new ClientPacket();
            cp.m_packetType = PacketType.IdCheck;
            cp.m_id = m_InputID.text;

            network.PacketSend(cp);

            //if (network.MysData == "���̵� ���� �Ұ�")                    // �Է¿� Ư�����ڰ��� ���� ����ֵ���
            //{
            //    Debug.Log("��ĭ�̾�~");

            //    m_PopupNotice.text = network.MysData;

            //    // '�ٸ� ���̵� �Է����ּ���' �˾�â
            //    m_PopupNotice.gameObject.SetActive(true);
            //    m_PopupNotice.gameObject.GetComponent<PopOffUI>().DelayTurnOffUI();
            //}
            
        }
        else          // ������ ������ �ʿ��ϰų� ���ڿ��϶�
        {
            m_PopupNotice.text = "��Ʈ��ũ ���� �ʿ�";

            // ��Ʈ��ũ ���� �ʿ� �˾�â
            m_PopupNotice.gameObject.SetActive(true);
            m_PopupNotice.gameObject.GetComponent<PopOffUI>().DelayTurnOffUI();
        }

    }

    private void IdCheckResult()
    {
        if (serverStr == "���̵� �ߺ�")                // ���̵� �ߺ�üũ
        {
            Debug.Log("���̵� �ߺ��̾�~");

            m_PopupNotice.text = network.MysData;

            // '���̵� �ߺ�' �˾�â
            m_PopupNotice.gameObject.SetActive(true);
            m_PopupNotice.gameObject.GetComponent<PopOffUI>().DelayTurnOffUI();
        }
        else if (serverStr == "���̵� ���� ����")      // ���̵� �ߺ��ƴҰ��
        {
            m_PopupNotice.text = network.MysData;

            // '���̵� ���� ����' �˾�â
            m_PopupNotice.gameObject.SetActive(true);
            m_PopupNotice.gameObject.GetComponent<PopOffUI>().DelayTurnOffUI();
        }
    }

    public void CheckPW()
    {
        // ��й�ȣ & ��й�ȣ Ȯ�� 
        if (m_InputPW.text != m_InputCheckPW.text)
        {
            m_PopupNotice.text = "��й�ȣ�� ���� �ʽ��ϴ�";

            // '��й�ȣ�� ���� �ʽ��ϴ�' �˾�â ����
            m_PopupNotice.gameObject.SetActive(true);

            Debug.Log("��й�ȣ �ٸ�");

            m_PopupNotice.gameObject.GetComponent<PopOffUI>().DelayTurnOffUI();    // �˾�â ���� 
        }
        else                // ���⼭ ������ �������ֱ�
        {
            m_PopupNotice.text = "���̵� ���� �Ϸ�";

            // "���̵� ���� �Ϸ�" �˾�â ����
            m_PopupNotice.gameObject.SetActive(true);

            Debug.Log("���̵� ���� �Ϸ�");

            m_PopupNotice.gameObject.GetComponent<PopOffUI>().DelayTurnOffUI();    // �˾�â ���� 

            m_NowLogInfo.MyID = m_InputID.text;
            m_NowLogInfo.MyPW = m_InputPW.text;

            // Json inst = new Json();

            //network.SelectMessage(m_NowLogInfo, this.gameObject.name);      // �α��� ������ ������ ����

            SHA256Managed sha256Managed = new SHA256Managed();
            byte[] encryptBytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(m_NowLogInfo.MyPW));

            string encryptPw = Convert.ToBase64String(encryptBytes);

            ClientPacket cp = new ClientPacket();
            cp.m_packetType = PacketType.SignUp;
            cp.m_id = m_NowLogInfo.MyID;
            cp.m_pw = encryptPw;

            network.PacketSend(cp);

            Debug.Log("ID : " + m_NowLogInfo.MyID);
            Debug.Log("PW : " + m_NowLogInfo.MyPW);

            // PlayerInfo.instance.MyID = m_NowLogInfo.MyID;

            // string str = JsonUtility.ToJson(m_NowLogInfo.MyID);     // �����͸� ���̽� �������� ��ȯ

            // Debug.Log("To Json? :" + str);


        }
    }
}