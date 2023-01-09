using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
// using StatData.Runtime;


public class SaveEventClassData
{
    public string[] EventDay = new string[4];               // 이벤트가 실행될 날짜 배열( 년, 월, 주, 요일)
    public string EventKeyward;                             // 고정인지 선택인지 구별할 스트링
    public string EventClassName;                           // 이벤트 이름
    public string EventInformation;                         // 이벤트 설명 내용

    public const int RewardStatCount = 6;                   // 보상스탯의 갯수는 6개

    public string[] EventRewardStatName = new string[RewardStatCount];    // 보상 - 스탯이름
    public float[] EventRewardStat = new float[RewardStatCount];          // 보상 - 스탯수치
    public int EventRewardMoney;                            // 보상 - 머니

    // 이벤트 가능한 날짜도 필요하려나? 이건 이벤트스케줄 스크립트에서 건드리자

}

// 보상이 스탯인 경우 : 마지막에 ObjectManager.Instance.m_StudentList 의 스탯을 변경 해주면 된다
// 보상이 재화인 경우 : PlayerInfo.Instance.m_MyMoney 값을 마지막으로 바꿔주면 된다
/// <summary>
/// Mang 2023. 01. 05
/// 
/// 이 클래스는 이벤트가 담길 프리팹을 생성해주는 클래스
/// 
/// 일시적으로 보일 이벤트프리팹이므로 여기서 에빈트 관련 업무를 다 보도록 하자
/// 
/// 
/// </summary>

public class EventClassPrefab : MonoBehaviour
{
    // Inspector 창에 연결해줄 변수들
    public GameObject m_prefab;
    public Transform m_Parent;
    public GameObject m_SelectedEventInfo;

    public GameObject RewardInfoBG;

    // Json 파일을 파싱해서 그 데이터들을 다 담아 줄 리스트 변수
    // 이 변수들도 EventSchedule 의 Instance.변수 들에 넣어주고 쓰도록 하자
    public List<SaveEventClassData> SelectEventClassInfo = new List<SaveEventClassData>();      // 전체 선택 이벤트
    public List<SaveEventClassData> FixedEventClassInfo = new List<SaveEventClassData>();       // 전체 고정 이벤트
    public List<SaveEventClassData> PossibleChooseEventClassList = new List<SaveEventClassData>();      //  사용가능한 이벤트 목록

    public SaveEventClassData IfIChoosedEvent;           // 현재 선택한 이벤트 담아 줄 임시 변수

