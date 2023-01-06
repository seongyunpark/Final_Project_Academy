using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
// using StatData.Runtime;


public class SaveEventClassData
{
    public string[] EventDay = new string[4];               // �̺�Ʈ�� ����� ��¥ �迭( ��, ��, ��, ����)
    public string EventKeyward;                             // �������� �������� ������ ��Ʈ��
    public string EventClassName;                           // �̺�Ʈ �̸�
    public string EventInformation;                         // �̺�Ʈ ���� ����

    public const int RewardStatCount = 6;

    public string[] EventRewardStatName = new string[RewardStatCount];    // ���� - �����̸�
    public float[] EventRewardStat = new float[RewardStatCount];          // ���� - ���ȼ�ġ
    public int EventRewardMoney;                            // ���� - �Ӵ�

    // �̺�Ʈ ������ ��¥�� �ʿ��Ϸ���? �̰� �̺�Ʈ������ ��ũ��Ʈ���� �ǵ帮��

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
    // Inspector â�� �������� ������
    public GameObject m_prefab;
    public Transform m_Parent;
    public GameObject m_SelectedEventInfo;

    public GameObject RewardInfoBG;

    // Json ������ �Ľ��ؼ� �� �����͵��� �� ��� �� ����Ʈ ����
    // �� �����鵵 EventSchedule �� Instance.���� �鿡 �־��ְ� ������ ����
    public List<SaveEventClassData> SelectEventClassInfo = new List<SaveEventClassData>();      // ��ü ���� �̺�Ʈ
    public List<SaveEventClassData> FixedEventClassInfo = new List<SaveEventClassData>();       // ��ü ���� �̺�Ʈ
    public List<SaveEventClassData> PossibleChooseEventClassList = new List<SaveEventClassData>();      //  ��밡���� �̺�Ʈ ���

    public SaveEventClassData IfIChoosedEvent;           // ���� ������ �̺�Ʈ ��� �� �ӽ� ����

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
            TempEventData.EventRewardStatName[0] = "StatName0";
            TempEventData.EventRewardStat[0] = 5;

            TempEventData.EventRewardStatName[1] = "StatName1";
            TempEventData.EventRewardStat[1] = 30;

            TempEventData.EventRewardStatName[2] = "StatName2";
            TempEventData.EventRewardStat[2] = 100;

            TempEventData.EventRewardMoney = 5247;

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
        RewardInfoBG.SetActive(false);

        // 2. ��ü �̺�Ʈ ����Ʈ���� ��밡���� �̺�Ʈ�� ��밡���̺�Ʈ ����Ʈ ������ ���

        // ���� ���õ� ���� ������Ʈ�� �˾ƿ��� �ǰ�
        // GameObject _nowObj = EventSystem.current.currentSelectedGameObject;

        // EventBox �������� ������ ����� ���� ���� �ݺ���
        foreach (var possibleEvent in PossibleChooseEventClassList)
        {
            // �������� ������ �� �̺�Ʈ ���� ��ũ�Ѻ��� �̺�Ʈ����Ʈ
            GameObject EventList = GameObject.Instantiate(m_prefab, m_Parent);

            EventList.name = possibleEvent.EventClassName;
            EventList.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventClassName;

            int BoxArrow = 0;       // ����ĭ�� ����ų �ӽú���

            if (possibleEvent.EventRewardMoney != 0)
            {
                EventList.transform.GetChild(1).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventRewardMoney.ToString();

                BoxArrow += 1;
            }

            // ���� ĭ�� ä��� ���� �����Ͱ� �ִ� �� Ȯ�� �� �ݺ��� (���� , �Ӵ�)
            for (int j = 0; j < SaveEventClassData.RewardStatCount; j++)
            {
                // ���� ������ ������ �Ѿ��
                if (possibleEvent.EventRewardStat[j] != 0)
                {
                    //
                    EventList.transform.GetChild(1).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventRewardStatName[j].ToString();

                    BoxArrow += 1;
                }
            }


            for (int x = 0; x < EventList.transform.GetChild(1).childCount; x++)
            {
                if (EventList.transform.GetChild(1).GetChild(x).GetChild(0).GetComponent<TextMeshProUGUI>().text == "����")
                {
                    EventList.transform.GetChild(1).GetChild(x).gameObject.SetActive(false);
                }
            }
            EventList.GetComponent<Button>().onClick.AddListener(ShowSelectedEventInfo);
        }
    }

    // �̺�Ʈ ����Ʈ ���� Ŭ������ �� ���� Ŭ���� �̺�Ʈ�� ������ ����ֱ� ���� �Լ�
    public void ShowSelectedEventInfo()
    {
        GameObject _EventDataObj = GameObject.Find("Reward_Image");
        GameObject _NowEvent = EventSystem.current.currentSelectedGameObject;

        // �ش� ��� in �ش縮��Ʈ ��ü
        foreach (var possibleEvent in PossibleChooseEventClassList)
        {
            if (_NowEvent.name == possibleEvent.EventClassName)
            {
                _EventDataObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventClassName;      // �̺�Ʈ �̸�
                _EventDataObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventInformation;      // �̺�Ʈ ����

                int BoxArrow = 0;       // ����ĭ�� ����ų �ӽú���

                // �Ӵ�
                if (possibleEvent.EventRewardMoney != 0)
                {
                    _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).gameObject.SetActive(true);

                    _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = "�Ӵ�";      // 
                    _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventRewardMoney.ToString();

                    BoxArrow += 1;
                }

                // ���� ĭ�� ä��� ���� �����Ͱ� �ִ� �� Ȯ�� �� �ݺ��� (����)
                for (int i = 0; i < SaveEventClassData.RewardStatCount; i++)
                {
                    // ���� ������ ������ �Ѿ��
                    if (possibleEvent.EventRewardStat[i] != 0)
                    {
                        _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).GetChild(0).gameObject.SetActive(true);
                        _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventRewardStatName[i];      // 
                        _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventRewardStat[i].ToString();

                    }
                    else
                    {
                        _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).gameObject.SetActive(false);
                    }

                    BoxArrow += 1;

                }

                // ���⼭ �ӽ÷� ���� ������ �����͸� ��Ƶд�
                IfIChoosedEvent = possibleEvent;
            }
        }

        // �̺�Ʈ ���� â�� ����ϰ� �Ⱥ��̰� �ϴٰ� ������ ���̰� �ϱ� ���� ������� ���� ���
        if (RewardInfoBG.activeSelf == true)
        {
            RewardInfoBG.SetActive(false);
        }
    }

    public void SaveMyEventChoice()
    {
        EventSchedule.Instance.MyEventList.Add(IfIChoosedEvent);

        Debug.Log("������ ����");
    }
}
