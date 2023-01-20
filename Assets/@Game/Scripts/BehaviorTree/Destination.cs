using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Destination : Conditional
{
    private string m_NowDestination = " ";


    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {

        if (InGameTest.Instance.m_ClassState == ClassState.ClassStart
            && m_NowDestination != "ClassEntrance"
            && m_NowDestination != "ClassSeat")
        {
            m_NowDestination = " ";
        }

        SetDestination();

        return TaskStatus.Success;
    }

    void SetDestination()
    {
        if (m_NowDestination == " ")
        {
            if (gameObject.GetComponent<Student>().m_DestinationQueue.Count != 0)
            {
                m_NowDestination = gameObject.GetComponent<Student>().m_DestinationQueue.Dequeue();
                string _name = gameObject.GetComponent<BehaviorTree>().ExternalBehavior.GetVariable(m_NowDestination).ToString();
                gameObject.GetComponent<BehaviorTree>().ExternalBehavior.SetVariableValue("Destination", GameObject.Find(_name).transform.position);
            }
        }
        else
        {
            string _name = gameObject.GetComponent<BehaviorTree>().ExternalBehavior.GetVariable(m_NowDestination).ToString();

            float _dis = Vector3.Distance(gameObject.transform.position, GameObject.Find(_name).transform.position);

            Vector3 v1 = GameObject.Find(_name).transform.position;
            SharedVector3 sv = (SharedVector3)gameObject.GetComponent<BehaviorTree>().ExternalBehavior.GetVariable("Destination");
            Vector3 v2 = sv.Value;
            if (v1 != v2)
            {
                gameObject.GetComponent<BehaviorTree>().ExternalBehavior.SetVariableValue("Destination", GameObject.Find(_name).transform.position);
            }

            if (_dis < 3)
            {
                if (gameObject.GetComponent<Student>().m_DestinationQueue.Count != 0)
                {
                    m_NowDestination = gameObject.GetComponent<Student>().m_DestinationQueue.Dequeue();
                    if (m_NowDestination == "ClassSeat")
                    {
                        _name = gameObject.GetComponent<BehaviorTree>().ExternalBehavior.GetVariable(m_NowDestination).ToString();
                        //a++;
                    }
                    else
                    {
                        _name = gameObject.GetComponent<BehaviorTree>().ExternalBehavior.GetVariable(m_NowDestination).ToString();

                    }
                    gameObject.GetComponent<BehaviorTree>().ExternalBehavior.SetVariableValue("Destination", GameObject.Find(_name).transform.position);
                }
                else
                {
                    // ������ �ƴ� Ư�� �������� �������� �� �̵��ϴ� �ɷ� �����ϱ�
                    gameObject.GetComponent<Student>().isDesSetting = false;

                    if (m_NowDestination == "ClassSeat")
                    {
                        m_NowDestination = " ";
                        gameObject.GetComponent<Student>().isArrivedClass = true;
                    }
                }
            }
        }
    }
}