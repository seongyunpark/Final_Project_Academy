using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public abstract class Node
    {
        public enum Status
        {
            SUCCESS,
            RUNNING,
            FAILURE,
        }

        protected List<Node> m_Children = new List<Node>(); // �ڽĳ����� ������ ����Ʈ
        public List<Node> Children => m_Children;

        public static bool isHereRestaurant = false;

        public Node() { }


        public void AddChildNode(Node n)
        {
            m_Children.Add(n);
        }

        public abstract Status Run();
    }

    public abstract class CompositeNode : Node
    {

    }

    public abstract class LeafNode : Node
    {
        public Student m_MyStudent { get; set; }
    }

    public class DecoratorNode : Node
    {
        public override Status Run()
        {
            foreach (Node child in Children)
            {
                return child.Run();
            }
            return Status.RUNNING;
        }
    }

    public class Selector : CompositeNode
    {
        public override Status Run()
        {
            foreach (Node child in Children)
            {
                if (child.Run() == Status.SUCCESS)
                {
                    return Status.SUCCESS;
                }
                else
                {

                }
            }

            return Status.FAILURE;
        }
    }

    public class Sequnce : CompositeNode
    {
        public override Status Run()
        {
            foreach (Node child in Children)
            {
                if (child.Run() == Status.FAILURE)
                {
                    return Status.FAILURE;
                }
                else
                {

                }
            }

            return Status.SUCCESS;
        }
    }

    public class Eat : LeafNode
    {
        public override Status Run()
        {
            if (m_MyStudent.isHereRestaurant == true) // ���ִ� ��Ұ� �Ĵ��̸� �Ա� �����ϱ� -> ĳ���� �̵��� ��ų �� �Ĵ翡 �����ϸ� bool������ �����ϱ�?
            {
                int _randomObj = Random.Range(60, 80);

                m_MyStudent.m_StudentCondition.m_StudentHungryValue += _randomObj;
                InGameTest.Instance.CheckStudentInfo();
                m_MyStudent.m_Doing = Student.Doing.Eat;
                m_MyStudent.isHereRestaurant = false;
                return Status.SUCCESS;
            }
            return Status.FAILURE;
        }
    }

    public class Rest : LeafNode
    {
        public override Status Run()
        {
            if (m_MyStudent.m_StudentCondition.m_StudentTiredValue <= 40)
            {
                int _randomObj = Random.Range(40, 70);

                m_MyStudent.m_StudentCondition.m_StudentTiredValue += _randomObj;
                InGameTest.Instance.CheckStudentInfo();
                m_MyStudent.m_Doing = Student.Doing.AtRest;

                return Status.SUCCESS;
            }
            return Status.FAILURE;
        }
    }

    public class FreeWalk : LeafNode
    {
        public override Status Run()
        {
            m_MyStudent.m_Doing = Student.Doing.FreeWalk;

            return Status.SUCCESS;
        }
    }


    public class GotoRestaurant : LeafNode
    {
        public override Status Run()
        {
            if (m_MyStudent.m_RestaurantNumOfPeople < 30 &&
                m_MyStudent.m_StudentCondition.m_StudentHungryValue <= 60 &&
                m_MyStudent.isHereRestaurant == false)
            {
                m_MyStudent.m_RestaurantNumOfPeople++;

                m_MyStudent.m_Doing = Student.Doing.GoTo;

                InGameTest.Instance.CheckStudentInfo();
              
                return Status.SUCCESS;
            }
            return Status.FAILURE;
        }
    }

    // ���� ��� ��ư�� ������ �л��� �ൿ�� Study�� �ٲ۴ٰ� �����ϰ� �ۼ�
    public class Studing : LeafNode
    {
        public override Status Run()
        {
            if (m_MyStudent.m_Doing == Student.Doing.Study)
            {
                return Status.SUCCESS;
            }
            return Status.FAILURE;
        }
    }

    public class Idel : LeafNode
    {
        public override Status Run()
        {
            m_MyStudent.m_StudentCondition.AutomaticCondition(5, 5);

            m_MyStudent.m_Doing = Student.Doing.FreeWalk;

            return Status.SUCCESS;
        }
    }

}