using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
// using StatData.Runtime;


public class SaveEventClassData
{
    public string EventKeyward;                 // 고정인지 선택인지 구별할 스트링
    public string EventClassName;               // 이벤트 이름
    public int[] EventRewardStat = new int[6];  // 보상 - 스탯
    // public List<int> EventRewardStat = new List<int>();
    public int EventRewardMoney;                // 보상 - 머니
    public string EventInformation;             // 이벤트 설명 내용

    // 이벤트 가능한 날짜도 필요하려나?

}

// 보상이 스탯인 경우 : 마지막에 ObjectManager.Instance.m_StudentList 의 스탯을 변경 해주면 된다
// 보상이 재화인 경우 : PlayerInfo.Instance.m_MyMoney 값을 마지막으로 바꿔주면 된다
/// <summary>
/// Mang 2023. 01. 05
/// 
/// 이 클래스는 이벤트가 담길 프리팹을 생성해주는 클래스
/// 일시적으로 보일 이벤트프리팹이므로 여기에 EventSchedule 클래스(싱글턴) 의 나의 이벤트 목록에 
/// 데이터들을 담아주도록 하자
/// </summary>

public class EventClassPrefab : MonoBehaviour
{
    int ArrDefaultSize = 5;
    // Inspector 창에 연결해줄 변수들
    public GameObject m_prefab;
    public Transform m_Parent;
    public GameObject m_SelectedEventInfo;



    // Json 파일을 파싱해서 그 데이터들을 다 담아 줄 리스트 변수
    // 이 변수들도 EventSchedule 의 Instance.변수 들에 넣어주고 쓰도록 하자
    public List<SaveEventClassData> SelectEventClassInfo = new List<SaveEventClassData>();      // 전체 선택 이벤트
    public List<SaveEventClassData> FixedEventClassInfo = new List<SaveEventClassData>();       // 전체 고정 이벤트
    public List<SaveEventClassData> PossibleChooseEventClassList = new List<SaveEventClassData>();      //  사용가능한 이벤트 목록

    // Start is called before the first frame update
    void Start()
    {
        // 1. 제이슨 파일 전체 이벤트리스트 변수에 담기

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
            TempEventData.EventRewardStat[0] = 5;
            TempEventData.EventRewardStat[1] = 30;
            TempEventData.EventRewardStat[2] = 100;
            TempEventData.EventRewardMoney = 1000;

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

    public void PutEventInPossibleList()
    {

    }


    // 버튼 눌렸을 때 이벤트버튼 목록들을 동적으로 생성해 줄 함수
    public void MakeEventClass()
    {
        // 2. 전체 이벤트 리스트에서 사용가능한 이벤트들 사용가능이벤트 리스트 변수에 담기

        // 현재 선택된 게임 오브젝트를 알아오는 건가
        // GameObject _nowObj = EventSystem.current.currentSelectedGameObject;

        // EventBox 프리팹을 여러개 만들기 위해 도는 반복문
        for (int i = 0; i < PossibleChooseEventClassList.Count; i++)
        {
            // 동적으로 생성해 줄 이벤트 선택 스크롤뷰의 이벤트리스트
            GameObject EventList = GameObject.Instantiate(m_prefab, m_Parent);

            // Debug.Log(PossibleChooseEventClassList[i].EventClassName);
            // Debug.Log(PossibleChooseEventClassList[i].EventReward[0]);



            EventList.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventClassName;

            int BoxArrow = 0;       // 보상칸을 가르킬 임시변수

            // 보상 칸을 채우기 위해 데이터가 있는 지 확인 할 반복문 (스탯 , 머니)
            for (int j = 0; j < PossibleChooseEventClassList[i].EventRewardStat.Length; j++)
            {
                // 스탯 보상이 없을땐 넘어간다
                if (PossibleChooseEventClassList[i].EventRewardStat[j] != 0)
                {
                    //
                    EventList.transform.GetChild(1).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventRewardStat[j].ToString();

                    BoxArrow += 1;
                }
            }
            EventList.transform.GetChild(1).GetChild(BoxArrow).GetChild(0).GetComponent<TextMeshProUGUI>().text = PossibleChooseEventClassList[i].EventRewardMoney.ToString();
        }
    }

    // 이벤트 리스트 들을 클릭했을 때 나올 클릭한 이벤트의 설명
    public void ShowSelectedEventInfo()
    {
        GameObject _EventDataObj = GameObject.Find("Reward_Image");
        GameObject _NowEvent = EventSystem.current.currentSelectedGameObject;

        // if (_NowEvent.name == )
        {

        }
    }
}
