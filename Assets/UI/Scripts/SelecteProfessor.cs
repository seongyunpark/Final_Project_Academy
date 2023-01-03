using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using StatData.Runtime;

public class Professor
{
    private List<ProfessorStat> m_ProductManagerProfessor = new List<ProfessorStat>();
    private List<ProfessorStat> m_ArtProfessor = new List<ProfessorStat>();
    private List<ProfessorStat> m_ProgrammingProfessor = new List<ProfessorStat>();

    #region _Professor_Property
    public List<ProfessorStat> ProductManagerProcessor
    {
        get { return m_ProductManagerProfessor; }
        set { m_ProductManagerProfessor = value; }
    }

    public List<ProfessorStat> ArtProcessor
    {
        get { return m_ArtProfessor; }
        set { m_ArtProfessor = value; }
    }

    public List<ProfessorStat> ProgrammingProfessor
    {
        get { return m_ProgrammingProfessor; }
        set { m_ProgrammingProfessor = value; }
    }
    #endregion
}

/// <summary>
/// ���� ������ ���� ��ũ��Ʈ. 
/// 
/// 
/// </summary>
public class SelecteProfessor : MonoBehaviour
{
    private static SelecteProfessor _instance = null;

    [SerializeField] private GameObject m_SelecteProfessor;
    [SerializeField] private ProfessorController m_LoadProfessorData;
    [SerializeField] private ClassPrefab m_ClassPrefabData;
    [SerializeField] private GameObject m_NextProfessorInfoButton;
    [SerializeField] private GameObject m_PreviousProfessorInfoButton;

    public GameObject m_SelecteClassName1;
    public GameObject m_SelecteClassName2;
    public GameObject m_SelecteClassName3;

    public GameObject m_ChoiceButton1;
    public GameObject m_ChoiceButton2;
    public GameObject m_ChoiceButton3;
    
    public GameObject m_ProfessorName1;
    public GameObject m_ProfessorName2;
    public GameObject m_ProfessorName3;

    public Professor m_NowPlayerProfessor = new Professor();

    public int m_ProfessorDataIndex; // �������� ������ ���� �� ����Ʈ���� ���� ������ ������ ������ ����ֱ� ���� �ε��� ����

    public Stack<int> m_ForClickNextButton = new Stack<int>(); // ���� ��ư�� ���� �� ���� �������ִ� ������ ���� �°� ��ư�� ���� �� �ְ� ���ֱ� ���� ����Ʈ

    public static SelecteProfessor Instance
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
        m_ProfessorDataIndex = 0;

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

    /// ���� ������ �ִ� ������ ����ŭ �� Ÿ���� ����Ʈ�� ������ �־��ش�.
    void Start()
    {
        for (int i = 0; i < m_LoadProfessorData.professorData.Count; i++)
        {
            if (m_LoadProfessorData.professorData.ElementAt(i).Value.m_ProfessorType == Type.Art)
            {
                m_NowPlayerProfessor.ArtProcessor.Add(m_LoadProfessorData.professorData.ElementAt(i).Value);
            }

            if (m_LoadProfessorData.professorData.ElementAt(i).Value.m_ProfessorType == Type.ProductManager)
            {
                m_NowPlayerProfessor.ProductManagerProcessor.Add(m_LoadProfessorData.professorData.ElementAt(i).Value);

            }

            if (m_LoadProfessorData.professorData.ElementAt(i).Value.m_ProfessorType == Type.Programming)
            {
                m_NowPlayerProfessor.ProgrammingProfessor.Add(m_LoadProfessorData.professorData.ElementAt(i).Value);

            }
        }
    }

    /// ���� ���� â�� ��� �� ������ ��ư�� ������ Ȯ���ϴ� �Լ�
    public void SetArrowButtons()
    {
        // ������ 1���̰ų� ����Ʈ�� ���� ������
        if (m_ForClickNextButton.Count == 0)
        {
            m_NextProfessorInfoButton.SetActive(true);
            m_PreviousProfessorInfoButton.SetActive(false);

        }
        else
        {
            m_NextProfessorInfoButton.SetActive(true);
            m_PreviousProfessorInfoButton.SetActive(true);
        }
    }

