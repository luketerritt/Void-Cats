using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UIElements;
using CreatureContainers;

//this namespace contains the containers for creature data :)
namespace CreatureContainers
{
    public enum CreatureState
    {
        Flee, //0 = Flee -- a walk/run away from player
        Notice, //1 = notice -- a turn around towards player
        Sleep, //2 = sleep -- goto set location, play sleep animation
        Eat, //3 = eat -- goto a set location (or the player if they have a berry?), play eat animation
        Wash, //4 = Wash -- UNIQUE to the Fish - goto a set location, play wash animation
        TailChase, //5 = chase tail -- UNIQUE* to the Dog - update set position near self and play chase tail animation
        Roar, //6 = roar -- UNIQUE to the Tiger - goto a set locaton, play roar animation
        Flop, //7 = flop -- UNIQUE* to the Dragon - find clear area, run forward, play flop animation as moving like a slide?
        Roll, //8 = roll -- UNIQUE* to the Cow - find clear area, move while playing animation? (similar to flop?)
        Peck, //9 = peck -- UNIQUE to the Duck - goto location, play peck animation
        Levitate, //10 = levitate -- UNIQUE to the Cat - goto location, play levitate animation
        Anger //11 = anger -- UNIQUE* to the rabbit - goto location, play punch animation and audio cue
    }

    public struct CreatureInfo
    {
        public int CreatureID;
        public CreatureState agentState;
        public string CreatureName;

    }

    //Creature information which needs to be persistent!!!!!
    //replace CreatureName string with final names later in development!

    //CreatureID 0 = Block -- (a test creature in debugging)
    //CreatureID 1 = Fish -- Nocturnal/Scared - Water Clean Action
    //CreatureID 2 = Dog -- Day/Passive - Chase tail Action
    //CreatureID 3 = Tiger -- Day/Passive - Roar Action
    //CreatureID 4 = Dragon -- Day/Passive - Run and Slide Action
    //CreatureID 5 = Cow -- Nocturnal/Passive - Roll 
    //CreatureID 6 = Duck -- Day/Scared - Peck Ground
    //CreatureID 7 = Cat -- Nocturnal/Scared - Levitate
    //CreatureID 8 = Rabbit -- Nocturnal/Scared - Tree Punch  
}

//this class contains creature behaviours

[RequireComponent(typeof(NavMeshAgent))]
public class TestCreature : MonoBehaviour
{
    public CreatureInfo info;

    public int ID;

    //the main state of the agent
    //public CreatureState AgentState;

    //a way to store the previous state of the agent
    [HideInInspector]
    //public CreatureState PreviousState;

    //an instance of the lighting manager (drag into via unity editor)
    public GameObject lightingManager;

    //a way of checking if the state just changed
    private bool stateJustChanged = false;

    public NavMeshAgent navMeshAgent;

    public float eyeStrength = 10.0f;

    //[HideInInspector]
    public bool canSeePlayer = false;

    public float losePlayerTimer = 5.0f;

    private float losePlayerIterator = 0.0f;

    public float speed = 3;

    public float runSpeed = 10;

    public bool passiveCreatureType = false;

    //drop in bush gameObjects, which this creature should go and eat at
    public GameObject[] FoodLocations;

    //drop in gameObjects (can be empty as long as it has transform), where creature should do unique actions
    public GameObject[] UniqueLocations;

    //the variable used to determine which foodLocation or UniqueLocation a creature can go to
    private int randomLocation = 0;

    //copy of the player -- used to get its transform
    private GameObject PlayerObject;


    // Start is called before the first frame update
    void Start()
    {
        info.agentState = 0;
        info.CreatureID = ID;
        //assign name based on ID -- replace with final names as they appear on file name!
        switch (info.CreatureID)
        {
            case (0):
                {
                    info.CreatureName = "Block";
                    break;
                }
            case (1):
                {
                    info.CreatureName = "Fish";
                    break;
                }
            case (2):
                {
                    info.CreatureName = "Dog";
                    break;
                }
            case (3):
                {
                    info.CreatureName = "Tiger";
                    break;
                }
            case (4):
                {
                    info.CreatureName = "Dragon";
                    break;
                }
            case (5):
                {
                    info.CreatureName = "Cow";
                    break;
                }
            case (6):
                {
                    info.CreatureName = "Duck";
                    break;
                }
            case (7):
                {
                    info.CreatureName = "Cat";
                    break;
                }
            case (8):
                {
                    info.CreatureName = "Rabbit";
                    break;
                }
            
        }

        //error checking incase food locations and unique locations are null
        if(FoodLocations[0] == null || UniqueLocations[0] == null)
        {
            Debug.Log("Creature " + this.gameObject + " FoodLocations or UniqueLocations is null!");
            FoodLocations[0] = this.gameObject;
            UniqueLocations[0] = this.gameObject;
        }
        PlayerObject = GameObject.FindGameObjectWithTag("Player");

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //update the action you should peform
        UpdateTimeState();

        //Debug.Log("agentState" + info.agentState);

        //peform behaviour
        switch(info.agentState)
        {
            case (CreatureState)0: //0 = Flee -- a walk/run away from player
                {
                    fleeState();
                    break;
                }
            case (CreatureState)1: //1 = notice -- a turn around towards player
                {
                    noticeState();
                    break;
                }
            case (CreatureState)2: //2 = sleep -- goto set location, play sleep animation
                {
                    sleepState();
                    break;
                }
            case (CreatureState)3: //3 = eat -- goto set location, play sleep animation
                {
                    eatState();
                    break;
                }
            case (CreatureState)4: //4 = wash face -- UNIQUE TO FISH, goto location, play wash animation
                {
                    washFaceState();
                    break;
                }
            case (CreatureState)5:
                {
                    chaseTailState(); //5 = chase tail -- UNIQUE TO DOG, goto location, play tail chase animation
                    break;
                }
        }

    }

