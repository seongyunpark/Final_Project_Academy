using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 2023. 01. 10 Mang
/// 
/// �̺�Ʈ, ���� �� ���� �ð��� �߻��� ��� �̺�Ʈ�� �ð� üũ �� �߻� ���� �� ��ũ��Ʈ
/// </summary>
public class ClockScheduler : MonoBehaviour
{
    [SerializeField] PopUpUI _popUpClassPanel;

    public GameObject EventStartButton;
    public GameObject EventPanel;


    // ���� �帣�� �ð��� ���ؼ� ��ȭ�� ���� �� �̺�Ʈ/ ���� ���� �߻� �����ֱ� ���� ���� �ð� ���� ����
    int nowYear;
    string nowMonth;
    string nowWeek;
    string nowDate;
    string checkMonth;       // ���� Date�� Week�� �ٲ�� �ð��� �޶� Date�� �ٲ����� Week�� �ٲ����ʾ� �߻��ϴ� ������ ��� ���� ���� ����

    EventClassPrefab EventPrefab;

    // Start is called before the first frame update
    void Start()
    {
        nowYear = GameTime.Instance.FlowTime.NowYear;
        nowMonth = GameTime.Instance.FlowTime.NowMonth;
        nowWeek = GameTime.Instance.FlowTime.NowWeek;
        nowDate = GameTime.Instance.FlowTime.NowDay;

        //EventPrefab = GameObject.Find("EventClassPrefab").GetComponent<EventClassPrefab>();
    }

    // Update is called once per frame
    void Update()
    {
        checkMonth = nowMonth;
        // �ð� ������ ��� ���� ��������
        ChangeTime();
        // ����Ʈ�� ���� ����� �Ѵٸ� ���� �Ʒ�����

        // �� �ٲ���� ��
        if (nowDate == "������" && nowWeek == "ù° ��" && checkMonth != GameTime.Instance.FlowTime.NowMonth)
        {
            Debug.Log("���� �� �� �̺�Ʈ����Ʈ �� ����");

            //EventSchedule.Instance.MyEventList.Clear();

            // ���� ���� ������ �Ϸ��ϰ� �� �����ް� �� �����޿��� ������ ������Ѿ��Ѵ�. 
            if (InGameTest.Instance._isRepeatClass == true)
            {
                InGameTest.Instance.NextClassStart();
                InGameTest.Instance._classCount++;
            }
            else if (InGameTest.Instance._isRepeatClass == false && checkMonth != null)
            {
                _popUpClassPanel.TurnOnUI();
            }

            checkMonth = nowMonth;
        }


        // PopUpEventStartButton();
        AutoEventPopUp();
    }

    public void ChangeTime()
    {
        if (nowYear != GameTime.Instance.FlowTime.NowYear)
        {
            nowYear = GameTime.Instance.FlowTime.NowYear;
        }

        if (nowMonth != GameTime.Instance.FlowTime.NowMonth)
        {
            nowMonth = GameTime.Instance.FlowTime.NowMonth;
        }

        if (nowWeek != GameTime.Instance.FlowTime.NowWeek)
        {
            nowWeek = GameTime.Instance.FlowTime.NowWeek;
        }

        if (nowDate != GameTime.Instance.FlowTime.NowDay)
        {
            nowDate = GameTime.Instance.FlowTime.NowDay;
        }
    }

    // ������ �̺�Ʈ â�� �ڵ����� �߰� �ϱ�
    public void AutoEventPopUp()
    {
        //foreach (var thisMonthEvent in SwitchEventList.Instance.MyEventList)
        for(int i = 0; i < SwitchEventList.Instance.MyEventList.Count; i++)
        {
            if (SwitchEventList.Instance.MyEventList[i].EventDay[1] == GameTime.Instance.FlowTime.NowMonth)
            {
                if (SwitchEventList.Instance.MyEventList[i].EventDay[2] == GameTime.Instance.FlowTime.NowWeek)
                {
                    if (SwitchEventList.Instance.MyEventList[i].EventDay[3] ==  GameTime.Instance.FlowTime.NowDay)
                    {
                        // 

                        EventPanel.SetActive(true);

                        if (nowDate == "������")
                        {
                            Debug.Log("�������Դϴ�");
                        }
                    }
                }
            }
        }

        // if (nowDate == "������")
        // {
        //     Debug.Log("�������Դϴ�");
        // }
    }

    public void PopUpEventStartButton()
    {
        if (nowMonth == "3��" || nowMonth == "6��" || nowMonth == "9��" || nowMonth == "12��")
        {
            EventStartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nowMonth;
        }
        else
        {
            EventStartButton.SetActive(true);
            EventStartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nowMonth;
        }
    }
}
