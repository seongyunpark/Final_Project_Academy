using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public struct NowTime
{
    public int NowYear;
    public int NowMonth;
    public int NowWeek;
    public int NowDay;
}

public class TimeDefine
{
    // const int 
}

/// <summary>
///  2023. 01. 30 Mang
/// 
/// �ð� : string -> int �� ����. UI �� ���̱⸸ �ϵ���
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

    private int Year = 1;           // 1 ~ 3��(���Ӹ��) - ����(���Ѹ��)
    private int Month = 3;          // 1 ~ 12��(12)
    private int Week = 1;           // 1 ~ 4��(4)
    private int Day = 1;            // �� ~ ��(5)

    Image TimeBarImg;

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
        Debug.Log(Day);

        IsGameMode = false;
        Debug.Log(IsGameMode);

        // Month[11] = "12��";
        // Week[0] = "ù°��";
        m_DrawnowTime.text = Year + "�� " + Month + "�� " + Week + "��";

        FlowTime.NowYear = Year;
        FlowTime.NowMonth = Month;
        FlowTime.NowWeek = Week;
        FlowTime.NowDay = Day;

        Debug.Log(Year + "��" + " " + Month + " " + Week);

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
        m_DrawnowTime.text = Year + "�� " + Month + "�� " + Week + "��";

        CheckPerSecond();

        // 30�� �Ѿ�� �� �� ��ȭ
        if (isChangeWeek)     // 
        {
            ChangeMonth();

            ChangeWeek();

            Debug.Log("Time.time : " + Time.time);

            // nowTime = Year + "�� " + Month[MonthIndex] + " " + Week[WeekIndex];
            if (Week != 4)
            {
                m_DrawnowTime.text = Year + "�� " + Month + "�� " + Week + "��";

                FlowTime.NowWeek = Week;
            }

            FlowTime.NowYear = Year;
            FlowTime.NowMonth = Month;

            // 3���̶�� ���� �ð��� ������ �� ��
            if (Year == 3 && Month == 12 && Week == 4)
            {
                IsLimitedGameTimeEnd();
            }

            PrevTime = 0.0f;
            isChangeWeek = false;
        }

        if (PrevTime == 0.0f)
        {
            PrevTime = Time.time;
        }
    }

    public void ChangeDay()
    {
        if (Day < 5)
        {
            Day++;

            Debug.Log("���� : " + Day);
        }
        else if (Day >= 5)
        {
            Day = 1;
        }
    }

    // �� ����
    public void ChangeWeek()
    {
        // Week ����
        if (Week < 4)
        {
            Week++;

            Debug.Log("�� : " + Week);
        }
        else if (Week >= 4)
        {
            Week = 1;
        }
    }

    // ��, �� ����
    public void ChangeMonth()
    {
        // ���� ���� �� �� �ʱ�ȭ
        if (Month >= 12 && Week >= 4)
        {
            Month = 1;

            Year++;
        }
        // �� ����
        else if (Month < 12 && Week >= 4)
        {
            Month++;
            Debug.Log("�� : " + Month);
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
            if (Week == 1 || Week == 2 || Week == 3 || Week == 4)
            {
                // (1 ~ 2 ����)1�ʸ��� �ð�üũ
                if (Time.time - PrevTime >= FirstHalfPerSecond)
                {
                    TimeBarImg.fillAmount += 0.2f;

                    // 1�ʸ��� �����ֱ�
                    ChangeDay();
                    FlowTime.NowDay = Day;

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
                    Debug.Log("���� : " + Week);
                }
            }
            //else if (Week == 3 || Week == 4)
            //{
            //    // (3 ~ 4 ����)6�ʸ��� �ð�üũ
            //    if (Time.time - PrevTime >= SecondHalfPerSecond)
            //    {
            //        TimeBarImg.fillAmount += 0.2f;
            //
            //        i += 1;
            //        // 6�ʸ��� �����ֱ�
            //        ChangeDay();
            //        FlowTime.NowDay = Day[DayIndex];
            //
            //        SecondHalfPerSecond += 6;
            //
            //        if (SecondHalfPerSecond > LimitTime2)
            //        {
            //            TimeBarImg.fillAmount = 0.2f;
            //
            //            SecondHalfPerSecond = 6;
            //
            //            i = 0;
            //
            //            Debug.Log(SecondHalfPerSecond + "�� üũ �ʱ�ȭ");
            //
            //            isChangeWeek = true;
            //        }
            //
            //        Debug.Log(SecondHalfPerSecond);
            //        Debug.Log(TimeBarImg.fillAmount);
            //        Debug.Log(Day[i]);
            //    }
            //}
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