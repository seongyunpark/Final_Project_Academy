using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Network;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Mang 11. 3
/// 
/// �÷��̾��� �α��ο� ���� Ŭ����
/// </summary>
public class LoginInfo : MonoBehaviour
{
    LogData m_NowLogInfo;                                       // ���� �Է��ϴ� �����͸� ��� ������ ����

    public TMP_InputField m_InputID;                           // �÷��̾ �Է��� ID
    public TMP_InputField m_InputPW;                           // �÷��̾ �Է��� PW

    public TextMeshProUGUI m_PopupNotice;                       // �ȳ����� ����� ����

    private Network network;                // �������� Ȯ���� �����͸� ���� ����

    public GameObject m_AcademySetting;

    private string serverStr = "";

    private bool isLoginSuccess = false;

    public string ServerStr
    {
        get { return serverStr; }
        set
        {
            serverStr = value;
            if (!isLoginSuccess)
            {
                LoginTest();
            }
        }
    }

    private void Awake()
    {
        network = Network.instance;
        serverStr = " ";
    }

    // Start is called before the first frame update
    void Start()
    {
        m_NowLogInfo = new LogData();       // ��� �α��� �����Ͱ� ����� ��ųʸ� �ʱ�ȭ?
    }

    // Update is called once per frame
    void Update()
    {
        if (network.MysData != " " && (network.MysData == "�α��� ����" || network.MysData == "�α��� ����"))
        {
            ServerStr = network.MysData;
            network.MysData = " ";
        }
    }

    public void CheckLoginData()
    {
        m_NowLogInfo.MyID = m_InputID.text;
        m_NowLogInfo.MyPW = m_InputPW.text;

        //network.SelectMessage(m_NowLogInfo, this.gameObject.name);

        SHA256Managed sha256Managed = new SHA256Managed();
        byte[] encryptBytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(m_NowLogInfo.MyPW));

        string encryptPw = Convert.ToBase64String(encryptBytes);

        ClientPacket cp = new ClientPacket();
        cp.m_packetType = PacketType.Login;
        cp.m_id = m_NowLogInfo.MyID;
        cp.m_pw = encryptPw;

        network.PacketSend(cp);

        // �α���
        //if (m_NowLogInfo.MyID != "" && m_NowLogInfo.MyPW != "" && network.MysData == "�α��� ����")   // ���̵� �Է��ߴٸ�
        //{
        //    PlayerInfo.Instance.m_PlayerID = m_NowLogInfo.MyID;

        //    // ��ī���� & ���� �̸� ���ϱ� 
        //    m_AcademySetting.SetActive(true);

        //}
        //else if (m_NowLogInfo.MyID != "" && m_NowLogInfo.MyPW != "" && network.MysData == "�α��� ����")   //  �ƹ��͵� �Է� ���ߴٸ�
        //{
        //    m_PopupNotice.text = network.MysData;

        //    // '��й�ȣ�� ���� �ʽ��ϴ�' �˾�â ����
        //    m_PopupNotice.gameObject.SetActive(true);
        //}

    }

    // ���̵� ã��
    public void FindIDs()
    {

    }

    // �Խ�Ʈ�α����Լ�
    public void GuestLoginInfoSave()
    {
        // �ӽ����� �ο�
        m_NowLogInfo.MyID = "@GuestLogin";
        Debug.Log("Guest : " + m_NowLogInfo.MyID);

        PlayerInfo.Instance.m_PlayerID = m_NowLogInfo.MyID;

        // string str = JsonUtility.ToJson(m_NowLogInfo.MyID);     // �����͸� ���̽� �������� ��ȯ

        // Debug.Log("To Json? :" + str);
    }

    public void LoginTest()
    {
        if (m_NowLogInfo.MyID != "" && m_NowLogInfo.MyPW != "" && serverStr == "�α��� ����")   // ���̵� �Է��ߴٸ�
        {
            PlayerInfo.Instance.m_PlayerID = m_NowLogInfo.MyID;
            isLoginSuccess = true;

            // ��ī���� & ���� �̸� ���ϱ� 
            m_AcademySetting.SetActive(true);
        }
        else if (m_NowLogInfo.MyID != "" && m_NowLogInfo.MyPW != "" && serverStr == "�α��� ����")   //  �ƹ��͵� �Է� ���ߴٸ�
        {
            m_PopupNotice.text = serverStr;

            m_PopupNotice.gameObject.SetActive(true);
            m_PopupNotice.gameObject.GetComponent<PopOffUI>().DelayTurnOffUI();    // �˾�â ���� 
        }
    }
}
