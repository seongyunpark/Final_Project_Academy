using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveEventData
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
    [Tooltip("�޷� â�� ���õ� �̺�Ʈ ����Ʈ�� ���� �����պ�����")]
    [Header("SelectdEvent&SetOkEvent Prefab")]
    public GameObject m_FixedPrefab;
    public GameObject m_SelectedPrefab;
    public Transform m_ParentCalenderScroll;


    // Json ������ �Ľ��ؼ� �� �����͵��� �� ��� �� ����Ʈ ����
    // �� �����鵵 EventSchedule �� Instance.���� �鿡 �־��ְ� ������ ����
    public List<SaveEventClassData> SelectEventClassInfo = new List<SaveEventClassData>();       // ��ü ���� �̺�Ʈ
    public List<SaveEventClassData> FixedEventClassInfo = new List<SaveEventClassData>();           // ��ü ���� �̺�Ʈ
    //

    public List<SaveEventClassData> PossibleChooseEventClassList = new List<SaveEventClassData>();      //  ��밡���� �����̺�Ʈ ���

    public SaveEventClassData IfIChoosedEvent;           // ���� ������ �̺�Ʈ ��� �� �ӽ� ����

    string month;

    const int FixedEvenetCount = 3;
    const int SelectedEventCount = 2;
    const int PossibleEventCount = 10;

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
        month = GameTime.Instance.FlowTime.NowMonth;
        // 1. ���̽� ���� ��ü �̺�Ʈ����Ʈ ������ ���

        // �����̺�Ʈ
        SaveEventClassData TempFixedData = new SaveEventClassData();
        TempFixedData.EventClassName = "FixedTestEvent0";
        TempFixedData.EventDay[0] = "1";
        TempFixedData.EventDay[1] = GameTime.Instance.Month[2];     // 3��
        TempFixedData.EventDay[2] = GameTime.Instance.Week[1];      // 2����
        TempFixedData.EventDay[3] = GameTime.Instance.Day[3];       // �����

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
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �ٲ� ������ ������ ���� �� �� �� ���� ���� �̺�Ʈ ������ �ް���
        if (month != GameTime.Instance.FlowTime.NowMonth)
        {
            month = GameTime.Instance.FlowTime.NowMonth;

            PutOnFixedEventData(month);     //�����̺�Ʈ
            PutOnPossibleEventData(month);  // ���ð����̺�Ʈ �־��ֱ�
        }
    }

    // �����Ͱ� �ִ��� Ȯ�� �� �ʿ��� ������ƮǮ�� ������Ʈ ������ŭ �ű��
    public void PutOnFixedEventList(string month)
    {
        // FixedEventClassInfo ���⿡ ������ ����ְ�
        // if ()
        {

        }
    }


    // ���ǿ� �´� �����̺�Ʈ�����͸� MyFixedEventList �� �־��ֱ�
    public void PutOnFixedEventData(string month)
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
            if (month == FixedEventClassInfo[i].EventDay[1]
                && FixedEventClassInfo[i].IsPossibleUseEvent)
            {
                // �θ� �ȿ� ��ü�� �ű��
                CalenderEventList = MailObjectPool.GetFixedEventObject(m_ParentCalenderScroll);

                // ������ �ʿ��� ���� ��ŭ �ɷ����Ƿ� �ٷ� ScrollView �� ������Ʈ�� �ִ´�

                CalenderEventList.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FixedEventClassInfo[i].EventDay[2];
                CalenderEventList.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = FixedEventClassInfo[i].EventDay[3];
                CalenderEventList.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = FixedEventClassInfo[i].EventClassName;

                EventSchedule.Instance.MyEventList.Add(SelectEventClassInfo[i]);
            }
            else
            {
                break;
            }
        }

    }

    // ���ð��� �̺�Ʈ ����Ʈ�� ���� �ֱ�
    public void PutOnPossibleEventData(string month)
    {
        GameObject PossibleEventList;              // ���ð����̺�Ʈ�� �θ�

        for (int i = 0; i < SelectEventClassInfo.Count; i++)
        {
            PossibleEventList = MailObjectPool.GetPossibleEventObject(m_PossibleParentScroll);

            // ���� �̺�Ʈ & �����̺�Ʈ���� & ��밡������ / ��밡������
            if ((month == SelectEventClassInfo[i].EventDay[1]
                && SelectEventClassInfo[i].IsPossibleUseEvent == true)
                || SelectEventClassInfo[i].IsPossibleUseEvent == true)
            {
                PossibleEventList.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SelectEventClassInfo[i].EventClassName;

                for (int j = 0; j < SelectEventClassInfo[i].EventRewardStat.Length; j++)
                {
                    if (SelectEventClassInfo[i].EventRewardStat != null)
                    {
                        PossibleEventList.transform.GetChild(1).GetChild(j).GetChild(0).GetComponent<TextMeshProUGUI>().text = SelectEventClassInfo[i].EventRewardStatName[j];
                    }
                    else
                    {
                        PossibleEventList.transform.GetChild(1).GetChild(j).gameObject.SetActive(false);
                    }
                }


                PossibleChooseEventClassList.Add(SelectEventClassInfo[i]);
            }
            else
            {
                break;
            }
        }
    }

    // ����, ���� �̺�Ʈ�� ù��° ���� ���� ���� �־����Ƿ� ������ �ٷ� �޷�â�� Scroll View�� �ֱ�
    // ������ Possible Scrollview �� �ֱ�
    public void SetSelectedEventOnCalender()
    {
        for (int i = 0; i < PossibleChooseEventClassList.Count; i++)
        {

        }
    }

    // ������Ʈ�� �ٽ� ������Ʈ Ǯ�� �ǵ��� ����ϴµ� ������ ť�� ��ť �ؾ���
    public void ReturnFixedEventToPool(string month)
    {
        int _DestroyMailObj = m_ParentCalenderScroll.childCount;        // 

        for (int i = 0; i < _DestroyMailObj; i++)
        {
            MailObjectPool.ReturnFixedEventObject(m_ParentCalenderScroll.transform.GetChild(i).gameObject);
        }
    }


    //  public void 
}
