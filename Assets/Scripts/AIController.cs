using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{

    private NoiseMaker playerNoise; //variable for player noise maker component
    public float stopDistance; //variable for enemy stopping distance

    public float volumeLoss; //volume lost for distance
    public float alertLevel; //for enemy volume alert threshold
    public float hearingDistance; //how far pawn can hear
    
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
        //******************************
        //TODO fix statement or functions causing issues. Might be related to attack and chase ranges.
        //DO NOT FORGET you also have a GameManager FSM you have to make to handle the menu screens or something
        //******************************

        switch (currentState)
        {
            case State.Idle:
                DoIdle();
                break;
            case State.Patrol:
                Patrol();
                //check to see if player can be heard
                //if (CanHear())
                //{
                //    //to check if its working
                //    Debug.Log("I can hear you!");
                //    //look at player
                //    pawn.LookTowards();
                //    //check to see if player can be seen
                //    if (CanSee(GameManager.instance.player))
                //    {
                //        ChangeState(State.Chase);
                //    }
                //}
                //check to see if player can be seen
                if (CanSee(GameManager.instance.player))
                {
                    ChangeState(State.Chase);
                }
                break;
            case State.Chase:
                Chase();
                //check to see if we are within attack range
                Debug.Log(Vector3.Distance(transform.position, GameManager.instance.player.transform.position));
                Debug.Log(CanSee(GameManager.instance.player));
                if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) < pawn.attackRange && CanSee(GameManager.instance.player))
                {
                    //attack
                    Debug.Log("Attacking!");
                    ChangeState(State.Attack);
                }
                //if player moves out of range
                else if (!CanSee(GameManager.instance.player))
                {
                    //go back to patrolling
                    Debug.Log("Going to idle");
                    ChangeState(State.Idle);
                }
                break;
            case State.Attack:
                pawn.Attack();
                if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) > pawn.attackRange) 
                {
                    Debug.Log("transitioning from attack to chase");
                    ChangeState(State.Chase);
                }
                break;
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
        StartCoroutine(Wait());
        IEnumerator Wait() 
        {
            yield return new WaitForSeconds(3f);
            ChangeState(State.Patrol);
        }
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
        }
    }

    //method for chase state
    public void Chase()
    {
        //set target to player
        pawn.targetTf = GameManager.instance.player.transform;

        if (Vector2.Distance(pawn.transform.position, GameManager.instance.player.transform.position) < pawn.attackRange)
        {
            //move pawn by 1
            pawn.Move(0);
            //towards the waypoint
            pawn.LookTowards();
        }
    }


    //******************
    //TODO fix this
    //******************

    //bool for AI hearing
    public bool CanHear()
    {
        //if player has no Noise Maker
        if (playerNoise == null)
        {
            return false;
        }

        if (Vector3.Distance(pawn.transform.position, GameManager.instance.player.transform.position) < hearingDistance + GameManager.instance.player.GetComponent<Pawn>().noiseDistance) 
        {
            if (playerNoise.volume > alertLevel) 
            {
                Debug.Log("I can hear you");
                return true;
            }
        }
        return false;
    }

    public bool CanSee(GameObject player)
    {
        // Find the vector from the agent to the target
        // We do this by subtracting "destination minus origin", so that "origin plus vector equals destination."
        Vector3 agentToPlayerVector = player.transform.position - pawn.transform.position;

        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToPlayer = Vector3.Angle(agentToPlayerVector, pawn.transform.right);

        // if that angle is less than our field of view
        if (angleToPlayer < pawn.viewRadius)
        {
            if (Vector3.Distance(pawn.transform.position, player.transform.position) < pawn.fieldOfView / 2)
            {
                // Raycast
                RaycastHit2D hitInfo = Physics2D.Raycast(pawn.transform.position, agentToPlayerVector, pawn.viewRadius);
                // If the first object we hit is our target 
                if (hitInfo.collider.gameObject == player)
                {
                    // return true 
                    return true;
                }
                else 
                {
                    return false;
                }
            } 
        }
        return false;
    }
}