    /// ������ ������ ����ִ� �Լ�
    public void ProfessorInfo()
    {
        m_SelecteProfessor.SetActive(true);

        int _tempIndex = 0; // ����Ʈ�� ����ִ� ���� �� ���� ������ ������ �ε����� ã���ֱ� ���� ��� ���� temp ����.

        if (m_ClassPrefabData.m_SelecteClassDataList.Count > 0)
        {
            _tempIndex = m_ClassPrefabData.m_SelecteClassDataList.Count - 1;
        }
        else
        {
            _tempIndex = m_ClassPrefabData.m_SelecteClassDataList.Count;
        }

        GameObject _classInfo = GameObject.Find("ClassDataPanel");
        GameObject _professorInfo = GameObject.Find("ProfessorInfoPanel");

        // ����� ������ �����̸��� �������� �س����� ���� ������ ������ �ִ� ������ ���� �������� ��.
        _classInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassName;
        _classInfo.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "System";
        _classInfo.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Content";
        _classInfo.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Balance";

        // ���� ������ ������ �´� Ÿ���� ���� ������ ����� �Ѵ�.
        if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.Art)
        {
            // �� ������ ������ �ѱ�� ��ư�� ������ Ȯ���� ����� �Ѵ�.
            SetArrowButtons();

            if (m_ForClickNextButton.Count == m_NowPlayerProfessor.ArtProcessor.Count - 1)
            {
                m_NextProfessorInfoButton.SetActive(false);
            }

            _professorInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NowPlayerProfessor.ArtProcessor[m_ProfessorDataIndex].m_ProfessorNameValue;
        }
        else if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.ProductManager)
        {
            SetArrowButtons();                

            if (m_ForClickNextButton.Count == m_NowPlayerProfessor.ProductManagerProcessor.Count - 1)
            {
                m_NextProfessorInfoButton.SetActive(false);
            }

            _professorInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NowPlayerProfessor.ProductManagerProcessor[m_ProfessorDataIndex].m_ProfessorNameValue;
        }
        else if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.Programming)
        {
            SetArrowButtons();
            
            if (m_ForClickNextButton.Count == m_NowPlayerProfessor.ProgrammingProfessor.Count - 1)
            {
                m_NextProfessorInfoButton.SetActive(false);
            }

            _professorInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NowPlayerProfessor.ProgrammingProfessor[m_ProfessorDataIndex].m_ProfessorNameValue;
        }
    }

    /// ������ �������� �� ������ ��ư�� ������ �ε����� ���������༭ ���� ������ ������ ����� �� �ְ� ���ش�.
    public void ClickRightArrowButton()
    {
        m_ForClickNextButton.Push(m_ProfessorDataIndex);

        m_ProfessorDataIndex++;

        GameObject _professorInfo = GameObject.Find("ProfessorInfoPanel");

        int _tempIndex = m_ClassPrefabData.m_SelecteClassDataList.Count - 1;

        if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.Art)
        {
            SetArrowButtons();
            
            if (m_ForClickNextButton.Count == m_NowPlayerProfessor.ArtProcessor.Count - 1)
            {
                m_NextProfessorInfoButton.SetActive(false);
            }
            
            _professorInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NowPlayerProfessor.ArtProcessor[m_ProfessorDataIndex].m_ProfessorNameValue;
        }
        else if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.ProductManager)
        {
            SetArrowButtons();
            
            if (m_ForClickNextButton.Count == m_NowPlayerProfessor.ProductManagerProcessor.Count - 1)
            {
                m_NextProfessorInfoButton.SetActive(false);
            }
            
            _professorInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NowPlayerProfessor.ProductManagerProcessor[m_ProfessorDataIndex].m_ProfessorNameValue;
        }
        else if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.Programming)
        {
            SetArrowButtons();

            if (m_ForClickNextButton.Count == m_NowPlayerProfessor.ProgrammingProfessor.Count - 1)
            {
                m_NextProfessorInfoButton.SetActive(false);
            }
            
            _professorInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NowPlayerProfessor.ProgrammingProfessor[m_ProfessorDataIndex].m_ProfessorNameValue;
        }
    }

    /// ���� ������ ������ �������� ��ư�� ������ �� �������� �Լ�
    public void ClickLeftArrowButton()
    {
        m_ForClickNextButton.Pop();

        m_ProfessorDataIndex--;

        GameObject _professorInfo = GameObject.Find("ProfessorInfoPanel");

        int _tempIndex = m_ClassPrefabData.m_SelecteClassDataList.Count -1;

        if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.Art)
        {
            SetArrowButtons();

            if (m_ForClickNextButton.Count == m_NowPlayerProfessor.ArtProcessor.Count - 1)
            {
                m_NextProfessorInfoButton.SetActive(false);
            }
            _professorInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NowPlayerProfessor.ArtProcessor[m_ProfessorDataIndex].m_ProfessorNameValue;
        }
        else if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.ProductManager)
        {
            SetArrowButtons();

            if (m_ForClickNextButton.Count == m_NowPlayerProfessor.ProductManagerProcessor.Count - 1)
            {
                m_NextProfessorInfoButton.SetActive(false);
            }
            _professorInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NowPlayerProfessor.ProductManagerProcessor[m_ProfessorDataIndex].m_ProfessorNameValue;

        }
        else if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.Programming)
        {
            SetArrowButtons();

            if (m_ForClickNextButton.Count == m_NowPlayerProfessor.ProgrammingProfessor.Count - 1)
            {
                m_NextProfessorInfoButton.SetActive(false);
            }
            _professorInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_NowPlayerProfessor.ProgrammingProfessor[m_ProfessorDataIndex].m_ProfessorNameValue;

        }
    }

    /// ��� ������ ������ �� (������ ����) ���� ���� �����ߴ� �͵��� �����͸� �����ص״ٰ� ȭ�鿡 ������ �Ѵ�.
    public void SelecteComplete()
    {
        int _tempIndex = 0;

        if (m_ClassPrefabData.m_SelecteClassDataList.Count > 0)
        {
            _tempIndex = m_ClassPrefabData.m_SelecteClassDataList.Count - 1;
        }
        else
        {
            _tempIndex = m_ClassPrefabData.m_SelecteClassDataList.Count;
        }

        // ���� ������ ��ư�� ��ġ�� ����ߴٰ� ������ ��ġ�� �ش� ������ ������ �־�����Ѵ�.
        if (m_ClassPrefabData.m_SelecteClassButtonName != null)
        {
            if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.Art)
            {
                m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave = m_NowPlayerProfessor.ArtProcessor[m_ProfessorDataIndex];
                m_ClassPrefabData.m_ArtData.Add(m_ClassPrefabData.m_SaveData);

                // Ŭ���� ��ư�� ������ ���� ����Ʈ�� �ε����� �ٲ��ش�(Button1�̸� �ε��� 0���� �־��ֱ�)
                if (m_ClassPrefabData.m_SaveData.m_ClickPointDataSave == "1Week_Button1")
                {
                    m_ClassPrefabData.ChangeListIndex(m_ClassPrefabData.m_ArtData, 0, m_ClassPrefabData.m_SaveData);
                    m_ClassPrefabData.m_ArtData.RemoveAt(3);
                    m_ChoiceButton1.SetActive(false);
                    m_SelecteClassName1.SetActive(true);
                    m_ProfessorName1.SetActive(true);
                    m_SelecteClassName1.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteClassDataSave.m_ClassName;
                    m_ProfessorName1.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave.m_ProfessorNameValue;
                }
                else if (m_ClassPrefabData.m_SaveData.m_ClickPointDataSave == "1Week_Button2")
                {
                    m_ClassPrefabData.ChangeListIndex(m_ClassPrefabData.m_ArtData, 1, m_ClassPrefabData.m_SaveData);
                    m_ClassPrefabData.m_ArtData.RemoveAt(3);
                    m_ChoiceButton2.SetActive(false);
                    m_SelecteClassName2.SetActive(true);
                    m_ProfessorName2.SetActive(true);
                    m_SelecteClassName2.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteClassDataSave.m_ClassName;
                    m_ProfessorName2.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave.m_ProfessorNameValue;

                }
                else if (m_ClassPrefabData.m_SaveData.m_ClickPointDataSave == "1Week_Button3")
                {
                    m_ClassPrefabData.ChangeListIndex(m_ClassPrefabData.m_ArtData, 2, m_ClassPrefabData.m_SaveData);
                    m_ClassPrefabData.m_ArtData.RemoveAt(3);
                    m_ChoiceButton3.SetActive(false);
                    m_SelecteClassName3.SetActive(true);
                    m_ProfessorName3.SetActive(true);
                    m_SelecteClassName3.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteClassDataSave.m_ClassName;
                    m_ProfessorName3.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave.m_ProfessorNameValue;
                }
            }
            else if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.ProductManager)
            {
                m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave = m_NowPlayerProfessor.ProductManagerProcessor[m_ProfessorDataIndex];
                m_ClassPrefabData.m_ProductManagerData.Add(m_ClassPrefabData.m_SaveData);

                if (m_ClassPrefabData.m_SaveData.m_ClickPointDataSave == "1Week_Button1")
                {
                    m_ClassPrefabData.ChangeListIndex(m_ClassPrefabData.m_ProductManagerData, 0, m_ClassPrefabData.m_SaveData);
                    m_ClassPrefabData.m_ProductManagerData.RemoveAt(3);
                    m_ChoiceButton1.SetActive(false);
                    m_SelecteClassName1.SetActive(true);
                    m_ProfessorName1.SetActive(true);
                    m_SelecteClassName1.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteClassDataSave.m_ClassName;
                    m_ProfessorName1.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave.m_ProfessorNameValue;
                }
                else if (m_ClassPrefabData.m_SaveData.m_ClickPointDataSave == "1Week_Button2")
                {
                    m_ClassPrefabData.ChangeListIndex(m_ClassPrefabData.m_ProductManagerData, 1, m_ClassPrefabData.m_SaveData);
                    m_ClassPrefabData.m_ProductManagerData.RemoveAt(3);
                    m_ChoiceButton2.SetActive(false);
                    m_SelecteClassName2.SetActive(true);
                    m_ProfessorName2.SetActive(true);
                    m_SelecteClassName2.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteClassDataSave.m_ClassName;
                    m_ProfessorName2.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave.m_ProfessorNameValue;

                }
                else if (m_ClassPrefabData.m_SaveData.m_ClickPointDataSave == "1Week_Button3")
                {
                    m_ClassPrefabData.ChangeListIndex(m_ClassPrefabData.m_ProductManagerData, 2, m_ClassPrefabData.m_SaveData);
                    m_ClassPrefabData.m_ProductManagerData.RemoveAt(3);
                    m_ChoiceButton3.SetActive(false);
                    m_SelecteClassName3.SetActive(true);
                    m_ProfessorName3.SetActive(true);
                    m_SelecteClassName3.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteClassDataSave.m_ClassName;
                    m_ProfessorName3.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave.m_ProfessorNameValue;
                }
            }
            else if (m_ClassPrefabData.m_SelecteClassDataList[_tempIndex].m_ClassType == Type.Programming)
            {
                m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave = m_NowPlayerProfessor.ProgrammingProfessor[m_ProfessorDataIndex];
                m_ClassPrefabData.m_ProgrammingData.Add(m_ClassPrefabData.m_SaveData);

                if (m_ClassPrefabData.m_SaveData.m_ClickPointDataSave == "1Week_Button1")
                {
                    m_ClassPrefabData.ChangeListIndex(m_ClassPrefabData.m_ProgrammingData, 0, m_ClassPrefabData.m_SaveData);
                    m_ClassPrefabData.m_ProgrammingData.RemoveAt(3);
                    m_ChoiceButton1.SetActive(false);
                    m_SelecteClassName1.SetActive(true);
                    m_ProfessorName1.SetActive(true);
                    m_SelecteClassName1.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteClassDataSave.m_ClassName;
                    m_ProfessorName1.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave.m_ProfessorNameValue;
                }
                else if (m_ClassPrefabData.m_SaveData.m_ClickPointDataSave == "1Week_Button2")
                {
                    m_ClassPrefabData.ChangeListIndex(m_ClassPrefabData.m_ProgrammingData, 1, m_ClassPrefabData.m_SaveData);
                    m_ClassPrefabData.m_ProgrammingData.RemoveAt(3);
                    m_ChoiceButton2.SetActive(false);
                    m_SelecteClassName2.SetActive(true);
                    m_ProfessorName2.SetActive(true);
                    m_SelecteClassName2.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteClassDataSave.m_ClassName;
                    m_ProfessorName2.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave.m_ProfessorNameValue;

                }
                else if (m_ClassPrefabData.m_SaveData.m_ClickPointDataSave == "1Week_Button3")
                {
                    m_ClassPrefabData.ChangeListIndex(m_ClassPrefabData.m_ProgrammingData, 2, m_ClassPrefabData.m_SaveData);
                    m_ClassPrefabData.m_ProgrammingData.RemoveAt(3);
                    m_ChoiceButton3.SetActive(false);
                    m_SelecteClassName3.SetActive(true);
                    m_ProfessorName3.SetActive(true);
                    m_SelecteClassName3.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteClassDataSave.m_ClassName;
                    m_ProfessorName3.GetComponent<TextMeshProUGUI>().text = m_ClassPrefabData.m_SaveData.m_SelecteProfessorDataSave.m_ProfessorNameValue;
                }
            }
        }
    }
}
