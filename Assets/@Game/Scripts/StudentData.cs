using UnityEngine;

namespace StatData.Runtime
{
    [CreateAssetMenu(fileName = "StudentData", menuName = "StatData/StudentData", order = 0)]
    /// �л��� ������ ����ִ� ����
    ///
    ///
    public class StudentData : ScriptableObject
    {
        [SerializeField] private string m_StudentName;
        [SerializeField] private int m_StudentSystem;
        [SerializeField] private int m_StudentContents;
        [SerializeField] private int m_StudentBalance;
        [SerializeField] private Type m_StudentType;

        public string StudentName => m_StudentName;
        public int StudentSystem => m_StudentSystem;
        public int StudentContents => m_StudentContents;
        public int StudentBalance => m_StudentBalance;

        public Type StudentType => m_StudentType;
    }

    public enum Type
    {
        Programming,
        Art,
        ProductManager,
    }
}
