using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Mang 10.17 
/// 
/// UI�� PopOff �� ����� �� UI ��ũ��Ʈ
/// </summary>
public class PopOffUI : MonoBehaviour
{
    public GameObject m_UI;         // ������ UI
    public int m_DelayTime;

    [SerializeField] private ClassPrefab m_ClassPrefab;
    [SerializeField] private SelecteProfessor m_ProfessorPrefab;

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

    // ���� �� �� ���� UI�� �� ���α�
    void Start()
    {
        //SetTurnOffUI(m_gameObject);
    }

    // �˾����� ��� UI�� ���� ���� ���� ���α�
    public void SetTurnOffUI(GameObject gObject)
    {
        gObject.SetActive(false);
    }

    // UI�� ���ִ� �ð� ���� �Լ�
    public void DelayTurnOffUI()
    {
        Invoke("TurnOffUI", m_DelayTime);
    }

    public void BackButton()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            m_UI.SetActive(false);
        }
        else
        {
            // ��ư�� Ȱ��ȭ �Ǿ��ִ� �� üũ
            if (m_UI.activeSelf == true)
            {
                m_UI.SetActive(false);

                // �������� �л� �������� �����ϴ��� üũ
                if (m_ClassPrefab != null)
                {
                    if (m_ClassPrefab.m_SelecteClassDataList != null)
                    {
                        m_ClassPrefab.m_SelecteClassDataList.Clear();
                    }

                    if (m_ClassPrefab.m_SelecteClassButtonName != null)
                    {
                        m_ClassPrefab.m_SelecteClassButtonName.Clear();
                    }
                }

                if (m_ProfessorPrefab != null)
                {
                    m_ProfessorPrefab.m_ProfessorDataIndex = 0;

                    if (m_ProfessorPrefab.m_ForClickNextButton != null)
                    {
                        m_ProfessorPrefab.m_ForClickNextButton.Clear();
                    }
                }

                InGameUI.Instance.UIStack.Pop();
                Debug.Log(InGameUI.Instance.UIStack.Count);
            }

            if (InGameUI.Instance.UIStack.Count == 0)
            {
                // �ΰ��� �������� �ð��� üũ�ǵ��� üũ
                if (SceneManager.GetActiveScene().name == "InGameScene")
                {
                    if (GameTime.Instance != null)
                    {
                        GameTime.Instance.IsGameMode = true;
                    }
                    Time.timeScale = 1;

                    Debug.Log("�ð� �帧");
                }
            }
        }
    }

    // �ڷΰ��� ��ư�� ������ ��
    public void TurnOffUI()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            // ��ư�� Ȱ��ȭ �Ǿ��ִ� �� üũ
            if (m_UI.activeSelf == true)
            {
                m_UI.SetActive(false);
            }
        }
        else
        {
            if (InGameUI.Instance.UIStack != null)
            {
                // ��ư�� Ȱ��ȭ �Ǿ��ִ� �� üũ
                if (m_UI.activeSelf == true)
                {
                    m_UI.SetActive(false);

                    // �������� �л� �������� �����ϴ��� üũ
                    if (m_ClassPrefab != null)
                    {
                        if (m_ClassPrefab.m_SelecteClassDataList != null)
                        {
                            m_ClassPrefab.m_SelecteClassDataList.Clear();
                        }

                        if (m_ClassPrefab.m_SelecteClassButtonName != null)
                        {
                            m_ClassPrefab.m_SelecteClassButtonName.Clear();
                        }
                    }

                    if (m_ProfessorPrefab != null)
                    {
                        m_ProfessorPrefab.m_ProfessorDataIndex = 0;

                        if (m_ProfessorPrefab.m_ForClickNextButton != null)
                        {
                            m_ProfessorPrefab.m_ForClickNextButton.Clear();
                        }
                    }

                    InGameUI.Instance.UIStack.Pop();
                    Debug.Log("�˾� â �翩�� ���� : " + InGameUI.Instance.UIStack.Count);
                }
            }

            if (InGameUI.Instance.UIStack.Count == 0)
            {
                // �ΰ��� �������� �ð��� üũ�ǵ��� üũ
                if (SceneManager.GetActiveScene().name == "InGameScene")
                {
                    if (GameTime.Instance != null)
                    {
                        GameTime.Instance.IsGameMode = true;
                    }
                    Time.timeScale = 1;

                    Debug.Log("�ð� �帧");
                }
            }
        }
    }
        // ���� �ϷḦ ������ ��
        //public void SelecteComplete()
        //{
        //    if (m_UI.activeSelf == true)
        //    {
        //        m_UI.SetActive(false);

        //        if (m_ClassPrefab.m_SelecteClassDataList != null)
        //        {
        //            m_ClassPrefab.m_SelecteClassDataList.Clear();
        //        }
        //    }
        //}
}