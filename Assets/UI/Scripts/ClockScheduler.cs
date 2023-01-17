using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2023. 01. 10 Mang
/// 
/// �̺�Ʈ, ���� �� ���� �ð��� �߻��� ��� �̺�Ʈ�� �ð� üũ �� �߻� ���� �� ��ũ��Ʈ
/// </summary>
public class ClockScheduler : MonoBehaviour
{

    // ���� �帣�� �ð��� ���ؼ� ��ȭ�� ���� �� �̺�Ʈ/ ���� ���� �߻� �����ֱ� ���� ���� �ð� ���� ����
    int nowYear;
    string nowMonth;
    string nowWeek;
    string nowDate;

    SwitchEventList EventPrefab;

    // Start is called before the first frame update
    void Start()
    {
        nowYear = GameTime.Instance.FlowTime.NowYear;
        nowMonth = GameTime.Instance.FlowTime.NowMonth;
        nowWeek = GameTime.Instance.FlowTime.NowWeek;
        nowDate = GameTime.Instance.FlowTime.NowDay;


        // EventPrefab = GameObject.Find("EventClassPrefab").GetComponent<SwitchEventList>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTime();       // �ð� ������ ��� ���� ��������

        // ����Ʈ�� ���� ����� �Ѵٸ� ���� �Ʒ�����

        // �� �ٲ���� ��
        if (nowDate == "������" && nowWeek == "ù° ��" && nowMonth != GameTime.Instance.FlowTime.NowMonth)
        {
            Debug.Log("���� �� �� �̺�Ʈ����Ʈ �� ����");

            EventSchedule.Instance.MyEventList.Clear();
        }
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
}
