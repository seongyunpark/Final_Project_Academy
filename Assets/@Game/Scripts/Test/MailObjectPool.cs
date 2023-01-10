using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailObjectPool : MonoBehaviour
{
    public static MailObjectPool Instance;

    [SerializeField] private GameObject m_PoolingMailPrefab;

    Queue<GameObject> m_PoolingMailQueue = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        Initalize(50);
    }
    
    void Initalize(int _count)
    {
        for(int i = 0; i < _count; i++)
        {
            m_PoolingMailQueue.Enqueue(CreateNewMail());
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
        if(Instance.m_PoolingMailQueue.Count > 0)
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
}
