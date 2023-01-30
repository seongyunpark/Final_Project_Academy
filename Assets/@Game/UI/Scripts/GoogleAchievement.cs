using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoogleAchievement : MonoBehaviour
{
    public GameObject MailUIButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsMailClicked()
    {
        GameObject nowClick = EventSystem.current.currentSelectedGameObject;

        if(nowClick.name == MailUIButton.name)
        {
            Debug.Log("���� Ŭ�� ���� �޼� �׽�Ʈ");


            if (GPGSBinder.Instance.UserId != null)
            {
                GPGSBinder.Instance.UnlockAchievement(GPGSIds.achievement_Mail);     //���� ���� �޼�
            }

        }
    }

    public void IsSetFirstSchedule()
    {
        Debug.Log("ù ���� ���� �Ǿ�������!");


        if (GPGSBinder.Instance.UserId != null)
        {
            GPGSBinder.Instance.UnlockAchievement(GPGSIds.achievement_SetClass);     //���� ���� �޼�
        }

    }

    public void GoogleAchevment()
    {
        Debug.Log("���� ���� â ����");


        if (GPGSBinder.Instance.UserId != null)
        {
            GPGSBinder.Instance.ShowAchievementUI();
        }

    }
}
