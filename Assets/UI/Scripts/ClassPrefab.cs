using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;
using StatData.Runtime;

/// <summary>
/// ������ �������� ��ũ��Ʈ
/// </summary>
public class ClassPrefab : MonoBehaviour
{
    public GameObject m_Prefab;
    public Transform m_parent;

    [SerializeField] private ClassController m_SelecteClass;
    [SerializeField] private GameObject m_Month1;
    [SerializeField] private GameObject m_Month2;
    [SerializeField] private GameObject m_Month3;
    [SerializeField] private GameObject m_ClassInfo;

    //bool isCheck = true;

    // ������� �������� �����ϱ� ���� ����Ʈ
    public List<GameObject> m_ClassList = new List<GameObject>();
    public List<Class> m_SelecteClassDataList = new List<Class>(); // ��� ������ ������ ������ �����Ϸ��� ���� ����Ʈ

    public void MakeClass()
    {
        if (m_ClassList.Count != 0)
        {
            for (int i = 0; i < m_ClassList.Count; i++)
            {
                Destroy(m_ClassList[i]);
            }
            m_ClassList.Clear();
            m_SelecteClassDataList.Clear();
        }

        if (m_Month1.name == "ProductManagerC_Button" || m_Month2.name == "ProductManagerC_Button" || m_Month3.name == "ProductManagerC_Button")
        {
            for (int i = 0; i < ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass.Count; i++)
            {
                GameObject _classClick1 = GameObject.Instantiate(m_Prefab, m_parent);
                _classClick1.name = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassName;
                m_ClassList.Add(_classClick1);
                _classClick1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassName;
                //_classClick1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.dataBase.classDatas[i].ClassName;
                _classClick1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassSystemValue.ToString();
                _classClick1.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassContentsValue.ToString();
                _classClick1.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassBalanceValue.ToString();
                //_classClick1.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.dataBase.classDatas[i].ClassName;

                _classClick1.GetComponent<Button>().onClick.AddListener(SelecteClassInfo);
            }
        }

        if (m_Month1.name == "ArtC_Button" || m_Month2.name == "ArtC_Button" || m_Month3.name == "ArtC_Button")
        {
            for (int i = 0; i < ClassSchedule.Instance.m_NowPlayerClass.ArtClass.Count; i++)
            {
                GameObject _classClick2 = GameObject.Instantiate(m_Prefab, m_parent);
                _classClick2.name = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassName;
                m_ClassList.Add(_classClick2);
                _classClick2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassName;
                //_classClick2.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.dataBase.classDatas[i].ClassName;
                _classClick2.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassSystemValue.ToString();
                _classClick2.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassContentsValue.ToString();
                _classClick2.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassBalanceValue.ToString();
                //_classClick2.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.dataBase.classDatas[i].ClassName;

                _classClick2.GetComponent<Button>().onClick.AddListener(SelecteClassInfo);
            }
        }

        if (m_Month1.name == "ProgrammingC_Button" || m_Month2.name == "ProgrammingC_Button" || m_Month3.name == "ProgrammingC_Button")
        {
            for (int i = 0; i < ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass.Count; i++)
            {
                GameObject _classClick3 = GameObject.Instantiate(m_Prefab, m_parent);
                _classClick3.name = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassName;
                m_ClassList.Add(_classClick3);
                _classClick3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassName;
                //_classClick3.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.dataBase.classDatas[i].ClassName;
                _classClick3.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassSystemValue.ToString();
                _classClick3.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassContentsValue.ToString();
                _classClick3.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassBalanceValue.ToString();
                //_classClick3.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.dataBase.classDatas[i].ClassName;

                _classClick3.GetComponent<Button>().onClick.AddListener(SelecteClassInfo);
            }
        }

    }

    // ������� ������ �������� �� ���� ������ �����ֱ� ���� �Լ�
    private void SelecteClassInfo()
    {
        m_ClassInfo.SetActive(true);

        GameObject _classDataObj = GameObject.Find("ClassInfoPanel");

        GameObject _nowObj = EventSystem.current.currentSelectedGameObject;

        for (int i = 0; i < m_SelecteClass.classData.Count; i++)
        {
            // ������ Ŭ������ �� ���� Ŭ���� ������ �����ϱ� ���� ����Ʈ�� �־����.

            // Ŭ���� �����̶� ���� �̸��� ���� ������ ����ֱ� ���� ���̴�.
            if (_nowObj.name == m_SelecteClass.classData.ElementAt(i).Value.m_ClassName)
            {
                _classDataObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.classData.ElementAt(i).Value.m_ClassName;
                //_classInfoObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.classData.ElementAt(i).Value.m_ClassName;
                _classDataObj.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "System";
                _classDataObj.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.classData.ElementAt(i).Value.m_ClassSystemValue.ToString();
                _classDataObj.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Contents";
                _classDataObj.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.classData.ElementAt(i).Value.m_ClassContentsValue.ToString();
                _classDataObj.transform.GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Balance";
                _classDataObj.transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = m_SelecteClass.classData.ElementAt(i).Value.m_ClassBalanceValue.ToString();
                m_SelecteClassDataList.Add(m_SelecteClass.classData.ElementAt(i).Value);
            }

        }
    }
}