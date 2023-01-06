using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSchedule : MonoBehaviour
{
    private static EventSchedule _instance = null;

    public List<SaveEventClassData> MyEventList = new List<SaveEventClassData>();

    public Button[,] CalenderArr = new Button[4, 5];

    public GameObject PossibleEventListScreen;
    public GameObject SetEventListScreen;

    public const int PossibleSetCount = 2;        // 이벤트 지정 가능 횟수

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

    public void ShowSelectedEventOnCalender()
    {

    }

    public void SetCalenderButton()
    {
        GameObject calender = GameObject.Find("CalenderDate_Panel");

        for(int i = 0; i < CalenderArr.GetLength(0); i++)           // y    1차원배열 크기
        {
            for(int j = 0; j < CalenderArr.GetLength(1); j++)       // x    2차원 배열 크기
            {
                // CalenderArr 배열에 
                // CalenderArr = calender.transform.GetChild(0)
            }
        }
    }


}
