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
        Anger, //11 = anger -- UNIQUE* to the rabbit - goto location, play punch animation and audio cue
        Common, //12 = Common -- first critter colour
        Exotic, //13 = Exotic -- second critter colour
        Rare, //14 = Rare -- third critter colour
        Legendary //15 = Legendary -- fourth critter colour
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

    //critter IDs
    //CreatureID 9 - Beetle
    //CreatureID 10 - Snail
    //CreatureID 11 - Worm
    //CreatureID 12 - Slug
    //CreatureID 13 - Butterfly
    //CreatureID 14 - Ant
}

//this class contains creature behaviours

//[RequireComponent(typeof(NavMeshAgent))]
public class TestCreature : MonoBehaviour
{
    public CreatureInfo info;

    public int ID;

    public int CritterRarity; // 1 = common, 2 = exotic, 3 = rare, 4 = legendary

    //the main state of the agent
    //public CreatureState AgentState;

    //a way to store the previous state of the agent
    //[HideInInspector]
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

    //specific points used for various behaviours, order DOES matter!
    public GameObject[] UniqueSecondaryLocations;

    private int secondaryLocationReached = 0;

    //the variable used to determine which foodLocation or UniqueLocation a creature can go to
    private int randomLocation = 0;

    //copy of the player -- used to get its transform
    private GameObject PlayerObject;

    [HideInInspector]
    public bool reachedDestination = false;

    public float roarDuration = 5.0f;
    private float roarIterator = 0;

    private bool startedUnInteruptable = false;
    
    public Animator creatureAnimator;

    public GameObject soundObject;

