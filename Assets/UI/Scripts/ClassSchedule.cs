using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StatData.Runtime;

/// <summary>
/// Mang 2022. 11. 21
/// Ocean 2022. 12. 22
/// 
/// Mang_�÷��̾ ������ ��ȹ, ��Ʈ, �ù� �� �� ���� ���� ��
/// Ocean_������ Ÿ�Կ� ���� ������ �� �� �ְ� ���ֱ�
/// </summary>

public class EachClass
{
    // �� Ŭ������ ���� �������� ������ ������
    private List<Class> m_ProductManagerClass = new List<Class>();         // ��ȹ
    private List<Class> m_ArtClass = new List<Class>();           // ��Ʈ
    private List<Class> m_ProgrammingClass = new List<Class>();   // �ø�

    // private List<Class> m_Design = new List<Class>();        // �������� �����Ϳ� ���� �ϸ� �� ����

    #region EachClass_Property
    public List<Class> ProductManagerClass
    {
        get { return m_ProductManagerClass; }
        set { m_ProductManagerClass = value; }
    }

    public List<Class> ArtClass
    {
        get { return m_ArtClass; }
        set { m_ArtClass = value; }
    }
    public List<Class> ProgrammingClass
    {
        get { return m_ProgrammingClass; }
        set { m_ProgrammingClass = value; }
    }
    #endregion
}


public class ClassSchedule : MonoBehaviour
{
    private static ClassSchedule _instance = null;

    [SerializeField] private GameObject m_SelecteClassArea1;
    [SerializeField] private GameObject m_SelecteClassArea2;
    [SerializeField] private GameObject m_SelecteClassArea3;

    [SerializeField] private ClassController m_LoadClassData;
    [SerializeField] private ClassPrefab m_ClassPrefab;
    [SerializeField] private TextMeshProUGUI m_ClickClass;
    [SerializeField] private TextMeshProUGUI m_RegularClass;
    [SerializeField] private SelecteProfessor m_ChangeData;
    [SerializeField] private Button m_NormalSetting;            // ���� ����â�� ������ ��ȹ�ݺ����ΰ� �����ֱ�����

    private List<GameObject> m_ManagementSelecteClassName = new List<GameObject>(); // ���� �̸� �����ϴ� ����Ʈ
    private List<GameObject> m_ManagementChoiceButton = new List<GameObject>(); // ������ ������ ������ư ���ִ� ����Ʈ
    private List<GameObject> m_ManagementChoiceProfessorName = new List<GameObject>(); // ������ ������ ������ư ���ִ� ����Ʈ

    public EachClass m_NowPlayerClass = new EachClass();
    //public List<string> m_

    public static ClassSchedule Instance
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

    void Start()
    {
        // ���� ������ �ִ� �������� ����� �� ��Ʈ���� ����Ʈ�� �־��ش�.
        for (int i = 0; i < m_LoadClassData.classData.Count; i++)
        {
            if (m_LoadClassData.classData.ElementAt(i).Value.m_ClassType == Type.Art)
            {
                m_NowPlayerClass.ArtClass.Add(m_LoadClassData.classData.ElementAt(i).Value);
                Debug.Log("��Ʈ����");
                Debug.Log(m_LoadClassData.classData.ElementAt(i).Value.m_ClassName);
            }

            if (m_LoadClassData.classData.ElementAt(i).Value.m_ClassType == Type.ProductManager)
            {
                m_NowPlayerClass.ProductManagerClass.Add(m_LoadClassData.classData.ElementAt(i).Value);
                Debug.Log("��ȹ����");
                Debug.Log(m_LoadClassData.classData.ElementAt(i).Value.m_ClassName);
            }

            if (m_LoadClassData.classData.ElementAt(i).Value.m_ClassType == Type.Programming)
            {
                m_NowPlayerClass.ProgrammingClass.Add(m_LoadClassData.classData.ElementAt(i).Value);
                Debug.Log("�ùּ���");
                Debug.Log(m_LoadClassData.classData.ElementAt(i).Value.m_ClassName);
            }
        }

        m_ManagementSelecteClassName.Add(m_ChangeData.m_SelecteClassName1);
        m_ManagementSelecteClassName.Add(m_ChangeData.m_SelecteClassName2);
        m_ManagementSelecteClassName.Add(m_ChangeData.m_SelecteClassName3);

        m_ManagementChoiceButton.Add(m_ChangeData.m_ChoiceButton1);
        m_ManagementChoiceButton.Add(m_ChangeData.m_ChoiceButton2);
        m_ManagementChoiceButton.Add(m_ChangeData.m_ChoiceButton3);

        m_ManagementChoiceProfessorName.Add(m_ChangeData.m_ProfessorName1);
        m_ManagementChoiceProfessorName.Add(m_ChangeData.m_ProfessorName2);
        m_ManagementChoiceProfessorName.Add(m_ChangeData.m_ProfessorName3);

        m_ClassPrefab.m_SaveData.m_ClassName = "ProductManager";    // ó�� ���� ���ÿ� ���� ������ ��ȹ�����̴� ������ ���ش�.
    }

