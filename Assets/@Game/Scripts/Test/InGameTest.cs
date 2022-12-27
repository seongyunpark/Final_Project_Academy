using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using StatData.Runtime;
using Conditiondata.Runtime;

/// <summary>
/// �ΰ��ӿ��� ���ư� �׽�Ʈ Ŭ����. ���� �Ŵ����� ���� ������ ��ũ��Ʈ�̴�.
/// ����� �̱������� ������� �л��� ������� ��ư�� ������ �׷��� �������
/// �л��� ������ ����Ʈȭ ���� �����ִ� ��ư�� �ִ�.
/// 
/// 2022.11.04
/// </summary>
public class InGameTest : MonoBehaviour
{
    private static InGameTest _instance = null;

    [SerializeField]
    GameObject _testStudentStat;
    [SerializeField]
    GameObject _studentStatBox;
    [SerializeField]
    Button _studentInfoButton;
    [SerializeField]
    Button _classInfoBox;
    [SerializeField]
    GameObject _testStudentInfo;
    [SerializeField]
    GameObject _testClassInfo;
    [SerializeField]
    GameObject _cancleClassInfoButton;
    [SerializeField]
    GameObject _startClassButton;
    [SerializeField]
    private ClassController _classInfo;
    [SerializeField]
    private StatController _statController;

    private List<GameObject> _studentInfoList = new List<GameObject>();
    private List<GameObject> _SelectStudentList = new List<GameObject>();
    private List<Button> _classInfoList = new List<Button>();

    private Dictionary<string, Class> _CheckClass = new Dictionary<string, Class>();
    private List<Student> _startClassStudent = new List<Student>();
    private List<StatModifier> _startClassMagnitude = new List<StatModifier>();

    public static InGameTest Instance
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

    // ��ư�� ������ �� ĳ���Ͱ� ������ �ǰ� �غ���
    public void ClickButtonTest()
    {
        ObjectManager.Instance.CreateStudent();
    }

    // ������� �л����� ������ UI�� ���� �������ִ� �Լ�
    public void CreateStudentInfo()
    {
        _testStudentStat.SetActive(true);
        _studentInfoButton.interactable = false;

        for (int i = 0; i < ObjectManager.Instance.m_StudentList.Count; i++)
        {
            var newStatsBox = Instantiate(_studentStatBox, new Vector3(0, 0, 0), Quaternion.identity);
            newStatsBox.transform.SetParent(GameObject.Find("Content").transform);
            _studentInfoList.Add(newStatsBox);
        }

        CheckStudentInfo();
    }

    // ���� �����ϰ��ִ� ������ ������ UI�� ���� �������ִ� �Լ�
    public void CreateClassInfo()
    {
        _testClassInfo.SetActive(true);
        _testStudentInfo.SetActive(true);
        _cancleClassInfoButton.SetActive(true);
        _startClassButton.SetActive(true);

        for (int i = 0; i < _classInfo.classData.Count; i++)
        {
            var newClassBox = Instantiate(_classInfoBox);
            newClassBox.transform.SetParent(GameObject.Find("ClassInfoContent").transform);
            newClassBox.onClick.AddListener(ClickClass);
            _classInfoList.Add(newClassBox);
        }

        ClickStudy();
    }

