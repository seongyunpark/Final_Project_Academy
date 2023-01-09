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

    public SaveEventClassData tempEventList;

    public List<SaveEventClassData> MyEventList = new List<SaveEventClassData>();

    public string[,] CalenderArr = new string[20, 2];

    public GameObject CalenderButton;

    public const int PossibleSetCount = 2;        // 이벤트 지정 가능 횟수
    public int nowPossibleCount = 2;

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
    }

    // Update is called once per frame
    void Update()
    {
    }

    GameObject _PrevClick = null;           // 이전클릭

    GameObject Event_One = null;
    GameObject Event_Two = null;
    public void ClickCalenderButton()
    {
        GameObject _EventDataObj = GameObject.Find("CalenderDate_Panel");
        GameObject _NowEvent = EventSystem.current.currentSelectedGameObject;
        GameObject _NowClick = _NowEvent;       // 현재클릭

        string[] clickdate = _NowEvent.name.Split("_");

        tempEventList.EventDay[0] = GameTime.Instance.FlowTime.NowYear.ToString();
        tempEventList.EventDay[1] = GameTime.Instance.FlowTime.NowMonth;
        // 주 구분
        if (clickdate[0] == "1")
        {
            tempEventList.EventDay[2] = GameTime.Instance.Week[0];
        }
        else if (clickdate[0] == "2")
        {
            tempEventList.EventDay[2] = GameTime.Instance.Week[1];
        }
        else if (clickdate[0] == "3")
        {
            tempEventList.EventDay[2] = GameTime.Instance.Week[2];
        }
        else if (clickdate[0] == "4")
        {
            tempEventList.EventDay[2] = GameTime.Instance.Week[3];
        }

        // 요일 구분
        if (clickdate[1] == "Monday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[0];
        }
        else if (clickdate[1] == "Tuesday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[1];
        }
        else if (clickdate[1] == "Wednsday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[2];
        }
        else if (clickdate[1] == "Thursday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[3];
        }
        else if (clickdate[1] == "Friday")
        {
            tempEventList.EventDay[3] = GameTime.Instance.Day[4];
        }

        // 날짜가 선택됬는지 안됬는지 체크
        _NowClick.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tempEventList.EventClassName;

        if (Event_One != null)
        {
            if(_PrevClick != Event_One)
            {
                _PrevClick.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

                _PrevClick = _NowClick;
            }
            else
            {
                _PrevClick = _NowClick;
            }
        }
        else if (Event_One == null)
        {
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
        }

        if (nowPossibleCount == 0)
        {

        }

        Debug.Log(tempEventList.EventDay[0]);
        Debug.Log(tempEventList.EventDay[1]);
        Debug.Log(tempEventList.EventDay[2]);
        Debug.Log(tempEventList.EventDay[3]);

    }

    public void ResetCalenderText()
    {
        _PrevClick.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
    }

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

        if (nowPossibleCount == 0)
        {
            Debug.Log("이제 선택 못함");
            ShowSelectedEventSettingOKButton();
        }
    }

    // 선택된 일정칸의 버튼을 관리 해 줄 함수
    public void SaveEventOnCalender()
    {
        if (Event_One == null)
        {
            Event_One = _PrevClick;

            //Event_One.transform.
            Event_One.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = MyEventList[0].EventClassName;
            Event_One.transform.GetComponent<Button>().interactable = false;
        }
        else if (Event_One != null)
        {
            Event_Two = _PrevClick;

            Event_Two.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = MyEventList[1].EventClassName;
            Event_Two.transform.GetComponent<Button>().interactable = false;
        }
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

    }
}
