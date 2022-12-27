
namespace Conditiondata.Runtime
{
    /// <summary>
    /// �� �л����� ������� ����� �����͸� �������ִ� Ŭ����
    /// 
    /// 2022.11.05
    /// </summary>
    public class StudentCondition
    {
        private ConditionData m_ConditionData;
        
        public int m_StudentHungryValue;
        public int m_StudentTiredValue;

        public int StudentHungry => m_ConditionData.Hungry;
        public int StudentTired => m_ConditionData.Tired;

        public StudentCondition(ConditionData _conditionData)
        {
            m_ConditionData = _conditionData;
            m_StudentHungryValue = StudentHungry;
            m_StudentTiredValue = StudentTired;
        }

        // ����� �ð��� ���� ������� �پ�� �� �ְ� ���ִ� �Լ�
        public void AutomaticCondition(int _hungry, int _tired)
        {
            if (m_StudentHungryValue > 0 && m_StudentTiredValue > 0)
            {
                m_StudentHungryValue -= _hungry;
                m_StudentTiredValue -= _tired;

                InGameTest.Instance.CheckStudentInfo();
                InGameTest.Instance.SelectClassAndStudent();
            }
        }
    }
}


