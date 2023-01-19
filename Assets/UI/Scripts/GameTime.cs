using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public enum eMonth
{
    January = 0,
    February,
    March, 
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December
}

public enum eWeek
{
    Week1 = 0,
    Week2,
    Week3,
    Week4
}

public enum eDate
{
    Monday = 0,
    Tuesday,
    Wednsday,
    Thursday,
    Friday
}

public struct NowTime
{
    public int NowYear;
    public string NowMonth;
    public string NowWeek;
    public string NowDay;
}

/// <summary>
///  
/// 
/// �ð� -> enum Ŭ���� �� �ٲ㼭 UI ������ ���϶��� �ѱ۷� ���������� ����
/// </summary>
public class GameTime : MonoBehaviour
{
    private static GameTime instance = null;

    public static GameTime Instance
    {
        get
        {
            return instance;
        }

        set { instance = value; }
    }

    public TextMeshProUGUI m_DrawnowTime;
    public TextMeshProUGUI m_TimeText;


    public NowTime FlowTime;

    const float LimitTime1 = 5.0f;      // 1 ~ 2�� ���ѽð�
    const float LimitTime2 = 30.0f;     // 3 ~ 4�� ���ѽð�
    float PrevTime = 0;

    int Year = 1;
    // string[] Month = new string[12];
    // string[] Week = new string[4];

    public string[] Month = { "1��", "2��", "3��", "4��", "5��", "6��", "7��", "8��", "9��", "10��", "11��", "12��" };
    public string[] Week = { "ù° ��", "��° ��", "��° ��", "��° ��" };


    public string[] Day = { "������", "ȭ����", "������", "�����", "�ݿ���" };

    Image TimeBarImg;

    public int MonthIndex = 2;
    public int WeekIndex = 0;
    public int DayIndex = 0;

    int FirstHalfPerSecond = 1;       //  (1�� - 2��) �Ϸ��� �ð� 1��(�� �� �� 5��)
    int SecondHalfPerSecond = 6;      // (3�� - 4��)�Ϸ��� �ð��� 6��                                       // 

    public bool IsGameMode = false;                 // ���ΰ���ȭ�� or UI â ȭ�� üũ�ؼ� �� ��� ���� ������ �͵��� �ϱ� ���� ����

    public bool IsMonthCycleCompleted = false;      // �� - �� ����Ŭ ���Ҵ��� üũ�� ����
    public bool IsOneSemesterCompleted = false;     // 3����(�� �б�) ����Ŭ ���Ҵ��� üũ
    public bool IsYearCycleCompleted = false;       // �� - �� ����Ŭ ���Ҵ��� üũ�� ����
    public bool IsGameEnd = false;                  // ������ ������ �� üũ�� ����

