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
        SetDestination();

        if(InGameTest.Instance.m_ClassState == "ClassStart" 
            && m_NowDestination != "ClassEnterance"
            && m_NowDestination != "ClassSeat")
        {
            m_NowDestination = " ";
        }
		return TaskStatus.Success;
	}

    void SetDestination()
    {
        if(m_NowDestination == " ")
        {
            if(gameObject.GetComponent<Student>().m_DestinationQueue.Count != 0)
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

            if(_dis < 1)
            {
                if(gameObject.GetComponent<Student>().m_DestinationQueue.Count != 0)
                {
                    m_NowDestination = gameObject.GetComponent<Student>().m_DestinationQueue.Dequeue();

                    _name = gameObject.GetComponent<BehaviorTree>().ExternalBehavior.GetVariable(m_NowDestination).ToString();
                    gameObject.GetComponent<BehaviorTree>().ExternalBehavior.SetVariableValue("Destination", GameObject.Find(_name).transform.position);
                }
                else
                {
                    gameObject.GetComponent<Student>().isArrivedClass = true;
                }
            }
        }
    }
}