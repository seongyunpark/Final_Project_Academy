using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailObjectPool : MonoBehaviour
{
    public static MailObjectPool Instance;

    [SerializeField] private GameObject m_PoolingMailPrefab;

    Queue<GameObject> m_PoolingMailQueue = new Queue<GameObject>();

    [SerializeField] private GameObject m_PoolingPossibleEventPrefab;               // �������� ����� ���� public ������Ʈ
    [SerializeField] private GameObject m_PoolingFixedEventPrefab;
    [SerializeField] private GameObject m_PoolingSelectedEventPrefab;

    Queue<GameObject> m_PoolingPossibleEventQueue = new Queue<GameObject>();        // ��밡�� �̺�Ʈ��� - UI ���� ������ƮǮ ť
    Queue<GameObject> m_PoolingFixedEventQueue = new Queue<GameObject>();           // ���õ� �̺�Ʈ���
    Queue<GameObject> m_PoolingSelectedEventQueue = new Queue<GameObject>();           // ������ �̺�Ʈ���

    // ���� ���ɼ� ����
    const int MaxPossibleEventCount = 10;
    const int MaxSelectedEventCount = 2;
    const int MaxFixedEventCount = 3;

    private void Awake()
    {
        Instance = this;
        Initalize(50);
    }

    void Initalize(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            m_PoolingMailQueue.Enqueue(CreateNewMail());
        }

        // �̺�Ʈ ���� �����յ��� ������ش�
        for (int i = 0; i < MaxPossibleEventCount; i++)
        {
            m_PoolingPossibleEventQueue.Enqueue(CreatePossibleEvents());
        }
        for (int i = 0; i < MaxFixedEventCount; i++)
        {
            m_PoolingFixedEventQueue.Enqueue(CreateFixedEvents());
        }
        for (int i = 0; i < MaxSelectedEventCount; i++)
        {
            m_PoolingSelectedEventQueue.Enqueue(CreateSelectedEvents());

        }
    }

    private GameObject CreateNewMail()
    {
        GameObject _newMail = Instantiate(m_PoolingMailPrefab);
        _newMail.gameObject.SetActive(false);
        _newMail.transform.SetParent(transform);

        return _newMail;
    }

    public static GameObject GetObject(Transform _setTransform)
    {
        if (Instance.m_PoolingMailQueue.Count > 0)
        {
            var _obj = Instance.m_PoolingMailQueue.Dequeue();
            _obj.transform.SetParent(_setTransform);
            _obj.gameObject.SetActive(true);
            return _obj;
        }
        else
        {
            var _newObj = Instance.CreateNewMail();
            _newObj.gameObject.SetActive(true);
            _newObj.transform.SetParent(_setTransform);
            return _newObj;
        }
    }

    public static void ReturnObject(GameObject _mail)
    {
        _mail.gameObject.SetActive(false);
        _mail.transform.SetParent(Instance.transform);
        Instance.m_PoolingMailQueue.Enqueue(_mail);
    }

    // �� �̺�Ʈ �����յ��� ������ִ� �Լ���
    private GameObject CreatePossibleEvents()
    {
        GameObject _PossibleEvent = Instantiate(m_PoolingPossibleEventPrefab);
        _PossibleEvent.gameObject.SetActive(false);
        _PossibleEvent.transform.SetParent(transform);

        return _PossibleEvent;
    }

    private GameObject CreateFixedEvents()
    {
        GameObject _FixedEvnets = Instantiate(m_PoolingFixedEventPrefab);
        _FixedEvnets.gameObject.SetActive(false);
        _FixedEvnets.transform.SetParent(transform);

        return _FixedEvnets;
    }

    private GameObject CreateSelectedEvents()
    {
        GameObject _SelectedEvents = Instantiate(m_PoolingSelectedEventPrefab);
        _SelectedEvents.gameObject.SetActive(false);
        _SelectedEvents.transform.SetParent(transform);

        return _SelectedEvents;
    }

    // ������Ʈ Ǯ ���� ������Ʈ�� Dequeue �ؼ� ���ϴ� ���� �������ֱ�
    public static GameObject GetPossibleEventObject(Transform _setTransform)
    {
        if (Instance.m_PoolingPossibleEventQueue.Count > 0)
        {
            var _obj = Instance.m_PoolingPossibleEventQueue.Dequeue();
            _obj.transform.SetParent(_setTransform);
            _obj.gameObject.SetActive(true);
            return _obj;
        }
        else
        {
            var _newObj = Instance.CreatePossibleEvents();
            _newObj.gameObject.SetActive(true);
            _newObj.transform.SetParent(_setTransform);
            return _newObj;
        }
    }

    public static GameObject GetFixedEventObject(Transform _setTransform)
    {
        if (Instance.m_PoolingFixedEventQueue.Count > 0)
        {
            var _obj = Instance.m_PoolingFixedEventQueue.Dequeue();
            _obj.transform.SetParent(_setTransform);
            _obj.gameObject.SetActive(true);
            return _obj;
        }
        else
        {
            var _newObj = Instance.CreateFixedEvents();
            _newObj.gameObject.SetActive(true);
            _newObj.transform.SetParent(_setTransform);
            return _newObj;
        }
    }

    public static GameObject GetSelectedEventObject(Transform _setTransform)
    {
        if (Instance.m_PoolingSelectedEventQueue.Count > 0)
        {
            var _obj = Instance.m_PoolingSelectedEventQueue.Dequeue();
            _obj.transform.SetParent(_setTransform);
            _obj.gameObject.SetActive(true);
            return _obj;
        }
        else
        {
            var _newObj = Instance.CreateSelectedEvents();
            _newObj.gameObject.SetActive(true);
            _newObj.transform.SetParent(_setTransform);
            return _newObj;
        }
    }

    // Dequeue �� ������Ʈ�� �ٽ� ������Ʈ Ǯ�� �־��ֱ�
    public static void ReturnPossibleEventObject(GameObject possibleEvent)
    {
        possibleEvent.gameObject.SetActive(false);
        possibleEvent.transform.SetParent(Instance.transform);
        Instance.m_PoolingPossibleEventQueue.Enqueue(possibleEvent);
    }

    public static void ReturnFixedEventObject(GameObject fixedEvent)
    {
        // if (fixedEvent.transform.childCount != 0)
        // {
        // }
            fixedEvent.gameObject.SetActive(false);
            fixedEvent.transform.SetParent(Instance.transform);
            Instance.m_PoolingFixedEventQueue.Enqueue(fixedEvent);
    }

    public static void ReturnSelectedEventObject(GameObject selectedEvent)
    {
        selectedEvent.gameObject.SetActive(false);
        selectedEvent.transform.SetParent(Instance.transform);
        Instance.m_PoolingSelectedEventQueue.Enqueue(selectedEvent);
    }
}
