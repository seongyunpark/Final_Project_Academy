using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 2022. 11. 22 Mang
/// 
/// ���ӿ��� �� �̵��� ����� Ŭ����
/// </summary>
public class MoveSceneManager : MonoBehaviour
{
    public static MoveSceneManager m_Instance = null;       // Manager ������ �̱������� ���

    // �̱������� ���� ������ �����ϰ� �����ϰ� ����ϱ� ���� �ʱ�ȭ(?) ���
    private void Awake()
    {
        // Instance �� �����Ѵٸ� gameObject ����
        if (m_Instance != null)
        {
            Destroy(this.gameObject);

            return;
        }

        // ���۵� �� �ν��Ͻ� �ʱ�ȭ, ���� �Ѿ���� �����Ǳ� ���� ó��
        m_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }
}
