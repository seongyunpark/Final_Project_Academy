using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Mang 10.18
/// 
/// UI�� PopUp ������ ������� ��ũ��Ʈ
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

    Vector3 target;

    public void Start()
    {
        // float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        target = new Vector3(m_UI.transform.position.x - ((float)Screen.width / 2f), m_UI.transform.position.y, m_UI.transform.position.z);
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

    // ��ư�� ������ �Ѱ�
    public void TurnOnUI()
    {
        InGameUI.Instance.UIStack.Push(this.gameObject);
        Debug.Log("stack count : " + InGameUI.Instance.UIStack.Count);

        if (InGameUI.Instance.UIStack != null)
        {
            this.gameObject.SetActive(true);

            if (m_UI.gameObject.activeSelf == false)
            {
                m_UI.gameObject.SetActive(true);
            }

            if (SceneManager.GetActiveScene().name == "InGameScene")
            {
                if (GameTime.Instance != null)
                {
                    GameTime.Instance.IsGameMode = false;
                }
                Time.timeScale = 0;

                Debug.Log("�ð� ����");
            }
        }
    }

    public void JustTurnOn()
    {
        InGameUI.Instance.UIStack.Push(this.gameObject);
        Debug.Log("stack count : " + InGameUI.Instance.UIStack.Count);

        if (InGameUI.Instance.UIStack != null)
        {
            if (this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
            }
        }

        if (SceneManager.GetActiveScene().name == "InGameScene")
        {
            if (GameTime.Instance != null)
            {
                GameTime.Instance.IsGameMode = false;
            }
            Time.timeScale = 0;

            Debug.Log("�ð� ����");
        }
    }

    // ���� �˾�â�� �� �ָ鼭 �� ���� UI �� ���ش�.
    public void PopUpMyUI()
    {
        InGameUI.Instance.UIStack.Push(this.gameObject);
        Debug.Log("stack count : " + InGameUI.Instance.UIStack.Count);

        if (InGameUI.Instance.UIStack.Count != 0)
        {
            if (m_UI.gameObject.activeSelf == false)
            {
                InGameUI.Instance.UIStack.Pop();
                m_UI.gameObject.SetActive(true);    // �� ��ũ��Ʈ�� �پ��ִ� ������Ʈ ����
                Debug.Log("stack count : " + InGameUI.Instance.UIStack.Count);

                //InGameUI.Instance.UIStack.Push(m_UI.gameObject);
                this.gameObject.SetActive(false);   // ������ ��ư�� ���ֱ�
                //Debug.Log("stack count : " + InGameUI.Instance.UIStack.Count);
            }
        }

        if (SceneManager.GetActiveScene().name == "InGameScene")
        {
            if (GameTime.Instance != null)
            {
                GameTime.Instance.IsGameMode = false;
            }
            Time.timeScale = 0;

            Debug.Log("�ð� ����");
        }
    }

    // �����̴� UI�� �ӵ�
    [SerializeField]
    float moveSpeed = 0;

    [SerializeField]            // �̵���Ű�� ���� ��ǥ
    float movedPositionX = 0;

    Vector3 prevMovedPosition = new Vector3(0, 0, 0);

    // UI �޴��� ������ �˾����� ������ �Լ� (���� �̵�)
    public void SlidePopUpUI()
    {
        // ���� ������Ʈ ��ǥ�� �̵���ų ��ǥ�� ���� ���� ��
        if (m_UI.transform.position != target)
        {

            prevMovedPosition = m_UI.transform.position;
            m_UI.transform.position = target;
        }
        else
        {
            m_UI.transform.position = prevMovedPosition;
            prevMovedPosition = new Vector3(0, 0, 0);
        }

    }
}