    // ���ο� ������ �������ֱ� ���� ����â�� �ʱ�ȭ ������ �Լ�
    public void ClearClassPanel()
    {
        m_NormalSetting.Select();

        m_ClickClass.text = "[��ȹ]�� �������� �����ּ���.";

        for (int i = 0; i < 3; i++)
        {
            m_ManagementSelecteClassName[i].SetActive(false);
            m_ManagementChoiceButton[i].SetActive(true);
            m_ManagementChoiceProfessorName[i].SetActive(false);

            m_ClassPrefab.m_ArtData[i] = new SaveClassAndProfesssorData();
            m_ClassPrefab.m_ProductManagerData[i] = new SaveClassAndProfesssorData();
            m_ClassPrefab.m_ProgrammingData[i] = new SaveClassAndProfesssorData();
        }

        m_SelecteClassArea1.name = "ProductManagerC_Button" + "1";
        m_SelecteClassArea2.name = "ProductManagerC_Button" + "2";
        m_SelecteClassArea3.name = "ProductManagerC_Button" + "3";
    }

    private void ChangeColorButton()
    {
        ColorBlock colorBlock = m_NormalSetting.colors;

        colorBlock.normalColor = new Color(1f, 0f, 0f, 1f);
        m_NormalSetting.colors = colorBlock;
    }

