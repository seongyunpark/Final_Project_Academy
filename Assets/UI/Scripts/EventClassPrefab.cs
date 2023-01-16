using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
// using StatData.Runtime;


public class SaveEventClassData
{
    public string[] EventDay = new string[4];                               // �̺�Ʈ�� ����� ��¥ �迭( ��, ��, ��, ����)
    public bool IsPossibleUseEvent = false;
    public bool IsFixedEvent;                                               // �������� �������� ������ ��Ʈ��
    public int EventNumber;                                                 // �ر� �̺�Ʈ �� �ΰ��� ������ ��ȣ�� ���ؼ� ������ �ر�
    public string EventClassName;                                           // �̺�Ʈ �̸� -> �ر��� ���� ���ǰ� �ر��� �̸� or ���ڸ� ����? �ϸ� ���� ������?
    public string EventInformation;                                         // �̺�Ʈ ���� ����

    public const int RewardStatCount = 6;                                   // �������� ������ 6��

    public string[] EventRewardStatName = new string[RewardStatCount];      // ���� - �����̸�
    public float[] EventRewardStat = new float[RewardStatCount];            // ���� - ���ȼ�ġ
    public int EventRewardMoney;                                            // ���� - �Ӵ�

    // �̺�Ʈ ������ ��¥�� �ʿ��Ϸ���? �̰� �̺�Ʈ������ ��ũ��Ʈ���� �ǵ帮��

}

// ������ ������ ��� : �������� ObjectManager.Instance.m_StudentList �� ������ ���� ���ָ� �ȴ�
// ������ ��ȭ�� ��� : PlayerInfo.Instance.m_MyMoney ���� ���������� �ٲ��ָ� �ȴ�
/// <summary>
/// Mang 2023. 01. 05
/// 
/// �� Ŭ������ �̺�Ʈ�� ��� �������� �������ִ� Ŭ����
/// 
/// �Ͻ������� ���� �̺�Ʈ�������̹Ƿ� ���⼭ ����Ʈ ���� ������ �� ������ ����
/// 
/// 
/// </summary>

public class EventClassPrefab : MonoBehaviour
{
    // ������ ���� �ؼ� ����� �� ������ ->  �迭�� �ϴ� ���� : ������ ��ȭ ����, List �� �ӵ� ����
    GameObject[] m_PossibleEvent = new GameObject[PossibleEventCount];
    GameObject[] m_FixedEvent = new GameObject[FixedEvenetCount];
    GameObject[] m_SelectedEvent = new GameObject[SelectedEventCount];

    // Inspector â�� �������� ������
    [Tooltip("�̺�Ʈ ���� â�� ��밡�� �̺�Ʈ����� ����� ���� �����պ�����")]
    [Header("EventList Prefab")]
    public GameObject m_PossibleEventprefab;     // 
    public Transform m_PossibleParentScroll;      // 

    [Space(10f)]
    [Tooltip("�޷� â�� ���õ� �̺�Ʈ ����Ʈ�� ���� �����պ�����")]
    [Header("SelectdEvent&SetOkEvent Prefab")]
    public GameObject m_FixedPrefab;
    public GameObject m_SelectedPrefab;
    public Transform m_ParentCalenderScroll;

    [Space(10f)]
    public GameObject m_SelectedEventInfo;

    public GameObject RewardInfoBG;

    // Json ������ �Ľ��ؼ� �� �����͵��� �� ��� �� ����Ʈ ����
    // �� �����鵵 EventSchedule �� Instance.���� �鿡 �־��ְ� ������ ����
    public List<SaveEventClassData> SelectEventClassInfo = new List<SaveEventClassData>();       // ��ü ���� �̺�Ʈ
    public List<SaveEventClassData> FixedEventClassInfo = new List<SaveEventClassData>();           // ��ü ���� �̺�Ʈ

    public List<SaveEventClassData> PossibleChooseEventClassList = new List<SaveEventClassData>();      //  ��밡���� �����̺�Ʈ ���

    public SaveEventClassData IfIChoosedEvent;           // ���� ������ �̺�Ʈ ��� �� �ӽ� ����

    string month;

    const int FixedEvenetCount = 3;
    const int SelectedEventCount = 2;
    const int PossibleEventCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        month = GameTime.Instance.FlowTime.NowMonth;
        // 1. ���̽� ���� ��ü �̺�Ʈ����Ʈ ������ ���

        // �����̺�Ʈ
        SaveEventClassData TempFixedData = new SaveEventClassData();
        TempFixedData.EventClassName = "FixedTestEvent0";
        TempFixedData.EventDay[0] = "1";
        TempFixedData.EventDay[1] = GameTime.Instance.Month[2];
        TempFixedData.EventDay[2] = GameTime.Instance.Week[1];
        TempFixedData.EventDay[3] = GameTime.Instance.Day[3];

        TempFixedData.EventInformation = "�̺�Ʈ����";
        TempFixedData.IsFixedEvent = true;

        TempFixedData.EventRewardStatName[0] = "StatName0";
        TempFixedData.EventRewardStat[0] = 2 + (1 * 3);

