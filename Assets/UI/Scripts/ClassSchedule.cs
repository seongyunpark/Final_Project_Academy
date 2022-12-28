using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    [SerializeField] private GameObject m_MonthClassSpace;
    [SerializeField] private ClassController m_LoadClassData;

    public EachClass m_NowPlayerClass = new EachClass();

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
    }

    // �� Ŭ������ ������ ���ϱ� ���� 
    public void SetEachOfClass()
    {
        // �� 3���� ������ �� ���ִ� â�� ������. ��� ������ ��ư�� �̸����� �����ϱ� �׷��� ������ ��� ���� �������� �˱� ����.
        GameObject gobj = EventSystem.current.currentSelectedGameObject;

        if (gobj.name == "ProductManagerC_Button")
        {
            Debug.Log("��ȹ��");

            //GameObject _button = GameObject.Instantiate(m_SelecteClassArea, m_MonthClassSpace.transform);
            m_SelecteClassArea1.name = gobj.name + "1";
            m_SelecteClassArea2.name = gobj.name + "2";
            m_SelecteClassArea3.name = gobj.name + "3";
        }

        if (gobj.name == "ArtC_Button")
        {
            Debug.Log("��Ʈ��");

            //GameObject _button = GameObject.Instantiate(m_SelecteClassArea, m_MonthClassSpace.transform);
            m_SelecteClassArea1.name = gobj.name + "1";
            m_SelecteClassArea2.name = gobj.name + "2";
            m_SelecteClassArea3.name = gobj.name + "3";
        }

        if (gobj.name == "ProgrammingC_Button")
        {
            Debug.Log("�ùֹ�");

            //GameObject.Instantiate(m_SelecteClassArea, m_MonthClassSpace.transform);
            m_SelecteClassArea1.name = gobj.name + "1";
            m_SelecteClassArea2.name = gobj.name + "2";
            m_SelecteClassArea3.name = gobj.name + "3";
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
