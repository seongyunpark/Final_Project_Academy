using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    private static GameTime instance = null;

    public static GameTime Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }

        set { instance = value; }
    }

    public TextMeshProUGUI m_nowTime;


    public float LimitTime = 30.0f;
    float PrevTime = 0.0f;

    int Year = 1;
    // string[] Month = new string[12];
    // string[] Week = new string[4];

    string[] Month = { "1��", "2��", "3��", "4��", "5��", "6��", "7��", "8��", "9��", "10��", "11��", "12��" };
    string[] Week = { "ù° ��", "��° ��", "��° ��", "��° ��" };


    int MonthIndex = 2;
    int WeekIndex = 0;

    public bool IsGameMode = false;        // ���ΰ���ȭ�� or UI â ȭ�� üũ�ؼ� �� ��� ���� ������ �͵��� �ϱ� ���� ����

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        IsGameMode = false;
        Debug.Log(IsGameMode);

        // Month[11] = "12��";
        // Week[0] = "ù°��";

        m_nowTime.text = Year + "�� " + Month[MonthIndex] + " " + Week[WeekIndex];

        Debug.Log(Year + "�� " + Month[MonthIndex] + " " + Week[WeekIndex]);
    }

    // Update is called once per frame
    public void Update()
    {
        if (IsGameMode == true)
        {
            ShowGameTime();

            FlowtheTime();
        }

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Time.timeScale = 0;
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Time.timeScale = 1;
        //}

    }

    public void FlowtheTime()
    {
        if (PrevTime == 0.0f)
        {
            PrevTime = Time.time;
        }

        if (LimitTime < (Time.time - PrevTime))
        {
            Debug.Log(Time.time);

            ChangeWeek();

            m_nowTime.text = Year + "�� " + Month[MonthIndex] + " " + Week[WeekIndex];

            Debug.Log(Year + "�� " + Month[MonthIndex] + " " + Week[WeekIndex]);

            // 3���̶�� ���� �ð��� ������ �� ��
            if (Year == 3 && MonthIndex == 11 && WeekIndex == 3)
            {
                IsLimitedGameTimeEnd();
            }

            ChangeYear();
            ChangeMonth();

            PrevTime = 0.0f;
        }

    }

    // �� �� ����
    public void ChangeWeek()
    {
        // Week ����
        if (WeekIndex != 3)
        {
            WeekIndex++;
        }
        else if (WeekIndex == 3)
        {
            WeekIndex = 0;
        }
    }

    // �� ����
    public void ChangeMonth()
    {
        // ���� ���� �� �� �ʱ�ȭ
        if (MonthIndex == 11 && WeekIndex == 3)
        {
            MonthIndex = 0;
        }
        // �� ����
        else if (MonthIndex != 11 && WeekIndex == 3)
        {
            MonthIndex++;
        }
    }

    public void ChangeYear()
    {
        // Year ����
        if (MonthIndex == 11 && WeekIndex == 3)
        {
            Year++;
        }
    }

    public void IsLimitedGameTimeEnd()
    {
        Debug.Log("���� ���丮 ��~");

    }

    public void ShowGameTime()
    {
        //Debug.Log("�ð� ���� : " + IsGameMode);

    }

    public void SendGameMode(bool b)
    {
        b = IsGameMode;
    }
}
