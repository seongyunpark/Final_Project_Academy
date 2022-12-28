using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ���� ��Ű�� 
/// 
/// Title -> InGame -> UI
/// </summary>
public class MergeScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("����_008_test");
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }

}
