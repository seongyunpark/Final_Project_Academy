using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// Mang 2022.10.12
/// 
/// ���� UI ��ũ��Ʈ���� ���� �� UI �Ŵ���
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager m_Instance = null;       // Manager ������ �̱������� ���

    // �̱������� ���� ������ �����ϰ� �����ϰ� ����ϱ� ���� �ʱ�ȭ(?) ���
    private void Awake()
    {
        // Instance �� �����Ѵٸ� gameObject ����
        if (m_Instance != null)
        {
            return;
        }

        // ���۵� �� �ν��Ͻ� �ʱ�ȭ, ���� �Ѿ���� �����Ǳ� ���� ó��
        m_Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    //public void PopUpUI()
    //{
    //    if (Input.GetKeyDown("1"))
    //    {

    //    }
    //}
}
