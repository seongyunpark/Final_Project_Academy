using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

// ������ ���� ������ ���� Ÿ��
public enum MailType
{
    Month,
    News,
    Satisfaction
}

// ���ϵ��� ������ ���� Ŭ����
public class MailBox
{
    public string m_MailTitle;
    public string m_SendMailDate;
    public string m_FromMail;
    public string m_MailContent;
    public MailType m_Type;
    public bool m_IsNewMail;
}

public class ChangeMailContent : MonoBehaviour
{
    [SerializeField] private Button m_NewMailButton;        // �űԸ��� ��ư
    [SerializeField] private GameObject m_Button;           // ó�� �������� ������ ������ �űԸ��Ϸ� ������ֱ� ���� ������Ʈ
    [SerializeField] private GameObject m_ReadMailContent;  // ���� ������ �ִ� ������Ʈ. ���� ������� �־�����Ѵ�.
    [SerializeField] private Transform m_MailBox;           // �߼۵� ���ϵ��� ��� ������ �� �������ִ� Transform
    [SerializeField] private GameObject m_NotNewMail;       // ���ο� ������ ������ "�űԸ����� �����ϴ�"�� ����ֱ� ���� ������Ʈ
    [SerializeField] private GameObject m_BulbIcon;         // �űԸ����� �߼۵Ǹ� �����Կ� ��� �̹���
    [SerializeField] private GameObject m_Filter;           // Ÿ�Ժ��� ������ �� ���ְ� �ϱ����� ������Ʈ

    private GameObject m_ClickMailButton;   // ��ü ���������� �ű� ���������� ����ؾ��Ѵ�.
    private GameObject m_CheckMailName;     // ���� �������� �ϴ� ������ �������� �˱����� ����
    private GameObject m_CheckFilter;       // ���� ������ �ϴ� ������ ���� Ÿ������ Ȯ���ϱ� ���� ����

    // ������ �����ϱ� ���� ����Ʈ��
    private Dictionary<string, MailBox> m_Mail = new Dictionary<string, MailBox>();     // �߼ۿ� ���̴� ��ü ����
    private List<MailBox> m_MailList = new List<MailBox>();                             // ���ο� ���� ����
    private List<GameObject> m_NewMailTextList = new List<GameObject>();                // ���� ���� ���ϵ��� �׾� �� ����Ʈ
    private List<GameObject> m_AllMailTextList = new List<GameObject>();                // ���°� ���� ���ϵ��� �׾� �� ����Ʈ

    int _i = 0;                     // ��ü �����Կ��� ������� ���ϵ��� �߼����ֱ� ���� ���� int��. ������ �߼��ϰ� ���� ���������ش�(���� ���� �������� ���´ٸ� �ٲ���� �� ����).
    bool _isNewMAilCheck = false;   // ���� ���ο� ������ �ִ��� Ȯ�����ִ� bool��
    bool _isClick = false;          // ���� ��ư�� ������ �� ���͸� ���� �״� �� �� �ְ� ���ַ��� ���� bool��


    private void Start()
    {
        m_ClickMailButton = m_Button;
        MakeMail("�ȳ�ȳ�", "2023�� 01�� 01��", "���ؾ�ī����", "���� �����̴�!", MailType.News);
        MakeMail("�����", "2023�� 01�� 02��", "���ؾ�ī����", " �׸�,,", MailType.Month);
    }

    // �ӽ÷� ������ ������ִ� �Լ�
    private void MakeMail(string _title, string _date, string _from, string _content, MailType _type)
    {
        MailBox m_MailComposition = new MailBox();
        m_MailComposition.m_MailTitle = _title;
        m_MailComposition.m_SendMailDate = _date;
        m_MailComposition.m_FromMail = _from;
        m_MailComposition.m_MailContent = _content;
        m_MailComposition.m_Type = _type;
        m_MailComposition.m_IsNewMail = true;
        m_Mail.Add(m_MailComposition.m_MailTitle, m_MailComposition);
    }

    // ����Ʈ�� �ε����� ã���ִ� �Լ�. ������Ʈ�� �̸����� ã�Ƽ� �ε����� ã��.
    public int FindListIndex(List<MailBox> list, string val)
    {
        if (list == null)
        {
            return -1;
        }

        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("���� ����Ʈ ī��Ʈ : " + list.Count);
            if (list[i].m_MailTitle == val)
            {
                return i;
            }
        }