        TempFixedData.EventRewardStatName[1] = "StatName1";
        TempFixedData.EventRewardStat[1] = 8 + (3 * 7);

        TempFixedData.EventRewardStatName[2] = "StatName2";
        TempFixedData.EventRewardStat[2] = 46 + (2 * 2);

        TempFixedData.EventRewardMoney = 386 + (1 * 4);

        FixedEventClassInfo.Add(TempFixedData);

        // �����̺�Ʈ
        // (�ӽ÷� ���⼭)2. ��ü �̺�Ʈ ����Ʈ���� ��밡���� �̺�Ʈ�� ��밡���̺�Ʈ ����Ʈ ������ ���
        for (int i = 0; i < 5; i++)
        {
            int index = i;
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

            TempEventData.EventDay[0] = "1��";

            if (4 <= index)
            {
                index = 0;
            }
            TempEventData.EventDay[1] = GameTime.Instance.Month[index];
            TempEventData.EventDay[2] = GameTime.Instance.Week[index];
            TempEventData.EventDay[3] = GameTime.Instance.Day[index];

            TempEventData.IsPossibleUseEvent = true;
            TempEventData.IsFixedEvent = false;      // �����̺�Ʈ���� �����̺�Ʈ���� ������ Ű����
            TempEventData.EventRewardStatName[0] = "StatName0";
            TempEventData.EventRewardStat[0] = 5 + (i * 3);

            TempEventData.EventRewardStatName[1] = "StatName1";
            TempEventData.EventRewardStat[1] = 30 + (i * 7);

            TempEventData.EventRewardStatName[2] = "StatName2";
            TempEventData.EventRewardStat[2] = 100 + (i * 2);

            TempEventData.EventRewardMoney = 5247 + (i * 4);

            SelectEventClassInfo.Add(TempEventData);
        }

        // ����Ʈ�� ���̽� �Ľ� �� �� : .Add() -> �ѹ��� �� �Ľ��ϴ°� �ƴ϶� 1. ���̽����� �ϳ� �о �ӽú����� ��� ����Ʈ�� �ӽú��� �ֱ�
        // �״��� �ϳ� �а� �ӽú��� ��� ����Ʈ �ֱ� �ݺ�! ����...
        // 

        // ObjectManager.Instance.m_StudentList[i].m_StudentData.m_StudentContentsValue += 3;       �̷��� �߰�

        // for (int i = 0; i < SelectEventClassInfo.Count; i++)
        // {
        //     PossibleChooseEventClassList.
        // }
        // LoadFixedEventList(month);

