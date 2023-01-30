using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SaveEventData
{
    public int[] EventDate = new int[4];                               // �̺�Ʈ�� ����� ��¥ �迭( ��, ��, ��, ����)
    public bool IsPossibleUseEvent = false;
    public bool IsFixedEvent;                                               // �������� �������� ������ ��Ʈ��
    public int EventNumber;                                                 // �ر� �̺�Ʈ �� �ΰ��� ������ ��ȣ�� ���ؼ� ������ �ر�
    public string EventClassName;                                           // �̺�Ʈ �̸� -> �ر��� ���� ���ǰ� �ر��� �̸� or ���ڸ� ����? �ϸ� ���� ������?
    public string EventInformation;                                         // �̺�Ʈ ���� ����
    public bool IsPopUp = false;

    public const int RewardStatCount = 6;                                   // �������� ������ 6��

    public string[] EventRewardStatName = new string[RewardStatCount];      // ���� - �����̸�
    public float[] EventRewardStat = new float[RewardStatCount];            // ���� - ���ȼ�ġ
    public int EventRewardMoney;                                            // ���� - �Ӵ�

}

/// <summary>
/// 2023. 01. 16 Mang
/// 
/// ���⼭ ���� ������ �����տ� �����͸� �ְ�, �����Ͱ� �� ������Ʈ�� �ű��, �� ���� ���̴�
/// �̱������� ����, ������ ���� �ʿ��� ������ �� �� �ֵ���
/// </summary>
public class SwitchEventList : MonoBehaviour
{
    private static SwitchEventList _instance = null;

    // Inspector â�� �������� ������
    [Tooltip("�̺�Ʈ ���� â�� ��밡�� �̺�Ʈ����� ����� ���� �����պ�����")]
    [Header("EventList Prefab")]
    public GameObject m_PossibleEventprefab;     // 
    public Transform m_PossibleParentScroll;      // 

    [Space(10f)]
    [Tooltip("�̺�Ʈ ���� â ������ �� ���� ���� â�� �θ�")]
    [Header("EventList Prefab")]
    public GameObject m_EventInfoParent;     // 

    [Space(10f)]
    [Tooltip("�޷� â�� ���õ� �̺�Ʈ ����Ʈ�� ���� �����պ�����")]
    [Header("SelectdEvent&SetOkEvent Prefab")]
    public GameObject m_FixedPrefab;
    public GameObject m_SelectedPrefab;
    public Transform m_ParentCalenderScroll;

    [Space(10f)]
    [Tooltip("�޷� â�� ���õ� �̺�Ʈ ����")]
    [Header("SelectdEvent On Calender")]
    public GameObject m_EventInfoOnCaledner;

    [Space(10f)]
    [Tooltip("�̺�Ʈ ���� �� ��� �� �� �ִ� ��ư")]
    [Header("Event Setting Screen CancleButton")]
    public GameObject m_CancleEventButton;

    [Space(10f)]
    [Header("EventInfoWhiteScreen")]
    public GameObject WhiteScreen;

    // Json ������ �Ľ��ؼ� �� �����͵��� �� ��� �� ����Ʈ ����
    // �� �����鵵 EventSchedule �� Instance.���� �鿡 �־��ְ� ������ ����
    public List<SaveEventData> SelectEventClassInfo = new List<SaveEventData>();              // ��ü ���� �̺�Ʈ
    public List<SaveEventData> FixedEventClassInfo = new List<SaveEventData>();               // ��ü ���� �̺�Ʈ
    //

    public List<SaveEventData> PossibleChooseEventClassList = new List<SaveEventData>();      //  ��밡���� �����̺�Ʈ ���
    public List<SaveEventData> MyEventList = new List<SaveEventData>();                       // ���� ���� �̺�Ʈ ���

    public List<SaveEventData> PrevIChoosedEvent = new List<SaveEventData>();                 // ���� ������ �̺�Ʈ ��� �� �ӽ� ����
    public SaveEventData TempIChoosed;

    int month;

    const int FixedEvenetCount = 3;
    const int SelectedEventCount = 2;
    const int PossibleEventCount = 10;

    public bool IsSetEventList = false;

    [SerializeField] PopOffUI _PopOfEventCalenderPanel;

    public static SwitchEventList Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 1. ���̽� ���� ��ü �̺�Ʈ����Ʈ ������ ���

