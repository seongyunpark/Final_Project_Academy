using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // �˾� �Ǿ ���� UI�� ���ִ� �Լ�
    public void TurnOffUI()
    {
        if (m_UI.activeSelf == true)
        {
            m_UI.SetActive(false);

            if (m_ClassPrefab != null)
            {
                m_ClassPrefab.m_SelecteClassDataList.Clear();
            }
        }

        GameTime.Instance.IsGameMode = true;
        Time.timeScale = 1;
        Debug.Log("�ð� �帧");
    }

}