    public bool isChangeWeek = false;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


    }

    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log(SecondHalfPerSecond);
        // Debug.Log(TimeBarImg.fillAmount);
        Debug.Log(Day[i]);

        IsGameMode = false;
        Debug.Log(IsGameMode);

        // Month[11] = "12��";
        // Week[0] = "ù°��";
        m_DrawnowTime.text = Year + "�� " + Month[MonthIndex] + " " + Week[WeekIndex];

        FlowTime.NowYear = Year;
        FlowTime.NowMonth = Month[MonthIndex];
        FlowTime.NowWeek = Week[WeekIndex];
        FlowTime.NowDay = Day[DayIndex];

        Debug.Log(Year + "��" + " " + Month[MonthIndex] + " " + Week[WeekIndex]);

        ShowGameTime();
    }

    bool call = false;
    // Update is called once per frame
    public void Update()
    {
        if (IsGameMode == true)
        {
            FlowtheTime();

            ShowGameTime();
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
        // 30�� �Ѿ�� �� �� ��ȭ
        if (isChangeWeek)     // 
        {
            ChangeWeek();

            Debug.Log("Time.time : " + Time.time);

            // nowTime = Year + "�� " + Month[MonthIndex] + " " + Week[WeekIndex];
            m_DrawnowTime.text = Year + "�� " + Month[MonthIndex] + " " + Week[WeekIndex];

            FlowTime.NowYear = Year;
            FlowTime.NowMonth = Month[MonthIndex];
            FlowTime.NowWeek = Week[WeekIndex];

            // m_DrawnowTime.text = nowTime;


            // 3���̶�� ���� �ð��� ������ �� ��
            if (Year == 3 && MonthIndex == 11 && WeekIndex == 3)
            {
                IsLimitedGameTimeEnd();
            }

            ChangeYear();
            ChangeMonth();

            PrevTime = 0.0f;
            isChangeWeek = false;
        }

        if (PrevTime == 0.0f)
        {
            PrevTime = Time.time;
        }

        CheckPerSecond();
    }

    public void ChangeDay()
    {
        if (DayIndex != 4)
        {
            DayIndex++;
        }
        else if (DayIndex == 4)
        {
            DayIndex = 0;
        }
    }

    // �� ����
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

    // �� ����
    public void ChangeYear()
    {
        // Year ����
        if (MonthIndex == 11 && WeekIndex == 3)
        {
            Year++;
        }
    }

    int i = 0;
    public void CheckPerSecond()
    {
        // ������ �������� �� ĳ���ʹ� ���������� �ð��� �帣���ʰ� �� �� PrevTime�� ���������� ������ ���� ���� �� �ð��� ������� �帥��.
        if (InGameTest.Instance.m_ClassState == ClassState.ClassStart)
        {
            PrevTime = Time.time;
        }

            // �������� �� �ݿ� ������ �� ������ �ð��� �帣�� �ȵȴ�. TimeScale�� ���߸� ĳ���Ͱ� �������� ������ �ٸ� �������,,
        if (GameTime.Instance.IsGameMode == true && InGameTest.Instance.m_ClassState != ClassState.ClassStart)
        {
            if (Week[WeekIndex] == "ù° ��" || Week[WeekIndex] == "��° ��")
            {
            // (1 ~ 2 ����)1�ʸ��� �ð�üũ
            if (Time.time - PrevTime >= FirstHalfPerSecond)
                {
                    TimeBarImg.fillAmount += 0.2f;

                    // 1�ʸ��� �����ֱ�
                    ChangeDay();
                    FlowTime.NowDay = Day[DayIndex];

                    i += 1;
                    FirstHalfPerSecond += 1;

                    if (FirstHalfPerSecond > LimitTime1)
                    {
                        TimeBarImg.fillAmount = 0.2f;

                        FirstHalfPerSecond = 1;

                        i = 0;

                        Debug.Log(FirstHalfPerSecond + "�� üũ �ʱ�ȭ");

                        isChangeWeek = true;
                    }
                    Debug.Log("�� : " + FirstHalfPerSecond);
                    Debug.Log("�̹��� : " + TimeBarImg.fillAmount);
                    Debug.Log("���� : " + Week[WeekIndex]);
                }

            }
            else if (Week[WeekIndex] == "��° ��" || Week[WeekIndex] == "��° ��")
            {
                // (3 ~ 4 ����)6�ʸ��� �ð�üũ
                if (Time.time - PrevTime >= SecondHalfPerSecond)
                {
                    TimeBarImg.fillAmount += 0.2f;

                    i += 1;
                    // 6�ʸ��� �����ֱ�
                    ChangeDay();
                    FlowTime.NowDay = Day[DayIndex];

                    SecondHalfPerSecond += 6;

                    if (SecondHalfPerSecond > LimitTime2)
                    {
                        TimeBarImg.fillAmount = 0.2f;

                        SecondHalfPerSecond = 6;

                        i = 0;

                        Debug.Log(SecondHalfPerSecond + "�� üũ �ʱ�ȭ");

                        isChangeWeek = true;
                    }

                    Debug.Log(SecondHalfPerSecond);
                    Debug.Log(TimeBarImg.fillAmount);
                    Debug.Log(Day[i]);
                }
            }
        }
    }

    //
    public void DrawTimeBar(Image img)
    {
        TimeBarImg = img;
    }

    public void IsLimitedGameTimeEnd()
    {
        if (IsGameEnd == true)
        {
            Debug.Log("���� ���丮 ��~");

        }
    }

    public void ShowGameTime()
    {
        m_TimeText.text = FlowTime.NowYear.ToString() + FlowTime.NowMonth + FlowTime.NowWeek + FlowTime.NowDay;
    }
}