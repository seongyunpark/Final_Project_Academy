using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScheduleUI : MonoBehaviour
{
    public SaveEventData tempEventList;                                // ���� ������ ��¥�� �ޱ� ���� �ӽ� ����
    public List<SaveEventData> MyEventList = new List<SaveEventData>();       // ���� ���� �̺�Ʈ ���

    [Header("PossibleCountImg")]
    public GameObject _nowPossibleCountImg;

    [Space(10f)]
    [Header("Buttons")]
    public GameObject Choose_Button;
    public GameObject SetOK_Button;

    [Header("EventInfoWhiteScreen")]
    public GameObject WhiteScreen;

    [Space(10f)]
    [Header("SelectdEvent&SetOkEvent")]
    public GameObject RewardInfoScreen;
    public GameObject MonthOfRewardInfoScreen;

    const int PossibleSetCount = 2;      // �ִ� �̺�Ʈ ���� ���� Ƚ��
    [Space(10f)]
    public int nowPossibleCount = 2;            // ���� �̺�Ʈ����Ƚ��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void GetISelectedEvent()
    {
        //MyeventListSwitchEventList.Instance.IfIChoosedEvent;
    }
}
