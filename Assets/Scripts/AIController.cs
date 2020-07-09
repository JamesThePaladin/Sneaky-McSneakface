using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{

    private NoiseMaker playerNoise; //variable for player noise maker component
    private Transform playerTf; //variable for player's transform
    public float stopDistance; //variable for enemy stopping distance

    public float volumeLoss; //volume lost for distance
    public float fieldOfView; //for Ai field of view
    public float alertLevel; //for enemy volume alert threshold


    public List<Transform> waypoints; //waypoint transforms
    public int patrolIndex; //current patrol waypoint index

    //enum for AI states
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
    }

    public State currentState;

    // Start is called before the first frame update
    void Awake()
    {
        playerNoise = GameManager.instance.player.GetComponent<NoiseMaker>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                DoIdle();
                break;
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                Debug.Log("Chasing!");
                if (Vector2.Distance(transform.position, playerTf.position) <= pawn.attackRange)
                {
                    Debug.Log("Attacking!");
                    ChangeState(State.Attack);
                }
                else if (Vector2.Distance(transform.position, playerTf.position) > pawn.chaseRange)
                {
                    Debug.Log("I lost it");
                    ChangeState(State.Patrol);
                }
                break;
            case State.Attack:
                pawn.Attack();
                break;
        }
    }

    public void FixedUpdate()
    {
        //get the player's transform
        if (playerTf == null) 
        {
            playerTf = GameManager.instance.player.GetComponent<Transform>(); 
        }
    }

    //state change helper method
    public void ChangeState(State newState)
    {
        // Change our currentState
        currentState = newState;
    }

    public void DoIdle()
    {
        //Do Nothing
    }

    //method for patrol state
    public void Patrol()
    {
        //set target to waypoint transform
        pawn.targetTf = waypoints[patrolIndex];

        //if target (waypoint) has a transform
        if (pawn.targetTf != null)
        {
            //move pawn by 1
            pawn.Move(1);
            //towards the waypoint
            pawn.LookTowards();
        }

        //if the pawn is close enough
        if (Vector3.Distance(pawn.transform.position, pawn.targetTf.position) < pawn.closeEnough)
        {
            //and if the patrol index hasn't been completed
            if (patrolIndex < waypoints.Count - 1)
            {
                //move to the next patrol waypoint
                patrolIndex++;
            }

            else
            {
                //if it has reset index
                patrolIndex = 0;
            }

            //check to see if player can be heard
            if (CanHear())
            {
                //to check if its working
                Debug.Log("I can hear you!");
                //look at player
                pawn.LookTowards();
                //check to see if player can be seen
                ChangeState(State.Chase);
            //    if (CanSee())
            //    {
            //        Debug.Log("I can see you!");
            //        ChangeState(State.Chase);
            //    }
            //}
            ////check to see if player can be seen
            //if (CanSee())
            //{
            //    Debug.Log("I can see you!");
            //
                //ChangeState(State.Chase);

            //TODO fix CanSee() method and stuff
            }
        }
    }

    //method for chase state
    public void Chase()
    {
        //if player is empty, because they got destroyed
        if (GameManager.instance.player == null)
        {
            //fill with current player
            GameManager.instance.player = GameObject.FindWithTag("Player");
            //get target transform
            playerTf = GameManager.instance.player.GetComponent<Transform>();
        }

        //if
        if (Vector2.Distance(pawn.transform.position, playerTf.position) > stopDistance)
        {
            //move towards player at a rate of speed * the amount of time since the last frame draw
            pawn.transform.position = Vector2.MoveTowards(pawn.transform.position, playerTf.position, pawn.speed * Time.deltaTime);
            //look at player
            pawn.transform.LookAt(playerTf, Vector3.up);
        }
    }

    //bool for AI hearing
    public bool CanHear()
    {
        //if player has no Noise Maker
        if (playerNoise == null)
        {
            return false;
        }
        //otherwise
        else
        {
            //if new volume is greater than the alert threshold
            if (playerNoise.volume > alertLevel)
            {
                
                return true;
            }
            //if not
            else
            {
                //return false
                return false;
            }
        }
    }

    public bool CanSee(GameObject target)
    {
        // We use the location of our target in a number of calculations - store it in a variable for easy access.

        // Find the vector from the agent to the target
        // We do this by subtracting "destination minus origin", so that "origin plus vector equals destination."
        Vector3 agentToTargetVector = playerTf.position - pawn.transform.position;

        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToPlayer = Vector3.Angle(agentToTargetVector, pawn.transform.forward);

        // if that angle is less than our field of view
        if (angleToPlayer < fieldOfView)
        {
            // Raycast
            RaycastHit2D hitInfo = Physics2D.Raycast(pawn.transform.position, agentToTargetVector);

            // If the first object we hit is our target 
            if (hitInfo.collider.gameObject == target)
            {
                // return true 
                return true;
            }
        }
        return false;
    }
}