    void UpdateTimeState()
    {
        var timeState = lightingManager.GetComponent<LightingManager>().currentQuater;

        
        float tempDistance = Vector3.Distance(this.transform.position, PlayerObject.transform.position);
        
        //if the distance between the player and the creature is less than the eye strength
        if(tempDistance < eyeStrength)
        {
            //peform a raycast from the creature to the player, if there is a hit, you can see
            //Vector3 direction = (tempPlayer.transform.position - transform.position).normalized;
            //Physics.Raycast(transform.position, direction, out hit)

            //peform a raycast from the creature forwards, if the player is hit, it can see it
            RaycastHit hit;
            //also dont do this check if the creature is sleeping
            if(Physics.Raycast(transform.position, transform.forward, out hit) &&
                info.agentState != CreatureState.Sleep)
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
                //if we see the player
                if(hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player was Found via eye-sight");
                    //since the player is seen, set it to true and reset the iterator of losePlayer
                    canSeePlayer = true;
                    losePlayerIterator = 0.0f;
                }
            }
            //if the player is not on the ground -- THIS WILL NEED TESTING on various terrain!
            if(PlayerObject.GetComponent<CharacterController>().isGrounded
                && info.agentState != CreatureState.Sleep)
            {
                Debug.Log("Player was Found in the air");
                //also act if the player was "Seen"
                canSeePlayer = true;
                losePlayerIterator = 0.0f;
            }
            
            
        }

        //if the creature can see the player, modify behaviour based on its type
        if(canSeePlayer)
        {
            //if you are a passive creature, you turn towards the player
            if (passiveCreatureType)
            {
                if (info.agentState != (CreatureState)1)
                {
                    info.agentState = (CreatureState)1; //agent notices
                    stateJustChanged = true;
                }
                    
            }
            else //you are not a passive creature so you will FLEEEEEEEEEEEEEE
            {
                if (info.agentState != (CreatureState)0)
                {
                    info.agentState = (CreatureState)0; //agent flees
                    stateJustChanged = true;
                }
            }

            //update iterator
            losePlayerIterator += Time.deltaTime;

            if (losePlayerIterator > losePlayerTimer)
            {
                canSeePlayer = false;
                losePlayerIterator -= losePlayerTimer;
            }
            
        }
        else //if the creature can not see the player
        {
            //determine which creature type this is by using a switch statement with creature ID
            switch (info.CreatureID)
            {
                case 0: //Block debug code
                    {
                        //if it is LateNight
                        if ((int)timeState == 0)
                        {
                            //if the AgentState is not in the desired state yet it becomes the desired state
                            if (/*AgentState*/ info.agentState != (CreatureState)2) //it may lead to bugs down the line when agent state does not match the state the time gives -- (add other states to the check here) 
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                /*AgentState*/
                                info.agentState = (CreatureState)2; //agent goes to sleep
                                stateJustChanged = true;
                                //return;
                                break;
                            }
                            break;
                        }

                        //if it is Morning
                        if ((int)timeState == 1)
                        {
                            if (info.agentState != (CreatureState)0)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)0; //agent walks
                                stateJustChanged = true;
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon
                        if ((int)timeState == 2)
                        {
                            if (info.agentState != (CreatureState)1)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)1; //agent notices
                                stateJustChanged = true;
                                break;
                            }
                            break;
                        }