        // �����̺�Ʈ
        SaveEventData TempFixedData = new SaveEventData();
        SaveEventData TempFixedData1 = new SaveEventData();

        TempFixedData.EventClassName = "FixedTestEvent0";
        TempFixedData.EventDate[0] = 1;
        TempFixedData.EventDate[1] = 3;     // 3��
        TempFixedData.EventDate[2] = 2;     // 2����
        TempFixedData.EventDate[3] = 4;     // �����

        TempFixedData.EventInformation = "�̺�Ʈ����";
        TempFixedData.IsFixedEvent = true;
        TempFixedData.IsPossibleUseEvent = true;

        TempFixedData.EventRewardStatName[0] = "StatName0";
        TempFixedData.EventRewardStat[0] = 2 + (1 * 3);

        TempFixedData.EventRewardStatName[1] = "StatName1";
        TempFixedData.EventRewardStat[1] = 8 + (3 * 7);

        TempFixedData.EventRewardStatName[2] = "StatName2";
        TempFixedData.EventRewardStat[2] = 46 + (2 * 2);

        TempFixedData.EventRewardMoney = 386 + (1 * 4);

        FixedEventClassInfo.Add(TempFixedData);
        ////
        TempFixedData1.EventClassName = "FixedTestEvent1";
        TempFixedData1.EventDate[0] = 1;
        TempFixedData1.EventDate[1] = 4;     // 4��
        TempFixedData1.EventDate[2] = 1;      // 1����
        TempFixedData1.EventDate[3] = 2;      // ȭ����

        TempFixedData1.EventInformation = "�̺�Ʈ����";
        TempFixedData1.IsFixedEvent = true;
        TempFixedData1.IsPossibleUseEvent = true;

        TempFixedData1.EventRewardStatName[0] = "StatName0";
        TempFixedData1.EventRewardStat[0] = 2 + (1 * 4);

        TempFixedData1.EventRewardStatName[1] = "StatName1";
        TempFixedData1.EventRewardStat[1] = 8 + (3 * 3);

        TempFixedData1.EventRewardStatName[2] = "StatName2";
        TempFixedData1.EventRewardStat[2] = 46 + (2 * 5);

        TempFixedData1.EventRewardMoney = 386 + (1 * 8);

        FixedEventClassInfo.Add(TempFixedData1);
        ////


        // �����̺�Ʈ
        // (�ӽ÷� ���⼭)2. ��ü �̺�Ʈ ����Ʈ���� ��밡���� �̺�Ʈ�� ��밡���̺�Ʈ ����Ʈ ������ ���
        // ���̽� ���� ���� �� �׽�Ʈ�� ���� ���� ����
        SaveEventData TempEventData = new SaveEventData();
        SaveEventData TempEventData1 = new SaveEventData();
        SaveEventData TempEventData2 = new SaveEventData();
        SaveEventData TempEventData3 = new SaveEventData();


        // �̺�Ʈ struct ���� �ʱ�ȭ ���ֱ�
        TempEventData.EventClassName = "test 0";
        TempEventData.EventDate[0] = 1;

        TempEventData.EventDate[1] = 4;

        TempEventData.IsPossibleUseEvent = false;
        TempEventData.IsFixedEvent = false;      // �����̺�Ʈ���� �����̺�Ʈ���� ������ Ű����
        TempEventData.EventRewardStatName[0] = "StatName0";
        TempEventData.EventRewardStat[0] = 5 + (1 * 3);

        TempEventData.EventRewardStatName[1] = "StatName1";
        TempEventData.EventRewardStat[1] = 30 + (1 * 7);

        TempEventData.EventRewardStatName[2] = "StatName2";
        TempEventData.EventRewardStat[2] = 100 + (2 * 2);

        TempEventData.EventRewardMoney = 5247 + (3 * 4);

        SelectEventClassInfo.Add(TempEventData);
        ////
        TempEventData1.EventClassName = "test 1";
        TempEventData1.EventDate[0] = 1;

        TempEventData1.EventDate[1] = 3;
        TempEventData1.IsPossibleUseEvent = false;
        TempEventData1.IsFixedEvent = false;      // �����̺�Ʈ���� �����̺�Ʈ���� ������ Ű����
        TempEventData1.EventRewardStatName[0] = "StatName0";
        TempEventData1.EventRewardStat[0] = 5 + (1 * 3);

