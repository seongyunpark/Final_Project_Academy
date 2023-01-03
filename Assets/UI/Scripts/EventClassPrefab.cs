using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatData.Runtime;

public struct SaveEventClassData
{
    public string EventClassName;       // �̺�Ʈ �̸�
    public int RewardStat;              // ���� ����
    public int RewardMoney;             // ���� �Ӵ�
    public int PossibleSetCount;        // ���� ���� Ƚ��
    public string EventInformation;     // �̺�Ʈ ���� ����
}

// ������ ������ ��� : �������� ObjectManager.Instance.m_StudentList �� ������ ���� ���ָ� �ȴ�
// ������ ��ȭ�� ��� : PlayerInfo.Instance.m_MyMoney ���� ���������� �ٲ��ָ� �ȴ�

public class EventClassPrefab : MonoBehaviour
{
    // ���̽� ���� ���� �� �׽�Ʈ�� ���� ���� ����
    SaveEventClassData TempEventData = new SaveEventClassData();

    // Json ������ �Ľ��ؼ� �� �����͵��� �� ��� �� ����Ʈ ����
    public List<SaveEventClassData> SelectEventClassInfo = new List<SaveEventClassData>();      // ���� �̺�Ʈ
    public List<SaveEventClassData> FixedEventClassInfo = new List<SaveEventClassData>();       // ���� �̺�Ʈ

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            // �׽�Ʈ
            // �̺�Ʈ struct ���� �ʱ�ȭ ���ֱ�
            TempEventData.EventClassName = "test" + i;
            TempEventData.RewardStat = 3;
            TempEventData.RewardMoney = 1000;

            SelectEventClassInfo.Add(TempEventData);

            TempEventData.RewardStat += 1;
            TempEventData.RewardMoney += 200;
        }
        // ����Ʈ�� ���̽� �Ľ� �� �� : .Add() -> �ѹ��� �� �Ľ��ϴ°� �ƴ϶� 1. ���̽����� �ϳ� �о �ӽú����� ��� ����Ʈ�� �ӽú��� �ֱ�
        // �״��� �ϳ� �а� �ӽú��� ��� ����Ʈ �ֱ� �ݺ�! ����...
        // 

        // ObjectManager.Instance.m_StudentList[i].m_StudentData.m_StudentContentsValue += 3;       �̷��� �߰�
    }

    // Update is called once per frame
    void Update()
    {

    }
}
