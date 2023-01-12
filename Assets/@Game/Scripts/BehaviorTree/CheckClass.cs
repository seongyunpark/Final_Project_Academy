using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CheckClass : Conditional
{
    public override TaskStatus OnUpdate()
    {
        if (InGameTest.Instance.m_ClassState == "ClassStart")
        {
            if (!gameObject.GetComponent<Student>().isDesSetting == false)
            {
                SetClassDestination();
            }
            return TaskStatus.Success;
        }
        else if(InGameTest.Instance.m_ClassState == "ClassEnd" )
        {
            if(gameObject.GetComponent<Student>().isDesSetting)
            {
                SetClassEndDestination();
            }
            return TaskStatus.Success;

        }
        return TaskStatus.Failure;

    }

    void SetClassDestination()
    {
        gameObject.GetComponent<Student>().m_DestinationQueue.Clear();
        gameObject.GetComponent<Student>().m_DestinationQueue.Enqueue("ClassEntrance");
        gameObject.GetComponent<Student>().m_DestinationQueue.Enqueue("ClassSeat");
        gameObject.GetComponent<Student>().isDesSetting = true;
    }

    void SetClassEndDestination()
    {
        gameObject.GetComponent<Student>().m_DestinationQueue.Clear();

        int _rand = Random.Range(1, 3);


    }
}