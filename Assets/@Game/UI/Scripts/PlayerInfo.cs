using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mang 11. 08
/// 
/// �÷��̾� ��ü�� ����
/// ���⿡ �÷��̾ ������� �����͵��� �� �־�θ� �������� ����������Ұ� ��
/// </summary>
public class PlayerInfo
{
    private static PlayerInfo instance = null;       // Manager ������ �̱������� ���

    public static PlayerInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerInfo();
            }
            return instance;
        }
    }

    // �α��� ������
    public string m_PlayerID;
    public string m_AcademyName;
    public string m_DirectorName;


    // �ΰ��� ������
    // ���� ����
    // ���� �л�
    // ���� �ݾ�
    public int m_MyMoney = 10000;
    // ����Ʈ
    // ������
    // Ķ����
    // ģ��
    // ��¥

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
