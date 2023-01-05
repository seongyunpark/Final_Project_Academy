using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MailBox
{
    public string m_MailTitle;
    public string m_SendMailDate;
    public string m_FromMail;
    public string m_MailContent;
}

public class ChangeMailContent : MonoBehaviour
{
    [SerializeField] private Button m_NewMailButton;
    [SerializeField] private MailSystem m_IsNewMail;
    [SerializeField] private GameObject m_NotNewMail;
    [SerializeField] private GameObject m_NewMailPrefab;
    [SerializeField] private GameObject m_ReadMailContent;
    [SerializeField] private Transform m_MailBox;
    [SerializeField] private Transform m_NotNewMailBox;
    private GameObject m_TempNotNewMail;
    private GameObject m_NewMailText;
    bool m_IsMakeOne = false; // 새로운메일을 한번만 띄워주기 위해

    private MailBox m_MailComposition = new MailBox();

    // 메일을 관리하기 위한 리스트들
    private Dictionary<string, MailBox> m_Mail = new Dictionary<string, MailBox>();     // 발송에 씌이는 전체 메일
    private List<MailBox> m_NewMail = new List<MailBox>();  // 새로온 메일
    private List<MailBox> m_AllMail = new List<MailBox>();  // 여태껏 받은 메일 

    private void Start()
    {
        m_MailComposition.m_MailTitle = "안녕하세요";
        m_MailComposition.m_SendMailDate = "2022년 12월 31일";
        m_MailComposition.m_FromMail = "망극언니";
        m_MailComposition.m_MailContent = "나는 망극이다!";
        m_Mail.Add(m_MailComposition.m_MailTitle, m_MailComposition);
    }

    public void SetButtonColor()
    {
        m_NewMailButton.Select();

        if (m_NewMail.Count == 0)
        {
            m_TempNotNewMail = GameObject.Instantiate(m_NotNewMail, m_NotNewMailBox);
            m_IsMakeOne = true;
        }
        else
        {
            Destroy(m_TempNotNewMail);
        }
    }

    // 메일의 제목과 보낸사람, 보낸날짜를 바꿔준다.
    public void ChangeDateAndFrom()
    {
        m_NewMail.Add(m_Mail.ElementAt(0).Value);

        m_NewMailText = Instantiate(m_NewMailPrefab, m_MailBox);
        m_NewMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NewMail[0].m_MailTitle;
        m_NewMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_NewMail[0].m_SendMailDate;
        m_NewMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_NewMail[0].m_FromMail;
        m_NewMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);
    }

    // 메일의 내용을 바꿔준다.
    public void ChangeContents()
    {

    }

    private void ReadMail()
    {
        m_AllMail.Add(m_NewMail[0]);
        m_NewMail.RemoveAt(0);
        Destroy(m_TempNotNewMail);
        Destroy(m_NewMailText);

        m_ReadMailContent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = m_AllMail[0].m_MailTitle;
        m_ReadMailContent.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = m_AllMail[0].m_FromMail;
        m_ReadMailContent.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = m_AllMail[0].m_MailContent;

        m_ReadMailContent.SetActive(true);
    }
}
