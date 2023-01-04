using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ���� �ý����� �׽�Ʈ�� ���� ��ũ��Ʈ
/// 
/// 
/// </summary>
public class MailSystem : MonoBehaviour
{
    public UnityEvent m_SendMail;
    public ChangeMailContent m_ChangeDate;
    bool m_IsTurnOnIcon = false;

    void Update()
    {
        if(GameTime.Instance.WeekIndex == 2 && m_IsTurnOnIcon == false)
        {
            //m_ChangeDate.ChangeDateAndFrom(GameTime.Instance.Week[2]);
            SatisfyRequirement();
        }
    }

    private void SatisfyRequirement()
    {
        m_SendMail.Invoke();
        m_IsTurnOnIcon = true;
        Debug.Log("���� �߼�");
    }
}
