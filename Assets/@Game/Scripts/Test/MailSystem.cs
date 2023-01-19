using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// ���� �ý����� �׽�Ʈ�� ���� ��ũ��Ʈ
/// 
/// 
/// </summary>
public class MailSystem : MonoBehaviour
{
    public UnityEvent m_SendMail;
    public bool m_IsTurnOnIcon = false;

    void Update()
    {
        if(m_IsTurnOnIcon == true && GameTime.Instance.isChangeWeek == true)
        {
            m_IsTurnOnIcon = false;
        }

        if((GameTime.Instance.WeekIndex == 1 || GameTime.Instance.WeekIndex == 2) && m_IsTurnOnIcon == false)
        {
            //SatisfyRequirement();
        }
    }

    private void SatisfyRequirement()
    {
        m_SendMail.Invoke();
        m_IsTurnOnIcon = true;
        Debug.Log("���� �߼�");
    }
}
