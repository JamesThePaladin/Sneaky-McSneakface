using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{

    private NoiseMaker playerNoise; //variable for player volume
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
        Death,
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
                if (CanSee(GameManager.instance.player) == true) 
                {
                    ChangeState(State.Chase);
                }
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                pawn.Attack();
                break;
            case State.Death:
                pawn.Death();
                break;
        }

    }
    public void ChangeState(State newState)
    {
        // Change our currentState
        currentState = newState;
        Debug.Log("");
    }
    public void DoIdle()
    {
        // Do Nothing!
    }

    //bool for AI hearing
    public bool CanHear(GameObject player)
    {
        //if player has no Noise Maker
        if (playerNoise == null)
        {
            return false;
        }
        //otherwise
        else
        {
            //adjust for distance loss
            float newVolume = playerNoise.volume - volumeLoss;
            //if new volume is greater than the alert threshold
            if (newVolume > alertLevel)
            {
                //return true
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

    //bool for FOV
    public bool CanSee(GameObject player)
    {
        //get the player's transform
        playerTf = player.GetComponent<Transform>();
        // Find the vector from the agent to the target
        Vector3 agentToPlayerVector = playerTf.position - GetComponent<Transform>().position;

        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToPlayer = Vector3.Angle(agentToPlayerVector, transform.forward);
        // if that angle is less than our field of view
        if (angleToPlayer < fieldOfView)
        {
            // Raycast
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, agentToPlayerVector);

            //if the first object hit is the player
            if (hitInfo.collider.gameObject == player)
            {
                //return true
                return true;
            }
        }
        //this will only run if nothing is hit or if something is hit that isnt the player
        return true;
    }
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
        if (Vector2.Distance(transform.position, playerTf.position) > stopDistance)
        {
            //move towards player at a rate of speed * the amount of time since the last frame draw
            transform.position = Vector2.MoveTowards(transform.position, playerTf.position, pawn.speed * Time.deltaTime);
        }
    }

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
        if (Vector3.Distance(transform.position, pawn.targetTf.position) < pawn.closeEnough)
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

        CanHear(GameManager.instance.player);
        CanSee(GameManager.instance.player);
    }
}