    // UI�� �л����� ������ ä���ִ� �Լ�
    public void CheckStudentInfo()
    {
        if (_studentInfoList.Count == 0)
        {
            return;
        }

        for (int i = 0; i < ObjectManager.Instance.m_StudentList.Count; i++)
        {
            StudentStat _stat = ObjectManager.Instance.m_StudentList[i].m_StudentData;
            StudentCondition _condition = ObjectManager.Instance.m_StudentList[i].m_StudentCondition;
            Student.Doing _doing = ObjectManager.Instance.m_StudentList[i].m_Doing;
            Type _type = ObjectManager.Instance.m_StudentList[i].m_StudentData.m_StudentType;

            _studentInfoList[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ObjectManager.Instance.m_StudentList[i].m_NameStudent;
            _studentInfoList[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = _stat.m_StudentSystemValue.ToString();
            _studentInfoList[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = _stat.m_StudentContentsValue.ToString();
            _studentInfoList[i].transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = _stat.m_StudentBalanceValue.ToString();
            _studentInfoList[i].transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = _condition.m_StudentHungryValue.ToString();
            _studentInfoList[i].transform.GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = _condition.m_StudentTiredValue.ToString();
            _studentInfoList[i].transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().text = _doing.ToString();
            _studentInfoList[i].transform.GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>().text = _type.ToString();
        }
    }

    // ������ �������� �� �ش��ϴ� Ÿ���� �л����� ��� �ҷ��ͼ� ȭ�鿡 ����ִ� �Լ�
    public void SelectClassAndStudent()
    {
        if (_SelectStudentList.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _SelectStudentList.Count; i++)
        {
            StudentStat _stat = _startClassStudent[i].m_StudentData;
            StudentCondition _condition = _startClassStudent[i].m_StudentCondition;
            Student.Doing _doing = _startClassStudent[i].m_Doing;
            Type _type = _startClassStudent[i].m_StudentData.m_StudentType;

            _SelectStudentList[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _startClassStudent[i].m_NameStudent;
            _SelectStudentList[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = _stat.m_StudentSystemValue.ToString();
            _SelectStudentList[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = _stat.m_StudentContentsValue.ToString();
            _SelectStudentList[i].transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = _stat.m_StudentBalanceValue.ToString();
            _SelectStudentList[i].transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = _condition.m_StudentHungryValue.ToString();
            _SelectStudentList[i].transform.GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = _condition.m_StudentTiredValue.ToString();
            _SelectStudentList[i].transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().text = _doing.ToString();
            _SelectStudentList[i].transform.GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>().text = _type.ToString();
        }
    }

    // �л����� ������ �� �� Cancle��ư�� ������ ����Ʈ�� ������ �����ִ� �Լ�
    public void ClickCancle()
    {
        _testStudentStat.SetActive(false);
        _studentInfoButton.interactable = true;

        for (int i = 0; i < _studentInfoList.Count; i++)
        {
            Destroy(_studentInfoList[i]);
        }
        _studentInfoList.Clear();
    }

    // ClassInfo��ư�� ������ �ش��ϴ� ������ �̸��� UI�� ����ִ� �Լ�
    public void ClickStudy()
    {
        if (_classInfoList.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _classInfo.classData.Count; i++)
        {
            Class _class = _classInfo.classData.ElementAt(i).Value;
            _CheckClass.Add(_class.m_ClassName, _class);
            _classInfoList[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _class.m_ClassName;
        }

    }

    // ������ Ŭ������ �� �Ӽ��� ���� �ش��ϴ� �л����� ����Ʈ�� �����ֱ� ���� �Լ�
    public void ClickClass()
    {
        GameObject _clickObject = EventSystem.current.currentSelectedGameObject; // ��� Ŭ���� UI�� ������ �̸��� �������� ���� �̺�Ʈ �Լ�

        Class _class = _CheckClass[_clickObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text];

        StatModifier statModifier;

        if (_SelectStudentList.Count != 0)
        {
            for (int i = 0; i < _SelectStudentList.Count; i++)
            {
                Destroy(_SelectStudentList[i]);
            }
            _SelectStudentList.Clear();
            _startClassMagnitude.Clear();
            _startClassStudent.Clear();
        }

        switch (_class.m_ClassType)
        {
            case Type.Art:
            {
                for (int i = 0; i < ObjectManager.Instance.m_StudentList.Count; i++)
                {
                    if (ObjectManager.Instance.m_StudentList[i].m_StudentData.m_StudentType == Type.Art)
                    {
                        var newStatsBox = Instantiate(_studentStatBox, new Vector3(0, 0, 0), Quaternion.identity);
                        newStatsBox.transform.SetParent(GameObject.Find("StudentContent").transform);
                        _SelectStudentList.Add(newStatsBox);
                        _startClassStudent.Add(ObjectManager.Instance.m_StudentList[i]);

                        statModifier = new StatModifier();

                        statModifier.StatsModifierInfo[ModifierStatType.System] = _CheckClass[_class.m_ClassName].m_ClassSystemValue;
                        statModifier.StatsModifierInfo[ModifierStatType.Contents] = _CheckClass[_class.m_ClassName].m_ClassContentsValue;
                        statModifier.StatsModifierInfo[ModifierStatType.Balance] = _CheckClass[_class.m_ClassName].m_ClassBalanceValue;

                        _startClassMagnitude.Add(statModifier);
                    }
                }
                SelectClassAndStudent();

                Debug.Log("��Ʈ");
            }
            break;

            case Type.ProductManager:
            {
                for (int i = 0; i < ObjectManager.Instance.m_StudentList.Count; i++)
                {
                    if (ObjectManager.Instance.m_StudentList[i].m_StudentData.m_StudentType == Type.ProductManager)
                    {
                        var newStatsBox = Instantiate(_studentStatBox, new Vector3(0, 0, 0), Quaternion.identity);
                        newStatsBox.transform.SetParent(GameObject.Find("StudentContent").transform);
                        _SelectStudentList.Add(newStatsBox);
                        _startClassStudent.Add(ObjectManager.Instance.m_StudentList[i]);

                        statModifier = new StatModifier();

                        statModifier.StatsModifierInfo[ModifierStatType.System] = _CheckClass[_class.m_ClassName].m_ClassSystemValue;
                        statModifier.StatsModifierInfo[ModifierStatType.Contents] = _CheckClass[_class.m_ClassName].m_ClassContentsValue;
                        statModifier.StatsModifierInfo[ModifierStatType.Balance] = _CheckClass[_class.m_ClassName].m_ClassBalanceValue;

                        _startClassMagnitude.Add(statModifier);
                    }
                }
                SelectClassAndStudent();

                Debug.Log("��ȹ");

            }
            break;

            case Type.Programming:
            {
                for (int i = 0; i < ObjectManager.Instance.m_StudentList.Count; i++)
                {
                    if (ObjectManager.Instance.m_StudentList[i].m_StudentData.m_StudentType == Type.Programming)
                    {
                        var newStatsBox = Instantiate(_studentStatBox, new Vector3(0, 0, 0), Quaternion.identity);
                        newStatsBox.transform.SetParent(GameObject.Find("StudentContent").transform);
                        _SelectStudentList.Add(newStatsBox);
                        _startClassStudent.Add(ObjectManager.Instance.m_StudentList[i]);

                        statModifier = new StatModifier();

                        statModifier.StatsModifierInfo[ModifierStatType.System] = _CheckClass[_class.m_ClassName].m_ClassSystemValue;
                        statModifier.StatsModifierInfo[ModifierStatType.Contents] = _CheckClass[_class.m_ClassName].m_ClassContentsValue;
                        statModifier.StatsModifierInfo[ModifierStatType.Balance] = _CheckClass[_class.m_ClassName].m_ClassBalanceValue;

                        _startClassMagnitude.Add(statModifier);
                    }
                }
                SelectClassAndStudent();

                Debug.Log("���α׷���");
            }
            break;
        }
    }

    // ��ư�� ������ ������ ���۵Ǹ鼭 �л����� ���°� ���������� �ٲ��.
    public void StarClass()
    {
        for (int i = 0; i < _startClassStudent.Count; i++)
        {
            _startClassStudent[i].m_Doing = Student.Doing.Study;
        }
        SelectClassAndStudent();
        StartCoroutine(EndClass());
    }

    // 3�� �� ������ ���ڸ�ŭ �л��� �ɷ�ġ�� �ö󰡰� ���ִ� �ڷ�ƾ
    IEnumerator EndClass()
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < _startClassStudent.Count; i++)
        {
            _startClassStudent[i].m_Doing = Student.Doing.FreeWalk;
            _statController.CalculateValue(_startClassStudent[i], _startClassMagnitude[i]);
        }

        SelectClassAndStudent();
    }
}

public class StudentStats
{
    public string name;
    public string system;
    public string contents;
    public string balance;
    public int hungry;
    public int tired;
}