        return -1;
    }

    // �ΰ��� UI �������� ������ �� ���ο� ������ �ֳ� ���� Ȯ���غ��� ���ο� ������ �����ϴٸ� ����ִ� ����
    public void SetButtonColor()
    {
        m_NewMailButton.Select();

        // ó�� ��������  ���� �űԸ������� ���;��Ѵ�.
        if (m_ClickMailButton.name == "All_Mail_Button")
        {
            m_ClickMailButton = m_Button;
        }

        bool _isNewMail = false;

        foreach (var mail in m_MailList)
        {
            if (mail.m_IsNewMail)
            {
                _isNewMail = true;
                _isNewMAilCheck = true; // ���� �Ұ��� �����ϸ� �ϼ��� ��..
                break;
            }
        }

        if (!_isNewMail)
        {
            m_NotNewMail.SetActive(true);
        }
        else
        {
            m_NotNewMail.SetActive(false);
        }
    }

    // ������ ����� �������, ������¥�� �ٲ��ش�.
    public void ChangeDateAndFrom()
    {
        m_MailList.Add(m_Mail.ElementAt(_i).Value);

        GameObject m_AllMailText;

        if (m_MailList[_i].m_IsNewMail)
        {
            m_AllMailText = MailObjectPool.GetObject(m_MailBox);

            m_AllMailText.transform.localScale = new Vector3(1f, 1f, 1f);
            m_AllMailText.name = m_MailList[_i].m_MailTitle;
            m_AllMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[_i].m_MailTitle;
            m_AllMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[_i].m_SendMailDate;
            m_AllMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[_i].m_FromMail;
            m_AllMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);

            m_NewMailTextList.Add(m_AllMailText);
            m_AllMailTextList.Add(m_AllMailText);
            _i++;
            _isNewMAilCheck = true;
        }
    }

    private void ReadMail()
    {
        if (m_ClickMailButton.name == "New_Mail_Button")
        {
            Debug.Log("�űԸ��� �б�");
            MailBox m_tempContent;

            // �� �θ� ã�� �θ��� �̸��� ������ ��ųʸ����� ���� �̸��� ������ ã�ƿ´�.
            m_CheckMailName = EventSystem.current.currentSelectedGameObject;
            string m_Parent = m_CheckMailName.transform.parent.name;
            Debug.Log(m_Parent);

            m_Mail.TryGetValue(m_Parent, out m_tempContent);

            int _index = FindListIndex(m_MailList, m_tempContent.m_MailTitle);

            m_MailList[_index].m_IsNewMail = false;
            _isNewMAilCheck = false;

            m_ReadMailContent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = m_tempContent.m_MailTitle;
            m_ReadMailContent.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = m_tempContent.m_FromMail;
            m_ReadMailContent.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = m_tempContent.m_MailContent;
            Debug.Log("���� ���� ���");
            m_ReadMailContent.SetActive(true);
        }
        else if (m_ClickMailButton.name == "All_Mail_Button")
        {
            Debug.Log("��ü���� �б�");
            MailBox m_tempContent;

            // �� �θ� ã�� �θ��� �̸��� ������ ��ųʸ����� ���� �̸��� ������ ã�ƿ´�.
            m_CheckMailName = EventSystem.current.currentSelectedGameObject;
            string m_Parent = m_CheckMailName.transform.parent.name;
            Debug.Log(m_Parent);

            m_Mail.TryGetValue(m_Parent, out m_tempContent);

            m_ReadMailContent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = m_tempContent.m_MailTitle;
            m_ReadMailContent.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = m_tempContent.m_FromMail;
            m_ReadMailContent.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = m_tempContent.m_MailContent;
            Debug.Log("���� ���� ���");
            m_ReadMailContent.SetActive(true);
        }
    }

    // �ű� �������� Ŭ���ϸ� ���ο� ������ �ִ��� �Ǵ����ְ� ��ü �������� Ŭ���ϸ� ������ ���� ���ϵ� ��� ����ֱ�
    public void ClickMailBox()
    {
        m_ClickMailButton = EventSystem.current.currentSelectedGameObject;

        int _DestroyMailCount = m_MailBox.childCount;

        if (m_ClickMailButton.name == "New_Mail_Button")
        {
            GameObject m_NewMailText; // = m_AllMailTextList[i]

            if (m_MailBox.childCount != 0)
            {
                for (int i = _DestroyMailCount - 1; i >= 0; i--)
                {
                    MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(i).gameObject);
                }

                for (int i = 0; i < m_MailList.Count; i++)
                {
                    if (m_MailList[i].m_IsNewMail == true)
                    {
                        m_NewMailText = MailObjectPool.GetObject(m_MailBox);

                        m_NewMailText.transform.localScale = new Vector3(1f, 1f, 1f);
                        m_NewMailText.name = m_MailList[i].m_MailTitle;
                        m_NewMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_MailTitle;
                        m_NewMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_SendMailDate;
                        m_NewMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_FromMail;
                        m_NewMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);

                        _isNewMAilCheck = true;
                    }
                }

                if (!_isNewMAilCheck)
                {
                    m_NotNewMail.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < m_MailList.Count; i++)
                {
                    if (m_MailList[i].m_IsNewMail == true)
                    {
                        m_NewMailText = MailObjectPool.GetObject(m_MailBox);

                        m_NewMailText.transform.localScale = new Vector3(1f, 1f, 1f);
                        m_NewMailText.name = m_MailList[i].m_MailTitle;
                        m_NewMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_MailTitle;
                        m_NewMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_SendMailDate;
                        m_NewMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_FromMail;
                        m_NewMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);

                        _isNewMAilCheck = true;
                    }
                }

                if (m_MailBox.childCount == 0)
                {
                    m_NotNewMail.SetActive(true);
                }
            }
        }
        else if (m_ClickMailButton.name == "All_Mail_Button")
        {
            if (m_MailBox.childCount != 0)
            {
                for (int i = _DestroyMailCount - 1; i >= 0; i--)
                {
                    MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(i).gameObject);
                }
            }

            for (int i = 0; i < m_AllMailTextList.Count; i++)
            {
                GameObject m_AllMailText; //= m_AllMailTextList[i];

                m_AllMailText = MailObjectPool.GetObject(m_MailBox);

                m_AllMailText.transform.localScale = new Vector3(1f, 1f, 1f);
                m_AllMailText.name = m_MailList[i].m_MailTitle;
                m_AllMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_MailTitle;
                m_AllMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_SendMailDate;
                m_AllMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_FromMail;
                m_AllMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);
            }
            m_NotNewMail.SetActive(false);
        }
    }

    // �ݱ� ��ư�� ������ ��� ���� ������ ���������ְ� �ű� ������ ���ٸ� �űԸ��� ���� ����ֱ�
    // �űԸ����Կ��� ���°��� ��ü �����Կ��� ���°��� ���� �ʿ�
    public void CloseMail()
    {
        if (m_ClickMailButton.name == "New_Mail_Button")// ���� ���� ��ü�����Կ� �ִ��� �ű� �����Կ� �ִ��� ������ ������Ѵ�.
        {
            string m_TitleName = m_ReadMailContent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

            int _index = FindListIndex(m_MailList, m_TitleName);

            Debug.Log(_index + " ����");
            if (m_MailBox.childCount > 1)
            {
                MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(_index).gameObject);
                m_NewMailTextList.RemoveAt(_index);
            }
            else
            {
                int _mailBoxCount = m_MailBox.childCount - 1;
                MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(_mailBoxCount).gameObject);
                if (m_NewMailTextList.Count == 1 && _index != 0)
                {
                    m_NewMailTextList.RemoveAt(_index - 1);
                }
                else
                {
                    m_NewMailTextList.RemoveAt(_index);
                }
            }

            bool _isNewMail = false;

            foreach (var mail in m_MailList)
            {
                if (mail.m_IsNewMail == true)
                {
                    _isNewMail = true;
                    _isNewMAilCheck = true;
                    break;
                }
            }

            if (!_isNewMail)
            {
                m_NotNewMail.SetActive(true);
                _isNewMAilCheck = false;
            }

            m_ReadMailContent.SetActive(false);

            if (m_MailBox.childCount == 0)
            {
                m_NotNewMail.SetActive(true);
            }
        }
        // ��ü�����Կ����� ���ֱ⸸ �ϸ� �ȴ�.
        else if (m_ClickMailButton.name == "All_Mail_Button")
        {
            _isNewMAilCheck = false;
            m_ReadMailContent.SetActive(false);
        }
    }

    // �ڷΰ��� ��ư�� ������ ��� �űԸ����� ����ó���� �ٲ��ֱ�
    public void ClickBackButton()
    {
        m_NewMailTextList.Clear();
        #region _�ڷΰ��⸦ ������ �űԸ����� �����ؾ��ϴ� ����� �ڵ� 
        //bool _isNewMail = false;

        //foreach (var mail in m_MailList)
        //{
        //    if (mail.m_IsNewMail)
        //    {
        //        _isNewMail = true;
        //        break;
        //    }
        //}


        //if (!_isNewMail)
        //{
        //    m_BulbIcon.SetActive(false);
        //}
        #endregion

        int _DestroyMailObj = m_MailBox.childCount;

        if (m_ClickMailButton.name == "New_Mail_Button")
        {
            if (m_MailBox.childCount != 0)
            {
                for (int i = _DestroyMailObj - 1; i >= 0; i--)
                {
                    MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(i).gameObject);
                }
            }

            foreach (var mail in m_MailList)
            {
                if (mail.m_IsNewMail)
                {
                    mail.m_IsNewMail = false;
                    _isNewMAilCheck = false;
                }
            }
            m_Filter.SetActive(false);
            m_BulbIcon.SetActive(false);
        }
        else if (m_ClickMailButton.name == "All_Mail_Button") // ���� Ȯ���� ��....
        {
            for (int i = m_AllMailTextList.Count - 1; i >= 0; i--)
            {
                MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(i).gameObject);
            }
            _isNewMAilCheck = false;
            m_Filter.SetActive(false);
            m_BulbIcon.SetActive(false);
        }
    }

    public void ClickFilter()
    {
        if (_isClick == false)
        {
            m_Filter.SetActive(true);
            _isClick = true;
        }
        else
        {
            m_Filter.SetActive(false);
            _isClick = false;
        }
    }

    private void MakeNewMailForType(MailType _type)
    {
        if (m_NewMailTextList.Count != 0)
        {
            int _DestroyMailCount = m_MailBox.childCount;

            GameObject m_NewMailText;

            if (m_MailBox.childCount != 0)
            {
                for (int i = _DestroyMailCount - 1; i >= 0; i--)
                {
                    MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(i).gameObject);
                }

                for (int i = 0; i < m_NewMailTextList.Count; i++)
                {
                    if (m_MailList[i].m_Type == _type)
                    {
                        m_NewMailText = MailObjectPool.GetObject(m_MailBox);

                        m_NewMailText.transform.localScale = new Vector3(1f, 1f, 1f);
                        m_NewMailText.name = m_MailList[i].m_MailTitle;
                        m_NewMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_MailTitle;
                        m_NewMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_SendMailDate;
                        m_NewMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_FromMail;
                        m_NewMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);

                        _isNewMAilCheck = true;
                    }
                }
                m_NotNewMail.SetActive(false);

                if (m_MailBox.childCount == 0)
                {
                    m_NotNewMail.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < m_NewMailTextList.Count; i++)
                {
                    if (m_MailList[i].m_Type == _type)
                    {
                        m_NewMailText = MailObjectPool.GetObject(m_MailBox);

                        m_NewMailText.transform.localScale = new Vector3(1f, 1f, 1f);
                        m_NewMailText.name = m_MailList[i].m_MailTitle;
                        m_NewMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_MailTitle;
                        m_NewMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_SendMailDate;
                        m_NewMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_FromMail;
                        m_NewMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);

                        _isNewMAilCheck = true;
                    }
                }
                m_NotNewMail.SetActive(false);

                if (m_MailBox.childCount == 0)
                {
                    m_NotNewMail.SetActive(true);
                }
            }
        }
    }

    private void MakeAllMailForType(MailType _type)
    {
        int _DestroyMailCount = m_MailBox.childCount;

        if (m_MailBox.childCount != 0)
        {
            for (int i = _DestroyMailCount - 1; i >= 0; i--)
            {
                MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < m_AllMailTextList.Count; i++)
        {
            GameObject m_AllMailText; //= m_AllMailTextList[i];

            if (m_MailList[i].m_Type == _type)
            {
                m_AllMailText = MailObjectPool.GetObject(m_MailBox);

                m_AllMailText.transform.localScale = new Vector3(1f, 1f, 1f);
                m_AllMailText.name = m_MailList[i].m_MailTitle;
                m_AllMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_MailTitle;
                m_AllMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_SendMailDate;
                m_AllMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_FromMail;
                m_AllMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);
            }
        }
        m_NotNewMail.SetActive(false);

        if (m_MailBox.childCount == 0)
        {
            m_NotNewMail.SetActive(true);
        }
    }

    public void SetMailForType()
    {
        m_CheckFilter = EventSystem.current.currentSelectedGameObject;

        if (m_ClickMailButton.name == "New_Mail_Button")
        {
            if (m_CheckFilter.name == "Month_Report")
            {
                MakeNewMailForType(MailType.Month);
            }
            else if (m_CheckFilter.name == "Game_News")
            {
                MakeNewMailForType(MailType.News);
            }
            else if (m_CheckFilter.name == "Satisfaction")
            {
                MakeNewMailForType(MailType.Satisfaction);
            }
            else if (m_CheckFilter.name == "All")
            {
                GameObject m_NewMailText;
                int _DestroyMailCount = m_MailBox.childCount;

                if (m_MailBox.childCount != 0)
                {
                    for (int i = _DestroyMailCount - 1; i >= 0; i--)
                    {
                        MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(i).gameObject);
                    }

                    for (int i = 0; i < m_MailList.Count; i++)
                    {
                        if (m_MailList[i].m_IsNewMail == true)
                        {
                            m_NewMailText = MailObjectPool.GetObject(m_MailBox);

                            m_NewMailText.transform.localScale = new Vector3(1f, 1f, 1f);
                            m_NewMailText.name = m_MailList[i].m_MailTitle;
                            m_NewMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_MailTitle;
                            m_NewMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_SendMailDate;
                            m_NewMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_FromMail;
                            m_NewMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);

                            _isNewMAilCheck = true;
                        }
                    }

                    if (!_isNewMAilCheck)
                    {
                        m_NotNewMail.SetActive(true);
                    }

                }
                else if (m_MailBox.childCount == 0 && m_NewMailTextList.Count != 0)
                {
                    for (int i = 0; i < m_MailList.Count; i++)
                    {
                        if (m_MailList[i].m_IsNewMail == true)
                        {
                            m_NewMailText = MailObjectPool.GetObject(m_MailBox);

                            m_NewMailText.transform.localScale = new Vector3(1f, 1f, 1f);
                            m_NewMailText.name = m_MailList[i].m_MailTitle;
                            m_NewMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_MailTitle;
                            m_NewMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_SendMailDate;
                            m_NewMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_FromMail;
                            m_NewMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);

                            _isNewMAilCheck = true;
                        }
                    }
                    m_NotNewMail.SetActive(false);
                }
                else if (m_NewMailTextList.Count == 0)
                {
                    m_NotNewMail.SetActive(true);
                }
            }
        }
        else if (m_ClickMailButton.name == "All_Mail_Button")
        {
            if (m_CheckFilter.name == "Month_Report")
            {
                MakeAllMailForType(MailType.Month);
            }
            else if (m_CheckFilter.name == "Game_News")
            {
                MakeAllMailForType(MailType.News);
            }
            else if (m_CheckFilter.name == "Satisfaction")
            {
                MakeAllMailForType(MailType.Satisfaction);
            }
            else if (m_CheckFilter.name == "All")
            {
                if (m_MailBox.childCount != 0)
                {
                    int _DestroyMailCount = m_MailBox.childCount;

                    for (int i = _DestroyMailCount - 1; i >= 0; i--)
                    {
                        MailObjectPool.ReturnObject(m_MailBox.transform.GetChild(i).gameObject);
                    }
                }

                for (int i = 0; i < m_AllMailTextList.Count; i++)
                {
                    GameObject m_AllMailText;

                    m_AllMailText = MailObjectPool.GetObject(m_MailBox);

                    m_AllMailText.transform.localScale = new Vector3(1f, 1f, 1f);
                    m_AllMailText.name = m_MailList[i].m_MailTitle;
                    m_AllMailText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_MailTitle;
                    m_AllMailText.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_SendMailDate;
                    m_AllMailText.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_MailList[i].m_FromMail;
                    m_AllMailText.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(ReadMail);
                }
                m_NotNewMail.SetActive(false);
            }
        }
    }
}
