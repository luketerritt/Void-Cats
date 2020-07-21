using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UIElements;
using CreatureContainers;

//this namespace contains the containers for creature data
namespace CreatureContainers
{
    public enum CreatureState
    {
        WALK,
        NOTICE,
        SLEEP,
    }

    public struct CreatureInfo
    {
        public int CreatureID;
        public CreatureState agentState;

    }
}

//this class contains creature behaviours

[RequireComponent(typeof(NavMeshAgent))]
public class TestCreature : MonoBehaviour
{
    //the main state of the agent
    public CreatureState AgentState;
    //a way to store the previous state of the agent
    public CreatureState PreviousState;

    //an instance of the lighting manager (drag into via unity editor)
    public GameObject lightingManager;

    //a way of checking if the state just changed
    private bool stateJustChanged = false;

    public NavMeshAgent navMeshAgent;
    

    // Start is called before the first frame update
    void Start()
    {
        AgentState = 0;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateTimeState();
        
        switch(AgentState)
        {
            case (CreatureState)0:
                {
                    walkState();
                    break;
                }
            case (CreatureState)1:
                {
                    noticeState();
                    break;
                }
            case (CreatureState)2:
                {
                    sleepState();
                    break;
                }
        }

    }

    void UpdateTimeState()
    {
        var timeState = lightingManager.GetComponent<LightingManager>().currentQuater;

        //if it is LateNight
        if((int)timeState == 0)
        {
            //if the AgentState is not in the desired state yet it becomes the desired state
            if(AgentState != (CreatureState)2) //it may lead to bugs down the line when agent state does not match the state the time gives -- (add other states to the check here) 
            {
                PreviousState = AgentState; //assign the previous state
                AgentState = (CreatureState)2; //agent goes to sleep
                stateJustChanged = true;
                return;
            }
            return;
        }

        //if it is Morning
        if ((int)timeState == 1)
        {
            if(AgentState != (CreatureState)0)
            {
                PreviousState = AgentState; //assign the previous state
                AgentState = (CreatureState)0; //agent walks
                stateJustChanged = true;
                return;
            }
           
            return;
        }

        //if it is Afternoon
        if ((int)timeState == 2)
        {
            if(AgentState != (CreatureState)1)
            {
                PreviousState = AgentState; //assign the previous state
                AgentState = (CreatureState)1; //agent notices
                stateJustChanged = true;
                return;                
            }
            return;
        }

        //if it is Night
        if ((int)timeState == 3)
        {
            if(AgentState != (CreatureState)2)
            {
                PreviousState = AgentState; //assign the previous state
                AgentState = (CreatureState)2; //agent sleeps
                stateJustChanged = true;
                return;
            }            
            return;
        }
    }

    
    void walkState()
    {
       if(stateJustChanged)
       {
            if(PreviousState == (CreatureState)2)
            {
                transform.rotation *= Quaternion.Euler(-90.0f, 0, 0);
            }
            else
            {
                transform.rotation *= Quaternion.Euler(0, 0, 0);
            }
            
            stateJustChanged = false;
       }



        Vector3 tempLocation = new Vector3(-5, 0, 0);
        navMeshAgent.SetDestination(tempLocation);

    }

    void noticeState()
    {
       if(stateJustChanged)
       {
            transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
       }


        Vector3 tempLocation = new Vector3(0, 0, -2);
        navMeshAgent.SetDestination(tempLocation);

        //rotate towards the camera position
        //var tempRotationLocation = GameObject.FindGameObjectWithTag("MainCamera");
        var tempRotationLocation = GameObject.FindGameObjectWithTag("Player");
        RotateTowards(tempRotationLocation.transform.position);

    }

    void sleepState()
    {
        if(stateJustChanged)
        {
            //need a way to apply this just once -- BINGO
            //transform.rotation *= Quaternion.Euler(90.0f, 0, 0);
            stateJustChanged = false;
        }

        //need a way to apply this just once -- BINGO
        transform.rotation *= Quaternion.Euler(0, 0, 90.0f);

        Vector3 tempLocation = this.transform.position;
        navMeshAgent.SetDestination(tempLocation);
    }

    //allows the agent to rotate towards an object without affecting the Y axis
    void RotateTowards(Vector3 target)
    {
        float rotationSpeed = 1;

        //set y as 90 for both to prevent it doing a Michael Jackson and slanting
        Vector3 targetOnlyY = new Vector3(target.x, 90, target.z);
        Vector3 positionOnlyY = new Vector3(transform.position.x, 90, transform.position.z);

        Vector3 direction = (targetOnlyY - positionOnlyY).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
