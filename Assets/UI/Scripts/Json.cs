using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ��� �ֵ��� ��� �־�� �Ѵٰ�?
public class Json
{
    // Json �������� ������ ��ȯ
    public void TranslateToJson()
    {
        string str = JsonUtility.ToJson(PlayerInfo.Instance);

        Debug.Log("To Json : " + str);
    }

    // Json ������ ������ �����ͷ� ��ȯ

    // Json  ���Ϸ� ����
    // 

}