    // �� Ŭ������ ������ ���ϱ� ���� 
    public void SetEachOfClass()
    {
        // �� 3���� ������ �� ���ִ� â�� ������. ��� ������ ��ư�� �̸����� �����ϱ� �׷��� ������ ��� ���� �������� �˱� ����.
        GameObject gobj = EventSystem.current.currentSelectedGameObject;

        // �ϴ��� ���� �� ����ش�.
        for (int i = 0; i < 3; i++)
        {
            m_ManagementSelecteClassName[i].SetActive(false);
            m_ManagementChoiceButton[i].SetActive(true);
            m_ManagementChoiceProfessorName[i].SetActive(false);
        }

        if (gobj.name == "ProductManagerC_Button")
        {
            Debug.Log("��ȹ��");
            m_ClickClass.text = "[��ȹ]�� �������� �����ּ���.";
            m_RegularClass.text = "��ȹ ���Լ���";
            m_SelecteClassArea1.name = gobj.name + "1";
            m_SelecteClassArea2.name = gobj.name + "2";
            m_SelecteClassArea3.name = gobj.name + "3";

            // �� �������� ������ �Ϸ����� �� �����ߴ� �������� ������ m_ProductManagerData����Ʈ�� ����ִ��� Ȯ���ؼ� ������� �ʴٸ� �ش� ������ �����ͼ� �־��ش�.
            for (int i = 0; i < 3; i++)
            {
                if (m_ClassPrefab.m_ProductManagerData[i].m_ClassName != null)
                {
                    m_ManagementSelecteClassName[i].SetActive(true);
                    m_ManagementChoiceButton[i].SetActive(false);
                    m_ManagementChoiceProfessorName[i].SetActive(true);
                    m_ManagementSelecteClassName[i].GetComponent<TextMeshProUGUI>().text = m_ClassPrefab.m_ProductManagerData[i].m_SelecteClassDataSave.m_ClassName;
                    m_ManagementChoiceProfessorName[i].GetComponent<TextMeshProUGUI>().text = m_ClassPrefab.m_ProductManagerData[i].m_SelecteProfessorDataSave.m_ProfessorNameValue;
                }
            }
            m_ClassPrefab.m_SaveData.m_ClassName = "ProductManager";
        }

        if (gobj.name == "ArtC_Button")
        {
            Debug.Log("��Ʈ��");
            m_ClickClass.text = "[��Ʈ]�� �������� �����ּ���.";
            m_RegularClass.text = "��Ʈ ���Լ���";
            //GameObject _button = GameObject.Instantiate(m_SelecteClassArea, m_MonthClassSpace.transform);
            m_SelecteClassArea1.name = gobj.name + "1";
            m_SelecteClassArea2.name = gobj.name + "2";
            m_SelecteClassArea3.name = gobj.name + "3";

            for (int i = 0; i < 3; i++)
            {
                if (m_ClassPrefab.m_ArtData[i].m_ClassName != null)
                {
                    m_ManagementSelecteClassName[i].SetActive(true);
                    m_ManagementChoiceButton[i].SetActive(false);
                    m_ManagementChoiceProfessorName[i].SetActive(true);
                    m_ManagementSelecteClassName[i].GetComponent<TextMeshProUGUI>().text = m_ClassPrefab.m_ArtData[i].m_SelecteClassDataSave.m_ClassName;
                    m_ManagementChoiceProfessorName[i].GetComponent<TextMeshProUGUI>().text = m_ClassPrefab.m_ArtData[i].m_SelecteProfessorDataSave.m_ProfessorNameValue;
                }
            }

            m_ClassPrefab.m_SaveData.m_ClassName = "Art";
        }

        if (gobj.name == "ProgrammingC_Button")
        {
            Debug.Log("�ùֹ�");
            m_ClickClass.text = "[���α׷���]�� �������� �����ּ���.";
            m_RegularClass.text = "���α׷��� ���Լ���";
            //GameObject.Instantiate(m_SelecteClassArea, m_MonthClassSpace.transform);
            m_SelecteClassArea1.name = gobj.name + "1";
            m_SelecteClassArea2.name = gobj.name + "2";
            m_SelecteClassArea3.name = gobj.name + "3";

            for (int i = 0; i < 3; i++)
            {
                if (m_ClassPrefab.m_ProgrammingData[i].m_ClassName != null)
                {
                    m_ManagementSelecteClassName[i].SetActive(true);
                    m_ManagementChoiceButton[i].SetActive(false);
                    m_ManagementChoiceProfessorName[i].SetActive(true);
                    m_ManagementSelecteClassName[i].GetComponent<TextMeshProUGUI>().text = m_ClassPrefab.m_ProgrammingData[i].m_SelecteClassDataSave.m_ClassName;
                    m_ManagementChoiceProfessorName[i].GetComponent<TextMeshProUGUI>().text = m_ClassPrefab.m_ProgrammingData[i].m_SelecteProfessorDataSave.m_ProfessorNameValue;
                }
            }

            m_ClassPrefab.m_SaveData.m_ClassName = "Programming";
        }

    }

    //public void ChangeColor()
    //{
    //    if (this.gameObject.name == "ProductManagerC_Button")
    //    {
    //        Debug.Log("��ȹ��");

    //    }
    //    if (this.gameObject.name == "ArtC_Button")
    //    {
    //        Debug.Log("��Ʈ��");

    //    }
    //    if (this.gameObject.name == "ProgrammingC_Button")
    //    {
    //        Debug.Log("�ùֹ�");


    //    }
    //}
}
