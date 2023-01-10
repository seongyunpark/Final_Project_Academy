using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// 2023. 01. 08 Mang
/// 
/// 이벤트 선택 창이 다 끝난 뒤 그 데이터를 저장 할 클래스
/// 여기서는 데이터 세이브, 로드만 다루는 걸로
/// </summary>
public class EventSchedule : MonoBehaviour
{
    private static EventSchedule _instance = null;

    public GameObject CalenderObj;                                          // 달력 버튼리스트에 정보 넣기 위한 달력 부모  오브젝트

    // 이 버튼의 존재 의의 -> 고정 이벤트의 정보를 먼저 담아놓기 위한 버튼 
    public List<GameObject> CalenderButton = new List<GameObject>();        // 달력 오브젝트 넣은 달력버튼 리스트

    public SaveEventClassData tempEventList;                                // 내가 선택한 날짜를 받기 위한 임시 변수

    public List<SaveEventClassData> MyEventList = new List<SaveEventClassData>();       // 현재 나의 이벤트 목록



    public const int PossibleSetCount = 2;      // 최대 이벤트 지정 가능 횟수
    public int nowPossibleCount = 2;            // 현재 이벤트가능횟수

    public static EventSchedule Instance
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
        GiveCalenderInfoToButton();
    }

    int monthArrow = 0;
    // Update is called once per frame
    void Update()
    {
    }

    // 달력(일정) 버튼 받아서 달아주기
    public void GiveCalenderInfoToButton()
    {
        for (int i = 0; i < CalenderObj.transform.childCount; i++)
        {
            CalenderButton.Add(CalenderObj.transform.GetChild(i).gameObject);
        }

    }

    GameObject _PrevClick = null;           // 이전클릭
    int index;      // 현재 눌린 달력오브젝트를 알기위한 변수
    //(선택 이벤트)달력 버튼이 눌림 -> 달력 칸에 이벤트 띄우기
    public void ClickCalenderButton()
    {
        GameObject _NowEvent = EventSystem.current.currentSelectedGameObject;
        GameObject _NowClick = _NowEvent;       // 현재클릭
        string[] clickdate;

        //string[] clickdate = _NowEvent.name.Split("_");

        //선택한 이벤트의 현재 년 / 월
        tempEventList.EventDay[0] = GameTime.Instance.FlowTime.NowYear.ToString();
        tempEventList.EventDay[1] = GameTime.Instance.FlowTime.NowMonth;

        // 클릭한 버튼 - 버튼 이름 비교 . 일치 시 버튼에 현재 이벤트의 이름 띄우기
        for (int i = 0; i < CalenderObj.transform.childCount; i++)
        {
            if (_NowClick.name == CalenderButton[i].name)
            {
                _NowClick.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tempEventList.EventClassName;
                index = i;
                clickdate = CalenderButton[i].name.Split("_");     // "_" 특정 문자열로 이름 나눠주기

                GetSelectedTime(clickdate);     // 선택한 달력의 날짜 받기

                break;
            }
        }

        // 날짜 하나만 선택되도록 체크(잔상 남지 않도록)
        if (_PrevClick != _NowClick)
        {
            if (_PrevClick != null)
            {
                _PrevClick.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
            _PrevClick = _NowEvent;
        }
        else
        {
            _PrevClick = _NowEvent;
        }

        Debug.Log(tempEventList.EventDay[0]);
        Debug.Log(tempEventList.EventDay[1]);
        Debug.Log(tempEventList.EventDay[2]);
        Debug.Log(tempEventList.EventDay[3]);


        // 
        // if (nowPossibleCount == 0)
        // {
        // 
        // }
    }

    // 현재 선택 가능한 이벤트 갯수 세는 함수
    public void CountPossibleEventSetting()
    {
        _PrevClick.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

        GameObject _nowPossibleCountImg = GameObject.Find("PossibleSelectCountInfo");

        MyEventList.Add(tempEventList);

        if (nowPossibleCount == 2)
        {
            _nowPossibleCountImg.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (nowPossibleCount == 1)
        {
            _nowPossibleCountImg.transform.GetChild(0).gameObject.SetActive(false);
        }

        nowPossibleCount -= 1;

        SaveEventOnCalender();

        if (nowPossibleCount == 0)
        {
            Debug.Log("이제 선택 못함");
            ShowSelectedEventSettingOKButton();
        }
    }

    // 선택된 일정칸 버튼의 데이터를 해당 이벤트의 날짜에 넣어주기
    public void SaveEventOnCalender()
    {
        for (int i = 0; i < MyEventList.Count; i++)
        {
            if(MyEventList[i].EventKeyward == "Select" && tempEventList.EventClassName == MyEventList[i].EventClassName)
            {
                string tempName = MyEventList[i].EventClassName;
                CalenderButton[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tempName;
                CalenderButton[index].transform.GetComponent<Button>().interactable = false;
            }
        }
        // if (Event_One == null)
        // {
        //     Event_One = _PrevClick;
        //
        //     //Event_One.transform.
        //     Event_One.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = MyEventList[0].EventClassName;
        //     Event_One.transform.GetComponent<Button>().interactable = false;
        // }
        // else if (Event_One != null)
        // {
        //     Event_Two = _PrevClick;
        //
        //     Event_Two.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = MyEventList[1].EventClassName;
        //     Event_Two.transform.GetComponent<Button>().interactable = false;
        // }
    }

    public GameObject RewardInfoScreen;
    public GameObject MonthOfRewardInfoScreen;

    public void ShowSelectedEventSettingOKButton()
    {
        RewardInfoScreen.SetActive(false);
        MonthOfRewardInfoScreen.SetActive(true);
    }

    public void IfIWantCancleEvent()
    {
        // if()
    }

    public void GetSelectedTime(string[] click)
    {
        //주 구분
        if (click[0] == "1")
        {
            tempEventList.EventDay[2] = GameTime.Instance.Week[0];
        }
        else if (click[0] == "2")
        {
            tempEventList.EventDay[2] = GameTime.Instance.Week[1];
        }
        else if (click[0] == "3")
        {
            tempEventList.EventDay[2] = GameTime.Instance.Week[2];
        }
        else if (click[0] == "4")
        {
            tempEventList.EventDay[2] = GameTime.Instance.Week[3];
        }
        // 요일 구분
        if (click[1] == "Monday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[0];
        }
        else if (click[1] == "Tuesday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[1];
        }
        else if (click[1] == "Wednsday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[2];
        }
        else if (click[1] == "Thursday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[3];
        }
        else if (click[1] == "Friday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[4];
        }
    }
}
