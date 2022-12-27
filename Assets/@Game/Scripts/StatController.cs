using UnityEngine;

namespace StatData.Runtime
{
    /// <summary>
    /// ������� �л����� ScriptableObject�� �������� �������� Ŭ����
    /// 
    /// 2022.11.05
    /// </summary>
    public class StatController : MonoBehaviour
    {
        [SerializeField] private DataBase m_StatDataBase;

        private bool m_IsInitialized = false;
        public bool isInitialized => m_IsInitialized;
        public DataBase dataBase => m_StatDataBase;

        public void CalculateValue(Student _studentStat, StatModifier _classInfo)
        {
            _studentStat.m_StudentData.m_StudentSystemValue += _classInfo.StatsModifierInfo[ModifierStatType.System];

            _studentStat.m_StudentData.m_StudentContentsValue += _classInfo.StatsModifierInfo[ModifierStatType.Contents];

            _studentStat.m_StudentData.m_StudentBalanceValue += _classInfo.StatsModifierInfo[ModifierStatType.Balance];

        }
    }
}