        // MakeSelectedEventInfoPrefab();
        // MakePossibleEventPrefab();
        // PutOnPossibleEventData(month);
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �ٲ� ������ ������ ���� �� �� �� ���� ���� �̺�Ʈ ������ �ް���
        if (month != GameTime.Instance.FlowTime.NowMonth)
        {
            month = GameTime.Instance.FlowTime.NowMonth;

            // LoadFixedEventList(month);
            PutOnPossibleEventData(month);
        }
    }

    // ���õ� �̺�Ʈ�� �����ر� ���� ��� �����ո� ���� ( )
    public void MakeSelectedEventInfoPrefab()
    {
        // ��밡���� �����̺�Ʈ ������ �����
        for(int i = 0; i < PossibleEventCount; i++)
        {
            m_PossibleEvent[i] = GameObject.Instantiate(m_PossibleEventprefab, m_PossibleParentScroll);
        }

        // ���õ� ���� �̺�Ʈ ������ �����
        for (int i = 0; i < FixedEvenetCount; i++)
        {
            m_FixedEvent[i] = GameObject.Instantiate(m_FixedPrefab, m_ParentCalenderScroll);

            m_FixedEvent[i].SetActive(false);

        }

        // ���õ� ���� �̺�Ʈ ������ �����
        for (int i = 0; i < SelectedEventCount; i++)
        {
            m_SelectedEvent[i] = GameObject.Instantiate(m_SelectedPrefab, m_ParentCalenderScroll);

            m_SelectedEvent[i].SetActive(false);
        }
    }

    // ���� ���� ����/ ���� ����Ʈ ��� ������ ����
    public void MakePossibleEventPrefab()
    {
        for (int i = 0; i < PossibleEventCount; i++)
        {
            m_PossibleEvent[i] = GameObject.Instantiate(m_PossibleEventprefab, m_PossibleParentScroll);
        }
    }

    // ���ǿ� ���� ����� �� ���� ������ �̺�Ʈ ����� ���� ��밡���� �̺�Ʈ ����Ʈ�� �־��ֱ� ���� �Լ�
    // ���� ���� �̺�Ʈ�� ������ ���� ��� ���������� ����
    // �� �� ���� �� �� ���� ���ư� �Լ�
    public void PutOnPossibleEventData(string month)
    {
        // �����̺�Ʈ -> ���� ���缭 MyEventList�� ������ �ȴ�
        for (int i = 0; i < FixedEventClassInfo.Count; i++)
        {
            // - ���� & �����̺�Ʈ���� & ��밡������
            if (month == FixedEventClassInfo[i].EventDay[1]
                && FixedEventClassInfo[i].IsPossibleUseEvent)

            {
                EventSchedule.Instance.MyEventList.Add(SelectEventClassInfo[i]);
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < SelectEventClassInfo.Count; i++)
        {
            // ���� �̺�Ʈ & �����̺�Ʈ���� & ��밡������ / ��밡������
            if ((month == SelectEventClassInfo[i].EventDay[1]
                && SelectEventClassInfo[i].IsPossibleUseEvent == true)
                || SelectEventClassInfo[i].IsPossibleUseEvent == true)
            {
                PossibleChooseEventClassList.Add(SelectEventClassInfo[i]);
            }
            else
            {
                break;
            }
        }
    }

    // ���� . ���� �̺�Ʈ�� �����͸� ������ �����տ� �����͸� �־��ֱ� ���� �Լ�
    public void PutPossibleEventDataOnPrefab()
    {

    }

    // ��ư ������ �� �̺�Ʈ��ư ��ϵ��� �������� ������ �� �Լ�
    public void MakeEventClass()
    {
        RewardInfoBG.SetActive(true);

        // 2. ��ü �̺�Ʈ ����Ʈ���� ��밡���� �̺�Ʈ�� ��밡���̺�Ʈ ����Ʈ ������ ���

        // ���� ���õ� ���� ������Ʈ�� �˾ƿ��� �ǰ�
        // GameObject _nowObj = EventSystem.current.currentSelectedGameObject;

        // EventBox �������� ������ ����� ���� ���� �ݺ���
        foreach (var possibleEvent in PossibleChooseEventClassList)
        {
            // �������� ������ �� �̺�Ʈ ���� ��ũ�Ѻ��� �̺�Ʈ����Ʈ
            GameObject EventList = GameObject.Instantiate(m_PossibleEventprefab, m_PossibleParentScroll);

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
                // �̺�Ʈ �̸� , ����
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
                EventSchedule.Instance.tempEventList = possibleEvent;
                EventSchedule.Instance.Choose_Button.transform.GetComponent<Button>().interactable = true;

                Debug.Log(IfIChoosedEvent.EventClassName);

            }
        }

        // �̺�Ʈ ���� â�� ����ϰ� �Ⱥ��̰� �ϴٰ� ������ ���̰� �ϱ� ���� ������� ���� ���
        if (RewardInfoBG.activeSelf == true)
        {
            RewardInfoBG.SetActive(false);
        }
    }

    // �̺�Ʈ ���� �� �޷�â���� ���� ������ �̺�Ʈ ���� â
    public void SelectedEventScreen()
    {
        GameObject _EventDataObj = GameObject.Find("CalenderRewardInfo");

        // ���� ��
        _EventDataObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameTime.Instance.FlowTime.NowMonth;

        // �̺�Ʈ �̸�
        _EventDataObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = IfIChoosedEvent.EventClassName;
        // �̺�Ʈ ����
        _EventDataObj.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = IfIChoosedEvent.EventInformation;

        // �̺�Ʈ ����
        int BoxArrow = 0;
        // �Ӵ�
        if (IfIChoosedEvent.EventRewardMoney != 0)
        {
            _EventDataObj.transform.GetChild(4).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = "�Ӵ�";
            _EventDataObj.transform.GetChild(4).GetChild(BoxArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = IfIChoosedEvent.EventRewardMoney.ToString();
            BoxArrow += 1;
        }

        // ����
        for (int i = 0; i < SaveEventClassData.RewardStatCount; i++)
        {
            if (IfIChoosedEvent.EventRewardStat[i] != 0)
            {
                _EventDataObj.transform.GetChild(4).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = IfIChoosedEvent.EventRewardStatName[i];
                _EventDataObj.transform.GetChild(4).GetChild(BoxArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = IfIChoosedEvent.EventRewardStat[i].ToString();
                BoxArrow += 1;
            }
            else
            {
                _EventDataObj.transform.GetChild(4).GetChild(BoxArrow).gameObject.SetActive(false);
                BoxArrow += 1;
            }
        }
    }

    // �� ���ϸ鼭 ���� �̺�Ʈ�� ���� ���� �̺�Ʈ ��Ͽ� �־��ֱ� ���� �Լ�
    public void LoadFixedEventList(string month)
    {
        for (int i = 0; i < FixedEventClassInfo.Count; i++)
        {
            if (month == FixedEventClassInfo[i].EventDay[1])
            {
                SaveEventClassData tempFixed = new SaveEventClassData();

                tempFixed = FixedEventClassInfo[i];

                EventSchedule.Instance.MyEventList.Add(tempFixed);
            }
        }

        // ���� �̺�Ʈ�� MyEventList�� �� �־��� �� ���⼭ �������� ����� �д�
        EventSchedule.Instance.MakeFixedEventInfoPrefab();
    }
}
