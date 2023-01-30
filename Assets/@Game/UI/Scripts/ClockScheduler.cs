using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 2023. 01. 10 Mang
/// 
/// �̺�Ʈ, ���� �� ���� �ð��� �߻��� ��� �̺�Ʈ�� �ð� üũ �� �߻� ���� �� ��ũ��Ʈ
/// </summary>
public class ClockScheduler : MonoBehaviour
{
    [SerializeField] PopUpUI _popUpClassPanel;

    [SerializeField] PopUpUI _PopUpEventPanel;
    [SerializeField] PopUpUI _TempEventPanel;
    [SerializeField] GameObject _tempEventPanelforText;


    public GameObject EventStartButton;
    public GameObject EventPanel;

    int checkMonth = 3;       // ���� Date�� Week�� �ٲ�� �ð��� �޶� Date�� �ٲ����� Week�� �ٲ����ʾ� �߻��ϴ� ������ ��� ���� ���� ����
    int checkWeek = 1;

    bool isReadyPopUpEvent = false;
    bool isAlreadySetEvent = false;

    bool IsResetEventList = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �ð� ������ ��� ���� ��������

        // �� �ٲ���� ��
        if (GameTime.Instance.FlowTime.NowDay == 1 && GameTime.Instance.FlowTime.NowWeek == 1 && checkMonth != GameTime.Instance.FlowTime.NowMonth)
        {
            // ���� ���� ������ �Ϸ��ϰ� �� �����ް� �� �����޿��� ������ ������Ѿ��Ѵ�. 
            if (InGameTest.Instance._isRepeatClass == true)
            {
                InGameTest.Instance.NextClassStart();
                InGameTest.Instance._classCount++;
            }
            else if (InGameTest.Instance._isRepeatClass == false && checkMonth != 0)
            {
                _popUpClassPanel.TurnOnUI();
            }

            if (checkMonth == 12)
            {
                checkMonth = 1;
            }
            else
            {
                checkMonth += 1;
            }
        }

        if (GameTime.Instance.IsGameMode == true)
        {
            AutoEventPopUp();           // ������ ������ �ڵ����� �̺�Ʈ â �˾�
        }


        // �� �ٲ���� ��
        if (GameTime.Instance.FlowTime.NowWeek == 1 && GameTime.Instance.FlowTime.NowDay == 1 && IsResetEventList == false)
        {
            Debug.Log("���� �� �� �̺�Ʈ����Ʈ �� ����");
            isAlreadySetEvent = false;
            EventSchedule.Instance.ResetPossibleCount();
            SwitchEventList.Instance.ReturnEventPrefabToPool();
            IsResetEventList = true;
            SwitchEventList.Instance.IsSetEventList = false;
            SwitchEventList.Instance.PrevIChoosedEvent.Clear();
        }

        if(GameTime.Instance.FlowTime.NowWeek != 1 && GameTime.Instance.FlowTime.NowDay != 1)
        {
            IsResetEventList = false;
        }

        PopUpEventStartButton();    // �� �� �̺�Ʈ ���� ����
    }

    // ������ �̺�Ʈ â�� �ڵ����� �߰� �ϱ�
    public void AutoEventPopUp()
    {
        //foreach (var thisMonthEvent in SwitchEventList.Instance.MyEventList)
        for (int i = 0; i < SwitchEventList.Instance.MyEventList.Count; i++)
        {
            if (SwitchEventList.Instance.MyEventList[i].IsPopUp == false)
            {
                if (SwitchEventList.Instance.MyEventList[i].EventDate[1] == GameTime.Instance.FlowTime.NowMonth)
                {
                    if (SwitchEventList.Instance.MyEventList[i].EventDate[2] == GameTime.Instance.FlowTime.NowWeek)
                    {
                        if (SwitchEventList.Instance.MyEventList[i].EventDate[3] == GameTime.Instance.FlowTime.NowDay)
                        {
                            _tempEventPanelforText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SwitchEventList.Instance.MyEventList[i].EventClassName + " �̺�Ʈ �߻�!";
                            _TempEventPanel.TurnOnUI();
                            SwitchEventList.Instance.MyEventList[i].IsPopUp = true;
                        }
                    }
                }
            }
        }
    }

    public void PopUpEventStartButton()
    {
        // �� -> 3, 6, 9, 12�� �϶� �б⺰ ���� �� �̺�Ʈ ���� �ǵ���
        if (GameTime.Instance.FlowTime.NowMonth % 3 == 0)
        {
            if (InGameTest.Instance._isSelectClassNotNull == true && isAlreadySetEvent == false)
            {
                EventStartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameTime.Instance.FlowTime.NowMonth.ToString();
                _PopUpEventPanel.TurnOnUI();

                isAlreadySetEvent = true;

                InGameTest.Instance._isSelectClassNotNull = false;
            }
        }
        else
        {
            if (isAlreadySetEvent == false)
            {
                EventStartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameTime.Instance.FlowTime.NowMonth.ToString();
                _PopUpEventPanel.TurnOnUI();

                isAlreadySetEvent = true;
            }
        }
    }
}
