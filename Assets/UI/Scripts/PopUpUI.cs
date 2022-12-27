using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mang 10.18
/// 
/// UI�� PopUp ������ ������� ��ũ��ƮS
/// </summary>
public class PopUpUI : MonoBehaviour
{
    public GameObject m_UI;         // ������ UI!
    public int m_DelayTime;

    [SerializeField]
    private GameObject MyUI
    {
        set { m_UI = value; }
    }

    [SerializeField]
    private int DelayTime
    {
        set { m_DelayTime = value; }
    }

    // UI�� ���ִ� �ð� ���� �Լ�
    public void DelayTurnOnUI(/*int t*/)
    {
        //if(t != 0)
        //{
        //    m_DelayTime = t;
        //}

        Invoke("JustTurnOn", m_DelayTime);
    }

    // ��ư�� ������ �׳� ���ֱ�
    public void TurnOnUI()
    {
        this.gameObject.SetActive(true);

        if (m_UI.gameObject.activeSelf == false)
        {
            m_UI.gameObject.SetActive(true);
        }

        GameTime.Instance.IsGameMode = false;
        Time.timeScale = 0;
        Debug.Log("�ð� ����");
    }

    public void JustTurnOn()
    {
        if (this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
        }

        GameTime.Instance.IsGameMode = false;
        Time.timeScale = 0;
        Debug.Log("�ð� ����");
    }

    // ���� �˾�â�� �� �ָ鼭 �� ���� UI �� ���ش�.
    public void PopUpMyUI()
    {
        if (m_UI.gameObject.activeSelf == false)
        {
            m_UI.gameObject.SetActive(true);    // �� ��ũ��Ʈ�� �پ��ִ� ������Ʈ ����
            this.gameObject.SetActive(false);   // ������ ��ư�� ���ֱ�
        }

        GameTime.Instance.IsGameMode = false;
        Time.timeScale = 0;
        Debug.Log("�ð� ����");
    }


}
