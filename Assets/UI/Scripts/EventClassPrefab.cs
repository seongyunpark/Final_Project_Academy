using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
// using StatData.Runtime;


public class SaveEventClassData
{
    public string EventKeyward;                 // �������� �������� ������ ��Ʈ��
    public string EventClassName;               // �̺�Ʈ �̸�
    public int[] EventRewardStat = new int[6];  // ���� - ����
    // public List<int> EventRewardStat = new List<int>();
    public int EventRewardMoney;                // ���� - �Ӵ�
    public string EventInformation;             // �̺�Ʈ ���� ����

    // �̺�Ʈ ������ ��¥�� �ʿ��Ϸ���?

}

// ������ ������ ��� : �������� ObjectManager.Instance.m_StudentList �� ������ ���� ���ָ� �ȴ�
// ������ ��ȭ�� ��� : PlayerInfo.Instance.m_MyMoney ���� ���������� �ٲ��ָ� �ȴ�
/// <summary>
/// Mang 2023. 01. 05
/// 
/// �� Ŭ������ �̺�Ʈ�� ��� �������� �������ִ� Ŭ����
/// �Ͻ������� ���� �̺�Ʈ�������̹Ƿ� ���⿡ EventSchedule Ŭ����(�̱���) �� ���� �̺�Ʈ ��Ͽ� 
/// �����͵��� ����ֵ��� ����
/// </summary>

public class EventClassPrefab : MonoBehaviour
{
    int ArrDefaultSize = 5;
    // Inspector â�� �������� ������
    public GameObject m_prefab;
    public Transform m_Parent;
    public GameObject m_SelectedEventInfo;



    // Json ������ �Ľ��ؼ� �� �����͵��� �� ��� �� ����Ʈ ����
    // �� �����鵵 EventSchedule �� Instance.���� �鿡 �־��ְ� ������ ����
    public List<SaveEventClassData> SelectEventClassInfo = new List<SaveEventClassData>();      // ��ü ���� �̺�Ʈ
    public List<SaveEventClassData> FixedEventClassInfo = new List<SaveEventClassData>();       // ��ü ���� �̺�Ʈ
    public List<SaveEventClassData> PossibleChooseEventClassList = new List<SaveEventClassData>();      //  ��밡���� �̺�Ʈ ���

    // Start is called before the first frame update
    void Start()
    {
        // 1. ���̽� ���� ��ü �̺�Ʈ����Ʈ ������ ���

        // (�ӽ÷� ���⼭)2. ��ü �̺�Ʈ ����Ʈ���� ��밡���� �̺�Ʈ�� ��밡���̺�Ʈ ����Ʈ ������ ���
        for (int i = 0; i < 5; i++)
        {
            // �׽�Ʈ
            // ���̽� ���� ���� �� �׽�Ʈ�� ���� ���� ����
            SaveEventClassData TempEventData = new SaveEventClassData();

            // �̺�Ʈ struct ���� �ʱ�ȭ ���ֱ�
            TempEventData.EventClassName = "test" + i;
            // if (TempEventData.EventReward.Length > 1)
            // {
            //     for (int j = 0; j < TempEventData.EventReward.Length; j++)
            //     {
            //     }
            // }
            TempEventData.EventRewardStat[0] = 5;
            TempEventData.EventRewardStat[1] = 30;
            TempEventData.EventRewardStat[2] = 100;
            TempEventData.EventRewardMoney = 1000;

            PossibleChooseEventClassList.Add(TempEventData);
        }
        // ����Ʈ�� ���̽� �Ľ� �� �� : .Add() -> �ѹ��� �� �Ľ��ϴ°� �ƴ϶� 1. ���̽����� �ϳ� �о �ӽú����� ��� ����Ʈ�� �ӽú��� �ֱ�
        // �״��� �ϳ� �а� �ӽú��� ��� ����Ʈ �ֱ� �ݺ�! ����...
        // 

        // ObjectManager.Instance.m_StudentList[i].m_StudentData.m_StudentContentsValue += 3;       �̷��� �߰�

        // for (int i = 0; i < SelectEventClassInfo.Count; i++)
        // {
        //     PossibleChooseEventClassList.
        // }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PutEventInPossibleList()
    {

    }


    // ��ư ������ �� �̺�Ʈ��ư ��ϵ��� �������� ������ �� �Լ�
    public void MakeEventClass()
    {
        // 2. ��ü �̺�Ʈ ����Ʈ���� ��밡���� �̺�Ʈ�� ��밡���̺�Ʈ ����Ʈ ������ ���

        // ���� ���õ� ���� ������Ʈ�� �˾ƿ��� �ǰ�
        // GameObject _nowObj = EventSystem.current.currentSelectedGameObject;

        // EventBox �������� ������ ����� ���� ���� �ݺ���
        for (int i = 0; i < PossibleChooseEventClassList.Count; i++)
        {
            // �������� ������ �� �̺�Ʈ ���� ��ũ�Ѻ��� �̺�Ʈ����Ʈ
            GameObject EventList = GameObject.Instantiate(m_prefab, m_Parent);

            // Debug.Log(PossibleChooseEventClassList[i].EventClassName);
            // Debug.Log(PossibleChooseEventClassList[i].EventReward[0]);



            EventList.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventClassName;

            int BoxArrow = 0;       // ����ĭ�� ����ų �ӽú���

            // ���� ĭ�� ä��� ���� �����Ͱ� �ִ� �� Ȯ�� �� �ݺ��� (���� , �Ӵ�)
            for (int j = 0; j < PossibleChooseEventClassList[i].EventRewardStat.Length; j++)
            {
                // ���� ������ ������ �Ѿ��
                if (PossibleChooseEventClassList[i].EventRewardStat[j] != 0)
                {
                    //
                    EventList.transform.GetChild(1).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventRewardStat[j].ToString();

                    BoxArrow += 1;
                }
            }
            EventList.transform.GetChild(1).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventRewardMoney.ToString();
        }
    }

    // �̺�Ʈ ����Ʈ ���� Ŭ������ �� ���� Ŭ���� �̺�Ʈ�� ����
    public void ShowSelectedEventInfo()
    {
        GameObject _EventDataObj = GameObject.Find("Reward_Image");
        GameObject _NowEvent = EventSystem.current.currentSelectedGameObject;

        // if (_NowEvent.name == )
        {

        }
    }
}
