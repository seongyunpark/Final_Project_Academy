using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mang 2022. 10. 14
/// 
/// �� ó�� ���� ȭ�� -> Ŭ�� �� �α��� �˾� â�� ��� �� Ŭ����
/// </summary>
public class StartTitle : MonoBehaviour
{
    public GameObject PressScreenButton;        // ȭ���� �����ּ��� ��ư

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ȭ���� ������ �α��� �˾�â�� ��� �� �Լ�
    public void ClickOkButton()
    {
        if (PressScreenButton.activeSelf == false)
        {
            PressScreenButton.SetActive(true);                                                                                                                                                                                                     
        }
    }
}