    // Start is called before the first frame update
    void Start()
    {
        // 1. 제이슨 파일 전체 이벤트리스트 변수에 담기

        // 고정이벤트
        for (int i = 0; i < 3; i++)
        {

            SaveEventClassData TempFixedData = new SaveEventClassData();
            TempFixedData.EventClassName = "Fixed" + i;
            TempFixedData.EventDay[0] = "1";
            TempFixedData.EventDay[1] = "1월";
            TempFixedData.EventDay[2] = "셋째 주";
            TempFixedData.EventDay[3] = "화요일";


        }

        // 선택이벤트
        // (임시로 여기서)2. 전체 이벤트 리스트에서 사용가능한 이벤트들 사용가능이벤트 리스트 변수에 담기
        for (int i = 0; i < 5; i++)
        {
            // 테스트
            // 제이슨 파일 생성 전 테스트를 위해 만든 변수
            SaveEventClassData TempEventData = new SaveEventClassData();

            // 이벤트 struct 관련 초기화 해주기
            TempEventData.EventClassName = "test" + i;
            // if (TempEventData.EventReward.Length > 1)
            // {
            //     for (int j = 0; j < TempEventData.EventReward.Length; j++)
            //     {
            //     }
            // }
            TempEventData.EventRewardStatName[0] = "StatName0";
            TempEventData.EventRewardStat[0] = 5 + (i * 3);

            TempEventData.EventRewardStatName[1] = "StatName1";
            TempEventData.EventRewardStat[1] = 30 + (i * 7);

            TempEventData.EventRewardStatName[2] = "StatName2";
            TempEventData.EventRewardStat[2] = 100 + (i * 2);

            TempEventData.EventRewardMoney = 5247 + (i * 4);

            PossibleChooseEventClassList.Add(TempEventData);
        }
        // 리스트에 제이슨 파싱 할 때 : .Add() -> 한번에 다 파싱하는게 아니라 1. 제이슨파일 하나 읽어서 임시변수에 담고 리스트에 임시변수 넣기
        // 그다임 하나 읽고 임시변수 담고 리스트 넣기 반복! 아하...
        // 

        // ObjectManager.Instance.m_StudentList[i].m_StudentData.m_StudentContentsValue += 3;       이렇게 추가

        // for (int i = 0; i < SelectEventClassInfo.Count; i++)
        // {
        //     PossibleChooseEventClassList.
        // }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 버튼 눌렸을 때 이벤트버튼 목록들을 동적으로 생성해 줄 함수
    public void MakeEventClass()
    {
        RewardInfoBG.SetActive(true);

        // 2. 전체 이벤트 리스트에서 사용가능한 이벤트들 사용가능이벤트 리스트 변수에 담기

        // 현재 선택된 게임 오브젝트를 알아오는 건가
        // GameObject _nowObj = EventSystem.current.currentSelectedGameObject;

        // EventBox 프리팹을 여러개 만들기 위해 도는 반복문
        foreach (var possibleEvent in PossibleChooseEventClassList)
        {
            // 동적으로 생성해 줄 이벤트 선택 스크롤뷰의 이벤트리스트
            GameObject EventList = GameObject.Instantiate(m_prefab, m_Parent);

            EventList.name = possibleEvent.EventClassName;
            EventList.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventClassName;

            int BoxArrow = 0;       // 보상칸을 가르킬 임시변수

            if (possibleEvent.EventRewardMoney != 0)
            {
                EventList.transform.GetChild(1).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventRewardMoney.ToString();

                BoxArrow += 1;
            }

            // 보상 칸을 채우기 위해 데이터가 있는 지 확인 할 반복문 (스탯 , 머니)
            for (int j = 0; j < SaveEventClassData.RewardStatCount; j++)
            {
                // 스탯 보상이 없을땐 넘어간다
                if (possibleEvent.EventRewardStat[j] != 0)
                {
                    //
                    EventList.transform.GetChild(1).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventRewardStatName[j].ToString();

                    BoxArrow += 1;
                }
            }


            for (int x = 0; x < EventList.transform.GetChild(1).childCount; x++)
            {
                if (EventList.transform.GetChild(1).GetChild(x).GetChild(0).GetComponent<TextMeshProUGUI>().text == "보상")
                {
                    EventList.transform.GetChild(1).GetChild(x).gameObject.SetActive(false);
                }
            }
            EventList.GetComponent<Button>().onClick.AddListener(ShowSelectedEventInfo);
        }
    }

    // 이벤트 리스트 들을 클릭했을 때 나올 클릭한 이벤트의 설명을 띄워주기 위한 함수
    public void ShowSelectedEventInfo()
    {
        GameObject _EventDataObj = GameObject.Find("Reward_Image");
        GameObject _NowEvent = EventSystem.current.currentSelectedGameObject;

        // 해당 요소 in 해당리스트 자체
        foreach (var possibleEvent in PossibleChooseEventClassList)
        {
            if (_NowEvent.name == possibleEvent.EventClassName)
            {
                // 이벤트 이름 , 설명
                _EventDataObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventClassName;      // 이벤트 이름
                _EventDataObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventInformation;      // 이벤트 설명

                int BoxArrow = 0;       // 보상칸을 가르킬 임시변수

                // 머니
                if (possibleEvent.EventRewardMoney != 0)
                {
                    _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).gameObject.SetActive(true);

                    _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = "머니";      // 
                    _EventDataObj.transform.GetChild(2).GetChild(BoxArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = possibleEvent.EventRewardMoney.ToString();

                    BoxArrow += 1;
                }

                // 보상 칸을 채우기 위해 데이터가 있는 지 확인 할 반복문 (스탯)
                for (int i = 0; i < SaveEventClassData.RewardStatCount; i++)
                {
                    // 스탯 보상이 없을땐 넘어간다
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

                // 여기서 임시로 내가 선택한 데이터를 담아둔다
                IfIChoosedEvent = possibleEvent;
                EventSchedule.Instance.tempEventList = possibleEvent;

                Debug.Log(IfIChoosedEvent.EventClassName);

            }
        }

        // 이벤트 설명 창을 깔끔하게 안보이게 하다가 내용을 보이게 하기 위해 어거지로 만든 배경
        if (RewardInfoBG.activeSelf == true)
        {
            RewardInfoBG.SetActive(false);
        }
    }

    // 이벤트 선택 후 달력창에서 보일 선택한 이벤트 설명 창
    public void SelectedEventScreen()
    {
        GameObject _EventDataObj = GameObject.Find("CalenderRewardInfo");

        // 현재 월
        _EventDataObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameTime.Instance.FlowTime.NowMonth;

        // 이벤트 이름
        _EventDataObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = IfIChoosedEvent.EventClassName;
        // 이벤트 설명
        _EventDataObj.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = IfIChoosedEvent.EventInformation;

        // 이벤트 보상
        int BoxArrow = 0;
        // 머니
        if (IfIChoosedEvent.EventRewardMoney != 0)
        {
            _EventDataObj.transform.GetChild(4).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = "머니";
            _EventDataObj.transform.GetChild(4).GetChild(BoxArrow).GetChild(1).GetComponent<TextMeshProUGUI>().text = IfIChoosedEvent.EventRewardMoney.ToString();
            BoxArrow += 1;
        }

        // 스탯
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
}
