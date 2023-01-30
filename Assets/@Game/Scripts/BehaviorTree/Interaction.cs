using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;

public class Interaction : Action
{
    private GameObject m_InterActionObj;
    private bool m_IsPlaying;

    public override void OnStart()
    {
        m_IsPlaying = false;
        SharedGameObject m_Interaction = gameObject.GetComponent<BehaviorTree>().ExternalBehavior.GetVariable("InteractionObject").ConvertTo<SharedGameObject>();
        m_InterActionObj = m_Interaction.Value;
    }

    public override TaskStatus OnUpdate()
    {
        m_InterActionObj.GetComponent<Student>().isInteracting = true;

        m_IsPlaying = true;

        gameObject.GetComponent<Student>().m_Roll = "A";
        m_InterActionObj.GetComponent<Student>().m_Roll = "B";

        ScriptPlay();
        return TaskStatus.Success;
    }

    public void ScriptPlay()
    {
        // Debug.Log(InGameTest.Instance._interactionScript[0]);
        // Debug.Log(InGameTest.Instance._interactionScript[1]);
        // Debug.Log(InGameTest.Instance._interactionScript[2]);

        m_IsPlaying = false;
    }
}