using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StatData.Runtime;
using BT;

public class MoveCharacter : MonoBehaviour
{
    GameObject m_ProgrammingRoom;
    GameObject m_ArtRoom;
    GameObject m_ProductManagerRoom;
    GameObject m_Restaurant;

    List<GameObject> m_FreeWalkPath = new List<GameObject>();

    bool m_isStop;

    Student m_Student;

    NavMeshAgent _agent;

    private void Start()
    {
        m_ProgrammingRoom = GameObject.FindGameObjectWithTag("Programming");
        m_ArtRoom = GameObject.FindGameObjectWithTag("Art");
        m_ProductManagerRoom = GameObject.FindGameObjectWithTag("ProductManager");

        m_FreeWalkPath.Add(GameObject.Find("RestPath1"));
        m_FreeWalkPath.Add(GameObject.Find("RestPath2"));
        m_FreeWalkPath.Add(GameObject.Find("RestPath3"));
        m_FreeWalkPath.Add(GameObject.Find("RestPath4"));
        m_FreeWalkPath.Add(GameObject.Find("RestPath5"));

        m_Restaurant = GameObject.FindGameObjectWithTag("Restaurant");

        _agent = GetComponent<NavMeshAgent>();
        m_Student = GetComponent<Student>();
    }

    private void Update()
    {
        MoveCharacters();
    }

    private void MoveCharacters()
    {

        if (m_Student.m_Doing == Student.Doing.FreeWalk)
        { 
            if (_agent.velocity.magnitude <= 0.001f)
            {
                int _randomPath = Random.Range(0, 4);
                
                _agent.SetDestination(m_FreeWalkPath[_randomPath].transform.position);
            }
        }

        if (m_Student.m_Doing == Student.Doing.AtRest)
        {
            if (_agent.velocity.magnitude <= 0.001f)
            {
                int _randomPath = Random.Range(0, 4);

                _agent.SetDestination(m_FreeWalkPath[_randomPath].transform.position);

            }
        }

        if (m_Student.m_Doing == Student.Doing.Study)
        {
            if (m_Student.m_StudentData.m_StudentType == Type.Programming)
            {
                _agent.SetDestination(m_ProgrammingRoom.transform.position);
            }
            else if (m_Student.m_StudentData.m_StudentType == Type.ProductManager)
            {
                _agent.SetDestination(m_ProductManagerRoom.transform.position);
            }
            else if (m_Student.m_StudentData.m_StudentType == Type.Art)
            {
                _agent.SetDestination(m_ArtRoom.transform.position);
            }
        }

        if (m_Student.m_Doing == Student.Doing.GoTo)
        {
            _agent.SetDestination(m_Restaurant.transform.position);

            if (m_Student.transform.position.x == m_Restaurant.transform.position.x)
            {
                m_Student.isHereRestaurant = true;
            }
        }
    }
}
