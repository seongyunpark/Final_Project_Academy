using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;
using StatData.Runtime;

public struct SaveClassAndProfesssorData
{
    public Class m_SelecteClassDataSave;
    public ProfessorStat m_SelecteProfessorDataSave;
    public string m_ClickPointDataSave;
    public string m_ClassName;
};

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

    //[SerializeField] private TextMeshProUGUI m_SelecteClassName1;
    //[SerializeField] private TextMeshProUGUI m_SelecteClassName2;
    //[SerializeField] private TextMeshProUGUI m_SelecteClassName3;

    //bool isCheck = true;

    // ������� �������� �����ϱ� ���� ����Ʈ
    public List<GameObject> m_ClassList = new List<GameObject>();
    public List<Class> m_SelecteClassDataList = new List<Class>(); // ��� ������ ������ ������ �����Ϸ��� ���� ����Ʈ
    public List<string> m_SelecteClassButtonName = new List<string>(); // ���� Ŭ���� ��ư�� ���������� ������ ������ �־��ֱ� ���� ���� ����Ʈ

    public SaveClassAndProfesssorData m_SaveData = new SaveClassAndProfesssorData();

    public List<SaveClassAndProfesssorData> m_ProductManagerData = new List<SaveClassAndProfesssorData>();
    public List<SaveClassAndProfesssorData> m_ArtData = new List<SaveClassAndProfesssorData>();
    public List<SaveClassAndProfesssorData> m_ProgrammingData = new List<SaveClassAndProfesssorData>();

    #region _����ü ������ ����Ʈ �ε����� �ٲٱ� ���� �Լ�
    public void ChangeListIndex(List<SaveClassAndProfesssorData> _tempList, int _index, SaveClassAndProfesssorData _saveData)
    {
        SaveClassAndProfesssorData _temp = _tempList[_index];
        _temp = _saveData;
        _tempList[_index] = _temp;
    }
    #endregion

    private void Start()
    {
        m_ProductManagerData.Capacity = 3;
        m_ArtData.Capacity = 3;
        m_ProgrammingData.Capacity = 3;

        for (int i = 0; i < 3; i++)
        {
            m_ProductManagerData.Add(new SaveClassAndProfesssorData());
            m_ArtData.Add(new SaveClassAndProfesssorData());
            m_ProgrammingData.Add(new SaveClassAndProfesssorData());
        }
    }

    public void MakeClass()
    {
        if (m_ClassList.Count != 0)
        {
            for (int i = 0; i < m_ClassList.Count; i++)
            {
                Destroy(m_ClassList[i]);
            }
            m_ClassList.Clear();
            //m_SelecteClassDataList.Clear();
        }

        if (m_Month1.name == "ProductManagerC_Button1" || m_Month2.name == "ProductManagerC_Button2" || m_Month3.name == "ProductManagerC_Button3")
        {
            for (int i = 0; i < ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass.Count; i++)
            {
                GameObject _nowObj = EventSystem.current.currentSelectedGameObject;
                GameObject _classClick1 = GameObject.Instantiate(m_Prefab, m_parent);

                if (_nowObj.name == "1Week_Button1" && m_SelecteClassButtonName.Count == 0)
                {
                    m_SelecteClassButtonName.Add(_nowObj.name);
                    m_SaveData.m_ClickPointDataSave = _nowObj.name;
                }
                else if (_nowObj.name == "1Week_Button2" && m_SelecteClassButtonName.Count == 0)
                {
                    m_SelecteClassButtonName.Add(_nowObj.name);
                    m_SaveData.m_ClickPointDataSave = _nowObj.name;
                }
                else if (_nowObj.name == "1Week_Button3" && m_SelecteClassButtonName.Count == 0)
                {
                    m_SelecteClassButtonName.Add(_nowObj.name);
                    m_SaveData.m_ClickPointDataSave = _nowObj.name;
                }

                _classClick1.name = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassName;

                m_ClassList.Add(_classClick1);

                _classClick1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassName;
                _classClick1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassSystemValue.ToString();
                _classClick1.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassContentsValue.ToString();
                _classClick1.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProductManagerClass[i].m_ClassBalanceValue.ToString();

                _classClick1.GetComponent<Button>().onClick.AddListener(SelecteClassInfo);
            }
        }

        if (m_Month1.name == "ArtC_Button1" || m_Month2.name == "ArtC_Button2" || m_Month3.name == "ArtC_Button3")
        {
            for (int i = 0; i < ClassSchedule.Instance.m_NowPlayerClass.ArtClass.Count; i++)
            {
                GameObject _nowObj = EventSystem.current.currentSelectedGameObject;
                GameObject _classClick2 = GameObject.Instantiate(m_Prefab, m_parent);

                if (_nowObj.name == "1Week_Button1" && m_SelecteClassButtonName.Count == 0)
                {
                    m_SelecteClassButtonName.Add(_nowObj.name);
                    m_SaveData.m_ClickPointDataSave = _nowObj.name;
                }
                else if (_nowObj.name == "1Week_Button2" && m_SelecteClassButtonName.Count == 0)
                {
                    m_SelecteClassButtonName.Add(_nowObj.name);
                    m_SaveData.m_ClickPointDataSave = _nowObj.name;
                }
                else if (_nowObj.name == "1Week_Button3" && m_SelecteClassButtonName.Count == 0)
                {
                    m_SelecteClassButtonName.Add(_nowObj.name);
                    m_SaveData.m_ClickPointDataSave = _nowObj.name;
                }

                _classClick2.name = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassName;

                m_ClassList.Add(_classClick2);

                _classClick2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassName;
                _classClick2.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassSystemValue.ToString();
                _classClick2.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassContentsValue.ToString();
                _classClick2.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ArtClass[i].m_ClassBalanceValue.ToString();

                _classClick2.GetComponent<Button>().onClick.AddListener(SelecteClassInfo);
            }
        }

        if (m_Month1.name == "ProgrammingC_Button1" || m_Month2.name == "ProgrammingC_Button2" || m_Month3.name == "ProgrammingC_Button3")
        {
            for (int i = 0; i < ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass.Count; i++)
            {
                GameObject _nowObj = EventSystem.current.currentSelectedGameObject;
                GameObject _classClick3 = GameObject.Instantiate(m_Prefab, m_parent);

                if (_nowObj.name == "1Week_Button1" && m_SelecteClassButtonName.Count == 0)
                {
                    m_SelecteClassButtonName.Add(_nowObj.name);
                    m_SaveData.m_ClickPointDataSave = _nowObj.name;
                }
                else if (_nowObj.name == "1Week_Button2" && m_SelecteClassButtonName.Count == 0)
                {
                    m_SelecteClassButtonName.Add(_nowObj.name);
                    m_SaveData.m_ClickPointDataSave = _nowObj.name;
                }
                else if (_nowObj.name == "1Week_Button3" && m_SelecteClassButtonName.Count == 0)
                {
                    m_SelecteClassButtonName.Add(_nowObj.name);
                    m_SaveData.m_ClickPointDataSave = _nowObj.name;
                }

                _classClick3.name = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassName;

                m_ClassList.Add(_classClick3);

                _classClick3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassName;
                _classClick3.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassSystemValue.ToString();
                _classClick3.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassContentsValue.ToString();
                _classClick3.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassSchedule.Instance.m_NowPlayerClass.ProgrammingClass[i].m_ClassBalanceValue.ToString();

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

                if (m_SelecteClassDataList.Contains(m_SelecteClass.classData.ElementAt(i).Value) == false)
                {
                    m_SelecteClassDataList.Add(m_SelecteClass.classData.ElementAt(i).Value);
                }
            }
        }
    }

    public void SaveClassData()
    {
        int _index = 0;

        if (m_SelecteClassDataList.Count > 0)
        {
            _index = m_SelecteClassDataList.Count - 1;
        }
        else
        {
            _index = m_SelecteClassDataList.Count;
        }

        m_SaveData.m_SelecteClassDataSave = m_SelecteClassDataList[_index];
    }
}
