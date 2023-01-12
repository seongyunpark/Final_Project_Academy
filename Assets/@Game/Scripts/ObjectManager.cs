using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatData.Runtime;
using Conditiondata.Runtime;
using BT;
using BehaviorDesigner.Runtime;

/// <summary>
/// �л��� ������ ����, ������ �������ִ� ��ũ��Ʈ
/// 
/// ������ ���������� ���ҽ��μ� �����ϸ�, ����ֱ⸦ ���.
/// 
/// 2022. 10. 31 Ocean
/// </summary>
public class ObjectManager : MonoBehaviour
{
    private static ObjectManager _instance = null;

    [SerializeField] private StatController m_StatController;
    [SerializeField] private ConditionController m_conditionData;
    [SerializeField] private List<GameObject> m_CharacterPartsHair = new List<GameObject>();
    [SerializeField] private List<GameObject> m_CharacterPartsTop = new List<GameObject>();
    [SerializeField] private List<GameObject> m_CharacterPartsBottom = new List<GameObject>();
    [SerializeField] private ExternalBehaviorTree studentTree;

    public GameObject StudentOriginal;    /// �������� ����� ������(�� �޴� GameObject)

    public List<Student> m_StudentList = new List<Student>();


    public static ObjectManager Instance
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

    // �л� ������Ʈ�� �ʿ��� �� �������� �������ֱ� ���� �Լ�
    // ������ ���ڸ� �����ͼ� �����ͺ��̽��� �ִ� �л��� ������ �������� �����´�
    // �л��� ������ �� ������ ������Ʈ�� �����ϴ°� �ƴ϶� Mesh�� �ٲ��ִ°ɷ� ���ֱ�
    public void CreateStudent()
    {
        int _randomStudent = Random.Range(0, 5);

        /// S. Monobehavior�� ����� �༮���� new�� �ϸ� �ȵȴ�.
        // GameObject student = new GameObject();

        /// Instantiate�� �ϴ� ���
        /// (�纻���κ��� ������ �����.)
        /// 1. ���ҽ�ȭ �� �������� Ư�� ������Ʈ�� ����ְ�, �װ��� �������� ����Ѵ�.
        GameObject _newStudentObject = GameObject.Instantiate(StudentOriginal) as GameObject;   // �������� �Լ�
                                                                                                //GameObject _newStudentObjec2 = GameObject.Instantiate(StudentOriginal);                 // ���������� ���ӿ�����Ʈ�� ��
                                                                                                //GameObject _newStudentObject3 = GameObject.Instantiate<GameObject>(StudentOriginal);    // Ÿ���� Generic���� �������ִ� ����
        #region _�������� �������� ����� ��� 
        /// (�纻���κ��� ������ �����.)
        /// 2. ���ҽ�ȭ �� �������� ���ҽ� �����κ��� �ٷ� �ε��ؼ�, �װ��� �������� ����Ѵ�.
        //GameObject _newStudentObject = GameObject.Instantiate(Resources.Load("Student")) as GameObject;

        /// (�ƹ��͵� ���� ���¿���, �ϳ��ϳ� �� ����� ���� ��)
        /// 3. GameObject�� �ϳ� �����, ������Ʈ���� �ϳ��ϳ� ���� ���δ�.
        //GameObject _newStudentObject2 = new GameObject();
        //_newStudentObject2.AddComponent<Student>();
        //_newStudentObject2.AddComponent<MeshFilter>();
        //MeshFilter _newFilter = _newStudentObject2.GetComponent<MeshFilter>();
        //_newFilter.mesh = new Mesh();

        // (����) ������Ʈ�� �������� ���� ���
        //Student _student2 = StudentOriginal.GetComponent<Student>();
        //Student _newStudentComponent = GameObject.Instantiate(_student2);   // �������� �Լ�
        #endregion
        // ĳ���͸� ������ �� ���� ���� �������� ������ش�.
        int _hairNum = Random.Range(0, m_CharacterPartsHair.Count);
        int _topNum = Random.Range(0, m_CharacterPartsTop.Count);
        int _bottomNum = Random.Range(0, m_CharacterPartsBottom.Count);
        
        // �Ӹ�ī���� ���� ������ �� �θ� _newStudentObject�� �������ش�.
        GameObject.Instantiate(m_CharacterPartsHair[_hairNum], _newStudentObject.transform.GetChild(0).transform);
        GameObject.Instantiate(m_CharacterPartsTop[_topNum], _newStudentObject.transform.GetChild(0).transform);
        GameObject.Instantiate(m_CharacterPartsBottom[_bottomNum], _newStudentObject.transform.GetChild(0).transform);

        // ��ƼƼ�κ��� ��ũ��Ʈ�� ����
        Student _student = _newStudentObject.GetComponent<Student>();

        // �л��� BT������Ʈ�� �ٿ��ش�
        _newStudentObject.GetComponent<BehaviorTree>().StartWhenEnabled = true;
        _newStudentObject.GetComponent<BehaviorTree>().PauseWhenDisabled = true;
        _newStudentObject.GetComponent<BehaviorTree>().RestartWhenComplete = true;

        _newStudentObject.GetComponent<BehaviorTree>().DisableBehavior();

        ExternalBehavior studentTreeInstance = Instantiate(studentTree);
        studentTreeInstance.Init();

        // �л����� ������ �������ֱ�
        //studentTreeInstance.SetVariableValue();

        _newStudentObject.GetComponent<BehaviorTree>().ExternalBehavior = studentTreeInstance;
        _newStudentObject.GetComponent<BehaviorTree>().EnableBehavior();
        
        // �� ��ũ��Ʈ�κ��� �̷� ���� ó���� �Ѵ�.
        //Node _node = CreateNode(_student);

        StudentStat _stat = new StudentStat(m_StatController.dataBase.studentDatas[_randomStudent]);

        string _studentName = m_StatController.dataBase.studentDatas[_randomStudent].StudentName;

        StudentCondition _studentCondition = new StudentCondition(m_conditionData.dataBase.studentCondition[0]);

        _student.Initialize(_stat, _studentName, _studentCondition);

        // ���� ���� �л� ������Ʈ�� ��ġ�� 0���� ������.
        _student.transform.position = new Vector3(0, 0, 0);

        // ���̾��Ű �󿡼� �θ� �������ش�.
        _student.transform.parent = transform;

        // ������� ������Ʈ�� Ư�� Ǯ�� �ִ´�.
        m_StudentList.Add(_student);
    }