        TempEventData1.EventRewardStatName[1] = "StatName1";
        TempEventData1.EventRewardStat[1] = 30 + (1 * 7);

        TempEventData1.EventRewardStatName[2] = "StatName2";
        TempEventData1.EventRewardStat[2] = 100 + (2 * 2);

        TempEventData1.EventRewardMoney = 5247 + (3 * 4);

        SelectEventClassInfo.Add(TempEventData1);
        ////
        TempEventData2.EventClassName = "test 2";
        TempEventData2.EventDate[0] = 1;

        TempEventData2.EventDate[1] = 3;

        TempEventData2.IsPossibleUseEvent = false;
        TempEventData2.IsFixedEvent = false;      // �����̺�Ʈ���� �����̺�Ʈ���� ������ Ű����
        TempEventData2.EventRewardStatName[0] = "StatName0";
        TempEventData2.EventRewardStat[0] = 5 + (1 * 3);

        TempEventData2.EventRewardStatName[1] = "StatName1";
        TempEventData2.EventRewardStat[1] = 30 + (1 * 7);

        TempEventData2.EventRewardStatName[2] = "StatName2";
        TempEventData2.EventRewardStat[2] = 100 + (2 * 2);

        TempEventData2.EventRewardMoney = 5247 + (3 * 4);

        SelectEventClassInfo.Add(TempEventData2);
        ////
        TempEventData3.EventClassName = "test 3";
        TempEventData3.EventDate[0] = 1;

        TempEventData3.EventDate[1] = 5;

        TempEventData3.IsPossibleUseEvent = false;
        TempEventData3.IsFixedEvent = false;      // �����̺�Ʈ���� �����̺�Ʈ���� ������ Ű����
        TempEventData3.EventRewardStatName[0] = "StatName0";
        TempEventData3.EventRewardStat[0] = 5 + (1 * 3);

        TempEventData3.EventRewardStatName[1] = "StatName1";
        TempEventData3.EventRewardStat[1] = 30 + (1 * 7);

        TempEventData3.EventRewardStatName[2] = "StatName2";
        TempEventData3.EventRewardStat[2] = 100 + (2 * 2);

        TempEventData3.EventRewardMoney = 5247 + (3 * 4);