                        //if it is Night
                        if ((int)timeState == 3)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps
                                stateJustChanged = true;
                                break;
                            }
                            break;
                        }
                        break;

                    }
                case 1: //Fish
                    {
                        //if it is LateNight - Fish should wash its face
                        if ((int)timeState == 0)
                        {
                            if (info.agentState != (CreatureState)4)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)4; //agent washes face - UNIQUE
                                if (UniqueLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, UniqueLocations.Length);
                                }

                                stateJustChanged = true;
                                break;
                            }
                            break;
                        }

                        //if it is Morning - Fish go to sleep
                        if ((int)timeState == 1)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps
                                stateJustChanged = true;
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon - Fish go to sleep
                        if ((int)timeState == 2)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps
                                stateJustChanged = true;
                                break;
                            }
                            break;

                        }
                        //if it is Night - Fish go and eat
                        if ((int)timeState == 3)
                        {
                            if (info.agentState != (CreatureState)3)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)3; //agent "goes for food"

                                //if the size of the foodlocations is less than 1
                                if (FoodLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, FoodLocations.Length);
                                }

                                stateJustChanged = true;
                                break;
                            }
                            break;
                        }
                        break;
                    }
                case 2: //Dog
                    {
                        //if it is LateNight - Dog should be asleep
                        if ((int)timeState == 0)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps
                                stateJustChanged = true;
                                break;
                            }
                            break;
                        }

                        //if it is Morning - Dog should eat
                        if ((int)timeState == 1)
                        {
                            if (info.agentState != (CreatureState)3)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)3; //agent "goes for food"

                                //if the size of the foodlocations is less than 1
                                if (FoodLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, FoodLocations.Length);
                                }

                                stateJustChanged = true;
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon - Dog should chase its tail
                        if ((int)timeState == 2)
                        {
                            if (info.agentState != (CreatureState)5)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)5; //agent chases tail - UNIQUE
                                if (UniqueLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, UniqueLocations.Length);
                                }

                                stateJustChanged = true;
                                break;
                            }
                            break;

                        }
                        //if it is Night - Dog should sleep
                        if ((int)timeState == 3)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps
                                stateJustChanged = true;
                                break;
                            }
                            break;
                        }
                        break;
                    }
            }
        }
        

    }

        
      

    //this happens if the creature sees the player and is a shy/scared type
    void fleeState()
    {
       if(stateJustChanged)
       {
            navMeshAgent.speed = runSpeed;
            stateJustChanged = false;
       }

        //get the players location
        Vector3 tempLocation = PlayerObject.transform.position;       
        Vector3 tempForward = PlayerObject.GetComponent<PlayableCamera>().firstPersonCamera.transform.forward;

        float distance = Vector3.Distance(tempLocation, transform.position);

        //get the opposite direction      
        tempForward *= (distance * 2);

        tempForward += tempLocation;

        //multiply by -1
        //tempLocation *= -1;
        //scale based on direction
        //tempLocation *= distance;

        //Debug.Log("Flee state, going to " + tempForward);

        //goto a location in the opposite direction
        navMeshAgent.SetDestination(tempForward);

    }

    void noticeState()
    {
       if(stateJustChanged)
       {
            navMeshAgent.speed = speed;
            transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
       }


        //Vector3 tempLocation = new Vector3(0, 0, -2);
        navMeshAgent.SetDestination(transform.position);

        //rotate towards the camera position
        //var tempRotationLocation = GameObject.FindGameObjectWithTag("MainCamera");
        var tempRotationLocation = GameObject.FindGameObjectWithTag("Player");
        RotateTowards(tempRotationLocation.transform.position);

    }

    void sleepState()
    {
        if(stateJustChanged)
        {
            navMeshAgent.speed = speed;
            //need a way to apply this just once -- BINGO
            //transform.rotation *= Quaternion.Euler(90.0f, 0, 0);
            stateJustChanged = false;
        }

        //need a way to apply this just once -- BINGO
        transform.rotation *= Quaternion.Euler(0, 0, 90.0f);

        Vector3 tempLocation = this.transform.position;
        navMeshAgent.SetDestination(tempLocation);
    }

    void eatState()
    {
        if (stateJustChanged)
        {
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
        }


        Vector3 tempLocation = FoodLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);
        //Debug.Log("Eat state, going to " + tempLocation);

        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if(distance <= 2)
        {
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");

            //probably play eat animation here
        }
    }

    // a unique behaviour to the FISH, which has it go to an empty gameobject near a water source
    void washFaceState()
    {
        if (stateJustChanged)
        {
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);

        //Debug.Log("Wash state, going to " + tempLocation);
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 2)
        {
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");

            //probably play wash face animation here
        }
    }

    //a unique behaviour to the DOG, in which it goes to a set location and plays an animation
    void chaseTailState()
    {
        if (stateJustChanged)
        {
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);

        //Debug.Log("Wash state, going to " + tempLocation);
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 2)
        {
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");

            //probably play chase tail animation here -- MAYBE apply rotations?
        }
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
