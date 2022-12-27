using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Object = UnityEngine.Object;

namespace StatData.Runtime
{
    /// <summary>
    /// �л��� ������ ����, ������ ������ �ִ� �� ������ ���⼭ ó�����ֱ�
    /// </summary>
    public class Stat
    {
        protected StudentData m_StudentData;
        //protected ProfessorData m_ProfessorData;

        public int m_StudentSystemValue;
        public int m_StudentContentsValue;
        public int m_StudentBalanceValue;
        public Type m_StudentType;

        public int studentSystem => m_StudentData.StudentSystem;
        public int studentContents => m_StudentData.StudentContents;
        public int studentBalance => m_StudentData.StudentBalance;
        public Type studentType => m_StudentData.StudentType;

        public Stat(StudentData _studentData)
        {
            m_StudentData = _studentData;
            m_StudentSystemValue = studentSystem;
            m_StudentContentsValue = studentContents;
            m_StudentBalanceValue = studentBalance;
            m_StudentType = studentType;
        }

        //public Stat(ProfessorData _professorData)
        //{
        //    m_ProfessorData = _professorData;
        //}
    }
}