        SelectEventClassInfo.Add(TempEventData3);

    }

    // Update is called once per frame
    void Update()
    {
        // ���� �ٲ� ������ ������ ���� �� �� �� ���� ���� �̺�Ʈ ������ �ް���
        if (IsSetEventList == false && month != GameTime.Instance.FlowTime.NowMonth)
        {
            month = GameTime.Instance.FlowTime.NowMonth;

            PutOnFixedEventData(month);     //�����̺�Ʈ
            PutOnPossibleEventData(month);  // ���ð����̺�Ʈ �־��ֱ�
        }
    }

    // ���ǿ� �´� �����̺�Ʈ�����͸� MyFixedEventList �� �־��ֱ�
    public void PutOnFixedEventData(int month)
    {
        GameObject CalenderEventList;               // �����̺�Ʈ�� �θ�

        // ������ƮǮ�� �ٽ� �ֱ�
        // �̹� null�̸� �Ѿ��
        if (m_ParentCalenderScroll.transform.childCount != 0)
        {
            for (int i = 0; i < m_ParentCalenderScroll.childCount; i++)
            {
                MailObjectPool.ReturnFixedEventObject(m_ParentCalenderScroll.gameObject);
            }
        }

        // �����̺�Ʈ -> �� ���Ǹ� ���缭 MyFixedEventList�� ������ �ȴ�
        for (int i = 0; i < FixedEventClassInfo.Count; i++)
        {
            // - ���� & �����̺�Ʈ���� & ��밡������
            if (month == FixedEventClassInfo[i].EventDate[1]
                && FixedEventClassInfo[i].IsPossibleUseEvent)
            {
                // �θ� �ȿ� ��ü�� �ű��
                CalenderEventList = MailObjectPool.GetFixedEventObject(m_ParentCalenderScroll);

                // ������ �ʿ��� ���� ��ŭ �ɷ����Ƿ� �ٷ� ScrollView �� ������Ʈ�� �ִ´�
                CalenderEventList.name = FixedEventClassInfo[i].EventClassName;

                CalenderEventList.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FixedEventClassInfo[i].EventDate[2].ToString();
                CalenderEventList.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = FixedEventClassInfo[i].EventDate[3].ToString();
                CalenderEventList.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = FixedEventClassInfo[i].EventClassName;

                MyEventList.Add(FixedEventClassInfo[i]);

                EventSchedule.Instance.ShowFixedEventOnCalender();     // ���� �̺�Ʈ �����α�
            }
        }
    }

    // ���ð��� �̺�Ʈ ����Ʈ�� ���� �ֱ�
    public void PutOnPossibleEventData(int month)
    {
        GameObject PossibleEventList;              // ���ð����̺�Ʈ�� �θ�

        m_CancleEventButton.SetActive(false);
        WhiteScreen.SetActive(true);
        int tempScrollChild = 0;

        for (int i = 0; i < SelectEventClassInfo.Count; i++)
        {
            int statArrow = 0;

            // ���� �̺�Ʈ & �����̺�Ʈ���� & ��밡������ / ��밡������
            if ((month == SelectEventClassInfo[i].EventDate[1] && SelectEventClassInfo[i].IsPossibleUseEvent == true)
                || SelectEventClassInfo[i].IsPossibleUseEvent == true
                || month == SelectEventClassInfo[i].EventDate[1])
            {
                PossibleEventList = MailObjectPool.GetPossibleEventObject(m_PossibleParentScroll);

                PossibleEventList.name = SelectEventClassInfo[i].EventClassName;
                PossibleEventList.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SelectEventClassInfo[i].EventClassName;

                if (SelectEventClassInfo[i].EventRewardMoney != 0)
                {
                    PossibleEventList.transform.GetChild(1).GetChild(statArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = SelectEventClassInfo[i].EventRewardMoney.ToString();

                    statArrow += 1;
                }

                for (int j = 0; j < SelectEventClassInfo[i].EventRewardStat.Length; j++)
                {
                    if (SelectEventClassInfo[i].EventRewardStat[j] != 0)
                    {
                        PossibleEventList.transform.GetChild(1).GetChild(statArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = SelectEventClassInfo[i].EventRewardStatName[j];
                        statArrow += 1;
                    }
                    else
                    {
                        PossibleEventList.transform.GetChild(1).GetChild(statArrow).gameObject.SetActive(false);
                        statArrow += 1;
                    }
                }

                // ���ð��� �̺�Ʈ ��ũ�Ѻ信�� ��ư�� Ŭ���� ���� �Լ��� �Ѿ���� �Ѵ�.
                PossibleEventList.GetComponent<Button>().onClick.AddListener(ShowISelectedPossibleEvent);

                PossibleChooseEventClassList.Add(SelectEventClassInfo[i]);

                tempScrollChild += 1;
            }

            statArrow = 0;
        }

        IsSetEventList = true;
    }

    // ���� ���ð����� �̺�Ʈ�� Ŭ�� ���� �� �ٷο��� �̺�Ʈ ����â�� ������ �̺�Ʈ�� ������ �����ְ�, 
    public void ShowISelectedPossibleEvent()
    {
        GameObject _NowEvent = EventSystem.current.currentSelectedGameObject;
        WhiteScreen.SetActive(false);

        Debug.Log("�̺�Ʈ ����");

        for (int i = 0; i < PossibleChooseEventClassList.Count; i++)
        {
            int statArrow = 0;

            if (_NowEvent.name == PossibleChooseEventClassList[i].EventClassName)
            {
                m_EventInfoParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventClassName;
                m_EventInfoParent.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventInformation;

                if (PossibleChooseEventClassList[i].EventRewardMoney != 0)
                {
                    m_EventInfoParent.transform.GetChild(2).GetChild(statArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = "�Ӵ�";
                    m_EventInfoParent.transform.GetChild(2).GetChild(statArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventRewardMoney.ToString();

                    statArrow += 1;
                }

                for (int j = 0; j < PossibleChooseEventClassList[i].EventRewardStat.Length; j++)
                {
                    if (SelectEventClassInfo[i].EventRewardStat[j] != 0)
                    {
                        m_EventInfoParent.transform.GetChild(2).GetChild(statArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventRewardStatName[j];
                        m_EventInfoParent.transform.GetChild(2).GetChild(statArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventRewardStat[j].ToString();
                    }
                    else
                    {
                        m_EventInfoParent.transform.GetChild(2).GetChild(statArrow).gameObject.SetActive(false);
                    }
                    statArrow += 1;
                }
                //// ���⼭ �ӽ÷� ���� ������ �����͸� ��Ƶд�
                TempIChoosed = PossibleChooseEventClassList[i];
                EventSchedule.Instance.tempEventList = PossibleChooseEventClassList[i];
                EventSchedule.Instance.Choose_Button.transform.GetComponent<Button>().interactable = true;
            }
        }

        // ���� ������ �̺�Ʈ�� ������ ���� ���� ���ΰ� �޶����� ���ǹ�
        if (PrevIChoosedEvent.Count != 0)
        {
            for (int i = 0; i < PrevIChoosedEvent.Count; i++)
            {
                if (_NowEvent.name == PrevIChoosedEvent[i].EventClassName)
                {
                    EventSchedule.Instance.Choose_Button.transform.GetComponent<Button>().interactable = false;
                    m_CancleEventButton.SetActive(true);

                    break;
                }
                else if (_NowEvent.name != PrevIChoosedEvent[i].EventClassName)
                {
                    EventSchedule.Instance.Choose_Button.transform.GetComponent<Button>().interactable = true;
                    m_CancleEventButton.SetActive(false);
                }
            }

            if (PrevIChoosedEvent.Count == 2)
            {
                EventSchedule.Instance.Choose_Button.transform.GetComponent<Button>().interactable = false;
            }
        }
        else if (PrevIChoosedEvent.Count == 0)
        {
            EventSchedule.Instance.Choose_Button.transform.GetComponent<Button>().interactable = true;
            m_CancleEventButton.SetActive(false);
        }


        // �̺�Ʈ ���� â�� ����ϰ� �Ⱥ��̰� �ϴٰ� ������ ���̰� �ϱ� ���� ������� ���� ���
        if (WhiteScreen.activeSelf == true)
        {
            WhiteScreen.SetActive(false);
        }
    }

    // ���� �̺�Ʈ - ���� ���� �����ߴ� �̺�Ʈ���� ��� �� ����Ʈ���� �����ϱ� /  �޷�â �������� ������ �����ϱ�
    public void PushCancleButton()
    {
        for (int i = 0; i < PrevIChoosedEvent.Count; i++)
        {
            if (PrevIChoosedEvent[i].EventClassName == TempIChoosed.EventClassName)
            {
                EventSchedule.Instance.Choose_Button.transform.GetComponent<Button>().interactable = true;
                PrevIChoosedEvent.RemoveAt(i);

                for (int j = 0; j < m_ParentCalenderScroll.childCount; j++)
                {
                    if (m_ParentCalenderScroll.GetChild(j).name == TempIChoosed.EventClassName)
                    {
                        MailObjectPool.ReturnSelectedEventObject(m_ParentCalenderScroll.GetChild(j).gameObject);

                        break;
                    }
                }
            }
        }
    }

    // ����, ���� �̺�Ʈ�� ù��° ���� ���� ���� �־����Ƿ� ������ �ٷ� �޷�â�� Scroll View�� �ֱ�
    // ������ Possible Scrollview �� �ֱ�
    public void PutMySelectedEventOnCalender()
    {
        // PrevIChoosedEvent.Add(TempIChoosed);        // ���⼭ �ӽ÷� ���� ������ �����͸� ��Ƶд�

        // �����̺�Ʈ �ֱ�
        for (int i = 0; i < PossibleChooseEventClassList.Count; i++)
        {
            int statArrow = 0;

            m_EventInfoOnCaledner.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = month.ToString();       // �ش� ��
            m_EventInfoOnCaledner.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = TempIChoosed.EventClassName;      // ���� �̺�Ʈ �̸�
            m_EventInfoOnCaledner.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = TempIChoosed.EventInformation;    // ���� �̺�Ʈ ����

            if (TempIChoosed.EventRewardMoney != 0)
            {
                m_EventInfoOnCaledner.transform.GetChild(4).GetChild(statArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = "�Ӵ�";
                m_EventInfoOnCaledner.transform.GetChild(4).GetChild(statArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = TempIChoosed.EventRewardMoney.ToString();
                statArrow += 1;
            }

            for (int j = 0; j < TempIChoosed.EventRewardStat.Length; j++)
            {
                if (TempIChoosed.EventRewardStat[j] != 0)
                {

                    m_EventInfoOnCaledner.transform.GetChild(4).GetChild(statArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = TempIChoosed.EventRewardStatName[j];
                    m_EventInfoOnCaledner.transform.GetChild(4).GetChild(statArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = TempIChoosed.EventRewardStat[j].ToString();
                    statArrow += 1;
                }
                else
                {
                    m_EventInfoOnCaledner.transform.GetChild(4).GetChild(statArrow).gameObject.SetActive(false);
                    statArrow += 1;
                }
            }
        }

        // IfIClickEventSetOKButton();
    }

    // ������Ʈ�� �ٽ� ������Ʈ Ǯ�� �ǵ��� ����ϴµ� ������ ť�� ��ť �ؾ���
    public void ReturnEventPrefabToPool()
    {
        int _DestroyMailObj = m_ParentCalenderScroll.childCount;        // 

        for (int i = _DestroyMailObj - 1; i >= 0; i--)
        {
            if (m_ParentCalenderScroll.GetChild(i).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text == "����")
            {
                MailObjectPool.ReturnFixedEventObject(m_ParentCalenderScroll.GetChild(i).gameObject);
            }
            else if (m_ParentCalenderScroll.GetChild(i).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text == "����")
            {
                MailObjectPool.ReturnSelectedEventObject(m_ParentCalenderScroll.GetChild(i).gameObject);
            }
        }
    }

    //  �����̺�Ʈ ���� �ΰ� ����� �д� - �̰� �ƴϴ�!
    public void PutMyEventListOnCalenderPage()
    {
        GameObject SetEventList;

        for (int i = 0; i < 2; i++)
        {
            SetEventList = MailObjectPool.GetPossibleEventObject(m_ParentCalenderScroll);

            SetEventList.transform.gameObject.SetActive(false);
        }
    }

    public void PutSelectEventOnCalender()
    {
        for (int i = 0; i < MyEventList.Count; i++)
        {
            if (MyEventList[i].IsFixedEvent == false)
            {
                string temp = m_ParentCalenderScroll.GetChild(i).name;
                if (temp == MyEventList[i].EventClassName)
                {
                    m_ParentCalenderScroll.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FixedEventClassInfo[i].EventDate[2].ToString();
                    m_ParentCalenderScroll.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = FixedEventClassInfo[i].EventDate[3].ToString();
                    m_ParentCalenderScroll.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = FixedEventClassInfo[i].EventClassName;
                    m_ParentCalenderScroll.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    // ��¥�� Ŭ�� �� ���� ��ư�� ���� ���� �̺�Ʈ�� ������� Ȯ���� ���´�
    public void IfIGaveTheEventDate()
    {
        if (EventSchedule.Instance.tempEventList.EventDate[2] != 0)
        {
            GameObject SetEventList;
            SetEventList = MailObjectPool.GetSelectedEventObject(m_ParentCalenderScroll);       //������ �̺�Ʈ ������ ����������

            PrevIChoosedEvent.Add(TempIChoosed);        // ���⼭ �ӽ÷� ���� ������ �����͸� ��Ƶд�

            // ���� �̺�Ʈ ���� �ֱ�
            SetEventList.name = TempIChoosed.EventClassName;
            SetEventList.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = TempIChoosed.EventDate[2].ToString();       // �ش� ����
            SetEventList.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = TempIChoosed.EventDate[3].ToString();       // �ش� ����
            SetEventList.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = TempIChoosed.EventClassName;


            // ������ ���� �Ŀ� �ش� �г� setActive(false) ���ֱ�
            _PopOfEventCalenderPanel.TurnOffUI();

            WhiteScreen.SetActive(true);
        }
    }

    public void ResetMyEventList()
    {
        // ReturnEventPrefabToPool();

        int possibleEvent = m_PossibleParentScroll.childCount;
        // 
        for (int i = possibleEvent - 1; i >= 0; i--)
        {
            MailObjectPool.ReturnPossibleEventObject(m_PossibleParentScroll.GetChild(i).gameObject);
        }

        PossibleChooseEventClassList.Clear();
    }
}