    private int soundRandom;
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
            case (9): //critter teritory now
                {
                    info.CreatureName = "Beetle";
                    break;
                }
            case (10):
                {
                    info.CreatureName = "Snail";
                    break;
                }
            case (11):
                {
                    info.CreatureName = "Worm";
                    break;
                }
            case (12):
                {
                    info.CreatureName = "Slug";
                    break;
                }
            case (13):
                {
                    info.CreatureName = "Butterfly";
                    break;
                }
            case (14):
                {
                    info.CreatureName = "Ant";
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
       

        //if this is a critter - ID is bigger than or equal to 9
        if (ID >= 9)
        {
            //assign agent state based on assigned rarity
            info.agentState = (CreatureState)(CritterRarity + 11);

            //disable navmesh agent
            //navMeshAgent.enabled = false;
        }
        else //set up the navmesh agent
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>();
            creatureAnimator = this.GetComponent<Animator>();
            navMeshAgent.speed = speed;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //if this not a critter
        if(ID < 9)
        {
            //update the action you should peform
            UpdateTimeState();

            //Debug.Log("agentState" + info.agentState);

            //peform behaviour
            switch (info.agentState)
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
                case (CreatureState)6:
                    {
                        roarState(); //6 = roar -- UNIQUE TO TIGER, goto location, play roar animation/sound
                        break;
                    }
                case (CreatureState)7:
                    {
                        bellyflopState(); //7 = flop -- UNIQUE* to the Dragon - find clear area, run forward, play flop animation as moving like a slide?
                        break;
                    }
                case (CreatureState)8:
                    {
                        rollState(); //8 = flop -- UNIQUE* to the Cow - find clear area, move similar to dog???
                        break;
                    }
                case (CreatureState)9:
                    {
                        peckState(); //9 = peck -- UNIQUE* to the Duck - goto location, play animation?
                        break;
                    }
                case (CreatureState)10:
                    {
                        levitateState(); //10 = levitate -- UNIQUE* to the Cat - goto place, play animation
                        break;
                    }
                case (CreatureState)11:
                    {
                        angerState(); //7 = anger -- UNIQUE* to the Rabbit - find tree, punch it (via anitmaiton)
                        break;
                    }
            }
        }
        

    }

    void UpdateTimeState()
    {
        var timeState = lightingManager.GetComponent<LightingManager>().currentQuater;

        
        float tempDistance = Vector3.Distance(this.transform.position, PlayerObject.transform.position);

        //Debug.Log("grounded = " + PlayerObject.GetComponent<CharacterController>().isGrounded);
        //if the distance between the player and the creature is less than the eye strength
        if (tempDistance < eyeStrength)
        {
            //peform a raycast from the creature to the player, if there is a hit, you can see
            //Vector3 direction = (tempPlayer.transform.position - transform.position).normalized;
            //Physics.Raycast(transform.position, direction, out hit)

            
            //peform a raycast from the creature forwards, if the player is hit, it can see it
            RaycastHit hit;
            //also dont do this check if the creature is sleeping or is doing an uninteruptable action
            if(Physics.Raycast(transform.position, transform.forward, out hit) &&
                info.agentState != CreatureState.Sleep
                && !startedUnInteruptable)
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
            //if the player is not on the ground or in a bush -- THIS WILL NEED TESTING on various terrain!
            if(!PlayerObject.GetComponent<CharacterController>().isGrounded
                && info.agentState != CreatureState.Sleep
                && !startedUnInteruptable
                && !PlayerObject.GetComponent<PlayerController3D>().inBush)
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
                    //revert animation back to default
                    //PlayFinishedAnimation();
                }
                    
            }
            else //you are not a passive creature so you will FLEEEEEEEEEEEEEE
            {
                if (info.agentState != (CreatureState)0)
                {
                    info.agentState = (CreatureState)0; //agent flees
                    stateJustChanged = true;
                    //revert animation back to default
                    //PlayFinishedAnimation();
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon - Fish continue to sleep
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                case 2: //Dog
                    {
                        //if it is LateNight - Dog should continue to sleep
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                case 3: //Tiger
                    {
                        //if it is LateNight - Tiger continue to be asleep
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

                        //if it is Morning - Tiger should eat
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon - Tiger should go ROAAAAAAAAAAAAAAAAAAAAAR
                        if ((int)timeState == 2)
                        {
                            if (info.agentState != (CreatureState)6)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)6; //agent roars - UNIQUE
                                if (UniqueLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, UniqueLocations.Length);
                                }

                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;

                        }
                        //if it is Night - Tiger should sleep
                        if ((int)timeState == 3)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                case 4: //Dragon
                    {
                        //if it is LateNight - Dragon should be asleep
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

                        //if it is Morning - Dragon should eat
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon - Dragon should do a belly flop
                        if ((int)timeState == 2)
                        {
                            if (info.agentState != (CreatureState)7)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)7; //agent roars - UNIQUE
                                if (UniqueLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, UniqueLocations.Length);
                                }

                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;

                        }
                        //if it is Night - Dragon should sleep
                        if ((int)timeState == 3)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                case 5: //Cow
                    {
                        //if it is LateNight - Cow should roll
                        if ((int)timeState == 0)
                        {
                            if (info.agentState != (CreatureState)8)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)8; //agent goes rolls                                
                                if (UniqueLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, UniqueLocations.Length);
                                }
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }

                        //if it is Morning - Cow should sleep
                        if ((int)timeState == 1)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps                               
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon - Cow should continue to sleep
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
                        //if it is Night - Dragon should go and eat
                        if ((int)timeState == 3)
                        {
                            if (info.agentState != (CreatureState)3)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)3; //agent sleeps
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                case 6: //Duck
                    {
                        //if it is LateNight - Duck should continue to be asleep
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

                        //if it is Morning - Duck should eat
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon - Duck should peck
                        if ((int)timeState == 2)
                        {
                            if (info.agentState != (CreatureState)9)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)9; //agent pecks - UNIQUE
                                if (UniqueLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, UniqueLocations.Length);
                                }

                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;

                        }
                        //if it is Night - Duck should sleep
                        if ((int)timeState == 3)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                case 7: //Cat
                    {
                        //if it is LateNight - Cat should levitate
                        if ((int)timeState == 0)
                        {
                            if (info.agentState != (CreatureState)10)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)10; //agent levitates                                
                                if (UniqueLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, UniqueLocations.Length);
                                }
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }

                        //if it is Morning - Cat should sleep
                        if ((int)timeState == 1)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps                               
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon - Cat should continue to sleep
                        if ((int)timeState == 2)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent roars - UNIQUE                                
                                stateJustChanged = true;
                                break;
                            }
                            break;

                        }
                        //if it is Night - cat should go and eat
                        if ((int)timeState == 3)
                        {
                            if (info.agentState != (CreatureState)3)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)3; //agent sleeps
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
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                case 8: //Rabbit
                    {
                        //if it is LateNight - Rabbit should eat
                        if ((int)timeState == 0)
                        {
                            if (info.agentState != (CreatureState)3)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)3; //agent eats                               
                                if (FoodLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0, FoodLocations.Length);
                                }
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }
                            break;
                        }

                        //if it is Morning - Rabbit should sleep
                        if ((int)timeState == 1)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent sleeps                               
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
                                break;
                            }

                            break;
                        }

                        //if it is Afternoon - Rabbit should continue to sleep
                        if ((int)timeState == 2)
                        {
                            if (info.agentState != (CreatureState)2)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)2; //agent roars - UNIQUE                                
                                stateJustChanged = true;
                                break;
                            }
                            break;

                        }
                        //if it is Night - Rabbit should go and punch a tree
                        if ((int)timeState == 3)
                        {
                            if (info.agentState != (CreatureState)11)
                            {
                                //PreviousState = info.agentState; //assign the previous state
                                info.agentState = (CreatureState)11; //agent sleeps is ANGER
                                //if the size of the foodlocations is less than 1
                                if (UniqueLocations.Length <= 1)
                                {
                                    randomLocation = 0;
                                }
                                else
                                {
                                    randomLocation = Random.Range(0,  UniqueLocations.Length);
                                }
                                stateJustChanged = true;
                                //revert animation back to default
                                //PlayFinishedAnimation();
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
            PlayFinishedAnimation();
            navMeshAgent.speed = runSpeed;
            stateJustChanged = false;
            reachedDestination = true;
            startedUnInteruptable = false;
            roarIterator = 0;
            PlayAlertScaredAnimation();
            StopFinishedAnimation();

            var tempSound = soundObject.GetComponent<SoundStorage>();
            

            tempSound.playSound(tempSound.StartledSound, this.transform.position);
            //ensure looping creature sounds are not playing
            if(tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if(tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
            
        }
        //play the scared animation
       
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
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = true;
            startedUnInteruptable = false;
            roarIterator = 0;
            PlayAlertPassiveAnimation();
            StopFinishedAnimation();
            var tempSound = soundObject.GetComponent<SoundStorage>();
            tempSound.playSound(tempSound.CuriousSound, this.transform.position);

            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
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
        var tempSound = soundObject.GetComponent<SoundStorage>();

        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            stateJustChanged = false;
            reachedDestination = true;
            startedUnInteruptable = false;
            roarIterator = 0;
            PlaySleepAnimation();
            //StopFinishedAnimation();
            //var tempSound = soundObject.GetComponent<SoundStorage>();
            tempSound.playSound(tempSound.SleepSound, this.transform.position);
            //make sure eat sound is not playing
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            

        }
        else
        {
            StopFinishedAnimation();
        }

        
        tempSound.playSound(tempSound.SleepSound, this.transform.position);

        //need a way to apply this just once -- BINGO
        transform.rotation *= Quaternion.Euler(0, 0, 90.0f);

        Vector3 tempLocation = this.transform.position;
        navMeshAgent.SetDestination(tempLocation);
    }

    void eatState()
    {
        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = false;
            startedUnInteruptable = false;
            roarIterator = 0;

            //make sure sleep sound is not playing
            var tempSound = soundObject.GetComponent<SoundStorage>();
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
        }
        

        Vector3 tempLocation = FoodLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);
        //Debug.Log("Eat state, going to " + tempLocation);

        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if(distance <= 2)
        {
            var tempSound = soundObject.GetComponent<SoundStorage>();

            if (!reachedDestination)
            {
                PlayEatAnimation();
                StopFinishedAnimation();
                
                
            }
            reachedDestination = true;

            RotateTowards(tempLocation);
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");

            tempSound.playSound(tempSound.EatSound, this.transform.position);
        }
    }

    // a unique behaviour to the FISH, which has it go to an empty gameobject near a water source
    void washFaceState()
    {
        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = false;
            var tempSound = soundObject.GetComponent<SoundStorage>();
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);

        //Debug.Log("Wash state, going to " + tempLocation);
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 2)
        {
            if(!reachedDestination)
            {
                PlaySplashAnimation();
                StopFinishedAnimation();
                //Debug.Log("" + this.gameObject + " now washing face!");
            }
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);            
            reachedDestination = true;
            //probably play wash face animation here
            
        }
    }

    //a unique behaviour to the DOG, in which it goes to a set location and then other locations
    void chaseTailState()
    {
        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = false;
            secondaryLocationReached = 0;
            startedUnInteruptable = false;
            var tempSound = soundObject.GetComponent<SoundStorage>();
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        if (!reachedDestination)
        {
            navMeshAgent.SetDestination(tempLocation);            
        }
       
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 2)
        {
            if(!reachedDestination)
            {
                Debug.Log("" + this.gameObject + "now chasing tail!");
                PlayTailChaseAnimation();
                StopFinishedAnimation();
            }

            //set the new position to go to be the spot you are standing
            //navMeshAgent.SetDestination(this.transform.position);
            reachedDestination = true;

            //probably play chase tail animation here -- MAYBE apply rotations?
            
        }

        //if the dog reached the starting point
        if(reachedDestination)
        {            
            //get the next location
            Vector3 nextLocation = UniqueSecondaryLocations[secondaryLocationReached].transform.position;
            navMeshAgent.SetDestination(nextLocation);

            startedUnInteruptable = true;

            //if the distance between the new position and the object is less than 2
            float nextDistance = Vector3.Distance(nextLocation, transform.position);
            if (nextDistance <= 2)
            {
                //if the length of secondary locations is bigger than the location we have reached
                if(UniqueSecondaryLocations.Length - 1 > secondaryLocationReached)
                {
                    secondaryLocationReached++;
                }
                else
                {
                    secondaryLocationReached = 0;
                }
            }
        }
    }

    //a unique behaviour to the tiger, which it goes to a set location and plays an animation
    void roarState()
    {
        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = false;
            startedUnInteruptable = false;
            roarIterator = 0;
            soundRandom = Random.Range(0, 2);
            var tempSound = soundObject.GetComponent<SoundStorage>();
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);

        //Debug.Log("roar state, going to " + tempLocation);
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 1)
        {
            if(!reachedDestination)
            {
                PlayRoarAnimation();
                //StopFinishedAnimation();
                Debug.Log("" + this.gameObject + " arrived at position number " + UniqueLocations[randomLocation]);
                //play roar sound here
                var tempSound = soundObject.GetComponent<SoundStorage>();
                tempSound.playSound(tempSound.TigerUniqueSound[soundRandom], this.transform.position);
            }
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");
            
            reachedDestination = true;
            startedUnInteruptable = true;
            //probably play roar animation/sound here - maybe a check to get here first to play sound?
            
        }

        //if you reached the destination, wait until the duration of the roar is over
        if(reachedDestination)
        {
            roarIterator += Time.deltaTime;
            //if the iterator is bigger than the timer
            if(roarIterator > roarDuration)
            {               
                //set a new random location
                if (UniqueLocations.Length <= 1)
                {
                    randomLocation = 0;
                }
                else
                {
                    randomLocation = Random.Range(0, UniqueLocations.Length);
                }
                reachedDestination = false;
                startedUnInteruptable = false;
                roarIterator -= roarDuration;
                soundRandom = Random.Range(0, 2);
                Debug.Log("" + this.gameObject + " roar finished, changing location to " + UniqueLocations[randomLocation]);
            }
        }
    }

    //a unique behaviour to the dragon, which it goes to start- speeds up, goes to locations in order (cannot be interupted by player)
    void bellyflopState()
    {
        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = false;
            secondaryLocationReached = 0;
            startedUnInteruptable = false;
            var tempSound = soundObject.GetComponent<SoundStorage>();
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);

        //Debug.Log("roar state, going to " + tempLocation);
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 2)
        {
            //modify speed
            if(!reachedDestination)
            {
                startedUnInteruptable = true;
                navMeshAgent.speed = runSpeed;
                PlaySlideStartAnimation();
                StopFinishedAnimation();
                Debug.Log("start runing " + this.gameObject);

            }

            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");
            reachedDestination = true;
            //probably play bellyflop animation/sound here - maybe a check to get here first to play sound?
        }

        //if the dragon reached the starting point
        if (reachedDestination)
        {
            //get the next location
            Vector3 nextLocation = UniqueSecondaryLocations[secondaryLocationReached].transform.position;
            navMeshAgent.SetDestination(nextLocation);

            //if the distance between the new position and the object is less than 2
            float nextDistance = Vector3.Distance(nextLocation, transform.position);
            if (nextDistance <= 2)
            {
                //if the secondary location is 0
                if (secondaryLocationReached == 0)
                {
                    //reset speed and play flop animation
                    navMeshAgent.speed = speed;
                    PlaySlideContinueAnimation();
                    //StopFinishedAnimation();
                    Debug.Log("FLOP! " + this.gameObject);
                }

                //if the length of secondary locations is bigger than the location we have reached
                if (UniqueSecondaryLocations.Length - 1 > secondaryLocationReached)
                {
                    secondaryLocationReached++;
                }
                else
                {
                    Debug.Log("Make secondary location path bigger for " + this.gameObject);
                }
                
                
                

                //probably play slide animation here -- MAYBE apply rotations?
            }
        }
    }

    //a unique behaviour to the cow, which it moves similar to the dog
    void rollState()
    {
        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = false;
            roarIterator = 0;
            var tempSound = soundObject.GetComponent<SoundStorage>();
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);

        //Debug.Log("roll state, going to " + tempLocation);
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 2)
        {
            if (!reachedDestination)
            {
                PlayRollAnimation();
                //StopFinishedAnimation();
                Debug.Log("" + this.gameObject + " arrived at position number " + UniqueLocations[randomLocation]);
                //play roar sound here
            }
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");

            reachedDestination = true;
            startedUnInteruptable = true;
            //probably play roar animation/sound here - maybe a check to get here first to play sound?

        }

        //if you reached the destination, wait until the duration of the roar is over
        if (reachedDestination)
        {
            roarIterator += Time.deltaTime;
            //if the iterator is bigger than the timer
            if (roarIterator > roarDuration)
            {
                //set a new random location
                if (UniqueLocations.Length <= 1)
                {
                    randomLocation = 0;
                }
                else
                {
                    randomLocation = Random.Range(0, UniqueLocations.Length);
                }
                reachedDestination = false;
                startedUnInteruptable = false;
                roarIterator -= roarDuration;
                Debug.Log("" + this.gameObject + " roll finished, changing location to " + UniqueLocations[randomLocation]);
            }
        }
    }

    //a unique behaviour to the duck, which it moves like the fish
    void peckState()
    {
        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = false;
            var tempSound = soundObject.GetComponent<SoundStorage>();
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);

        //Debug.Log("roar state, going to " + tempLocation);
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 2)
        {
            if(!reachedDestination)
            {
                PlayPeckAnimation();
                StopFinishedAnimation();
                Debug.Log("" + this.gameObject + " starting pecking");
            }

            reachedDestination = true;
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");

            
        }
    }

    //a unique behaviour to the cat, which it moves like the fish/duck
    void levitateState()
    {
        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = false;
            var tempSound = soundObject.GetComponent<SoundStorage>();
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);

        //Debug.Log("roar state, going to " + tempLocation);
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 2)
        {
            if (!reachedDestination)
            {
                PlayLevitateAnimation();
                StopFinishedAnimation();
                Debug.Log("" + this.gameObject + " starting levitating");
            }
            reachedDestination = true;
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");

            
        }
    }

    //a unique behaviour to the rabbit, which it moves like the tiger
    void angerState()
    {
        if (stateJustChanged)
        {
            PlayFinishedAnimation();
            navMeshAgent.speed = speed;
            //transform.rotation *= Quaternion.Euler(0, 0, 0);
            stateJustChanged = false;
            reachedDestination = false;
            var tempSound = soundObject.GetComponent<SoundStorage>();
            if (tempSound.EatSound.isPlaying)
            {
                tempSound.stopSound(tempSound.EatSound);
            }
            if (tempSound.SleepSound.isPlaying)
            {
                tempSound.stopSound(tempSound.SleepSound);
            }
        }

        Vector3 tempLocation = UniqueLocations[randomLocation].transform.position;
        navMeshAgent.SetDestination(tempLocation);

        
        //if you get close enough to your destination
        float distance = Vector3.Distance(tempLocation, transform.position);
        if (distance <= 2)
        {
            if(!reachedDestination)
            {
                PlayPunchAnimation();
                StopFinishedAnimation();
                Debug.Log("" + this.gameObject + " reached tree");
            }

            reachedDestination = true;
            startedUnInteruptable = true;
            //set the new position to go to be the spot you are standing
            navMeshAgent.SetDestination(this.transform.position);
            //Debug.Log("" + this.gameObject + " reached a bush");

            //probably play punch animation/sound here - maybe a check to get here first to play sound?
           
        }

        //if you reached the destination, wait until the duration of the punch is over
        if (reachedDestination)
        {
            RotateTowards(tempLocation);
            roarIterator += Time.deltaTime;
            //if the iterator is bigger than the timer
            if (roarIterator > roarDuration)
            {
                //set a new random location
                if (UniqueLocations.Length <= 1)
                {
                    randomLocation = 0;
                }
                else
                {
                    randomLocation = Random.Range(0, UniqueLocations.Length);
                }
                reachedDestination = false;
                startedUnInteruptable = false;
                roarIterator -= roarDuration;
                Debug.Log("" + this.gameObject + " punch finished, changing location to " + UniqueLocations[randomLocation]);
            }
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



    //Animation functions below!!! ----------------------------------------------------------------------


    //finish whatever you are doing (transition back to walking after time changes)
    public void PlayFinishedAnimation()
    {
        Debug.Log("" + this.gameObject + " finished an Animation");
        creatureAnimator.SetTrigger("FinishedTrigger");
    }

    //call this after play finished animation to prevent bugs?
    public void StopFinishedAnimation()
    {
        //Debug.Log("" + this.gameObject + " clearing FinishedTrigger");
        creatureAnimator.ResetTrigger("FinishedTrigger");
    }

    //eat animation (shared by all creatures) - needs to be turned off
    public void PlayEatAnimation()
    {
        Debug.Log("" + this.gameObject + " started Eat Animation");
        creatureAnimator.SetTrigger("EatTrigger");
    }    

    //sleep animation (shared by all creatures) - needs to be turned off
    public void PlaySleepAnimation()
    {
        Debug.Log("" + this.gameObject + " started sleep Animation");
        creatureAnimator.SetTrigger("SleepTrigger");
    }

    //passive (shared by half creatures) - needs to be turned off
    public void PlayAlertPassiveAnimation()
    {
        Debug.Log("" + this.gameObject + " started passive Animation");
        creatureAnimator.SetTrigger("PassiveTrigger");
    }

    //scared (shared by half creatures, handles transition to run) - needs to be turned off
    public void PlayAlertScaredAnimation()
    {
        Debug.Log("" + this.gameObject + " started scared Animation");
        creatureAnimator.SetTrigger("ScaredTrigger");
    }

    //fish splash animation - needs to be turned off
    public void PlaySplashAnimation()
    {
        Debug.Log("" + this.gameObject + " started splash Animation");
        creatureAnimator.SetTrigger("SplashTrigger");
    }

    //dog notice tail animation (just the start, animator handles transition to run, - needs to be turned off)
    public void PlayTailChaseAnimation()
    {
        Debug.Log("" + this.gameObject + " started tail chase Animation");
        creatureAnimator.SetTrigger("ChaseTrigger");
    }

    //tiger roar animation - (animator handles end transition)
    public void PlayRoarAnimation()
    {
        Debug.Log("" + this.gameObject + " started roar Animation");
        creatureAnimator.SetTrigger("RoarTrigger");
    }

    //dragon slide - just the start (run)
    public void PlaySlideStartAnimation()
    {
        Debug.Log("" + this.gameObject + " started slide Animation");
        creatureAnimator.SetTrigger("SlideTrigger");
    }

    //dragon slide (run to slide - needs to be turned off)
    public void PlaySlideContinueAnimation()
    {
        Debug.Log("" + this.gameObject + " started slide Animation");
        creatureAnimator.SetTrigger("Slide2Trigger");
    }

    //cow roll - needs to be turned off
    public void PlayRollAnimation()
    {
        Debug.Log("" + this.gameObject + " started roll Animation");
        creatureAnimator.SetTrigger("RollTrigger");
    }

    //duck peck - needs to be turned off
    public void PlayPeckAnimation()
    {
        Debug.Log("" + this.gameObject + " started peck Animation");
        creatureAnimator.SetTrigger("PeckTrigger");
    }

    //cat levitate - needs to be turned off
    public void PlayLevitateAnimation()
    {
        Debug.Log("" + this.gameObject + " started levitate Animation");
        creatureAnimator.SetTrigger("LevitateTrigger");
    }

    //rabbit punch - (animator handles end transition)
    public void PlayPunchAnimation()
    {
        Debug.Log("" + this.gameObject + " started punch Animation");
        creatureAnimator.SetTrigger("PunchTrigger");
    }
}
