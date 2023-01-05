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
    bool m_IsMakeOne = false; // ���ο������ �ѹ��� ����ֱ� ����

    private MailBox m_MailComposition = new MailBox();

    // ������ �����ϱ� ���� ����Ʈ��
    private Dictionary<string, MailBox> m_Mail = new Dictionary<string, MailBox>();     // �߼ۿ� ���̴� ��ü ����
    private List<MailBox> m_NewMail = new List<MailBox>();  // ���ο� ����
    private List<MailBox> m_AllMail = new List<MailBox>();  // ���²� ���� ���� 

    private void Start()
    {
        m_MailComposition.m_MailTitle = "�ȳ��ϼ���";
        m_MailComposition.m_SendMailDate = "2022�� 12�� 31��";
        m_MailComposition.m_FromMail = "���ؾ��";
        m_MailComposition.m_MailContent = "���� �����̴�!";
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

    // ������ ����� �������, ������¥�� �ٲ��ش�.
    public void ChangeDateAndFrom()
    {
        m_NewMail.Add(m_Mail.ElementAt(0).Value);

        m_NewMailText = Instantiate(m_NewMailPrefab, m_MailBox);
        m_NewMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NewMail[0].m_MailTitle;
        m_NewMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_NewMail[0].m_SendMailDate;
        m_NewMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_NewMail[0].m_FromMail;
        m_NewMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);
    }

    // ������ ������ �ٲ��ش�.
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
