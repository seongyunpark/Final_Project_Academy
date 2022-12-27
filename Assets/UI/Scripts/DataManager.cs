using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    private List<string> testDataA = new List<string>();
    public List<int> testDataB = new List<int>();
    public List<float> testDataC = new List<float>();

    public int _intA;
    public float _floatA;

    public List<string> MyTest
    {
        get { return testDataA; }
        set { testDataA = value; }
    }
}

/// <summary>
/// �����͸� ���̽����� �����Ϸ��� ���� ��ũ��Ʈ
/// �߰� �ʿ�
/// </summary>
public class DataManager : MonoBehaviour
{
    string path;

    // Start is called before the first frame update
    void Start()
    {
        path = Path.Combine(Application.persistentDataPath + "/Data/", "database.json");
        JasonLoad();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void JasonSave()
    {
        SaveData saveData = new SaveData();

        for (int i = 0; i < 10; i++)
        {
            saveData.MyTest.Add("�׽�Ʈ?" + i);
        }

        for(int i = 0; i < 10; i++)
        {
            saveData.testDataB.Add(i);
        }

        // saveData._intA = TestJason.instance.playerGold;
        // saveData._floatA = TestJason.instance.playerPower;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);

        Debug.Log("data : " + saveData);
    }


    public void JasonLoad()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            // TestJason.instance.playerGold = 100;
            // TestJason.instance.playerPower = 4;
        }
        else
        {
            string loadJason = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJason);

            if (saveData != null)
            {
                for (int i = 0; i < saveData.MyTest.Count; i++)
                {
                    //  TestJason.instance.testDataA.Add(saveData.MyTest[i]);
                    // TestJason.instance.mystring.Add(saveData.MyTest[i]);
                }

                for (int i = 0; i < saveData.testDataB.Count; i++)
                {
                    // TestJason.instance.testDataB.Add(saveData.testDataB[i]);
                    // TestJason.instance.mystring.Add(saveData.MyTest[i]);

                }

            }
        }
    }
}