    // ���� ������Ʈ�� �ʿ��� �� �������� �������ֱ� ���� �Լ�
    public void CreateProfessor()
    {

    }

    public Node CreateNode(Student _student)
    {
        // �ֻ�� ���
        Selector _root = new Selector();

        // �ֻ�� ��忡 �� Composite����
        Sequnce _studySeq = new Sequnce();
        Sequnce _goSeq = new Sequnce();
        Sequnce _eatSeq = new Sequnce();
        Sequnce _restSeq = new Sequnce();
        Sequnce _idelSeq = new Sequnce();
        //Sequnce _freeWalkSeq = new Sequnce();

        // �л����� �ൿ�� ����� ����
        Studing _studingLeaf = new Studing();
        _studingLeaf.m_MyStudent = _student;

        GotoRestaurant _gotoRestourantLeaf = new GotoRestaurant();
        _gotoRestourantLeaf.m_MyStudent = _student;

        Eat _eatLeaf = new Eat();
        _eatLeaf.m_MyStudent = _student;

        Rest _restLeaf = new Rest();
        _restLeaf.m_MyStudent = _student;


        Idel _idelLeaf = new Idel();
        _idelLeaf.m_MyStudent = _student;

        //FreeWalk _freeWalkLeaf = new FreeWalk();
        //_freeWalkLeaf.m_MyStudent = _student;

        _studySeq.AddChildNode(_studingLeaf);

        _goSeq.AddChildNode(_gotoRestourantLeaf);

        _eatSeq.AddChildNode(_eatLeaf);

        _restSeq.AddChildNode(_restLeaf);

        _idelSeq.AddChildNode(_idelLeaf);
        //_freeWalkSeq.AddChildNode(_freeWalkLeaf);

        _root.AddChildNode(_studySeq);
        _root.AddChildNode(_goSeq);
        _root.AddChildNode(_eatSeq);
        _root.AddChildNode(_restSeq);
        _root.AddChildNode(_idelSeq);
        //_root.AddChildNode(_freeWalkSeq);

        return _root;
    }
}
